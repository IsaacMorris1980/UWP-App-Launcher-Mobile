using appLauncher.Brushes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.FileProperties;
using Windows.Storage.Streams;
using Windows.UI;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;

namespace appLauncher.Model
{
   public class BackgroundImages
    {
        private IRandomAccessStream backimage;
        public StorageItemThumbnail thumbnail;
        public string FileDisplayName;
       
        public MaskedBrush BackGroundImageBrush
        {
            get {
                MaskedBrush mb = new MaskedBrush(backimage);
                mb.overlaycolor = Colors.Transparent;
                return mb;
            }
        }
        public async Task FileNameAsync(string filename)
        {
            StorageFolder storageFolder =await ApplicationData.Current.LocalFolder.CreateFolderAsync("BackGroundImage",CreationCollisionOption.OpenIfExists);
            StorageFile sf = await storageFolder.GetFileAsync(filename);
            FileDisplayName = sf.DisplayName;
            thumbnail = await sf.GetThumbnailAsync(ThumbnailMode.PicturesView, 10, ThumbnailOptions.ResizeThumbnail);
            backimage = await sf.OpenAsync(FileAccessMode.Read);
        }
       //private BitmapImage bitmapimage;
       //public string Filename { get => filename; set => filename = value; }
       //public BitmapImage Bitmapimage { get => bitmapimage; set => bitmapimage = value; }
    }
}
