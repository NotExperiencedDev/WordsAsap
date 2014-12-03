using FirstFloor.ModernUI.Windows.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WordsAsap.Pages.ViewModels;

namespace WordsAsap.WordsServices
{
    public class DefaultMessageService : IMessagesService
    {
        private static DefaultMessageService _messageService;
        private DefaultMessageService()
        {

        }

        public static IMessagesService MessageService
        {
            get
            {
                if (_messageService == null)
                    _messageService = new DefaultMessageService();
                return _messageService;
            }
        }

        public void ShowErrorMessage(string title, string message)
        {
            ModernDialog.ShowMessage(title, message, MessageBoxButton.OK);
        }
    }
}
