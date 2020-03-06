using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CEB.Inventarios.logica;
using CEB.Inventarios.entidades;
using Microsoft.VisualBasic;
using Microsoft.Reporting.WinForms;
using System.IO;
using System.Drawing.Printing;
using System.Drawing.Imaging;
using System.Configuration;
using CEB.Inventarios.comunes;

namespace CEB.Inventarios
{
    public partial class frmVentas : Form
    {
        private Usuario user;

        internal Usuario User
        {
            get { return user; }
            set { user = value; }
        }

        public frmVentas()
        {
            InitializeComponent();
            cargarProductos();
            listaProductosVenta = new List<ProductoVenta>();
            lblId.Text = "";
        }

        private List<ProductoVenta> listaProductosVenta;
        private Producto producto;
        private decimal valorFactura;

        public void cargarProductos()
        {
            LogicaGo logica = new LogicaGo();
            lbProductos.DataSource = logica.obtenerListaProductosDisponibles(txtFiltro.Text);
            if (lbProductos.Items.Count > 0)
                lbProductos.SelectedIndex = 0;
            lbProductos.DisplayMember = "Nombre";
            lbProductos.ValueMember = "Id";
            cargarCategorias();

        }

        private void lbProductos_SelectedIndexChanged(object sender, EventArgs e)
        {
            LogicaGo logica = new LogicaGo();
            lblId.Text = lbProductos.SelectedValue.ToString();
            producto = logica.ObtenerProducto(lblId.Text);
            if (producto == null) return;
            
            lblId.Text = producto.IdProducto;
            lblNombre.Text = producto.Nombre;
            lblDescripcion.Text = "Descripción: " + producto.Descripcion;

            Double value;
            if (Double.TryParse(producto.Precio.ToString(), out value))
                lblPrecio.Text = "Precio: " + String.Format(System.Globalization.CultureInfo.CurrentCulture, "{0:C2}", value);
            else
                lblPrecio.Text = "Precio: " + String.Empty;
            cbUnidades.Value = 1;
            if (!producto.Servicio)
                cbUnidades.Maximum = producto.Existencias;
            else
                cbUnidades.Maximum = 1000;
            lblMaxUnidades.Text = "Max: " + producto.Existencias + " unidades";
            lblTipo.Text = "Tipo: " + producto.Tipo;
            txtCbEstado.SelectedValue = producto.IdEstado;
            txtCbCategoria.SelectedValue = producto.Categoria;
            txtCbSubCategoria.SelectedValue = producto.Subcategoria;
            lblEstado.Text = "Estado: " + txtCbEstado.Text;
            lblFecha.Text = "Fecha Ingreso: " + producto.Fecha.ToString();
            lblCategoria.Text = "Categoria: " + txtCbCategoria.Text;
            lblSubCategoria.Text = "SubCategoria: " + txtCbSubCategoria.Text;
            if (producto.RutaImagen != "")
            {
                string rutaArchivo = ConfigurationManager.AppSettings["rutaImagenes"];
                Image image;
                if (File.Exists(rutaArchivo + producto.RutaImagen))
                    image = Image.FromFile(rutaArchivo + producto.RutaImagen);
                else
                    image = Image.FromFile(rutaArchivo + @"Images\Productos\default_product.png");
                Rectangle newRect = ImageHandling.GetScaledRectangle(image, pictureBox1.ClientRectangle);
                pictureBox1.Image = ImageHandling.GetResizedImage(image, newRect);
                pictureBox1.Height = 120;
                pictureBox1.Width = 120;
            }
            else
                pictureBox1.Image = null;
        }


        public void cargarCategorias()
        {
            LogicaGo logica = new LogicaGo();
            txtCbCategoria.DataSource = logica.obtenerListaCategorias("");
            txtCbCategoria.DisplayMember = "Nombre";
            txtCbCategoria.ValueMember = "Id";
        }

        private void txtCbCategoria_SelectedIndexChanged(object sender, EventArgs e)
        {
            LogicaGo logica = new LogicaGo();
            txtCbSubCategoria.DataSource = logica.obtenerListaSubCategorias(txtCbCategoria.SelectedValue.ToString());
            txtCbSubCategoria.DisplayMember = "Nombre";
            txtCbSubCategoria.ValueMember = "Id";
        }

        private void txtFiltro_TextChanged(object sender, EventArgs e)
        {
            cargarProductos();
        }

        private void frmVentas_Load(object sender, EventArgs e)
        {
            // TODO: esta línea de código carga datos en la tabla 'dsEstados.tblEstadosProductos' Puede moverla o quitarla según sea necesario.
            this.tblEstadosProductosTableAdapter.Fill(this.dsEstados.tblEstadosProductos);

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (lblId.Text.Equals(""))
                MessageBox.Show("Seleccione producto");
            else
            {
                bool productoSeleccionado = false;
                foreach (ProductoVenta p in listaProductosVenta.Where(p => p.IdProducto == producto.IdProducto))
                {
                    productoSeleccionado = true;
                }
                if (!productoSeleccionado)
                {
                    if (producto.ValorVariable)
                    {
                        var texto = Interaction.InputBox("Valor venta:", "Valor Venta", "");
                        int valorProducto;
                        var result = Int32.TryParse(texto, out valorProducto);
                        if (result)
                        {
                            producto.Precio = valorProducto;
                        }
                        else
                        {
                            MessageBox.Show("Debe Ingresar un valor valido");
                        }

                    }
                    ProductoVenta auxProducto = new ProductoVenta();
                    auxProducto.IdProducto = producto.IdProducto;
                    auxProducto.Nombre = producto.Nombre;
                    auxProducto.Cantidad = cbUnidades.Value;
                    auxProducto.Valor = producto.Precio;
                    auxProducto.Total = auxProducto.Cantidad * auxProducto.Valor;
                    if (!producto.Servicio)
                        auxProducto.MaximoUnidades = producto.Existencias;
                    else
                        auxProducto.MaximoUnidades = 1000;
                    listaProductosVenta.Add(auxProducto);
                    CargarDatosProductos();
                    var dataGridViewColumn = dgvProductos.Columns["Total"];
                    if (dataGridViewColumn != null)
                        dataGridViewColumn.DefaultCellStyle.Format = "c";
                    var gridViewColumn = dgvProductos.Columns["Valor"];
                    if (gridViewColumn != null)
                        gridViewColumn.DefaultCellStyle.Format = "c";
                    var viewColumn = dgvProductos.Columns["IdProducto"];
                    if (viewColumn != null) viewColumn.Visible = false;
                    //dgvProductos.Columns["MaximoUnidades"].Visible = false;
                    valorFactura = 0;
                    UpdateTotal();
                }
                else
                    MessageBox.Show("El producto que desea agregar ya fue seleccionado previamente");
            }

        }

        private void UpdateTotal()
        {
            valorFactura = 0;
            foreach (ProductoVenta p in listaProductosVenta)
            {
                valorFactura += p.Total;

            }
            Double value;
            if (Double.TryParse(valorFactura.ToString(), out value))
                lblValorFactura.Text = String.Format(System.Globalization.CultureInfo.CurrentCulture, "{0:C2}", value);
            else
                lblValorFactura.Text = "$0";
            txtRecibido.Text = valorFactura.ToString();
        }

        private void CargarDatosProductos()
        {
            dgvProductos.DataSource = ConvertToDataTable(listaProductosVenta);
        }


        public DataTable ConvertToDataTable<T>(IList<T> data)
        {
            PropertyDescriptorCollection properties =
               TypeDescriptor.GetProperties(typeof(T));
            DataTable table = new DataTable();
            foreach (PropertyDescriptor prop in properties)
                table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
            foreach (T item in data)
            {
                DataRow row = table.NewRow();
                foreach (PropertyDescriptor prop in properties)
                    row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;
                table.Rows.Add(row);
            }
            return table;

        }

        private void dgvProductos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //Do nothing if a header is clicked
            if (e.RowIndex < 0 || e.ColumnIndex < 0)
                return;

            //Switch through the column names
            switch (dgvProductos.Columns[e.ColumnIndex].Name)
            {
                case "Eliminar":
                    foreach (ProductoVenta p in listaProductosVenta)
                    {
                        if (dgvProductos.Rows[e.RowIndex].Cells["IdProducto"].Value.ToString().Equals(p.IdProducto.ToString()))
                        {
                            dgvProductos.Rows.RemoveAt(e.RowIndex);
                            listaProductosVenta.Remove(p);
                            cargarProductos();
                            UpdateTotal();
                            break;
                        }

                    }

                    break;
                case "Editar":
                    var correcto = true;
                    var texto = "";
                    do
                    {
                        texto = Interaction.InputBox("Cantidad:", "Cantidad", "");
                        var number1 = 0;
                        correcto = Int32.TryParse(texto, out number1);
                        if (!correcto)
                            MessageBox.Show("Debe ingresar un valor valido");
                        else
                        {
                            foreach (ProductoVenta p in listaProductosVenta)
                            {
                                if (dgvProductos.Rows[e.RowIndex].Cells["IdProducto"].Value.ToString().Equals(p.IdProducto.ToString()))
                                {
                                    if (number1 > Convert.ToInt32(dgvProductos.Rows[e.RowIndex].Cells["MaximoUnidades"].Value))
                                    {
                                        MessageBox.Show("No puede seleccionar mas de " + dgvProductos.Rows[e.RowIndex].Cells["MaximoUnidades"].Value + " Unidades");
                                        correcto = false;
                                    }
                                    else
                                        correcto = true;
                                }

                            }
                        }
                    } while (!correcto);
                    //MessageBox.Show("ahora la cantidad son: " + texto);
                    foreach (ProductoVenta p in listaProductosVenta)
                    {
                        if (
                            !dgvProductos.Rows[e.RowIndex].Cells["IdProducto"].Value.ToString()
                                .Equals(p.IdProducto.ToString())) continue;
                        listaProductosVenta.First(d => d.IdProducto == p.IdProducto).Cantidad = Convert.ToInt32(texto);
                        listaProductosVenta.First(d => d.IdProducto == p.IdProducto).Total = Convert.ToInt32(texto) * p.Valor;
                        dgvProductos.Rows[e.RowIndex].Cells["Cantidad"].Value = p.Cantidad;
                        dgvProductos.Rows[e.RowIndex].Cells["Total"].Value = Convert.ToInt32(texto) * p.Valor;
                        UpdateTotal();
                        break;
                    }
                    break;
            }


        }

        private void btnCheck_Click(object sender, EventArgs e)
        {
            LogicaGo logica = new LogicaGo();
            
            Double value;
            if (txtRecibido.Text.Equals(""))
            {
                //txtRecibido.Text = "0";
                MessageBox.Show("Debe ingresar el valor entregado por el Cliente");
                
            }
            else
            {
                if (listaProductosVenta.Count == 0)
                {
                    MessageBox.Show("No ha Seleccionado Productos");
                }
                else
                {
                    if (txtNombre.Text.Equals(""))
                        txtNombre.Text = "Usuario No Definido";
                    if (Convert.ToDecimal(txtRecibido.Text) < valorFactura)
                        MessageBox.Show("El valor Recibido no debe ser inferior del total de la factura");
            
                    else
                    {
                        if (Double.TryParse((Convert.ToDecimal(txtRecibido.Text) - valorFactura).ToString(), out value))
                            lblCambio.Text = String.Format(System.Globalization.CultureInfo.CurrentCulture, "{0:C2}", value);
                        lblCambioTitulo.Text = "Cambio: ";
                        int factura = logica.insertarFactura(txtNombre.Text, txtCelular.Text, txtDireccion.Text, valorFactura, user.Nombres + " " + user.Apellidos);
                        foreach (ProductoVenta p in listaProductosVenta)
                        {
                            logica.insertarVenta(factura, p.IdProducto, (int)p.Cantidad, (double)p.Valor, (double)p.Total);

                        }
                        //int factura = 100;
                        btnCheck.Enabled = false;

                        var confirmResult = MessageBox.Show("Desea Imprimir la factura?",
                                             "Confirmar Impresión!!",
                                             MessageBoxButtons.YesNo);
                        if (confirmResult == DialogResult.Yes)
                        {
                            LocalReport reporte = new LocalReport();
                            //reporte.ReportPath = @"..\..\rptFactura.rdlc";
                            reporte.ReportPath = ConfigurationManager.AppSettings["rutaReporteFactura"];
                            ReportParameter[] parameters = new ReportParameter[2];
                            parameters[0] = new ReportParameter("nombre", txtNombre.Text);
                            parameters[1] = new ReportParameter("facturaId", factura.ToString());
                            reporte.SetParameters(parameters);
                            ReportDataSource rds = new ReportDataSource("dsProductosVentas", ConvertToDataTable(listaProductosVenta));
                            reporte.DataSources.Add(rds);
                            Export(reporte);
                            Print();
                        }
                        else
                        {
                            MessageBox.Show("Venta Realizada Correctamente..");
                        }
                    }
                }


            }
            //frmReportFactura reporteFactura = new frmReportFactura();
            //reporteFactura.generarReporte(listaProductosVenta, txtNombre.Text, valorFactura, factura);
            //reporteFactura.Show();
            //MessageBox.Show("Factura generada con exito. Cambio: "+ String.Format(System.Globalization.CultureInfo.CurrentCulture, "{0:C2}", value) +" idFactura: "+factura);

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

        private void button2_Click(object sender, EventArgs e)
        {
            txtCelular.Text = "";
            txtNombre.Text = "";
            txtRecibido.Text = "";
            txtDireccion.Text = "";
            listaProductosVenta = new List<ProductoVenta>();
            UpdateTotal();
            cargarProductos();
            btnCheck.Enabled = true;
            lblCambioTitulo.Text = "";
            lblCambio.Text = "";
        }

        private void txtCodigoBarras_TextChanged(object sender, EventArgs e)
        {
            LogicaGo logica = new LogicaGo();
            producto = logica.obtenerProductoByCodigo(txtCodigoBarras.Text);
            if (producto != null && producto.Nombre != null && !txtCodigoBarras.Text.Equals(""))
            {
                lblId.Text = producto.IdProducto;
                lblNombre.Text = producto.Nombre;
                lblDescripcion.Text = "Descripción: " + producto.Descripcion;

                Double value;
                if (Double.TryParse(producto.Precio.ToString(), out value))
                    lblPrecio.Text = "Precio: " + String.Format(System.Globalization.CultureInfo.CurrentCulture, "{0:C2}", value);
                else
                    lblPrecio.Text = "Precio: " + String.Empty;
                cbUnidades.Value = 1;
                if (!producto.Servicio)
                    cbUnidades.Maximum = producto.Existencias;
                else
                    cbUnidades.Maximum = 1000;
                lblMaxUnidades.Text = "Max: " + producto.Existencias + " unidades";
                lblTipo.Text = "Tipo: " + producto.Tipo;
                txtCbEstado.SelectedValue = producto.IdEstado;
                txtCbCategoria.SelectedValue = producto.Categoria;
                txtCbSubCategoria.SelectedValue = producto.Subcategoria;
                lblEstado.Text = "Estado: " + txtCbEstado.Text;
                lblFecha.Text = "Fecha Ingreso: " + producto.Fecha.ToString();
                lblCategoria.Text = "Categoria: " + txtCbCategoria.Text;
                lblSubCategoria.Text = "SubCategoria: " + txtCbSubCategoria.Text;
                btnAdd.PerformClick();
                txtCodigoBarras.Text = "";
            }

        }

        private void buttonNewProduct_Click(object sender, EventArgs e)
        {
            frmProductos childForm = new frmProductos();
            //childForm.MdiParent = this;
            //childForm.WindowState = FormWindowState.Maximized;
            childForm.Show();
        }

        private void btnAddCustom_Click(object sender, EventArgs e)
        {
            //string x = Interaction.InputBox("hi", "hello", "nothing", 10, 10);
            //MessageBox.Show(x);
            FrmNuevoProductoCustom NuevoProducto = new FrmNuevoProductoCustom();
            NuevoProducto.DataSent +=NuevoProducto_DataSent;
            NuevoProducto.ShowDialog(this);
           
        }

        private void NuevoProducto_DataSent(string productoNew, string valor, decimal cantidad)
        {
            txtCelular.Text = productoNew;
        }

    }
}
