using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordsAsapUnitTEst
{

    public class GeneralPurposeTest
    {
        IList<string>
        private bool HasTranslation()
        {
            if (Translations == null || Translations.Count < 1)
                return false;

            return Translations.Any(x => !string.IsNullOrWhiteSpace(x.Translation));
        }
    }
}
