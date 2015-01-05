using System;
using System.Timers;
using System.Windows.Threading;
using WordsAsap.Entities;
using WordsAsap.WordsServices;

namespace WordsAsap
{
    public class ShowWord
    {
        private const int MinimumShowInterval = 1; //in minutes
        public delegate void ShowDialogDelegate();
        private readonly ShowDialogDelegate _showDialog;
        private readonly Dispatcher _context;
        private readonly Timer _timer;
        private bool _paused;
        private WordDialog _wordDialog;
        private Random _random;
        private WordsCollectionService _wordsCollectionService;
        private object _locker;
        double _top = double.NaN; 
        double _left = double.NaN;
        double _height = double.NaN; 
        double _width = double.NaN;

               
        public ShowWord( Dispatcher context)
        {
            _locker = new object();
            var intervalInMinutes = WordsSettings.WordsAsapSettings.WordDialogShowInterval;
            var interval = GetMiliseconds(intervalInMinutes);           

            if (interval < MinimumShowInterval)
                interval = GetMiliseconds(MinimumShowInterval);           

            _context = context;
            _showDialog = ShowDialog;
            _timer = new Timer { Enabled = false, Interval = interval };
            _timer.Elapsed += OnElapsed;
            _random = new Random();
            //TODO: refactor, handle correctly
            try
            {
                _wordsCollectionService = WordsCollectionService.CreateWordsCollectionService(WordsSettings.WordsAsapSettings);
            }
            catch (Exception e)
            {
                DefaultMessageService.MessageService.ShowErrorMessage("WordsAsap Error", e.ToString());
                //TODO: add log
            }
            WordsSettings.SettingsChanged += WordsSettings_SettingsChanged;
        }

        private static int GetMiliseconds(int intervalInMinutes)
        {
            var interval = intervalInMinutes * 60 * 1000;

            if (System.Diagnostics.Debugger.IsAttached)
                interval = 10 * 1000;

            return interval;
        }

        void WordsSettings_SettingsChanged(object sender, EventArgs e)
        {
            var interval = GetMiliseconds(WordsSettings.WordsAsapSettings.WordDialogShowInterval);
            if (_timer.Interval == interval)
                return;
            var wasEnabled = _timer.Enabled;
            _timer.Enabled = false;
            _timer.Interval = GetMiliseconds(WordsSettings.WordsAsapSettings.WordDialogShowInterval);
            _timer.Enabled = wasEnabled;
        }

        public void Resume()
        {
            lock (_locker)
            {
                _paused = false;
                _timer.Enabled = true;
            }
        }

        public void Pause()
        {
            lock (_locker)
            {
                _paused = true;            
                if (_timer.Enabled)
                    _timer.Enabled = false;
            }
            if(_wordDialog != null && (_wordDialog.IsActive || _wordDialog.IsVisible))
                _wordDialog.Close();
        }

        private void OnElapsed(object sender, ElapsedEventArgs e)
        {
            lock (_locker)
            {
                if (_paused)
                {
                    if (_timer.Enabled)
                        _timer.Enabled = false;
                    return;
                }
                _timer.Enabled = false;
            }
            Execute();
        }

        public void Execute()
        {
            _context.BeginInvoke(DispatcherPriority.Normal, _showDialog);
        }

        private void ShowDialog()
        {
            if (WordsSettings.WordsAsapSettings.ShowWordInPopupDialog)
            {
                ShowPopuDialog();
            }
            else
            {
                NotifyIcon.SystryIcon.ShowBaloonTip( );
                NotifyIcon.SystryIcon.BalloonClosed += OnBallonClosed;
            }
            
        }

        private void ShowPopuDialog()
        {
            if (_wordDialog != null)
                _wordDialog.Close();

            _wordDialog = new WordDialog();
            _wordDialog.Topmost = true;
            _wordDialog.Focusable = false;
            if (double.IsNaN(_top))
                _wordDialog.WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            else
            {
                _wordDialog.WindowStartupLocation = System.Windows.WindowStartupLocation.Manual;
                _wordDialog.Top = _top; _wordDialog.Left = _left;
                _wordDialog.Height = _height; _wordDialog.Width = _width;
            }
            _wordDialog.ShowDialog();
            _top = _wordDialog.Top;
            _left = _wordDialog.Left;
            _height = _wordDialog.ActualHeight;
            _width = _wordDialog.ActualWidth;
            lock (_locker)
                _timer.Enabled = true;
        }
      
        private void OnBallonClosed(object sender, EventArgs args)
        {
            NotifyIcon.SystryIcon.BalloonClosed -= OnBallonClosed;
            lock(_locker)
                _timer.Enabled = true;
        }
    }
}