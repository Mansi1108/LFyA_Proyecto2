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
        public string estadoinicial;
        public string numestados;
        public List<Transiciones> transiciones = new List<Transiciones>();

        public string[] palabra;


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


            // VERIFICAR QUE LA PALABRA ESTE EN EL ALFABETO
            for (int i = 0; i < alfabeto.Length; i++)
            {
                for (int j = 0; j < palabra.Length; j++)
                {
                    if (!alfabeto[i].Contains(palabra[j]))
                    {
                        palabra = "NO ACEPTADA".ToCharArray().Select(c => c.ToString()).ToArray(); 
                    }
                }
            }

            // LLENAR CINTA
            DataGridViewColumn columna;
            for (int i = 0; i <= palabra.Length+1; i++)
                {
                    columna = new DataGridViewColumn();
                    columna.CellTemplate = new DataGridViewTextBoxCell();
                    columna.Width = 40;
                    columna.Resizable = DataGridViewTriState.False;
                    dataGridView1.Columns.Add(columna);
                }

            dataGridView1[0, 0].Value = "_";
            for (int i = 1; i <= palabra.Length; i++)
            {
                dataGridView1[i, 0].Value = palabra[i-1].ToString();
            }
            dataGridView1[palabra.Length+1, 0].Value = "_";


            
            // GUARDAR TRANSICIONES
            Cabezal cabezal = new Cabezal();
            Transiciones transicionesi = new Transiciones();
            foreach (var pasitos in Pasos)
            {
                string[] estados = (pasitos.Split(','));
                transicionesi.estadoinicial = estados[0];
                transicionesi.caracterleido = estados[1];
                transicionesi.estadofinal = estados[2];
                transicionesi.caracteraescribir = estados[3];
                transicionesi.movimiento = estados[4];
                transiciones.Add(transicionesi);
            }


            cabezal.posicion = 0;
            bool encontrado = true;
            int contador = 0;
            bool flag = true;
            cabezal.caracter = dataGridView1[0,0].Value.ToString();
            while (encontrado)
            {
                if (contador+1 > transiciones.Count) 
                {
                    encontrado = false;
                }
                if (transiciones[contador].estadoinicial == estadoinicial && transiciones[contador].caracterleido == cabezal.caracter)
                {
                    switch (transiciones[contador].movimiento.ToUpper())
                    {
                        case "R":
                            cabezal.posicion++;
                            break;
                        case "D":
                            cabezal.posicion++;
                            break;
                        case "L":
                            cabezal.posicion--;
                            break;
                        case "I":
                            cabezal.posicion--;
                            break;
                        case "0":
                            break;
                        case "P":
                            break;
                        default:
                            break;
                    }
                    dataGridView1[cabezal.posicion, 0].Value = transiciones[contador].caracteraescribir;
                    encontrado = false;
                }
            }

        }

    }
}
