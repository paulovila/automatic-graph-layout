using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Msagl.Core.Routing;
using Microsoft.Msagl.Drawing;

namespace MinWpfApp {
    public partial class MainWindow {
        private Graph _graph;

        public MainWindow() {
            InitializeComponent();
            Loaded += MainWindow_Loaded;
        }
        public class Transition {
            public string StartState { get; set; }
            public string EndState { get; set; }
            public string Name { get; set; }
        }
        private void MainWindow_Loaded(object sender, RoutedEventArgs e) {

            _graph = new Graph();
            var transitions = new[] {

             new Transition { Name = "Add",                 StartState="New",               EndState="Modifying"},
             new Transition { Name = "Cancel",              StartState="Modifying",         EndState="New"},
             new Transition { Name = "Save",                StartState="Modifying",         EndState="Modifying"},
             new Transition { Name = "Submit",              StartState="Modifying",         EndState="SubmittedLive"},
             new Transition { Name = "Cancel",              StartState="SubmittedLive",     EndState="Modifying"},
             new Transition { Name = "Approve",             StartState="SubmittedLive",     EndState="GoingLive"},
             new Transition { Name = "Cancel",              StartState="GoingLive",         EndState="SubmittedLive"},
             new Transition { Name = "Go Live",             StartState="GoingLive",         EndState="Live"},
             new Transition { Name = "Submit Demo",         StartState="Modifying",         EndState="SubmittedDemo"},
             new Transition { Name = "Cancel Demo",         StartState="SubmittedDemo",     EndState="Modifying"},
             new Transition { Name = "Cancel Demo",         StartState="GoingDemo",         EndState="SubmittedDemo"},
             new Transition { Name = "Approve Demo",        StartState="SubmittedDemo",     EndState="GoingDemo"},
             new Transition { Name = "Go Demo",             StartState="GoingDemo",         EndState="Demo"},
             new Transition { Name = "Edit",                StartState="Live",              EndState="Modifying"},
             new Transition { Name = "Cancel",              StartState="Modifying",         EndState="Live"},
             new Transition { Name = "Edit",                StartState="Demo",              EndState="Modifying"},
             new Transition { Name = "Cancel",              StartState="Modifying",         EndState="Demo"},
             new Transition { Name = "Submit Delete",       StartState="Live",              EndState="Deleting"},
             new Transition { Name = "Cancel",              StartState="Deleting",          EndState="Live"},
             new Transition { Name = "Cancel",              StartState="Deleting",          EndState="Demo"},
             new Transition { Name = "Submit Delete Demo",  StartState="Demo",              EndState="Deleting"},
             new Transition { Name = "Delete",              StartState="Deleting",          EndState="Deleted"},
             new Transition { Name = "Restore",             StartState="Deleted",           EndState="Modifying"},



            };
            foreach (var transition in transitions) {
                var edge = _graph.AddEdge(transition.StartState, transition.Name, transition.EndState);
                edge.SourceNode.Attr.FillColor = Color.LightBlue;
                edge.SourceNode.LabelText = transition.StartState;
                edge.TargetNode.Attr.FillColor = Color.LightBlue;
                edge.TargetNode.LabelText = transition.EndState;
                edge.Label.FontSize = edge.Label.FontSize * 0.6;
            }
            Node F(string s) => _graph.FindNode(s);

            _graph.LayerConstraints.AddSameLayerNeighbors(F("New"));
            _graph.LayerConstraints.AddSameLayerNeighbors(F("SubmittedDemo"), F("Modifying"), F("SubmittedLive"));
            _graph.LayerConstraints.AddSameLayerNeighbors(F("GoingDemo"), F("Deleted"), F("GoingLive"));
            _graph.LayerConstraints.AddSameLayerNeighbors(F("Demo"), F("Deleting"), F("Live"));
            _graph.LayerConstraints.AddUpDownVerticalConstraint(F("New"), F("Modifying"));
            _graph.LayerConstraints.AddUpDownVerticalConstraint(F("Modifying"), F("Deleted"));
            _graph.LayerConstraints.AddUpDownVerticalConstraint(F("Deleted"), F("Deleting"));
            _graph.LayerConstraints.AddUpDownVerticalConstraint(F("SubmittedLive"), F("GoingLive"));
            _graph.LayerConstraints.AddUpDownVerticalConstraint(F("GoingLive"), F("Live"));
            _graph.LayerConstraints.AddUpDownVerticalConstraint(F("SubmittedDemo"), F("GoingDemo"));
            _graph.LayerConstraints.AddUpDownVerticalConstraint(F("GoingDemo"), F("Demo"));
            _graph.LayoutAlgorithmSettings.EdgeRoutingSettings.EdgeRoutingMode = EdgeRoutingMode.Spline;
            graphControl.Graph = _graph;
        }

        private void routingModeCombo_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            if (_graph == null) return;

                var mode = (EdgeRoutingMode)Enum.Parse(typeof(EdgeRoutingMode), (string)((ComboBoxItem)routingModeCombo.SelectedItem).Content);
            _graph.LayoutAlgorithmSettings.EdgeRoutingSettings.EdgeRoutingMode = mode;
            graphControl.Graph = null;
            graphControl.Graph = _graph;
           
        }
    }
}