using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;
using appLauncher.Helpers;
using appLauncher.Model;
using Windows.UI.Xaml.Controls;

namespace appLauncher.ViewModels
{
public class BackGroundImageViewModel : ViewModelBase
    {
       
        private Color ForeGroundColor = Colors.Transparent;
        private List<AppColor> overlaycolors;
        private BackgroundImages selecteditem;
        private string appColor;
        public string PreviewText
        {
            get { return $"This is what image would look like with {appColor} Overlay"; } 
        }
        public BackgroundImages SelectedItem
        {
            get { return selecteditem; }
            set { selecteditem = value; }
        }
        public void UpdateImage(object sender,SelectionChangedEventArgs e)
        {
            ForeGroundColor = (selecteditem.ForgroundColor!=ForeGroundColor)?ForeGroundColor:;
            AppColor c =(AppColor)((ComboBox)sender).SelectedValue;
             SelectedItem.ForgroundColor = c.ActualColor;
            appColor = c.ColorName;
            OnproperyChanged("PreviewText");
            OnproperyChanged("SelectedItem");
        }
        public List<AppColor> OverlayColors
        {
            get
            {
                if (overlaycolors==null)
                {
                    OverlayColors = new List<AppColor>();
                    GenerateColors();
                    return overlaycolors;
                }
                return overlaycolors;
            }
            set { overlaycolors = value; OnproperyChanged("OverLayColors"); }
            
        }
        public void GenerateColors()
        {
            foreach (var color in typeof(Colors).GetRuntimeProperties())
            {
                OverlayColors.Add(new AppColor
                {
                    ActualColor = (Color)color.GetValue(null),
                    ColorName = color.Name
                });
                
            }
        }
}}
