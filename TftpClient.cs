//********************************************************************************************
//Author: Sergey Stoyan 
//        sergey.stoyan@gmail.com
//        sergey_stoyan@yahoo.com
//        http://www.cliversoft.com
//        16 October 2007
//Copyright: (C) 2006-2007, Sergey Stoyan
//********************************************************************************************

using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.IO;
using System.Threading;
using System.Net;
using System.Web;
using System.Xml;
using System.Security;
using System.Text;
using System.Text.RegularExpressions;
using System.Diagnostics;

namespace Cliver.CisteraNotification
{
    class TftpClient
    {
        static TftpClient()
        {
        }

        static public void Play(string file)
        {
            stream = new MemoryStream();
            player = new System.Media.SoundPlayer(stream);
            transfer = client.Download(file);
            transfer.OnProgress += transfer_OnProgress;
            transfer.OnFinished += transfer_OnFinshed;
            transfer.OnError += transfer_OnError;
            transfer.Start(stream);
        }
        readonly static Tftp.Net.TftpClient client = new Tftp.Net.TftpClient("localhost");
        static Tftp.Net.ITftpTransfer transfer = null;
        static MemoryStream stream = null;
        static System.Media.SoundPlayer player = null;

        static void transfer_OnProgress(Tftp.Net.ITftpTransfer transfer, Tftp.Net.TftpTransferProgress progress)
        {
            player.Play();
        }

        static void transfer_OnError(Tftp.Net.ITftpTransfer transfer, Tftp.Net.TftpTransferError error)
        {
            throw new Exception("Tftp transfer failed: " + error);
        }

        static void transfer_OnFinshed(Tftp.Net.ITftpTransfer transfer)
        {
        }
    }
}