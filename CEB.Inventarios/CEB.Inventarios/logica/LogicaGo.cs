using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CEB.Inventarios.entidades;
using CEB.Inventarios.Conexion;
using System.Data;

namespace CEB.Inventarios.logica
{
    class LogicaGo
    {
        public LogicaGo() { }

        public List<ObjetoLista> obtenerListaCategorias(string filtro)
        {
            ConexionDB con = new ConexionDB();
            return con.obtenerListaCategorias(filtro);
        }

        public int insertarCategoria(string nombre) { 
            ConexionDB con = new ConexionDB();
            return con.insertarCategoria(nombre);
        }

        internal int actualizarCategoria(string nombre,string id)
        {
            ConexionDB con = new ConexionDB();
            return con.actualizarCategoria(nombre,id);
        }

        internal int eliminarCategoria(string id)
        {
            ConexionDB con = new ConexionDB();
            return con.eliminarCategoria(id);
        }

        public List<ObjetoLista> obtenerListaSubCategorias(string id)
        {
            ConexionDB con = new ConexionDB();
            return con.obtenerListaSubCategorias(id);
        }

        public int insertarSubCategoria(string categoria,string nombre)
        {
            ConexionDB con = new ConexionDB();
            return con.insertarSubCategoria(nombre,categoria);
        }

        internal int actualizarSubCategoria(string nombre, string id)
        {
            ConexionDB con = new ConexionDB();
            return con.actualizarSubCategoria(nombre, id);
        }

        internal int eliminarSubCategoria(string id)
        {
            ConexionDB con = new ConexionDB();
            return con.eliminarSubCategoria(id);
        }



        internal List<ObjetoLista> obtenerListaProductos(string filtro)
        {
            ConexionDB con = new ConexionDB();
            return con.obtenerListaProductos(filtro);
        }

        internal List<ObjetoLista> obtenerListaProductosDisponibles(string filtro)
        {
            ConexionDB con = new ConexionDB();
            return con.obtenerListaProductosDisponibles(filtro);
        }


        internal List<VentasProductos> obtenerListaProductosImagenDisponibles(string filtro)
        {
            ConexionDB con = new ConexionDB();
            return con.obtenerListaProductosImagenDisponibles(filtro);
        }

        internal int InsertarProducto(string nombre, string desripcion, string precio, decimal existencias, 
            string tipo, string marca, string estado, DateTime fecha, string subcategoria, string categoria, string codigoBarras, bool servicio,bool activo,string precioCompra, string imagen, bool valorVariable)
        {
            ConexionDB con = new ConexionDB();
            return con.InsertarProducto(nombre,desripcion,precio, existencias, tipo, marca,estado,fecha,subcategoria,categoria,codigoBarras, servicio,activo,precioCompra,imagen,valorVariable);
        }


         internal int ActualizarProducto(string id, string nombre, string desripcion, string precio, decimal existencias,
            string tipo, string marca, string estado, DateTime fecha, string subcategoria, string categoria, string codigoBarras, bool servicio, bool activo, string precioCompra, string imagen, bool valorVariable)
        {
            ConexionDB con = new ConexionDB();
            return con.ActualizarProducto(id, nombre, desripcion, precio, existencias, tipo, marca, estado, fecha, subcategoria, categoria, codigoBarras, servicio, activo, precioCompra, imagen, valorVariable);
        }

        internal Producto ObtenerProducto(string id)
        {
            ConexionDB con = new ConexionDB();
            return con.ObtenerProducto(id);
        }

       
        internal int EliminarProducto(string id)
        {
            ConexionDB con = new ConexionDB();
            return con.eliminarProducto(id);
        }

        internal Usuario IniciarSesion(string user, string pass)
        {
            ConexionDB con = new ConexionDB();
            return con.iniciarSesion(user,pass);
        }

        internal int eliminarUsuario(string id)
        {
            ConexionDB con = new ConexionDB();
            return con.eliminarUsuario(id);
        }

        internal bool verificarUsuario(string user)
        {
            ConexionDB con = new ConexionDB();
            return con.verificarUser(user);
        }

        internal List<ObjetoLista> obtenerListaUsuarios(string filtro)
        {
            ConexionDB con = new ConexionDB();
            return con.obtenerListaUsuarios(filtro);
        }

        internal int insertarUsuario(string identificacion, string nombres, string apellidos, string celular, string direccion, string pass, string rol, bool activo)
        {
            ConexionDB con = new ConexionDB();
            return con.insertarUsuario(identificacion,  nombres,  apellidos,  celular,  direccion, Base64Encode(pass),  rol,activo);
        }

        internal int actualizarUsuario(string id,string identificacion, string nombres, string apellidos, string celular, string direccion, string pass, string rol, bool activo)
        {
            ConexionDB con = new ConexionDB();
            return con.actualizarUsuario(id,identificacion, nombres, apellidos, celular, direccion, Base64Encode(pass), rol, activo);
        }




        public static string Base64Encode(string pass)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(pass);
            return System.Convert.ToBase64String(plainTextBytes);
        }

        public static string Base64Decode(string base64EncodedData)
        {
            var base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);
            return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
        }


        internal Usuario obtenerUsuario(string id)
        {
            ConexionDB con = new ConexionDB();
            return con.obtenerUsuario(id);
        }

        internal int insertarFactura(string nombre, string celular, string direccion, decimal valorFactura, string user)
        {
            ConexionDB con = new ConexionDB();
            Double iva = (double) valorFactura * (double)0.16;
            Double valor_bruto = (double) valorFactura - iva;


            return con.insertarFactura(nombre, celular, direccion, valor_bruto, iva, (double)valorFactura,user);
        }

        internal int insertarVenta(int factura, string idProducto, int cantidad, double valorUnitario, double total)
        {
            ConexionDB con = new ConexionDB();
            return con.insertarProductoVenta(factura,idProducto,cantidad,valorUnitario,total);
        }

        internal System.Data.DataTable obtenerProductosAll()
        {
            ConexionDB con = new ConexionDB();
            return con.obtenerProductosAll();
        }

        internal List<ObjetoLista> obtenerListaFacturas(string filtro)
        {
            ConexionDB con = new ConexionDB();
            return con.obtenerListaFacturas(filtro);
        }

        internal List<ObjetoLista> obtenerListaFacturas(string fechaIni,string fechaFin)
        {
            ConexionDB con = new ConexionDB();
            return con.obtenerListaFacturas(fechaIni,fechaFin);
        }

        internal Factura obtenerFactura(string id)
        {
            ConexionDB con = new ConexionDB();
            return con.obtenerFactura(id);
        }


        internal DataTable obteverProductosVenta(string id)
        {
            ConexionDB con = new ConexionDB();
            return con.obteverProductosVenta(id);
        }


        internal List<ObjetoLista> obtenerListaGastos(string filtro)
        {
            ConexionDB con = new ConexionDB();
            return con.obtenerListaGastos(filtro);
        }

        internal List<ObjetoLista> obtenerListaGastos(string fechaIni, string fechaFin)
        {
            ConexionDB con = new ConexionDB();
            return con.obtenerListaGastos(fechaIni, fechaFin);
        }



        internal int insertarGasto(string concepto, decimal cantidad, string valor, string total, string fecha)
        {
            ConexionDB con = new ConexionDB();
            return con.insertarCompra(concepto, cantidad, valor,total, fecha);
        }

        internal Gasto obtenerGasto(string id)
        {
            ConexionDB con = new ConexionDB();
            return con.obtenerGasto(id);
        }

        internal int eliminarGasto(string id)
        {
            ConexionDB con = new ConexionDB();
            return con.eliminarGasto(id);
        }

        internal DataTable obtenerIngresosGastos(string fechaIni, string fechaFin)
        {
            ConexionDB con = new ConexionDB();
            return con.obtenerIngresosGastos(fechaIni, fechaFin);
        }

        //obtenerProductoByCodigo
        internal Producto obtenerProductoByCodigo(string codigo)
        {
            ConexionDB con = new ConexionDB();
            return con.ObtenerProductoByCodigo(codigo);
        }


        internal DataTable obtenerMovimientos(string fecha, string concepto, string tipo, double valor)
        {
            ConexionDB con = new ConexionDB();
            return con.obtenerMovimientos(fecha,concepto,tipo,valor);
        }

        internal int insertarMovimiento(string fecha, string concepto, string tipo, double valor)
        {
            ConexionDB con = new ConexionDB();
            return con.insertarMovimiento(fecha, concepto, tipo, valor);
        }

        internal int insertarMovimientoProductos(string fecha, string descripcion, string idProducto,
            int cantidad, double valor, double total)
        {
            ConexionDB con = new ConexionDB();
            return con.insertarMovimientoProductos(fecha, descripcion, idProducto, cantidad, valor, total);
        }
    }
}
