using FirstFloor.ModernUI.Presentation;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using WordsAsap.Entities;
using WordsAsap.Pages.ViewModels;
using WordsAsap.WordsServices;

namespace WordsAsap.Pages
{
    public class WordPageViewModel:BaseViewModel
    {

        private Random _random;
        private int _maxNumberOfWordDisplays;

        
        
        protected override void InitializeModel()
        {
            Translations = new ObservableCollection<string>();
            ShowTranslation = false;
            OnPropertyChanged("ShowTranslation");
            _random = new Random();
            _maxNumberOfWordDisplays = WordsSettings.WordsAsapSettings.MaxNumberOfWordDisplays;
            GetWordToShow();  
        }

        private void GetWordToShow()
        {
            WordToLearn = GetWordToLearn.WordToLearn.GetNextWord();
            if (WordToLearn == null)
                return;

            var wordTranslationDirection = _random.Next(1, 1000) < 501;
            if(wordTranslationDirection)
            {
                WordToDisplay = WordToLearn.Value;
                Translations.Clear();
                foreach (var t in WordToLearn.Translations)
                    Translations.Add(t.Value);
            }
            else
            {
                var numberOfTrnaslations = WordToLearn.Translations.Count;
                var selectedTranslationNr = _random.Next(0, numberOfTrnaslations);
                if (selectedTranslationNr > -1)
                {
                    var selectedTranslation = WordToLearn.Translations.ElementAt(selectedTranslationNr);
                    if (selectedTranslation != null)
                    { 
                        WordToDisplay = selectedTranslation.Value;
                        var otherTranslations = WordToLearn.Translations.Where(x => x.Id != selectedTranslation.Id);
                        Translations.Clear();
                        Translations.Add(WordToLearn.Value);
                        foreach (var t in otherTranslations)
                            Translations.Add(t.Value);
                    }
                }
            }

            OnPropertyChanged("WordToDisplay");
        }      

        public Word WordToLearn { get; set; }

        public string WordToDisplay { get; set; }

        public ObservableCollection<string> Translations
        {
            get;
            set;
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

        //TODO: redo it - shouldn't care about the host/view
        private void TryCloseWindow(object obj)
        {
            if (WordsSettings.WordsAsapSettings.ShowWordInPopupDialog)
            {
                var page = obj as UserControl;
                if (page == null)
                    return;

                var window = Window.GetWindow(page);
                if (window != null)
                    window.Close();
            }
            else 
                 NotifyIcon.SystryIcon.CloseBalloon();
            
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

        public SolidColorBrush FontColor
        {
            get
            {
                if (AppearanceManager.DarkThemeSource == AppearanceManager.Current.ThemeSource)
                    return new SolidColorBrush( Colors.White);

                return new SolidColorBrush( Colors.Black);
            }
        }

        public int FontSize
        {
            get
            {
                if( AppearanceManager.Current.FontSize == FirstFloor.ModernUI.Presentation.FontSize.Large)
                    return 16;
                return 12;
            }
        }
    }
}
