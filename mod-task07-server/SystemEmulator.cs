using System;
using System.Collections.Generic;
using System.Text;

namespace mod_task07_server {
	public class SystemEmulator {
		public struct Config {
			public float serviceIntensive { get; set; }
			public float requestIntensive { get; set; }
			public int threadCount { get; set; }
		}
		private Server server;
		private Client client;
		private Config config;

		public bool isStop { get; private set; }

		public SystemEmulator(Config config) {
			this.config = config;
			server = new Server(config.serviceIntensive, config.threadCount);
			client = new Client(server.Service);
			Console.WriteLine("Emulator created sucessfull!");
		}
		public void Run(int count) {
			Console.WriteLine("Emulator running");
			client.SendingRepeat(1 / config.requestIntensive, count);
			isStop = false;
			while (client.isSending) {

			}
			isStop = true;
		}
		public void Stop() {
			client.StopSendRepeat();
		}
		public Server.Statistic GetStatistic() {
			return server.GetStatistic();
		}
	}
}
