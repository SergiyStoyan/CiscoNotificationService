using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.Net;
using System.Threading;
using LumiSoft.Net.RTP;
using LumiSoft.Net.RTP.Debug;
using LumiSoft.Net.Media.Codec.Audio;
using LumiSoft.Net.Media;

namespace Rtp_Audio_Demo
{
    /// <summary>
    /// Application main window.
    /// </summary>
    public class wfrm_Main : Form
    {
        private TabControl    m_pTab              = null;
        private Label         mt_OutDevices       = null;
        private ComboBox      m_pOutDevices       = null;
        private GroupBox      m_pSeparator1       = null;
        private Label         mt_LocalEndPoint    = null;
        private ComboBox      m_pLocalIP          = null;
        private NumericUpDown m_pLocalPort        = null;
        private Button        m_pToggleRun        = null;
        private Label         mt_RemoteEndPoint   = null;
        private TextBox       m_pRemoteIP         = null;
        private NumericUpDown m_pRemotePort       = null;
        private CheckBox      m_pRecord           = null;
        private Label         mt_RecordFile       = null;
        private TextBox       m_pRecordFile       = null;
        private Button        m_pRecordFileBrowse = null;
        private GroupBox      m_pSeparator2       = null;
        private Label         mt_Codec            = null;
        private ComboBox      m_pCodec            = null;
        private Label         mt_Microphone       = null;
        private Button        m_pToggleMic        = null;
        private Label         mt_SendTestSound    = null;
        private Button        m_pSendTestSound    = null;
        private Button        m_pPlayTestSound    = null;

        private bool                       m_IsRunning     = false;
        private bool                       m_IsSendingTest = false;
        private RTP_MultimediaSession      m_pRtpSession   = null;
        private AudioOut                   m_pWaveOut      = null;
        private FileStream                 m_pRecordStream = null;
        private string                     m_PlayFile      = "";
        private Dictionary<int,AudioCodec> m_pAudioCodecs  = null;
        private AudioCodec                 m_pActiveCodec  = null;

        /// <summary>
        /// Default constructor.
        /// </summary>
        public wfrm_Main()
        {
            InitUI();

            LoadWaveDevices();

            m_pAudioCodecs = new Dictionary<int,AudioCodec>();
            m_pAudioCodecs.Add(0,new PCMU());
            m_pAudioCodecs.Add(8,new PCMA());

            m_pActiveCodec = new PCMA();

            m_pToggleRun_Click(null, null);
        }

        #region method InitUI

        /// <summary>
        /// Creates and initializes UI.
        /// </summary>
        private void InitUI()
        {
            this.ClientSize = new Size(400,400);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Text = "RTP audio demo";
            this.FormClosed += new FormClosedEventHandler(wfrm_Main_FormClosed);

            m_pTab = new TabControl();
            m_pTab.Size = this.ClientSize;
            m_pTab.Dock = DockStyle.Fill;
            m_pTab.Location = new Point(0,0);

            #region Tab page "Main"

            m_pTab.TabPages.Add("main","Main");

            mt_OutDevices = new Label();
            mt_OutDevices.Size = new Size(100,20);
            mt_OutDevices.Location = new Point(0,45);
            mt_OutDevices.TextAlign = ContentAlignment.MiddleRight;
            mt_OutDevices.Text = "Output device:";

            m_pOutDevices = new ComboBox();
            m_pOutDevices.Size = new Size(275,20);
            m_pOutDevices.Location = new Point(100,45);
            m_pOutDevices.DropDownStyle = ComboBoxStyle.DropDownList;

            m_pSeparator1 = new GroupBox();
            m_pSeparator1.Size = new Size(395,3);
            m_pSeparator1.Location = new Point(2,80);

            mt_LocalEndPoint = new Label();
            mt_LocalEndPoint.Size = new Size(100,20);
            mt_LocalEndPoint.Location = new Point(0,90);
            mt_LocalEndPoint.TextAlign = ContentAlignment.MiddleRight;
            mt_LocalEndPoint.Text = "Local EP:";

            m_pLocalIP = new ComboBox();
            m_pLocalIP.Size = new Size(135,20);
            m_pLocalIP.Location = new Point(100,90);
            m_pLocalIP.DropDownStyle = ComboBoxStyle.DropDownList;
            foreach(IPAddress ip in System.Net.Dns.GetHostAddresses("")){
                m_pLocalIP.Items.Add(ip.ToString());
            }
            m_pLocalIP.Items.Add("127.0.0.1");
            m_pLocalIP.SelectedIndex = 0;
                        
            m_pLocalPort = new NumericUpDown();
            m_pLocalPort.Size = new Size(60,20);
            m_pLocalPort.Location = new Point(240,90);
            m_pLocalPort.Minimum = 1;
            m_pLocalPort.Maximum = 99999;
            m_pLocalPort.Value = 11000;

            m_pToggleRun = new Button();
            m_pToggleRun.Size = new Size(70,20);
            m_pToggleRun.Location = new Point(305,90);
            m_pToggleRun.Text = "Start";
            m_pToggleRun.Click += new EventHandler(m_pToggleRun_Click);

            mt_RemoteEndPoint = new Label();
            mt_RemoteEndPoint.Size = new Size(100,20);
            mt_RemoteEndPoint.Location = new Point(0,115);
            mt_RemoteEndPoint.TextAlign = ContentAlignment.MiddleRight;
            mt_RemoteEndPoint.Text = "Remote EP:";

            m_pRemoteIP = new TextBox();
            m_pRemoteIP.Size = new Size(135,20);
            m_pRemoteIP.Location = new Point(100,115);

            m_pRemotePort = new NumericUpDown();
            m_pRemotePort.Size = new Size(60,20);
            m_pRemotePort.Location = new Point(240,115);
            m_pRemotePort.Minimum = 1;
            m_pRemotePort.Maximum = 99999;
            m_pRemotePort.Value = 10000;

            m_pRecord = new CheckBox();
            m_pRecord.Size = new Size(200,20);
            m_pRecord.Location = new Point(100,140);
            m_pRecord.Text = "Record incoming audio";
            m_pRecord.CheckedChanged += new EventHandler(m_pRecord_CheckedChanged);

            mt_RecordFile = new Label();
            mt_RecordFile.Size = new Size(100,20);
            mt_RecordFile.Location = new Point(0,160);
            mt_RecordFile.TextAlign = ContentAlignment.MiddleRight;
            mt_RecordFile.Text = "File Name:";

            m_pRecordFile = new TextBox();
            m_pRecordFile.Size = new Size(240,20);
            m_pRecordFile.Location = new Point(100,160);
            m_pRecordFile.Enabled = false;

            m_pRecordFileBrowse = new Button();
            m_pRecordFileBrowse.Size = new Size(30,20);
            m_pRecordFileBrowse.Location = new Point(345,160);
            m_pRecordFileBrowse.Text = "...";
            m_pRecordFileBrowse.Enabled = false;
            m_pRecordFileBrowse.Click += new EventHandler(m_pRecordFileBrowse_Click);
                        
            m_pSeparator2 = new GroupBox();
            m_pSeparator2.Size = new Size(395,3);
            m_pSeparator2.Location = new Point(2,190);

            mt_Codec = new Label();
            mt_Codec.Size = new Size(100,20);
            mt_Codec.Location = new Point(0,205);
            mt_Codec.TextAlign = ContentAlignment.MiddleRight;
            mt_Codec.Text = "Codec:";

            m_pCodec = new ComboBox();
            m_pCodec.Size = new Size(150,20);
            m_pCodec.Location = new Point(100,205);
            m_pCodec.DropDownStyle = ComboBoxStyle.DropDownList;
            m_pCodec.Items.Add("G711 a-law");
            m_pCodec.Items.Add("G711 u-law");
            m_pCodec.SelectedIndex = 0;
            m_pCodec.Enabled = false;
            m_pCodec.SelectedIndexChanged += new EventHandler(m_pCodec_SelectedIndexChanged);

            mt_Microphone = new Label();
            mt_Microphone.Size = new Size(100,20);
            mt_Microphone.Location = new Point(0,230);
            mt_Microphone.TextAlign = ContentAlignment.MiddleRight;
            mt_Microphone.Text = "Microphone:";

            m_pToggleMic = new Button();
            m_pToggleMic.Size = new Size(70,20);
            m_pToggleMic.Location = new Point(100,230);
            m_pToggleMic.Text = "Send";
            m_pToggleMic.Enabled = false;
            m_pToggleMic.Click += new EventHandler(m_pToggleMic_Click);

            mt_SendTestSound = new Label();
            mt_SendTestSound.Size = new Size(100,20);
            mt_SendTestSound.Location = new Point(0,255);
            mt_SendTestSound.TextAlign = ContentAlignment.MiddleRight;
            mt_SendTestSound.Text = "Test sound:";

            m_pSendTestSound = new Button();
            m_pSendTestSound.Size = new Size(70,20);
            m_pSendTestSound.Location = new Point(100,255);
            m_pSendTestSound.Text = "Send";
            m_pSendTestSound.Enabled = false;
            m_pSendTestSound.Click += new EventHandler(m_pSendTestSound_Click);

            m_pPlayTestSound = new Button();
            m_pPlayTestSound.Size = new Size(70,20);
            m_pPlayTestSound.Location = new Point(180,255);
            m_pPlayTestSound.Text = "Play";
            m_pPlayTestSound.Enabled = false;
            m_pPlayTestSound.Click += new EventHandler(m_pPlayTestSound_Click);

            m_pTab.TabPages["main"].Controls.Add(mt_OutDevices);
            m_pTab.TabPages["main"].Controls.Add(m_pOutDevices);
            m_pTab.TabPages["main"].Controls.Add(mt_Codec);
            m_pTab.TabPages["main"].Controls.Add(m_pCodec);
            m_pTab.TabPages["main"].Controls.Add(m_pSeparator1);
            m_pTab.TabPages["main"].Controls.Add(mt_LocalEndPoint);
            m_pTab.TabPages["main"].Controls.Add(m_pLocalIP);
            m_pTab.TabPages["main"].Controls.Add(m_pLocalPort);
            m_pTab.TabPages["main"].Controls.Add(m_pToggleRun);
            m_pTab.TabPages["main"].Controls.Add(mt_RemoteEndPoint);
            m_pTab.TabPages["main"].Controls.Add(m_pRemoteIP);
            m_pTab.TabPages["main"].Controls.Add(m_pRemotePort);
            m_pTab.TabPages["main"].Controls.Add(m_pRecord);
            m_pTab.TabPages["main"].Controls.Add(mt_RecordFile);
            m_pTab.TabPages["main"].Controls.Add(m_pRecordFile);
            m_pTab.TabPages["main"].Controls.Add(m_pRecordFileBrowse);
            m_pTab.TabPages["main"].Controls.Add(m_pSeparator2);
            m_pTab.TabPages["main"].Controls.Add(mt_Microphone);
            m_pTab.TabPages["main"].Controls.Add(m_pToggleMic);
            m_pTab.TabPages["main"].Controls.Add(mt_SendTestSound);
            m_pTab.TabPages["main"].Controls.Add(m_pSendTestSound);
            m_pTab.TabPages["main"].Controls.Add(m_pPlayTestSound);

            #endregion
                                                
            this.Controls.Add(m_pTab);
        }
                                                                                                                                               
        #endregion


        #region Events Handling

        #region method m_pToggleRun_Click

        private void m_pToggleRun_Click(object sender,EventArgs e)
        {
            if(m_IsRunning){
                m_IsRunning = false;
                m_IsSendingTest = false;

                m_pRtpSession.Dispose();
                m_pRtpSession = null;

                m_pWaveOut.Dispose();
                m_pWaveOut = null;

                if(m_pRecordStream != null){
                    m_pRecordStream.Dispose();
                    m_pRecordStream = null;
                }

                m_pOutDevices.Enabled = true;
                m_pToggleRun.Text = "Start";
                m_pRecord.Enabled = true;
                m_pRecordFile.Enabled = true;
                m_pRecordFileBrowse.Enabled = true;
                m_pRemoteIP.Enabled = false;
                m_pRemotePort.Enabled = false;
                m_pCodec.Enabled = false;
                m_pToggleMic.Text = "Send";
                m_pToggleMic.Enabled = false;
                m_pSendTestSound.Enabled = false;
                m_pSendTestSound.Text = "Send";
                m_pPlayTestSound.Enabled = false;
                m_pPlayTestSound.Text = "Play";
            }
            else{
                if(m_pOutDevices.SelectedIndex == -1){
                    MessageBox.Show(this,"Please select output device !","Error:",MessageBoxButtons.OK,MessageBoxIcon.Error);
                    return;
                }
                if(m_pRecord.Checked && m_pRecordFile.Text == ""){
                    MessageBox.Show(this,"Please specify record file !","Error:",MessageBoxButtons.OK,MessageBoxIcon.Error);
                    return;
                }

                if(m_pRecord.Checked){
                    m_pRecordStream = File.Create(m_pRecordFile.Text);
                }

                m_IsRunning = true;

                m_pWaveOut = new AudioOut(AudioOut.Devices[m_pOutDevices.SelectedIndex],8000,16,1);
               
                m_pRtpSession = new RTP_MultimediaSession(RTP_Utils.GenerateCNAME());
                // --- Debug -----
                wfrm_RTP_Debug frmRtpDebug = new wfrm_RTP_Debug(m_pRtpSession);
                frmRtpDebug.Show();
                //-----------------
                // m_pRtpSession.CreateSession(new RTP_Address(IPAddress.Parse(m_pLocalIP.Text),(int)m_pLocalPort.Value,(int)m_pLocalPort.Value + 1),new RTP_Clock(0,8000));
                string ip = "127.0.0.1";
                int port = 1100;
                int remote_port = 20700;
                //RTP_Session session = m_pRtpSession.CreateSession(new RTP_Address(IPAddress.Parse(ip), port, port + 1), new RTP_Clock(0, 8000));
                //session.AddTarget(new RTP_Address(IPAddress.Parse("127.0.0.1"), remote_port, remote_port + 1));
                RTP_Session session = m_pRtpSession.CreateMulticastSession(new RTP_Clock(0, 8000));
                session.AddTarget(new RTP_Address(IPAddress.Parse("224.0.0.1"), remote_port, remote_port + 1));
                session.Payload = 0;
                session.Start();

                m_pOutDevices.Enabled = false;
                m_pToggleRun.Text = "Stop";
                m_pRecord.Enabled = false;
                m_pRecordFile.Enabled = false;
                m_pRecordFileBrowse.Enabled = false;
                m_pRemoteIP.Enabled = true;
                m_pRemotePort.Enabled = true;
                m_pCodec.Enabled = true;
                m_pToggleMic.Enabled = true;
                m_pSendTestSound.Enabled = true;
                m_pSendTestSound.Text = "Send";
                m_pPlayTestSound.Enabled = true;                
                m_pPlayTestSound.Text = "Play";
            }


            wfrm_SendAudio frm = new wfrm_SendAudio(this, m_pRtpSession.Sessions[0], @"D:\_d\_PROJECTS\CisteraDesktopNotificationService\Lumisoft.Net.Rtp\Rtp Audio Demo\Rtp Audio Demo\bin\Debug\audio\futurama.raw");
            frm.Show();
        }
                                        
        #endregion

        #region method m_pRecord_CheckedChanged

        private void m_pRecord_CheckedChanged(object sender,EventArgs e)
        {
            if(m_pRecord.Checked){
                m_pRecordFile.Enabled = true;
                m_pRecordFileBrowse.Enabled = true;
            }
            else{
                m_pRecordFile.Enabled = false;
                m_pRecordFileBrowse.Enabled = false;
            }
        }

        #endregion

        #region method m_pRecordFileBrowse_Click

        private void m_pRecordFileBrowse_Click(object sender,EventArgs e)
        {
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.FileName = "record.raw";
            if(dlg.ShowDialog(this) == DialogResult.OK){
                m_pRecordFile.Text = dlg.FileName;
            }
        }

        #endregion

        #region method m_pCodec_SelectedIndexChanged

        private void m_pCodec_SelectedIndexChanged(object sender,EventArgs e)
        {
            // G711 a-law
            if(m_pCodec.SelectedIndex == 0){
                m_pActiveCodec = new PCMA();
                m_pRtpSession.Sessions[0].Payload = 8;
            }
            // G711 u-law
            else{
                m_pActiveCodec = new PCMU();
                m_pRtpSession.Sessions[0].Payload = 0;
            } 
        }

        #endregion

        #region method m_pToggleMic_Click

        private void m_pToggleMic_Click(object sender,EventArgs e)
        {
            wfrm_SendMic frm = new wfrm_SendMic(this,m_pRtpSession.Sessions[0]);
            frm.Show();
        }
                
        #endregion

        #region method m_pSendTestSound_Click

        private void m_pSendTestSound_Click(object sender,EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.InitialDirectory = Application.StartupPath + "\\audio";
            if(dlg.ShowDialog(null) == DialogResult.OK){
                wfrm_SendAudio frm = new wfrm_SendAudio(this,m_pRtpSession.Sessions[0],dlg.FileName);
                frm.Show();
            }
        }

        #endregion

        #region method m_pPlayTestSound_Click

        private void m_pPlayTestSound_Click(object sender,EventArgs e)
        {
            if(m_IsSendingTest){
                m_IsSendingTest = false;
            }
            else{
                OpenFileDialog dlg = new OpenFileDialog();
                dlg.InitialDirectory = Application.StartupPath + "\\audio";
                if(dlg.ShowDialog(null) == DialogResult.OK){
                    m_PlayFile = dlg.FileName;

                    m_IsSendingTest = true;

                    m_pToggleMic.Enabled = false;
                    m_pSendTestSound.Enabled = false;
                    m_pPlayTestSound.Text = "Stop";

                    Thread tr = new Thread(new ThreadStart(this.PlayTestAudio));
                    tr.Start();
                }
            }
        }

        #endregion


        #region method m_pRtpSession_NewReceiveStream

        /// <summary>
        /// This method is called when RTP session gets new receive stream.
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="e">Event data.</param>
        private void m_pRtpSession_NewReceiveStream(object sender,RTP_ReceiveStreamEventArgs e)
        {            
            this.BeginInvoke(new MethodInvoker(delegate(){
                wfrm_Receive frm = new wfrm_Receive(e.Stream,m_pAudioCodecs);
                frm.Show();
            }));
        }
                
        #endregion


        #region method wfrm_Main_FormClosed

        /// <summary>
        /// This method is called when this form is closed.
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="e">Event data.</param>
        private void wfrm_Main_FormClosed(object sender,FormClosedEventArgs e)
        {
            m_IsRunning = false;
            if(m_pRtpSession != null){
                m_pRtpSession.Close(null);
                m_pRtpSession = null;
            }
            if(m_pWaveOut != null){
                m_pWaveOut.Dispose();
                m_pWaveOut = null;
            }
            if(m_pRecordStream != null){
                m_pRecordStream.Dispose();
                m_pRecordStream = null;
            }
        }

        #endregion

        #endregion


        #region method PlayTestAudio

        /// <summary>
        /// Plays test audio.
        /// </summary>
        private void PlayTestAudio()
        {
            try{           
                using(FileStream fs = File.OpenRead(m_PlayFile)){
                    byte[] buffer       = new byte[400];
                    int    readedCount  = fs.Read(buffer,0,buffer.Length);
                    long   lastSendTime = DateTime.Now.Ticks;
                    while(m_IsSendingTest && readedCount > 0){
                        // Send and read next.
                        m_pWaveOut.Write(buffer,0,readedCount);
                        readedCount = fs.Read(buffer,0,buffer.Length);

                        Thread.Sleep(25);

                        lastSendTime = DateTime.Now.Ticks;
                    }                    
                }
            }
            catch(Exception x){
                MessageBox.Show(null,"Error: " + x.ToString(),"Error:",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }

        #endregion


        #region method LoadWaveDevices

        /// <summary>
        /// Loads available wave input and output devices to UI.
        /// </summary>
        private void LoadWaveDevices()
        {            
            // Load output devices.
            m_pOutDevices.Items.Clear();
            foreach(AudioOutDevice device in AudioOut.Devices){
                m_pOutDevices.Items.Add(device.Name);
            }
            if(m_pOutDevices.Items.Count > 0){
                m_pOutDevices.SelectedIndex = 0;
            }
        }

        #endregion


        #region Properties implementation

        /// <summary>
        /// Gets active codec.
        /// </summary>
        public AudioCodec ActiveCodec
        {
            get{ return m_pActiveCodec; }
        }

        /// <summary>
        /// Gets audio codecs.
        /// </summary>
        public Dictionary<int,AudioCodec> AudioCodecs
        {
            get{ return m_pAudioCodecs; }
        }

        #endregion

    }
}
