﻿using System.Windows.Input;
using FirstFloor.ModernUI.Presentation;
using FirstFloor.ModernUI.Windows.Controls;
using System.Collections.ObjectModel;
using System.Windows;
using WordsAsap.Entities;
using WordsAsap.WordsServices;

namespace WordsAsap.Pages
{
    public class DictionaryViewModel: NotifyPropertyChanged
    {
         private IWordsCollectionService _wordsCollectionService;
        private WordViewModel _selectedWord;       
       
        public DictionaryViewModel()
        {
            _wordsCollectionService = WordsCollectionServiceFactory.CreateWordsCollectionService(SettingsServiceFactory.GetWordsAsapSettings());
            WordsCollection = new ObservableCollection<WordViewModel>();
             var collection = _wordsCollectionService.GetData<Word>();
            if(collection != null)
                foreach (var item in collection)
                    WordsCollection.Add(new WordViewModel { WordToDisplay = item });
        }

        public ObservableCollection<WordViewModel> WordsCollection { get; set; }
        

        public WordViewModel SelectedWord
        {
            set
            {
                if (_selectedWord == value)
                    return;
                _selectedWord = value;
                OnPropertyChanged("SelectedWord");
            }
            get
            {
                return _selectedWord;
            }
        }

        public RelayCommand RemoveWordCommand
        {
            get { return new RelayCommand(RemoveWord, CanRemoveWord); }
        }


       

        private bool CanRemoveWord(object o)
        {
            return  _wordsCollectionService != null;
        }

        private void RemoveWord(object o)
        {
            var word = o as WordViewModel;
            if (word == null)
                return;

            var r = ModernDialog.ShowMessage("do you really want to remove word?", "word removing", MessageBoxButton.YesNo);
            if (r == MessageBoxResult.No)
                return;

            _wordsCollectionService.Remove<Word>(word.WordToDisplay);

            WordsCollection.Remove(word);
            
        }


    }
}
