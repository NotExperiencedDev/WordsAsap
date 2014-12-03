using System.Windows.Input;
using FirstFloor.ModernUI.Presentation;
using FirstFloor.ModernUI.Windows.Controls;
using System.Collections.ObjectModel;
using System.Windows;
using WordsAsap.Entities;
using WordsAsap.WordsServices;
using WordsAsap.Pages.ViewModels;

namespace WordsAsap.Pages
{
    public class DictionaryViewModel : BaseViewModel
    {  
        protected override void InitializeModel()
        {
            WordsCollection = new ObservableCollection<WordViewModel>();
            LoadWordsCollection();
            WordsService.WordsCollectionChanged += WordsCollectionService_WordsCollectionChanged;
        }

        void WordsCollectionService_WordsCollectionChanged(object sender, System.EventArgs e)
        {
            LoadWordsCollection();
        }

        private void LoadWordsCollection()
        {
            WordsCollection.Clear();
            var collection = WordsService.GetData<Word>();
            if (collection != null)
                foreach (var item in collection)
                    WordsCollection.Add(new WordViewModel { WordToDisplay = item });
        }

        public ObservableCollection<WordViewModel> WordsCollection { get; set; }

        public RelayCommand RemoveWordCommand
        {
            get { return new RelayCommand(RemoveWord, CanRemoveWord); }
        }

        private bool CanRemoveWord(object o)
        {
            return WordsService != null;
        }

        private void RemoveWord(object o)
        {
            var word = o as WordViewModel;
            if (word == null)
                return;

            var r = ModernDialog.ShowMessage("do you really want to remove word?", "word removing", MessageBoxButton.YesNo);
            if (r == MessageBoxResult.No)
                return;

            WordsService.Remove<Word>(word.WordToDisplay);

            WordsCollection.Remove(word);            
        }
    }
}
