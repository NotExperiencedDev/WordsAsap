using FirstFloor.ModernUI.Presentation;
using FirstFloor.ModernUI.Windows.Controls;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace WordsAsap.Pages.Settings
{
    /// <summary>
    /// A simple view model for configuring theme, font and accent colors.
    /// </summary>
    public class BehaviourViewModel
        : NotifyPropertyChanged
    {
        public BehaviourViewModel()
        {
            WordDialogShowInterval = Properties.Settings.Default.WordDialogShowInterval;
        }

        private int _wordDialogShowInterval;

        public int WordDialogShowInterval
        {
            get { return _wordDialogShowInterval; }
            set
            {
                _wordDialogShowInterval = value;
                OnPropertyChanged("WordDialogShowInterval");
            }
        }
      
        public ICommand UpCommand
        {
            get { return new RelayCommand((o) => { WordDialogShowInterval++; }); }
        }

        public ICommand DownCommand
        {
            get { return new RelayCommand((o) => { WordDialogShowInterval--; }, (o1) => { return WordDialogShowInterval > 1; }); }
        }
             

        public ICommand SaveSettingsCommand
        {
            get { return new RelayCommand(SaveSettings); }
        }

        private void SaveSettings(object o)
        {
            Properties.Settings.Default.WordDialogShowInterval = WordDialogShowInterval;
            Properties.Settings.Default.Save();
            ModernDialog.ShowMessage("settings saved", "save settings", MessageBoxButton.OK);
        }
    }
      

   
}
