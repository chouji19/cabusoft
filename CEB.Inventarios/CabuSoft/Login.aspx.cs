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
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Login1_Authenticate(object sender, AuthenticateEventArgs e)
        {
            InicioSesion sesion = new InicioSesion();
            tblUsuarios usuario = sesion.iniciarSesion(Login1.UserName, Login1.Password);
            if (usuario != null)
            {
                e.Authenticated = true;
                Session["user"] = usuario;
                Response.Redirect(Request.Url.AbsoluteUri);
            }
            else
            {
                e.Authenticated = false;
            }
        }
    }
}