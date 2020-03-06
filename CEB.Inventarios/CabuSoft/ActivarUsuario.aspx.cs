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
    public partial class ActivarUsuario : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["user"] == null)
                {
                    AdminUsuarios admin = new AdminUsuarios();
                    tblUsuarios usu = new tblUsuarios();
                    string usuario = Request.QueryString["add"];
                    usu = admin.obtenerUsuarioEncriptado(usuario);
                    lblUsuario.Text = usu.username;
                    if (usu.activo.Value)
                        Response.Redirect("Index.aspx");
                }
            }
        }

        protected void txtPassword_TextChanged(object sender, EventArgs e)
        {
            AdminUsuarios aUs = new AdminUsuarios();
            if (aUs.ValidatePassword(txtPassword.Text))
            {
                pnlPassword.CssClass = "form-group has-success";
                lblErrorPass.Visible = false;

            }
            else
            {
                pnlPassword.CssClass = "form-group has-error  has-feedback";
                lblErrorPass.Visible = true;
            }
        }

        protected void lknGuardar_Click(object sender, EventArgs e)
        {
            if (txtCaptcha.Text != "")
            {
                Captcha1.ValidateCaptcha(txtCaptcha.Text.Trim());
                if (Captcha1.UserValidated)
                {
                    AdminUsuarios admin = new AdminUsuarios();
                    if (Session["user"] == null)
                    {
                        tblUsuarios usu = new tblUsuarios();
                        string usuario = Request.QueryString["add"];
                        usu = admin.obtenerUsuarioEncriptado(usuario);
                        admin.actualizarPasswordActivacion(usu.id_usuario, txtPassword.Text);
                        pnlAdvertencia.CssClass = "alert alert-success alert-dismissible";
                        lblResultadoProceso.Text = "La contraseña ha sido actualizada con exito, Ya puede iniciar Sesió en la plataforma con el nombre de usuario y contraseña correspondientes";
                        pnlAdvertencia.Visible = true;
                    }
                    else
                    {
                        pnlAdvertencia.CssClass = "alert alert-success alert-dismissible";
                        lblResultadoProceso.Text = "No se puede Realizar la accion dado que ya ha iniciado sesion con un usuario diferente.";
                        pnlAdvertencia.Visible = true;
                    }
                }
            }
        }
    }
}