using CABU.Data;
using CABU.Data.Logica;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CabuSoft
{
    public partial class IngresosGastos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            { 
                AdminMovimientos am = new AdminMovimientos();
                tblUsuarios usuario;
                if (Session["user"] != null)
                {
                    usuario = (tblUsuarios)Session["user"];
                    InicioSesion inicioSesion = new InicioSesion();
                    if (usuario.id_rol==5 && !inicioSesion.validarPermiso(usuario.id_usuario, "MovimientosES"))
                    {
                        Response.Redirect("Index.aspx");
                    }
                    ddDescripcion.DataSource = am.obtenerMovimientosByEmpresa(usuario.tblSucursales.id_empresa);
                    ddDescripcion.DataBind();
                }
            }
        }

        protected void lknInsertar_Click(object sender, EventArgs e)
        {
            AdminMovimientos aMov = new AdminMovimientos();
            if (txtDpFecha.Text != "" && txtTipo.SelectedValue != "" && ddDescripcion.SelectedValue!="" && txtPrecio.Text != "")
            {
                tblUsuarios usuario;
                if (Session["user"] != null)
                {
                    usuario = (tblUsuarios)Session["user"];
                    gvIngresos.DataSource = aMov.insertarMovimiento(txtDpFecha.Text, txtTipo.SelectedValue, ddDescripcion.SelectedValue, txtPrecio.Text, usuario.id_sucursal.Value);
                    gvIngresos.DataBind();
                }
            }
            else
            {
                pnlAdvertencia.CssClass = "alert alert-success alert-dismissible";
                lblResultadoProceso.Text = "Debe Diligenciar todos los campos";
                pnlAdvertencia.Visible = true;
            }

        }

        protected void lknBusqueda_Click(object sender, EventArgs e)
        {
            AdminMovimientos aMov = new AdminMovimientos();
            tblUsuarios usuario;
            if (Session["user"] != null)
            {
                usuario = (tblUsuarios)Session["user"];
                gvIngresos.DataSource = aMov.consultarMovimientos(txtDpFecha.Text, txtTipo.SelectedValue, txtDescripcion.Text, txtPrecio.Text, usuario.id_sucursal.Value);
                gvIngresos.DataBind();
            }
        }

        protected void gvIngresos_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            tblUsuarios usuario;
            if (Session["user"] != null)
            {
                usuario = (tblUsuarios)Session["user"];
                AdminMovimientos aMov = new AdminMovimientos();
                gvIngresos.PageIndex = e.NewPageIndex;
                gvIngresos.DataSource = aMov.consultarMovimientos(txtDpFecha.Text, txtTipo.SelectedValue, txtDescripcion.Text, txtPrecio.Text,usuario.id_sucursal.Value);
                gvIngresos.DataBind();
            }
        }

        protected void lknBtnAddCategoria_Click(object sender, EventArgs e)
        {
            txtDescripcion.Visible = true;
            lknAddDescripcion.Visible = true;
        }

        protected void lknAddDescripcion_Click(object sender, EventArgs e)
        {
            AdminCategorias aCat = new AdminCategorias();
            if (txtDescripcion.Text != "")
            {
                ddDescripcion.Items.Add(new ListItem(txtDescripcion.Text, txtDescripcion.Text));
                //ddDescripcion.Items.FindByText(txtDescripcion.Text).Selected = true;
                ddDescripcion.SelectedValue = txtDescripcion.Text;
                txtDescripcion.Visible = false;
                lknAddDescripcion.Visible = false;
                
            }
        }
    }
}