using System.Threading;

namespace Resin_Tracker
{
	public class Tracker
	{
		private const int MaxResin = 160;
		private int currentResin;
		private readonly ResinUpdated updater;

		public Tracker(int startResin, ResinUpdated updaterDelegate)
		{
			currentResin = startResin;
			updater = updaterDelegate;
		}

		public void Reset()
		{
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