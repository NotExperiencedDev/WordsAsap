using FluentNHibernate.Mapping;
using WordsAsap.Entities;

namespace WordsAsap.Mappings
{
    public class WordStatisticsMap:ClassMap<WordStatistics>
    {
        public WordStatisticsMap()
        {
             Id(x => x.Id);
             Map(x => x.CorrectAnswers);
             Map(x => x.WrongAnswers);
             Map(x => x.NumberOfDisplays);
             Map(x => x.LastDisplayTime);
             References(x => x.Word);
        }
    }
}
