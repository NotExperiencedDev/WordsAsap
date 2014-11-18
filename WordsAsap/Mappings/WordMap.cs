using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WordsAsap.Entities;

namespace WordsAsap.Mappings
{
    public class WordMap:ClassMap<Word>
    {
        public WordMap()
        {
            Id(x => x.Id);
            Map(x => x.Value).UniqueKey("unique_key_only_one_word");
            Map(x => x.CreationDate);
            References(x => x.Statistics);
            HasManyToMany(x => x.Translations)
              .Cascade.All()
             .Table("WordTranslations");
        }

    }
}
