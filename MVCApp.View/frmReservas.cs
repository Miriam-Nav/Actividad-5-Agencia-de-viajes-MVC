using System;
using System.Linq;
using System.Windows.Forms;
using MVCApp.Controller;
using MVCApp.Model;

namespace MVCApp.View
{
    public partial class frmReservas : Form
    {
        private ReservaAPI reservaApi = new ReservaAPI();
        private ClienteAPI clienteApi = new ClienteAPI();
        private ViajeAPI viajeApi = new ViajeAPI();

        public frmReservas()
        {
            InitializeComponent();
            cargarCombos();
            listar();
        }

        // Cargar combos de clientes y viajes
        private void cargarCombos()
        {
            cmbClientes.DataSource = clienteApi.Listar();
            cmbClientes.DisplayMember = "Nombre";
            cmbClientes.ValueMember = "IdCliente";

            cmbViajes.DataSource = viajeApi.Listar();
            cmbViajes.DisplayMember = "Destino";
            cmbViajes.ValueMember = "IdViaje";
        }

        // Listar reservas
        private void listar()
        {
            dgvReservas.DataSource = reservaApi.Listar();

            dgvReservas.Columns["Clientes"].Visible = false;
            dgvReservas.Columns["Viajes"].Visible = false;

            dgvReservas.ClearSelection();
        }

        private void limpiarFormulario()
        {
            // Limpiar combos
            cmbClientes.SelectedIndex = -1;
            cmbViajes.SelectedIndex = -1;

            // Limpiar fecha
            txtFechaReserva.Clear();

            // Deseleccionar filas en el DataGridView
            dgvReservas.ClearSelection();
        }


        // Crear reserva
        private void btnCrear_Click(object sender, EventArgs e)
        {
            try
            {
                int idCliente = (int)cmbClientes.SelectedValue;
                int idViaje = (int)cmbViajes.SelectedValue;

                // Validar plazas disponibles antes de crear
                var viaje = viajeApi.Listar().FirstOrDefault(v => v.IdViaje == idViaje);
                
                reservaApi.Crear(idCliente, idViaje);
                listar();
                limpiarFormulario();
                MessageBox.Show("Reserva creada correctamente");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }

        // Cancelar reserva
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvReservas.CurrentRow != null)
                {
                    int idReserva = ((Reservas)dgvReservas.CurrentRow.DataBoundItem).IdReserva;
                    reservaApi.Cancelar(idReserva);
                    listar();
                    limpiarFormulario();
                    MessageBox.Show("Reserva cancelada correctamente");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }
        private void dgvReservas_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvReservas.CurrentRow != null)
            {
                var reserva = (Reservas)dgvReservas.CurrentRow.DataBoundItem;

                if (reserva != null)
                {
                    // Seleccionar cliente en el combo
                    cmbClientes.SelectedValue = reserva.IdCliente;

                    // Seleccionar viaje en el combo
                    cmbViajes.SelectedValue = reserva.IdViaje;

                    // Si quieres mostrar la fecha en un TextBox
                    txtFechaReserva.Text = reserva.FechaReserva.ToString("dd/MM/yyyy");
                }
            }
        }
    }
}

