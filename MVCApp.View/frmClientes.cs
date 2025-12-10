using System;
using System.Windows.Forms;
using MVCApp.Controller;
using MVCApp.Model;

namespace MVCApp.View
{
    public partial class frmClientes : Form
    {
        private ClienteAPI api = new ClienteAPI();

        public frmClientes()
        {
            InitializeComponent();
            listar();
        }

        private void limpiarFormulario()
        {
            txtNombre.Clear();
            txtApellidos.Clear();
            txtEmail.Clear();
            dgvClientes.CurrentCell = null;
        }


        // Listar clientes
        private void listar()
        {
            dgvClientes.DataSource = api.Listar();
            dgvClientes.ClearSelection();
        }

        // Crear cliente
        private void btnCrear_Click(object sender, EventArgs e)
        {
            try
            {
                var c = new Clientes
                {
                    Nombre = txtNombre.Text,
                    Apellidos = txtApellidos.Text,
                    Email = txtEmail.Text
                };

                api.Crear(c);
                listar();
                limpiarFormulario();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }

        // Editar cliente
        private void btnEditar_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvClientes.CurrentRow != null)
                {
                    var c = (Clientes)dgvClientes.CurrentRow.DataBoundItem;
                    c.Nombre = txtNombre.Text;
                    c.Apellidos = txtApellidos.Text;
                    c.Email = txtEmail.Text;

                    api.Editar(c);
                    listar();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }

        // Eliminar cliente
        private void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvClientes.CurrentRow != null)
                {
                    int id = ((Clientes)dgvClientes.CurrentRow.DataBoundItem).IdCliente;
                    api.Eliminar(id);
                    listar();
                    limpiarFormulario();
                    MessageBox.Show("Cliente eliminado correctamente");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }

        private void dgvClientes_SelectionChanged(object sender, EventArgs e)
        {
            // Comprueba que hay cliente seleccionado
            if (dgvClientes.CurrentRow != null)
            {
                // Obtenemos el objeto Clientes directamente del DataGridView
                var selectC = (Clientes)dgvClientes.CurrentRow.DataBoundItem;

                // Si el cliente no es nulo, rellenamos los campos
                if (selectC != null)
                {
                    txtNombre.Text = selectC.Nombre;
                    txtApellidos.Text = selectC.Apellidos;
                    txtEmail.Text = selectC.Email;
                }
            }
        }

    }
}
