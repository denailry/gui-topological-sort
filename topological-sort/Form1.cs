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
                using (StreamReader sr = new StreamReader(openFileDialog1.FileName))
                {
                    string line; //Nyobain baca isi file, ntar yang keluar baris terakhir
                    while ((line = sr.ReadLine()) != null)
                    {
                        textBox2.Text = line;
                    }
                }
            }
            catch (Exception ex)
            {
                textBox2.Text = "The file could not be read:";
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
