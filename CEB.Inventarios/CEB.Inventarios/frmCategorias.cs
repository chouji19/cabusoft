using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CEB.Inventarios.logica;
 

namespace CEB.Inventarios
{
    public partial class frmCategorias : Form
    {
        public frmCategorias()
        {
            InitializeComponent();
            cargarCategorias();
            txtId.Text = "";
            txtNombre.Text = "";
        }

        private void frmCategorias_Load(object sender, EventArgs e)
        {
            // TODO: esta línea de código carga datos en la tabla 'dsCategorias.tblCategorias' Puede moverla o quitarla según sea necesario.
            this.tblCategoriasTableAdapter.Fill(this.dsCategorias.tblCategorias);

        }

        public void cargarCategorias()
        {
            LogicaGo logica = new LogicaGo();
            lbCategorias.DataSource = logica.obtenerListaCategorias(txtFiltro.Text);
            lbCategorias.DisplayMember = "Nombre";
            lbCategorias.ValueMember = "Id";
        }

        public void cargarSubCategorias()
        {
            LogicaGo logica = new LogicaGo();
            lbSubcategorias.DataSource = logica.obtenerListaSubCategorias(txtId.Text);
            lbSubcategorias.DisplayMember = "Nombre";
            lbSubcategorias.ValueMember = "Id";
            txtSubCategoriaId.Text = "";
            txtSubCategoria.Text = "";
        }

        private void lbCategorias_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtId.Text = lbCategorias.SelectedValue.ToString();
            txtNombre.Text = lbCategorias.Text;
            cargarSubCategorias();
            btnDelete.Enabled = true;
            btnSave.Enabled = true;
            btnAddSub.Enabled = true;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (!txtNombre.Text.Equals(""))
            {
                LogicaGo logica = new LogicaGo();
                int res = logica.insertarCategoria(txtNombre.Text);
                if (res != 1)
                {
                    MessageBox.Show("No se pudo ingresar el registro");
                }
                else
                {
                    MessageBox.Show("Categoria Cargada Correctamente");
                    cargarCategorias();
                }
            }
            else
                MessageBox.Show("Debe ingresar un nombre");
        }

        

        private void btnSave_Click(object sender, EventArgs e)
        {

            if (!txtNombre.Text.Equals(""))
            {
                LogicaGo logica = new LogicaGo();
                int res = logica.actualizarCategoria(txtNombre.Text, txtId.Text);
                if (res != 1)
                {
                    MessageBox.Show("No se pudo ingresar el registro");
                }
                else
                {
                    MessageBox.Show("Categoria Cargada Correctamente");
                    cargarCategorias();
                }
            }
            else
                MessageBox.Show("Debe ingresar un nombre");
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (!txtNombre.Text.Equals(""))
            {
                LogicaGo logica = new LogicaGo();
                int res = logica.eliminarCategoria(txtId.Text);
                if (res != 1)
                {
                    MessageBox.Show("No se pudo ingresar el registro");
                }
                else
                {
                    MessageBox.Show("Categoria eliminada Correctamente");
                    cargarCategorias();
                }
            }
            else
                MessageBox.Show("Debe ingresar un nombre");

        }

        private void btnAddSub_Click(object sender, EventArgs e)
        {
            if (!txtSubCategoria.Text.Equals(""))
            {
                LogicaGo logica = new LogicaGo();
                int res = logica.insertarSubCategoria(txtId.Text, txtSubCategoria.Text);
                if (res != 1)
                {
                    MessageBox.Show("No se pudo ingresar el registro");
                }
                else
                {
                    MessageBox.Show("Categoria Cargada Correctamente");
                    cargarSubCategorias();
                }
            }
            else
                MessageBox.Show("Debe ingresar un nombre");
        }

        private void btnSaveSub_Click(object sender, EventArgs e)
        {
            if (!txtSubCategoria.Text.Equals(""))
            {
                LogicaGo logica = new LogicaGo();
                int res = logica.actualizarSubCategoria(txtSubCategoria.Text, txtSubCategoriaId.Text);
                if (res != 1)
                {
                    MessageBox.Show("No se pudo ingresar el registro");
                }
                else
                {
                    MessageBox.Show("SubCategoria Cargada Correctamente");
                    cargarSubCategorias();
                }
            }
            else
                MessageBox.Show("Debe ingresar un nombre");
        }

        private void btnDelSub_Click(object sender, EventArgs e)
        {

            if (!txtSubCategoria.Text.Equals(""))
            {
                LogicaGo logica = new LogicaGo();
                int res = logica.eliminarSubCategoria(txtSubCategoriaId.Text);
                if (res != 1)
                {
                    MessageBox.Show("No se pudo ingresar el registro");
                }
                else
                {
                    MessageBox.Show("Categoria eliminada Correctamente");
                    cargarSubCategorias();
                }
            }
            else
                MessageBox.Show("Debe ingresar un nombre");
        }

        private void lbSubcategorias_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtSubCategoriaId.Text = lbSubcategorias.SelectedValue.ToString();
            txtSubCategoria.Text = lbSubcategorias.Text;
            btnDelSub.Enabled = true;
            btnSaveSub.Enabled = true;
        }

        private void txtFiltro_TextChanged(object sender, EventArgs e)
        {
            cargarCategorias();
        }
    }
}
