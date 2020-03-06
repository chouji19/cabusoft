using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CEB.Inventarios.logica;

namespace CEB.Inventarios
{
    public partial class FrmMovimientos : Form
    {
        public FrmMovimientos()
        {
            InitializeComponent();
        }

        private void txtValor_Leave(object sender, EventArgs e)
        {
            Double value;
            if (!txtValor.Text.StartsWith("$"))
            {
                if (Double.TryParse(txtValor.Text, out value))
                    txtValor.Text = String.Format(System.Globalization.CultureInfo.CurrentCulture, "{0:C2}", value);
                else
                    txtValor.Text = String.Empty;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            LogicaGo logica = new LogicaGo();
            double valor = 0; 
            if(!txtValor.Text.Equals(""))
                valor = double.Parse(txtValor.Text, NumberStyles.Currency);
            dgvMovimientos.DataSource = logica.obtenerMovimientos(dpFecha.Text, txtConcepto.Text, cbTipo.Text, valor);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (cbTipo.Text.Equals(""))
                MessageBox.Show("Debe Seleccionar un tipo de Movimiento");
            else
            {
                if(txtValor.Text.Equals(""))
                {
                    MessageBox.Show("Debe Ingresar un Valor");
                }
                else
                {
                    LogicaGo logica = new LogicaGo();
                    double valor = double.Parse(txtValor.Text, NumberStyles.Currency);
                    int resultado = logica.insertarMovimiento(dpFecha.Text, txtConcepto.Text, cbTipo.Text, valor);
                    if (resultado == 1)
                    {
                        MessageBox.Show("Movimiento Ingresado Con Exito");
                    }
                    else
                        MessageBox.Show("No se pudo ingresar el Movimiento por favor intente de nuevo mas tarde");

                }
            }
            
        }
    }
}
