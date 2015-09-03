using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using FeserWard.Controls;

namespace WordsAsap.Pages
{
    /// <summary>
    /// Interaction logic for Home.xaml
    /// </summary>
    public partial class Home : UserControl
    {
        public Home()
        {
            InitializeComponent();
        }
        
        private void Home_OnLoaded(object sender, RoutedEventArgs e)
        {
            var elementWithFocus = Keyboard.FocusedElement as UIElement;
            if (elementWithFocus is Intellibox)
                return;
            
            wordInput.Focus();
        }

    }
}
