using appLauncher.Core.Brushes;
using appLauncher.Core.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;
using Windows.UI.Xaml.Media;

namespace appLauncher.Core.Models
{
   public class AppTile : BaseModel
    {
		public string AppName { get; set; }
		public string AppFullName { get; set; }
		public string AppDeveloper { get; set; }
		public DateTimeOffset AppInstalled { get; set; }
		public Color AppTileForgroundcolor { get; set; }
		public Color AppTileBackgroundcolor { get; set; }
		public double AppTileOpacity { get; set; } = 1;
		public byte[] AppLogo { get; set; }
		public int AppFontSize { get; set; } = 12;
		public Brush TextColorBrush => TextForeGroundColorBrush();
        public Brush AppTileBackground => BackgroundColorBrush();
		public Brush AppLogoForeground => ForegroundLabel();
        public async Task<bool> LaunchAsync()
		{
			var packages = await packageHelper.pkgManager.FindPackage(this.AppFullName).GetAppListEntriesAsync();
			return await packages[0].LaunchAsync();
		}



		public MaskedBrush ForegroundLabel()
		{
			return new MaskedBrush(AppLogo,AppTileForgroundcolor,AppTileOpacity);
			
		}
		public SolidColorBrush BackgroundColorBrush()
		{
			SolidColorBrush scb = new SolidColorBrush(AppTileBackgroundcolor);
			scb.Opacity = this.AppTileOpacity;
			return scb;
		}
		public SolidColorBrush TextForeGroundColorBrush()
        {
			SolidColorBrush scb = new SolidColorBrush(AppTileForgroundcolor);
			scb.Opacity = this.AppTileOpacity;
			return scb;
        }
	}
}
