using FirstFloor.ModernUI.Presentation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordsAsap.Pages.ViewModels
{
    public class AboutViewModel : NotifyPropertyChanged
    {
        public string Author
        {
            get { return ApplicationInfo.CompanyFunc(); }
        }

        public string Version
        {
            get
            {
                return ApplicationInfo.VersionFunc();
            }
        }

        public string FileVersion
        {
            get
            {
                return ApplicationInfo.FileVersionFunc();
            }
        }
    }
}
