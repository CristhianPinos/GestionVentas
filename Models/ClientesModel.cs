using GestionVentas.Config;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionVentas.Models
{
    internal class ClientesModel
    {
        public int Cliente_id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Email { get; set; }
        public string Telefono { get; set; }

        private List<ClientesModel> listaClientes = new List<ClientesModel>();
        private Conexion conexion = new Conexion();
        private SqlCommand cmd = new SqlCommand();
        public List<ClientesModel> todos()
        {
            string cadena = "select * from Clientes";
            SqlDataAdapter adapter = new SqlDataAdapter(cadena, conexion.AbrirConexion());
            DataTable tabla = new DataTable();
            adapter.Fill(tabla);

            foreach (DataRow cliente in tabla.Rows)
            {
                ClientesModel nuevoCliente = new ClientesModel
                {
                    Cliente_id = Convert.ToInt32(cliente["Cliente_id"]),
                    Nombre = cliente["Nombre"].ToString(),
                    Apellido = cliente["Apellido"].ToString(),
                    Email = cliente["Email"].ToString(),
                    Telefono = cliente["Telefono"].ToString(),
                };
                listaClientes.Add(nuevoCliente);
            }
            conexion.CerrarConexion();
            return listaClientes;
        }
        public ClientesModel uno(ClientesModel cliente)
        {
            string cadena = "select * from Clientes where Cliente_id = " + cliente.Cliente_id;
            cmd = new SqlCommand(cadena, conexion.AbrirConexion());
            SqlDataReader lector = cmd.ExecuteReader();

            lector.Read();
            ClientesModel clienteRegresa = new ClientesModel
            {
                Cliente_id = Convert.ToInt32(lector["Cliente_id"]),
                Nombre = lector["Nombre"].ToString(),
                Apellido = lector["Apellido"].ToString(),
                Email = lector["Email"].ToString(),
                Telefono = lector["Telefono"].ToString(),
            };

            conexion.CerrarConexion();
            return clienteRegresa;
        }
        public string insertar(ClientesModel cliente)
        {
            try
            {
                cmd.Connection = conexion.AbrirConexion();
                cmd.CommandText = "insert into Clientes (Cliente_id, Nombre, Apellido, Email, Telefono) values ('" +
                                  cliente.Cliente_id + "', '" +
                                  cliente.Nombre + "', '" +
                                  cliente.Apellido + "', '" +
                                  cliente.Email + "', '" +
                                  cliente.Telefono + "')";
                cmd.ExecuteNonQuery();
                return "ok";
            }
            catch (Exception ex)
            {
                return "error: " + ex.Message;
            }
            finally
            {
                conexion.CerrarConexion();
            }
        }
        public string actualizar(ClientesModel cliente)
        {
            try
            {
                cmd.Connection = conexion.AbrirConexion();
                cmd.CommandText = "update Clientes set Nombre='" + cliente.Nombre +
                                  "', Apellido='" + cliente.Apellido +
                                  "', Email='" + cliente.Email +
                                  "', Telefono='" + cliente.Telefono +
                                  "' where Cliente_id=" + cliente.Cliente_id;
                cmd.ExecuteNonQuery();
                return "ok";
            }
            catch (Exception ex)
            {
                return "error: " + ex.Message;
            }
            finally
            {
                conexion.CerrarConexion();
            }
        }
        public string eliminar(ClientesModel cliente)
        {
            try
            {
                cmd.Connection = conexion.AbrirConexion();
                cmd.CommandText = "delete Clientes where Cliente_id=" + cliente.Cliente_id;
                cmd.ExecuteNonQuery();
                return "ok";
            }
            catch (Exception ex)
            {
                return "error: " + ex.Message;
            }
            finally
            {
                conexion.CerrarConexion();
            }
        }
    }
}
