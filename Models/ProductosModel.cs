using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using GestionVentas.Config;

namespace GestionVentas.Models
{
    internal class ProductosModel
    {
        public int Producto_id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public decimal Precio { get; set; }
        public int Stock { get; set; }

        List<ProductosModel> listaProductos = new List<ProductosModel>();
        private Conexion conexion = new Conexion();
        SqlCommand cmd = new SqlCommand();

        public List<ProductosModel> todos()
        {
            string cadena = "select *  from Productos";
            SqlDataAdapter adapter = new SqlDataAdapter(cadena, conexion.AbrirConexion());
            DataTable tabla = new DataTable();
            adapter.Fill(tabla);
            foreach (DataRow producto in tabla.Rows)
            {
                ProductosModel nuevoproducto = new ProductosModel
                {
                    Producto_id = Convert.ToInt32(producto["Producto_id"]),
                    Nombre = producto["Nombre"].ToString(),
                    Descripcion = producto["Descripcion"].ToString(),
                    Precio = Convert.ToDecimal(producto["Precio"]),
                    Stock = Convert.ToInt32(producto["Stock"])
                };
                listaProductos.Add(nuevoproducto);
            }
            conexion.CerrarConexion();
            return listaProductos;
        }
        public ProductosModel uno(ProductosModel producto)
        {
            string cadena = "select * from Productos where Producto_id = " + producto.Producto_id;
            cmd = new SqlCommand(cadena, conexion.AbrirConexion());
            SqlDataReader lector = cmd.ExecuteReader();

            lector.Read();
            ProductosModel productoRegresa = new ProductosModel
            {
                Producto_id = Convert.ToInt32(lector["Producto_id"]),
                Nombre = lector["Nombre"].ToString(),
                Descripcion = lector["Descripcion"].ToString(),
                Precio = Convert.ToDecimal(lector["Precio"]),
                Stock = Convert.ToInt32(lector["Stock"])
            };
            conexion.CerrarConexion();
            return productoRegresa;
        }
        public string insertar(ProductosModel producto)
        {
            try
            {
                cmd.Connection = conexion.AbrirConexion();
                cmd.CommandText = "insert into Productos (Nombre, Descripcion, Precio, Stock) values ('" +
                                  producto.Nombre + "', '" +
                                  producto.Descripcion + "', " +
                                  producto.Precio + ", " +
                                  producto.Stock + ")";
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
        public string actualizar(ProductosModel producto)
        {
            try
            {
                cmd.Connection = conexion.AbrirConexion();
                cmd.CommandText = "update Productos set Nombre='" + producto.Nombre +
                                  "', Descripcion='" + producto.Descripcion +
                                  "', Precio=" + producto.Precio +
                                  ", Stock=" + producto.Stock +
                                  " where Producto_id=" + producto.Producto_id;
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
        public string eliminar(ProductosModel producto)
        {
            try
            {
                cmd.Connection = conexion.AbrirConexion();
                cmd.CommandText = "delete from Productos where Producto_id=" + producto.Producto_id;
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

