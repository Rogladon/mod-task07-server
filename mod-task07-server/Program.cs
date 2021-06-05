using System;
using System.Linq;
using System.Diagnostics;
namespace mod_task07_server {
	class Program {
		public static SystemEmulator.Config config => new SystemEmulator.Config {
			requestIntensive = 100f,
			serviceIntensive = 10f,
			threadCount = 5,
		};
		public static SystemEmulator emulator;
		static void Main(string[] args) {
			emulator = new SystemEmulator(config);
			Stopwatch s = new Stopwatch();
			s.Start();
			emulator.Run(100);
			while (!emulator.isStop) {
			}
			s.Stop();
			GetStatistic(s.ElapsedMilliseconds);
		}

		static void GetStatistic(float time) {
			//emulator.Stop();
			var s = emulator.GetStatistic();
			float _p = config.requestIntensive / config.serviceIntensive;
			float pdowntime = 0;
			for(int i = 1; i <= config.threadCount; i++) {
				pdowntime += MathF.Pow(_p, i) / Fact(i);
			}
			pdowntime = MathF.Pow(pdowntime, -1);
			float preject = (MathF.Pow(_p,config.threadCount) / Fact(config.threadCount)) * pdowntime;
			float relThr = 1 - preject;
			float absThr = config.requestIntensive * relThr;
			float avCountThread = absThr / config.serviceIntensive;
			Console.WriteLine($"вероятность простоя системы: {pdowntime}\n" +
				$"вероятность отказа системы: {preject}\n" +
				$"относительная пропускная способность: {relThr}\n" +
				$"абсолютная пропускная способность: {absThr}\n" +
				$"среднее число занятых каналов: {avCountThread}\n");
		}
		static float Fact(int n) {
			float res = 1;
			for(int i = 1; i <= n; i++) {
				res *= i;
			}
			return res == 0?1:res;
		}
	}
}
