using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace mod_task07_server {
	public class Server {
		public struct Statistic {
			public int countRequest { get;  set; }
			public int countRejectedRequest { get;  set; }
			public int countSucessfulRequest { get;  set; }
			public List<int> countSucessfulRequestForThread { get;  set; }
			public List<float> timeEmpteForThread { get;  set; }

		}
		private ThreadPool threadPool;
		private float serviceTimeout;
		private object locker = new object();
		private int countRequest = 0;
		private int countRejectedrequest = 0;
		public Server(float serviceIntencive, int countThred) {
			threadPool = new ThreadPool(countThred);
			serviceTimeout = 1 / serviceIntencive;
		}
		
		public void Service(Client.ApplicationConfig config) {
			countRequest++;
			Console.Write($"request: {config.id}...");
			lock (locker) {
				if (threadPool.TrySetService(serviceTimeout)) {
					Console.WriteLine("True");
					countRejectedrequest++;
				} else
					Console.WriteLine("False");
			}
		}
		public Statistic GetStatistic() {
			return new Statistic {
				countRequest = countRequest,
				countSucessfulRequest = countRequest - countRejectedrequest,
				countRejectedRequest = countRejectedrequest,
				countSucessfulRequestForThread = threadPool.threads.Select(p => p.countRequest).ToList(),
				timeEmpteForThread = threadPool.threads.Select(p => p.timeEmty).ToList()
			};
		}
	}
}
