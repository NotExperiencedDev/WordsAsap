﻿using FirstFloor.ModernUI.Presentation;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

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

                    if (WordsSettings.WordsAsapSettings.BalloonTipGradientOff)
                    {
                        myLinearGradientBrush.GradientStops.Add(
                           new GradientStop(colour, 0.0));
                    }
                    else
                    {
                        myLinearGradientBrush.GradientStops.Add(
                            new GradientStop(Colors.WhiteSmoke, 0.0));
                        myLinearGradientBrush.GradientStops.Add(
                            new GradientStop(colour, 0.75));
                        myLinearGradientBrush.GradientStops.Add(
                            new GradientStop(Colors.WhiteSmoke, 1.0));
                    }
                    // Use the brush to paint the rectangle.
                    Background = myLinearGradientBrush;
                
                Background.Opacity = WordsSettings.WordsAsapSettings.BalloonTipTransparency;

            }
        }
    }
}
