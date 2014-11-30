using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WordsAsap.WordsServices;

namespace WordsAsap
{
    public class WordsSettings
    {
        private static WordsSettings _wordsSettings;

        private WordsSettings()
        {

        }

        public static WordsSettings GetWordsAsapSettings()
        {
            if(_wordsSettings == null)               
                _wordsSettings = new WordsSettings();
            return _wordsSettings;
        }

        public string CollectionStorage
        {
            get
            {
                return Properties.Settings.Default.WordsCollectionStorage;
            }
            set
            {
                Properties.Settings.Default.WordsCollectionStorage = value;
                Properties.Settings.Default.Save();
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
                Properties.Settings.Default.Save();
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
                Properties.Settings.Default.Save();
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
                Properties.Settings.Default.Save();
            }
        }

    }
}
