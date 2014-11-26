using NHibernate.Criterion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordsAsap.WordsServices
{
    public interface IWordsCollectionService
    {
        IList<T> GetData<T>(ICriterion expression = null) where T: class;
        void AddWord(string word, string translation);

        void Update<T>(T entity) where T:class;

        void Remove<T>(T entity) where T : class;

        event EventHandler WordsCollectionChanged;
    }

    public static class WordsCollectionServiceFactory
    {
        public static IWordsCollectionService CreateWordsCollectionService(IWordsCollectionSettingsService settings)
        {
            return WordsCollectionService.CreateWordsCollectionService(settings);
        }
    }
}
