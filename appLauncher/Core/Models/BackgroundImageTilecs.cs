using appLauncher.Core.Brushes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;

namespace appLauncher.Core.Models
{
  public class BackgroundImageTilecs :BaseModel
    {
        public string Filename { get; set; }
        public byte[] Backgroundimage { get; set; }
        public double BackgroundImageOpacity { get; set; } = 1;
        public MaskedBrush BackgroundBrush()
        {
            MaskedBrush mb = new MaskedBrush(Backgroundimage);
            mb.overlaycolor = Colors.Transparent;
            mb.Opacity = BackgroundImageOpacity;
            return mb;
        }
    }
}
