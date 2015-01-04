using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WordsAsap.WordsServices;

namespace WordsAsap
{
    public class WordsSettings
    {
        private static WordsSettings _wordsSettings;
        public static event EventHandler SettingsChanged;

        private WordsSettings()
        {

        }

        public static WordsSettings WordsAsapSettings
        {
            get
            {
                if (_wordsSettings == null)
                    _wordsSettings = new WordsSettings();
                return _wordsSettings;
            }
        }

        public static string DefaultFileExtension { get { return "db3"; } }

        public string CollectionStorageFile
        {
            get
            {
                return Properties.Settings.Default.WordsCollectionStorageFile;
            }
            set
            {
                Properties.Settings.Default.WordsCollectionStorageFile = value;
                SaveSettings();
            }
        }

        public string CollectionStorageFolder
        {
            get
            {
                if (string.IsNullOrEmpty(Properties.Settings.Default.WordsCollectionStorageFolder))
                {
                    var directory = Path.Combine(new string[] { System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments), ApplicationInfo.ProductFunc() });
                    Properties.Settings.Default.WordsCollectionStorageFolder = directory;
                    Properties.Settings.Default.Save();
                  
                }
                return Properties.Settings.Default.WordsCollectionStorageFolder;
            }
            set
            {
                Properties.Settings.Default.WordsCollectionStorageFolder = value;
                SaveSettings();
            }
        }

        public int WordDialogShowInterval
        {
            get 
            {
                return Properties.Settings.Default.WordDialogShowInterval; 
            }
            set
            {
                Properties.Settings.Default.WordDialogShowInterval = value;
                SaveSettings();
            }
        }

        public int MaxNumberOfWordDisplays 
        {
            get
            {
                return Properties.Settings.Default.MaxNumberOfWordDisplays;
            }
            set
            {
                Properties.Settings.Default.MaxNumberOfWordDisplays = value;
                SaveSettings();
            }
        }

        public bool ShowWordInPopupDialog 
        {
            get
            {
                return Properties.Settings.Default.ShowWordInPopupDialog;
            }
            set
            {
                Properties.Settings.Default.ShowWordInPopupDialog = value;
                SaveSettings();
            }
        }

        public double BalloonTipTransparency
        {
            get
            {
                return Properties.Settings.Default.BalloonTipTransparency;
            }
            set
            {
                Properties.Settings.Default.BalloonTipTransparency = value;
                SaveSettings();
            }
        }

        private static void OnSettingsChanged()
        {
            if (SettingsChanged != null)
                SettingsChanged(_wordsSettings, new EventArgs());
        }

        private static void SaveSettings()
        {
            Properties.Settings.Default.Save();
            OnSettingsChanged();
        }
    }
}
