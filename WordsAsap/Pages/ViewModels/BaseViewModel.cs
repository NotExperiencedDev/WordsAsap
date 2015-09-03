using FirstFloor.ModernUI.Presentation;
using System;
using System.Windows;
using WordsAsap.WordsServices;

namespace WordsAsap.Pages.ViewModels
{
    public abstract class BaseViewModel : NotifyPropertyChanged
    {
        private WordsCollectionService _wordsCollectionService;
        
       
        public BaseViewModel()
        {
            ViewModelInit();
        }

        protected void ViewModelInit()
        {
            try
            {
                if (IsInDesignMode)
                    return;
                _wordsCollectionService = WordsCollectionService.CreateWordsCollectionService(WordsSettings.WordsAsapSettings);
                InitializeModel();
            }
            catch (Exception e)
            {
                if (MessagesService != null)
                    MessagesService.ShowErrorMessage("WordsAsap Error", e.ToString());
                DefaultMessageService.MessageService.ShowErrorMessage("WordsAsap Error", e.ToString());
                //TODO: log
            }
        }

        public static bool IsInDesignMode
        {
            get
            {
                var dependencyObject = new DependencyObject();
                return System.ComponentModel.DesignerProperties.GetIsInDesignMode(dependencyObject);
            }
        }

        protected abstract void InitializeModel();

        protected WordsCollectionService WordsService
        {
            get { return _wordsCollectionService; }
            private set { }
        }

        public IMessagesService MessagesService { get; set; }
    }
}
