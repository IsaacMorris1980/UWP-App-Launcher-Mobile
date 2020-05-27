﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Activation;
using Windows.Foundation;
using Windows.UI.Xaml.Controls;

using appLauncher.Core.Models;
using Microsoft.Toolkit.Uwp.UI.Animations;
using System.IO;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation.Collections;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using appLauncher.Core.Helpers;
using appLauncher.Pages;

namespace appLauncher.Core.ViewModels
{
   public  class SplashScreenViewModel : BaseModel
    {
        internal Rect splashImageRect; // Rect to store splash screen image coordinates.
        private SplashScreen mySplash; // Variable to hold the splash screen object.
        internal bool dismissed = false; // Variable to track splash screen dismissal status.
        internal static Frame rootFrame;
        static bool appsLoaded = false;
        public static Image theImage = new Image();
        internal DispatcherTimer timer;
        public SplashScreenViewModel(SplashScreen splashscreen, bool loadState, ref Frame RootFrame,Image images)
        {
            timer.Tick += Timer_Tick;
            timer.Interval = TimeSpan.FromSeconds(15);
            timer.Start();
            // Listen f or window resize events to reposition the extended splash screen image accordingly.
            // This ensures that the extended splash screen formats properly in response to window resizing.
            Window.Current.SizeChanged += new WindowSizeChangedEventHandler(ExtendedSplash_OnResize);
            theImage = images;
            mySplash = splashscreen;
            if (mySplash != null)
            {
                rootFrame = RootFrame;
                // Register an event handler to be executed when the splash screen has been dismissed.
                mySplash.Dismissed += new TypedEventHandler<SplashScreen, Object>(DismissedEventHandler);

                // Retrieve the window coordinates of the splash screen image.
                splashImageRect = mySplash.ImageLocation;
                PositionImage();


            }
            Task.Run(() => packageHelper.getAllAppsAsync());
            // Create a Frame to act as the navigation context


        }

        private void Timer_Tick(object sender, object e)
        {
            
            DismissExtendedSplash();
            (sender as DispatcherTimer).Stop();
        }
 
        private async void DismissedEventHandler(SplashScreen sender, object args)
        {
            await Windows.ApplicationModel.Core.CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                if (mySplash != null)
                {
                    // Update the coordinates of the splash screen image.
                    splashImageRect = mySplash.ImageLocation;
                    PositionImage();


                }
            });

            dismissed = true;


            await Windows.ApplicationModel.Core.CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, async () =>
            {
                while (appsLoaded == false)
                {
                    await theImage.Scale(0.9f, 0.9f, (float)theImage.ActualWidth / 2, (float)theImage.ActualHeight / 2, 1000, 0, EasingType.Linear).StartAsync();
                    await theImage.Scale(1f, 1f, (float)theImage.ActualWidth / 2, (float)theImage.ActualHeight / 2, 1000, 0, EasingType.Linear).StartAsync();


                }

            });
           // Complete app setup operations here...

        }





        public async void DismissExtendedSplash()
        {
            appsLoaded = true;
            await Windows.ApplicationModel.Core.CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, async () =>
            {
                //await theImage.Scale(0.5f, 0.5f, (float)theImage.ActualWidth / 2, (float)theImage.ActualHeight / 2, 200, 0, EasingType.Linear).StartAsync();
                var bounds = Window.Current.Bounds;
                double width = bounds.Width;
                double height = bounds.Height;
                var imageVisual = theImage.TransformToVisual(Window.Current.Content);
                var visualStuff = imageVisual.TransformPoint(new Point(0, 0));
                var imagePosX = visualStuff.X;

                var imageXToTravelTo = width - imagePosX;



                await theImage.Offset(-100, 100).StartAsync();
                var anim = theImage.Offset((float)width / 2, (float)-height / 2, 100, 0, EasingType.Cubic).Fade(0, 50, 50);


                anim.Completed += Anim_Completed;
                await anim.StartAsync();


            });
        }


        private void Anim_Completed(object sender, AnimationSetCompletedEventArgs e)
        {

            rootFrame.Content = new Pages.MainPage();
            Window.Current.Content = rootFrame;
            rootFrame.Navigate(typeof(MainPage));
        }

        private void ExtendedSplash_OnResize(object sender, WindowSizeChangedEventArgs e)
        {
            // Safely update the extended splash screen image coordinates. This function will be executed when a user resizes the window.
            if (mySplash != null)
            {
                // Update the coordinates of the splash screen image.
                splashImageRect = mySplash.ImageLocation;
                PositionImage();

                // If applicable, include a method for positioning a progress control.
                // PositionRing();
            }


        }

        void PositionImage()
        {
            theImage.SetValue(Canvas.LeftProperty, splashImageRect.X);
            theImage.SetValue(Canvas.TopProperty, splashImageRect.Y);
            theImage.Height = splashImageRect.Height;
            theImage.Width = splashImageRect.Width;
        }





    }
}
