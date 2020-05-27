using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media;

namespace appLauncher.Core.Models
{
   public class AppTile
    {
		public string AppName { get; set; }
		public string AppFullName { get; set; }
		public string AppDeveloper { get; set; }
		public DateTimeOffset AppInstalled { get; set; }
		public Color AppTileForgroundcolor { get; set; }
		public Color AppTileBackgroundcolor { get; set; }
		public double AppTileOpacity { get; set; } = 1;
		public byte[] appLogo { get; set; }
		public async Task<bool> LaunchAsync()
		{
			var packages = await packageHelper.pkgManager.FindPackage(this.AppFullName).GetAppListEntriesAsync();
			return await packages[0].LaunchAsync();
		}



		public MaskedBrush ForegroundLabel()
		{
			MaskedBrush mb = new MaskedBrush(appLogo);
			mb.Opacity = 1;
			mb.overlaycolor = AppTileForgroundcolor;
			return mb;
		}
		public SolidColorBrush BackgroundColorBrush()
		{
			SolidColorBrush scb = new SolidColorBrush(AppTileBackgroundcolor);
			scb.Opacity = this.AppTileOpacity;
			return scb;
		}
	}
}
