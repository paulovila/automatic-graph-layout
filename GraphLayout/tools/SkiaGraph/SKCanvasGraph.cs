using Microsoft.Msagl.Drawing;
using SkiaSharp;

namespace Msagl.SkiaGraph {
    public class SKCanvasGraph {
        private readonly Graph _graph;
        private readonly SKCanvas _canvas;
        private readonly float _width;
        private readonly float _height;
        public SKCanvasGraph(SKPaintSurfaceEventArgs arg, Graph graph) {
            _graph = graph;
            _canvas = arg.Surface.Canvas;
            _width = arg.Info.Width;
            _height = arg.Info.Height;
        }

        public void Draw() {
            if (_graph == null) {
                _canvas.Clear();
                return;
            }
            var paint = new SKPaint { Color = new SKColor(0, 250, 0), TextSize = 34 };
            var position = new SKPoint(_width / 2f, _height / 2f);
            foreach (var node in _graph.Nodes) {
                _canvas.DrawText(node.LabelText, position, paint);
                position.Y += 10;
            }
        }
    }
}
