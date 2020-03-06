using CABU.Data;
using CABU.Data.entidades;
using CABU.Data.Logica;
using CABU.Data.Utils;
using CabuSoft.Utils;
using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace CabuSoft
{
    public partial class VentaLista : System.Web.UI.Page
    {
        private List<ProductoVenta> listaProductos;


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                cargarProductos();
                if (listaProductos == null)
                {
                    if (Session["ventas"] != null)
                    {
                        listaProductos = (List<ProductoVenta>)Session["ventas"];
                        cargarDatosProductos();
                        updateTotal();
                    }

                }
            }
        }

        protected void lknBusqueda_Click(object sender, EventArgs e)
        {
            cargarProductos();
        }

        public void cargarProductos()
        {
            tblUsuarios usuario;
            if (Session["user"] != null)
            {
                usuario = (tblUsuarios)Session["user"];
                InicioSesion inicioSesion = new InicioSesion();
                if (usuario.id_rol == 5 && !inicioSesion.validarPermiso(usuario.id_usuario, "Ventas"))
                {
                    Response.Redirect("Index.aspx");
                }
                dsListaProductos.WhereParameters["filtro"].DefaultValue = txtBusqueda.Text;
                dsListaProductos.WhereParameters["id_sucursal"].DefaultValue = usuario.id_sucursal.ToString();
            }
        }

        protected void lknLimpiar_Click(object sender, EventArgs e)
        {
            Session["ventas"] = null;
            txtCelular.Text = "";
            txtId.Value = "";
            txtIdProducto.Value = "";
            txtRecibido.Text = "";
            txtUnidades.Text = "1";
            cargarDatosProductos();
        }

        protected void lknTerminar_Click(object sender, EventArgs e)
        {
            string mensaje = String.Empty;
            AdminProductos aPro = new AdminProductos();
            lblCambioTitulo.Text = "Cambio: ";
            Double value;
            if (txtRecibido.Text.Equals(""))
            {
                //txtRecibido.Text = "0";
                mensaje = "Debe ingresar el valor entregado por el Cliente";
            }
            else
            {

                if (txtNombre.Text.Equals(""))
                    txtNombre.Text = "Usuario No Definido";

                tblUsuarios usuario;
                if (Session["user"] != null)
                {
                    if (Session["ventas"] != null)
                    {
                        listaProductos = (List<ProductoVenta>)Session["ventas"];
                    }
                    updateTotal();
                    usuario = (tblUsuarios)Session["user"];
                    int factura = aPro.insertarFactura(txtNombre.Text, txtCelular.Text, txtDireccion.Text, valorFactura, usuario.nombres + " " + usuario.apellidos, usuario.id_sucursal.Value);
                    if (factura != 0)
                    {

                        if (Double.TryParse((Convert.ToDecimal(txtRecibido.Text) - valorFactura).ToString(), out value))
                            lblCambioTitulo.Text = lblCambioTitulo.Text + " " + String.Format(System.Globalization.CultureInfo.CurrentCulture, "{0:C2}", value);
                        foreach (ProductoVenta p in listaProductos)
                        {
                            aPro.insertarVenta(factura, p.IdProducto, (int)p.Cantidad, (double)p.Valor, (double)p.Total);

                        }

                        LocalReport localReport = new LocalReport();
                        localReport.ReportPath = "rptFactura.rdlc";
                        ReportParameter[] parameters = new ReportParameter[2];
                        parameters[0] = new ReportParameter("nombre", txtNombre.Text);
                        parameters[1] = new ReportParameter("facturaId", factura.ToString());
                        localReport.SetParameters(parameters);
                        ReportDataSource rds = new ReportDataSource("dsProductosVentas", ConvertDataTable.ConvertToDataTable(listaProductos));
                        localReport.DataSources.Add(rds);
                        ImprimirArchivos imp = new ImprimirArchivos();
                        imp.Export(localReport);
                        imp.Print();
                    }

                }
            }
        }

        protected void txtCodigoBarras_TextChanged(object sender, EventArgs e)
        {
            AdminProductos ap = new AdminProductos();
            tblProductos producto = ap.obtenerProductoByCodigo(txtCodigoBarras.Text);
            if (producto != null && producto.nombre != null && !txtCodigoBarras.Text.Equals("") && (producto.existencias>0 || producto.servicio.Value))
            {
                txtIdProducto.Value = producto.id_producto.ToString();
                lblNombre.Text = producto.nombre;
                lblDescripcion.Text = "Descripción: " + producto.descripcion;

                Double value;
                if (Double.TryParse(producto.precio.ToString(), out value))
                    lblPrecio.Text = String.Format(System.Globalization.CultureInfo.CurrentCulture, "{0:C2}", value);
                else
                    lblPrecio.Text = "$0";

                if (producto.servicio.HasValue && !producto.servicio.Value)
                    lblUnidades.Text = "Unidades: MAX(" + producto.existencias.ToString() + " Unidades) ";
                else
                    lblUnidades.Text = "Unidades: ";
                lblMarca.Text = producto.marca;
                if (producto.imagen != "")
                {
                    txtArchivo.ImageUrl = producto.imagen;
                }
                else
                    txtArchivo.ImageUrl = "Images/default_product.png";
                if (producto.tblCategorias != null)
                    lblCategoria.Text = producto.tblCategorias.nombre;
                if (producto.tblSubCategorias != null)
                    lblSubCategoria.Text = producto.tblSubCategorias.nombre;
                agregarProducto(producto);
                txtCodigoBarras.Text = "";
            }
        }

        private void agregarProducto(tblProductos producto)
        {
            if (listaProductos == null)
            {
                if (Session["ventas"] != null)
                    listaProductos = (List<ProductoVenta>)Session["ventas"];
                else
                    listaProductos = new List<ProductoVenta>();

            }
            bool productoSeleccionado = false;
            if (listaProductos.Count > 0)
            {
                ProductoVenta pv = listaProductos.FirstOrDefault(p => p.IdProducto == producto.id_producto.ToString());
                if (pv != null)
                {
                    if (Convert.ToInt32(txtUnidades.Text) + pv.Cantidad <= pv.MaximoUnidades)
                    {
                        pv.Cantidad = pv.Cantidad + Convert.ToInt32(txtUnidades.Text);
                        pv.Total = pv.Cantidad * pv.Valor;
                        cargarDatosProductos();
                        Session["ventas"] = listaProductos;
                        valorFactura = 0;
                        updateTotal();
                    }
                    productoSeleccionado = true;
                }
            }
            if (!productoSeleccionado)
            {
                ProductoVenta auxProducto = new ProductoVenta();
                auxProducto.IdProducto = producto.id_producto.ToString();
                auxProducto.Nombre = producto.nombre;
                auxProducto.Cantidad = Convert.ToDecimal(txtUnidades.Text);
                auxProducto.Valor = producto.precio;
                auxProducto.Total = auxProducto.Cantidad * auxProducto.Valor;
                if (producto.servicio.HasValue && !producto.servicio.Value)
                    auxProducto.MaximoUnidades = producto.existencias;
                else
                    auxProducto.MaximoUnidades = 1000;
                listaProductos.Add(auxProducto);
                cargarDatosProductos();
                Session["ventas"] = listaProductos;
                updateTotal();
            }
        }

        private decimal valorFactura;

        private void updateTotal()
        {
            valorFactura = 0;
            foreach (ProductoVenta p in listaProductos)
            {
                valorFactura += p.Total;

            }
            Double value;
            if (Double.TryParse(valorFactura.ToString(), out value))
            {
                lblValorFactura.Text = "Precio: " + String.Format(System.Globalization.CultureInfo.CurrentCulture, "{0:C2}", value);
                txtTotal.Text = valorFactura.ToString();
            }
            else
                lblValorFactura.Text = "$0";
            txtRecibido.Text = Convert.ToInt32(valorFactura).ToString();
        }

        private void cargarDatosProductos()
        {

            gvVentas.DataSource = listaProductos;
            gvVentas.DataBind();
            //gvVentas.Columns["Total"].DefaultCellStyle.Format = "c";
        }

        protected void gvVentas_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument);
            GridViewRow row = gvVentas.Rows[index];
            if (e.CommandName == "Eliminar")
            {
                if (listaProductos == null)
                {
                    if (Session["ventas"] != null)
                    {
                        listaProductos = (List<ProductoVenta>)Session["ventas"];
                    }

                }
                int ItemId = Int32.Parse(gvVentas.DataKeys[index].Values[0].ToString());
                string idProducto = ItemId.ToString();
                foreach (ProductoVenta p in listaProductos)
                {
                    if (p.IdProducto == idProducto)
                    {
                        listaProductos.Remove(p);
                        break;
                    }
                }
                Session["ventas"] = listaProductos;
                cargarDatosProductos();
            }
        }

        protected void gvVentas_RowEditing(object sender, GridViewEditEventArgs e)
        {

        }

        protected void gvVentas_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            string mensaje = String.Empty;
            int idProducto = Convert.ToInt32(gvVentas.DataKeys[e.RowIndex].Value.ToString());
            GridViewRow row = (GridViewRow)gvVentas.Rows[e.RowIndex];
            TextBox txtCantidad = (TextBox)row.FindControl("txtEditCantidad");
            string nuevaCantidad = txtCantidad.Text;
            int n;
            bool isNumeric = int.TryParse(nuevaCantidad, out n);
            if (isNumeric)
            {
                if (listaProductos == null)
                {
                    if (Session["ventas"] != null)
                    {
                        listaProductos = (List<ProductoVenta>)Session["ventas"];
                        ProductoVenta producto = listaProductos.FirstOrDefault(p => p.IdProducto == idProducto.ToString());
                        AdminProductos ap = new AdminProductos();
                        tblProductos pro = ap.obtenerProductoById(Convert.ToInt32(producto.IdProducto));
                        if (!pro.servicio.Value && producto.MaximoUnidades < n)
                        {
                            mensaje = "Error pailas";
                        }
                        else
                        {
                            producto.Cantidad = n;
                            producto.Total = producto.Cantidad * producto.Valor;
                            Session["ventas"] = listaProductos;
                            gvVentas.EditIndex = -1;
                            cargarDatosProductos();
                            updateTotal();
                        }

                    }

                }
            }
        }

        protected void Repeater1_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "addProductos")
            {
                int idProducto = Convert.ToInt32(e.CommandArgument);
                AdminProductos aProducto = new AdminProductos();
                tblProductos producto = aProducto.obtenerProductoById(idProducto);
                if (producto != null)
                {
                    txtId.Value = idProducto.ToString();
                    lblNombre.Text = producto.nombre;
                    lblDescripcion.Text = producto.descripcion;

                    Double value;
                    if (Double.TryParse(producto.precio.ToString(), out value))
                        lblPrecio.Text = String.Format(System.Globalization.CultureInfo.CurrentCulture, "{0:C2}", value);
                    else
                        lblPrecio.Text = "$0";
                    if (producto.servicio.HasValue && !producto.servicio.Value)
                        lblUnidades.Text = "Unidades: MAX(" + producto.existencias.ToString() + " Unidades) ";
                    else
                        lblUnidades.Text = "Unidades: ";
                    lblMarca.Text = producto.marca;
                    if (producto.imagen != "")
                    {
                        txtArchivo.ImageUrl = producto.imagen;
                    }
                    else
                        txtArchivo.ImageUrl = "Images/default_product.png";
                    if (producto.tblCategorias != null)
                        lblCategoria.Text = producto.tblCategorias.nombre;
                    if (producto.tblSubCategorias != null)
                        lblSubCategoria.Text = producto.tblSubCategorias.nombre;
                    agregarProducto(producto);
                }
            }
        }





    }
}