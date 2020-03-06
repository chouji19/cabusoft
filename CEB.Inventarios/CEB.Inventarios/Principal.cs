using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CEB.Inventarios.entidades;

namespace CEB.Inventarios
{
    public partial class Principal : Form
    {

        private Usuario user;

        internal Usuario User
        {
            get { return user; }
            set { user = value; }
        }
        public Principal()
        {
            InitializeComponent();
            //this.WindowState = FormWindowState.Maximized; 
        }

        public void ponerTitulo()
        {
            this.Text = "Sistema Inventarios - Usuario: " + user.Nombres + " " + user.Apellidos;
            if (user.Rol == 2)
                tabUsuarios.Visible = true;
            else
                tabUsuarios.Visible = false;
        }


        private void ribbonButton1_Click(object sender, EventArgs e)
        {
            frmProductos childForm = new frmProductos();
            childForm.MdiParent = this;
            childForm.WindowState = FormWindowState.Maximized;
            childForm.Show();
        }

        private void btnCategorias_Click(object sender, EventArgs e)
        {
            frmCategorias childForm = new frmCategorias();
            childForm.MdiParent = this;
            childForm.WindowState = FormWindowState.Maximized;
            childForm.Show();
        }

        private void ribbonButton2_DoubleClick(object sender, EventArgs e)
        {
            frmLogin childForm = new frmLogin();
            childForm.MdiParent = this;
            childForm.WindowState = FormWindowState.Maximized;
            childForm.Show();
        }

        private void ribbonButton3_Click(object sender, EventArgs e)
        {
            frmUsuarios childForm = new frmUsuarios();
            childForm.MdiParent = this;
            childForm.WindowState = FormWindowState.Maximized;
            childForm.Show();
        }

        private void Principal_FormClosed(object sender, FormClosedEventArgs e)
        {
            frmLogin login = new frmLogin();
            login.Show();
        }

        private void ribbonButton2_Click(object sender, EventArgs e)
        {
            frmVentas childForm = new frmVentas();
            childForm.User = user;
            childForm.MdiParent = this;
            childForm.WindowState = FormWindowState.Maximized;
            childForm.Show();
        }

        private void ribbonButton6_Click(object sender, EventArgs e)
        {
            frmInventario childForm = new frmInventario();
            childForm.MdiParent = this;
            childForm.WindowState = FormWindowState.Maximized;
            childForm.Show();
        }

        private void ribbonButton5_Click(object sender, EventArgs e)
        {
            frmGastos childForm = new frmGastos();
            childForm.MdiParent = this;
            childForm.WindowState = FormWindowState.Maximized;
            childForm.Show();
        }

        private void ribbonButton4_Click(object sender, EventArgs e)
        {
            frmConsultaFacturas childForm = new frmConsultaFacturas();
            childForm.MdiParent = this;
            childForm.WindowState = FormWindowState.Maximized;
            childForm.Show();
        }

        private void ribbonButton7_Click(object sender, EventArgs e)
        {
            frmReporteIngresosGastos childForm = new frmReporteIngresosGastos();
            childForm.MdiParent = this;
            childForm.WindowState = FormWindowState.Maximized;
            childForm.Show();
        }

        private void btnMovimientos_Click(object sender, EventArgs e)
        {
            FrmMovimientos childForm = new FrmMovimientos();
            childForm.MdiParent = this;
            childForm.WindowState = FormWindowState.Maximized;
            childForm.Show();
        }

        private void ribbonButton8_Click(object sender, EventArgs e)
        {
            FrmMovimientos childForm = new FrmMovimientos();
            childForm.MdiParent = this;
            childForm.WindowState = FormWindowState.Maximized;
            childForm.Show();
        }

        private void ribbonButton10_Click(object sender, EventArgs e)
        {
            FrmMovimientosMercancia childForm = new FrmMovimientosMercancia();
            childForm.MdiParent = this;
            childForm.WindowState = FormWindowState.Maximized;
            childForm.Show();
        }

        private void ribbonButton11_Click(object sender, EventArgs e)
        {
            frmInventario childForm = new frmInventario();
            childForm.MdiParent = this;
            childForm.WindowState = FormWindowState.Maximized;
            childForm.Show();
        }

        private void ribbonButton12_Click(object sender, EventArgs e)
        {
            frmReporteIngresosGastos childForm = new frmReporteIngresosGastos();
            childForm.MdiParent = this;
            childForm.WindowState = FormWindowState.Maximized;
            childForm.Show();
        }

        private void btnVentas2_Click(object sender, EventArgs e)
        {
            VentaVista2 childForm = new VentaVista2();
            childForm.User = user;
            childForm.MdiParent = this;
            childForm.WindowState = FormWindowState.Maximized;
            childForm.Show();
        }


    }
}
