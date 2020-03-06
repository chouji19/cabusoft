using CABU.Data;
using CABU.Data.Logica;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CabuSoft
{
    public partial class Site1 : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //if (!this.Page.User.Identity.IsAuthenticated)
            //{
            //    FormsAuthentication.RedirectToLoginPage();
            //}

            tblUsuarios usuario;
            if (Session["user"] != null)
            {
                usuario = (tblUsuarios)Session["user"];
                lknCrearUsuario.Visible = false;
                lknIniciarSesion.Visible = false;
                lblUser.Text = usuario.nombres;
                pnlMenuUser.Visible = true;
                cargarPermisos();
            }
        }

        private void cargarPermisos()
        {
            InicioSesion inicioSesion = new InicioSesion();
            tblUsuarios usuario;
            if (Session["user"] != null)
            {
                usuario = (tblUsuarios)Session["user"];
                if (usuario.id_rol == 1 || usuario.id_rol == 4)
                {
                    lknProducto.Visible = true;
                    lknCategorias.Visible = true;
                    lknMenuVentas.Visible = true;
                    lknVentas.Visible = true;
                    lknVentas2.Visible = true;
                    lknConsultarFacturas.Visible = true;
                    lknMovimientos.Visible = true;
                    lknIngresosGastos.Visible = true;
                    lknMovProductos.Visible = true;
                    lknReportes.Visible = true;
                    lknReporteInventario.Visible = true;
                    lknReporteIngresosGastos.Visible = true;
                    lknSucursales.Visible = true;
                }
                else
                {
                    if (inicioSesion.validarPermiso(usuario.id_usuario, "ProductosR") || inicioSesion.validarPermiso(usuario.id_usuario, "ProductosRW"))
                    {
                        lknProducto.Visible = true;
                    }
                    if (inicioSesion.validarPermiso(usuario.id_usuario, "CategoriasR") || inicioSesion.validarPermiso(usuario.id_usuario, "CategoriasRW"))
                    {
                        lknCategorias.Visible = true;
                    }
                    if (inicioSesion.validarPermiso(usuario.id_usuario, "Ventas"))
                    {
                        lknMenuVentas.Visible = true;
                        lknVentas.Visible = true;
                        lknVentas2.Visible = true;
                    }
                    if (inicioSesion.validarPermiso(usuario.id_usuario, "FacturasC"))
                    {
                        lknMenuVentas.Visible = true;
                        lknConsultarFacturas.Visible = true;
                    }
                    if (inicioSesion.validarPermiso(usuario.id_usuario, "MovimientosES"))
                    {
                        lknMovimientos.Visible = true;
                        lknIngresosGastos.Visible = true;
                    }
                    if (inicioSesion.validarPermiso(usuario.id_usuario, "MovimientosP"))
                    {
                        lknMovimientos.Visible = true;
                        lknMovProductos.Visible = true;
                    }
                    if (inicioSesion.validarPermiso(usuario.id_usuario, "ReporteI"))
                    {
                        lknReportes.Visible = true;
                        lknReporteInventario.Visible = true;
                    }
                    if (inicioSesion.validarPermiso(usuario.id_usuario, "ReporteIG"))
                    {
                        lknReportes.Visible = true;
                        lknReporteIngresosGastos.Visible = true;
                    }
                    if (inicioSesion.validarPermiso(usuario.id_usuario, "Sucursales"))
                    {
                        lknSucursales.Visible = true;
                    }
                }
            }
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            InicioSesion sesion = new InicioSesion();
            tblUsuarios usuario = sesion.iniciarSesion(txtCorreo.Text, txtPassword.Text);
            if (usuario != null)
            {
                Session["user"] = usuario;
                Response.Redirect(Request.Url.AbsoluteUri);
            }
        }
    }
}