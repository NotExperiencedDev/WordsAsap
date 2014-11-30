using System;
using System.ComponentModel;
using System.Windows;

namespace WordsAsap
{
    
    public partial class MainWindow
    {        
        private WindowState _storedWindowState;
        private readonly ShowWord _showWord;

        public MainWindow()
        {
            InitializeComponent();
            NotifyIcon.SystryIcon.Click += NotifyIcon_Click;
            
          
            _showWord = new ShowWord(Properties.Settings.Default.WordDialogShowInterval, Dispatcher);
        }

        private void OnClose(object sender, CancelEventArgs args)
        {
           
        }

        private void OnStateChanged(object sender, EventArgs args)
        {
            if (WindowState == WindowState.Minimized)
            {
                Hide();
                NotifyIcon.SystryIcon.ShowBaloonTip(
                    1700,
                    ApplicationInfo.TitleFunc(),
                    string.Format("The {0} has been minimised. Click the tray icon to show it back.", ApplicationInfo.TitleFunc())
                    );
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
            NotifyIcon.SystryIcon.Show = show;
        }
    }
}
