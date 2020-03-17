using SkiaSharp;

namespace Msagl.SkiaGraph
{
    public class SKPaintSurfaceEventArgs {
        public SKImageInfo Info { get; }
        public SKSurface Surface { get; }

        public SKPaintSurfaceEventArgs(SKSurface surface, SKImageInfo info) {
            Info = info;
            Surface = surface;
        }
    }
}