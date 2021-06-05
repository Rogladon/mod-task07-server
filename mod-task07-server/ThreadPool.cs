using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Linq;

namespace mod_task07_server {
	public class ThreadPool {

		public List<ServiceThread> threads;

		public ThreadPool(int count) {
			threads = new List<ServiceThread>();
			for(int i = 0; i < count; i++) {
				threads.Add(new ServiceThread());
			}
		}

		public bool TrySetService(float time) {
			if (threads.AsParallel().All(p => !p.isFree)) return false;
			var t = threads.First(p => p.isFree);
			Console.WriteLine($"thred id: {threads.IndexOf(t)}");
			t.SetService(time);
			return true;
		}
	}
}
