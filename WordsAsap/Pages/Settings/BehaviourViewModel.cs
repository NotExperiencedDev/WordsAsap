using FirstFloor.ModernUI.Presentation;
using FirstFloor.ModernUI.Windows.Controls;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using WordsAsap.WordsServices;

namespace WordsAsap.Pages.Settings
{
    /// <summary>
    /// A simple view model for configuring theme, font and accent colors.
    /// </summary>
    public class BehaviourViewModel
        : NotifyPropertyChanged
    {
        IWordsCollectionSettingsService _settings;
        public BehaviourViewModel()
        {
            _settings = SettingsServiceFactory.GetWordsAsapSettings();
            WordDialogShowInterval = _settings.WordDialogShowInterval;
            WordsCollectionStorage = _settings.CollectionStorage;
        }

        private int _wordDialogShowInterval;
        private string _wordsCollectionStorageFile;

        public int WordDialogShowInterval
        {
            get { return _wordDialogShowInterval; }
            set
            {
                _wordDialogShowInterval = value;
                OnPropertyChanged("WordDialogShowInterval");
            }
        }

        public string WordsCollectionStorage
        {
            get { return _wordsCollectionStorageFile; }
            set 
            { 
                _wordsCollectionStorageFile = value; 
                OnPropertyChanged("WordsCollectionStorage"); 
            }
        }

      
        public ICommand UpCommand
        {
            get { return new RelayCommand((o) => { WordDialogShowInterval++; }); }
        }

        public ICommand DownCommand
        {
            get { return new RelayCommand((o) => { WordDialogShowInterval--; }, (o1) => { return WordDialogShowInterval > 1; }); }
        }
             

        public ICommand SaveSettingsCommand
        {
            get { return new RelayCommand(SaveSettings); }
        }

        private void SaveSettings(object o)
        {
            _settings.WordDialogShowInterval = WordDialogShowInterval;
            _settings.CollectionStorage = WordsCollectionStorage;
            ModernDialog.ShowMessage("settings saved", "save settings", MessageBoxButton.OK);
        }
    }
      

   
}
