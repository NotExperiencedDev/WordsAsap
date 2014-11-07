using System;
using System.ComponentModel;
using System.Windows;


namespace WordsAsap
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        private System.Windows.Forms.NotifyIcon _notifyIcon;
        private WindowState _storedWindowState;
        private readonly ShowWord _showWord;

        public MainWindow()
        {
            InitializeComponent();
            _notifyIcon = new System.Windows.Forms.NotifyIcon
            {
                BalloonTipText =
                    string.Format("The {0} has been minimised. Click the tray icon to show it back.",
                        ApplicationInfo.TitleFunc()),
                BalloonTipTitle = ApplicationInfo.TitleFunc(),
                Text = ApplicationInfo.TitleFunc(),
                Icon = new System.Drawing.Icon("acorn-7-16.ico")
            };
            _notifyIcon.Click += NotifyIcon_Click;
            _showWord= new ShowWord(10000, Dispatcher);

        }

        private void OnClose(object sender, CancelEventArgs args)
        {
            _notifyIcon.Dispose();
            _notifyIcon = null;
        }

        private void OnStateChanged(object sender, EventArgs args)
        {
            if (WindowState == WindowState.Minimized)
            {
                Hide();
                if (_notifyIcon != null)
                    _notifyIcon.ShowBalloonTip(2000);
                _showWord.Resume();
            }
            else
            {
                _showWord.Pause();
                _storedWindowState = WindowState;
            }
        }

        private void OnIsVisibleChanged(object sender, DependencyPropertyChangedEventArgs args)
        {
            CheckTrayIcon();
        }

        private void NotifyIcon_Click(object sender, EventArgs e)
        {
            Show();
            WindowState = _storedWindowState;
        }

        private void CheckTrayIcon()
        {
            ShowTrayIcon(!IsVisible);
        }

        private void ShowTrayIcon(bool show)
        {
            if (_notifyIcon != null)
                _notifyIcon.Visible = show;
        }
    }
}
