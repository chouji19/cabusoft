using CEB.Inventarios.entidades;
using CEB.Inventarios.logica;
using Microsoft.Reporting.WinForms;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CEB.Inventarios
{
    public partial class VentaVista2 : Form
    {
        public VentaVista2()
        {
            InitializeComponent();
        }

        private void VentaVista2_Load(object sender, EventArgs e)
        {
            cargarProductos();
            listaProductosVenta = new List<ProductoVenta>();
            lblId.Text = "";
        }

        public void cargarProductos()
        {
            LogicaGo logica = new LogicaGo();
            List<VentasProductos> lista = logica.obtenerListaProductosImagenDisponibles(txtFiltro.Text);
            int indice = 0;
            listView1.Clear();
            imageList1.Images.Clear();
            listView1.View = View.LargeIcon;
            listView1.LargeImageList = imageList1;
            foreach (VentasProductos item in lista)
            {
                string rutaArchivo = ConfigurationManager.AppSettings["rutaImagenes"];
                Image image;
                if (File.Exists(rutaArchivo + item.RutaImagen))
                    image = Image.FromFile(rutaArchivo + item.RutaImagen);
                else
                    image = Image.FromFile(rutaArchivo + @"Images\Productos\default_product.png");
                imageList1.Images.Add(image);
                ListViewItem lvi = new ListViewItem();
                lvi.ImageIndex = indice;
                string nombre = String.Empty;
                if (item.Nombre.Length > 14)
                    nombre = item.Nombre.Substring(0, 15);
                else
                    nombre = item.Nombre;
                lvi.Text = item.Nombre;
                lvi.Name = item.IdProducto;
                listView1.Items.Add(lvi);
                indice++;
            }
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            if (listView1.SelectedItems.Count > 0)
            {
                ListViewItem item = listView1.SelectedItems[0];
                LogicaGo logica = new LogicaGo();
                lblId.Text = item.Name;
                producto = logica.ObtenerProducto(lblId.Text);
                if (producto != null)
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
                    cbUnidades.Maximum = !producto.Servicio ? producto.Existencias : 1000;
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
                    listView1.SelectedItems.Clear();
                }

            }
        }

        private Usuario user;

        internal Usuario User
        {
            get { return user; }
            set { user = value; }
        }


        private List<ProductoVenta> listaProductosVenta;
        private Producto producto;
        private decimal valorFactura;


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


        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (lblId.Text.Equals(""))
                MessageBox.Show("Seleccione producto");
            else
            {
                bool productoSeleccionado = false;
                ProductoVenta pv = listaProductosVenta.FirstOrDefault(p=>p.IdProducto == producto.IdProducto);
                if (pv != null)
                {
                    if (Convert.ToInt32(cbUnidades.Value) + pv.Cantidad <= pv.MaximoUnidades)
                    {
                        pv.Cantidad = pv.Cantidad + Convert.ToInt32(cbUnidades.Value);
                        pv.Total = pv.Cantidad * pv.Valor;
                        cargarDatosProductos();
                        var dataGridViewColumn = dgvProductos.Columns["Total"];
                        if (dataGridViewColumn != null)
                            dataGridViewColumn.DefaultCellStyle.Format = "c";
                        var gridViewColumn = dgvProductos.Columns["Valor"];
                        if (gridViewColumn != null)
                            gridViewColumn.DefaultCellStyle.Format = "c";
                        var viewColumn = dgvProductos.Columns["IdProducto"];
                        if (viewColumn != null)
                            viewColumn.Visible = false;
                        //dgvProductos.Columns["MaximoUnidades"].Visible = false;
                        valorFactura = 0;
                        updateTotal();
                    }
                    else {
                        MessageBox.Show("No puede ingresar mas de "+pv.MaximoUnidades+" Unidades");
                    }
                    productoSeleccionado = true;
                }
                if (productoSeleccionado) return;
                ProductoVenta auxProducto = new ProductoVenta();

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
                cargarDatosProductos();
                var column = dgvProductos.Columns["Total"];
                if (column != null) column.DefaultCellStyle.Format = "c";
                var dataGridViewColumn1 = dgvProductos.Columns["Valor"];
                if (dataGridViewColumn1 != null) dataGridViewColumn1.DefaultCellStyle.Format = "c";
                var gridViewColumn1 = dgvProductos.Columns["IdProducto"];
                if (gridViewColumn1 != null) gridViewColumn1.Visible = false;
                //dgvProductos.Columns["MaximoUnidades"].Visible = false;
                valorFactura = 0;
                updateTotal();
            }

        }

        private void updateTotal()
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

        private void cargarDatosProductos()
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

            string message = "Row " + e.RowIndex.ToString() + " is ";

            //Switch through the column names
            switch (dgvProductos.Columns[e.ColumnIndex].Name)
            {
                case "Eliminar":
                    message += "Eliminar you!";
                    foreach (ProductoVenta p in listaProductosVenta)
                    {
                        if (dgvProductos.Rows[e.RowIndex].Cells["IdProducto"].Value.ToString().Equals(p.IdProducto.ToString()))
                        {
                            dgvProductos.Rows.RemoveAt(e.RowIndex);
                            listaProductosVenta.Remove(p);
                            cargarProductos();
                            updateTotal();
                            break;
                        }

                    }

                    break;
                case "Editar":
                    message += "Editar you!";
                    bool correcto = true;
                    string texto = "";
                    do
                    {
                        texto = Interaction.InputBox("Cantidad:", "Cantidad", "");
                        int number1 = 0;
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
                        if (dgvProductos.Rows[e.RowIndex].Cells["IdProducto"].Value.ToString().Equals(p.IdProducto.ToString()))
                        {
                            listaProductosVenta.First(d => d.IdProducto == p.IdProducto).Cantidad = Convert.ToInt32(texto);
                            listaProductosVenta.First(d => d.IdProducto == p.IdProducto).Total = Convert.ToInt32(texto) * p.Valor;
                            dgvProductos.Rows[e.RowIndex].Cells["Cantidad"].Value = p.Cantidad;
                            dgvProductos.Rows[e.RowIndex].Cells["Total"].Value = Convert.ToInt32(texto) * p.Valor;
                            updateTotal();
                            break;
                        }

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
            updateTotal();
            cargarProductos();
            btnCheck.Enabled = true;
            lblCambioTitulo.Text = "";
            lblCambio.Text = "";
        }

        private void txtCodigoBarras_TextChanged(object sender, EventArgs e)
        {
            LogicaGo logica = new LogicaGo();
            producto = logica.obtenerProductoByCodigo(txtCodigoBarras.Text);
            if (producto == null || producto.Nombre == null || txtCodigoBarras.Text.Equals("")) return;
            lblId.Text = producto.IdProducto;
            lblNombre.Text = producto.Nombre;
            lblDescripcion.Text = "Descripción: " + producto.Descripcion;

            Double value;
            if (Double.TryParse(producto.Precio.ToString(), out value))
                lblPrecio.Text = "Precio: " + String.Format(System.Globalization.CultureInfo.CurrentCulture, "{0:C2}", value);
            else
                lblPrecio.Text = "Precio: " + String.Empty;
            cbUnidades.Value = 1;
            cbUnidades.Maximum = !producto.Servicio ? producto.Existencias : 1000;
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

        private void buttonNewProduct_Click(object sender, EventArgs e)
        {
            frmProductos childForm = new frmProductos();
            //childForm.MdiParent = this;
            //childForm.WindowState = FormWindowState.Maximized;
            childForm.Show();
        }

        
    }
}

