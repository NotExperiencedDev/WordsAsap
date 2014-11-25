using FirstFloor.ModernUI.Presentation;
using FirstFloor.ModernUI.Windows.Controls;
using NHibernate.Criterion;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using WordsAsap.Entities;
using WordsAsap.WordsServices;

namespace WordsAsap.Pages
{
    public class HomeViewModel : NotifyPropertyChanged    
    {
        private IWordsCollectionService _wordsCollectionService;
        
        public string NewWord { get; set; }
        public ObservableCollection<TranslationItem> Translations { get; set; }
       
        public HomeViewModel()
        {
            _wordsCollectionService = WordsCollectionServiceFactory.CreateWordsCollectionService(SettingsServiceFactory.GetWordsAsapSettings());
            Translations = new ObservableCollection<TranslationItem>();
            Translations.Add(new TranslationItem { Translation = string.Empty });
        }

       
        public ICommand AddTranslationCommand
        {
            get { return new RelayCommand(AddTranslation); }
        }

        private void AddTranslation(object obj)
        {
            Translations.Add(new TranslationItem());
        }

        public ICommand RemoveTranslationCommand
        {
            get { return new RelayCommand(RemoveTranslation); }
        }

        private void RemoveTranslation(object o)
        {
            var t = o as TranslationItem;
            if (t != null)
                Translations.Remove(t);
        }

        public ICommand SaveWordCommand
        {
            get { return new RelayCommand(SaveWord); }
        }
       


        private void SaveWord(object o)
        {
            foreach (var t in Translations)
            {
                _wordsCollectionService.AddWord(NewWord, t.Translation);
            }
           
            SimpleExpression e = Restrictions.Eq("Value", NewWord);
            var reply = _wordsCollectionService.GetData<Word>(e);            

            ModernDialog.ShowMessage("word updated", "save or update word", MessageBoxButton.OK);

           
        }
        
    }

    public class TranslationItem
    {
        public string Translation { get; set; }
    }
}
