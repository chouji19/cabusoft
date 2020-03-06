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
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();
        }

        private void btnInicio_Click(object sender, EventArgs e)
        {
            if (txtUsuario.Text.Equals(""))
            {
                MessageBox.Show("Debe ingresar el nombre de usuario");
            }
            else
            {
                if (txtPassword.Text.Equals(""))
                {
                    MessageBox.Show("La contraseña no puede estar vacia");
                }
                else
                {
                    LogicaGo logica = new LogicaGo();
                    Usuario user = null;
                    bool userExiste = logica.verificarUsuario(txtUsuario.Text);
                    if (userExiste)
                    {
                        user = logica.IniciarSesion(txtUsuario.Text, LogicaGo.Base64Encode(txtPassword.Text));
                        if (user != null) {
                            Principal principal = new Principal();
                            principal.User = user;
                            principal.ponerTitulo();
                            principal.Show();
                            this.Hide();
                        }
                        else
                            MessageBox.Show("Contraseña incorrecta. por favor intente de nuevo");
                    }
                    else
                        MessageBox.Show("Usuario no existe");
                    
                }
            }
        }

        private void frmLogin_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }


    }
}
