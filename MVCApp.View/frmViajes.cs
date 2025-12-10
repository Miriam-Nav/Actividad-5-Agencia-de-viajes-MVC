using MVCApp.Controller;
using MVCApp.Model;
using System;
using System.Drawing;
using System.Security.Cryptography;
using System.Windows.Forms;

namespace MVCApp.View
{
    public partial class frmViajes : Form
    {
        private ViajeAPI api = new ViajeAPI();

        public frmViajes()
        {
            InitializeComponent();
            listar();
        }

        // Listar viajes
        private void listar()
        {
            // Llama al método de listar
            dgvViajes.DataSource = api.Listar();

            dgvViajes.Columns["Reservas"].Visible = false;
            dgvViajes.ClearSelection();
        }

        // Limpiar formulario
        private void limpiarFormulario()
        {
            txtDestino.Clear();
            txtPrecio.Clear();
            txtPlazas.Clear();
            dgvViajes.ClearSelection();
            dgvViajes.CurrentCell = null;
        }

        // Crear viaje
        private void btnCrear_Click(object sender, EventArgs e)
        {
            try
            {
                // Crea objeto viaje con datos del formulario
                var v = new Viajes
                {
                    Destino = txtDestino.Text,
                    Precio = decimal.Parse(txtPrecio.Text),
                    PlazasDisponibles = int.Parse(txtPlazas.Text)
                };

                // Llama a la API para crear viaje
                api.Crear(v);

                listar();
                limpiarFormulario();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Editar viaje
        private void btnEditar_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvViajes.CurrentRow != null)
                {
                    // Viaje seleccionado en el DGV
                    var viaje = (Viajes)dgvViajes.CurrentRow.DataBoundItem;

                    // Actualiza datos con valores del formulario
                    viaje.Destino = txtDestino.Text;
                    viaje.Precio = decimal.Parse(txtPrecio.Text);
                    viaje.PlazasDisponibles = int.Parse(txtPlazas.Text);

                    // Llama a la API para editar viaje
                    api.Editar(viaje);
                    listar();
                }
                else
                {
                    MessageBox.Show("No hay Viaje seleccionado.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Eliminar viaje
        private void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvViajes.CurrentRow != null)
                {
                    // ID del viaje seleccionado
                    int id = ((Viajes)dgvViajes.CurrentRow.DataBoundItem).IdViaje;

                    // Llama a la API para eliminar viaje
                    api.Eliminar(id);

                    listar();
                    limpiarFormulario();
                }
                else
                {
                    MessageBox.Show("No hay Viaje seleccionado.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Al seleccionar un viaje en el DataGridView
        private void dgvViajes_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvViajes.CurrentRow != null)
            {
                // Viaje seleccionado
                var selectV = (Viajes)dgvViajes.CurrentRow.DataBoundItem;
                if (selectV != null)
                {
                    // Rellena los campos con los datos del viaje
                    txtDestino.Text = selectV.Destino;
                    txtPrecio.Text = selectV.Precio.ToString();
                    txtPlazas.Text = selectV.PlazasDisponibles.ToString();
                }
            }
        }
        

    }
}
