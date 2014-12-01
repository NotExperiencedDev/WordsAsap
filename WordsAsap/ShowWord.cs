using System;
using System.Timers;
using System.Windows.Threading;
using WordsAsap.Entities;
using WordsAsap.WordsServices;

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
        private Random _random;
        private WordsCollectionService _wordsCollectionService;
        private object _locker;
               
        public ShowWord(int intervalInMinutes, Dispatcher context)
        {
            _locker = new object();
            var interval = intervalInMinutes * 60 * 1000;
            if (System.Diagnostics.Debugger.IsAttached)
                interval = 10 * 1000;

            _context = context;
            _showDialog = ShowDialog;
            _timer = new Timer { Enabled = false, Interval = interval };
            _timer.Elapsed += OnElapsed;
            _random = new Random();
            _wordsCollectionService = WordsCollectionService.CreateWordsCollectionService(WordsSettings.GetWordsAsapSettings());
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
            if (WordsSettings.GetWordsAsapSettings().ShowWordInPopupDialog)
            {
                if (_wordDialog != null)
                    _wordDialog.Close();

                _wordDialog = new WordDialog();

                _wordDialog.ShowDialog();
                lock(_locker)
                    _timer.Enabled = true;
            }
            else
            {
                NotifyIcon.SystryIcon.ShowBaloonTip( );
                NotifyIcon.SystryIcon.BalloonClosed += OnBallonClosed;
            }
            
        }

        private void OnBallonClosed(object sender, EventArgs args)
        {
            NotifyIcon.SystryIcon.BalloonClosed -= OnBallonClosed;
            lock(_locker)
                _timer.Enabled = true;
        }
    }
}