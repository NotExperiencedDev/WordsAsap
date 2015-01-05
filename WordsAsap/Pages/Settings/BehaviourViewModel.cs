using FirstFloor.ModernUI.Presentation;
using FirstFloor.ModernUI.Windows.Controls;
using System.ComponentModel;
using System.IO;
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
        private const int MinimuWordDisplayInterval = 1;
        private const int MinimuWordDisplayTimes = 5;
      
        WordsSettings _settings;

        public BehaviourViewModel()
        {
            _settings = WordsSettings.WordsAsapSettings;
            WordDialogShowInterval = _settings.WordDialogShowInterval;
            WordsCollectionStorageFile = _settings.CollectionStorageFile;
            WordsCollectionStorageLocation = _settings.CollectionStorageFolder;
            MaxNumberOfWordsDisplays = _settings.MaxNumberOfWordDisplays;
           
        }

        private int _wordDialogShowInterval;
        private string _wordsCollectionStorageFile;
        private string _wordsCollectionStorageLocation;
        private int _maxNumberOfWordsDisplay;
      

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

        public RelayCommand UpShowIntervalCommand
        {
            get { return new RelayCommand((o) => { WordDialogShowInterval++; }); }
        }

        public RelayCommand DownShowIntervalCommand
        {
            get { return new RelayCommand((o) => { WordDialogShowInterval--; }, (o) => { return WordDialogShowInterval > MinimuWordDisplayInterval; }); }
        }

        public RelayCommand UpMaxWordShowCommand
        {
            get { return new RelayCommand((o) => { MaxNumberOfWordsDisplays++; }); }
        }

        public RelayCommand DownMaxWordShowCommand
        {
            get { return new RelayCommand((o) => { MaxNumberOfWordsDisplays--; }, (o) => { return MaxNumberOfWordsDisplays > MinimuWordDisplayTimes; }); }
        }

        public RelayCommand SelectStorageLocationCommand
        {
            get { return new RelayCommand(SelectStorageLocation); }
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

            
            _settings.MaxNumberOfWordDisplays = MaxNumberOfWordsDisplays;
           

            ModernDialog.ShowMessage("settings saved", "save settings", MessageBoxButton.OK);
        }

        private void SelectStorageLocation(object o)
        {
            // Configure open file dialog box
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.FileName = Path.GetFileNameWithoutExtension( WordsCollectionStorageFile); // Default file name
            dlg.DefaultExt = string.Format(".{0}",WordsSettings.DefaultFileExtension); // Default file extension
            dlg.Filter = string.Format("Words Collections (.{0})|*.{0}", WordsSettings.DefaultFileExtension); // Filter files by extension
            dlg.InitialDirectory = WordsCollectionStorageLocation;
            // Show open file dialog box
            var result = dlg.ShowDialog();

            // Process open file dialog box results
            if (result == true)
            {
                WordsCollectionStorageLocation = Path.GetDirectoryName(dlg.FileName);
                WordsCollectionStorageFile = Path.GetFileName(dlg.FileName);
            }
        }
    }
      

   
}
