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

            gr1.Focus();
            FocusNext();
        }
        
        private void FocusNext()
        {
            var elementWithFocus = Keyboard.FocusedElement as UIElement;
            if (elementWithFocus is Intellibox)
                return;

            const FocusNavigationDirection focusDirection = FocusNavigationDirection.Next;

            // MoveFocus takes a TraveralReqest as its argument.
            var request = new TraversalRequest(focusDirection);

            // Change keyboard focus. 
            if (elementWithFocus != null)
            {
                elementWithFocus.MoveFocus(request);
            }
        }

        private void UIElement_OnGotFocus(object sender, RoutedEventArgs e)
        {
           
        }
    }
}
