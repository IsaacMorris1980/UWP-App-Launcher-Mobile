using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using appLauncher.Model;
using System.Collections.ObjectModel;
using appLauncher.Helpers;
using Windows.Foundation;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using System.IO;
using Newtonsoft.Json;

namespace appLauncher
{
    public static class GlobalVariables
    {
        public static int appsperscreen { get; set; }
        public static finalAppItem itemdragged { get; set; }
        public static int columns { get; set; }
        public static int oldindex { get; set; }
        public static int newindex { get; set; }
        public static int pagenum { get; set; }
        public static bool isdragging { get; set; }
        public static bool bgimagesavailable { get; set; }

        private static StorageFolder localFolder = ApplicationData.Current.LocalFolder;
        public static PagniationAsyncObservableCollection<BackgroundImages> backgroundImage { get; set; } = new PagniationAsyncObservableCollection<BackgroundImages>();
        public static PagniationAsyncObservableCollection<AppItems> AllItems { get; set; } = new PagniationAsyncObservableCollection<AppItems>();
        public static Point startingpoint { get; set; }



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
        public static async Task LoadCollectionAsync()
        {

            List<finalAppItem> oc1 = AllApps.listOfApps.ToList();
            ObservableCollection<finalAppItem> oc = new ObservableCollection<finalAppItem>();
            if (await IsFilePresent("collection.txt"))
            {

                StorageFile item = (StorageFile)await ApplicationData.Current.LocalFolder.TryGetItemAsync("collection.txt");
                var apps = await FileIO.ReadTextAsync(item);
                List<AppItems> apps1 = JsonConvert.DeserializeObject<List<AppItems>>(apps);
                AllItems = new PagniationAsyncObservableCollection<AppItems>(apps1);
              
            }





        }
        public static async Task SaveCollectionAsync()
        {
           
                List<AppItems> finals = AllItems.ToList();
            var listapp = JsonConvert.SerializeObject(finals,Formatting.Indented);
                 StorageFile item = (StorageFile)await ApplicationData.Current.LocalFolder.CreateFileAsync("collection.txt", CreationCollisionOption.ReplaceExisting);
                await FileIO.WriteTextAsync(item, listapp);
           

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
                if (await IsFilePresent("images.txt"))
                {
                    try
                    {
                        
                        StorageFile item = (StorageFile)await ApplicationData.Current.LocalFolder.TryGetItemAsync("images.txt");
                        var images = await FileIO.ReadTextAsync(item);
                        StorageFolder localFolder = ApplicationData.Current.LocalFolder;
                       List<BackgroundImages> image = JsonConvert.DeserializeObject<List<BackgroundImages>>(images);
                        backgroundImage = new PagniationAsyncObservableCollection<BackgroundImages>(image);
                        //var backgroundImageFolder = await localFolder.CreateFolderAsync("backgroundImage", CreationCollisionOption.OpenIfExists);
                        //var filesInFolder = await backgroundImageFolder.GetFilesAsync();
                        //if (images.Count > 0)
                        //{
                        //    foreach (string y in images)
                        //    {
                        //        foreach (var items in filesInFolder)
                        //        {
                        //            if (items.DisplayName == y)
                        //            {
                        //                BackgroundImages bi = new BackgroundImages();
                        //                await bi.FileNameAsync(items);
                        //            }
                        //        }
                        //    }
                        //}
                        //else
                        //{
                        //    foreach (var items in filesInFolder)
                        //    {
                        //        BackgroundImages bi = new BackgroundImages();
                        //        await bi.FileNameAsync(items);                               
                        //        backgroundImage.Add(bi);


                        //    }
                        //}
                    }
                    catch (Exception e)
                    {
                        await Logging(e.ToString());
                    }
                }
                else
                {
                    StorageFolder localFolder = ApplicationData.Current.LocalFolder;
                    var backgroundImageFolder = await localFolder.CreateFolderAsync("backgroundImage", CreationCollisionOption.OpenIfExists);
                    var filesInFolder = await backgroundImageFolder.GetFilesAsync();
                    foreach (var items in filesInFolder)
                    {
                        BackgroundImages bi = new BackgroundImages();
                        await bi.FileNameAsync(items);
                        backgroundImage.Add(bi);

                    }
                }
            }

        }

        public static async Task SaveImageOrder()
        {
            List<BackgroundImages> images = backgroundImage.ToList();
             var json = JsonConvert.SerializeObject(images,Formatting.Indented);
                StorageFile item = (StorageFile)await ApplicationData.Current.LocalFolder.CreateFileAsync("images.txt", CreationCollisionOption.ReplaceExisting);
           await FileIO.WriteTextAsync(item,json);

           

        }
    }
    
    }

