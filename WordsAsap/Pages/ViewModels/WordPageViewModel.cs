using FirstFloor.ModernUI.Presentation;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using WordsAsap.Entities;
using WordsAsap.Pages.ViewModels;
using WordsAsap.WordsServices;

namespace WordsAsap.Pages
{
    public class WordPageViewModel:BaseViewModel
    {

        private Random _random;
        private int _maxNumberOfWordDisplays;

        public Word WordToLearn { get; set; }
        
        protected override void InitializeModel()
        {
            ShowTranslation = false;
            OnPropertyChanged("ShowTranslation");
            _random = new Random();
            _maxNumberOfWordDisplays = WordsSettings.GetWordsAsapSettings().MaxNumberOfWordDisplays;
            GetWordToShow();  
        }

        private void GetWordToShow()
        {
            WordToLearn = GetWordToLearn.WordToLearn.GetNextWord();
            OnPropertyChanged("WordToLearn");
        }

        public RelayCommand ShowTranslationCommand
        {
            get { return new RelayCommand((o) => { ShowTranslation = !ShowTranslation; OnPropertyChanged("ShowTranslation"); }); }
        }


        public RelayCommand RightAnswer
        {
            get { return new RelayCommand(RightAnswerCommand, CanExecuteRightAnswer); }
        }

        public RelayCommand WrongAnswer
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
            else if(page.Parent != null)
            {
                var popup = page.Parent as Popup;
                if (popup != null)
                    popup.IsOpen = false;
            }
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
            WordsService.Update<WordStatistics>(statistics);
        }

        public bool ShowTranslation { get; set; }
    }
}
