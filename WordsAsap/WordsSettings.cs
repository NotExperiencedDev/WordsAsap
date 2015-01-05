using FirstFloor.ModernUI.Presentation;
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
                if (Properties.Settings.Default.WordsCollectionStorageFile != value)
                {
                    Properties.Settings.Default.WordsCollectionStorageFile = value;
                    SaveSettings();
                }
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
                if (Properties.Settings.Default.WordsCollectionStorageFolder != value)
                {
                    Properties.Settings.Default.WordsCollectionStorageFolder = value;
                    SaveSettings();
                }
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
                if (Properties.Settings.Default.WordDialogShowInterval != value)
                {
                    Properties.Settings.Default.WordDialogShowInterval = value;
                    SaveSettings();
                }
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
                if (Properties.Settings.Default.MaxNumberOfWordDisplays != value)
                {
                    Properties.Settings.Default.MaxNumberOfWordDisplays = value;
                    SaveSettings();
                }
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
                if (Properties.Settings.Default.ShowWordInPopupDialog != value)
                {
                    Properties.Settings.Default.ShowWordInPopupDialog = value;
                    SaveSettings();
                }
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
                if (Properties.Settings.Default.BalloonTipTransparency != value)
                {
                    Properties.Settings.Default.BalloonTipTransparency = value;
                    SaveSettings();
                }
            }
        }

        public string AccentColor
        {
            get
            {
                return Properties.Settings.Default.AccentColor;
            }
            set
            {
                if (Properties.Settings.Default.AccentColor != value)
                {
                    Properties.Settings.Default.AccentColor = value;
                    SaveSettings();
                }
            }
        }

        public Uri ThemeSource
        {
            get
            {
                return Properties.Settings.Default.ThemeSource;
            }
            set
            {
                if (Properties.Settings.Default.ThemeSource != value)
                {
                    Properties.Settings.Default.ThemeSource = value;
                    SaveSettings();
                }
            }
        }

        public FontSize SelectedFontSize
        {
            get
            {
                return Properties.Settings.Default.SelectedFontSize;
            }
            set
            {
                if (value != Properties.Settings.Default.SelectedFontSize)
                {
                    Properties.Settings.Default.SelectedFontSize = value;
                    SaveSettings();
                }
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
