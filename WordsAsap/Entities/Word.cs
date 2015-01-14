using System;
using System.Collections.Generic;

namespace WordsAsap.Entities
{
    public class Word
    {
        public virtual int Id{get;set;}
        public virtual string Value{get;set;}
        public virtual IList<Translation> Translations{get; protected set;}
        public virtual WordStatistics Statistics { get; set; }
        public virtual DateTime CreationDate { get; set; }

        public Word()
        {
            Translations = new List<Translation>();
        }

        public virtual void AddTranslation(Translation translation)
        {
            translation.WordsStoredIn.Add(this);
            Translations.Add(translation);
        }

        
    }
}
