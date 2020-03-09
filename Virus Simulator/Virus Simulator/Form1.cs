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

            Program.firstInfectedCity = firstInfectedCity;
            Program.Country = Country;

            graphViewer.Graph = graph;
        }

        private void ResetInfection() {
            foreach (var node in graphViewer.Graph.Nodes) {
                node.Attr.Color = Microsoft.Msagl.Drawing.Color.Black;
            }
            foreach (var edge in graphViewer.Graph.Edges) {
                edge.Attr.Color = Microsoft.Msagl.Drawing.Color.Black;
            }
        }

        private void Infect(int t) {
            Graph<City>.AdjacentNodes<City>[] infectionPath = Algo.BFS(Program.Country, Program.Country.FindNodeIndex(n => n.item.name == Program.firstInfectedCity.name), t);
            infectionList.Text = "";
            if (t >= 0)
            {
                Program.DrawInfected(graphViewer.Graph, Program.firstInfectedCity.name);
            }
            foreach (var infection in infectionPath)
            {
                Program.DrawInfection(graphViewer.Graph, infection.first.name, infection.second.name);
                infectionList.Text += infection.first.name + " => " + infection.second.name + "\n";
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void runButton_Click(object sender, EventArgs e) {
            graphViewer.SuspendLayout();
            ResetInfection();
            Infect((int)inputField.Value);
            graphViewer.Pan(1, 0);
            graphViewer.Pan(-1, 0);
            graphViewer.ResumeLayout();
        }
    }
}
