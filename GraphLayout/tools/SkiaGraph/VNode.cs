using System;
using System.Collections.Generic;
using Microsoft.Msagl.Drawing;

namespace Msagl.SkiaGraph
{
    internal class VNode : IViewerNode {
        public VNode(Node node) {
            Node = node;
        }

        public DrawingObject DrawingObject { get; }
        public bool MarkedForDragging { get; set; }
        public event EventHandler MarkedForDraggingEvent;
        public event EventHandler UnmarkedForDraggingEvent;
        public Node Node { get; }
        public IEnumerable<IViewerEdge> InEdges { get; }
        public IEnumerable<IViewerEdge> OutEdges { get; }
        public IEnumerable<IViewerEdge> SelfEdges { get; }
        public event Action<IViewerNode> IsCollapsedChanged;
    }
}