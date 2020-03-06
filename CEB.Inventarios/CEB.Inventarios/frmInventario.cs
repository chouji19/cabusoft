using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.Reporting.WinForms;
using CEB.Inventarios.logica;

namespace CEB.Inventarios
{
    public partial class frmInventario : Form
    {
        public frmInventario()
        {
            InitializeComponent();
            LogicaGo logica = new LogicaGo();
            DataTable datos = logica.obtenerProductosAll();
            ReportDataSource rds = new ReportDataSource("dsProductos", datos);
            this.reportViewer1.LocalReport.DataSources.Clear();
            this.reportViewer1.LocalReport.DataSources.Add(rds);
            this.reportViewer1.LocalReport.Refresh();
            this.reportViewer1.RefreshReport();
        }

        private void frmInventario_Load(object sender, EventArgs e)
        {
            // TODO: esta línea de código carga datos en la tabla 'dbProductos.vProductos' Puede moverla o quitarla según sea necesario.
            this.vProductosTableAdapter.Fill(this.dbProductos.vProductos);

            this.reportViewer1.RefreshReport();
        }
    }
}
