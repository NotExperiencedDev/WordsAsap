namespace WordsAsap.Dialogs
{
    /// <summary>
    /// Interaction logic for SaveWordConfirmationDialog.xaml
    /// </summary>
    public partial class SaveWordConfirmationDialog 
    {
        public SaveWordConfirmationDialog(string title, string displayText="")
        {
            Title = title;
            DisplayText = displayText;
            InitializeComponent();
            DataContext = this;
        }

        public string DisplayText { get; set; }

        public bool ShowDialogNextTime
        {
            get { return WordsSettings.WordsAsapSettings.AddWordConfirmation; }
            set
            {
                if (WordsSettings.WordsAsapSettings.AddWordConfirmation != value)
                {
                    WordsSettings.WordsAsapSettings.AddWordConfirmation = value;
                }
            }
        }

       
    }
}
