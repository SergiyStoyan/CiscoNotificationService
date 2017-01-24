using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.IO;
using LumiSoft.Net.RTP;
using LumiSoft.Net.Media.Codec.Audio;

namespace LumiSoft.Net.Media
{
    /// <summary>
    /// This class implements audio-out (eg. speaker,headphones) device RTP audio player.
    /// </summary>
    public class AudioOut_RTP : IDisposable
    {
        private bool                       m_IsDisposed      = false;
        private bool                       m_IsRunning       = false;
        private AudioOutDevice             m_pAudioOutDevice = null;
        private RTP_ReceiveStream          m_pRTP_Stream     = null;
        private Dictionary<int,AudioCodec> m_pAudioCodecs    = null;
        private AudioOut                   m_pAudioOut       = null;
        private AudioCodec                 m_pActiveCodec    = null;

        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <param name="audioOutDevice">Audio-out device used to play out RTP audio.</param>
        /// <param name="stream">RTP receive stream which audio to play.</param>
        /// <param name="payloads2codec">Audio codecs with RTP payload number. For example: 0-PCMU,8-PCMA.</param>
        /// <exception cref="ArgumentNullException">Is raised when <b>audioOutDevice</b>,<b>stream</b> or <b>codecs</b> is null reference.</exception>
        public AudioOut_RTP(AudioOutDevice audioOutDevice,RTP_ReceiveStream stream,Dictionary<int,AudioCodec> payloads2codec)
        {
            if(audioOutDevice == null){
                throw new ArgumentNullException("audioOutDevice");
            }
            if(stream == null){
                throw new ArgumentNullException("stream");
            }
            if(payloads2codec == null){
                throw new ArgumentNullException("payloads2codec");
            }

            m_pAudioOutDevice = audioOutDevice;
            m_pRTP_Stream     = stream;
            m_pAudioCodecs    = payloads2codec;
        }

    #region method Dispose

    /// <summary>
    /// Cleans up any resource being used.
    /// </summary>
    public void Dispose()
        {
            if(m_IsDisposed){
                return;
            }

            Stop();

            this.Error        = null;
            m_pAudioOutDevice = null;
            m_pRTP_Stream     = null;
            m_pAudioCodecs    = null;
            m_pActiveCodec    = null;
        }

        #endregion


        #region Events handling

        #region method m_pRTP_Stream_PacketReceived

        /// <summary>
        /// This method is called when new RTP packet received.
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="e">Event data.</param>
        private void m_pRTP_Stream_PacketReceived(object sender, RTP_PacketEventArgs e)
        {
            if (m_IsDisposed)
            {
                return;
            }

            try
            {
                AudioCodec codec = null;
                if (!m_pAudioCodecs.TryGetValue(e.Packet.PayloadType, out codec))
                {
                    // Unknown codec(payload value), skip it.

                    return;
                }
                m_pActiveCodec = codec;

                // Audio-out not created yet, create it.
                if (m_pAudioOut == null)
                {
                    m_pAudioOut = new AudioOut(m_pAudioOutDevice, codec.AudioFormat, volume100);
                }
                // Audio-out audio format not compatible to codec, recreate it.
                else if (!m_pAudioOut.AudioFormat.Equals(codec.AudioFormat))
                {
                    m_pAudioOut.Dispose();
                    m_pAudioOut = new AudioOut(m_pAudioOutDevice, codec.AudioFormat, volume100);
                }

                // Decode RTP audio frame and queue it for play out.
                byte[] decodedData = codec.Decode(e.Packet.Data, 0, e.Packet.Data.Length);
                m_pAudioOut.Write(decodedData, 0, decodedData.Length);

                OutputWavFile of;
                if (codecs2OutputWavFile.TryGetValue(codec, out of))
                {
                    of.BW.Write(decodedData, 0, decodedData.Length);
                    of.SampleCount += decodedData.Length;
                }
            }
            catch (Exception x)
            {
                if (!this.IsDisposed)
                {
                    // Raise error event(We can't throw expection directly, we are on threadpool thread).
                    OnError(x);
                }
            }
        }

        #endregion

        #endregion

        #region method Start

        /// <summary>
        /// Starts receiving RTP audio and palying it out.
        /// </summary>
        /// <param name="volume100">Volume level between 0 and 100.</param>
        /// <param name="codecs2output_wav_file">Audio codecs to output file if any</param>
        /// <exception cref="ObjectDisposedException">Is raised when this object is disposed and this method is accessed.</exception>
        public bool Start(uint? volume100 = null, Dictionary<AudioCodec, string> codecs2output_wav_file = null)
        {
            if (this.IsDisposed) {
                throw new ObjectDisposedException(this.GetType().Name);
            }
            if (m_IsRunning) {
                return false;
            }

            m_IsRunning = true;
            open_output_wav_files(codecs2output_wav_file);
            this.volume100 = volume100;
            m_pRTP_Stream.PacketReceived += new EventHandler<RTP_PacketEventArgs>(m_pRTP_Stream_PacketReceived);
            return true;
        }
        uint? volume100 = null;
        readonly Dictionary<AudioCodec, OutputWavFile> codecs2OutputWavFile = new Dictionary<AudioCodec, OutputWavFile>();
        class OutputWavFile
        {
            internal BinaryWriter BW;
            internal int SampleCount;
        }

        void open_output_wav_files(Dictionary<AudioCodec, string> codecs2output_wav_file)
        {
            if (codecs2output_wav_file == null)
                return;

            foreach (AudioCodec ac in m_pAudioCodecs.Values)
            {
                string output_file;
                if (!codecs2output_wav_file.TryGetValue(ac, out output_file))
                    continue;
                if (!Regex.IsMatch(output_file, @"\.wav$", RegexOptions.IgnoreCase))
                    output_file += ".wav";
                BinaryWriter bw = new BinaryWriter(File.Create(output_file));

                char[] Riff = { 'R', 'I', 'F', 'F' };
                char[] Wave = { 'W', 'A', 'V', 'E' };
                char[] Fmt = { 'f', 'm', 't', ' ' };
                char[] Data = { 'd', 'a', 't', 'a' };
                short padding = 1;
                int formatLength = 0x10;
                int length = 0; // fill this in later!
                short shBytesPerSample = 2; // changing the WaveFormat recording parameters will impact on this

                bw.Write(Riff);
                bw.Write(length);
                bw.Write(Wave);
                bw.Write(Fmt);
                bw.Write(formatLength);
                bw.Write(padding);
                bw.Write(ac.AudioFormat.Channels);
                bw.Write(ac.AudioFormat.SamplesPerSecond);
                var averageBytesPerSecond = ac.AudioFormat.Channels * ac.AudioFormat.SamplesPerSecond * ac.AudioFormat.SamplesPerSecond * (int)((float)ac.AudioFormat.BitsPerSample / 8);
                bw.Write(averageBytesPerSecond);
                bw.Write(shBytesPerSample);
                bw.Write(ac.AudioFormat.BitsPerSample);
                bw.Write(Data);
                bw.Write((int)0); // update sample later

                codecs2OutputWavFile[ac] = new OutputWavFile { BW = bw, SampleCount = 0 };
            }
        }

        void close_output_wav_files()
        {
            foreach (OutputWavFile of in codecs2OutputWavFile.Values)
            {
                of.BW.Seek(4, SeekOrigin.Begin);
                of.BW.Write(of.SampleCount + 36);
                of.BW.Seek(40, SeekOrigin.Begin);
                of.BW.Write(of.SampleCount);
                of.BW.Close();
            }
            codecs2OutputWavFile.Clear();
        }

        #endregion

        #region method Stop

        /// <summary>
        /// Stops receiving RTP audio and palying it out.
        /// </summary>
        /// <exception cref="ObjectDisposedException">Is raised when this object is disposed and this method is accessed.</exception>
        public void Stop()
        {
            if (this.IsDisposed) {
                throw new ObjectDisposedException(this.GetType().Name);
            }
            if (!m_IsRunning) {
                return;
            }

            m_IsRunning = false;

            m_pRTP_Stream.PacketReceived -= new EventHandler<RTP_PacketEventArgs>(m_pRTP_Stream_PacketReceived);

            if (m_pAudioOut != null) {
                m_pAudioOut.Dispose();
                m_pAudioOut = null;
            }

            close_output_wav_files();
        }

        #endregion


        #region Properties implementation

        /// <summary>
        /// Gets if this object is disposed.
        /// </summary>
        public bool IsDisposed
        {
            get{ return m_IsDisposed; }
        }

        /// <summary>
        /// Gets if audio player is running.
        /// </summary>
        /// <exception cref="ObjectDisposedException">Is raised when this object is disposed and this property is accessed.</exception>
        public bool IsRunning
        {
            get{
                if(this.IsDisposed){
                    throw new ObjectDisposedException(this.GetType().Name);
                }

                return m_IsRunning; 
            }
        }

        /// <summary>
        /// Gets audio-out device is used to play out sound.
        /// </summary>
        /// <exception cref="ObjectDisposedException">Is raised when this object is disposed and this property is accessed.</exception>
        /// <exception cref="ArgumentNullException">Is raised when null reference passed.</exception>
        public AudioOutDevice AudioOutDevice
        {
            get{   
                if(this.IsDisposed){
                    throw new ObjectDisposedException(this.GetType().Name);
                }
                
                return m_pAudioOutDevice; 
            }

            set{
                if(this.IsDisposed){
                    throw new ObjectDisposedException(this.GetType().Name);
                }
                if(value == null){
                    throw new ArgumentNullException("AudioOutDevice");
                }

                m_pAudioOutDevice = value;

                if(this.IsRunning){
                    Stop();
                    Start();
                }
            }
        }

        /// <summary>
        /// Audio codecs with RTP payload number. For example: 0-PCMU,8-PCMA.
        /// </summary>
        /// <exception cref="ObjectDisposedException">Is raised when this object is disposed and this property is accessed.</exception>
        public Dictionary<int,AudioCodec> Codecs
        {
            get{
                if(this.IsDisposed){
                    throw new ObjectDisposedException(this.GetType().Name);
                }

                return m_pAudioCodecs; 
            }
        }

        /// <summary>
        /// Gets active audio codec. This value may be null if yet no data received from RTP.
        /// </summary>
        /// <remarks>Audio codec may change during RTP session, if remote-party(sender) changes it.</remarks>
        /// <exception cref="ObjectDisposedException">Is raised when this object is disposed and this property is accessed.</exception>
        public AudioCodec ActiveCodec
        {
            get{ 
                if(this.IsDisposed){
                    throw new ObjectDisposedException(this.GetType().Name);
                }

                return m_pActiveCodec; 
            }
        }

        #endregion

        #region Events implementation

        /// <summary>
        /// This method is raised when asynchronous thread Exception happens.
        /// </summary>
        public event EventHandler<ExceptionEventArgs> Error = null;

        #region method OnError

        /// <summary>
        /// Raises <b>Error</b> event.
        /// </summary>
        /// <param name="x">Error what happened.</param>
        private void OnError(Exception x)
        {
            if(this.Error != null){
                this.Error(this,new ExceptionEventArgs(x));
            }
        }

        #endregion

        #endregion
    }
}
