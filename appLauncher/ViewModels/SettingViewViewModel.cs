using appLauncher.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI;

namespace appLauncher.ViewModels
{
   public class SettingViewViewModel : ViewModelBase
    {
        private List<string> logginginfo = new List<string>();
        public SettingViewViewModel()
        {
            logginginfo.Clear();
            logginginfo.Add("SettingsView");
            base.Logging("screen", logginginfo);
        }
        
        StorageFolder localFolder = ApplicationData.Current.LocalFolder;
        
        
        private async void ImageButton_Click(object sender, RoutedEventArgs e)
        {
            var picker = new Windows.Storage.Pickers.FileOpenPicker();
            picker.ViewMode = Windows.Storage.Pickers.PickerViewMode.Thumbnail;
            picker.SuggestedStartLocation = Windows.Storage.Pickers.PickerLocationId.PicturesLibrary;
            //Standard Image Support
            picker.FileTypeFilter.Add(".jpg");
            picker.FileTypeFilter.Add(".jpeg");
            picker.FileTypeFilter.Add(".jpe");
            picker.FileTypeFilter.Add(".png");
            picker.FileTypeFilter.Add(".svg");
            picker.FileTypeFilter.Add(".tif");
            picker.FileTypeFilter.Add(".tiff");
            picker.FileTypeFilter.Add(".bmp");

            //JFIF Support
            picker.FileTypeFilter.Add(".jif");
            picker.FileTypeFilter.Add(".jfif");

            //GIF Support
            picker.FileTypeFilter.Add(".gif");
            picker.FileTypeFilter.Add(".gifv");

            var file = await picker.PickMultipleFilesAsync();
            if (file.Any())
            {
                var backgroundImageFolder = await localFolder.CreateFolderAsync("backgroundImage", CreationCollisionOption.OpenIfExists);

                if (GlobalVariables.bgimagesavailable)
                {
                    BitmapImage bitmap = new BitmapImage();
                    var filesInFolder = await backgroundImageFolder.GetFilesAsync();
                    foreach (StorageFile item in file)
                    {
                        BackgroundImages bi = new BackgroundImages();
                        await bi.FileNameAsync(item);
                        bool exits = filesInFolder.Any(x => x.DisplayName == item.DisplayName);
                        if (!exits)
                        {
                            GlobalVariables.backgroundImage.Add(bi);
                           await item.CopyAsync(backgroundImageFolder);
                        }


                    }
                }
                else
                {
                    foreach (var item in file)
                    {
                        BackgroundImages bi = new BackgroundImages();
                        await bi.FileNameAsync(item);
                        GlobalVariables.backgroundImage.Add(bi);
                       await item.CopyAsync(backgroundImageFolder);
                    }

                    App.localSettings.Values["bgImageAvailable"] = true;
                    GlobalVariables.bgimagesavailable = true;
                }
                //   StorageFile savedImage = await file.CopyAsync(backgroundImageFolder);
                //    ((Window.Current.Content as Frame).Content as MainPage).loadSettings();
            }
            else
            {
                Debug.WriteLine("Operation cancelled.");
            }
        }
        private async void RemoveButton_ClickAsync(object sender, RoutedEventArgs e)
        {
            ListView imagelist = (ListView)sender;
            if (imagelist.SelectedIndex != -1)
            {
                BackgroundImages bi = (BackgroundImages)imagelist.SelectedItem;
                if (GlobalVariables.backgroundImage.Any(x => x.FileDisplayName== bi.FileDisplayName))
                {
                    var files = (from x in GlobalVariables.backgroundImage where x.FileDisplayName == bi.FileDisplayName select x).ToList();
                    foreach (var item in files)
                    {
                       GlobalVariables.backgroundImage.Remove(item);
                       
                    }
                }
                var backgroundImageFolder = await localFolder.CreateFolderAsync("backgroundImage", CreationCollisionOption.OpenIfExists);
                var filesinfolder = await backgroundImageFolder.GetFilesAsync();
                if (filesinfolder.Any(x => x.DisplayName == bi.FileDisplayName))
                {
                    IEnumerable<StorageFile> files = (from x in filesinfolder where x.DisplayName == bi.FileDisplayName select x).ToList();
                    foreach (var item in files)
                    {
                        await item.DeleteAsync();
                    }
                }
            }
        }
        public async Task ClearSettings()
        {
            var backgroundImageFolder = await localFolder.CreateFolderAsync("backgroundImage", CreationCollisionOption.OpenIfExists);
            await backgroundImageFolder.DeleteAsync(StorageDeleteOption.Default);
            App.localSettings.Values["bgImageAvailable"] = false;
            GlobalVariables.backgroundImage.Clear();
            foreach (AppItems item in GlobalVariables.AllItems)
            {
                item.BackgroundColor = Colors.Blue;
                item.ForgroundColor = Colors.Transparent;               
            }

        }
    }
}
