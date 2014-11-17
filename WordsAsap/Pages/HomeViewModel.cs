using FirstFloor.ModernUI.Presentation;
using FirstFloor.ModernUI.Windows.Controls;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using WordsAsap.Entities;
using WordsAsap.WordsServices;

namespace WordsAsap.Pages
{
    public class HomeViewModel : NotifyPropertyChanged    
    {
        private IWordsCollectionService _wordsCollectionService;
        private Word _selectedWord;
        public string NewWord { get; set; }
        public string Translation { get; set; }
        public IList<Word> WordsCollection { get; set; }
        public IList<Translation> WordsTranslations { get; set; }

       
        public HomeViewModel()
        {
            _wordsCollectionService = WordsCollectionServiceFactory.CreateWordsCollectionService(SettingsServiceFactory.GetWordsAsapSettings());
            WordsCollection = _wordsCollectionService.GetData<Word>();
        }

        public ICommand SaveSettingsCommand
        {
            get { return new RelayCommand(SaveSettings); }
        }

        private void SaveSettings(object o)
        {
            _wordsCollectionService.AddWord(NewWord, Translation);
            WordsCollection = _wordsCollectionService.GetData<Word>();
            OnPropertyChanged("WordsCollection");
           
            ModernDialog.ShowMessage("settings saved", "save settings", MessageBoxButton.OK);
        }
    }
}
