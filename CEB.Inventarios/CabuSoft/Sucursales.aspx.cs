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
    public partial class Sucursales : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            cargarSucursales();
        }

        protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            string mensaje = String.Empty;
            int idProducto = Convert.ToInt32(gvSucrusales.DataKeys[e.RowIndex].Value.ToString());
            GridViewRow row = (GridViewRow)gvSucrusales.Rows[e.RowIndex];
        }

        public void cargarSucursales()
        {
            tblUsuarios usuario;
            if (Session["user"] != null)
            {
                usuario = (tblUsuarios)Session["user"];
                InicioSesion inicioSesion = new InicioSesion();
                if (usuario.id_rol == 5 && !inicioSesion.validarPermiso(usuario.id_usuario, "Sucursales"))
                {
                    Response.Redirect("Index.aspx");
                }
                dsSucursales.WhereParameters["id_empresa"].DefaultValue = usuario.tblSucursales.id_empresa.ToString();
            }
        }

        protected void Insert(object sender, EventArgs e)
        {
            tblUsuarios usuario;
            if (Session["user"] != null)
            {
                usuario = (tblUsuarios)Session["user"];
                AdminSucursales aSuc = new AdminSucursales();
                int res=0;
                if(txtNombre.Text!="" && txtDireccion.Text !="" && txtEmail.Text!="" )
                 res = aSuc.insertarSucursal(txtNombre.Text, txtDireccion.Text, txtTelefono.Text, txtEmail.Text, usuario.tblSucursales.id_empresa.Value);
                if (res > 0)
                {
                    cargarSucursales();
                    Response.Redirect(Request.Url.AbsoluteUri);
                }
            }
        }

        protected void gvSucrusales_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Usuarios")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                int idSucursal = Convert.ToInt32(gvSucrusales.DataKeys[index].Value.ToString());
                Response.Redirect("Usuarios.aspx?sucursal=" + idSucursal);
            }
        }

    }
}