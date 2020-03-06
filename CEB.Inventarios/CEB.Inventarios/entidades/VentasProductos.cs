using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CEB.Inventarios.entidades
{
    class VentasProductos
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
