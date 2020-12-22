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
        public static Color AppFontColor { get; set; } = Colors.Black;
        public static Color AppBackGroundColor { get; set; } = Colors.Blue;
        public static int AppFontSize { get; set; } = 12;
        public static bool AppAllowLogging { get; set; } = false;

    }
}
