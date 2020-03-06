using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CEB.Inventarios.entidades
{
    class Factura
    {
        private string idFactura;

        public string IdFactura
        {
            get { return idFactura; }
            set { idFactura = value; }
        }
        private string nombre;

        public string Nombre
        {
            get { return nombre; }
            set { nombre = value; }
        }
        private string celular;

        public string Celular
        {
            get { return celular; }
            set { celular = value; }
        }
        private string direccion;

        public string Direccion
        {
            get { return direccion; }
            set { direccion = value; }
        }
        private string fecha;

        public string Fecha
        {
            get { return fecha; }
            set { fecha = value; }
        }
        private decimal valorBruto;

        public decimal ValorBruto
        {
            get { return valorBruto; }
            set { valorBruto = value; }
        }
        private decimal iva;

        public decimal Iva
        {
            get { return iva; }
            set { iva = value; }
        }
        private decimal valorTotal;

        public decimal ValorTotal
        {
            get { return valorTotal; }
            set { valorTotal = value; }
        }

        private string usuario;

        public string Usuario
        {
            get { return usuario; }
            set { usuario = value; }
        }

    }
}
