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
using Microsoft.Reporting.WinForms;
using System.Configuration;
using System.Drawing.Printing;
using System.IO;
using System.Drawing.Imaging;

namespace CEB.Inventarios
{
    public partial class frmConsultaFacturas : Form
    {
        public frmConsultaFacturas()
        {
            InitializeComponent();
        }

        private Factura factura;
        private List<ProductoVenta> listaProductosVenta;
        DataTable datos;

        public void cargarFacturas()
        {
            LogicaGo logica = new LogicaGo();
            lbFacturas.DataSource = logica.obtenerListaFacturas(txtFiltro.Text);
            if (lbFacturas.Items.Count > 0)
                lbFacturas.SelectedIndex = 0;
            lbFacturas.DisplayMember = "Nombre";
            lbFacturas.ValueMember = "Id";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            LogicaGo logica = new LogicaGo();
            lbFacturas.DataSource = logica.obtenerListaFacturas(txtDpFechaIni.Text, txtDpFechaFin.Text);
            if (lbFacturas.Items.Count > 0)
                lbFacturas.SelectedIndex = 0;
            lbFacturas.DisplayMember = "Nombre";
            lbFacturas.ValueMember = "Id";
        }

        private void txtFiltro_TextChanged(object sender, EventArgs e)
        {
            cargarFacturas();
        }

        private void lbFacturas_SelectedIndexChanged(object sender, EventArgs e)
        {
            LogicaGo logica = new LogicaGo();
            lblFacturaId.Text = "Factura Numero: " + lbFacturas.SelectedValue.ToString();
            factura = logica.obtenerFactura(lbFacturas.SelectedValue.ToString());
            if (factura != null)
            {
                lblComprador.Text = factura.Nombre;
                lblCelular.Text = "Celular: " + factura.Celular;
                lblDireccion.Text = "Dirección: " + factura.Direccion;
                Decimal value;
                if (Decimal.TryParse(factura.ValorBruto.ToString(), out value))
                    lblValorBruto.Text = "Valor Bruto: " + String.Format(System.Globalization.CultureInfo.CurrentCulture, "{0:C2}", value);
                else
                    lblValorBruto.Text = "Valor Bruto: $0";
                if (Decimal.TryParse(factura.Iva.ToString(), out value))
                    lblIVA.Text = "IVA: " + String.Format(System.Globalization.CultureInfo.CurrentCulture, "{0:C2}", value);
                else
                    lblIVA.Text = "IVA: $0";
                if (Decimal.TryParse(factura.ValorTotal.ToString(), out value))
                    lblTotal.Text = "Total: " + String.Format(System.Globalization.CultureInfo.CurrentCulture, "{0:C2}", value);
                else
                    lblTotal.Text = "Total: $0";
                lblFecha.Text = "Fecha: " + factura.Fecha;
                lblUsuario.Text = "Usuario: " + factura.Usuario;
                datos = logica.obteverProductosVenta(lbFacturas.SelectedValue.ToString());
                dgvProductos.DataSource = datos;
                listaProductosVenta = new List<ProductoVenta>();
                ProductoVenta pv;
                foreach(DataRow dt in datos.Rows)
                {
                    pv = new ProductoVenta();
                    pv.Nombre = dt["nombre"].ToString();
                    pv.Cantidad = Convert.ToDecimal(dt["cantidad"].ToString());
                    pv.Total = Convert.ToDecimal(dt["valor_total"].ToString());
                    listaProductosVenta.Add(pv);
                }
                btnReimprimir.Enabled = true;
            }
        }

        private void btnReimprimir_Click(object sender, EventArgs e)
        {
            LogicaGo logica = new LogicaGo();
            var confirmResult = MessageBox.Show("Desea Imprimir la factura?",
                                 "Confirmar Impresión!!",
                                 MessageBoxButtons.YesNo);
            if (confirmResult == DialogResult.Yes)
            {
                if(factura==null)
                {
                    factura = logica.obtenerFactura(lbFacturas.SelectedValue.ToString());
                }
                else
                {
                    LocalReport reporte = new LocalReport();
                    //reporte.ReportPath = @"..\..\rptFactura.rdlc";
                    reporte.ReportPath = ConfigurationManager.AppSettings["rutaReporteFactura"];
                    ReportParameter[] parameters = new ReportParameter[2];

                    parameters[0] = new ReportParameter("nombre", factura.Nombre);
                    parameters[1] = new ReportParameter("facturaId", factura.IdFactura);
                    reporte.SetParameters(parameters);
                    ReportDataSource rds = new ReportDataSource("dsProductosVentas", ConvertToDataTable(listaProductosVenta));
                    reporte.DataSources.Add(rds);
                    Export(reporte);
                    Print();
                }
            }
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


        private Stream CreateStream(string name, string fileNameExtension, Encoding encoding, string mimeType, bool willSeek)
        {
            Stream stream = new MemoryStream();
            m_streams.Add(stream);
            return stream;
        }

        private int m_currentPageIndex;
        private IList<Stream> m_streams;


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
    }
}
