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
        public List<string> Pasos;
        public string[] alfabeto;
        string estadoinicial;
        string numestados;

        string[] palabra;


        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog
            {
                DefaultExt = "txt",
                Filter = "txt files (*.txt)|*.txt",
            };
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string ruta = openFileDialog1.FileName;
                string informacion;
                string[] instrucciones;
                using (StreamReader sr = new StreamReader(ruta)) {
                    informacion = sr.ReadToEnd();
                    instrucciones = informacion.Split('\n');
                }
                numestados=instrucciones[0];
                estadoinicial=instrucciones[1];
                alfabeto = instrucciones[2].Split(',');
                Pasos = new List<string>();
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

        private void button3_Click(object sender, EventArgs e)
        {
            // CONVERTIR PALABRA EN ARREGLO
            palabra = textBox1.Text.ToCharArray().Select(c => c.ToString()).ToArray();


            // LLENAR CINTA
            DataGridViewColumn columna;

                for (int i = 0; i <= palabra.Length; i++)
                {
                    columna = new DataGridViewColumn();
                    columna.CellTemplate = new DataGridViewTextBoxCell();
                    columna.Width = 40;
                    columna.Resizable = DataGridViewTriState.False;
                    dataGridView1.Columns.Add(columna);
                }

            for (int i = 0; i < palabra.Length; i++)
            {
                dataGridView1[i, 0].Value = palabra[i].ToString();
            }

            // VERIFICAR QUE LA PALABRA ESTE EN EL ALFABETO
            for (int i = 0; i < alfabeto.Length; i++)
            {
                for (int j = 0; j < palabra.Length; j++)
                {
                    if (!alfabeto[i].Contains(palabra[j]))
                    {
                        textBox2.Text = "NO ACEPTADA";
                    }
                }
            }



        }

    }
}
