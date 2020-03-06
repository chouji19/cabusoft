using CABU.Data;
using CABU.Data.Logica;
using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CabuSoft
{
    public partial class ReporteIngresosGastos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                tblUsuarios usuario;
                if (Session["user"] != null)
                {
                    usuario = (tblUsuarios)Session["user"];
                    InicioSesion inicioSesion = new InicioSesion();
                    if (usuario.id_rol == 5 && !inicioSesion.validarPermiso(usuario.id_usuario, "ReporteIG"))
                    {
                        Response.Redirect("Index.aspx");
                    }
                }
            }
        }

        protected void lknBusqueda_Click(object sender, EventArgs e)
        {
            tblUsuarios usuario;
            if (Session["user"] != null)
            {
                usuario = (tblUsuarios)Session["user"];
                ReportViewer1.Visible = true;
                AdminMovimientos aMov = new AdminMovimientos();
                ReportViewer1.ProcessingMode = ProcessingMode.Local;
                DataTable datos = aMov.obtenerIngresosGastos(txtDesde.Text, txtHasta.Text, usuario.id_sucursal.Value);
                ReportViewer1.LocalReport.ReportPath = Server.MapPath("rptIngresosGastos.rdlc");
                ReportParameter[] parameters = new ReportParameter[2];
                parameters[0] = new ReportParameter("FechaIni", txtDesde.Text);
                parameters[1] = new ReportParameter("FechaFin", txtHasta.Text);
                ReportViewer1.LocalReport.SetParameters(parameters);
                ReportDataSource rds = new ReportDataSource("dsIngresosGastos", datos);
                ReportViewer1.LocalReport.DataSources.Clear();
                ReportViewer1.LocalReport.DataSources.Add(rds);
            }
        }
    }
}