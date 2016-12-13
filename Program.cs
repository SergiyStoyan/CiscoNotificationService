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
using System.Net;

namespace Cliver
{
    static class Program
    {
        [STAThread] 
        public static void Main(string[] args)
        {
            try
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);

                SysTray f = SysTray.This;
                Application.Run(f);
            }
            catch (Exception e)
            {
                Message.Error(e);
            }
            finally
            {
                Exit();
            }

            //Processor p = new Processor();          
            //Application.Run(new Form());
        }

        static public void Exit()
        {
            Application.Exit();
            Environment.Exit(0);
        }
    }
}
