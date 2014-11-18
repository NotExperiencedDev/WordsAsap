using FirstFloor.ModernUI.Presentation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using WordsAsap.Entities;
using WordsAsap.WordsServices;

namespace WordsAsap.Pages
{
    public class WordPageViewModel:NotifyPropertyChanged
    {

        private Random _random;
        private IWordsCollectionService _wordsCollectionService;


        public Word WordToLearn { get; set; }

        public WordPageViewModel()
        {
            ShowTranslation = Visibility.Collapsed;
            OnPropertyChanged("ShowTranslation");
            _random = new Random();
            _wordsCollectionService = WordsCollectionServiceFactory.CreateWordsCollectionService(SettingsServiceFactory.GetWordsAsapSettings());
            GetWordToShow();
        }

        private void GetWordToShow()
        {
            var words = _wordsCollectionService.GetData<Word>();
            if (words == null || words.Count < 1)
                return;
            
            
            var selection = _random.Next(0, words.Count > 5 ? 4: words.Count - 1);
            WordToLearn = words
                .OrderBy(x => x.CreationDate)
                .ThenBy(x=>x.Statistics.WrongAnswers)
                .ThenBy(x=>x.Statistics.LastDisplayTime)
                .ThenByDescending(x=>x.Statistics.NumberOfDisplays)
                .ToArray()[selection];
            OnPropertyChanged("WordToLearn");
        }

        public ICommand ShowTranslationCommand
        {
            get { return new RelayCommand((o) => { ShowTranslation = Visibility.Visible; OnPropertyChanged("ShowTranslation"); }); }
        }


        public ICommand RightAnswer
        {
            get { return new RelayCommand(RightAnswerCommand, CanExecuteRightAnswer); }
        }

        public ICommand WrongAnswer
        {
            get { return new RelayCommand(WrongAnswerCommand, CanExecuteWrongAnswer); }
        }

        private bool CanExecuteWrongAnswer(object arg)
        {
            return WordToLearn != null;
        }

        private void WrongAnswerCommand(object obj)
        {
            UpdateStatistics();
            WordToLearn.Statistics.WrongAnswers++;
            SaveStatistics(WordToLearn.Statistics);
            TryCloseWindow(obj);
        }

        private void TryCloseWindow(object obj)
        {
            var page = obj as UserControl;
            if (page == null)
                return;

            var window = Window.GetWindow(page);
            if (window != null)
                window.Close();
        }

        private void RightAnswerCommand(object obj)
        {
            UpdateStatistics();
            WordToLearn.Statistics.CorrectAnswers++;
            SaveStatistics(WordToLearn.Statistics);
            TryCloseWindow(obj);
        }

        private bool CanExecuteRightAnswer(object arg)
        {
            return WordToLearn != null;
        }

        private void UpdateStatistics()
        {
            WordToLearn.Statistics.NumberOfDisplays++;
            WordToLearn.Statistics.LastDisplayTime = DateTime.UtcNow;
        }

        private void SaveStatistics(WordStatistics statistics)
        {
            _wordsCollectionService.Update<WordStatistics>(statistics);
        }

        public Visibility ShowTranslation { get; set; }
    }
}
