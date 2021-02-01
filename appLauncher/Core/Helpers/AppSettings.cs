using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;

namespace appLauncher.Core.Helpers
{
  public class AppSettings :BaseModel
    {
        public Color AppFontColor { get; set; } = Colors.Black;
        public Color AppBackGroundColor { get; set; } = Colors.Blue;
        public int AppFontSize { get; set; } = 12;
        public bool AppAllowLogging { get; set; } = false;

    }
}
