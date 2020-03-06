using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CEB.Inventarios
{

    public delegate void DataSentHandler(string producto, string valor, Decimal cantidad);

    public partial class FrmNuevoProductoCustom : Form
    {
        public event DataSentHandler DataSent;
        public FrmNuevoProductoCustom()
        {
            InitializeComponent();
        }

        private void fileSystemWatcher1_Changed(object sender, System.IO.FileSystemEventArgs e)
        {

        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            if (DataSent != null) DataSent(txtProducto.Text, txtValor.Text, txtCantidad.Value);
        }
    }
}
