using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
