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
        WordsSettings _settings;
        public BehaviourViewModel()
        {
            _settings = WordsSettings.GetWordsAsapSettings();
            WordDialogShowInterval = _settings.WordDialogShowInterval;
            WordsCollectionStorageFile = _settings.CollectionStorageFile;
            WordsCollectionStorageLocation = _settings.CollectionStorageFolder;
            MaxNumberOfWordsDisplays = _settings.MaxNumberOfWordDisplays;
            ShowWordInPopupDialog = _settings.ShowWordInPopupDialog;
        }

        private int _wordDialogShowInterval;
        private string _wordsCollectionStorageFile;
        private string _wordsCollectionStorageLocation;
        private int _maxNumberOfWordsDisplay;
        private bool _showWordInPopupDialog;

        public int WordDialogShowInterval
        {
            get { return _wordDialogShowInterval; }
            set
            {
                _wordDialogShowInterval = value;
                OnPropertyChanged("WordDialogShowInterval");
            }
        }

        public string WordsCollectionStorageFile
        {
            get { return _wordsCollectionStorageFile; }
            set 
            { 
                _wordsCollectionStorageFile = value; 
                OnPropertyChanged("WordsCollectionStorageFile"); 
            }
        }

        public string WordsCollectionStorageLocation
        {
            get { return _wordsCollectionStorageLocation; }
            set
            {
                _wordsCollectionStorageLocation = value;
                OnPropertyChanged("WordsCollectionStorageLocation");
            }
        }

        public int MaxNumberOfWordsDisplays
        {
            get {  return _maxNumberOfWordsDisplay;  }
            set
            {
                _maxNumberOfWordsDisplay = value;
                OnPropertyChanged("MaxNumberOfWordsDisplays");
            }
        }

        public bool ShowWordInPopupDialog
        {
            get { return _showWordInPopupDialog; }
            set
            {
                _showWordInPopupDialog = value;
                OnPropertyChanged("ShowWordInPopupDialog");
            }
        }


        public RelayCommand UpShowIntervalCommand
        {
            get { return new RelayCommand((o) => { WordDialogShowInterval++; }); }
        }

        public RelayCommand DownShowIntervalCommand
        {
            get { return new RelayCommand((o) => { WordDialogShowInterval--; }, (o) => { return WordDialogShowInterval > 2; }); }
        }

        public RelayCommand UpMaxWordShowCommand
        {
            get { return new RelayCommand((o) => { MaxNumberOfWordsDisplays++; }); }
        }

        public RelayCommand DownMaxWordShowCommand
        {
            get { return new RelayCommand((o) => { MaxNumberOfWordsDisplays--; }, (o) => { return MaxNumberOfWordsDisplays > 4; }); }
        }


        public RelayCommand SaveSettingsCommand
        {
            get { return new RelayCommand(SaveSettings); }
        }

        private void SaveSettings(object o)
        {
            _settings.WordDialogShowInterval = WordDialogShowInterval;
            _settings.CollectionStorageFile = WordsCollectionStorageFile;
            _settings.CollectionStorageFolder = WordsCollectionStorageLocation;

            _settings.ShowWordInPopupDialog = ShowWordInPopupDialog;
            _settings.MaxNumberOfWordDisplays = MaxNumberOfWordsDisplays;
            ModernDialog.ShowMessage("settings saved", "save settings", MessageBoxButton.OK);
        }
    }
      

   
}
