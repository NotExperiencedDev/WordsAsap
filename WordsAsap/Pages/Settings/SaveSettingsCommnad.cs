using FirstFloor.ModernUI.Presentation;
using FirstFloor.ModernUI.Windows.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WordsAsap.Pages.Settings
{
    public class SaveSettingsCommnand
      : CommandBase
    {    
        /// <summary>
        /// Executes the command.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        protected override void OnExecute(object parameter)
        {
            Properties.Settings.Default.AccentColor = AppearanceManager.Current.AccentColor.ToString();
            Properties.Settings.Default.ThemeSource = AppearanceManager.Current.ThemeSource;
            Properties.Settings.Default.SelectedFontSize = AppearanceManager.Current.FontSize;
            Properties.Settings.Default.Save();
            ModernDialog.ShowMessage("settings saved", "save settings", MessageBoxButton.OK);
        }
    }
}
