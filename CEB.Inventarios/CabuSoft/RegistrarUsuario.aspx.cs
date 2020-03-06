using CABU.Data.Logica;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CabuSoft
{
    public partial class RegistrarUsuario : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Wizard1_FinishButtonClick(object sender, WizardNavigationEventArgs e)
        {
            if (txtCaptcha.Text != "")
            {
                Captcha1.ValidateCaptcha(txtCaptcha.Text.Trim());
                if (Captcha1.UserValidated)
                {
                    AdminUsuarios aU = new AdminUsuarios();
                    if (aU.validarUsuarioEmpresa(txtUsername.Text))
                    {
                        lblUserExiste.Visible = false;
                        int idEmpresa = aU.insertarEmpresa(txtNombre.Text, txtDDTipoId.SelectedValue, txtIdentificacion.Text,
                            txtDireccion.Text, txtContacto.Text, txtTelefono.Text, txtCelular.Text, txtMail.Text);
                        if (idEmpresa > 0)
                        {
                            int idSucursal = aU.insertarSucursalPrincipal(idEmpresa);
                            int idUsuario = aU.insertarUsuario(txtIdUsuario.Text, txtUNombres.Text, txtApellidos.Text,
                                txtUCelular.Text, txtPassword.Text, 4, idSucursal, txtUsername.Text,txtUEmail.Text);
                            if (idUsuario > 0)
                            {
                                pnlProgress2.CssClass = "progress-bar  progress-bar-striped progress100";
                                pnlAdvertencia.CssClass = "alert alert-success alert-dismissible";
                                lblResultadoProceso.Text = "Usuario Creado Con Exito... Se enviara un correo para la activacion de la cuenta";
                                pnlAdvertencia.Visible = true;
                            }
                        }
                    }
                    else
                    {
                        lblUserExiste.Visible = true;
                    }
                    lblMessage.Text = "";
                }
                else
                {
                    lblMessage.Text = "Captcha Incorrecto";
                }
            }
            else
            {
                lblMessage.Text = "Ingrese el Captcha";
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


        
    }
}