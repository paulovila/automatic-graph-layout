using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Forms.Integration;
using Microsoft.Msagl.Drawing;
using Microsoft.Msagl.GraphViewerGdi;
using Microsoft.Msagl.WpfGraphControl;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SkiaTests {
    [TestClass]
    public class SkiaTest {
        private Graph TestGraph() {
            var graph = new Graph();
            graph.AddNode("Octagon").Attr.Shape = Shape.Octagon;
            graph.Attr.LayerDirection = LayerDirection.LR;
            return graph;
        }

        async Task Run(Action action) {
            var tcs = new TaskCompletionSource<bool>();
            var t = new Thread(() => action());
            t.SetApartmentState(ApartmentState.STA);
            t.Start();
            t.Join();
            await tcs.Task;
            Assert.IsTrue(true);
        }

        [TestMethod]
        public Task TestWPFSkiaOverlapped() => Run(() => {
            var grid = new Grid();
            var window = new Window();
            var graph = TestGraph();
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

        [TestMethod]
        public Task TestWindowsFormsSkiaSideBySide() => Run(() => {
            var grid = new Grid();
            grid.ColumnDefinitions.Add(new ColumnDefinition());
            grid.ColumnDefinitions.Add(new ColumnDefinition());
            var window = new Window();
            var graph = TestGraph();
            var viewer = new GViewer { Graph = TestGraph(), Dock = DockStyle.Fill };
            var host = new WindowsFormsHost { Child = viewer };
            var skia = new AutomaticGraphLayoutSkia();
            skia.Loaded += (s, e) => skia.Graph = graph;
            Grid.SetColumn(host, 1);
            grid.Children.Add(host);
            grid.Children.Add(skia);
            window.Content = grid;
            window.Show();
            System.Windows.Threading.Dispatcher.Run();
        });
    }
}