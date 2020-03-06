using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CEB.Inventarios.logica;
using CEB.Inventarios.entidades;

namespace CEB.Inventarios
{
    public partial class frmUsuarios : Form
    {
        
        
        public frmUsuarios()
        {
            InitializeComponent();
            cargarlistaUsuarios();
        }

        private void cargarlistaUsuarios()
        {
            LogicaGo logica= new LogicaGo();
            lbUsuarios.DataSource = logica.obtenerListaUsuarios(txtFiltro.Text);
            lbUsuarios.DisplayMember = "Nombre";
            lbUsuarios.ValueMember = "Id";    
        }

        private void txtFiltro_TextChanged(object sender, EventArgs e)
        {
            cargarlistaUsuarios();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (!txtIdentificacion.Text.Equals("") && !txtPassword.Text.Equals(""))
            {
                LogicaGo logica = new LogicaGo();
                int res = logica.insertarUsuario(txtIdentificacion.Text,txtNombres.Text,txtApellidos.Text,txtCelular.Text,txtDireccion.Text,txtPassword.Text,"3",cbActivo.Checked);
                if (res != 1)
                {
                    MessageBox.Show("No se pudo ingresar el registro");
                }
                else
                {
                    MessageBox.Show("Usuario Creado Correctamente");
                    cargarlistaUsuarios();
                }
            }
            else
                MessageBox.Show("Debe ingresar su identificacion");
        }

        private void lbUsuarios_SelectedIndexChanged(object sender, EventArgs e)
        {
            LogicaGo logica = new LogicaGo();
            txtId.Text = lbUsuarios.SelectedValue.ToString();
            Usuario usuario = logica.obtenerUsuario(txtId.Text);
            if (usuario != null)
            {
                txtId.Text = usuario.IdUsuario;
                txtIdentificacion.Text = usuario.Identificacion;
                txtNombres.Text = usuario.Nombres;
                txtApellidos.Text = usuario.Apellidos;
                txtCelular.Text = usuario.Celular;
                txtDireccion.Text = usuario.Direccion;
                txtPassword.Text = LogicaGo.Base64Decode(usuario.Password);
                cbActivo.Checked = usuario.Activo;
                if (usuario.Rol == 1 || usuario.Rol == 2)
                    txtIdentificacion.Enabled = false;
                else
                    txtIdentificacion.Enabled = true;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!txtIdentificacion.Equals(""))
            {
                LogicaGo logica = new LogicaGo();
                int res = logica.actualizarUsuario(txtId.Text, txtIdentificacion.Text, txtNombres.Text, txtApellidos.Text, txtCelular.Text, txtDireccion.Text, txtPassword.Text, txtRol.Text,cbActivo.Checked);
                if (res != 1)
                {
                    MessageBox.Show("No se pudo actualizar el registro");
                }
                else
                {
                    MessageBox.Show("Producto actualizado Correctamente");
                    cargarlistaUsuarios();
                }
            }
            else
                MessageBox.Show("Debe ingresar una identifiación");
        }

        private void txtIdentificacion_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (!txtIdentificacion.Text.Equals(""))
            {
                LogicaGo logica = new LogicaGo();
                int res = logica.eliminarUsuario(txtId.Text);
                if (res != 1)
                {
                    MessageBox.Show("No se pudo ingresar el registro");
                }
                else
                {
                    MessageBox.Show("Usuario eliminado Correctamente");
                    cargarlistaUsuarios();
                }
            }
            else
                MessageBox.Show("Debe Seleccionar un Usuario");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            txtIdentificacion.Enabled = true;
            txtIdentificacion.Text = "";
            txtNombres.Text = "";
            txtApellidos.Text = "";
            txtCelular.Text = "";
            txtDireccion.Text = "";
            txtPassword.Text = "";
            txtRol.Text = "3";
        }
    }
}
