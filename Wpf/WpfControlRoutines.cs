using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Threading;
using System.Reflection;
using System.Windows.Forms;
using System.Drawing;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Interop;

namespace Cliver
{
    public class WindowRoutines
    {
        public static ImageSource GetAppIcon()
        {
            Icon i = System.Drawing.Icon.ExtractAssociatedIcon(System.Reflection.Assembly.GetEntryAssembly().ManifestModule.Name);
            return Imaging.CreateBitmapSourceFromHIcon(
                i.Handle,
                new Int32Rect(0, 0, i.Width, i.Height),
                BitmapSizeOptions.FromEmptyOptions()
                );
        }
    }

    public static class WpfControlRoutines
    {
        public static object Invoke(this System.Windows.Controls.Control c, Func<object> function)
        {
            return c.Dispatcher.Invoke(function);
        }

        public static object Invoke(this System.Windows.Controls.Control c, MethodInvoker code)
        {
            return c.Dispatcher.Invoke(code);
        }

        public static void BeginInvoke(this System.Windows.Controls.Control c, MethodInvoker code)
        {
            c.Dispatcher.BeginInvoke(code);
        }

        //public static Thread SlideVertically(this System.Windows.Window c, double pixelsPerMss, double position2, int step = 1, MethodInvoker finished = null)
        //{
        //    lock (c)
        //    {
        //        Thread t = null;
        //        //if (controls2sliding_thread.TryGetValue(c, out t) && t.IsAlive)
        //        //    return t;

        //        step = c.Top > position2 ? -step : step;
        //        double total_mss = Math.Abs(position2 - c.Top) / pixelsPerMss;
        //        int sleep = (int)(total_mss / ((position2 - c.Top) / step));
        //        t = ThreadRoutines.Start(() =>
        //        {
        //            try
        //            {
        //                while (c.Visibility == Visibility.Visible && !(bool)WpfControlRoutines.Invoke(c, () =>
        //                {
        //                    c.Top = c.Top + step;
        //                    return step < 0 ? c.Top <= position2 : c.Top >= position2;
        //                })
        //                )
        //                    System.Threading.Thread.Sleep(sleep);
        //                WpfControlRoutines.Invoke(c, () =>
        //                {
        //                    finished?.Invoke();
        //                });
        //            }
        //            catch (Exception e)//control disposed
        //            {
        //            }
        //        });
        //        //controls2sliding_thread[c] = t;
        //        return t;
        //    }
        //}

        //public static Thread Condense(this System.Windows.Window c, double opacityPerMss, double opacity2, double step = 0.05, MethodInvoker finished = null)
        //{
        //    lock (c)
        //    {
        //        Thread t = null;
        //        //if (controls2condensing_thread.TryGetValue(f, out t) && t.IsAlive)
        //        //    return t;

        //        step = c.Opacity < opacity2 ? step : -step;
        //        double total_mss = Math.Abs(opacity2 - c.Opacity) / opacityPerMss;
        //        int sleep = (int)(total_mss / ((opacity2 - c.Opacity) / step));
        //        t = ThreadRoutines.Start(() =>
        //        {
        //            try
        //            {
        //                while (!(bool)WpfControlRoutines.Invoke(c, () =>
        //                {
        //                    c.Opacity = c.Opacity + step;
        //                    return step > 0 ? c.Opacity >= opacity2 : c.Opacity <= opacity2;
        //                })
        //                )
        //                    System.Threading.Thread.Sleep(sleep);
        //                WpfControlRoutines.Invoke(c, () =>
        //                {
        //                    finished?.Invoke();
        //                });
        //            }
        //            catch (Exception e)//control disposed
        //            {
        //            }
        //        });
        //        //controls2condensing_thread[f] = t;
        //        return t;
        //    }
        //}
    }
}
