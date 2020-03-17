using Microsoft.Msagl.Drawing;
using Msagl.SkiaGraph;

namespace SkiaTests {
    public partial class AutomaticGraphLayoutSkia  {
        public AutomaticGraphLayoutSkia() {
            InitializeComponent();
        }

        public static readonly System.Windows.DependencyProperty GraphProperty = System.Windows.DependencyProperty.Register(
            "Graph", typeof(Graph), typeof(AutomaticGraphLayoutSkia), new System.Windows.PropertyMetadata(default(Graph),
                (s,e)=> (s as AutomaticGraphLayoutSkia).SkElement.InvalidateVisual()));

        public Graph Graph {
            get => (Graph) GetValue(GraphProperty);
            set => SetValue(GraphProperty, value);
        }

        private void SKElement_OnPaintSurface(object sender, SKPaintSurfaceEventArgs e) {
            new SKCanvasGraph(e, Graph).Draw();
        }
    }
}