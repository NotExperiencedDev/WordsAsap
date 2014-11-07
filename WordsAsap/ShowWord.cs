using System.Timers;
using System.Windows;
using System.Windows.Threading;
using FirstFloor.ModernUI.Windows.Controls;

namespace WordsAsap
{
    public class ShowWord
    {
        public delegate MessageBoxResult ShowDialogDelegate();
        private ShowDialogDelegate m_showDialog;
        private Dispatcher _context;
        private Timer _timer;
        private bool _paused;

        public ShowWord(int intervalInSeconds, Dispatcher context)
        {
            _context = context;
            m_showDialog = ShowDialog;
            _timer = new Timer { Enabled = false, Interval = intervalInSeconds };
            _timer.Elapsed += OnElapsed;
        }

        public void Resume()
        {
            _paused = false;
            _timer.Enabled = true;
        }

        public void Pause()
        {
            _paused = true;
        }

        private void OnElapsed(object sender, ElapsedEventArgs e)
        {
            if (_paused)
            {
                if (_timer.Enabled)
                    _timer.Enabled = false;
                return;
            }
            _timer.Enabled = false;
            Execute();
        }

        public void Execute()
        {
            _context.BeginInvoke(DispatcherPriority.Normal, m_showDialog);
        }

        private MessageBoxResult ShowDialog()
        {
            var result = ModernDialog.ShowMessage("TODO: Display another word to learn", "word asap", MessageBoxButton.OK);
            _timer.Enabled = true;
            return result;
        }
    }
}