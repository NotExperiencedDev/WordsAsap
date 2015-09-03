using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using WordsAsap.Entities;

namespace WordsAsap.WordsServices
{
    public class GetWordToLearn
    {
        private const int MinimumListOfWords = 2;
        private Random _random;
        private WordsCollectionService _wordsCollectionService;
        private int _maxNumberOfWordDisplays;
        private Queue<Word> _lastWords;

        private static GetWordToLearn _getWordToLearn;

        public static GetWordToLearn WordToLearn
        {
            get
            {
                if (_getWordToLearn == null)
                    _getWordToLearn = new GetWordToLearn();
                return _getWordToLearn;
            }
        }

        private GetWordToLearn()
        {
            _random = new Random();
            _lastWords = new Queue<Word>();
            try
            {
                _wordsCollectionService = WordsCollectionService.CreateWordsCollectionService(WordsSettings.WordsAsapSettings);
            }
            catch (Exception e)
            {
                DefaultMessageService.MessageService.ShowErrorMessage("WordsAsap Error", e.ToString());
                //TODO: add log
            }
            _maxNumberOfWordDisplays = WordsSettings.WordsAsapSettings.MaxNumberOfWordDisplays;
        }

        public Word GetNextWord()
        {
            var words = _wordsCollectionService.GetData<Word>();
            if (words == null || words.Count < 1)
                return null;

            var oldWords = from w in words
                           where w.Statistics.CorrectAnswers - w.Statistics.WrongAnswers > _maxNumberOfWordDisplays
                           orderby w.Statistics.LastDisplayTime ascending
                           select w;
            var displayOldWord = _random.Next(1, 10000) < 50;
            if (displayOldWord && oldWords.Count() > 0)          
                    return oldWords.FirstOrDefault();
            

            var wordsToConsider = from w in words
                                  let wa = w.Statistics.WrongAnswers
                                  let z = w.Statistics.CorrectAnswers - wa
                                  where z <= _maxNumberOfWordDisplays
                                  orderby (DateTime.UtcNow - w.Statistics.LastDisplayTime).TotalSeconds * (wa > 0 ? 1.1+wa/10.0 : 1) descending,                                          
                                          w.Statistics.WrongAnswers descending,
                                          w.Statistics.NumberOfDisplays ascending,
                                          w.Statistics.CorrectAnswers ascending,
                                          w.CreationDate ascending
                                  select w;

            if (wordsToConsider.Count() == 0)
                return oldWords.FirstOrDefault();
           
            if (wordsToConsider.Count() < MinimumListOfWords)
                return wordsToConsider.FirstOrDefault();

            var randResult = _random.Next(0, MinimumListOfWords-1);
            return wordsToConsider.Take(MinimumListOfWords).ElementAt(randResult);
        }
    }
}
