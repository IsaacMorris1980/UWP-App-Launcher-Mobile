using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using appLauncher.Core.Control;
using appLauncher.Core.Models;
using System.Collections.ObjectModel;
using Windows.UI.Core;
using appLauncher.Core;
using appLauncher.Core.Helpers;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace appLauncher.Core.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class SearchPage : Page
    {
        public ReadOnlyObservableCollection<AppTile> queriedApps = new ReadOnlyObservableCollection<AppTile>(GlobalVariables.AllApps);
        public SearchPage()
        {
            this.InitializeComponent();
            SystemNavigationManager.GetForCurrentView().BackRequested += SearchPage_BackRequested;
            DesktopBackButton.ShowBackButton();
            QueriedAppsListView.ItemsSource = queriedApps;
        }

        private void SearchPage_BackRequested(object sender, BackRequestedEventArgs e)
        {
            DesktopBackButton.HideBackButton();
            e.Handled = true;
            Frame.Navigate(typeof(MainPage));
        }

        private void useMeTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            string query = useMeTextBox.Text.ToLower();
            if (!String.IsNullOrEmpty(query))
            {
                QueriedAppsListView.ItemsSource = queriedApps.Where(p => p.AppName.ToLower().Contains(query));

            }
            else
            {
                QueriedAppsListView.ItemsSource = queriedApps;
            }
           
        }
        private async void QueriedAppsListView_ItemClick(object sender, ItemClickEventArgs e)
        {

            await ((AppTile)e.ClickedItem).LaunchAsync();


        }


    }
}
