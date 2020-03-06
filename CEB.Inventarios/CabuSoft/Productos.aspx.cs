using CABU.Data;
using CABU.Data.Logica;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CabuSoft
{
    public partial class Productos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                cargarProductos();
                tblUsuarios usuario;
                if (Session["user"] != null)
                {
                    usuario = (tblUsuarios)Session["user"];
                    InicioSesion inicioSesion = new InicioSesion();
                    if (usuario.id_rol == 5 && !inicioSesion.validarPermiso(usuario.id_usuario, "ProductosR") && !inicioSesion.validarPermiso(usuario.id_usuario, "ProductosRW"))
                    {
                        Response.Redirect("Index.aspx");
                    }
                    else
                    { 
                        if(inicioSesion.validarPermiso(usuario.id_usuario, "ProductosR"))
                        {
                            lknNuevo.Visible = false;
                            lknGuardar.Visible = false;
                            lknEliminar.Visible = false;
                        }
                    }
                    dsCategorias.WhereParameters["id_empresa"].DefaultValue = usuario.tblSucursales.id_empresa.ToString();
                }
            }
        }

        protected void lknBusqueda_Click(object sender, EventArgs e)
        {
            cargarProductos();
        }

        protected void ListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            AdminProductos aProducto = new AdminProductos();
            tblProductos producto = aProducto.obtenerProductoById(Convert.ToInt32(txtListaProductos.SelectedValue));
            if (producto != null)
            {
                txtId.Value = txtListaProductos.SelectedValue;
                txtNombre.Text = producto.nombre;
                txtDescripcion.Text = producto.descripcion;
                txtPrecio.Text = producto.precio.ToString();
                txtPrecioCompra.Text = producto.valor_compra.ToString();
                txtUnidades.Text = producto.existencias.ToString();
                //txtCbTipo.Text = producto.Tipo;
                txtCbEstado.SelectedValue = producto.id_estado.ToString();
                if (producto.fecha_ingreso.HasValue)
                    txtDpFecha.Text = producto.fecha_ingreso.Value.ToString(@"dd/MM/yyyy");
                else
                    txtDpFecha.Text = "";
                txtCbCategoria.SelectedValue = producto.id_categoria.ToString();
                obtenerSubCategorias(txtCbCategoria.SelectedValue);
                txtCbSubCategoria.SelectedValue = producto.id_subcategoria.ToString();
                txtCodigoBarras.Text = producto.codigo;
                txtCBActivo.Checked = producto.activo.Value;
                txtEsServicio.Checked = producto.servicio.Value;
                if (producto.imagen != "")
                {
                    txtArchivo.ImageUrl = producto.imagen;
                }
                else
                    txtArchivo.ImageUrl = "Images/default_product.png";
                lblGuardar.Text = "Actualizar";
            }

        }

        protected void lknNuevo_Click(object sender, EventArgs e)
        {
            txtId.Value = txtListaProductos.SelectedValue;
            txtNombre.Text = "";
            txtDescripcion.Text = "";
            txtPrecio.Text = "";
            txtPrecioCompra.Text = "";
            txtUnidades.Text = "0";
            txtDpFecha.Text = "";
            txtCodigoBarras.Text = "";
            lblGuardar.Text = "Guardar";
        }

        public void cargarProductos()
        {
            tblUsuarios usuario;
            if (Session["user"] != null)
            {
                usuario = (tblUsuarios)Session["user"];
                dsListaProductos.WhereParameters["filtro"].DefaultValue = txtBusqueda.Text;
                dsListaProductos.WhereParameters["id_sucursal"].DefaultValue = usuario.id_sucursal.ToString();
            }
        }

        public void obtenerSubCategorias(string categoria)
        {
            if (categoria != "")
            {
                AdminProductos aProducto = new AdminProductos();
                txtCbSubCategoria.Items.Clear();
                txtCbSubCategoria.DataSource = aProducto.obtenerListaSubCategorias(categoria);
                txtCbSubCategoria.DataTextField = "Nombre";
                txtCbSubCategoria.DataValueField = "id_subcategoria";
                txtCbSubCategoria.DataBind();
            }
        }

        protected void txtCbCategoria_SelectedIndexChanged(object sender, EventArgs e)
        {
            obtenerSubCategorias(txtCbCategoria.SelectedValue);
        }

        protected void lknGuardar_Click(object sender, EventArgs e)
        {
            AdminProductos aProducto = new AdminProductos();
            int resultado = 0;
            string mensaje = String.Empty;
            tblUsuarios usuario;
            if (Session["user"] != null)
            {
                usuario = (tblUsuarios)Session["user"];
                if (!txtNombre.Equals(""))
                {
                    if (txtUnidades.Text == "0" && !txtEsServicio.Checked)
                        mensaje = "Las unidades disponibles no pueden ser 0";
                    else
                    {
                        double precio = 0;
                        double precioCompra = 0;
                        if (txtPrecio.Text != "")
                        {
                            precio = Convert.ToDouble(txtPrecio.Text);
                        }
                        if (txtPrecioCompra.Text != "")
                        {
                            precioCompra = Convert.ToDouble(txtPrecioCompra.Text);
                        }
                        String imagen = String.Empty;
                        if (txtImagen.HasFile)
                        {
                            string fileName = Path.GetFileName(txtImagen.PostedFile.FileName);
                            imagen = txtId.Value + "_" + DateTime.Now.ToString("yyyyMMddHHmmss") + fileName;
                            txtImagen.PostedFile.SaveAs(Server.MapPath("~/Images/Productos/") + imagen);
                            imagen = @"Images/Productos/" + imagen;
                        }
                        DateTime fecha = DateTime.ParseExact(txtDpFecha.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None);
                        if (lblGuardar.Text.Equals("Guardar"))
                        {
                            resultado = aProducto.insertarProducto(txtNombre.Text, txtDescripcion.Text, precio.ToString(),
                                                    Convert.ToDecimal(txtUnidades.Text), "", txtMarca.Text, txtCbEstado.SelectedValue.ToString(),
                                                    fecha, txtCbSubCategoria.SelectedValue.ToString(),
                                                    txtCbCategoria.SelectedValue.ToString(), txtCodigoBarras.Text,
                                                    txtEsServicio.Checked, txtCBActivo.Checked, precioCompra.ToString(), imagen, usuario.id_sucursal.Value);
                        }
                        else
                        {
                            resultado = aProducto.actualizarProducto(Convert.ToInt32(txtListaProductos.SelectedValue), txtNombre.Text, txtDescripcion.Text, precio.ToString(),
                                        Convert.ToDecimal(txtUnidades.Text), "", txtMarca.Text, txtCbEstado.SelectedValue.ToString(),
                                        fecha, txtCbSubCategoria.SelectedValue.ToString(),
                                        txtCbCategoria.SelectedValue.ToString(), txtCodigoBarras.Text,
                                        txtEsServicio.Checked, txtCBActivo.Checked, precioCompra.ToString(), imagen);
                        }
                        if (resultado != 1)
                        {
                            mensaje = "No se pudo ingresar el registro";
                        }
                        else
                        {
                            mensaje = "Producto Cargada Correctamente";
                            txtBusqueda.Text = txtNombre.Text;
                            cargarProductos();
                        }
                    }
                }
                else
                    mensaje = "Debe ingresar un nombre";
                pnlAdvertencia.CssClass = "alert alert-success alert-dismissible";
                lblResultadoProceso.Text = mensaje;
                pnlAdvertencia.Visible = true;
            }
        }

        protected void txtEsServicio_CheckedChanged(object sender, EventArgs e)
        {
            if(txtEsServicio.Checked)
                txtUnidades.Enabled = false;
            else
                txtUnidades.Enabled = true;
        }

        protected void lknEliminar_Click(object sender, EventArgs e)
        {
            AdminProductos aProducto = new AdminProductos();
            int resultado = aProducto.eliminarProducto(Convert.ToInt32(txtListaProductos.SelectedValue));
            string mensaje = String.Empty;
            if (resultado > 0)
            {
                mensaje = "Producto Eliminado Correctamente";
                txtListaProductos.DataBind();
            }
            else
                mensaje = "Error al eliminar Un Producto, es probable que ya tenga procesos registrados";
            pnlAdvertencia.CssClass = "alert alert-success alert-dismissible";
            lblResultadoProceso.Text = mensaje;
            pnlAdvertencia.Visible = true;
        }

        protected void lknBtnAddCategoria_Click(object sender, EventArgs e)
        {
            txtCategoriaNueva.Visible = true;
            lknAddCategoria.Visible = true;
        }

        protected void lknAddCategoria_Click(object sender, EventArgs e)
        {

            AdminCategorias aCat = new AdminCategorias();
            int idCategoria = 0;
            if (txtCategoriaNueva.Text != "")
            {
                tblUsuarios usuario;
                if (Session["user"] != null)
                {
                    usuario = (tblUsuarios)Session["user"];
                    idCategoria = aCat.crearCategoria(txtCategoriaNueva.Text,usuario.tblSucursales.id_empresa.Value);
                }
            }
            if (idCategoria != 0)
            {
                txtCbCategoria.Items.Clear();
                txtCbCategoria.DataBind();
                txtCbCategoria.SelectedValue = idCategoria.ToString();
                tblUsuarios usuario;
                if (Session["user"] != null)
                {
                    usuario = (tblUsuarios)Session["user"];
                    dsCategorias.WhereParameters["id_empresa"].DefaultValue = usuario.tblSucursales.id_empresa.ToString();
                }
                txtCategoriaNueva.Visible = false;
                lknAddCategoria.Visible = false;
            }
        }


        protected void lknBtnAddSubCategoria_Click(object sender, EventArgs e)
        {
            if (txtCbCategoria.SelectedValue != "")
            {
                txtSubcategoriaNueva.Visible = true;
                lknAddSubcategoria.Visible = true;
            }
            else
            {
                pnlAdvertencia.CssClass = "alert alert-success alert-dismissible";
                lblResultadoProceso.Text = "Debe Seleccionar Una Categoría";
                pnlAdvertencia.Visible = true;
            }
        }

        protected void lknAddSubcategoria_Click(object sender, EventArgs e)
        {
            if (txtCbCategoria.SelectedValue != "")
            {
                int idCategoria = Convert.ToInt32(txtCbCategoria.SelectedValue);
                AdminCategorias aCat = new AdminCategorias();
                int idSubCategoria = 0;
                if (txtSubcategoriaNueva.Text != "")
                    idSubCategoria = aCat.crearSubCategoria(txtSubcategoriaNueva.Text, idCategoria);
                if (idSubCategoria != 0)
                {
                    obtenerSubCategorias(txtCbCategoria.SelectedValue);
                    txtCbSubCategoria.SelectedValue = idSubCategoria.ToString();
                    txtSubcategoriaNueva.Visible = false;
                    lknAddSubcategoria.Visible = false;
                }
            }
        }

    }


}
