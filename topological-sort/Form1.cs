using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace topological_sort
{
    public partial class Form1 : Form
    {
        private Graph g1 = new Graph(25);

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.InitialDirectory = @"C:\";
            openFileDialog1.Title = "Browse Input File";
            openFileDialog1.CheckFileExists = true;
            openFileDialog1.CheckPathExists = true;
            openFileDialog1.DefaultExt = "txt";
            openFileDialog1.Filter = "Text files (*.txt)|*.txt";
            openFileDialog1.FilterIndex = 2;
            openFileDialog1.RestoreDirectory = true;
            openFileDialog1.ReadOnlyChecked = true;
            openFileDialog1.ShowReadOnly = true;
            if (openFileDialog1.ShowDialog().ToString().Equals("OK"))
            {
                textBox1.Text = openFileDialog1.FileName;
            }
            
            try
            {
                listBox1.Items.Clear();
                using (StreamReader sr = new StreamReader(openFileDialog1.FileName))
                {
                    string line;
                    List<string> g1vertex;
                    while ((line = sr.ReadLine()) != null)
                    {
                        listBox1.Items.Add(line);
                        g1vertex = line.Split(',').ToList<string>();
                        foreach (string value in g1vertex)
                        {
                            if (g1.GetVertexIndex(value) == -1)
                            {
                                g1.AddVertex(value);
                            }
                        }
                        foreach (string value in g1vertex)
                        {
                            Console.WriteLine(value);
                            if (value != g1vertex[0])
                            {
                                g1.AddEdge(value, g1vertex[0]);
                            }
                        }
                    }
                }
                g1.Display();
                TopologicalSort ts = new TopologicalSort(g1);
                ts.BFS();
                int i = 1;
                foreach(string value in ts.GetResult())
                {
                    listBox1.Items.Add("Semester "+i+": "+value);
                    i++;
                }
            } catch
            {
                textBox1.Text = "The file could not be read!";
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            string[] lines = System.IO.File.ReadAllLines("BFS.dat");
           
            Microsoft.Msagl.GraphViewerGdi.GViewer viewer = new Microsoft.Msagl.GraphViewerGdi.GViewer();
            Microsoft.Msagl.Drawing.Graph graph = new Microsoft.Msagl.Drawing.Graph("graph");

            //create the graph content 
            for (int i = 0; i < g1.GetGraphSize(); ++i)
            {
                string vertex = g1.GetVertex(i).data;
                List<string> neighbours = g1.GetNeighbor(vertex);

                foreach (var neighbour in neighbours)
                {
                    graph.AddEdge(vertex, neighbour);
                }
                graph.FindNode(vertex).LabelText = vertex + " 0";
                graph.FindNode(vertex).Attr.Shape = Microsoft.Msagl.Drawing.Shape.Diamond;
            }
            //bind the graph to the viewer 
            viewer.Graph = graph;
            viewer.Dock = System.Windows.Forms.DockStyle.Fill;

            System.Windows.Forms.Form form = new System.Windows.Forms.Form();
            form.SuspendLayout();
            form.Controls.Add(viewer);
            form.ResumeLayout();
            form.ShowDialog();

            int turn = 0;
            int counter = 0;
            string prevVertex = "";
            bool finish = false;

            const int vertexIndexLength = 2;
            int vertexIndex = 0 - vertexIndexLength;

            int[] vc = new int[g1.GetGraphSize()];
            for (int i = 0; i < g1.GetGraphSize(); ++i)
            {
                vc[i] = 0;
            }

            while (!finish)
            {
                if ((turn % 2) == 0) {
                    vertexIndex += vertexIndexLength;
                    prevVertex = lines[vertexIndex];
                    graph.FindNode(lines[vertexIndex]).Attr.FillColor = Microsoft.Msagl.Drawing.Color.Green;
                    graph.FindNode(lines[vertexIndex]).LabelText = lines[vertexIndex] + " " + vc[g1.GetVertexIndex(prevVertex)].ToString();
                    counter++;
                } else
                {
                    List<string> neighbours = lines[vertexIndex + 1].Split(',').ToList<string>();

                    for (int i = 0; i < neighbours.Count-1; ++i)
                    {
                        int c = vc[g1.GetVertexIndex(neighbours[i])];
                        graph.FindNode(neighbours[i]).Attr.FillColor = Microsoft.Msagl.Drawing.Color.Red;
                        graph.FindNode(neighbours[i]).LabelText = neighbours[i] + " " + (c + 1).ToString();
                        vc[g1.GetVertexIndex(neighbours[i])]++;
                    }
                }
                if (counter == g1.GetGraphSize())
                {
                    finish = true;
                } else
                {
                    form.Controls.Add(viewer);
                    form.ResumeLayout();
                    form.ShowDialog();
                    turn++;
                }
            }
            vertexIndex += vertexIndexLength;
            Console.WriteLine(lines[vertexIndex]);
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        /*
            //create a form 
            System.Windows.Forms.Form form = new System.Windows.Forms.Form();
            //create a viewer object 
            Microsoft.Msagl.GraphViewerGdi.GViewer viewer = new Microsoft.Msagl.GraphViewerGdi.GViewer();
            //create a graph object 
            Microsoft.Msagl.Drawing.Graph graph = new Microsoft.Msagl.Drawing.Graph("graph");
            //create the graph content 
            graph.AddEdge("A", "B");
            graph.AddEdge("B", "C");
            graph.AddEdge("A", "C").Attr.Color = Microsoft.Msagl.Drawing.Color.Green;
            graph.FindNode("A").Attr.FillColor = Microsoft.Msagl.Drawing.Color.Magenta;
            graph.FindNode("B").Attr.FillColor = Microsoft.Msagl.Drawing.Color.MistyRose;
            Microsoft.Msagl.Drawing.Node c = graph.FindNode("C");
            c.Attr.FillColor = Microsoft.Msagl.Drawing.Color.PaleGreen;
            c.Attr.Shape = Microsoft.Msagl.Drawing.Shape.Diamond;
            //bind the graph to the viewer 
            viewer.Graph = graph;
            //associate the viewer with the form 
            form.SuspendLayout();
            viewer.Dock = System.Windows.Forms.DockStyle.Fill;
            form.Controls.Add(viewer);
            form.ResumeLayout();
            //show the form 
            form.ShowDialog();
            graph.AddEdge("A", "C").Attr.Color = Microsoft.Msagl.Drawing.Color.Magenta;
            form.Controls.Add(viewer);
            form.ResumeLayout();
            //show the form 
            form.ShowDialog();
            */
    }
}
