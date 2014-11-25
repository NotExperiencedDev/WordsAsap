using FirstFloor.ModernUI.Presentation;
using FirstFloor.ModernUI.Windows.Controls;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using WordsAsap.Entities;
using WordsAsap.WordsServices;

namespace WordsAsap.Pages
{
    public class WordViewModel : NotifyPropertyChanged
    {      
        private IWordsCollectionService _wordsCollectionService;
        public WordViewModel(){
             _wordsCollectionService = WordsCollectionServiceFactory.CreateWordsCollectionService(SettingsServiceFactory.GetWordsAsapSettings());
             
        }
              

        public virtual string FirstTranslation
        {
            get
            {
                if (WordToDisplay == null)
                    return string.Empty;
                if (WordToDisplay.Translations != null)
                {
                    var first = WordToDisplay.Translations.FirstOrDefault();
                    if (first != null)
                        return first.Value;
                }
                return string.Empty;
            }
        }

        public Word WordToDisplay { get; set; }

        public bool ShowOtherTranslations { get; set; }
        public bool ShowFirstTranslation { get { return !ShowOtherTranslations; } }

        public IList<Translation> Translations
        {
            get
            {
                if (WordToDisplay == null)
                    return null;
                return WordToDisplay.Translations;
            }
        }

        public RelayCommand ShowOtherTranslationsCommand
        {
            get
            {
                return new RelayCommand(ShowOtherTranslationsManager);
               
            }
        }


        private void ShowOtherTranslationsManager(object o)
        {
            ShowOtherTranslations = !ShowOtherTranslations;
            OnPropertyChanged("ShowOtherTranslations");
            OnPropertyChanged("ShowFirstTranslation");
        }

        public RelayCommand RemoveTranslationCommand
        {
            get { return new RelayCommand(RemoveTranslation);}
        }

        private void RemoveTranslation(object o)
        {
            var translation = o as Translation;
            if (translation == null)
                return;

            var r = ModernDialog.ShowMessage("do you really want to remove trnaslation?", "translation removing", MessageBoxButton.YesNo);
            if (r == MessageBoxResult.No)
                return;

            WordToDisplay.Translations.Remove(translation);
            _wordsCollectionService.Update<Word>(WordToDisplay);
            //hack: cannot figure it out how to refresh the view right now
            ShowOtherTranslations = !ShowOtherTranslations;
            OnPropertyChanged("ShowOtherTranslations");
            ShowOtherTranslations = !ShowOtherTranslations;
            OnPropertyChanged("ShowOtherTranslations");
            OnPropertyChanged("ShowFirstTranslation");
        }
       
    }
}
