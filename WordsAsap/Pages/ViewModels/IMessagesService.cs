using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WordsAsap.Pages.ViewModels
{
    public interface IMessagesService
    {
        void ShowErrorMessage(string title, string message);
    }
}
