using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CABU.Data.entidades
{
    public class ProductoVenta
    {
        private string idProducto;

        public string IdProducto
        {
            get { return idProducto; }
            set { idProducto = value; }
        }
        private string nombre;

        public string Nombre
        {
            get { return nombre; }
            set { nombre = value; }
        }
        private decimal cantidad;

        public decimal Cantidad
        {
            get { return cantidad; }
            set { cantidad = value; }
        }
        private decimal valor;

        public decimal Valor
        {
            get { return valor; }
            set { valor = value; }
        }
        private decimal total;

        public decimal Total
        {
            get { return total; }
            set { total = value; }
        }

        private int maximoUnidades;

        public int MaximoUnidades
        {
            get { return maximoUnidades; }
            set { maximoUnidades = value; }
        }
    }
}
