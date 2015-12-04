using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;

namespace System.Windows.Threading
{
    public static class DispatcherExtensions
    {
        private static Dictionary<string, DispatcherTimer> timers = new Dictionary<string, DispatcherTimer>();
        private static readonly object syncRoot = new object();

        public static void DelayInvoke(this Dispatcher dispatcher, string namedInvocation, Action action = null, double delay = 1.0f, DispatcherPriority priority = DispatcherPriority.Normal)
        {
            dispatcher.DelayInvoke(namedInvocation, action, TimeSpan.FromSeconds(delay), priority);
        }
        public static void DelayInvoke(this Dispatcher dispatcher, string namedInvocation, Action action, TimeSpan delay, DispatcherPriority priority = DispatcherPriority.Normal)
        {
            lock (syncRoot)
            {
                if (timers.ContainsKey(namedInvocation))
                {
                    timers[namedInvocation].Stop();
                    timers.Remove(namedInvocation);
                }
                if (action != null)
                {
                    var timer = new DispatcherTimer(delay, priority, (s, e) => action(), dispatcher);
                    timer.Start();
                    timers.Add(namedInvocation, timer);
                }
            }
        }
    } // class
} // namespace
