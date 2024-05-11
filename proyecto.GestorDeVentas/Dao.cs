using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace proyecto.GestorDeVentas
{
    public class Dao
    {
        private string connectionString = "server=localhost;" +
            "user=progra2024;" +
            "password=progra2024;" +
            "database=progra1_2024;";

        // Método para obtener todos los productos de la base de datos
        public List<Producto> ObtenerTodosLosProductos()
        {
            List<Producto> listaProductos = new List<Producto>();

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                //aperturar conexion
                conn.Open();
                //diseñar la consulta
                string query = "SELECT id, nombre, descripcion, precio, cantidad_disponible, codigo_producto FROM productos";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Producto producto = new Producto();
                            producto.Id = Convert.ToInt32(reader["id"]);
                            producto.Nombre = reader["nombre"].ToString();
                            producto.Descripcion = reader["descripcion"].ToString();
                            producto.Precio = Convert.ToDecimal(reader["precio"]);
                            producto.CantidadDisponible = Convert.ToInt32(reader["cantidad_disponible"]);
                            producto.CodigoProducto = reader["codigo_producto"].ToString();

                            listaProductos.Add(producto);
                        }
                    }
                }
            }

            return listaProductos;
        }

        // Método para obtener un producto por su ID
        public Producto ObtenerProductoPorId(int id)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();

                string query = "SELECT id, nombre, descripcion, precio, cantidad_disponible, codigo_producto FROM productos WHERE id = @id";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@id", id);

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            Producto producto = new Producto();
                            producto.Id = Convert.ToInt32(reader["id"]);
                            producto.Nombre = reader["nombre"].ToString();
                            producto.Descripcion = reader["descripcion"].ToString();
                            producto.Precio = Convert.ToDecimal(reader["precio"]);
                            producto.CantidadDisponible = Convert.ToInt32(reader["cantidad_disponible"]);
                            producto.CodigoProducto = reader["codigo_producto"].ToString();

                            return producto;
                        }
                        else
                        {
                            return null;
                        }
                    }
                }
            }
        }

        // Método para insertar un nuevo producto en la base de datos
        public void InsertarProducto(Producto producto)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();

                string query = "INSERT INTO productos (nombre, descripcion, precio, cantidad_disponible, codigo_producto) VALUES " +
                    "(@nombre, @descripcion, @precio, @cantidad_disponible, @codigo_producto)";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@nombre", producto.Nombre);
                    cmd.Parameters.AddWithValue("@descripcion", producto.Descripcion);
                    cmd.Parameters.AddWithValue("@precio", producto.Precio);
                    cmd.Parameters.AddWithValue("@cantidad_disponible", producto.CantidadDisponible);
                    cmd.Parameters.AddWithValue("@codigo_producto", producto.CodigoProducto);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        // Método para actualizar un producto en la base de datos
        public void ActualizarProducto(Producto producto)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();

                string query = "UPDATE productos SET nombre = @nombre, descripcion = @descripcion, " +
                    "precio = @precio, cantidad_disponible = @cantidad_disponible, codigo_producto = @codigo_producto WHERE id = @id";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@nombre", producto.Nombre);
                    cmd.Parameters.AddWithValue("@descripcion", producto.Descripcion);
                    cmd.Parameters.AddWithValue("@precio", producto.Precio);
                    cmd.Parameters.AddWithValue("@cantidad_disponible", producto.CantidadDisponible);
                    cmd.Parameters.AddWithValue("@codigo_producto", producto.CodigoProducto);
                    cmd.Parameters.AddWithValue("@id", producto.Id);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        // Método para eliminar un producto de la base de datos por su ID
        public void EliminarProducto(int id)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();

                string query = "DELETE FROM productos WHERE id = @id";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@id", id);

                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
