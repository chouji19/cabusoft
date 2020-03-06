using CABU.Data;
using CABU.Data.entidades;
using CABU.Data.Logica;
using CABU.Data.Utils;
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
    public partial class Ventas : System.Web.UI.Page
    {

        private List<ProductoVenta> listaProductos;


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                
                if (listaProductos == null)
                {
                    if (Session["ventas"] != null)
                    {
                        listaProductos = (List<ProductoVenta>)Session["ventas"];
                        cargarDatosProductos();
                        updateTotal();
                    }
                }
                cargarProductos();
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
            lknAgregar.Enabled = true;
        }

        protected void lknAgregar_Click(object sender, EventArgs e)
        {
            AdminProductos aProducto = new AdminProductos();
            tblProductos producto = aProducto.obtenerProductoById(Convert.ToInt32(txtListaProductos.SelectedValue));
            agregarProducto(producto);
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
                    int factura = aPro.insertarFactura(txtNombre.Text, txtCelular.Text, txtDireccion.Text, valorFactura, usuario.nombres + " " + usuario.apellidos,usuario.id_sucursal.Value);
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
                        Export(localReport);
                        Print();
                    }
                   
                }



            }
        }

        private int m_currentPageIndex;
        private IList<Stream> m_streams;

        private Stream CreateStream(string name, string fileNameExtension, Encoding encoding, string mimeType, bool willSeek)
        {
            Stream stream = new MemoryStream();
            m_streams.Add(stream);
            return stream;
        }
        // Export the given report as an EMF (Enhanced Metafile) file.
        private void Export(LocalReport report)
        {
            string deviceInfo =
              @"<DeviceInfo>
                <OutputFormat>EMF</OutputFormat>
                <PageWidth>3.15in</PageWidth>
                <MarginTop>0.25in</MarginTop>
                <MarginLeft>0.25in</MarginLeft>
                <MarginRight>0.25in</MarginRight>
                <MarginBottom>0.25in</MarginBottom>
            </DeviceInfo>";
            Warning[] warnings;
            m_streams = new List<Stream>();
            report.Render("Image", deviceInfo, CreateStream,
               out warnings);
            foreach (Stream stream in m_streams)
                stream.Position = 0;
        }

        // Handler for PrintPageEvents
        private void PrintPage(object sender, PrintPageEventArgs ev)
        {
            Metafile pageImage = new
               Metafile(m_streams[m_currentPageIndex]);

            // Adjust rectangular area with printer margins.
            Rectangle adjustedRect = new Rectangle(
                ev.PageBounds.Left - (int)ev.PageSettings.HardMarginX,
                ev.PageBounds.Top - (int)ev.PageSettings.HardMarginY,
                ev.PageBounds.Width,
                ev.PageBounds.Height);

            // Draw a white background for the report
            ev.Graphics.FillRectangle(Brushes.White, adjustedRect);

            // Draw the report content
            ev.Graphics.DrawImage(pageImage, adjustedRect);

            // Prepare for the next page. Make sure we haven't hit the end.
            m_currentPageIndex++;
            ev.HasMorePages = (m_currentPageIndex < m_streams.Count);
        }

        private void Print()
        {
            if (m_streams == null || m_streams.Count == 0)
                throw new Exception("Error: no stream to print.");
            PrintDocument printDoc = new PrintDocument();
            if (!printDoc.PrinterSettings.IsValid)
            {
                throw new Exception("Error: cannot find the default printer.");
            }
            else
            {
                printDoc.PrintPage += new PrintPageEventHandler(PrintPage);
                m_currentPageIndex = 0;
                printDoc.Print();
            }
        }

        protected void ListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            AdminProductos aProducto = new AdminProductos();
            tblProductos producto = aProducto.obtenerProductoById(Convert.ToInt32(txtListaProductos.SelectedValue));
            if (producto != null)
            {
                txtId.Value = txtListaProductos.SelectedValue;
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
            }
        }

        protected void txtCodigoBarras_TextChanged(object sender, EventArgs e)
        {
            AdminProductos ap = new AdminProductos();
            tblProductos producto = ap.obtenerProductoByCodigo(txtCodigoBarras.Text);
            if (producto != null && producto.nombre != null && !txtCodigoBarras.Text.Equals("") && (producto.existencias > 0 || producto.servicio.Value))
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

                foreach (ProductoVenta p in listaProductos)
                {
                    if (p.IdProducto == producto.id_producto.ToString())
                    {
                        productoSeleccionado = true;
                    }

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
                //gvVentas.Columns["Total"].DefaultCellStyle.Format = "c";
                //dgvProductos.Columns["Valor"].DefaultCellStyle.Format = "c";
                //dgvProductos.Columns["IdProducto"].Visible = false;
                //dgvProductos.Columns["MaximoUnidades"].Visible = false;
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





    }
}