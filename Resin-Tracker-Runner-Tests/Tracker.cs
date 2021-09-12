using System.Threading;
using Resin_Tracker_Runner_Tests;

namespace Resin_Tracker_Runner
{
	public class Tracker
	{
		private const int MaxResin = 5;
		private int currentResin;
		private readonly ResinUpdated updater;

		public Tracker(ResinUpdated updaterDelegate)
		{
			updater = updaterDelegate;
		}

		public void Reset()
		{
			currentResin = 0;

			do
			{
				Thread.Sleep(2000);
				currentResin += 1;
				updater?.Invoke(currentResin);

			} while (currentResin < MaxResin);

			updater?.Invoke(currentResin);
		}
	}
}