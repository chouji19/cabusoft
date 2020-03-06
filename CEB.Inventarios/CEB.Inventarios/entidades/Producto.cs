using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CEB.Inventarios.entidades
{
    class Producto
    {
        public string IdProducto { get; set; }

        public string Nombre { get; set; }

        public string Descripcion { get; set; }

        public Decimal Precio { get; set; }

        public int Existencias { get; set; }

        public string Tipo { get; set; }

        public string Marca { get; set; }

        public int IdEstado { get; set; }

        public DateTime Fecha { get; set; }

        public int Categoria { get; set; }

        public int Subcategoria { get; set; }

        public DateTime LastUpdate { get; set; }

        public string Codigo { get; set; }

        public bool Servicio { get; set; }

        public bool Activo { get; set; }

        public Decimal PrecioCompra { get; set; }

        public string RutaImagen { get; set; }

        public bool ValorVariable { get; set; }

    }
}
