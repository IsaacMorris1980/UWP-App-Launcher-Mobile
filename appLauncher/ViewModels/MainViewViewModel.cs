using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using appLauncher.Pages;

namespace appLauncher.ViewModels
{
   public class MainViewViewModel : ViewModelBase
    {
        private Page _displayPage;

        public MainViewViewModel()
        {
        }

        public Page DisplayedPage
        {
            get { return (_displayPage == null) ? new settings() : _displayPage; }
            set { _displayPage = (value == null) ? new settings() : value; }
        }

    }
}
