using System.Collections.Generic;

namespace WordsAsap.Entities
{
    public class Translation
    {
        public virtual int Id{get;set;}
        public virtual string Value{get;set;}
        public virtual int LanguageId { get; set; }
        public virtual IList<Word> WordsStoredIn{get; protected set;}

        public Translation()
        {
            WordsStoredIn = new List<Word>();
        }
    }
}
