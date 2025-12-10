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

        // Limpiar formulario
        private void limpiarFormulario()
        {
            txtNombre.Clear();
            txtApellidos.Clear();
            txtEmail.Clear();
            dgvClientes.ClearSelection();
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
                // Crea un objeto cliente con datos del formulario
                var c = new Clientes
                {
                    Nombre = txtNombre.Text,
                    Apellidos = txtApellidos.Text,
                    Email = txtEmail.Text
                };

                // Llama a la API para crear cliente
                api.Crear(c);

                listar();
                limpiarFormulario();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Editar cliente
        private void btnEditar_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvClientes.CurrentRow != null)
                {
                    // Obtiene el cliente seleccionado en el DGV
                    var c = (Clientes)dgvClientes.CurrentRow.DataBoundItem;

                    // Actualiza datos con valores del formulario
                    c.Nombre = txtNombre.Text;
                    c.Apellidos = txtApellidos.Text;
                    c.Email = txtEmail.Text;

                    // Llama a la API para editar cliente
                    api.Editar(c);
                    listar();
                }
                else
                {
                    MessageBox.Show("No hay Cliente seleccionado.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Eliminar cliente
        private void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvClientes.CurrentRow != null)
                {
                    // Obtiene del ID del cliente seleccionado
                    int id = ((Clientes)dgvClientes.CurrentRow.DataBoundItem).IdCliente;

                    // Confirma eliminación
                    var confirmacion = MessageBox.Show("¿Quieres eliminar este cliente?", "Confirmar eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (confirmacion == DialogResult.Yes)
                    {
                        // Llama a la API para eliminar cliente
                        api.Eliminar(id);
                        listar();
                        limpiarFormulario();
                    }
                }
                else {
                    MessageBox.Show("No hay Cliente seleccionado.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvClientes_SelectionChanged(object sender, EventArgs e)
        {
            // Comprueba que hay cliente seleccionado
            if (dgvClientes.CurrentRow != null)
            {
                // Obtiene el objeto Clientes del DGV
                var selectC = (Clientes)dgvClientes.CurrentRow.DataBoundItem;

                // Si el cliente no es nulo rellenalos campos
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
