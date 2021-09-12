using System;
using System.ComponentModel;
using System.Threading;
using System.Windows;
using System.Windows.Input;
using Hardcodet.Wpf.TaskbarNotification;
using Resin_Tracker.Properties;

namespace Resin_Tracker
{
	public delegate void ResinUpdated(int currentResin);

	public partial class MainWindow : Window
	{
		private Tracker tracker;
		private Thread trackerThread;
		private int currentResin;
		private DateTime lastUpdateTime;

		public MainWindow()
		{
			InitializeComponent();

			ResetButton.Click += ResetButtonOnClick;
			ResinBlock.MouseLeftButtonUp +=ResinBlockOnMouseLeftButtonUp;

			ResinBlock.Text = $"{currentResin}/160";

			var taskbarIcon = new TaskbarIcon {Icon = Properties.Resources.ResinIcon, ToolTipText = "Hello!"};
			taskbarIcon.TrayLeftMouseDown += TaskbarIconOnTray_MouseClick;
		}

		#region UIActions
		private void ResinBlockOnMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
		{
			ResinBlock.Text = "160/160";
		}

		private void ResetButtonOnClick(object sender, RoutedEventArgs e)
		{
			ResinBlock.Text = "2137";
		}
		#endregion

		#region Main Window Events
		private void MainWindow_OnInitialized(object sender, EventArgs eventArgs)
		{
			if (Settings.Default.currentResinCount == 0 || Settings.Default.LastUpdateDate.Equals(null))
			{
				Settings.Default.currentResinCount = 0;
				Settings.Default.LastUpdateDate = DateTime.Now;
			}

			currentResin = Settings.Default.currentResinCount;
			lastUpdateTime = Settings.Default.LastUpdateDate;

			StartTrackerThread(currentResin);
		}

		private void MainWindow_OnClosing(object sender, CancelEventArgs e)
		{
			Settings.Default.Save();
		}

		private void MainWindow_OnStateChanged(object sender, EventArgs e)
		{
			ShowInTaskbar = WindowState != WindowState.Minimized;
		}

		private void TaskbarIconOnTray_MouseClick(object sender, RoutedEventArgs e)
		{
			if (WindowState == WindowState.Minimized)
			{
				WindowState = WindowState.Normal;
			}
		}
		#endregion

		#region Private Methods
		private void StartTrackerThread(int startResin)
		{
			tracker = new Tracker(startResin, new ResinUpdated(CounterCallback));
			trackerThread = new Thread(new ThreadStart(tracker.Reset));
			trackerThread.Start();
		}
		#endregion

		#region delegates
		public void CounterCallback(int resinCount)
		{
			Dispatcher.Invoke(() =>
				{
					ResinBlock.Text = $"{resinCount}/160";
					currentResin = resinCount;
					Settings.Default.currentResinCount = currentResin;
					lastUpdateTime = DateTime.Now;
					Settings.Default.LastUpdateDate = lastUpdateTime;
					Settings.Default.Save();
				});
		}
		#endregion
	}
}
