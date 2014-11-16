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

namespace WordsAsap
{
    public static class DatabaseHelper
    {
        private const string DatabaseFileName = "WordsAsapWordsCollection.db3";
        public static ISessionFactory CreateSessionFactory()
        {
            return Fluently.Configure()
              .Database(
                SQLiteConfiguration.Standard
                  .UsingFile(DatabaseFileName)
              )
              .Mappings(m =>
                m.FluentMappings.AddFromAssemblyOf<WordsAsap.App>())
              .ExposeConfiguration(BuildSchema)
              .BuildSessionFactory();
        }

        public static void BuildSchema(Configuration config)
        {
            // delete the existing db on each run
            if (File.Exists(DatabaseFileName))
                return;

            // this NHibernate tool takes a configuration (with mapping info in)
            // and exports a database schema from it
            new SchemaExport(config)
              .Create(false, true);
        }
    }
}
