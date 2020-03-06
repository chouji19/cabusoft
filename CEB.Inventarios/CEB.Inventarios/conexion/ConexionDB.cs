using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using CEB.Inventarios.entidades;

namespace CEB.Inventarios.Conexion
{
    class ConexionDB
    {

        private string connString;
        private SqlDataAdapter dataAdapter;

        public string ConnString
        {
            get { return connString; }
            set { connString = value; }
        }

        public SqlDataAdapter DataAdapter
        {
            get { return dataAdapter; }
            set { dataAdapter = value; }
        }


        public ConexionDB()
        {

            connString = System.Configuration.ConfigurationManager.ConnectionStrings["dbgo"].ConnectionString;
        }

        public List<ObjetoLista> obtenerListaCategorias(string filtro)
        {

            SqlConnection conn = new SqlConnection(connString);
            conn.Open();
            using (SqlCommand cmd = new SqlCommand(String.Format("select * from tblCategorias where nombre like '%{0}%' order by nombre", filtro), conn))
            {
                cmd.CommandType = CommandType.Text;
                using (SqlDataReader r = cmd.ExecuteReader())
                {
                    List<ObjetoLista> entidades = new List<ObjetoLista>();
                    while (r.Read())
                    {
                        entidades.Add(new ObjetoLista()
                        {
                            Id = (int)r[0]
                            ,
                            Nombre = r[1].ToString().TrimEnd()
                        });
                    }
                    conn.Close();
                    return entidades;
                }
            }
        }

        internal int insertarCategoria(string nombre)
        {
            SqlConnection conn = new SqlConnection(connString);
            conn.Open();
            int n;
            using (SqlCommand command = conn.CreateCommand())
            {
                command.CommandText = string.Format("INSERT INTO dbo.tblCategorias (nombre) VALUES ('{0}');", nombre);
                n = command.ExecuteNonQuery();

            }
            conn.Close();
            return n;
        }

        internal int actualizarCategoria(string nombre, string id)
        {
            SqlConnection conn = new SqlConnection(connString);
            conn.Open();
            int n;
            using (SqlCommand command = conn.CreateCommand())
            {
                if (id == "")
                    return 0;
                command.CommandText = string.Format("update dbo.tblCategorias set  nombre = '{0}' where id_categoria = {1};", nombre, id);
                n = command.ExecuteNonQuery();

            }
            conn.Close();
            return n;
        }


        internal int eliminarCategoria(string id)
        {
            SqlConnection conn = new SqlConnection(connString);
            conn.Open();
            int n;
            using (SqlCommand command = conn.CreateCommand())
            {
                if (id == "")
                    return 0;
                command.CommandText = string.Format("delete dbo.tblCategorias where id_categoria = {0};", id);
                n = command.ExecuteNonQuery();

            }
            conn.Close();
            return n;
        }

        internal int eliminarUsuario(string id)
        {
            SqlConnection conn = new SqlConnection(connString);
            conn.Open();
            int n;
            using (SqlCommand command = conn.CreateCommand())
            {
                if (id == "")
                    return 0;
                command.CommandText = string.Format("delete dbo.tblUsuarios where id_usuario = {0};", id);
                n = command.ExecuteNonQuery();

            }
            conn.Close();
            return n;
        }

        public List<ObjetoLista> obtenerListaSubCategorias(string id)
        {

            SqlConnection conn = new SqlConnection(connString);
            conn.Open();

            long number1 = 0;
            bool canConvert = long.TryParse(id, out number1);
            if (canConvert)
            {

                using (SqlCommand cmd = new SqlCommand(String.Format("select * from tblsubCategorias where id_categoria = {0} order by nombre", id), conn))
                {
                    cmd.CommandType = CommandType.Text;
                    using (SqlDataReader r = cmd.ExecuteReader())
                    {
                        List<ObjetoLista> entidades = new List<ObjetoLista>();
                        while (r.Read())
                        {
                            entidades.Add(new ObjetoLista()
                            {
                                Id = (int)r[0]
                                ,
                                Nombre = r[1].ToString().TrimEnd()
                            });
                        }
                        conn.Close();
                        return entidades;
                    }
                }
            }
            return null;
        }

        internal int insertarSubCategoria(string nombre, string categoria)
        {
            SqlConnection conn = new SqlConnection(connString);
            conn.Open();
            int n;
            using (SqlCommand command = conn.CreateCommand())
            {
                if (categoria == "")
                    return 0;
                command.CommandText = string.Format("INSERT INTO dbo.tblSubCategorias (nombre,id_categoria) VALUES ('{0}',{1});", nombre, categoria);
                n = command.ExecuteNonQuery();

            }
            conn.Close();
            return n;
        }

        internal int actualizarSubCategoria(string nombre, string id)
        {
            SqlConnection conn = new SqlConnection(connString);
            conn.Open();
            int n;
            using (SqlCommand command = conn.CreateCommand())
            {
                if (id == "")
                    return 0;
                command.CommandText = string.Format("update dbo.tblSubCategorias set  nombre = '{0}' where id_subcategoria = {1};", nombre, id);
                n = command.ExecuteNonQuery();

            }
            conn.Close();
            return n;
        }

        internal int eliminarSubCategoria(string id)
        {
            SqlConnection conn = new SqlConnection(connString);
            conn.Open();
            int n;
            using (SqlCommand command = conn.CreateCommand())
            {
                if (id == "")
                    return 0;
                command.CommandText = string.Format("delete dbo.tblSubCategorias where id_subCategoria = {0};", id);
                n = command.ExecuteNonQuery();

            }
            conn.Close();
            return n;
        }

        public List<ObjetoLista> obtenerListaProductos(string filtro)
        {
            SqlConnection conn = new SqlConnection(connString);
            conn.Open();
            using (SqlCommand cmd = new SqlCommand(String.Format("select id_producto,pro.nombre from tblProductos pro "
  + "where id_producto like '%{0}%' or pro.nombre  like '%{0}%' or descripcion  like '%{0}%' or "
  + " pro.precio like '%{0}%' or tipo  like '%{0}%' or marca  like '%{0}%' or fecha_ingreso  like '%{0}%'"
  + " or codigo like '%{0}%'  "
  + " 	or id_categoria in (select id_categoria from tblCategorias where nombre like '%{0}%') "
  + "	or id_subcategoria in (select id_subcategoria from tblSubCategorias where nombre like '%{0}%') order by nombre", filtro), conn))
            {
                cmd.CommandType = CommandType.Text;
                using (SqlDataReader r = cmd.ExecuteReader())
                {
                    List<ObjetoLista> entidades = new List<ObjetoLista>();
                    while (r.Read())
                    {
                        entidades.Add(new ObjetoLista()
                        {
                            Id = (int)r[0]
                            ,
                            Nombre = r[1].ToString().TrimEnd()
                        });
                    }
                    conn.Close();
                    return entidades;
                }
            }
        }


        public List<ObjetoLista> obtenerListaProductosDisponibles(string filtro)
        {
            SqlConnection conn = new SqlConnection(connString);
            conn.Open();
            using (SqlCommand cmd = new SqlCommand(String.Format("select id_producto,pro.nombre from tblProductos pro "
  + "where (id_producto like '%{0}%' or pro.nombre  like '%{0}%' or descripcion  like '%{0}%' or "
  + " pro.precio like '%{0}%' or tipo  like '%{0}%' or marca  like '%{0}%' or fecha_ingreso  like '%{0}%'"
  + " or codigo like '%{0}%'  "
  + " 	or id_categoria in (select id_categoria from tblCategorias where nombre like '%{0}%') "
  + "	or id_subcategoria in (select id_subcategoria from tblSubCategorias where nombre like '%{0}%')) and existencias > 0 and activo = 1 order by nombre", filtro), conn))
            {
                cmd.CommandType = CommandType.Text;
                using (SqlDataReader r = cmd.ExecuteReader())
                {
                    List<ObjetoLista> entidades = new List<ObjetoLista>();
                    while (r.Read())
                    {
                        entidades.Add(new ObjetoLista()
                        {
                            Id = (int)r[0]
                            ,
                            Nombre = r[1].ToString().TrimEnd()
                        });
                    }
                    conn.Close();
                    return entidades;
                }
            }
        }


        public List<VentasProductos> obtenerListaProductosImagenDisponibles(string filtro)
        {
            SqlConnection conn = new SqlConnection(connString);
            conn.Open();
            using (SqlCommand cmd = new SqlCommand(String.Format("select top 200 id_producto,pro.nombre,pro.imagen from tblProductos pro "
  + "where (id_producto like '%{0}%' or pro.nombre  like '%{0}%' or descripcion  like '%{0}%' or "
  + " pro.precio like '%{0}%' or tipo  like '%{0}%' or marca  like '%{0}%' or fecha_ingreso  like '%{0}%'"
  + " or codigo like '%{0}%'  "
  + " 	or id_categoria in (select id_categoria from tblCategorias where nombre like '%{0}%') "
  + "	or id_subcategoria in (select id_subcategoria from tblSubCategorias where nombre like '%{0}%')) and existencias > 0 and activo = 1 order by nombre", filtro), conn))
            {
                cmd.CommandType = CommandType.Text;
                using (SqlDataReader r = cmd.ExecuteReader())
                {
                    List<VentasProductos> entidades = new List<VentasProductos>();
                    while (r.Read())
                    {
                        entidades.Add(new VentasProductos()
                        {
                            IdProducto = r[0].ToString().TrimEnd()
                            ,
                            Nombre = r[1].ToString().TrimEnd(),
                            RutaImagen = r[2].ToString().TrimEnd()
                        });
                    }
                    conn.Close();
                    return entidades;
                }
            }
        }


        internal int InsertarProducto(string nombre, string desripcion, string precio,
            decimal existencias, string tipo, string marca, string estado,
            DateTime fecha, string subcategoria, string categoria, string codigoBarras, bool servicio, bool activo, string precioCompra, string imagen, bool valorVariable)
        {

            string fechaAux = fecha.ToString("yyyy-MM-dd HH:mm:ss");
            SqlConnection conn = new SqlConnection(connString);
            conn.Open();
            int n;
            using (SqlCommand command = conn.CreateCommand())
            {
                command.CommandText = string.Format("INSERT INTO dbo.tblProductos  (nombre  ,descripcion "
                    + "  ,precio  ,existencias  ,tipo  ,marca  ,id_estado  ,fecha_ingreso "
                    + ",last_update  ,id_subcategoria  ,id_categoria, codigo,servicio,activo, valor_compra,imagen,valor_variable)"
                    + " VALUES  ('{0}','{1}','{2}' ,{3},'{4}','{5}',{6},'{7}',getDate(),{8},{9},'{10}','{11}','{12}','{13}','{14}','{15}');", nombre, desripcion,
                    precio, existencias, tipo, marca, estado, fechaAux, subcategoria, categoria, codigoBarras, servicio, activo, precioCompra, imagen, valorVariable);
                n = command.ExecuteNonQuery();

            }
            conn.Close();
            return n;
        }

        internal Producto ObtenerProducto(string id)
        {
            long number1 = 0;
            bool canConvert = long.TryParse(id, out number1);
            if (canConvert != true) return null;
            SqlConnection conn = new SqlConnection(connString);
            conn.Open();
            using (var cmd = new SqlCommand(String.Format("select id_producto ,nombre,codigo,descripcion,precio,existencias " +
                                                                 ",servicio,tipo,marca,id_estado,fecha_ingreso" +
                                                                 ",last_update,id_subcategoria,id_categoria,activo" +
                                                                 ",valor_compra,imagen,id_sucursal,valor_variable from tblProductos where id_producto = {0} order by nombre", id), conn))
            {
                cmd.CommandType = CommandType.Text;
                using (var r = cmd.ExecuteReader())
                {
                    Producto producto = new Producto();
                    while (r.Read())
                    {
                        producto.IdProducto = r[0].ToString();
                        producto.Nombre = r[1].ToString().TrimEnd();
                        producto.Codigo = r[2].ToString();
                        producto.Descripcion = r[3].ToString();
                        producto.Precio = Convert.ToDecimal(r[4].ToString());
                        producto.Existencias = Convert.ToInt32(r[5].ToString());
                        producto.Servicio = Convert.ToBoolean(r[6].ToString());
                        producto.Tipo = r[7].ToString();
                        producto.Marca = r[8].ToString();
                        producto.IdEstado = Convert.ToInt32(r[9].ToString());
                        producto.Fecha = Convert.ToDateTime(r[10].ToString());
                        producto.LastUpdate = Convert.ToDateTime(r[11].ToString());
                        if (r[12].ToString() != "")
                            producto.Subcategoria = Convert.ToInt32(r[12].ToString());
                        producto.Categoria = Convert.ToInt32(r[13].ToString());
                        producto.Activo = Convert.ToBoolean(r[14].ToString());
                        producto.PrecioCompra = Convert.ToDecimal(r[15].ToString());
                        producto.RutaImagen = r[16].ToString();
                        producto.ValorVariable = Convert.ToBoolean(r[18].ToString());
                    }
                    conn.Close();
                    return producto;
                }
            }
        }


        internal Producto ObtenerProductoByCodigo(string codigo)
        {
            SqlConnection conn = new SqlConnection(connString);
            conn.Open();
            using (SqlCommand cmd = new SqlCommand(String.Format("select * from tblProductos where codigo = '{0}' and existencias > 0 order by nombre", codigo), conn))
            {
                cmd.CommandType = CommandType.Text;
                using (SqlDataReader r = cmd.ExecuteReader())
                {
                    Producto producto = new Producto();
                    while (r.Read())
                    {
                        producto.IdProducto = r[0].ToString();
                        producto.Nombre = r[1].ToString().TrimEnd();
                        producto.Codigo = r[2].ToString();
                        producto.Descripcion = r[3].ToString();
                        producto.Precio = Convert.ToDecimal(r[4].ToString());
                        producto.Existencias = Convert.ToInt32(r[5].ToString());
                        producto.Servicio = Convert.ToBoolean(r[6].ToString());
                        producto.Tipo = r[7].ToString();
                        producto.Marca = r[8].ToString();
                        producto.IdEstado = Convert.ToInt32(r[9].ToString());
                        producto.Fecha = Convert.ToDateTime(r[10].ToString());
                        producto.LastUpdate = Convert.ToDateTime(r[11].ToString());
                        producto.Subcategoria = Convert.ToInt32(r[12].ToString());
                        producto.Categoria = Convert.ToInt32(r[13].ToString());
                        producto.Activo = Convert.ToBoolean(r[14].ToString());
                        producto.PrecioCompra = Convert.ToDecimal(r[15].ToString());
                        producto.ValorVariable = Convert.ToBoolean(r[18].ToString());

                    }
                    conn.Close();
                    return producto;
                }
            }
        }

        internal int ActualizarProducto(string id, string nombre, string desripcion, string precio,
            decimal existencias, string tipo, string marca, string estado,
            DateTime fecha, string subcategoria, string categoria, string codigoBarras, bool servicio, bool activo, string precioCompra, string imagen, bool valorVariable)
        {

            string fechaAux = fecha.ToString("yyyy-MM-dd HH:mm:ss");
            SqlConnection conn = new SqlConnection(connString);
            conn.Open();
            int n;
            using (SqlCommand command = conn.CreateCommand())
            {
                if (imagen.Equals(""))
                {
                    command.CommandText = string.Format("UPDATE dbo.tblProductos SET nombre = '{1}', "
                    + " descripcion = '{2}'  ,precio = '{3}'  ,existencias = {4}  ,tipo = '{5}' "
                    + ",marca = '{6}'  ,id_estado = '{7}'  ,fecha_ingreso = '{8}' "
                    + ",last_update = getdate()  ,id_subcategoria = {9}  ,id_categoria = {10}, "
                    + " codigo = '{11}', servicio = '{12}', activo = '{13}', valor_compra = '{14}' ,valor_variable = '{15}'"
                    + "WHERE id_producto = {0};", id, nombre, desripcion,
                        precio, existencias, tipo, marca, estado, fechaAux, subcategoria, categoria,
                        codigoBarras, servicio, activo, precioCompra,valorVariable);
                }
                else
                {
                    command.CommandText = string.Format("UPDATE dbo.tblProductos SET nombre = '{1}', "
                    + " descripcion = '{2}'  ,precio = '{3}'  ,existencias = {4}  ,tipo = '{5}' "
                    + ",marca = '{6}'  ,id_estado = '{7}'  ,fecha_ingreso = '{8}' "
                    + ",last_update = getdate()  ,id_subcategoria = {9}  ,id_categoria = {10}, "
                    + " codigo = '{11}', servicio = '{12}', activo = '{13}', valor_compra = '{14}', imagen = '{15}'"
                    + "WHERE id_producto = {0};", id, nombre, desripcion,
                        precio, existencias, tipo, marca, estado, fechaAux, subcategoria, categoria,
                        codigoBarras, servicio, activo, precioCompra, imagen);
                }
                n = command.ExecuteNonQuery();

            }
            conn.Close();
            return n;
        }

        internal int eliminarProducto(string id)
        {
            SqlConnection conn = new SqlConnection(connString);
            conn.Open();
            int n;
            using (SqlCommand command = conn.CreateCommand())
            {
                command.CommandText = string.Format("delete dbo.tblProductos where id_producto = {0};", id);
                n = command.ExecuteNonQuery();

            }
            conn.Close();
            return n;
        }

        internal List<ObjetoLista> obtenerListaUsuarios(string filtro)
        {
            SqlConnection conn = new SqlConnection(connString);
            conn.Open();
            using (SqlCommand cmd = new SqlCommand(String.Format("select id_usuario,nombres +' '+ apellidos from tblUsuarios where nombres like '%{0}%' or apellidos like '%{0}%' or celular like '%{0}%' or direccion like '%{0}%' order by nombres", filtro), conn))
            {
                cmd.CommandType = CommandType.Text;
                using (SqlDataReader r = cmd.ExecuteReader())
                {
                    List<ObjetoLista> entidades = new List<ObjetoLista>();
                    while (r.Read())
                    {
                        entidades.Add(new ObjetoLista()
                        {
                            Id = (int)r[0]
                            ,
                            Nombre = r[1].ToString().TrimEnd()
                        });
                    }
                    conn.Close();
                    return entidades;
                }
            }
        }

        internal int insertarUsuario(string identificacion, string nombres, string apellidos, string celular, string direccion, string pass, string rol, bool activo)
        {
            SqlConnection conn = new SqlConnection(connString);
            conn.Open();
            int n;
            using (SqlCommand command = conn.CreateCommand())
            {
                command.CommandText = string.Format("INSERT INTO dbo.tblUsuarios (identificacion , "
                + "nombres ,apellidos ,celular ,direccion ,password ,id_rol,fecha_ingreso,activo)    "
                + "VALUES ('{0}' ,'{1}' ,'{2}' ,'{3}' ,'{4}','{5}',{6},getDate(),'{7}');", identificacion, nombres, apellidos, celular, direccion, pass, rol, activo);
                n = command.ExecuteNonQuery();

            }
            conn.Close();
            return n;
        }

        internal Usuario obtenerUsuario(string id)
        {
            long number1 = 0;
            bool canConvert = long.TryParse(id, out number1);
            if (canConvert == true)
            {

                SqlConnection conn = new SqlConnection(connString);
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(String.Format("select * from tblUsuarios where id_usuario = {0}", id), conn))
                {
                    cmd.CommandType = CommandType.Text;
                    using (SqlDataReader r = cmd.ExecuteReader())
                    {
                        Usuario usuario = new Usuario();
                        while (r.Read())
                        {
                            usuario.IdUsuario = r[0].ToString();
                            usuario.Identificacion = r[1].ToString().TrimEnd();
                            usuario.Nombres = r[2].ToString();
                            usuario.Apellidos = r[3].ToString();
                            usuario.Celular = r[4].ToString();
                            usuario.Direccion = r[5].ToString();
                            usuario.Password = r[6].ToString();
                            usuario.Rol = Convert.ToInt32(r[7].ToString());
                            usuario.Activo = Convert.ToBoolean(r[9].ToString());


                        }
                        conn.Close();
                        return usuario;
                    }
                }
            } return null;
        }


        internal bool verificarUser(string user)
        {
            SqlConnection conn = new SqlConnection(connString);
            conn.Open();
            using (SqlCommand cmd = new SqlCommand(String.Format("select * from tblUsuarios where identificacion = '{0}'", user), conn))
            {
                cmd.CommandType = CommandType.Text;
                using (SqlDataReader r = cmd.ExecuteReader())
                {
                    Usuario usuario = new Usuario();
                    while (r.Read())
                    {
                        return true;
                    }
                    conn.Close();

                }
            }
            return false;
        }


        internal Usuario iniciarSesion(string user, string pass)
        {
            SqlConnection conn = new SqlConnection(connString);
            conn.Open();
            using (SqlCommand cmd = new SqlCommand(String.Format("select * from tblUsuarios where identificacion = '{0}' and password = '{1}' and activo = 1", user, pass), conn))
            {
                cmd.CommandType = CommandType.Text;
                using (SqlDataReader r = cmd.ExecuteReader())
                {
                    Usuario usuario = null;
                    while (r.Read())
                    {
                        usuario = new Usuario();
                        usuario.IdUsuario = r[0].ToString();
                        usuario.Identificacion = r[1].ToString().TrimEnd();
                        usuario.Nombres = r[2].ToString();
                        usuario.Apellidos = r[3].ToString();
                        usuario.Celular = r[4].ToString();
                        usuario.Direccion = r[5].ToString();
                        usuario.Password = r[6].ToString();
                        usuario.Rol = Convert.ToInt32(r[7].ToString());
                        //int activo = Convert.ToInt16(r[9].ToString());
                        //usuario.Activo = activo==1 ? true : false;
                        usuario.Activo = Convert.ToBoolean(r[9].ToString());
                    }
                    conn.Close();
                    return usuario;
                }
            }
        }

        internal int insertarFactura(string nombre, string celular, string direccion, double valor_bruto, double iva, double valorFactura, string user)
        {
            string valorTotal = valorFactura.ToString().Replace(".", ",");
            string valorBruto = valor_bruto.ToString().Replace(".", ",");
            string iva1 = iva.ToString().Replace(".", ",");
            SqlConnection conn = new SqlConnection(connString);
            conn.Open();
            int n;
            using (SqlCommand command = conn.CreateCommand())
            {
                command.CommandText = string.Format("INSERT INTO dbo.tblFacturas(nombre,celular, "
                + "direccion,fecha,valor_bruto,iva,valor_neto,usuario) "
                + " VALUES('{0}','{1}','{2}',getdate(),'{3}','{4}','{5}','{6}');",
                nombre, celular, direccion, valorBruto, iva1, valorTotal, user);
                n = command.ExecuteNonQuery();

            }
            conn.Close();
            conn = new SqlConnection(connString);
            conn.Open();
            if (n == 1)
            {
                using (SqlCommand cmd = new SqlCommand("select max(id_factura) from tblFacturas", conn))
                {
                    cmd.CommandType = CommandType.Text;
                    using (SqlDataReader r = cmd.ExecuteReader())
                    {
                        r.Read();
                        return Convert.ToInt32(r[0].ToString());
                    }
                }
            }
            else
                n = -1;
            conn.Close();
            return n;
        }

        internal int insertarProductoVenta(int factura, string idProducto, int cantidad, double valorUnitario, double total)
        {
            SqlConnection conn = new SqlConnection(connString);
            conn.Open();
            int n;
            using (SqlCommand command = conn.CreateCommand())
            {
                command.CommandText = string.Format("INSERT INTO dbo.tblVentas(id_producto,id_factura, "
                + " cantidad,valor_unitario,valor_total,fecha) VALUES({0},{1},'{2}','{3}','{4}',getdate());",
                idProducto, factura, cantidad, valorUnitario, total);
                n = command.ExecuteNonQuery();

            }
            if (n == 1)
            {
                using (SqlCommand command = conn.CreateCommand())
                {
                    command.CommandText = string.Format("UPDATE dbo.tblProductos SET existencias = existencias - {1} "
                    + "WHERE id_producto = {0} and servicio = 0;", idProducto, Convert.ToInt32(cantidad));
                    n = command.ExecuteNonQuery();

                }
            }
            conn.Close();
            return n;
        }

        internal DataTable obtenerProductosAll()
        {
            SqlConnection conn = new SqlConnection(connString);
            conn.Open();
            SqlCommand cmd;
            DataTable dt = new DataTable();
            //string query = "select * from dbo.tblConciliacion_cdb where id not in (select id_documento from dbo.tblResultado_conciliacion_cdb where id_documento is not null) and debito > 0";
            cmd = new SqlCommand("select * from vproductos", conn);
            cmd.CommandType = CommandType.Text;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            return dt;
        }


        public List<ObjetoLista> obtenerListaFacturas(string filtro)
        {
            SqlConnection conn = new SqlConnection(connString);
            conn.Open();
            using (SqlCommand cmd = new SqlCommand(String.Format("select top 1000 id_factura, CAST(id_factura AS varchar(12)) +'-'+ fac.nombre from tblFacturas fac "
  + "where id_factura like '%{0}%' or fac.nombre  like '%{0}%' or fac.celular  like '%{0}%' or "
  + " fac.celular like '%{0}%' "
  + " order by id_factura", filtro), conn))
            {
                cmd.CommandType = CommandType.Text;
                using (SqlDataReader r = cmd.ExecuteReader())
                {
                    List<ObjetoLista> entidades = new List<ObjetoLista>();
                    while (r.Read())
                    {
                        entidades.Add(new ObjetoLista()
                        {
                            Id = (int)r[0]
                            ,
                            Nombre = r[1].ToString().TrimEnd()
                        });
                    }
                    conn.Close();
                    return entidades;
                }
            }
        }


        public List<ObjetoLista> obtenerListaFacturas(string fechaIni, string fechaFin)
        {
            SqlConnection conn = new SqlConnection(connString);
            conn.Open();
            using (SqlCommand cmd = new SqlCommand(String.Format("select top 1000 id_factura, CAST(id_factura AS varchar(12))  +'-'+ fac.nombre from tblFacturas fac "
  + "where convert(datetime, convert(varchar(10), fecha, 102))  >= convert(datetime,'{0}') "
  + " and convert(datetime, convert(varchar(10), fecha, 102))  <= convert(datetime,'{1}')", fechaIni, fechaFin), conn))
            {
                cmd.CommandType = CommandType.Text;
                using (SqlDataReader r = cmd.ExecuteReader())
                {
                    List<ObjetoLista> entidades = new List<ObjetoLista>();
                    while (r.Read())
                    {
                        entidades.Add(new ObjetoLista()
                        {
                            Id = (int)r[0]
                            ,
                            Nombre = r[1].ToString().TrimEnd()
                        });
                    }
                    conn.Close();
                    return entidades;
                }
            }
        }

        internal Factura obtenerFactura(string id)
        {
            long number1 = 0;
            bool canConvert = long.TryParse(id, out number1);
            if (canConvert == true)
            {

                SqlConnection conn = new SqlConnection(connString);
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(String.Format("select * from tblFacturas where id_factura = {0} order by nombre", id), conn))
                {
                    cmd.CommandType = CommandType.Text;
                    using (SqlDataReader r = cmd.ExecuteReader())
                    {
                        Factura factura = new Factura();
                        while (r.Read())
                        {
                            factura.IdFactura = r[0].ToString();
                            factura.Nombre = r[1].ToString().TrimEnd();
                            factura.Celular = r[2].ToString();
                            factura.Direccion = r[3].ToString();
                            factura.Fecha = r[4].ToString();
                            factura.ValorBruto = Convert.ToDecimal(r[5].ToString());
                            factura.Iva = Convert.ToDecimal(r[6].ToString());
                            factura.ValorTotal = Convert.ToDecimal(r[7].ToString());
                            factura.Usuario = r[8].ToString();
                        }
                        conn.Close();
                        return factura;
                    }
                }
            } return null;
        }


        internal DataTable obteverProductosVenta(string id)
        {
            SqlConnection conn = new SqlConnection(connString);
            conn.Open();
            SqlCommand cmd;
            DataTable dt = new DataTable();
            cmd = new SqlCommand(String.Format("select * from vProductosVentas where id_factura = {0} order by nombre", id), conn);
            cmd.CommandType = CommandType.Text;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            return dt;
        }

        internal List<ObjetoLista> obtenerListaGastos(string filtro)
        {
            SqlConnection conn = new SqlConnection(connString);
            conn.Open();
            using (SqlCommand cmd = new SqlCommand(String.Format("select top 1000 id_compra, descripcion from tblCompras  "
  + "where id_compra like '%{0}%' or descripcion  like '%{0}%' or valor_compra  = '{0}' "
  + " order by descripcion", filtro), conn))
            {
                cmd.CommandType = CommandType.Text;
                using (SqlDataReader r = cmd.ExecuteReader())
                {
                    List<ObjetoLista> entidades = new List<ObjetoLista>();
                    while (r.Read())
                    {
                        entidades.Add(new ObjetoLista()
                        {
                            Id = (int)r[0]
                            ,
                            Nombre = r[1].ToString().TrimEnd()
                        });
                    }
                    conn.Close();
                    return entidades;
                }
            }
        }


        public List<ObjetoLista> obtenerListaGastos(string fechaIni, string fechaFin)
        {
            SqlConnection conn = new SqlConnection(connString);
            conn.Open();
            using (SqlCommand cmd = new SqlCommand(String.Format("select top 1000 id_compra, descripcion from tblCompras "
  + "where convert(datetime, convert(varchar(10), fecha, 102))  >= convert(datetime,'{0}') "
  + " and convert(datetime, convert(varchar(10), fecha, 102))  <= convert(datetime,'{1}')", fechaIni, fechaFin), conn))
            {
                cmd.CommandType = CommandType.Text;
                using (SqlDataReader r = cmd.ExecuteReader())
                {
                    List<ObjetoLista> entidades = new List<ObjetoLista>();
                    while (r.Read())
                    {
                        entidades.Add(new ObjetoLista()
                        {
                            Id = (int)r[0]
                            ,
                            Nombre = r[1].ToString().TrimEnd()
                        });
                    }
                    conn.Close();
                    return entidades;
                }
            }
        }

        internal int insertarCompra(string concepto, decimal cantidad, string valor, string total, string fecha)
        {
            SqlConnection conn = new SqlConnection(connString);
            conn.Open();
            int n;
            using (SqlCommand command = conn.CreateCommand())
            {
                command.CommandText = string.Format("INSERT INTO dbo.tblCompras(descripcion,cantidad,valor_compra,valor_total,fecha)  "
                    + "VALUES('{0}',{1},'{2}','{3}','{4}');", concepto, cantidad, valor, total, fecha);
                n = command.ExecuteNonQuery();

            }
            conn.Close();
            return n;
        }

        internal Gasto obtenerGasto(string id)
        {
            long number1 = 0;
            bool canConvert = long.TryParse(id, out number1);
            if (canConvert == true)
            {

                SqlConnection conn = new SqlConnection(connString);
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(String.Format("select * from tblCompras where id_compra = {0} order by descripcion", id), conn))
                {
                    cmd.CommandType = CommandType.Text;
                    using (SqlDataReader r = cmd.ExecuteReader())
                    {
                        Gasto gasto = new Gasto();
                        while (r.Read())
                        {
                            gasto.IdGasto = r[0].ToString();
                            gasto.Concepto = r[1].ToString().TrimEnd();
                            gasto.Cantidad = Convert.ToInt32(r[2].ToString());
                            gasto.ValorCompra = r[3].ToString();
                            gasto.ValorTotal = r[4].ToString();
                            gasto.Fecha = r[5].ToString();
                        }
                        conn.Close();
                        return gasto;
                    }
                }
            } return null;
        }

        internal int eliminarGasto(string id)
        {
            SqlConnection conn = new SqlConnection(connString);
            conn.Open();
            int n;
            using (SqlCommand command = conn.CreateCommand())
            {
                command.CommandText = string.Format("delete dbo.tblCompras where id_compra = {0};", id);
                n = command.ExecuteNonQuery();

            }
            conn.Close();
            return n;
        }

        internal DataTable obtenerIngresosGastos(string fechaIni, string fechaFin)
        {
            SqlConnection conn = new SqlConnection(connString);
            conn.Open();
            SqlCommand cmd;
            DataTable dt = new DataTable();
            cmd = new SqlCommand(String.Format("select * from vIngresosGastos "
                          + "where convert(datetime, convert(varchar(10), fecha, 102))  >= convert(datetime,'{0}') "
                          + " and convert(datetime, convert(varchar(10), fecha, 102))  <= convert(datetime,'{1}')", fechaIni, fechaFin), conn);

            cmd.CommandType = CommandType.Text;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            return dt;
        }

        internal int actualizarUsuario(string id, string identificacion, string nombres, string apellidos, string celular, string direccion, string pass, string rol, bool activo)
        {
            SqlConnection conn = new SqlConnection(connString);
            conn.Open();
            int n;
            using (SqlCommand command = conn.CreateCommand())
            {
                //command.CommandText = string.Format("update dbo.tblCategorias set  nombre = '{0}' where id_categoria = {1};", nombre, id);
                command.CommandText = string.Format("UPDATE dbo.tblUsuarios   SET identificacion = '{1}' , "
+ "nombres = '{2}'    ,apellidos = '{3}' ,celular = '{4}'   ,direccion = '{5}'"
+ ",password = '{6}'      ,id_rol = {7} ,activo = '{8}' WHERE id_usuario = {0};", id, identificacion, nombres, apellidos, celular, direccion, pass, rol, activo);
                n = command.ExecuteNonQuery();

            }
            conn.Close();
            return n;
        }


        internal DataTable obtenerMovimientos(string fecha, string concepto, string tipo, double valor)
        {
            SqlConnection conn = new SqlConnection(connString);
            conn.Open();
            SqlCommand cmd;
            string query = String.Format("select * from tblMovimientos "
                          + "where convert(datetime, convert(varchar(10), fecha, 102))  >= convert(datetime,'{0}') ", fecha);
            if (!tipo.Equals(""))
                query += " and tipo like '%" + tipo + "%' ";
            if (!concepto.Equals(""))
                query += " and descripcion like '%" + concepto + "%' ";
            if (valor != 0)
                query += " and valor like '%" + concepto + "%' ";
            DataTable dt = new DataTable();
            cmd = new SqlCommand(query, conn);

            cmd.CommandType = CommandType.Text;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            return dt;
        }

        internal int insertarMovimiento(string fecha, string concepto, string tipo, double valor)
        {
            SqlConnection conn = new SqlConnection(connString);
            conn.Open();
            int n;
            using (SqlCommand command = conn.CreateCommand())
            {
                command.CommandText = string.Format("INSERT INTO dbo.tblMovimientos(fecha,descripcion,tipo,valor, fecha_ingreso)  "
                    + "VALUES('{0}','{1}','{2}','{3}', getdate());", fecha, concepto, tipo, valor);
                n = command.ExecuteNonQuery();
            }
            conn.Close();
            return n;
        }

        internal int insertarMovimientoProductos(string fecha, string descripcion, string idProducto, int cantidad, double valor, double total)
        {
            SqlConnection conn = new SqlConnection(connString);
            conn.Open();
            int n;
            DateTime dt = DateTime.Parse(fecha);
            using (SqlCommand command = conn.CreateCommand())
            {
                command.CommandText = string.Format("INSERT INTO dbo.tblMovimientoProductos(fecha,descripcion,id_producto,valor, cantidad,total,fecha_registro)  "
                    + "VALUES('{0}','{1}',{2},'{3}','{4}','{5}', getdate());", dt.ToString("yyyy-MM-dd"), descripcion, idProducto, valor, cantidad, total);
                n = command.ExecuteNonQuery();
            }
            if (n == 1)
            {
                using (SqlCommand command = conn.CreateCommand())
                {
                    command.CommandText = string.Format("UPDATE dbo.tblProductos SET existencias = existencias - {1} "
                    + "WHERE id_producto = {0} and servicio = 0;", idProducto, Convert.ToInt32(cantidad));
                    n = command.ExecuteNonQuery();

                }
            }
            conn.Close();
            return n;
        }
    }
}
