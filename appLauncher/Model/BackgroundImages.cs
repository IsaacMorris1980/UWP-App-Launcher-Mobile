using appLauncher.Brushes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    public class BackgroundImages : ModelBase
    {
      
        public StorageItemThumbnail thumbnail;
        private string filedisplayname;
        private string fullname;
        
       

     public BackgroundImages()
        {
            
        }

        public string FileDisplayName
        {
            get { return filedisplayname; }
            set { filedisplayname = value; }
        }
       
        
            
        
              public async Task FileNameAsync(StorageFile sf)
        {
            FileDisplayName = sf.DisplayName;
            fullname = sf.Name;
            thumbnail = await sf.GetThumbnailAsync(ThumbnailMode.PicturesView, 10, ThumbnailOptions.ResizeThumbnail);
            await  Convert(await sf.OpenAsync(FileAccessMode.Read));
        }
        private Color forgroundColor = Colors.Transparent;
        private Color backgroundColor = Colors.Transparent;

        public byte[] ImageBytes { get; set; }
        public Color BackgroundColor { get { return backgroundColor; } set { backgroundColor = value; } } //not used by background images
        public Color ForgroundColor { get { return forgroundColor; } set { forgroundColor = value; OnPropertyChanged("GetBackGoundImageBrush"); } }
        public async Task Convert(IRandomAccessStream s)
        {
            using (var dr = new DataReader(s.GetInputStreamAt(0)))
            {

                var bytes = new byte[s.Size];
                await dr.LoadAsync((uint)s.Size);
                dr.ReadBytes(bytes);
                ImageBytes = bytes;
                await s.FlushAsync();
                s.Dispose();
                OnPropertyChanged("GetBackGroundImageBrush");
            }
        }
        public MaskedBrush GetBackGroundImageBrush()
        {
            MaskedBrush mb = new MaskedBrush(ImageBytes);
            mb.overlaycolor = this.ForgroundColor;
            return mb;
        }
        //private BitmapImage bitmapimage;
        //public string Filename { get => filename; set => filename = value; }
        //public BitmapImage Bitmapimage { get => bitmapimage; set => bitmapimage = value; }
    }
}
