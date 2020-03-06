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
    public partial class Usuarios : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
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
                if (Request.QueryString["sucursal"] != null && usuario.id_rol == 4)
                {
                    string sucursal = Request.QueryString["sucursal"];
                    cargarUsuarios(sucursal);
                    AdminSucursales admin = new AdminSucursales();
                    tblSucursales suc = admin.obtenerSucursalById(Convert.ToInt16(sucursal));
                    lblSucursal.Text = suc.nombre;
                }
            }
        }

        private void cargarUsuarios(string idSucursal)
        {
            dsUsuarios.WhereParameters["id_sucursal"].DefaultValue = idSucursal;
        }

        protected void Insert(object sender, EventArgs e)
        {
            tblUsuarios usuario;
            if (Session["user"] != null)
            {
                usuario = (tblUsuarios)Session["user"];
                string url = HttpContext.Current.Request.Url.Authority;
                AdminUsuarios aUser = new AdminUsuarios();
                string sucursal = Request.QueryString["sucursal"];
                AdminUsuarios admin = new AdminUsuarios();
                if (admin.validarUsuarioEmpresa(txtUsername.Text))
                {
                    int idUsuario = aUser.insertarUsuarioActivacion(txtIdentificacion.Text, txtNombre.Text,
                        txtApellidos.Text, txtCelular.Text, 5, Convert.ToInt16(sucursal), txtUsername.Text, txtEmail.Text, url);
                    if (idUsuario > 0)
                    {
                        pnlAdvertencia.CssClass = "alert alert-success alert-dismissible";
                        lblResultadoProceso.Text = "Usuario Creado Con Exito... Se enviara un correo para la activacion de la cuenta";
                        pnlAdvertencia.Visible = true;
                    }
                }
                else
                {
                    lblUserExiste.Visible = true;
                }
            }
        }

        protected void gvUsuarios_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Permisos")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                int idUsuario = Convert.ToInt32(gvUsuarios.DataKeys[index].Value.ToString());
                Response.Redirect("PermisosUsuario.aspx?usuario=" + idUsuario);
            }
        }
    }
}