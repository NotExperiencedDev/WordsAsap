﻿using FirstFloor.ModernUI.Presentation;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using WordsAsap.WordsServices;

namespace WordsAsap
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public void OnStartup(object sender, StartupEventArgs e)
        {
            try
            {
                var mainWindow = new MainWindow();

                mainWindow.Title = ApplicationInfo.TitleFunc();
                mainWindow.IsTitleVisible = true;
                LoadSettings();
                mainWindow.Show();
            }
            catch (Exception ex)
            {
                DefaultMessageService.MessageService.ShowErrorMessage("WordsAsap Error", ex.ToString());
                //TODO: add log
            }
        }       

        private void LoadSettings()
        {
            AppearanceManager.Current.ThemeSource = WordsAsap.Properties.Settings.Default.ThemeSource ?? AppearanceManager.Current.ThemeSource;
            var colorFromSettings = GetColor(WordsAsap.Properties.Settings.Default.AccentColor);
            AppearanceManager.Current.AccentColor = colorFromSettings.HasValue ? colorFromSettings.Value : AppearanceManager.Current.AccentColor;
            AppearanceManager.Current.FontSize = WordsAsap.Properties.Settings.Default.SelectedFontSize;
        }

        private Color? GetColor(string color)
        {
            if (string.IsNullOrWhiteSpace(color))
                return null;
            var colorObject = ColorConverter.ConvertFromString(color);
            return colorObject as Color?;
        }
    }

    
}
