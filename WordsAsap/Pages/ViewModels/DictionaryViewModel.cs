using FirstFloor.ModernUI.Presentation;
using FirstFloor.ModernUI.Windows.Controls;
using System.Collections.ObjectModel;
using System.Windows;
using WordsAsap.Entities;
using WordsAsap.Pages.ViewModels;
using System.Collections.Generic;
using System.Linq;

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


        //TODO: use nhibernate join
        private async void LoadWordsCollection(NHibernate.Criterion.ICriterion expression = null)
        {
            var allWords = new List<Word>();
            WordsCollection.Clear();
            var collection = await WordsService.GetDataAsync<Word>(expression);
            if (collection != null)
            {
                foreach (var item in collection)
                    if(!allWords.Exists(x=>x.Id == item.Id))
                        allWords.Add(item);
            }
            foreach (var item in allWords)
                WordsCollection.Add(new WordViewModel { WordToDisplay = item });
            var alreadyAdded = allWords.Count;
            //only in case of searching it does matter
            if (expression != null)
            {
                var collection2 = await WordsService.GetDataAsync<Translation>(expression);
                if (collection2 != null)
                {
                    
                    foreach (var item in collection2)
                        foreach (var w in item.WordsStoredIn)
                            if (!allWords.Exists(x => x.Id == w.Id))
                                allWords.Add(w);
                    
                }
            }

            foreach (var item in allWords.Skip(alreadyAdded))
                WordsCollection.Add(new WordViewModel { WordToDisplay = item });
            allWords = null;
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
