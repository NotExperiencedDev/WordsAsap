using FirstFloor.ModernUI.Presentation;
using FirstFloor.ModernUI.Windows.Controls;
using System.Collections.ObjectModel;
using System.Windows;
using WordsAsap.Dialogs;
using WordsAsap.Entities;
using WordsAsap.Pages.ViewModels;

namespace WordsAsap.Pages
{
    public class HomeViewModel : BaseViewModel    
    {        
        public string NewWord { get; set; }
        public ObservableCollection<TranslationItem> Translations { get; set; }
       
        protected override void InitializeModel()
        {
            Translations = new ObservableCollection<TranslationItem>();
            AddTranslation(null);
        }

        public RelayCommand NewWordCommand
        {
            get { return new RelayCommand(NewWordCommandExec); }
        }

        private void NewWordCommandExec(object obj)
        {
            if (!string.IsNullOrWhiteSpace(NewWord))
            {
                var sql = NHibernate.Criterion.Expression.Sql("lower({alias}.Value) = lower(?)", NewWord, NHibernate.NHibernateUtil.String);
                var reply = WordsService.GetData<Word>(sql);  
                if((reply == null || reply.Count == 0) && HasTranslation())
                {
                    var r = ModernDialog.ShowMessage("word not saved do you want to save it?", "save or update word", MessageBoxButton.YesNo);
                    if (r == MessageBoxResult.Yes)
                        SaveWord(null);
                }
                Translations.Clear();                
                NewWord = string.Empty;
                AddTranslation(null);
                OnPropertyChanged("NewWord");
            }
              
        }

        public RelayCommand AddTranslationCommand
        {
            get { return new RelayCommand(AddTranslation); }
        }

        private void AddTranslation(object obj)
        {
            Translations.Add(new TranslationItem());
        }

        public RelayCommand RemoveTranslationCommand
        {
            get { return new RelayCommand(RemoveTranslation); }
        }

        private void RemoveTranslation(object o)
        {
            var t = o as TranslationItem;
            if (t != null)
                Translations.Remove(t);
        }

        public RelayCommand SaveWordCommand
        {
            get { return new RelayCommand(SaveWord, (o) => !string.IsNullOrWhiteSpace(NewWord) && HasTranslation()); }
        }

        private bool HasTranslation()
        {
            if (Translations == null || Translations.Count < 1)
                return false;
           
            foreach (var item in Translations)
                if (!string.IsNullOrWhiteSpace(item.Translation))
                    return true;
            return false;
        }

        private void SaveWord(object o)
        {
            foreach (var t in Translations)
            {
                if (string.IsNullOrWhiteSpace(t.Translation))
                    continue;
                WordsService.AddWord(NewWord, t.Translation);
            }
            if (WordsSettings.WordsAsapSettings.AddWordConfirmation)
            {
                //ModernDialog.ShowMessage("word updated", "save or update word", MessageBoxButton.OK);
                var confirmDialog = new SaveWordConfirmationDialog("save or update word");
                confirmDialog.ShowDialog();
            }
            NewWordCommandExec(null);
        }
        
    }

    public class TranslationItem
    {
        public string Translation { get; set; }
    }
}
