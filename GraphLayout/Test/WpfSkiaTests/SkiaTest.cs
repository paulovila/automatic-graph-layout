using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Msagl.Drawing;
using Microsoft.Msagl.WpfGraphControl;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SkiaTests {
    [TestClass]
    public class SkiaTest {
        [TestMethod]
        public async Task TestMethod1() {
            var tcs = new TaskCompletionSource<bool>();
            var t = new Thread(() => {
                var graph = new Graph();
                graph.AddNode("Octagon").Attr.Shape = Shape.Octagon;
                graph.Attr.LayerDirection = LayerDirection.LR;
                var grid = new Grid();
                var window = new Window();
                var wpf = new AutomaticGraphLayoutControl();
                wpf.Loaded += (s, e) => wpf.Graph = graph;
                var skia = new AutomaticGraphLayoutSkia();
                skia.Loaded += (s, e) => skia.Graph = graph;
                grid.Children.Add(wpf);
                grid.Children.Add(skia);
                window.Content = grid;
                window.Show();
                System.Windows.Threading.Dispatcher.Run();
            });
            t.SetApartmentState(ApartmentState.STA);
            t.Start();
            t.Join();
            await tcs.Task;
            Assert.IsTrue(true);
        }
    }
}