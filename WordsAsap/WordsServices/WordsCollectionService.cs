using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WordsAsap.Entities;

namespace WordsAsap.WordsServices
{
    public class WordsCollectionService: IWordsCollectionService, IDisposable
    {
        private string _databaseFileName;
        private static WordsCollectionService _wordsCollectionService;
        private ISession _session;

        private WordsCollectionService()
        {

        }

        private WordsCollectionService(string dbFileName)
        {
            _databaseFileName = dbFileName;
            var sessionFactory = CreateSessionFactory();
            _session = sessionFactory.OpenSession();
        }

       
        private ISessionFactory CreateSessionFactory()
        {
            return Fluently.Configure()
              .Database(
                SQLiteConfiguration.Standard
                  .UsingFile(_databaseFileName)
              )
              .Mappings(m =>
                m.FluentMappings.AddFromAssemblyOf<WordsAsap.App>())
              .ExposeConfiguration(BuildSchema)
              .BuildSessionFactory();
        }

        private void BuildSchema(Configuration config)
        {
            // delete the existing db on each run
            if (File.Exists(_databaseFileName))
                return;

            // this NHibernate tool takes a configuration (with mapping info in)
            // and exports a database schema from it
            new SchemaExport(config)
              .Create(false, true);
        }

        public static IWordsCollectionService CreateWordsCollectionService(IWordsCollectionSettingsService settings)
        {
            if (_wordsCollectionService != null && string.Equals(_wordsCollectionService._databaseFileName, settings.CollectionStorage))
                return _wordsCollectionService;
            _wordsCollectionService = new WordsCollectionService(settings.CollectionStorage);
            return _wordsCollectionService;
        }

        public IList<T> GetData<T>() where T:class
        {
          
               // using (var transaction = session.BeginTransaction())
                    return _session.CreateCriteria<T>().List<T>();
            
        }

        public void AddWord(string word, string translation)
        {
            if (string.IsNullOrWhiteSpace(word))
                throw new ArgumentNullException("word cannot be null");

            if (string.IsNullOrWhiteSpace(translation))
                throw new ArgumentNullException("translation cannot be null");

           
                using (var transaction = _session.BeginTransaction())
                {
                   
                    var words = GetData<Word>().Where(x => x.Value.ToLower() == word.ToLower());
                    if (words != null && words.Count() > 0)
                    {
                        var t = new Translation();
                        t.Value = translation;
                        foreach (var word1 in words)
                        {
                            word1.Translations.Add(t);
                            _session.SaveOrUpdate(word1);
                        }
                        _session.SaveOrUpdate(t);
                    }
                    else
                    {
                        var w = new Word();
                        w.CreationDate = DateTime.UtcNow;
                        w.Value = word.ToLower();
                        var t = new Translation();
                        t.Value = translation;
                        var s = new WordStatistics();
                        _session.SaveOrUpdate(s);
                        w.Statistics = s;                       
                      
                        w.Translations.Add(t);
                        _session.SaveOrUpdate(w);
                    }
                   
                    // save both stores, this saves everything else via cascading
                   

                    transaction.Commit();
                }
            
        }

        public void Update<T1>(T1 entity) where T1 : class
        {
            if (entity == null)
                throw new ArgumentException("Entity to update cannot be null");
            using (var transaction = _session.BeginTransaction())
            {
                _session.SaveOrUpdate(entity);
                transaction.Commit();
            }
        }

        public void Dispose()
        {
            if (_session != null)
                _session.Close();
        }
    }
}
