using System.Timers;
using System.Windows.Threading;

namespace WordsAsap
{
    public class ShowWord
    {
        public delegate void ShowDialogDelegate();
        private readonly ShowDialogDelegate _showDialog;
        private readonly Dispatcher _context;
        private readonly Timer _timer;
        private bool _paused;
        private WordDialog _wordDialog;

        public ShowWord(int intervalInSeconds, Dispatcher context)
        {
            _context = context;
            _showDialog = ShowDialog;
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
            if (_timer.Enabled)
                _timer.Enabled = false;
            if(_wordDialog != null && (_wordDialog.IsActive || _wordDialog.IsVisible))
                _wordDialog.Close();
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
            _context.BeginInvoke(DispatcherPriority.Normal, _showDialog);
        }

        private void ShowDialog()
        {
            if(_wordDialog != null)
                    _wordDialog.Close();
                
            _wordDialog = new WordDialog();
            
            _wordDialog.ShowDialog();
            _timer.Enabled = true;
        }
    }
}