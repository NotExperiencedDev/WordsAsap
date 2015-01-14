using System;

namespace WordsAsap.Entities
{
    public class WordStatistics
    {
        public virtual int Id { get; set; }
        public virtual int CorrectAnswers { get; set; }
        public virtual int WrongAnswers { get; set; }
        public virtual int NumberOfDisplays { get; set; }
        public virtual DateTime LastDisplayTime { get; set; }
        public virtual Word Word { get; set; }
    }
}
