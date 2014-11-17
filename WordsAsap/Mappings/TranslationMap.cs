using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WordsAsap.Entities;

namespace WordsAsap.Mappings
{
    public class TranslationMap:ClassMap<Translation>
    {

        public TranslationMap()
        {
            Id(x => x.Id);
            Map(x => x.Value);
            Map(x => x.LanguageId);
            HasManyToMany(x => x.WordsStoredIn)
              .Cascade.All()
              .Inverse()
              .Table("WordTranslations").AsSet();
        }
       
    }
}
