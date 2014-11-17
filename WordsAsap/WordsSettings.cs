using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WordsAsap.WordsServices;

namespace WordsAsap
{
    public class WordsSettings:IWordsCollectionSettingsService
    {
        private static IWordsCollectionSettingsService _wordsCollectionServiceSettings;

        private WordsSettings()
        {

        }

        public static IWordsCollectionSettingsService GetWordsAsapSettings()
        {
            if(_wordsCollectionServiceSettings == null)               
                _wordsCollectionServiceSettings = new WordsSettings();
            return _wordsCollectionServiceSettings;
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
    }
}
