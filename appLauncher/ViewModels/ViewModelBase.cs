using GoogleAnalytics;
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
        public void Logging(string typeoflog,List<string> stufftolog,bool isFatal=false)
        {
            switch (typeoflog)
            {
                case "crash":
                    App.AnalyticsTracker.Send(HitBuilder.CreateException(stufftolog[0], isFatal).Build());
                    break;
                case "screen":
                    App.AnalyticsTracker.Send(HitBuilder.CreateScreenView(stufftolog[0]).Build());
                    break;
                case "events": //for all other items that need logged
                 App.AnalyticsTracker.Send(HitBuilder.CreateCustomEvent(stufftolog[0], stufftolog[1], stufftolog[2]).Build());
                 break;
                default:

                    break;
            }


        }


    }
}
