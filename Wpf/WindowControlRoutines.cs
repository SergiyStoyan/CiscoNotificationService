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

namespace Cliver
{
    public static class WindowControlRoutines
    {
        public static object Invoke(this System.Windows.Controls.Control c, Func<object> function)
        {
            return c.Dispatcher.Invoke(function);
        }

        public static void Invoke(this System.Windows.Controls.Control c, MethodInvoker code)
        {
            c.Dispatcher.Invoke(code);
        }

        public static void BeginInvoke(this System.Windows.Controls.Control c, MethodInvoker code)
        {
            c.Dispatcher.BeginInvoke(code);
        }

        //public static object InvokeFromUiThread(Delegate d)
        //{
        //    return Application.OpenForms[0].Invoke(d);
        //}

        public static Thread SlideVertically(this System.Windows.Window c, double pixelsPerMss, double position2, int delta = 1, MethodInvoker finished = null)
        {
            lock (c)
            {
                Thread t = null;
                //if (controls2sliding_thread.TryGetValue(c, out t) && t.IsAlive)
                //    return t;

                delta = c.Top > position2 ? -delta : delta;
                double total_mss = Math.Abs(position2 - c.Top) / pixelsPerMss;
                int sleep = (int)(total_mss / ((position2 - c.Top) / delta));
                t = ThreadRoutines.Start(() =>
                {
                    try
                    {
                        while (c.Visibility == Visibility.Visible && !(bool)WindowControlRoutines.Invoke(c, () =>
                        {
                            c.Top = c.Top + delta;
                            return delta < 0 ? c.Top <= position2 : c.Top >= position2;
                        })
                        )
                            System.Threading.Thread.Sleep(sleep);
                        WindowControlRoutines.Invoke(c, () =>
                        {
                            finished?.Invoke();
                        });
                    }
                    catch (Exception e)//control disposed
                    {
                    }
                });
                //controls2sliding_thread[c] = t;
                return t;
            }
        }
        //static readonly  Dictionary<Control, Thread> controls2sliding_thread = new Dictionary<Control, Thread>();

        public static Thread Condense(this System.Windows.Window c, double centOpacityPerMss, double opacity2, double delta = 0.05, MethodInvoker finished = null)
        {
            lock (c)
            {
                Thread t = null;
                //if (controls2condensing_thread.TryGetValue(f, out t) && t.IsAlive)
                //    return t;

                delta = c.Opacity < opacity2 ? delta : -delta;
                double total_mss = Math.Abs(opacity2 - c.Opacity) / (centOpacityPerMss / 100);
                int sleep = (int)(total_mss / ((opacity2 - c.Opacity) / delta));
                t = ThreadRoutines.Start(() =>
                {
                    try
                    {
                        while (!(bool)WindowControlRoutines.Invoke(c, () =>
                        {
                            c.Opacity = c.Opacity + delta;
                            return delta > 0 ? c.Opacity >= opacity2 : c.Opacity <= opacity2;
                        })
                        )
                            System.Threading.Thread.Sleep(sleep);
                        WindowControlRoutines.Invoke(c, () =>
                        {
                            finished?.Invoke();
                        });
                    }
                    catch (Exception e)//control disposed
                    {
                    }
                });
                //controls2condensing_thread[f] = t;
                return t;
            }
        }
        //static readonly Dictionary<Form, Thread> controls2condensing_thread = new Dictionary<Form, Thread>();
    }
}
