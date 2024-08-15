
namespace GestionVentas.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using GestionVentas.Models;
    internal class ClientesController
    {
        private ClientesModel modeloCliente = new ClientesModel();

        public List<ClientesModel> todos()
        {
            return modeloCliente.todos();
        }
        public ClientesModel uno(ClientesModel cliente)
        {
            return modeloCliente.uno(cliente);
        }
        public string insertar(ClientesModel cliente)
        {
            return modeloCliente.insertar(cliente);
        }
        public string actualizar(ClientesModel cliente)
        {
            return modeloCliente.actualizar(cliente);
        }
        public string eliminar(ClientesModel cliente)
        {
            return modeloCliente.eliminar(cliente);
        }
    }
}
