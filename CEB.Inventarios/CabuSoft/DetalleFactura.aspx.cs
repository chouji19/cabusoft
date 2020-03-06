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
    public partial class DetalleFactura : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                tblUsuarios usuario;
                if (Session["user"] != null)
                {
                    //Validar que no consulte cualquier factura //pending
                    usuario = (tblUsuarios)Session["user"];
                    InicioSesion inicioSesion = new InicioSesion();
                    if (usuario.id_rol == 5 && !inicioSesion.validarPermiso(usuario.id_usuario, "FacturasC"))
                    {
                        Response.Redirect("Index.aspx");
                    }
                    if (Request.QueryString["factura"] != null)
                    {
                        int idFactura = Convert.ToInt32(Request.QueryString["factura"]);
                        AdminFacturas af = new AdminFacturas();
                        tblFacturas factura = af.obtenerFacturaByID(idFactura);
                        if (factura != null && factura.id_sucursal==usuario.id_sucursal)
                        {
                            lblCelular.Text = factura.celular;
                            lblComprador.Text = factura.nombre;
                            lblDireccion.Text = factura.direccion;
                            lblFecha.Text = factura.fecha.ToString();
                            lblIdFactura.Text = factura.id_factura.ToString();
                            lblIva.Text = String.Format(System.Globalization.CultureInfo.CurrentCulture, "{0:C2}", factura.iva);
                            lblUsuario.Text = factura.usuario;
                            lblValorBruto.Text = String.Format(System.Globalization.CultureInfo.CurrentCulture, "{0:C2}", factura.valor_bruto);
                            lblValorNeto.Text = String.Format(System.Globalization.CultureInfo.CurrentCulture, "{0:C2}", factura.valor_neto);
                            gvVentas.DataSource = af.obtenerVentasByFactura(factura.id_factura);
                            gvVentas.DataBind();

                        }
                    }
                }
            }
        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {

            LocalReport localReport = new LocalReport();

            localReport.ReportPath = "rptFactura.rdlc";

            if (Request.QueryString["factura"] != null)
            {
                int idFactura = Convert.ToInt32(Request.QueryString["factura"]);
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

        
    }
}