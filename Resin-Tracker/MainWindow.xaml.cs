using System;
using System.ComponentModel;
using System.Threading;
using System.Windows;
using System.Windows.Input;
using Hardcodet.Wpf.TaskbarNotification;
using Resin_Tracker.Properties;
using KeyEventArgs = System.Windows.Input.KeyEventArgs;
using MessageBox = System.Windows.MessageBox;

namespace Resin_Tracker
{
	public delegate void ResinUpdated(int currentResin);

	public partial class MainWindow : Window
	{
		private Tracker tracker;
		private Thread trackerThread;
		private readonly TaskbarIcon taskbarIcon;

		private const int MaxResin = 160;
		private const int MinutesPerResin = 8;

		public MainWindow()
		{
			InitializeComponent();

			ResetButton.Click += ResetButton_OnClick;
			ResinBlock.GotFocus += ResinBlockOnGotFocus;
			ResinBlock.KeyUp += ResinBlock_OnKeyUp;
			ResinBlock.LostKeyboardFocus += ResinBlock_OnKeyboardLostFocus;
			ResinBlock.Text = $"{Settings.Default.CurrentResinCount}/{MaxResin}";

			taskbarIcon = new TaskbarIcon {Icon = Properties.Resources.ResinIcon, ToolTipText = Properties.Resources.UIText_Credits};
			taskbarIcon.TrayMouseDoubleClick += TaskbarIconOnTray_MouseDoubleClick;
			taskbarIcon.TrayToolTipOpen += TaskbarIcon_OnTrayBalloonTipShown;
		}

		#region UIActions
		private void ResinBlockOnGotFocus(object sender, RoutedEventArgs e)
		{
			ResinBlock.Text = Settings.Default.CurrentResinCount.ToString();
		}

		private void ResinBlock_OnKeyUp(object sender, KeyEventArgs e)
		{
			switch (e.Key)
			{
				case Key.Enter:
				{
					var text = ResinBlock.Text;

					if (!string.IsNullOrEmpty(text) && int.TryParse(text, out var input))
					{
						ResetSettings(input);
						trackerThread.Interrupt();
						StartTrackerThread(Settings.Default.CurrentResinCount);
						Keyboard.ClearFocus();
					}
					else
					{
						Keyboard.ClearFocus();
					}

					break;
				}
			}
		}

		private void ResinBlock_OnKeyboardLostFocus(object sender, RoutedEventArgs e)
		{
			ResinBlock.Text = $"{Settings.Default.CurrentResinCount}/{MaxResin}";
		}

		private void ResetButton_OnClick(object sender, RoutedEventArgs e)
		{
			var result = MessageBox.Show(Properties.Resources.UIText_ResetQuestion, Properties.Resources.UIText_Credits, MessageBoxButton.YesNo);
			switch (result)
			{
				case MessageBoxResult.Yes:
					ResetSettings();
					trackerThread.Interrupt();
					StartTrackerThread(0);
					break;

				default:
					break;
			}
		}
		#endregion

		#region Main Window Events
		private void MainWindow_OnInitialized(object sender, EventArgs eventArgs)
		{
			if (Settings.Default.HasDataSaved == false)
			{
				ResetSettings();
			}
			else
			{
				var ts = DateTime.Now - Settings.Default.LastUpdateDate;
				if (ts.TotalMinutes > MinutesPerResin)
				{
					var resinToAdd = (int) ts.TotalMinutes / MinutesPerResin;

					if (Settings.Default.CurrentResinCount + resinToAdd > MaxResin)
					{
						Settings.Default.CurrentResinCount = MaxResin;
					}
					else
					{
						Settings.Default.CurrentResinCount += resinToAdd;
					}

					Settings.Default.LastUpdateDate = DateTime.Now;
				}
				Settings.Default.Save();

				StartTrackerThread(Settings.Default.CurrentResinCount);
			}
		}

		private void MainWindow_OnClosing(object sender, CancelEventArgs e)
		{
			trackerThread?.Interrupt();
			Settings.Default.Save();
			taskbarIcon.Dispose();
		}

		private void TaskbarIconOnTray_MouseDoubleClick(object sender, RoutedEventArgs e)
		{
			ShowInTaskbar = true;
			MainAppWindow.Show();
			MainAppWindow.Activate();
		}

		private void MainWindow_OnStateChanged(object sender, EventArgs e)
		{
			ShowInTaskbar = WindowState != WindowState.Minimized;
			if (ShowInTaskbar == false)
			{
				MainAppWindow.Hide();
			}
			else
			{
				MainAppWindow.Activate();
			}
		}

		private void TaskbarIcon_OnTrayBalloonTipShown(object sender, RoutedEventArgs e)
		{
			var ts = Settings.Default.FinishDate - DateTime.Now;
			taskbarIcon.ToolTipText = string.Join(string.Empty, $"{Settings.Default.CurrentResinCount}/{MaxResin} Resin", Environment.NewLine, ts.Hours, ":", ts.Minutes, ":", ts.Seconds, " Time Remaining");
		}
		#endregion

		#region Private Methods
		private void StartTrackerThread(int startResin)
		{
			tracker = new Tracker(startResin, MaxResin, CounterCallback);
			trackerThread = new Thread(tracker.Reset);
			trackerThread.Start();
		}

		private static int GetMinutesUntilResinFull(int currentResin)
		{
			return (MaxResin - currentResin) * MinutesPerResin;
		}

		private static void ResetSettings(int startResin = 0)
		{
			Settings.Default.CurrentResinCount = startResin;
			Settings.Default.LastUpdateDate = DateTime.Now;
			Settings.Default.FinishDate = DateTime.Now.Add(TimeSpan.FromMinutes(GetMinutesUntilResinFull(startResin)));
			Settings.Default.HasDataSaved = true;
		}
		#endregion

		#region delegates
		public void CounterCallback(int resinCount)
		{
			Dispatcher.Invoke(() =>
				{
					Settings.Default.CurrentResinCount = resinCount;
					Settings.Default.LastUpdateDate = DateTime.Now;
					Settings.Default.Save();
					ResinBlock.Text = $"{Settings.Default.CurrentResinCount}/{MaxResin}";
				});
		}
		#endregion
	}
}
