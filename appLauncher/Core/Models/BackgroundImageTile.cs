using appLauncher.Core.Brushes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;
using Windows.UI.Xaml.Media;

namespace appLauncher.Core.Models
{
  public class BackgroundImageTile :BaseModel
    {
        public string DisplayName { get; set; }
        public byte[] Backgroundimage { get; set; }
        public double BackgroundImageOpacity { get; set; } = 1;
        public Color BackgroundImageColor { get; set; } = Colors.Transparent;
        public Brush BackgroundImageBrush => new MaskedBrush(Backgroundimage, BackgroundImageColor, BackgroundImageOpacity);
    }
}
