using System.Threading;

namespace Resin_Tracker
{
	public class Tracker
	{
		private readonly int maxResin;
		private int currentResin;
		private readonly ResinUpdated updater;

		public Tracker(int startResin, int totalResin, ResinUpdated updaterDelegate)
		{
			currentResin = startResin;
			maxResin = totalResin;
			updater = updaterDelegate;
		}

		public void Reset()
		{
			try
			{
				updater?.Invoke(currentResin);

				do
				{
					Thread.Sleep(480000);
					currentResin += 1;
					updater?.Invoke(currentResin);

				} while (currentResin < maxResin);

			}
			catch (ThreadInterruptedException)
			{
			}

		}
	}
}