using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CEB.Inventarios.logica;
using Microsoft.Reporting.WinForms;

namespace CEB.Inventarios
{
    public partial class frmReporteIngresosGastos : Form
    {
        public frmReporteIngresosGastos()
        {
            InitializeComponent();
        }

        private void frmReporteIngresosGastos_Load(object sender, EventArgs e)
        {
            //// TODO: esta línea de código carga datos en la tabla 'dsIngresosGastos.vIngresosGastos' Puede moverla o quitarla según sea necesario.
            //this.vIngresosGastosTableAdapter.Fill(this.dsIngresosGastos.vIngresosGastos);
            //// TODO: esta línea de código carga datos en la tabla 'dbProductos.vProductos' Puede moverla o quitarla según sea necesario.
            //this.vProductosTableAdapter.Fill(this.dbProductos.vProductos);

            //this.reportViewer1.RefreshReport();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            LogicaGo logica = new LogicaGo();
            DataTable datos = logica.obtenerIngresosGastos(txtDpFechaIni.Text , txtDpFechaFin.Text);
            ReportParameter[] parameters = new ReportParameter[2];
            parameters[0] = new ReportParameter("FechaIni", txtDpFechaIni.Text);
            parameters[1] = new ReportParameter("FechaFin", txtDpFechaFin.Text);
            reportViewer1.LocalReport.SetParameters(parameters);
            ReportDataSource rds = new ReportDataSource("dsIngresosGastos", datos);
            this.reportViewer1.LocalReport.DataSources.Clear();
            this.reportViewer1.LocalReport.DataSources.Add(rds);
            this.reportViewer1.LocalReport.Refresh();
            this.reportViewer1.RefreshReport();
        }
    }
}
