using FirstFloor.ModernUI.Presentation;
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

namespace WordsAsap.Pages
{
    /// <summary>
    /// Interaction logic for WordPage.xaml
    /// </summary>
    public partial class WordPage : UserControl
    {
        public WordPage()
        {
            InitializeComponent();
            if (!WordsSettings.WordsAsapSettings.ShowWordInPopupDialog)
            {
                var colour = AppearanceManager.Current.AccentColor;
                if (AppearanceManager.DarkThemeSource == AppearanceManager.Current.ThemeSource)
                    colour = Colors.Black;
                
                LinearGradientBrush myLinearGradientBrush = new LinearGradientBrush();
                myLinearGradientBrush.StartPoint = new Point(0, 0);
                myLinearGradientBrush.EndPoint = new Point(0, 1);
                myLinearGradientBrush.GradientStops.Add(
                    new GradientStop(Colors.WhiteSmoke, 0.0));
                myLinearGradientBrush.GradientStops.Add(
                    new GradientStop(colour, 0.75));
                myLinearGradientBrush.GradientStops.Add(
                    new GradientStop(Colors.WhiteSmoke, 1.0));

                // Use the brush to paint the rectangle.
                Background = myLinearGradientBrush;
                Background.Opacity = WordsSettings.WordsAsapSettings.BalloonTipTransparency;

            }
        }
    }
}
