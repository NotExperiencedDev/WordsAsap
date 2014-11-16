using FirstFloor.ModernUI.Presentation;
using FirstFloor.ModernUI.Windows.Controls;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using WordsAsap.Entities;

namespace WordsAsap.Pages
{
    public class HomeViewModel : NotifyPropertyChanged    
    {
        public string NewWord { get; set; }
        public string Translation { get; set; }
        public IList<Word> WordsCollection { get; set; }
        public IList<Translation> WordsTranslations { get; set; }
       
        public HomeViewModel()
        {
             var sessionFactory = DatabaseHelper.CreateSessionFactory();

             using (var session = sessionFactory.OpenSession())
             {
                 using (var transaction = session.BeginTransaction())
                     WordsCollection = session.CreateCriteria<Word>().List<Word>();
             }
        }

        public ICommand SaveSettingsCommand
        {
            get { return new RelayCommand(SaveSettings); }
        }

        private void SaveSettings(object o)
        {
            if (string.IsNullOrWhiteSpace(NewWord))
                return;

            var sessionFactory = DatabaseHelper.CreateSessionFactory();
            using (var session = sessionFactory.OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    var w = new Word();
                    w.Value = NewWord.ToLower();
                    var t = new Translation();
                    w.Translations.Add(t);

                    // save both stores, this saves everything else via cascading
                    session.SaveOrUpdate(t);
                    session.SaveOrUpdate(w);

                    transaction.Commit();
                    WordsCollection = session.CreateCriteria<Word>().List<Word>();
                    OnPropertyChanged("WordsCollection");
                }
            }
           
            ModernDialog.ShowMessage("settings saved", "save settings", MessageBoxButton.OK);
        }
    }
}
