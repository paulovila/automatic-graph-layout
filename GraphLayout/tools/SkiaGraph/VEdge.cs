using System;
using Microsoft.Msagl.Drawing;

namespace Msagl.SkiaGraph
{
    internal class VEdge : IViewerEdge {
        public DrawingObject DrawingObject { get; }
        public bool MarkedForDragging { get; set; }
        public event EventHandler MarkedForDraggingEvent;
        public event EventHandler UnmarkedForDraggingEvent;
        public Edge Edge { get; }
        public IViewerNode Source { get; }
        public IViewerNode Target { get; }
        public double RadiusOfPolylineCorner { get; set; }
    }
}