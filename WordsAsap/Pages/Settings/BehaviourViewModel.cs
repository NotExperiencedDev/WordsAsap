﻿using FirstFloor.ModernUI.Presentation;
using System.IO;

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

        public int WordDialogShowInterval
        {
            get { return _settings.WordDialogShowInterval; }
            set
            {
                if (_settings.WordDialogShowInterval != value)
                {
                    _settings.WordDialogShowInterval = value;
                    OnPropertyChanged("WordDialogShowInterval");
                }
            }
        }

        public string WordsCollectionStorageFile
        {
            get { return _settings.CollectionStorageFile; }
            set 
            {
                if (_settings.CollectionStorageFile != value)
                {
                    _settings.CollectionStorageFile = value;
                    OnPropertyChanged("WordsCollectionStorageFile");
                }
            }
        }

        public string WordsCollectionStorageLocation
        {
            get { return _settings.CollectionStorageFolder; }
            set
            {
                if (_settings.CollectionStorageFolder != value)
                {
                    _settings.CollectionStorageFolder = value;
                    OnPropertyChanged("WordsCollectionStorageLocation");
                }
            }
        }

        public int MaxNumberOfWordsDisplays
        {
            get { return _settings.MaxNumberOfWordDisplays; }
            set
            {
                if (_settings.MaxNumberOfWordDisplays != value)
                {
                    _settings.MaxNumberOfWordDisplays = value;
                    OnPropertyChanged("MaxNumberOfWordsDisplays");
                }
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
