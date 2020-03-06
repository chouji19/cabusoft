using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CEB.Inventarios.comunes;
using CEB.Inventarios.entidades;
using CEB.Inventarios.logica;

namespace CEB.Inventarios
{
    public partial class frmProductos : Form
    {
        public frmProductos()
        {
            InitializeComponent();
            cargarProductos();
        }

        public void cargarProductos()
        {
            LogicaGo logica = new LogicaGo();
            lbProductos.DataSource = logica.obtenerListaProductos(txtFiltro.Text);
            if (lbProductos.Items.Count > 0)
                lbProductos.SelectedIndex = 0;
            lbProductos.DisplayMember = "Nombre";
            lbProductos.ValueMember = "Id";
            cargarCategorias();
        }

        private void lbProductos_SelectedIndexChanged(object sender, EventArgs e)
        {
                LogicaGo logica = new LogicaGo();
                txtId.Text = lbProductos.SelectedValue.ToString();
                Producto producto = logica.ObtenerProducto(txtId.Text);
                if (producto != null)
                {
                    txtId.Text = producto.IdProducto;
                    txtNombre.Text = producto.Nombre;
                    txtDescripcion.Text = producto.Descripcion;
                    Double value;
                    txtPrecio.Text = Double.TryParse(producto.Precio.ToString(), out value) ? String.Format(System.Globalization.CultureInfo.CurrentCulture, "{0:C2}", value) : String.Empty;

                    if (Double.TryParse(producto.PrecioCompra.ToString(), out value))
                        txtPrecioCompra.Text = String.Format(System.Globalization.CultureInfo.CurrentCulture, "{0:C2}", value);
                    else
                        txtPrecioCompra.Text = String.Empty;
                    //txtPrecio.Text = producto.Precio.ToString();
                    txtUnidades.Value = producto.Existencias;
                    txtCbTipo.Text = producto.Tipo;
                    txtCbEstado.SelectedValue = producto.IdEstado;
                    txtDpFecha.Value = producto.Fecha;
                    txtCbCategoria.SelectedValue = producto.Categoria;
                    txtCbSubCategoria.SelectedValue = producto.Subcategoria;
                    txtCodigoBarras.Text = producto.Codigo;
                    cbActivo.Checked = producto.Activo;
                    cbServicio.Checked = producto.Servicio;
                    cbValorVariable.Checked = producto.ValorVariable;
                    if (producto.RutaImagen != "")
                    {
                        string rutaArchivo = ConfigurationManager.AppSettings["rutaImagenes"];
                        var image = File.Exists(rutaArchivo + producto.RutaImagen) ? Image.FromFile(rutaArchivo + producto.RutaImagen) : Image.FromFile(rutaArchivo + @"Images\Productos\default_product.png");
                        pictureBox1.Image = image;
                        pictureBox1.Height = 120;
                        pictureBox1.Width = 120;
                    }
                    else
                        pictureBox1.Image = null;
                    btnSave.Enabled = true;
                    btnDelete.Enabled = true;
                }
        }


        private void Productos_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'dbgoDataSet.tblEstadosProductos' table. You can move, or remove it, as needed.
            this.tblEstadosProductosTableAdapter.Fill(this.dbgoDataSet.tblEstadosProductos);

        }

        public void cargarCategorias() 
        {
            LogicaGo logica = new LogicaGo();
            txtCbCategoria.DataSource = logica.obtenerListaCategorias("");
            txtCbCategoria.DisplayMember = "Nombre";
            txtCbCategoria.ValueMember = "Id";
            obtenerSubCategorias("1");
            if (txtCbCategoria.Text.Equals("General") || txtCbCategoria.Text.Equals("1. General"))
            {
                txtCbCategoria.SelectedValue = 1;
                obtenerSubCategorias("1");
            }
        }

        public void obtenerSubCategorias(string categoria)
        {
            LogicaGo logica = new LogicaGo();
            txtCbSubCategoria.DataSource = logica.obtenerListaSubCategorias(categoria);
            txtCbSubCategoria.DisplayMember = "Nombre";
            txtCbSubCategoria.ValueMember = "Id";
        }

        private void txtCbCategoria_SelectedIndexChanged(object sender, EventArgs e)
        {
            LogicaGo logica = new LogicaGo();
            if (txtCbCategoria.SelectedValue != null)
            {
                txtCbSubCategoria.DataSource = logica.obtenerListaSubCategorias(txtCbCategoria.SelectedValue.ToString());
                txtCbSubCategoria.DisplayMember = "Nombre";
                txtCbSubCategoria.ValueMember = "Id";
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (!txtNombre.Text.Equals(""))
            {
                if (txtUnidades.Value == 0)
                    MessageBox.Show("Las unidades disponibles no pueden ser 0");
                else
                {
                    //if (txtCodigoBarras.Text.Equals(""))
                    //{
                    //    MessageBox.Show("Un codigo de barras no puede pertenecer a mas de un producto. establesca las unidades a uno");
                    //}
                    //else
                    //{
                        LogicaGo logica = new LogicaGo();
                        double precio = 0;
                        double precioCompra = 0;
                        if(txtPrecio.Text != "")
                         precio = double.Parse(txtPrecio.Text, NumberStyles.Currency);
                        if (txtPrecioCompra.Text != "")
                         precioCompra = double.Parse(txtPrecioCompra.Text, NumberStyles.Currency);
                        int res = logica.InsertarProducto(txtNombre.Text, txtDescripcion.Text, precio.ToString(CultureInfo.InvariantCulture),
                                        txtUnidades.Value, txtCbTipo.Text, txtMarca.Text, txtCbEstado.SelectedValue.ToString(), 
                                        txtDpFecha.Value, txtCbSubCategoria.SelectedValue.ToString(), 
                                        txtCbCategoria.SelectedValue.ToString(), txtCodigoBarras.Text,
                                        cbServicio.Checked, cbActivo.Checked,precioCompra.ToString(CultureInfo.InvariantCulture),txtImagen.Text, cbValorVariable.Checked);
                        if (res != 1)
                        {
                            MessageBox.Show("No se pudo ingresar el registro");
                        }
                        else
                        {
                            MessageBox.Show("Producto Cargada Correctamente");
                            txtFiltro.Text = txtNombre.Text;
                            cargarProductos();
                        }
                    //}
                }
            }
            else
                MessageBox.Show("Debe ingresar un nombre");
        }

        private void txtFiltro_TextChanged(object sender, EventArgs e)
        {
            cargarProductos();
        }

        private void txtPrecio_TextChanged(object sender, EventArgs e)
        {
            //if(!txtPrecio.Text.Equals(""))
            //    txtPrecio.Text = (Decimal.Parse(txtPrecio.Text) / 100).ToString("f2");
        }

        private void txtPrecio_Leave(object sender, EventArgs e)
        {
            Double value;
            if (!txtPrecio.Text.StartsWith("$"))
            {
                if (Double.TryParse(txtPrecio.Text, out value))
                    txtPrecio.Text = String.Format(System.Globalization.CultureInfo.CurrentCulture, "{0:C2}", value);
                else
                    txtPrecio.Text = String.Empty;
            }

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!txtNombre.Equals(""))
            {
                LogicaGo logica = new LogicaGo();
                double precio = 0;
                double precioCompra = 0;
                if (txtPrecio.Text != "")
                    precio = double.Parse(txtPrecio.Text, NumberStyles.Currency);
                if (txtPrecioCompra.Text != "")
                    precioCompra = double.Parse(txtPrecioCompra.Text, NumberStyles.Currency);
                int res = logica.ActualizarProducto(txtId.Text,txtNombre.Text, txtDescripcion.Text, precio.ToString(),
                                txtUnidades.Value, txtCbTipo.Text, txtMarca.Text, txtCbEstado.SelectedValue.ToString(),
                                txtDpFecha.Value, txtCbSubCategoria.SelectedValue.ToString(), 
                                txtCbCategoria.SelectedValue.ToString(), txtCodigoBarras.Text,
                                cbServicio.Checked, cbActivo.Checked, precioCompra.ToString(), txtImagen.Text,cbValorVariable.Checked);
                if (res != 1)
                {
                    MessageBox.Show("No se pudo actualizar el registro");
                }
                else
                {
                    MessageBox.Show("Producto actualizado Correctamente");
                    cargarProductos();
                }
            }
            else
                MessageBox.Show("Debe ingresar un nombre");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            txtId.Text = "";
            txtNombre.Text = "";
            txtDescripcion.Text = "";
            txtPrecio.Text = "";
            txtUnidades.Value = 0;
            txtCbTipo.Text = "";
            txtCodigoBarras.Text = "";
            txtMarca.Text = "";
            cbServicio.Checked = false;
            cbActivo.Checked = true;
            txtPrecioCompra.Text = "";
            btnSave.Enabled = false;
            btnDelete.Enabled = false;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (!txtNombre.Equals(""))
            {
                LogicaGo logica = new LogicaGo();
                int res = logica.EliminarProducto(txtId.Text);
                if (res != 1)
                {
                    MessageBox.Show("No se pudo eliminar el registro, por favor intente de nuevo mas tarde");
                }
                else
                {
                    MessageBox.Show("Producto eliminado Correctamente");
                    cargarProductos();
                }
            }
            else
                MessageBox.Show("Debe ingresar un nombre");
        }

        private void cbServicio_CheckedChanged(object sender, EventArgs e)
        {
            if (cbServicio.Checked)
            {
                txtUnidades.Value = 1;
                txtUnidades.Visible = false;
                lblUnidades.Visible = false;
            }
            else {
                txtUnidades.Visible = true;
                lblUnidades.Visible = true;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            cargarProductos();
        }

        private void txtPrcedioCompra_Leave(object sender, EventArgs e)
        {
            Double value;
            if (!txtPrecioCompra.Text.StartsWith("$"))
            {
                if (Double.TryParse(txtPrecioCompra.Text, out value))
                    txtPrecioCompra.Text = String.Format(System.Globalization.CultureInfo.CurrentCulture, "{0:C2}", value);
                else
                    txtPrecioCompra.Text = String.Empty;
            }
        }

        public static Image ScaleImage(Image image, int maxWidth, int maxHeight)
        {
            var ratioX = (double)maxWidth / image.Width;
            var ratioY = (double)maxHeight / image.Height;
            var ratio = Math.Min(ratioX, ratioY);

            var newWidth = (int)(image.Width * ratio);
            var newHeight = (int)(image.Height * ratio);

            var newImage = new Bitmap(newWidth, newHeight);

            using (var graphics = Graphics.FromImage(newImage))
                graphics.DrawImage(image, 0, 0, newWidth, newHeight);

            return newImage;
        }



        private void btnImage_Click(object sender, EventArgs e)
        {
            Stream myStream = null;
            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            openFileDialog1.InitialDirectory = "c:\\";
            openFileDialog1.Filter = "JPEG Files (*.jpeg)|*.jpeg|PNG Files (*.png)|*.png|JPG Files (*.jpg)|*.jpg|GIF Files (*.gif)|*.gif|All files (*.*)|*.*";
            openFileDialog1.FilterIndex = 5;
            openFileDialog1.RestoreDirectory = true;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    if ((myStream = openFileDialog1.OpenFile()) != null)
                    {
                        using (myStream)
                        {
                            // Insert code to read the stream here.
                            
                            Image image = Image.FromFile(openFileDialog1.FileName);
                            // Set the PictureBox image property to this image.
                            // ... Then, adjust its height and width properties.
                            Image newImage = ScaleImage(image, 120, 120);
                            Image mostrar;
                                string rutaArchivo = ConfigurationManager.AppSettings["rutaImagenes"];
                                newImage.Save(rutaArchivo + System.IO.Path.GetFileName(openFileDialog1.FileName), Util.GetImageFormat(image));
                                mostrar = Image.FromFile(rutaArchivo + System.IO.Path.GetFileName(openFileDialog1.FileName));    
                            pictureBox1.Image = mostrar;
                                pictureBox1.Height = 120;
                                pictureBox1.Width = 120;
                                txtImagen.Text = System.IO.Path.GetFileName(openFileDialog1.FileName);
                           
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: Could not read file from disk. Original error: " + ex.Message);
                }
            }
            
            
        }


    }
}
