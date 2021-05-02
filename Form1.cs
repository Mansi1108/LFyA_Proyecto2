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

namespace LFyA_Proyecto2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string ruta = openFileDialog1.FileName;
                string informacion;
                string[] instrucciones;
                using (StreamReader sr = new StreamReader(ruta)) {
                    informacion = sr.ReadToEnd();
                    instrucciones = informacion.Split('\n');
                }
                string numestados=instrucciones[0];
                string estadoinicial=instrucciones[1];
                string[] alfabeto = instrucciones[2].Split(',');
                List<string> Pasos = new List<string>();
                for (int i = 3; i < instrucciones.Length-1; i++)
                {
                    Pasos.Add(instrucciones[i]);
                }
                string final = instrucciones[instrucciones.Length - 1];
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }
    }
}
