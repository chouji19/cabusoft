using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CEB.Inventarios.entidades;
using CEB.Inventarios.logica;

namespace CEB.Inventarios
{
    public partial class frmGastos : Form
    {
        public frmGastos()
        {
            InitializeComponent();
        }


        public void cargarGastos()
        {
            LogicaGo logica = new LogicaGo();
            lbGastos.DataSource = logica.obtenerListaGastos(txtFiltro.Text);
            if (lbGastos.Items.Count > 0)
                lbGastos.SelectedIndex = 0;
            lbGastos.DisplayMember = "Nombre";
            lbGastos.ValueMember = "Id";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            LogicaGo logica = new LogicaGo();
            lbGastos.DataSource = logica.obtenerListaGastos(txtDpFechaIni.Text, txtDpFechaFin.Text);
            if (lbGastos.Items.Count > 0)
                lbGastos.SelectedIndex = 0;
            lbGastos.DisplayMember = "Nombre";
            lbGastos.ValueMember = "Id";
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (!txtConcepto.Equals("") && !txtValorCompra.Equals(""))
            {
                    LogicaGo logica = new LogicaGo();
                    double valor = double.Parse(txtValorCompra.Text, NumberStyles.Currency);
                    double total = double.Parse(txtTotal.Text, NumberStyles.Currency);
                    int res = logica.insertarGasto(txtConcepto.Text, txtCantidad.Value, valor.ToString(),total.ToString(),dpFecha.Text);
                    if (res != 1)
                    {
                        MessageBox.Show("No se pudo ingresar el registro");
                    }
                    else
                    {
                        MessageBox.Show("Gasto Cargado Correctamente");
                        cargarGastos();
                    }
            }
            else
                MessageBox.Show("Debe ingresar una descripcion");
        }

        private void txtValorCompra_Leave(object sender, EventArgs e)
        {
            Double value;
            if (!txtValorCompra.Text.StartsWith("$"))
            {
                if (Double.TryParse(txtValorCompra.Text, out value))
                    txtValorCompra.Text = String.Format(System.Globalization.CultureInfo.CurrentCulture, "{0:C2}", value);
                else
                    txtValorCompra.Text = String.Empty;
                txtTotal.Text = String.Format(System.Globalization.CultureInfo.CurrentCulture, "{0:C2}", txtCantidad.Value * Convert.ToDecimal(value));
            }
        }

        private void lbGastos_SelectedIndexChanged(object sender, EventArgs e)
        {
            LogicaGo logica = new LogicaGo();
            txtId.Text = lbGastos.SelectedValue.ToString();
            Gasto gasto = logica.obtenerGasto(txtId.Text);
            if (gasto != null)
            {
                txtId.Text = gasto.IdGasto;
                txtConcepto.Text = gasto.Concepto;
                txtCantidad.Value = gasto.Cantidad;
                Double value;
                if (Double.TryParse(gasto.ValorCompra.ToString(), out value))
                    txtValorCompra.Text = String.Format(System.Globalization.CultureInfo.CurrentCulture, "{0:C2}", value);
                else
                    txtValorCompra.Text = String.Empty;
                if (Double.TryParse(gasto.ValorTotal.ToString(), out value))
                    txtTotal.Text = String.Format(System.Globalization.CultureInfo.CurrentCulture, "{0:C2}", value);
                else
                    txtTotal.Text = String.Empty;
                dpFecha.Text = gasto.Fecha;
                //txtPrecio.Text = producto.Precio.ToString();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            LogicaGo logica = new LogicaGo();
            double valor = double.Parse(txtValorCompra.Text, NumberStyles.Currency);
            int res = logica.eliminarGasto(txtId.Text);
            if (res != 1)
            {
                MessageBox.Show("No se pudo eliminar el registro");
            }
            else
            {
                MessageBox.Show("Gasto eliminado Correctamente");
                cargarGastos();
            }
        }

        private void txtFiltro_TextChanged(object sender, EventArgs e)
        {
            cargarGastos();
        }

        private void txtCantidad_ValueChanged(object sender, EventArgs e)
        {
            if (!txtValorCompra.Text.Equals(""))
            {
                double valor = double.Parse(txtValorCompra.Text, NumberStyles.Currency);
                txtTotal.Text = String.Format(System.Globalization.CultureInfo.CurrentCulture, "{0:C2}", txtCantidad.Value * Convert.ToDecimal(valor));
            }
       }

        private void button2_Click(object sender, EventArgs e)
        {
            txtConcepto.Text = "";
            txtCantidad.Value = 1;
            txtValorCompra.Text = "";
            txtTotal.Text = "";
            txtId.Text = "";
        }


    }
}
