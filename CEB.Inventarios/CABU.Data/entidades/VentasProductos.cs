using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CABU.Data.entidades
{
    public class VentasProductos
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

        private string rutaImagen;

        public string RutaImagen
        {
            get { return rutaImagen; }
            set { rutaImagen = value; }
        }

    }
}
