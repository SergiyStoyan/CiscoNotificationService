using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

using LumiSoft.Net.RTP;
using LumiSoft.Net.Media;

namespace Rtp_Audio_Demo
{
    /// <summary>
    /// Send microphone audio window.
    /// </summary>
    public class wfrm_SendMic : Form
    {        
        private Label    mt_InDevices   = null;
        private ComboBox m_pInDevices   = null;
        private Label    mt_Codec       = null;
        private TextBox  m_pCodec       = null;
        private Label    mt_PacketsSent = null;
        private Label    m_pPacketsSent = null;
        private Label    mt_KBSent      = null;
        private Label    m_pKBSent      = null;
        private Button   m_pToggleSend  = null;
        
        private wfrm_Main      m_pMainUI     = null;
        private RTP_Session    m_pSession    = null;
        private AudioIn_RTP    m_pAudioInRTP = null;
        private RTP_SendStream m_pSendStream = null;
        private Timer          m_pTimer      = null;

        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <param name="owner">Main UI.</param>
        /// <param name="session">RTP session.</param>
        /// <exception cref="ArgumentNullException">Is raised when <b>owner</b> or <b>session</b> is null reference.</exception>
        public wfrm_SendMic(wfrm_Main owner,RTP_Session session)
        {
            if(owner == null){
                throw new ArgumentNullException("owner");
            }
            if(session == null){
                throw new ArgumentNullException("session");
            }

            m_pMainUI  = owner;
            m_pSession = session;

            InitUI();

            // Load input devices.
            m_pInDevices.Items.Clear();
            foreach(AudioInDevice device in AudioIn.Devices){
                m_pInDevices.Items.Add(device.Name);
            }
            if(m_pInDevices.Items.Count > 0){
                m_pInDevices.SelectedIndex = 0;
            }
        }

        #region method InitUI

        /// <summary>
        /// Creates and initializes UI.
        /// </summary>
        private void InitUI()
        {
            this.Size = new Size(350,150);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Text = "Sending microphone";
            this.FormClosing += new FormClosingEventHandler(wfrm_SendMic_FormClosing);

            mt_InDevices = new Label();
            mt_InDevices.Size = new Size(100,20);
            mt_InDevices.Location = new Point(0,10);
            mt_InDevices.Text = "Output device:";
            mt_InDevices.TextAlign = ContentAlignment.MiddleRight;

            m_pInDevices = new ComboBox();
            m_pInDevices.Size = new Size(200,20);
            m_pInDevices.Location = new Point(105,10);
            m_pInDevices.DropDownStyle = ComboBoxStyle.DropDownList;

            mt_Codec = new Label();
            mt_Codec.Location = new Point(0,35);
            mt_Codec.Size = new Size(100,20);
            mt_Codec.Text = "Codec:";
            mt_Codec.TextAlign = ContentAlignment.MiddleRight;

            m_pCodec = new TextBox();
            m_pCodec.Location = new Point(105,35);
            m_pCodec.Size = new Size(100,20);
            m_pCodec.ReadOnly = true;

            mt_PacketsSent = new Label();
            mt_PacketsSent.Location = new Point(0,60);
            mt_PacketsSent.Size = new Size(100,20);
            mt_PacketsSent.Text = "Packets sent:";
            mt_PacketsSent.TextAlign = ContentAlignment.MiddleRight;

            m_pPacketsSent = new Label();
            m_pPacketsSent.Location = new Point(105,60);
            m_pPacketsSent.Size = new Size(100,20);
            m_pPacketsSent.Text = "0";
            m_pPacketsSent.TextAlign = ContentAlignment.MiddleLeft;

            mt_KBSent = new Label();
            mt_KBSent.Location = new Point(0,85);
            mt_KBSent.Size = new Size(100,20);
            mt_KBSent.Text = "KB sent:";
            mt_KBSent.TextAlign = ContentAlignment.MiddleRight;

            m_pKBSent = new Label();
            m_pKBSent.Location = new Point(105,85);
            m_pKBSent.Size = new Size(100,20);
            m_pKBSent.Text = "0";
            m_pKBSent.TextAlign = ContentAlignment.MiddleLeft;

            m_pToggleSend = new Button();
            m_pToggleSend.Size = new Size(70,20);
            m_pToggleSend.Location = new Point(235,85);
            m_pToggleSend.Text = "Send";
            m_pToggleSend.Click += new EventHandler(m_pToggleSend_Click);

            this.Controls.Add(mt_InDevices);
            this.Controls.Add(m_pInDevices);
            this.Controls.Add(mt_Codec);
            this.Controls.Add(m_pCodec);
            this.Controls.Add(mt_PacketsSent);
            this.Controls.Add(m_pPacketsSent);
            this.Controls.Add(mt_KBSent);
            this.Controls.Add(m_pKBSent);
            this.Controls.Add(m_pToggleSend);
        }
                                
        #endregion


        #region Events handling

        #region method m_pToggleSend_Click

        private void m_pToggleSend_Click(object sender,EventArgs e)
        {
            if(m_pAudioInRTP == null){
                m_pSendStream = m_pSession.CreateSendStream();

                m_pAudioInRTP = new AudioIn_RTP(AudioIn.Devices[m_pInDevices.SelectedIndex],20,m_pMainUI.AudioCodecs,m_pSendStream);
                m_pAudioInRTP.Start();

                m_pTimer = new Timer();
                m_pTimer.Interval = 500;
                m_pTimer.Tick += delegate(object s1,EventArgs e1){
                    m_pCodec.Text       = m_pAudioInRTP.AudioCodec.Name;
                    m_pPacketsSent.Text = m_pSendStream.RtpPacketsSent.ToString();
                    m_pKBSent.Text      = Convert.ToString(m_pSendStream.RtpBytesSent / 1000);
                };
                m_pTimer.Start();

                m_pToggleSend.Text = "Stop";
            }
            else{
                m_pTimer.Dispose();
                m_pTimer = null;
                m_pAudioInRTP.Dispose();
                m_pAudioInRTP = null;
                m_pSendStream.Close();
                m_pSendStream = null;

                m_pToggleSend.Text = "Send";
            }
        }
                                
        #endregion


        #region method wfrm_SendMic_FormClosing

        private void wfrm_SendMic_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(m_pTimer != null){
                m_pTimer.Dispose();
                m_pTimer = null;
            }
            if(m_pAudioInRTP != null){
                m_pAudioInRTP.Dispose();
                m_pAudioInRTP = null;
            }
            if(m_pSendStream != null){
                m_pSendStream.Close();
                m_pSendStream = null;
            }
        }

        #endregion

        #endregion
    }
}
