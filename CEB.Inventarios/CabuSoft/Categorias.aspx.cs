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
    public partial class Categorias : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                tblUsuarios usuario;
                if (Session["user"] != null)
                {
                    usuario = (tblUsuarios)Session["user"];
                    dsListaCategorias.WhereParameters["id_empresa"].DefaultValue = usuario.tblSucursales.id_empresa.ToString();
                    InicioSesion inicioSesion = new InicioSesion();
                    if (usuario.id_rol == 5 && !inicioSesion.validarPermiso(usuario.id_usuario, "CategoriasR") && !inicioSesion.validarPermiso(usuario.id_usuario, "CategoriasRW"))
                    {
                        Response.Redirect("Index.aspx");
                    }
                }
            }
        }

        protected void lknBusqueda_Click(object sender, EventArgs e)
        {
            dsListaCategorias.WhereParameters["filtro"].DefaultValue = txtBusqueda.Text;
        }

        protected void txtListaProductos_SelectedIndexChanged(object sender, EventArgs e)
        {
            AdminCategorias aCat = new AdminCategorias();
            tblCategorias categoria = aCat.obtenerCategoriaById(Convert.ToInt32(lbCategorias.SelectedValue));
            if (categoria != null)
            {
                txtNombre.Text = categoria.nombre;
                txtId.Value = categoria.id_categoria.ToString();
                cargarSubCategorias();
                lknGuardar.Visible = true;
                lknEliminar.Visible = true;
                lknAgregarSub.Visible = true;

            }
        }

        public void cargarSubCategorias()
        {
            AdminCategorias aCat = new AdminCategorias();
            lbSubcategorias.DataSource = aCat.obtenerSubCategoriasByCategoria(Convert.ToInt32(lbCategorias.SelectedValue));
            lbSubcategorias.DataTextField = "nombre";
            lbSubcategorias.DataValueField = "id_subcategoria";
            lbSubcategorias.DataBind();
            if (lbSubcategorias.Items.Count > 0)
            {
                lknEliminarSub.Visible = true;
                lknGuardarSub.Visible = true;
            }
            else
            {
                lknEliminarSub.Visible = false;
                lknGuardarSub.Visible = false;
            }
        }

        protected void lknEliminar_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(lbCategorias.SelectedValue))
            {
                AdminCategorias aCat = new AdminCategorias();
                int resultado = aCat.eliminarCategoria(Convert.ToInt32(lbCategorias.SelectedValue));
                string mensaje = String.Empty;
                if (resultado > 0)
                {
                    mensaje = "Categoria Eliminada Con Exito";
                }
                else
                {
                    if (resultado == -2)
                    {
                        mensaje = "No Puede eliminar una categoria si Tiene productos Asociados";
                    }
                    else
                    {
                        mensaje = "error al eliminar la categoria";
                    }
                }
                pnlAdvertencia.CssClass = "alert alert-success alert-dismissible";
                lblResultadoProceso.Text = mensaje;
                pnlAdvertencia.Visible = true;
                lbCategorias.DataBind();
            }
        }

        protected void lknGuardar_Click(object sender, EventArgs e)
        {
            string mensaje = String.Empty;
            if (txtNombre.Text != "")
            {
                AdminCategorias aCat = new AdminCategorias();
                int resultado = aCat.actualizarCategoria(Convert.ToInt32(lbCategorias.SelectedValue), txtNombre.Text);
                if (resultado > 0)
                {
                    mensaje = "Categoria Guardada Con Exito";
                }
                else
                {
                    mensaje = "error al guardar la categoria";
                }
                lbCategorias.DataBind();
                lbCategorias.SelectedValue = resultado.ToString();
            }
            else
            {
                mensaje = "Debe ingresar el nombre de la categoria";
            }
            pnlAdvertencia.CssClass = "alert alert-success alert-dismissible";
            lblResultadoProceso.Text = mensaje;
            pnlAdvertencia.Visible = true;
            
        }

        protected void lknAgregar_Click(object sender, EventArgs e)
        {
            string mensaje = String.Empty;
            if (txtNombre.Text != "")
            {
                AdminCategorias aCat = new AdminCategorias();
                int resultado=0; 
                tblUsuarios usuario;
                if (Session["user"] != null)
                {
                    usuario = (tblUsuarios)Session["user"];
                    resultado = aCat.crearCategoria(txtNombre.Text,usuario.tblSucursales.id_empresa.Value);
                }
                if (resultado > 0)
                {
                    mensaje = "Categoria Guardada Con Exito";
                }
                else
                {
                    mensaje = "error al guardar la categoria";
                }
                lbCategorias.DataBind();
                lbCategorias.SelectedValue = resultado.ToString();
            }
            else
            {
                mensaje = "Debe ingresar el nombre de la categoria";
            }
            pnlAdvertencia.CssClass = "alert alert-success alert-dismissible";
            lblResultadoProceso.Text = mensaje;
            pnlAdvertencia.Visible = true;

        }

        protected void lknAgregarSub_Click(object sender, EventArgs e)
        {
            string mensaje = String.Empty;
            if (txtSubCategoria.Text != "")
            {
                AdminCategorias aCat = new AdminCategorias();
                int resultado = aCat.crearSubCategoria(txtSubCategoria.Text, Convert.ToInt32(lbCategorias.SelectedValue));
                if (resultado > 0)
                {
                    mensaje = "SubCategoria Agregada Con Exito";
                }
                else
                {
                    mensaje = "error al Crear la subCategoria";
                }
                cargarSubCategorias();
            }
            else
            {
                mensaje = "Debe ingresar el nombre de la categoria";
            }
            pnlAdvertencia.CssClass = "alert alert-success alert-dismissible";
            lblResultadoProceso.Text = mensaje;
            pnlAdvertencia.Visible = true;

        }

        protected void lknGuardarSub_Click(object sender, EventArgs e)
        {
            string mensaje = String.Empty;
            if (txtSubCategoria.Text != "" && !String.IsNullOrEmpty(lbSubcategorias.SelectedValue))
            {
                AdminCategorias aCat = new AdminCategorias();
                int resultado = aCat.actualizarSubCategoria(Convert.ToInt32(lbSubcategorias.SelectedValue), txtSubCategoria.Text);

                if (resultado > 0)
                {
                    mensaje = "SubCategoria Guardada Con Exito";
                }
                else
                {
                    mensaje = "error al Guardar la subCategoria";
                }
                cargarSubCategorias();
            }
            else
            {
                mensaje = "Debe ingresar el valor de la categoria";
            }
            pnlAdvertencia.CssClass = "alert alert-success alert-dismissible";
            lblResultadoProceso.Text = mensaje;
            pnlAdvertencia.Visible = true;
        }

        protected void lknEliminarSub_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(lbSubcategorias.SelectedValue))
            {
                AdminCategorias aCat = new AdminCategorias();
                int resultado = aCat.eliminarSubCategoria(Convert.ToInt32(lbSubcategorias.SelectedValue));
                string mensaje = String.Empty;
                if (resultado > 0)
                {
                    mensaje = "SubCategoria Eliminada Con Exito";
                }
                else
                {
                    if (resultado == -2)
                    {
                        mensaje = "No Puede eliminar una subcategoria si Tiene productos Asociados";
                    }
                    else
                    {
                        mensaje = "error al eliminar la subcategorias";
                    }
                }
                pnlAdvertencia.CssClass = "alert alert-success alert-dismissible";
                lblResultadoProceso.Text = mensaje;
                pnlAdvertencia.Visible = true;
                cargarSubCategorias();
            }
        }

        protected void lbSubcategorias_SelectedIndexChanged(object sender, EventArgs e)
        {
            AdminCategorias aCat = new AdminCategorias();
            tblSubCategorias cat = aCat.obtenerSubCategoriaById(Convert.ToInt32(lbSubcategorias.SelectedValue));
            if (cat != null)
            {
                txtSubCategoria.Text = cat.nombre;
                lknEliminarSub.Visible = true;
                lknGuardarSub.Visible = true;
            }
        }


    }
}