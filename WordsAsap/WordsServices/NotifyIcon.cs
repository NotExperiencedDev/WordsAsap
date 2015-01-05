using FirstFloor.ModernUI.Presentation;
using Hardcodet.Wpf.TaskbarNotification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WordsAsap.Pages;

namespace WordsAsap
{
    public class NotifyIcon
    {
        private static NotifyIcon _notifyIcon;

        TaskbarIcon _taskBarIcon;
        public event EventHandler Click;
        public event EventHandler BalloonClosed;
       
        private NotifyIcon()
        {
            _taskBarIcon = new TaskbarIcon();
            _taskBarIcon.Icon = Resource.acorn_7_16;
            _taskBarIcon.ToolTipText = ApplicationInfo.TitleFunc();
            _taskBarIcon.LeftClickCommand = Test;
        }

        private RelayCommand Test
        {
            get { return new RelayCommand((l) => { OnNotifyIconClick(); }); }
        }
        public static NotifyIcon SystryIcon
        {
            get
            {
                if (_notifyIcon == null)
                    _notifyIcon = new NotifyIcon();
                return _notifyIcon;
            }
        }

        private void OnNotifyIconClick()
        {
            if (Click != null)
                Click(this, new EventArgs());
        }

        public void ShowBaloonTip(int interval, string title, string description)
        {
            _taskBarIcon.ShowBalloonTip(title, description, BalloonIcon.Info);            
        }      

        public void ShowBaloonTip()
        {
            var wordPage = new WordPage();
           _taskBarIcon.ShowCustomBalloon(wordPage, System.Windows.Controls.Primitives.PopupAnimation.Slide, null);
           _taskBarIcon.CustomBalloon.Closed += CustomBalloon_Closed;
        }

        void CustomBalloon_Closed(object sender, EventArgs e)
        {
            if(_taskBarIcon.CustomBalloon != null)
                _taskBarIcon.CustomBalloon.Closed += CustomBalloon_Closed;
            if (BalloonClosed != null)
                BalloonClosed(sender, e);
        }

        public void CloseBalloon()
        {
            if (_taskBarIcon != null)
                _taskBarIcon.CloseBalloon();
        }

        public bool Show
        {
            set
            {
                if (!value)
                    _taskBarIcon.CloseBalloon();
                _taskBarIcon.Visibility = value ? System.Windows.Visibility.Visible : System.Windows.Visibility.Collapsed;
            }
        }

    }
}
