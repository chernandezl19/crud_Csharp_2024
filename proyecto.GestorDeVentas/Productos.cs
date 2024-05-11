using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace proyecto.GestorDeVentas
{
    public partial class Productos : Form
    {

        private bool esNuevo = false;
        public Productos()
        {
            InitializeComponent();
        }

        private void habilitarCampos(bool readOnly)
        {
            txtNombre.ReadOnly = readOnly;
            txtDescripcion.ReadOnly = readOnly;
            txtDisponibilidad.ReadOnly = readOnly;
            txtPrecio.ReadOnly = readOnly;
            txtCodigo.ReadOnly = readOnly;
        }
        private void listarProductos()
        {
            Dao dao = new Dao();
            DgvProductos.DataSource = dao.ObtenerTodosLosProductos();
        }

        private void Productos_Load(object sender, EventArgs e)
        {
            listarProductos();
        }

        private void HabDeshabGuardarCancelar(bool enable)
        {
            btnGuardar.Enabled = enable;
            btnCancelar.Enabled = enable;
        }
        private void btnCrear_Click(object sender, EventArgs e)
        {
            habilitarCampos(false);
            HabDeshabGuardarCancelar(true);
            esNuevo = true;
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            limpiarCampos();
            habilitarCampos(true);
            HabDeshabGuardarCancelar(false);
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (esNuevo)
            {
                createProd();
            }
            else
            {
                modifyProd();
            }
            listarProductos();
            limpiarCampos();
            HabDeshabGuardarCancelar(false);
        }

        private void modifyProd()
        {
            Dao dao = new Dao();
            Producto producto = new Producto();

            DataGridViewRow Fila = DgvProductos.SelectedRows[0];
            int id = (int)Fila.Cells[0].Value;
            producto.Id = id;
            producto.Nombre = txtNombre.Text;
            producto.Descripcion = txtDescripcion.Text;
            producto.Precio = Decimal.Parse(txtPrecio.Text);
            producto.CantidadDisponible = int.Parse(txtDisponibilidad.Text);
            producto.CodigoProducto = txtCodigo.Text;
            dao.ActualizarProducto(producto);
            habilitarCampos(true);
            listarProductos();
        }
        private void createProd()
        {
            Dao dao = new Dao();
            Producto producto = new Producto();
            producto.Nombre = txtNombre.Text;
            producto.Descripcion = txtDescripcion.Text;
            producto.Precio = Decimal.Parse(txtPrecio.Text);
            producto.CantidadDisponible = int.Parse(txtDisponibilidad.Text);
            producto.CodigoProducto = txtCodigo.Text;
            dao.InsertarProducto(producto);
            habilitarCampos(true);
            listarProductos();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            DataGridViewRow Fila = DgvProductos.SelectedRows[0];
            int id = (int)Fila.Cells[0].Value;
            Dao dao = new Dao();
            dao.EliminarProducto(id);
            listarProductos();

        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            DataGridViewRow Fila = DgvProductos.SelectedRows[0];
            txtNombre.Text = (String)Fila.Cells[1].Value;
            txtDescripcion.Text = (String)Fila.Cells[2].Value;
            txtPrecio.Text = ((decimal)Fila.Cells[3].Value).ToString();
            txtDisponibilidad.Text = ((int)Fila.Cells[4].Value).ToString();
            txtCodigo.Text = (String)Fila.Cells[5].Value;
            habilitarCampos(false);
            HabDeshabGuardarCancelar(true);
            esNuevo = false;
        }
        private void limpiarCampos()
        {
            txtNombre.Text = "";
            txtDescripcion.Text = "";
            txtPrecio.Text = "";
            txtDisponibilidad.Text = "";
            txtCodigo.Text = "";
        }
    }
}
