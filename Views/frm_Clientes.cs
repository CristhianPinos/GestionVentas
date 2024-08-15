
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
    public partial class frm_Clientes : Form
    {
        private ClientesController clientesController = new ClientesController();
        private int codigoCliente = 0;

        public frm_Clientes()
        {
            InitializeComponent();
        }
        private void frm_Clientes_Load(object sender, EventArgs e)
        {
            cargaListaClientes();
        }
        private void cargaListaClientes()
        {
            try
            {
                List<ClientesModel> clientes = clientesController.todos();
                lst_Clientes.Items.Clear();
                foreach (var cliente in clientes)
                {
                    lst_Clientes.Items.Add(FormatearCliente(cliente));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar los clientes: " + ex.Message);
            }
        }
        private string FormatearCliente(ClientesModel cliente)
        {
            return $"{cliente.Cliente_id} - {cliente.Nombre} {cliente.Apellido} - {cliente.Email} - {cliente.Telefono}";
        }
        private void lst_Clientes_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lst_Clientes.SelectedItem != null)
            {
                var itemSeleccionado = lst_Clientes.SelectedItem.ToString();
                var cliente = clientesController.todos().Find(c => FormatearCliente(c) == itemSeleccionado);
                if (cliente != null)
                {
                    codigoCliente = cliente.Cliente_id;
                    txt_Nombre.Text = cliente.Nombre;
                    txt_Apellido.Text = cliente.Apellido;
                    txt_Email.Text = cliente.Email;
                    txt_Telefono.Text = cliente.Telefono;
                    txt_Clienteid.Text = cliente.Cliente_id.ToString();
                    txt_Nombre.Enabled = true;
                    txt_Apellido.Enabled = true;
                    txt_Email.Enabled = true;
                    txt_Telefono.Enabled = true;
                    btn_Eliminar.Enabled = true;
                    btn_Editar.Enabled = true;
                }
            }
            else
            {
                txt_Nombre.Text = "";
                txt_Apellido.Text = "";
                txt_Email.Text = "";
                txt_Telefono.Text = "";
                txt_Clienteid.Text = "";
                txt_Nombre.Enabled = false;
                txt_Apellido.Enabled = false;
                txt_Email.Enabled = false;
                txt_Telefono.Enabled = false;
                btn_Eliminar.Enabled = false;
                btn_Editar.Enabled = false;
            }
        }
        private void btn_Guardar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txt_Clienteid.Text) ||
                string.IsNullOrWhiteSpace(txt_Nombre.Text) ||
                string.IsNullOrWhiteSpace(txt_Apellido.Text) ||
                string.IsNullOrWhiteSpace(txt_Email.Text) ||
                string.IsNullOrWhiteSpace(txt_Telefono.Text))
            {
                MessageBox.Show("Por favor, rellene todos los campos");
                return;
            }
            int clienteId;
            if (!int.TryParse(txt_Clienteid.Text, out clienteId))
            {
                MessageBox.Show("Cliente ID debe ser un numero valido");
                return;
            }
            ClientesModel cliente = new ClientesModel
            {
                Cliente_id = clienteId,
                Nombre = txt_Nombre.Text,
                Apellido = txt_Apellido.Text,
                Email = txt_Email.Text,
                Telefono = txt_Telefono.Text,
            };
            try
            {
                string respuesta;
                if (codigoCliente == 0)
                {
                    respuesta = clientesController.insertar(cliente);
                }
                else
                {
                    respuesta = clientesController.actualizar(cliente);
                }
                if (respuesta == "ok")
                {
                    MessageBox.Show("Se guardo con exito");
                    btn_Cancelar_Click(null, null);
                    cargaListaClientes();
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
            if (codigoCliente == 0)
            {
                MessageBox.Show("Seleccione un cliente para eliminar");
                return;
            }

            DialogResult result = MessageBox.Show("¿Desea eliminar el cliente?", "Confirmacion", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                ClientesModel cliente = new ClientesModel { Cliente_id = codigoCliente };
                string respuesta = clientesController.eliminar(cliente);

                if (respuesta == "ok")
                {
                    MessageBox.Show("Se elimino con exito");
                    btn_Cancelar_Click(null, null);
                    cargaListaClientes();
                }
                else
                {
                    MessageBox.Show("Error: " + respuesta);
                }
            }
        }
        private void btn_Editar_Click(object sender, EventArgs e)
        {
            if (codigoCliente == 0)
            {
                MessageBox.Show("Seleccione un cliente para editar");
                return;
            }
            if (string.IsNullOrWhiteSpace(txt_Nombre.Text) ||
                string.IsNullOrWhiteSpace(txt_Apellido.Text) ||
                string.IsNullOrWhiteSpace(txt_Email.Text) ||
                string.IsNullOrWhiteSpace(txt_Telefono.Text))
            {
                MessageBox.Show("Por favor, rellene todos los campos");
                return;
            }
            ClientesModel cliente = new ClientesModel
            {
                Cliente_id = codigoCliente,
                Nombre = txt_Nombre.Text,
                Apellido = txt_Apellido.Text,
                Email = txt_Email.Text,
                Telefono = txt_Telefono.Text,
            };
            string respuesta = clientesController.actualizar(cliente);

            if (respuesta == "ok")
            {
                MessageBox.Show("Cliente actualizado con exito");
                btn_Cancelar_Click(null, null);
                cargaListaClientes();
            }
            else
            {
                MessageBox.Show("Error al actualizar el cliente: " + respuesta);
            }
        }
        private void btn_Cancelar_Click(object sender, EventArgs e)
        {
            txt_Nombre.Text = "";
            txt_Apellido.Text = "";
            txt_Email.Text = "";
            txt_Telefono.Text = "";
            txt_Clienteid.Text = "";
            codigoCliente = 0;
            lst_Clientes.ClearSelected();
            txt_Nombre.Enabled = false;
            txt_Apellido.Enabled = false;
            txt_Email.Enabled = false;
            txt_Telefono.Enabled = false;
            btn_Eliminar.Enabled = false;
            btn_Editar.Enabled = false;
        }
        private void btn_Salir_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void lst_Clientes_DoubleClick(object sender, EventArgs e)
        {
            if (lst_Clientes.SelectedItem != null)
            {
                var itemSeleccionado = lst_Clientes.SelectedItem.ToString();
                var cliente = clientesController.todos().Find(c => FormatearCliente(c) == itemSeleccionado);

                if (cliente != null)
                {
                    codigoCliente = cliente.Cliente_id;
                    txt_Nombre.Text = cliente.Nombre;
                    txt_Apellido.Text = cliente.Apellido;
                    txt_Email.Text = cliente.Email;
                    txt_Telefono.Text = cliente.Telefono;
                    txt_Clienteid.Text = cliente.Cliente_id.ToString();
                    txt_Nombre.Enabled = true;
                    txt_Apellido.Enabled = true;
                    txt_Email.Enabled = true;
                    txt_Telefono.Enabled = true;
                    btn_Eliminar.Enabled = true;
                    btn_Editar.Enabled = true;
                }
            }
        }
    }
}
