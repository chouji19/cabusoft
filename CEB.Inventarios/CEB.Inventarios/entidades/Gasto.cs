using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CEB.Inventarios.entidades
{
    class Gasto
    {
        private string idGasto;

        public string IdGasto
        {
            get { return idGasto; }
            set { idGasto = value; }
        }
        private string concepto;

        public string Concepto
        {
            get { return concepto; }
            set { concepto = value; }
        }
        private int cantidad;

        public int Cantidad
        {
            get { return cantidad; }
            set { cantidad = value; }
        }
        private string valorCompra;

        public string ValorCompra
        {
            get { return valorCompra; }
            set { valorCompra = value; }
        }
        private string fecha;

        public string Fecha
        {
            get { return fecha; }
            set { fecha = value; }
        }

        private string valorTotal;

        public string ValorTotal
        {
            get { return valorTotal; }
            set { valorTotal = value; }
        }

    }
}
