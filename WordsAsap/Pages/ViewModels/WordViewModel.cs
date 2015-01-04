using FirstFloor.ModernUI.Presentation;
using FirstFloor.ModernUI.Windows.Controls;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using WordsAsap.Entities;
using WordsAsap.WordsServices;

namespace WordsAsap.Pages
{
    public class WordViewModel : NotifyPropertyChanged
    {
        private WordsCollectionService _wordsCollectionService;
        Word _wordToDisplay;
        public WordViewModel(){
            _wordsCollectionService = WordsCollectionService.CreateWordsCollectionService(WordsSettings.WordsAsapSettings);
             Translations = new ObservableCollection<Translation>();
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

        public Word WordToDisplay
        {
            get { return _wordToDisplay; }
            set
            {
                _wordToDisplay = value;
                if (_wordToDisplay != null)
                {
                    Translations.Clear();
                    foreach (var item in _wordToDisplay.Translations)
                        Translations.Add(item);
                }
            }
        }

        public ObservableCollection<Translation> Translations
        {
            get;
            set;
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
            WordToDisplay = WordToDisplay;
        }
       
    }
}
