using System;
using System.Threading;
using Resin_Tracker_Runner;

namespace Resin_Tracker_Runner_Tests
{
	public delegate void ResinUpdated(int currentResin);

	internal class Program
	{
		private static void Main(string[] args)
		{
			var tracker = new Tracker(new ResinUpdated(ResultCallback));

			var t = new Thread(new ThreadStart(tracker.Reset));
			t.Start();
			t.Join();
		}

		public static void ResultCallback(int resinCount)
		{
			Console.WriteLine($"Current Resin: {resinCount}");
		}
	}
}