﻿using appLauncher.Brushes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage.Streams;
using Windows.UI;

namespace appLauncher.Model
{
    public class AppItems : ModelBase
    {
        public string developer { get; set; }
        public string name { get; set; }
        public bool isfavorite { get; set; }
        public DateTimeOffset installed { get; set; }
        public string fullName { get; set; }
        private Color forgroundColor = Colors.Transparent;
        private Color backgroundColor = Colors.Blue;

        public byte[] ImageBytes { get; set; }
        public Color BackgroundColor
        {
            get {
                if (backgroundColor==null)
                {
                    return Colors.Blue;
                }
                
                return backgroundColor; }
            set
            {
                if (value == null)
                {
                    backgroundColor = Colors.Blue;
                    OnPropertyChanged("BackgroundColor");
                    return;
                }
                backgroundColor = value;
                OnPropertyChanged("BackgroundColor");
            } } //not used by background images

        public Color ForgroundColor { get {return forgroundColor; }
            set
            {
                if (value==null)
                {
                    forgroundColor = Colors.Blue;
                    OnPropertyChanged("GetAppLogoImageBrush");
                }  forgroundColor = value; OnPropertyChanged("GetAppLogoImageBrush"); } }
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
                OnPropertyChanged("GetAppLogoImageBrush");
            }
        }
        public MaskedBrush GetAppLogoImageBrush()
        {
            MaskedBrush mb = new MaskedBrush(ImageBytes);
            mb.overlaycolor = this.ForgroundColor;
            return mb;
        }
    }
    }
