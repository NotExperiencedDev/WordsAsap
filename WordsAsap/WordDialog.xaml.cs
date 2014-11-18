using WordsAsap.Entities;
namespace WordsAsap
{
    
    public partial class WordDialog
    {
     
        public WordDialog()
        {
            InitializeComponent();
        }

        public WordDialog(Word wordToShow):this()
        {
            DataContext = wordToShow;
        }

      
    }
}
