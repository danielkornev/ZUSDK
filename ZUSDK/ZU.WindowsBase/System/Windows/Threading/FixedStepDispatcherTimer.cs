using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Windows.Threading
{
	/// <summary>
	/// The class is constructed with a fixed time step and supports Start/Stop/Reset, 
	/// Start/Stop/Start works like a resume operation. The timer is like a stopwatch in that regard.
	/// </summary>
	/// <remarks>
	/// Source: http://stackoverflow.com/questions/1329900/net-event-every-minute-on-the-minute-is-a-timer-the-best-option/3250747#3250747
	/// </remarks>
	public class FixedStepDispatcherTimer
	{
		/// <summary>
		/// Occurs when the timer interval has elapsed.
		/// </summary>
		public event EventHandler Tick;

		DispatcherTimer timer;

		public bool IsRunning { get { return timer.IsEnabled; } }

		long step, nextTick, n;

		public TimeSpan Elapsed { get { return new TimeSpan(n * step); } }

		public FixedStepDispatcherTimer(TimeSpan interval)
		{
			if (interval < TimeSpan.Zero)
			{
				throw new ArgumentOutOfRangeException("interval");
			}
			this.timer = new DispatcherTimer();
			this.timer.Tick += new EventHandler(OnTimerTick);
			this.step = interval.Ticks;
		}

		TimeSpan GetTimerInterval()
		{
			var interval = nextTick - DateTime.Now.Ticks;
			if (interval > 0)
			{
				return new TimeSpan(interval);
			}
			return TimeSpan.Zero; // yield
		}

		void OnTimerTick(object sender, EventArgs e)
		{
			if (DateTime.Now.Ticks >= nextTick)
			{
				n++;
				if (Tick != null)
				{
					Tick(this, EventArgs.Empty);
				}
				nextTick += step;
			}
			var interval = GetTimerInterval();
			//Trace.WriteLine(interval);
			timer.Interval = interval;
		}

		public void Reset()
		{
			n = 0;
			nextTick = 0;
		}

		public void Start()
		{
			var now = DateTime.Now.Ticks;
			nextTick = now + (step - (nextTick % step));
			timer.Interval = GetTimerInterval();
			timer.Start();
		}

		public void Stop()
		{
			timer.Stop();
			nextTick = DateTime.Now.Ticks % step;
		}
	} // class
} // namespace
