using System;
using System.Threading;
using System.Timers;

namespace Oxide.Ext.Hive.Utils
{
	public class TimerUtil
	{
		private Action<int> callback;
		private System.Timers.Timer timer;


		public TimerUtil()
		{
			timer = new System.Timers.Timer();
		}


		public void Once(int time, Action<int> callback)
		{
			this.callback = callback;

			timer.Interval = time;
			timer.Elapsed += HereThread;
			timer.Start();
		}

		//public void Delay(double time, Action callback)
		//{

		//}

		public void Stop()
		{

		}

		private void HereThread(Object source, ElapsedEventArgs e)
		{
			timer.Stop();
			callback.Invoke(0);

		}

	}
}

