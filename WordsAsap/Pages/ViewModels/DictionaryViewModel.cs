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

        private void LoadWordsCollection(NHibernate.Criterion.ICriterion expression = null)
        {
            WordsCollection.Clear();
            var collection = WordsService.GetData<Word>(expression);
            if (collection != null)
            {
                WordsCollection.Clear();
                foreach (var item in collection)
                    WordsCollection.Add(new WordViewModel { WordToDisplay = item });
            }
        }

        public ObservableCollection<WordViewModel> WordsCollection { get; set; }

        public string SearchText { get; set; }

        public RelayCommand RemoveWordCommand
        {
            get { return new RelayCommand(RemoveWord, CanRemoveWord); }
        }

        public RelayCommand SearchCommand
        {
            get { return new RelayCommand(SearchWord); }
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

        private void SearchWord(object o)
        {
            var searchText = o as string;
            if (string.IsNullOrWhiteSpace(searchText))
            {
                LoadWordsCollection();
                return;
            }
            var sql = NHibernate.Criterion.Expression.Sql("lower({alias}.Value) like lower(?)", string.Format("%{0}%",searchText), NHibernate.NHibernateUtil.String);
            LoadWordsCollection(sql);
        }
    }
}
