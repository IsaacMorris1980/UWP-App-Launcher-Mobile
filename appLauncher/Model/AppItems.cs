using appLauncher.Brushes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage.Streams;

namespace appLauncher.Model
{
    public class AppItems
    {
        public string developer;
        public string name;
        public bool isfavorite;
        public DateTimeOffset installed;
        public string fullName;
        public IRandomAccessStream appLogo;
       public MaskedBrush maskedbrush
        {
            get { MaskedBrush mb = new MaskedBrush(appLogo);
                return mb;
            }
        }
    }
    }
