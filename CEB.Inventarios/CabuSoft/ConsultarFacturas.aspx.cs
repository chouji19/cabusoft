using CABU.Data;
using CABU.Data.entidades;
using CABU.Data.Logica;
using CABU.Data.Utils;
using CabuSoft.Utils;
using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CabuSoft
{
    public partial class ConsultarFacturas : System.Web.UI.Page
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
                    if (usuario.id_rol == 5 && !inicioSesion.validarPermiso(usuario.id_usuario, "FacturasC"))
                    {
                        Response.Redirect("Index.aspx");
                    }
                }
            }
        }

        protected void lknBusqueda_Click(object sender, EventArgs e)
        {
            llenarGrilla();
        }

        protected void gvFacturas_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Ver")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                int idFactura = Convert.ToInt32(gvFacturas.DataKeys[index].Value.ToString());
                Response.Redirect("DetalleFactura.aspx?factura=" + idFactura);
            }
            if (e.CommandName == "Print")
            {
                LocalReport localReport = new LocalReport();
                localReport.ReportPath = "rptFactura.rdlc";
                int index = Convert.ToInt32(e.CommandArgument);
                int idFactura = Convert.ToInt32(gvFacturas.DataKeys[index].Value.ToString());
                AdminFacturas af = new AdminFacturas();
                tblFacturas factura = af.obtenerFacturaByID(idFactura);
                if (factura != null)
                {
                    ReportParameter[] parameters = new ReportParameter[2];
                    parameters[0] = new ReportParameter("nombre", factura.nombre);
                    parameters[1] = new ReportParameter("facturaId", factura.id_factura.ToString());
                    localReport.SetParameters(parameters);
                    List<tblVentas> listaVentas = af.obtenerVentasByFactura(factura.id_factura);
                    List<ProductoVenta> listaProductos = new List<ProductoVenta>();
                    foreach (tblVentas item in listaVentas)
                    {
                        ProductoVenta pro = new ProductoVenta();
                        pro.Cantidad = item.cantidad.Value;
                        pro.IdProducto = item.id_producto.Value.ToString();
                        pro.Nombre = item.tblProductos.nombre;
                        pro.Total = Convert.ToDecimal(item.valor_total);
                        pro.Valor = Convert.ToDecimal(item.valor_unitario);
                        listaProductos.Add(pro);
                    }

                    ReportDataSource rds = new ReportDataSource("dsProductosVentas", ConvertDataTable.ConvertToDataTable(listaProductos));
                    localReport.DataSources.Add(rds);
                    ImprimirArchivos imp = new ImprimirArchivos();
                    imp.Export(localReport);
                    imp.Print();
                }
            }
        }

        protected void gvFacturas_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvFacturas.PageIndex = e.NewPageIndex;
            llenarGrilla();
        }

        public void llenarGrilla()
        {
            AdminFacturas af = new AdminFacturas();
            tblUsuarios usuario;
            if (Session["user"] != null)
            {
                usuario = (tblUsuarios)Session["user"];
                gvFacturas.DataSource = af.obtenerFacturasPorFechaYConcidencia(txtInicio.Text, txtFin.Text, txtFiltro.Text, usuario.id_sucursal.Value);
                gvFacturas.DataBind();
            }
        }
    }
}