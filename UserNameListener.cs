////********************************************************************************************
////Author: Sergey Stoyan
////        sergey.stoyan@gmail.com
////        sergey_stoyan@yahoo.com
////        http://www.cliversoft.com
////        16 October 2007
////Copyright: (C) 2006-2007, Sergey Stoyan
////********************************************************************************************

//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Windows.Forms;
//using System.IO;
//using System.Threading;
//using System.Runtime.InteropServices;
//using Bonjour;

//namespace Cliver.CisteraNotification
//{
//    /// <summary>
//    /// discarded
//    /// </summary>
//    static class UserNameListener
//    {
//        public static void Start()
//        {
//            if (t != null)
//                return;
//            t = ThreadRoutines.StartTry(() =>
//            {
//                listen();
//            });
//        }
//        static Thread t = null;

//        public static void Stop()
//        {
//            if (t != null && t.IsAlive)
//            {
//                t.Abort();
//                t = null;
//            }
//            if (hWnd != IntPtr.Zero)
//            {
//                DestroyWindow(hWnd);
//                hWnd = IntPtr.Zero;
//            }
//        }

//        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
//        struct WNDCLASSEX
//        {
//            [MarshalAs(UnmanagedType.U4)]
//            public int cbSize;
//            [MarshalAs(UnmanagedType.U4)]
//            public int style;
//            public IntPtr lpfnWndProc;
//            public int cbClsExtra;
//            public int cbWndExtra;
//            public IntPtr hInstance;
//            public IntPtr hIcon;
//            public IntPtr hCursor;
//            public IntPtr hbrBackground;
//            public string lpszMenuName;
//            public string lpszClassName;
//            public IntPtr hIconSm;
//        }

//        [System.Runtime.InteropServices.DllImport("user32.dll", SetLastError = true)]
//        static extern bool DestroyWindow(IntPtr hWnd);

//        [DllImport("user32.dll", SetLastError = true, EntryPoint = "CreateWindowEx")]
//        public static extern IntPtr CreateWindowEx(
//           int dwExStyle,
//           //UInt16 regResult,
//           string lpClassName,
//           string lpWindowName,
//           UInt32 dwStyle,
//           int x,
//           int y,
//           int nWidth,
//           int nHeight,
//           IntPtr hWndParent,
//           IntPtr hMenu,
//           IntPtr hInstance,
//           IntPtr lpParam);

//        [DllImport("user32.dll", SetLastError = true, EntryPoint = "RegisterClassEx")]
//        static extern System.UInt16 RegisterClassEx([In] ref WNDCLASSEX lpWndClass);

//        [DllImport("kernel32.dll")]
//        static extern uint GetLastError();

//        [DllImport("user32.dll")]
//        static extern IntPtr DefWindowProc(IntPtr hWnd, uint uMsg, IntPtr wParam, IntPtr lParam);

//        [DllImport("user32.dll")]
//        static extern sbyte GetMessage(out uint lpMsg, IntPtr hWnd, uint wMsgFilterMin, uint wMsgFilterMax);

//        [DllImport("user32.dll")]
//        static extern bool TranslateMessage([In] ref uint lpMsg);

//        [DllImport("user32.dll")]
//        static extern IntPtr DispatchMessage([In] ref uint lpmsg);

//        static void listen()
//        {
//            WNDCLASSEX wind_class = new WNDCLASSEX();
//            wind_class.cbSize = Marshal.SizeOf(typeof(WNDCLASSEX));
//            wind_class.hInstance = IntPtr.Zero;
//            wind_class.hIcon = IntPtr.Zero;
//            wind_class.lpszClassName = "MESSAGE_ONLY_CLASS";
//            wind_class.lpfnWndProc = Marshal.GetFunctionPointerForDelegate(delegWndProc);
//            if (RegisterClassEx(ref wind_class) == 0)
//            {
//                uint error = GetLastError();
//                return;
//            }
//            string wndClass = wind_class.lpszClassName;

//            hWnd = CreateWindowEx(0, wind_class.lpszClassName, "Bogus Window For Listening Messages", 0, 0, 0, 0, 0, HWND_MESSAGE, IntPtr.Zero, wind_class.hInstance, IntPtr.Zero);
//            //IntPtr hWnd = CreateWindowEx(0, regResult, "Hello Win32", WS_OVERLAPPEDWINDOW | WS_VISIBLE, 0, 0, 300, 400, IntPtr.Zero, IntPtr.Zero, wind_class.hInstance, IntPtr.Zero);
//            if (hWnd == IntPtr.Zero)
//            {
//                uint error = GetLastError();
//                return;
//            }

//            uint msg;
//            int rc;
//            while (true)
//            {
//                rc = GetMessage(out msg, IntPtr.Zero, 0, 0);
//                if (rc == -1)
//                {
//                    Message.Error("BonjourService: GetMessage returned -1");
//                    return;
//                }
//                else if (rc == 0)
//                    return;

//                TranslateMessage(ref msg);
//                DispatchMessage(ref msg);
//            }
//        }
//        static IntPtr hWnd = IntPtr.Zero;

//        static readonly IntPtr HWND_MESSAGE = new IntPtr(-3);
//        const Int32 WTS_SESSION_LOGON = 0x5;
//        const Int32 WTS_SESSION_LOGOFF = 0x6;

//        delegate IntPtr WndProc(IntPtr hWnd, uint msg, IntPtr wParam, IntPtr lParam);
//        static private WndProc delegWndProc = (IntPtr hWnd, uint uMsg, IntPtr wParam, IntPtr lParam) =>
//        {
//            if (uMsg == Win32.WM_WTSSESSION_CHANGE)
//            {
//                switch (wParam.ToInt32())
//                {
//                    case WTS_SESSION_LOGON:
//                        //log->message(_T("BonjourService: WTS_SESSION_LOGON."));
//                        BonjourService.Start();
//                        break;
//                    case WTS_SESSION_LOGOFF:
//                        //log->message(_T("BonjourService: WTS_SESSION_LOGOFF."));
//                        BonjourService.Start();
//                        break;
//                        /*	WTS_CONSOLE_CONNECT
//                            WTS_CONSOLE_DISCONNECT
//                            WTS_REMOTE_CONNECT
//                            WTS_REMOTE_DISCONNECT
//                            WTS_SESSION_LOGON*/
//                }
//            }
//            return DefWindowProc(hWnd, uMsg, wParam, lParam);
//        };
//    }
//}