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
            //Bagian ini tinggal dimodif buat baca file
            try
            {
                listBox1.Items.Clear();
                Graph g1 = new Graph(25);
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
                ts.DFS();
                int i = 1;
                foreach(string value in ts.GetResult())
                {
                    listBox1.Items.Add("Semester "+i+": "+value);
                    i++;
                }
            }
            catch (Exception ex)
            {
                textBox1.Text = "The file could not be read:";
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
    }
}
