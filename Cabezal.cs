using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LFyA_Proyecto2
{
    public class Cabezal
    {
        private int direccion;
        private int estado;
        private string caracter;
        private char nuevoCaracter;
        private int posicion;

        public Cabezal() {}

        public int Direccion { get => direccion; set => direccion = value; }
        public string Caracter { get => caracter; set => caracter = value; }
        public int Estado { get => estado; set => estado = value; }
        public int Posicion { get => posicion; set => posicion = value; }
        public char NuevoCaracter { get => nuevoCaracter; set => nuevoCaracter = value; }
    }
}
