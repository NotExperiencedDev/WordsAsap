using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordsAsap.WordsServices
{
    public interface IWordsCollectionSettingsService
    {
        string CollectionStorage { get; set; }
        int WordDialogShowInterval { get; set; }
        
    }

    public static class SettingsServiceFactory
    {
        public static IWordsCollectionSettingsService GetWordsAsapSettings()
        {
            return WordsSettings.GetWordsAsapSettings();
        }
    }
}
