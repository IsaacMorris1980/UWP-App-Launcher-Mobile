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
		public Color AppTileForgroundcolor { get; set; } = Colors.Black;
		public Color AppTileBackgroundcolor { get; set; } = Colors.Blue;
		public Color AppTileTextColor { get; set; } = Colors.Red;
		public double AppTileOpacity { get; set; } = .25;
		public byte[] AppLogo { get; set; }
		public int AppFontSize { get; set; } = 12;
		public Brush TextColorBrush {get {  
			SolidColorBrush scb = new SolidColorBrush(AppTileTextColor);
		scb.Opacity = this.AppTileOpacity;
			return scb;
        } }
	public Brush AppTileBackground { get { SolidColorBrush scb = new SolidColorBrush(AppTileBackgroundcolor);
				scb.Opacity = 1;
				return scb; } }
		public Brush AppLogoForeground => new MaskedBrush(AppLogo, AppTileForgroundcolor, 1);
		public async Task<bool> LaunchAsync()
		{
			var packages = await packageHelper.pkgManager.FindPackage(this.AppFullName).GetAppListEntriesAsync();
			return await packages[0].LaunchAsync();
		}
    
	}
}
