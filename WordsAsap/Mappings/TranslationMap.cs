using FluentNHibernate.Mapping;
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
              .Table("WordTranslations");
        }
       
    }
}
