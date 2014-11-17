using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordsAsap.WordsServices
{
    public interface IWordsCollectionService
    {
        IList<T> GetData<T>() where T: class;
        void AddWord(string word, string translation);
    }

    public static class WordsCollectionServiceFactory
    {
        public static IWordsCollectionService CreateWordsCollectionService(IWordsCollectionSettingsService settings)
        {
            return WordsCollectionService.CreateWordsCollectionService(settings);
        }
    }
}
