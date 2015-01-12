using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

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
