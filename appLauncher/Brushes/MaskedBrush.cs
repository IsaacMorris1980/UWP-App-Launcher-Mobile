using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage.Streams;
using Windows.UI;
using Windows.UI.Composition;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media;

namespace appLauncher.Brushes
{
    public sealed class MaskedBrush : XamlCompositionBrushBase
    {
        public MaskedBrush(IRandomAccessStream stream)
        {
            this.stream = stream;
        }
        private IRandomAccessStream stream; 
        private CompositionMaskBrush _maskedbrush;
        public Color overlaycolor { get; set; }
        protected override void OnConnected()
        {
                      // Get a reference to the Compositor
            Compositor compositor = Window.Current.Compositor;
            CompositionColorBrush colorbrush;
            // Use LoadedImageSurface API to get ICompositionSurface from image uri provided
            colorbrush = compositor.CreateColorBrush((overlaycolor!=null)?Colors.Red:overlaycolor);
            _maskedbrush = compositor.CreateMaskBrush();
            _maskedbrush.Source = colorbrush;
            LoadedImageSurface loadedSurface = LoadedImageSurface.StartLoadFromStream(stream);
            _maskedbrush.Mask = compositor.CreateSurfaceBrush(loadedSurface);
            CompositionBrush = _maskedbrush;

        }
  
        protected override void OnDisconnected()
{
    // Dispose Surface and CompositionBrushes if XamlCompBrushBase is removed from tree
    _maskedbrush?.Dispose();
    _maskedbrush = null;

    CompositionBrush?.Dispose();
    CompositionBrush = null;
}
    }
}
