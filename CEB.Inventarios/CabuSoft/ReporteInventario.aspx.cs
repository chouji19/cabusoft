using CABU.Data;
using CABU.Data.Logica;
using CABU.Data.Utils;
using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CabuSoft
{
    public partial class ReporteInventario : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            AdminProductos ap = new AdminProductos();      
            if (!IsPostBack)
            {
                tblUsuarios usuario;
                if (Session["user"] != null)
                {
                    usuario = (tblUsuarios)Session["user"];
                    InicioSesion inicioSesion = new InicioSesion();
                    if (usuario.id_rol == 5 && !inicioSesion.validarPermiso(usuario.id_usuario, "ReporteI"))
                    {
                        Response.Redirect("Index.aspx");
                    }
                    ReportViewer1.ProcessingMode = ProcessingMode.Local;
                    ReportViewer1.LocalReport.ReportPath = Server.MapPath("rptInventario.rdlc");
                    List<tblProductos> lista = ap.obtenerProductosAll("", "", usuario.id_sucursal.Value);
                    ReportDataSource rds = new ReportDataSource("dsProductos", ConvertToDataTable(lista));
                    ReportViewer1.LocalReport.DataSources.Clear();
                    ReportViewer1.LocalReport.DataSources.Add(rds);
                }
            }
        }



        public DataTable ConvertToDataTable(List<tblProductos> data)
        {
            DataTable table = new DataTable();
            table.Columns.Add("nombre");
            table.Columns.Add("descripcion");
            table.Columns.Add("precio");
            table.Columns.Add("existencias");
            table.Columns.Add("marca");
            table.Columns.Add("estado");
            table.Columns.Add("categoria");
            table.Columns.Add("subcategoria");
            foreach (tblProductos item in data)
            {
                DataRow row = table.NewRow();
                row["nombre"] = item.nombre;
                row["descripcion"] = item.descripcion;
                row["precio"] = item.precio.ToString();
                row["existencias"] = item.existencias.ToString();
                row["marca"] = item.marca;
                row["estado"] = item.tblEstadosProductos.nombre;
                row["categoria"] = item.tblCategorias != null ? item.tblCategorias.nombre : "";
                row["subcategoria"] = item.tblSubCategorias != null ? item.tblSubCategorias.nombre : "";
                table.Rows.Add(row);
            }
            return table;

        }

        protected void txtCbCategoria_SelectedIndexChanged(object sender, EventArgs e)
        {
            obtenerSubCategorias(txtCbCategoria.SelectedValue);
        }

        public void obtenerSubCategorias(string categoria)
        {
            if (categoria != "")
            {
                AdminProductos aProducto = new AdminProductos();
                txtCbSubCategoria.Items.Clear();
                txtCbSubCategoria.DataSource = aProducto.obtenerListaSubCategorias(categoria);
                txtCbSubCategoria.DataTextField = "Nombre";
                txtCbSubCategoria.DataValueField = "id_subcategoria";
                txtCbSubCategoria.DataBind();
                txtCbSubCategoria.Items.Add(new ListItem("Select", ""));
                txtCbSubCategoria.Items.FindByText("Select").Selected = true;
            }
            else
            {
                txtCbSubCategoria.Items.Clear();
                txtCbSubCategoria.Items.Add(new ListItem("Select", ""));
                txtCbSubCategoria.Items.FindByText("Select").Selected = true;
            }
        }

        protected void lknBusqueda_Click(object sender, EventArgs e)
        {
            generarReporte();
        }

        private void generarReporte()
        {
            tblUsuarios usuario;
            if (Session["user"] != null)
            {
                usuario = (tblUsuarios)Session["user"];
                AdminProductos ap = new AdminProductos();
                ReportViewer1.ProcessingMode = ProcessingMode.Local;
                ReportViewer1.LocalReport.ReportPath = Server.MapPath("rptInventario.rdlc");
                List<tblProductos> lista = ap.obtenerProductosAll(txtCbCategoria.SelectedValue.ToString(), txtCbSubCategoria.SelectedValue.ToString(), usuario.id_sucursal.Value);
                ReportDataSource rds = new ReportDataSource("dsProductos", ConvertToDataTable(lista));
                ReportViewer1.LocalReport.DataSources.Clear();
                ReportViewer1.LocalReport.DataSources.Add(rds);
            }
        }
    }
}