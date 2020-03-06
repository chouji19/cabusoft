using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CEB.Inventarios.entidades;
using CEB.Inventarios.logica;
using Microsoft.VisualBasic;

namespace CEB.Inventarios
{
    public partial class FrmMovimientosMercancia : Form
    {

        private Usuario user;

        internal Usuario User
        {
            get { return user; }
            set { user = value; }
        }

        public FrmMovimientosMercancia()
        {
            InitializeComponent();
            cargarProductos();
            listaProductosVenta = new List<ProductoVenta>();
            lblId.Text = "";
        }

        private List<ProductoVenta> listaProductosVenta;
        private Producto producto;
        private decimal valorFactura;

        public void cargarProductos()
        {
            LogicaGo logica = new LogicaGo();
            lbProductos.DataSource = logica.obtenerListaProductosDisponibles(txtFiltro.Text);
            if (lbProductos.Items.Count > 0)
                lbProductos.SelectedIndex = 0;
            lbProductos.DisplayMember = "Nombre";
            lbProductos.ValueMember = "Id";
            cargarCategorias();

        }

        public void cargarCategorias()
        {
            LogicaGo logica = new LogicaGo();
            txtCbCategoria.DataSource = logica.obtenerListaCategorias("");
            txtCbCategoria.DisplayMember = "Nombre";
            txtCbCategoria.ValueMember = "Id";
        }

        private void txtCbCategoria_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            txtConcepto.Text = "";
            btnCheck.Enabled = true;
            listaProductosVenta = null;
            dgvProductos.DataSource = null;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (lblId.Text.Equals(""))
                MessageBox.Show("Seleccione producto");
            else
            {
                bool productoSeleccionado = false;
                foreach (ProductoVenta p in listaProductosVenta)
                {
                    if (p.IdProducto == producto.IdProducto)
                    {
                        productoSeleccionado = true;
                    }

                }
                if (!productoSeleccionado)
                {
                    ProductoVenta auxProducto = new ProductoVenta();
                    auxProducto.IdProducto = producto.IdProducto;
                    auxProducto.Nombre = producto.Nombre;
                    auxProducto.Cantidad = 1;
                    auxProducto.Valor = producto.Precio;
                    auxProducto.Total = auxProducto.Cantidad * auxProducto.Valor;
                    if (!producto.Servicio)
                        auxProducto.MaximoUnidades = producto.Existencias;
                    else
                        auxProducto.MaximoUnidades = 1000;
                    listaProductosVenta.Add(auxProducto);
                    cargarDatosProductos();
                    dgvProductos.Columns["Total"].DefaultCellStyle.Format = "c";
                    dgvProductos.Columns["Valor"].DefaultCellStyle.Format = "c";
                    dgvProductos.Columns["IdProducto"].Visible = false;
                    //dgvProductos.Columns["MaximoUnidades"].Visible = false;
                    valorFactura = 0;
                    updateTotal();
                }
                else
                    MessageBox.Show("El producto que desea agregar ya fue seleccionado previamente");
            }
        }

        private void lbProductos_SelectedIndexChanged(object sender, EventArgs e)
        {
            LogicaGo logica = new LogicaGo();
            lblId.Text = lbProductos.SelectedValue.ToString();
            producto = logica.ObtenerProducto(lblId.Text);
            if (producto != null)
            {
                lblId.Text = producto.IdProducto;
                lblNombre.Text = producto.Nombre;
                lblDescripcion.Text = "Descripción: " + producto.Descripcion;

                Double value;
                if (Double.TryParse(producto.Precio.ToString(), out value))
                    lblPrecio.Text = "Precio: " + String.Format(System.Globalization.CultureInfo.CurrentCulture, "{0:C2}", value);
                else
                    lblPrecio.Text = "Precio: " + String.Empty;
                lblTipo.Text = "Tipo: " + producto.Tipo;
                txtCbEstado.SelectedValue = producto.IdEstado;
                txtCbCategoria.SelectedValue = producto.Categoria;
                txtCbSubCategoria.SelectedValue = producto.Subcategoria;
                lblEstado.Text = "Estado: " + txtCbEstado.Text;
                lblFecha.Text = "Fecha Ingreso: " + producto.Fecha.ToString();
                lblCategoria.Text = "Categoria: " + txtCbCategoria.Text;
                lblSubCategoria.Text = "SubCategoria: " + txtCbSubCategoria.Text;
                if (Double.TryParse(producto.PrecioCompra.ToString(), out value))
                    lblPrecio.Text = "Precio Compra: " + String.Format(System.Globalization.CultureInfo.CurrentCulture, "{0:C2}", value);
                else
                    lblPrecio.Text = "Precio Compra: " + String.Empty;
            }
        }

        private void txtFiltro_TextChanged(object sender, EventArgs e)
        {
            cargarProductos();
        }

        private void updateTotal()
        {
            valorFactura = 0;
            foreach (ProductoVenta p in listaProductosVenta)
            {
                valorFactura += p.Total;

            }
            Double value;
            if (Double.TryParse(valorFactura.ToString(), out value))
                lblValorFactura.Text = String.Format(System.Globalization.CultureInfo.CurrentCulture, "{0:C2}", value);
            else
                lblValorFactura.Text = "$0";
        }

        private void cargarDatosProductos()
        {
            dgvProductos.DataSource = ConvertToDataTable(listaProductosVenta);
        }

        public DataTable ConvertToDataTable<T>(IList<T> data)
        {
            PropertyDescriptorCollection properties =
               TypeDescriptor.GetProperties(typeof(T));
            DataTable table = new DataTable();
            foreach (PropertyDescriptor prop in properties)
                table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
            foreach (T item in data)
            {
                DataRow row = table.NewRow();
                foreach (PropertyDescriptor prop in properties)
                    row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;
                table.Rows.Add(row);
            }
            return table;

        }

        private void dgvProductos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0)
                return;

            string message = "Row " + e.RowIndex.ToString() + " is ";

            //Switch through the column names
            switch (dgvProductos.Columns[e.ColumnIndex].Name)
            {
                case "Eliminar":
                    message += "Eliminar you!";
                    foreach (ProductoVenta p in listaProductosVenta)
                    {
                        if (dgvProductos.Rows[e.RowIndex].Cells["IdProducto"].Value.ToString().Equals(p.IdProducto.ToString()))
                        {
                            dgvProductos.Rows.RemoveAt(e.RowIndex);
                            listaProductosVenta.Remove(p);
                            cargarProductos();
                            updateTotal();
                            break;
                        }

                    }

                    break;
                case "Editar":
                    message += "Editar you!";
                    bool correcto = true;
                    string texto = "";
                    do
                    {
                        texto = Interaction.InputBox("Cantidad:", "Cantidad", "");
                        int number1 = 0;
                        correcto = Int32.TryParse(texto, out number1);
                        if (!correcto)
                            MessageBox.Show("Debe ingresar un valor valido");
                        else
                        {
                            foreach (ProductoVenta p in listaProductosVenta)
                            {
                                if (dgvProductos.Rows[e.RowIndex].Cells["IdProducto"].Value.ToString().Equals(p.IdProducto.ToString()))
                                {
                                    if (number1 > Convert.ToInt32(dgvProductos.Rows[e.RowIndex].Cells["MaximoUnidades"].Value))
                                    {
                                        MessageBox.Show("No puede seleccionar mas de " + dgvProductos.Rows[e.RowIndex].Cells["MaximoUnidades"].Value + " Unidades");
                                        correcto = false;
                                    }
                                    else
                                        correcto = true;
                                }

                            }
                        }
                    } while (!correcto);
                    //MessageBox.Show("ahora la cantidad son: " + texto);
                    foreach (ProductoVenta p in listaProductosVenta)
                    {
                        if (dgvProductos.Rows[e.RowIndex].Cells["IdProducto"].Value.ToString().Equals(p.IdProducto.ToString()))
                        {
                            listaProductosVenta.First(d => d.IdProducto == p.IdProducto).Cantidad = Convert.ToInt32(texto);
                            listaProductosVenta.First(d => d.IdProducto == p.IdProducto).Total = Convert.ToInt32(texto) * p.Valor;
                            dgvProductos.Rows[e.RowIndex].Cells["Cantidad"].Value = p.Cantidad;
                            dgvProductos.Rows[e.RowIndex].Cells["Total"].Value = Convert.ToInt32(texto) * p.Valor;
                            updateTotal();
                            break;
                        }

                    }
                    break;
            }
        }

        private void btnCheck_Click(object sender, EventArgs e)
        {
            LogicaGo logica = new LogicaGo();
            int registrosGuardados = 0;
            int resultado = 0; 
            foreach (ProductoVenta p in listaProductosVenta)
            {
                //logica.insertarVenta(factura, p.IdProducto, (int)p.Cantidad, (double)p.Valor, (double)p.Total);
                resultado = logica.insertarMovimientoProductos(txtDpFecha.Text,txtConcepto.Text, p.IdProducto, (int)p.Cantidad, (double)p.Valor, (double)p.Total);
                if (resultado == 1)
                    registrosGuardados++;
            }
            MessageBox.Show("Se registraron "+registrosGuardados+" Movimiento(s) de Producto(s)");
            btnCheck.Enabled = false;
        }

        private void txtCodigoBarras_TextChanged(object sender, EventArgs e)
        {
            LogicaGo logica = new LogicaGo();
            producto = logica.obtenerProductoByCodigo(txtCodigoBarras.Text);
            if (producto != null && producto.Nombre != null && !txtCodigoBarras.Text.Equals(""))
            {
                lblId.Text = producto.IdProducto;
                lblNombre.Text = producto.Nombre;
                lblDescripcion.Text = "Descripción: " + producto.Descripcion;

                Double value;
                if (Double.TryParse(producto.Precio.ToString(), out value))
                    lblPrecio.Text = "Precio: " + String.Format(System.Globalization.CultureInfo.CurrentCulture, "{0:C2}", value);
                else
                    lblPrecio.Text = "Precio: " + String.Empty;
                lblTipo.Text = "Tipo: " + producto.Tipo;
                txtCbEstado.SelectedValue = producto.IdEstado;
                txtCbCategoria.SelectedValue = producto.Categoria;
                txtCbSubCategoria.SelectedValue = producto.Subcategoria;
                lblEstado.Text = "Estado: " + txtCbEstado.Text;
                lblFecha.Text = "Fecha Ingreso: " + producto.Fecha.ToString();
                lblCategoria.Text = "Categoria: " + txtCbCategoria.Text;
                lblSubCategoria.Text = "SubCategoria: " + txtCbSubCategoria.Text;
                if (Double.TryParse(producto.PrecioCompra.ToString(), out value))
                    lblPrecio.Text = "Precio Compra: " + String.Format(System.Globalization.CultureInfo.CurrentCulture, "{0:C2}", value);
                else
                    lblPrecio.Text = "Precio Compra: " + String.Empty;

                btnAdd.PerformClick();
                txtCodigoBarras.Text = "";
            }
        }
    }
}
