using CABU.Data.entidades;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CABU.Data.Logica
{
    public class AdminProductos
    {

        private CABUConection context;

        public CABUConection DbContext
        {
            get { return context ?? (context = new CABUConection()); }
        }

        public  tblProductos obtenerProductoById(int id)
        {
            var producto = DbContext.tblProductos.FirstOrDefault(p=>p.id_producto ==  id);
            return producto ?? null;
        }

        public tblProductos obtenerProductoByCodigo(string codigo)
        {
            var producto = DbContext.tblProductos.FirstOrDefault(p => p.codigo.Contains(codigo));
            return producto ?? null;
        }


        public object obtenerListaSubCategorias(string categoria)
        {
            int cat = Convert.ToInt32(categoria);
            var query = from p in DbContext.tblSubCategorias
                        where p.id_categoria == cat select p;
            return query.ToList();
        }

        public int insertarProducto(string nombre, string desripcion, string precio, decimal existencias,
            string tipo, string marca, string estado, DateTime fecha, string subcategoria, string categoria, 
            string codigoBarras, bool servicio, bool activo, string precioCompra, string imagen, int idSucursal)
        {
            tblProductos producto = new tblProductos();
            producto.nombre = nombre;
            producto.descripcion = desripcion;
            producto.precio = Convert.ToDecimal(precio);
            producto.existencias = Convert.ToInt32(existencias);
            producto.marca = marca;
            producto.id_estado = Convert.ToInt32(estado);
            producto.fecha_ingreso = fecha;
            producto.id_subcategoria = Convert.ToInt32(subcategoria);
            producto.id_categoria = Convert.ToInt32(categoria);
            producto.codigo = codigoBarras;
            producto.servicio = servicio;
            producto.activo = activo;
            producto.valor_compra = Convert.ToDecimal(precioCompra);
            producto.id_sucursal = idSucursal;
            producto.imagen = !String.IsNullOrEmpty(imagen) ? imagen : "Images/default_product.png";
            DbContext.tblProductos.Add(producto);
            return DbContext.SaveChanges();
        }

        public int actualizarProducto(int idProducto, string nombre, string desripcion, string precio, decimal existencias,
            string tipo, string marca, string estado, DateTime fecha, string subcategoria, string categoria,
            string codigoBarras, bool servicio, bool activo, string precioCompra, string imagen)
        {
            tblProductos producto = DbContext.tblProductos.FirstOrDefault(p=>p.id_producto == idProducto);
            if (producto != null)
            {
                producto.nombre = nombre;
                producto.descripcion = desripcion;
                producto.precio = Convert.ToDecimal(precio);
                producto.existencias = Convert.ToInt32(existencias);
                producto.marca = marca;
                producto.id_estado = Convert.ToInt32(estado);
                producto.fecha_ingreso = fecha;
                producto.id_subcategoria = Convert.ToInt32(subcategoria);
                producto.id_categoria = Convert.ToInt32(categoria);
                producto.codigo = codigoBarras;
                producto.servicio = servicio;
                producto.activo = activo;
                producto.valor_compra = Convert.ToDecimal(precioCompra);
                producto.last_update = DateTime.Now;
                producto.imagen = !String.IsNullOrEmpty(imagen) ? imagen : "Images/default_product.png";
            }
            return DbContext.SaveChanges();
        }


        public int eliminarProducto(int idProducto)
        {
            tblProductos producto = new tblProductos();
            producto = DbContext.tblProductos.FirstOrDefault(p => p.id_producto == idProducto);
            if (producto != null)
            {
                DbContext.tblProductos.Remove(producto);
            }
            return DbContext.SaveChanges();
        }

        public int insertarFactura(string nombre, string celular, string direccion, decimal valorFactura, string usuario, int idSucursal)
        {
            Double iva = (double)valorFactura * (double)0.16;
            Double valor_bruto = (double)valorFactura - iva;
            tblFacturas factura = new tblFacturas();
            factura.celular = celular;
            factura.nombre = nombre;
            factura.direccion = direccion;
            factura.usuario = usuario;
            factura.valor_bruto = valor_bruto;
            factura.valor_neto = (double)valorFactura;
            factura.iva = iva;
            factura.fecha = DateTime.Now;
            factura.id_sucursal = idSucursal;
            DbContext.tblFacturas.Add(factura);
            int res = DbContext.SaveChanges();
            if (res > 0)
            {
                DbContext.Entry(factura).GetDatabaseValues();
                return factura.id_factura;
            }
            return 0;
        }

        public void insertarVenta(int factura, string idProducto, int cantidad, double valorUnitario, double total)
        {
            tblVentas venta = new tblVentas();
            venta.cantidad = cantidad;
            venta.fecha = DateTime.Now;
            venta.id_factura = factura;
            venta.id_producto = Convert.ToInt32(idProducto);
            venta.valor_total = total;
            venta.valor_unitario = valorUnitario;
            DbContext.tblVentas.Add(venta);
            int res = DbContext.SaveChanges();
            if (res > 0)
            {
                tblProductos producto = new tblProductos();
                int pro = Convert.ToInt32(idProducto);
                producto = DbContext.tblProductos.FirstOrDefault(p => p.id_producto == pro);
                if (producto != null)
                {
                    if(!producto.servicio.Value)
                        producto.existencias = producto.existencias - cantidad;
                    DbContext.SaveChanges();
                }
            }
        }

        public List<VentasProductos> obtenerListaProductosImagenDisponibles(string filtro)
        {
            //List<VentasProductos> 
               var query = (from p in DbContext.tblProductos
                                            where p.nombre.Contains(filtro) || p.precio.ToString().Contains(filtro)
                                            || p.codigo.Contains(filtro) || p.descripcion.Contains(filtro)
                                            || p.marca.Contains(filtro) || p.tblCategorias.nombre.Contains(filtro)
                                            select p);
            List<VentasProductos> ventas = new List<VentasProductos>();
            foreach (var item in query)
            {
                VentasProductos venta = new VentasProductos();
                venta.IdProducto = item.id_producto.ToString();
                venta.Nombre = item.nombre;
                venta.RutaImagen = item.imagen;
                ventas.Add(venta);
            }
            return ventas;
        }

        public List<tblProductos> obtenerProductosAll(string categoria, string subcategoria, int idSucursal)
        {
            List<tblProductos> lista = null;
            if (categoria == "")
                lista = (from p in DbContext.tblProductos
                         where p.servicio == false && p.activo == true
                         && p.existencias > 0 && p.id_sucursal == idSucursal
                         select p).ToList();
            else
            {
                if (subcategoria == "")
                {
                    int cat = Convert.ToInt32(categoria);
                    lista = (from p in DbContext.tblProductos
                             where p.servicio == false && p.activo == true
                             && p.existencias > 0 && p.id_categoria == cat
                              && p.id_sucursal == idSucursal
                             select p).ToList();
                }
                else
                {
                    int cat = Convert.ToInt32(categoria);
                    int sub = Convert.ToInt32(subcategoria);
                    lista = (from p in DbContext.tblProductos
                             where p.servicio == false && p.activo == true
                             && p.existencias > 0 && p.id_categoria == cat
                             && p.id_subcategoria == sub && p.id_sucursal == idSucursal
                             select p).ToList();
                }
            }
            return lista;
        }

        public int insertarMovimientoProductos(string fecha, string descripcion, string idProducto,
            int cantidad, double valor, double total)
        {
            DateTime fechaMvt = DateTime.ParseExact(fecha, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None);
            tblMovimientoProductos mov = new tblMovimientoProductos();
            mov.cantidad = cantidad;
            mov.descripcion = descripcion;
            mov.fecha = fechaMvt;
            mov.fecha_registro = DateTime.Now;
            mov.id_producto = Convert.ToInt32(idProducto);
            mov.total = Convert.ToDecimal(total);
            mov.valor = Convert.ToDecimal(valor);
            DbContext.tblMovimientoProductos.Add(mov);
            int resultado = DbContext.SaveChanges();
            int idPro = Convert.ToInt32(idProducto);
            tblProductos pro = DbContext.tblProductos.FirstOrDefault(p=>p.id_producto == idPro);
            if (pro != null && !pro.servicio.Value)
            {
                pro.existencias = pro.existencias - cantidad;
                resultado = DbContext.SaveChanges();
            }
            return resultado;
        }
    }
}
