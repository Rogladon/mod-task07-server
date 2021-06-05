using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading;


namespace mod_task07_server {
	public class ServiceThread {
		private Thread thread;
		private Stopwatch stopwatch;
		public int countRequest = 0;
		public float timeEmty = 0;
		public bool isFree { get; private set; } = true;
		public ServiceThread() {
			stopwatch = new Stopwatch();
		}
		public void SetService(float time) {
			if (!isFree) Console.WriteLine("Expection: This Thread are not free");
			stopwatch.Stop();
			timeEmty += stopwatch.ElapsedMilliseconds;
			isFree = false;
			thread = new Thread(Service);
			thread.Start(time);
		}

		private void Service(object obj) {
			float time = (float)obj;
			Thread.Sleep((int)(time * 1000));
			
			countRequest++;
			stopwatch.Reset();
			stopwatch.Start();
			isFree = true;
		}
	}
}
