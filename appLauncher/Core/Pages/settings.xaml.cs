using appLauncher.Core.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;
using appLauncher.Core.Helpers;
using Windows.UI;


// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace appLauncher.Core.Pages
{
    /// <summary>
    /// Page where the launcher settings are configured
    /// </summary>
    public sealed partial class settings : Page
    {
        StorageFolder localFolder = ApplicationData.Current.LocalFolder;
   

        public settings()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Runs when the app has navigated to this page.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            if (GlobalVariables.bgimagesavailable)
            {
                imageButton.Content = "Add Image";
            }
        }

        /// <summary>
        /// Launches the file picker and allows the user to pick an image from their pictures library.<br/>
        /// The image will then be used as the background image in the main launcher page.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void imageButton_Click(object sender, RoutedEventArgs e)
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
            if (GlobalVariables.bgimagesavailable) 
                {
                    
                  foreach (StorageFile item in file)
                    {
                       bool exits = GlobalVariables.BackgroundImage.Any(x => x.DisplayName == item.DisplayName);
                        if (!exits)
                        {
                            BackgroundImageTile bi = new BackgroundImageTile();
                            bi.DisplayName = item.DisplayName;
                            var te = await item.OpenAsync(FileAccessMode.Read);
                            byte[] bitmapImageBytes = null;
                            var reader = new Windows.Storage.Streams.DataReader(te.GetInputStreamAt(0));
                            bitmapImageBytes = new byte[te.Size];
                            await reader.LoadAsync((uint)te.Size);
                            reader.ReadBytes(bitmapImageBytes);
                            bi.Backgroundimage = bitmapImageBytes;
                            bi.BackgroundImageColor = Colors.Transparent;
                            bi.BackgroundImageOpacity = 1;
                            GlobalVariables.BackgroundImage.Add(bi);
                        }
                       

                    }
                }
                else
                {
                    foreach (var item in file)
                    {
                        BackgroundImageTile bi = new BackgroundImageTile();
                        bi.DisplayName = item.DisplayName;
                        bi.BackgroundImageOpacity = 1;
                        bi.BackgroundImageColor = Colors.Transparent;
                        var te = await item.OpenAsync(FileAccessMode.Read);
                        byte[] bitmapImageBytes = null;
                        var reader = new Windows.Storage.Streams.DataReader(te.GetInputStreamAt(0));
                        bitmapImageBytes = new byte[te.Size];
                        await reader.LoadAsync((uint)te.Size);
                        reader.ReadBytes(bitmapImageBytes);
                        bi.Backgroundimage = bitmapImageBytes;
                        GlobalVariables.BackgroundImage.Add(bi);
                        GlobalVariables.localSettings.Values["bgImageAvailable"] = true;
                        GlobalVariables.bgimagesavailable = true;

                    }
                    
                   
                }
               //   StorageFile savedImage = await file.CopyAsync(backgroundImageFolder);
           //    ((Window.Current.Content as Frame).Content as MainPage).loadSettings();
            }
            else
            {
                Debug.WriteLine("Operation cancelled.");
            }
        }

        private void RemoveButton_Click(object sender, RoutedEventArgs e)
        {

            if (imagelist.SelectedIndex != -1)
            {
                BackgroundImageTile bi = (BackgroundImageTile)imagelist.SelectedItem;
                if (GlobalVariables.BackgroundImage.Any(x => x.DisplayName == bi.DisplayName))
                {
                    var files = (from x in GlobalVariables.BackgroundImage where x.DisplayName == bi.DisplayName select x).ToList();
                    foreach (var item in files)
                    {
                        GlobalVariables.BackgroundImage.Remove(item);
                    }
                }

            }

        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {

        }
    }
}
