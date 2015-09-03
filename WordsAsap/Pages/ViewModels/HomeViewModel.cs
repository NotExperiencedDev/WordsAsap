using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Navigation;
using FirstFloor.ModernUI.Presentation;
using FirstFloor.ModernUI.Windows.Controls;
using System.Collections.ObjectModel;
using System.Windows;
using FeserWard.Controls;
using WordsAsap.Dialogs;
using WordsAsap.Entities;
using WordsAsap.Pages.ViewModels;
using WordsAsap.WordsServices;
using System;

namespace WordsAsap.Pages
{
    public class HomeViewModel : BaseViewModel  
    {
        private Word m_selectedWord;
        public string NewWord { get; set; }

        public ObservableCollection<TranslationItem> Translations { get; set; }
       
        protected override void InitializeModel()
        {
            LinqToEntitiesProvider = new LinqToEntitiesResultsProvider(WordsService);
            Translations = new ObservableCollection<TranslationItem>();
            AddTranslation(null);
        }

        public IIntelliboxResultsProvider LinqToEntitiesProvider { get; private set; }

        public Word SelectedWord
        {
            get { return m_selectedWord; }
            set
            {
                m_selectedWord = value;
                if (value == null)
                {
                    NewWordCommandExec(null);
                    OnPropertyChanged("NewWord");
                    return;
                }
                NewWord = m_selectedWord.Value;
                if (m_selectedWord.Translations.Count > 0)
                {
                    ClearUnusedTranslationFields();
                    AddTranslationsFromWord();
                }
                OnPropertyChanged("NewWord");
            }
        }

        private void AddTranslationsFromWord()
        {
            foreach (var translation in m_selectedWord.Translations)
            {
                Translations.Add(new TranslationItem {Translation = translation.Value});
            }
        }

        private void ClearUnusedTranslationFields()
        {
            if (Translations.Count <= 0) 
                return;
            var translationsToRemove = Translations.Where(x => string.IsNullOrWhiteSpace(x.Translation)).ToList();
            foreach (var translationItem in translationsToRemove)
                Translations.Remove(translationItem);
        }

        public RelayCommand NewWordCommand
        {
            get { return new RelayCommand(NewWordCommandExec, CanClear); }
        }

        private bool CanClear(object arg)
        {
            return !string.IsNullOrWhiteSpace(NewWord) || (Translations.Count > 0 && Translations.Any(x=>!string.IsNullOrWhiteSpace(x.Translation)));
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
                m_selectedWord = null;
                AddTranslation(null);
                OnPropertyChanged("NewWord");
                OnPropertyChanged("SelectedWord");
            }
              
        }

        public RelayCommand AddTranslationCommand
        {
            get { return new RelayCommand(AddTranslation, o=>!string.IsNullOrWhiteSpace(NewWord)); }
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

            return !Translations.Any(x=>string.IsNullOrWhiteSpace(x.Translation));
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


    public class LinqToEntitiesResultsProvider: IIntelliboxResultsProvider
    {
        private readonly WordsCollectionService m_wordService;

        public LinqToEntitiesResultsProvider(WordsCollectionService wordService)
        {
            m_wordService = wordService;
        }

        public System.Collections.IEnumerable DoSearch(string searchTerm, int maxResults, object extraInfo)
        {
            try
            {
                var sql = NHibernate.Criterion.Expression.Sql("lower({alias}.Value) like lower(?)", string.Format("%{0}%", searchTerm), NHibernate.NHibernateUtil.String);
                var reply = m_wordService.GetData<Word>(sql);
                if (reply == null || reply.Count == 0)
                {
                    //return null;
                    return new List<Word> { new Word { Value = searchTerm } };
                }
                return reply.ToList();
            }
            catch (Exception ex)
            {              
                DefaultMessageService.MessageService.ShowErrorMessage("WordsAsap Error", ex.ToString());
                return null;
            }
        }
        
    }
}
