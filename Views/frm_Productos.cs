
namespace GestionVentas.Views
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows.Forms;
    using GestionVentas.Controllers;
    using GestionVentas.Models;
    public partial class frm_Productos : Form
    {
        private ProductosController productosController = new ProductosController();
        private int codigoProducto = 0;
        public frm_Productos()
        {
            InitializeComponent();
        }
        private void frm_Productos_Load(object sender, EventArgs e)
        {
            cargaListaProductos();
        }
        private void cargaListaProductos()
        {
            try
            {
                List<ProductosModel> productos = productosController.todos();
                lst_Productos.Items.Clear();
                foreach (var producto in productos)
                {
                    lst_Productos.Items.Add(FormatearProducto(producto));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar los productos: " + ex.Message);
            }
        }
        private string FormatearProducto(ProductosModel producto)
        {
            return $"{producto.Producto_id} - {producto.Nombre} - {producto.Descripcion} - ${producto.Precio} - Stock: {producto.Stock}";
        }
        private void lst_Productos_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            if (lst_Productos.SelectedItem != null)
            {
                var itemSeleccionado = lst_Productos.SelectedItem.ToString();
                var producto = productosController.todos().Find(p => FormatearProducto(p) == itemSeleccionado);
                if (producto != null)
                {
                    codigoProducto = producto.Producto_id;
                    txt_Nombre.Text = producto.Nombre;
                    txt_Descripcion.Text = producto.Descripcion;
                    txt_Precio.Text = producto.Precio.ToString();
                    txt_Stock.Text = producto.Stock.ToString();
                    txt_Nombre.Enabled = true;
                    txt_Descripcion.Enabled = true;
                    txt_Precio.Enabled = true;
                    txt_Stock.Enabled = true;
                    btn_Eliminar.Enabled = true;
                    btn_Editar.Enabled = true;
                }
            }
            else
            {
                txt_Nombre.Text = "";
                txt_Descripcion.Text = "";
                txt_Precio.Text = "";
                txt_Stock.Text = "";
                txt_Nombre.Enabled = false;
                txt_Descripcion.Enabled = false;
                txt_Precio.Enabled = false;
                txt_Stock.Enabled = false;
                btn_Eliminar.Enabled = false;
                btn_Editar.Enabled = false;
            }
        }
        private void btn_Guardar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txt_Nombre.Text) ||
                string.IsNullOrWhiteSpace(txt_Descripcion.Text) ||
                string.IsNullOrWhiteSpace(txt_Precio.Text) ||
                string.IsNullOrWhiteSpace(txt_Stock.Text))
            {
                MessageBox.Show("Por favor, rellene todos los campos");
                return;
            }

            ProductosModel producto = new ProductosModel
            {
                Producto_id = codigoProducto,
                Nombre = txt_Nombre.Text,
                Descripcion = txt_Descripcion.Text,
                Precio = Convert.ToDecimal(txt_Precio.Text),
                Stock = Convert.ToInt32(txt_Stock.Text)
            };

            try
            {
                string respuesta;
                if (codigoProducto == 0)
                {
                    respuesta = productosController.insertar(producto);
                }
                else
                {
                    respuesta = productosController.actualizar(producto);
                }

                if (respuesta == "ok")
                {
                    MessageBox.Show("Se guardo con exito");
                    btn_Cancelar_Click(null, null);
                    cargaListaProductos();
                }
                else
                {
                    MessageBox.Show("Error: " + respuesta);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void btn_Eliminar_Click(object sender, EventArgs e)
        {
            if (codigoProducto == 0)
            {
                MessageBox.Show("Seleccione un producto para eliminar");
                return;
            }

            DialogResult result = MessageBox.Show("¿Desea eliminar el producto?", "Confirmacion", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                ProductosModel producto = new ProductosModel { Producto_id = codigoProducto };
                string respuesta = productosController.eliminar(producto);

                if (respuesta == "ok")
                {
                    MessageBox.Show("Se elimino con exito");
                    btn_Cancelar_Click(null, null);
                    cargaListaProductos();
                }
                else
                {
                    MessageBox.Show("Error: " + respuesta);
                }
            }
        }
        private void btn_Editar_Click(object sender, EventArgs e)
        {
            if (codigoProducto == 0)
            {
                MessageBox.Show("Seleccione un producto para editar");
                return;
            }
            if (string.IsNullOrWhiteSpace(txt_Nombre.Text) ||
                string.IsNullOrWhiteSpace(txt_Descripcion.Text) ||
                string.IsNullOrWhiteSpace(txt_Precio.Text) ||
                string.IsNullOrWhiteSpace(txt_Stock.Text))
            {
                MessageBox.Show("Por favor, rellene todos los campos");
                return;
            }
            ProductosModel producto = new ProductosModel
            {
                Producto_id = codigoProducto,
                Nombre = txt_Nombre.Text,
                Descripcion = txt_Descripcion.Text,
                Precio = Convert.ToDecimal(txt_Precio.Text),
                Stock = Convert.ToInt32(txt_Stock.Text)
            };
            string respuesta = productosController.actualizar(producto);

            if (respuesta == "ok")
            {
                MessageBox.Show("Producto actualizado con exito");
                btn_Cancelar_Click(null, null);
                cargaListaProductos();  
            }
            else
            {
                MessageBox.Show("Error al actualizar el producto: " + respuesta);
            }
        }
        private void btn_Cancelar_Click(object sender, EventArgs e)
        {
            txt_Nombre.Text = "";
            txt_Descripcion.Text = "";
            txt_Precio.Text = "";
            txt_Stock.Text = "";
            codigoProducto = 0;
            lst_Productos.ClearSelected();
            txt_Nombre.Enabled = false;
            txt_Descripcion.Enabled = false;
            txt_Precio.Enabled = false;
            txt_Stock.Enabled = false;
            btn_Eliminar.Enabled = false;
            btn_Editar.Enabled = false;
        }
        private void btn_Salir_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void lst_Productos_DoubleClick(object sender, EventArgs e)
        {
            if (lst_Productos.SelectedItem != null)
            {
                var itemSeleccionado = lst_Productos.SelectedItem.ToString();
                var producto = productosController.todos().Find(p => FormatearProducto(p) == itemSeleccionado);

                if (producto != null)
                {
                    codigoProducto = producto.Producto_id;
                    txt_Nombre.Text = producto.Nombre;
                    txt_Descripcion.Text = producto.Descripcion;
                    txt_Precio.Text = producto.Precio.ToString();
                    txt_Stock.Text = producto.Stock.ToString();
                    txt_Nombre.Enabled = true;
                    txt_Descripcion.Enabled = true;
                    txt_Precio.Enabled = true;
                    txt_Stock.Enabled = true;
                    btn_Eliminar.Enabled = true;
                    btn_Editar.Enabled = true;
                }
            }
        }
    }
}
