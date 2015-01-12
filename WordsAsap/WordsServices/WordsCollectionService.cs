using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Criterion;
using NHibernate.Tool.hbm2ddl;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WordsAsap.Entities;

namespace WordsAsap.WordsServices
{
    public class WordsCollectionService:  IDisposable
    {
        private string _databaseFile;
        private static WordsCollectionService _wordsCollectionService;
        private ISession _session;

        public event EventHandler WordsCollectionChanged;

        private WordsCollectionService()
        {

        }

        private WordsCollectionService(string dbFolder, string dbFileName)
        {
            _databaseFile = Path.Combine(dbFolder, dbFileName);
            var sessionFactory = CreateSessionFactory();
            _session = sessionFactory.OpenSession();
        }

       
        private ISessionFactory CreateSessionFactory()
        {
            return Fluently.Configure()
              .Database(
                SQLiteConfiguration.Standard
                  .UsingFile(_databaseFile)
              )
              .Mappings(m =>
                m.FluentMappings.AddFromAssemblyOf<WordsAsap.App>())
              .ExposeConfiguration(BuildSchema)
              .BuildSessionFactory();
        }

        private void BuildSchema(Configuration config)
        {
           
            if (File.Exists(_databaseFile))
                return;
            var directory = Path.GetDirectoryName(_databaseFile);
            if (!Directory.Exists(directory))
                Directory.CreateDirectory(directory);

            // this NHibernate tool takes a configuration (with mapping info in)
            // and exports a database schema from it
            try
            {
                new SchemaExport(config)
                  .Create(false, true);
            }
            catch (Exception e)
            {                
                File.AppendAllText("Logs//CreateDBlog.txt", e.ToString());
            }
        }
        
        public static WordsCollectionService CreateWordsCollectionService(WordsSettings settings)
        {
            if (_wordsCollectionService != null && string.Equals( _wordsCollectionService._databaseFile, Path.Combine(settings.CollectionStorageFolder, settings.CollectionStorageFile)))
                return _wordsCollectionService;
            if (_wordsCollectionService != null)
            {
                 _wordsCollectionService.Dispose();
                 _wordsCollectionService = null;
             }

            _wordsCollectionService = new WordsCollectionService(settings.CollectionStorageFolder, settings.CollectionStorageFile);
            return _wordsCollectionService;
        }

        public IList<T> GetData<T>(ICriterion expression = null) where T:class
        {
            var criteria = _session.CreateCriteria<T>();
               if(expression != null)
             criteria = criteria.Add(expression);

            return criteria.List<T>();            
        }

        public Task<IList<T>> GetDataAsync<T>(ICriterion expression = null) where T : class
        {
            var criteria = _session.CreateCriteria<T>();
            if (expression != null)
                criteria = criteria.Add(expression);
            return Task.FromResult<IList<T>>( criteria.List<T>());        
        }

        public void AddWord(string word, string translation)
        {
            if (string.IsNullOrWhiteSpace(word))
                throw new ArgumentNullException("word cannot be null");

            if (string.IsNullOrWhiteSpace(translation))
                throw new ArgumentNullException("translation cannot be null");

           
                using (var transaction = _session.BeginTransaction())
                {
                    var sql = NHibernate.Criterion.Expression.Sql("lower({alias}.Value) = lower(?)", word, NHibernate.NHibernateUtil.String);
                    var words = GetData<Word>(sql); 
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
                        _session.Flush();
                        _session.Refresh(t);
                    }
                    else
                    {
                        var w = new Word();
                        w.CreationDate = DateTime.UtcNow;
                        w.Value = word;
                        var t = new Translation();
                        t.Value = translation;
                        var s = new WordStatistics();
                        _session.SaveOrUpdate(s);
                        w.Statistics = s;                       
                      
                        w.Translations.Add(t);
                        _session.SaveOrUpdate(w);
                        _session.Flush();
                        _session.Refresh(t);
                    }
                   
                    transaction.Commit();
                    
                    OnWordsCollectionChanged();
                }
            
        }

        public void Update<T>(T entity) where T : class
        {
            if (entity == null)
                throw new ArgumentException("Entity to update cannot be null");
            using (var transaction = _session.BeginTransaction())
            {
                _session.SaveOrUpdate(entity);
               
                transaction.Commit();
                
                _session.Flush();
                _session.Refresh(entity);
            }
        }

        public void Remove<T>(T entity) where T : class
        {
            if(entity == null)
                throw new ArgumentException("Entity to remove cannot be null");

            using (var transaction = _session.BeginTransaction())
            {
               _session.Delete(entity);
                
                transaction.Commit();
         
                _session.Flush();
            }
        }

        public void Dispose()
        {
            if (_session != null)
                _session.Close();
        }


        private void OnWordsCollectionChanged(){
            if (WordsCollectionChanged != null)
                WordsCollectionChanged(this, new EventArgs());
        }
    }
}
