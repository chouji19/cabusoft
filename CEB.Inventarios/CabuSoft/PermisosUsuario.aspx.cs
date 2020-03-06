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
    public partial class PermisosUsuario : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                cargarPermisos();
            }

        }

        private void cargarPermisos()
        {
            tblUsuarios usuario;
            if (Session["user"] != null)
            {
                if (Request.QueryString["usuario"] != null)
                {
                    usuario = (tblUsuarios)Session["user"];
                    InicioSesion inicioSesion = new InicioSesion();
                    if (usuario.id_rol == 5 && !inicioSesion.validarPermiso(usuario.id_usuario, "Sucursales"))
                    {
                        Response.Redirect("Index.aspx");
                    }
                    List<tblPermisosUsuarios> permisos = new List<tblPermisosUsuarios>();
                    AdminUsuarios aUser = new AdminUsuarios();
                    cbPermisos.DataSource = aUser.obtenerPermisos();
                    cbPermisos.DataValueField = "id_permiso";
                    cbPermisos.DataTextField = "descripcion";
                    cbPermisos.DataBind();
                    int idUsuario = Convert.ToInt16(Request.QueryString["usuario"]);
                    permisos = aUser.obtenerPermisosUsuarios(idUsuario);
                    if (permisos.Count > 0 && cbPermisos.Items.Count > 0)
                    {
                        foreach (tblPermisosUsuarios item in permisos)
                        {
                            cbPermisos.Items.FindByValue(item.id_permiso.ToString()).Selected = true;
                        }
                    }
                }
            }
        }

        protected void btnActualizar_Click(object sender, EventArgs e)
        {
            AdminUsuarios aUser = new AdminUsuarios();
            if (Request.QueryString["usuario"] != null)
            {
                int idUsuario = Convert.ToInt16(Request.QueryString["usuario"]);
                aUser.limpiarPermisos(idUsuario);
                tblUsuarios usuario = new tblUsuarios();
                usuario = aUser.obtenerUsuarioById(idUsuario);
                lblUsuario.Text = usuario.username;
                foreach (ListItem item in cbPermisos.Items)
                {
                    if (item.Selected)
                    {
                        aUser.agregarPermiso(idUsuario,item.Value);
                    }
                }
                cargarPermisos();
                pnlAdvertencia.CssClass = "alert alert-success alert-dismissible";
                lblResultadoProceso.Text = "Los permisos han sido actualizados";
                pnlAdvertencia.Visible = true;
            }
        }

    }
}