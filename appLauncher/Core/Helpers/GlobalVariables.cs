using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using appLauncher.Core.Models;
using System.Collections.ObjectModel;
using appLauncher.Core.Helpers;
using Windows.Foundation;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using System.IO;
using Newtonsoft.Json;

namespace appLauncher.Core.Helpers
{
    public static class GlobalVariables
    {
        public static int appsperscreen { get; set; }
        public static AppTile itemdragged { get; set; }
        public static int columns { get; set; }
        public static int oldindex { get; set; }
        public static int newindex { get; set; }
        public static int pagenum { get; set; }
        public static bool isdragging { get; set; }
        public static bool bgimagesavailable { get; set; }
        public static Point startingpoint { get; set; }
        public static AppSettings appSettings {get;set;}=new AppSettings();


        private static StorageFolder localFolder = ApplicationData.Current.LocalFolder;
        public static ObservableCollection<BackgroundImageTile> backgroundImage { get; set; } = new ObservableCollection<BackgroundImageTile>();
       public static ObservableCollection<AppTile> allApps { get; set; } = new ObservableCollection<AppTile>();


        public static async Task Logging(string texttolog)
        {
            StorageFolder stors = ApplicationData.Current.LocalFolder;
            await FileIO.AppendTextAsync(await stors.CreateFileAsync("logfile.txt", CreationCollisionOption.OpenIfExists), texttolog);
            await FileIO.AppendTextAsync(await stors.CreateFileAsync("logfile.txt", CreationCollisionOption.OpenIfExists), Environment.NewLine);
        }
        public static int NumofRoworColumn(int padding, int objectsize, int sizetofit)
        {
            int amount = 0;
            int intsize = objectsize + (padding + padding);
            int size = intsize;
            while (size + intsize < sizetofit)
            {
                amount += 1;
                size += intsize;
            }
            return amount;

        }
     
        public static async Task SaveCollectionAsync()
        {
          StorageFile item = (StorageFile)await ApplicationData.Current.LocalFolder.CreateFileAsync("collection.json", CreationCollisionOption.ReplaceExisting);
          var te = JsonConvert.SerializeObject(allApps,Formatting.Indented);
          await FileIO.WriteTextAsync(item,te);
           

        }
        public static async Task<bool> IsFilePresent(string fileName)
        {
            var item = await ApplicationData.Current.LocalFolder.TryGetItemAsync(fileName);
            return item != null;
        }
        public static async Task LoadBackgroundImages()
        {
            if (bgimagesavailable)
            {
                if (await IsFilePresent("images.json"))
                {
                    try
                    {
                        StorageFile item = (StorageFile)await ApplicationData.Current.LocalFolder.TryGetItemAsync("images.json");
                       backgroundImage = JsonConvert.DeserializeObject<ObservableCollection<BackgroundImageTile>>(await FileIO.ReadTextAsync(item));
                    }
                    catch (Exception e)
                    {
                        await Logging(e.ToString());
                    }
                }
                
            }

        }

        public static async Task SaveImageOrder()
        {
                StorageFile item = (StorageFile)await ApplicationData.Current.LocalFolder.CreateFileAsync("images.json", CreationCollisionOption.ReplaceExisting);
            var str = JsonConvert.SerializeObject(backgroundImage, Formatting.Indented);
                await FileIO.WriteTextAsync(item, str);

           

        }
    }
    
    }

