using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Virus_Simulator
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void gViewer1_Load(object sender, EventArgs e) {
            Graph<City> Country = new Graph<City>();
            List<City> Cities = new List<City>();
            City firstInfectedCity = new City();
            Program.ReadCities(Cities, firstInfectedCity);
            foreach (City city in Cities)
            {
                Country.AddNode(city);
            }
            Program.ReadCitiesConnection(Country);

            Microsoft.Msagl.Drawing.Graph graph = new Microsoft.Msagl.Drawing.Graph("graph");
            // Draw nodes
            for (int i = 0; i < Country.nodes.Count; i++)
            {
                City city = Country.nodes[i].item;
                Microsoft.Msagl.Drawing.Node thisNode;
                if (graph.FindNode(city.name) == null) {
                    thisNode = graph.AddNode(city.name);
                } else {
                    thisNode = graph.FindNode(city.name);
                }
                Graph<City>.AdjacentNodes<City>[] adjacentNodes = Country.Adjacent(i);
                foreach (Graph<City>.AdjacentNodes<City> adj in adjacentNodes)
                {
                    if (thisNode.InEdges.Where(edge => edge.Source == adj.second.name).Count() != 0) {
                        thisNode.InEdges.Where(edge => edge.Source == adj.second.name).Single().Attr.ArrowheadAtSource = Microsoft.Msagl.Drawing.ArrowStyle.Normal;
                    } else {
                        graph.AddEdge(adj.first.name, adj.second.name);
                    }
                }
            }

            Program.DrawInfection(graph, "A", "B");
            //graph.AddEdge("A", "B");
            //graph.AddEdge("B", "C");
            //graph.AddEdge("A", "C").Attr.Color = Microsoft.Msagl.Drawing.Color.Green;
            //graph.FindNode("A").Attr.FillColor = Microsoft.Msagl.Drawing.Color.Magenta;
            //graph.FindNode("B").Attr.FillColor = Microsoft.Msagl.Drawing.Color.MistyRose;
            //Microsoft.Msagl.Drawing.Node c = graph.FindNode("C");
            //c.Attr.FillColor = Microsoft.Msagl.Drawing.Color.PaleGreen;
            //c.Attr.Shape = Microsoft.Msagl.Drawing.Shape.Diamond;

            graphViewer.Graph = graph;
        }
    }
}
