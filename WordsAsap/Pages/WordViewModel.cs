using FirstFloor.ModernUI.Presentation;
using FirstFloor.ModernUI.Windows.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using WordsAsap.Entities;
using WordsAsap.WordsServices;

namespace WordsAsap.Pages
{
    public class WordViewModel : NotifyPropertyChanged
    {      

        public virtual string FirstTranslation
        {
            get
            {
                if (WordToDisplay == null)
                    return string.Empty;
                if (WordToDisplay.Translations != null)
                {
                    var first = WordToDisplay.Translations.FirstOrDefault();
                    if (first != null)
                        return first.Value;
                }
                return string.Empty;
            }
        }

        public Word WordToDisplay { get; set; }

        public bool ShowOtherTranslations { get; set; }

        public ICommand ShowTranslationsCommand
        {
            get { return new RelayCommand((o) => { ShowOtherTranslations = !ShowOtherTranslations; OnPropertyChanged("ShowOtherTranslations"); }); }
        }

       
    }
}
