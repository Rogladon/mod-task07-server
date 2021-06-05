using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Threading;

namespace mod_task07_server {
	public class Client {
		public class ApplicationConfig {
			public int id { get; }
			public ApplicationConfig(int id) {
				this.id = id;
			}
		}
		private struct ThreadRepeatSendingConfig {
			public float timeout { get; }
			public int count { get; }
			public ThreadRepeatSendingConfig(float timeout, int count) {
				this.timeout = timeout;
				this.count = count;
			}
		}

		private Action<ApplicationConfig> request;
		private Thread thread;

		public bool isSending { get; private set; } = false;
		public Client( Action<ApplicationConfig> request) {
			this.request = request;
		}
		/// <summary>
		/// Начинает отправку сообщений на сервер
		/// </summary>
		/// <param name="timeout">Задержка между отправками в секундах</param>
		/// <param name="count">Количество сообщений (-1 - бесконечно)</param>
		public void SendingRepeat(float timeout, int count = -1) {
			isSending = true;
			thread = new Thread(_SendinRepeat);
			thread.Start(new ThreadRepeatSendingConfig(timeout, count));
		}
		public void StopSendRepeat() {
			thread.Abort();
		}
		private void _SendinRepeat(object obj) {
			ThreadRepeatSendingConfig config = (ThreadRepeatSendingConfig)obj;
			int applicationId = 0;
			for(int i = config.count; i != 0; i--) {
				request(new ApplicationConfig(applicationId));
				applicationId++;
				Thread.Sleep((int)(config.timeout*1000));
			}
			isSending = false;
		}

		
	}
}
