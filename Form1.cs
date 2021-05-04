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
        private List<string> Pasos;
        private string[] alfabeto;
        private string estadoinicial;
        private string numestados;
        private List<Transiciones> transiciones = new List<Transiciones>();
        private Cabezal cabezal = new Cabezal();
        private string[] palabra;

        private void button1_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string ruta = openFileDialog1.FileName;
                string informacion;
                string[] instrucciones;
                using (StreamReader sr = new StreamReader(ruta)) {
                    informacion = sr.ReadToEnd();
                    instrucciones = informacion.Split('\n');
                }
                numestados=instrucciones[0].Replace("\r","");
                estadoinicial=instrucciones[1].Replace("\r", "");
                alfabeto = instrucciones[2].Split(',');
                Pasos = new List<string>();
                for (int i = 3; i < instrucciones.Length; i++)
                {
                    Pasos.Add(instrucciones[i].Replace("\r", ""));
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

            bool aceptada = true;
            // VERIFICAR QUE LA PALABRA ESTE EN EL ALFABETO
            for (int i = 0; i < alfabeto.Length; i++)
            {
                for (int j = 0; j < palabra.Length; j++)
                {
                    if (!alfabeto[i].Contains(palabra[j]))
                    {
                        palabra = "NO ACEPTADA".ToCharArray().Select(c => c.ToString()).ToArray();
                        aceptada = false;
                        i = alfabeto.Length;
                        j = palabra.Length;
                    }
                }
            }

            DataGridViewColumn columna;
            if (aceptada)
            {
                // LLENAR CINTA               
                for (int i = 0; i <= palabra.Length + 5; i++)
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
                    dataGridView1[i, 0].Value = palabra[i - 1].ToString();
                }
                for (int i = palabra.Length+1; i <= palabra.Length+5; i++)
                {
                    dataGridView1[i, 0].Value = "_";
                }
                


                // GUARDAR TRANSICIONES
                

                foreach (var pasitos in Pasos)
                {
                    Transiciones transicionesi = new Transiciones();
                    string[] estados = (pasitos.Split(','));
                    transicionesi.estadoinicial = estados[0];
                    transicionesi.caracterleido = estados[1];
                    transicionesi.estadofinal = estados[2];
                    transicionesi.caracteraescribir = estados[3];
                    transicionesi.movimiento = estados[4];
                    transiciones.Add(transicionesi);
                }

                cabezal.posicion = 0;
                //dataGridView1.Rows[cabezal.posicion].Cells[0].Style.BackColor = Color.Yellow;
                cabezal.caracter = dataGridView1[0, 0].Value.ToString();
                cabezal.estadoD = estadoinicial;
                Recorrido(cabezal, true);
            }
            else
            {
                for (int i = 0; i <= palabra.Length + 1; i++)
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
                    dataGridView1[i, 0].Value = palabra[i - 1].ToString();
                }
                dataGridView1[palabra.Length + 1, 0].Value = "_";
            }


        }
        


        public void Recorrido (Cabezal cabezal, bool flag2)
        {
            int posanterior = 0;
            int contadorenciclado = 0;
            while (flag2)
            {
                
                cabezal.caracter = dataGridView1[cabezal.posicion, 0].Value.ToString();
                    for (int contador = 0; contador < transiciones.Count ; contador++)
                    {
                     
                    if (cabezal.estadoD == transiciones[contador].estadoinicial && transiciones[contador].caracterleido == cabezal.caracter)
                    {

                        posanterior = cabezal.posicion;
                        dataGridView1.Columns[posanterior].DefaultCellStyle.BackColor = Color.White;
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
                                flag2 = false;
                                break;
                            default:
                                break;
                        }

                        cabezal.estadoD = transiciones[contador].estadofinal;
                        cabezal.caracter = dataGridView1[cabezal.posicion, 0].Value.ToString();

                        dataGridView1.Columns[posanterior].DefaultCellStyle.BackColor = Color.White;
                        dataGridView1.Columns[cabezal.posicion].DefaultCellStyle.BackColor = Color.Yellow;

                        dataGridView1[posanterior, 0].Value = transiciones[contador].caracteraescribir.ToString();
                        textBox3.Text = cabezal.estadoD;

                    }
                    else {
                        contadorenciclado++;
                        if (contadorenciclado==500)
                        {
                            MessageBox.Show("La misma instruccion se ha realizado 500 veces la maquina se ha detenido revise la maquina");
                            textBox2.Text = "Error";
                            flag2 = false;
                        }
                    }

                    
                    }
                     
                
       
                if (cabezal.posicion == dataGridView1.Columns.Count-1)
                {
                    flag2 = false;
                }

            }
           

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }
        private int paso;

        public void RecorridoPaso(Cabezal cabezal)
        {
            cabezal.caracter = dataGridView1[cabezal.posicion, 0].Value.ToString();
            for (int contador = 0; contador < transiciones.Count; contador++)
            {

                if (cabezal.estadoD == transiciones[contador].estadoinicial)
                {
                    if (transiciones[contador].caracterleido == cabezal.caracter)
                    {
                        paso = cabezal.posicion;
                        //dataGridView1.Columns[paso].DefaultCellStyle.BackColor = Color.Yellow;
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

                        cabezal.estadoD = transiciones[contador].estadofinal;
                        cabezal.caracter = dataGridView1[cabezal.posicion, 0].Value.ToString();

                        dataGridView1.Columns[paso].DefaultCellStyle.BackColor = Color.White;
                        dataGridView1.Columns[cabezal.posicion].DefaultCellStyle.BackColor = Color.Yellow;

                        dataGridView1[paso, 0].Value = transiciones[contador].caracteraescribir.ToString();
                        textBox3.Text = cabezal.estadoD;
                    }
                }


            }

        }
        private bool PrimeraVuelta = true;
        private void button2_Click(object sender, EventArgs e)
        {
            if (PrimeraVuelta)
            {
                PrimeraVuelta = false;
                // CONVERTIR PALABRA EN ARREGLO
                palabra = textBox1.Text.ToCharArray().Select(c => c.ToString()).ToArray();

                bool aceptada = true;
                // VERIFICAR QUE LA PALABRA ESTE EN EL ALFABETO
                for (int i = 0; i < alfabeto.Length; i++)
                {
                    for (int j = 0; j < palabra.Length; j++)
                    {
                        if (!alfabeto[i].Contains(palabra[j]))
                        {
                            palabra = "NO ACEPTADA".ToCharArray().Select(c => c.ToString()).ToArray();
                            aceptada = false;
                            i = alfabeto.Length;
                            j = palabra.Length;
                        }
                    }
                }

                DataGridViewColumn columna;
                if (aceptada)
                {
                    // LLENAR CINTA               
                    for (int i = 0; i <= palabra.Length + 1; i++)
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
                        dataGridView1[i, 0].Value = palabra[i - 1].ToString();
                    }
                    dataGridView1[palabra.Length + 1, 0].Value = "_";


                    // GUARDAR TRANSICIONES


                    foreach (var pasitos in Pasos)
                    {
                        Transiciones transicionesi = new Transiciones();
                        string[] estados = (pasitos.Split(','));
                        transicionesi.estadoinicial = estados[0];
                        transicionesi.caracterleido = estados[1];
                        transicionesi.estadofinal = estados[2];
                        transicionesi.caracteraescribir = estados[3];
                        transicionesi.movimiento = estados[4];
                        transiciones.Add(transicionesi);
                    }

                    cabezal.posicion = 0;
                    cabezal.caracter = dataGridView1[0, 0].Value.ToString();
                    cabezal.estadoD = estadoinicial;
                    RecorridoPaso(cabezal);
                    //dataGridView1.Columns[paso].DefaultCellStyle.BackColor = Color.Yellow;

                }
                else
                {
                    for (int i = 0; i <= palabra.Length + 1; i++)
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
                        dataGridView1[i, 0].Value = palabra[i - 1].ToString();
                    }
                    dataGridView1[palabra.Length + 1, 0].Value = "_";
                }
            }
            else {
                DataGridViewColumn columna;
                columna = new DataGridViewColumn();
                columna.CellTemplate = new DataGridViewTextBoxCell();
                columna.Width = 40;
                columna.Resizable = DataGridViewTriState.False;
                dataGridView1.Columns.Add(columna);
                dataGridView1[dataGridView1.ColumnCount - 1, 0].Value = "_";
                RecorridoPaso(cabezal);
            }
        }
    }
}
