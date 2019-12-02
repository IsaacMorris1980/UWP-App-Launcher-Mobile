using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace appLauncher.ViewModels
{
   public class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public void OnproperyChanged(string propertyname)
        {
            PropertyChanged(this, new PropertyChangedEventArgs(propertyname));
        }
        public async Task Logging(string stufftolog,string typeoflog="")
        {

        }


    }
}
