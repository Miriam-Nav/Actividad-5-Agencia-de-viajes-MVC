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

            // Oculta las columnas de Clientes y Viajes
            dgvReservas.Columns["Clientes"].Visible = false;
            dgvReservas.Columns["Viajes"].Visible = false;

            // Limpia la selección
            dgvReservas.ClearSelection();
        }

        private void limpiarFormulario()
        {
            // Limpiar combos
            cmbClientes.SelectedIndex = -1;
            cmbViajes.SelectedIndex = -1;

            // Limpiar fecha
            txtFechaReserva.Clear();

            // Deselecciona filas en el DataGridView
            dgvReservas.ClearSelection();
            dgvReservas.CurrentCell = null;
        }


        // Crear reserva
        private void btnCrear_Click(object sender, EventArgs e)
        {
            try
            {
                // IDs de cliente y viaje
                int idCliente = (int)cmbClientes.SelectedValue;
                int idViaje = (int)cmbViajes.SelectedValue;

                // Validar plazas disponibles antes de crear
                var viaje = viajeApi.Listar().FirstOrDefault(v => v.IdViaje == idViaje);

                // Llama al metodo de crear reserva
                reservaApi.Crear(idCliente, idViaje, txtFechaReserva.Text);

                listar();
                limpiarFormulario();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }

        // Cancelar reserva
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvReservas.CurrentRow != null)
                {
                    // Saca el id de la reserva
                    int idReserva = ((Reservas)dgvReservas.CurrentRow.DataBoundItem).IdReserva;

                    // Confirma cancelación
                    var confirmacion = MessageBox.Show("¿Quieres cancelar esta reserva?", 
                        "Confirmar cancelación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    
                    // Solo se cancela si el usuario pulsa SI
                    if (confirmacion == DialogResult.Yes)
                    {
                        // Llama al metodo de cancelar reserva
                        reservaApi.Cancelar(idReserva);  
                        listar();                        
                        limpiarFormulario();             
                    }
                }
                else
                {
                    MessageBox.Show("No hay Reserva seleccionada.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvReservas_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvReservas.CurrentRow != null)
            {
                // Saca el objeto reserva
                var reserva = (Reservas)dgvReservas.CurrentRow.DataBoundItem;

                if (reserva != null)
                {
                    // Seleccionar cliente 
                    cmbClientes.SelectedValue = reserva.IdCliente;

                    // Seleccionar viaje 
                    cmbViajes.SelectedValue = reserva.IdViaje;

                    // Muestra la fecha en un TextBox
                    txtFechaReserva.Text = reserva.FechaReserva.ToString("dd/MM/yyyy");
                }
            }
        }

        private void btnFiltrar_Click(object sender, EventArgs e)
        {
            try
            {
                // Filtrar por cliente
                if (cbCliente.Checked && !cbViaje.Checked)
                {
                    if (cmbClientes.SelectedValue != null)
                    {
                        // Selecciona el cliente
                        int idCliente = (int)cmbClientes.SelectedValue;
                        dgvReservas.DataSource = reservaApi.ReservasPorCliente(idCliente);
                    }
                    else
                    {
                        MessageBox.Show("Seleccione un cliente para filtrar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }

                // Filtrar por viaje
                else if (cbViaje.Checked && !cbCliente.Checked)
                {
                    if (cmbViajes.SelectedValue != null)
                    {
                        // Selecciona el viaje
                        int idViaje = (int)cmbViajes.SelectedValue;
                        dgvReservas.DataSource = reservaApi.ReservasPorViaje(idViaje);
                    }
                    else
                    {
                        MessageBox.Show("Seleccione un viaje para filtrar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                // Ambos checkbox marcados filtran por cliente y viaje a la vez
                else if (cbCliente.Checked && cbViaje.Checked)
                {
                    if (cmbClientes.SelectedValue != null && cmbViajes.SelectedValue != null)
                    {
                        // Selecciona cliente y viaje
                        int idCliente = (int)cmbClientes.SelectedValue;
                        int idViaje = (int)cmbViajes.SelectedValue;

                        // Filtrar combinando cliente y viaje
                        var reservas = reservaApi.ReservasPorCliente(idCliente).Where(reserva => reserva.IdViaje == idViaje).ToList();

                        dgvReservas.DataSource = reservas;
                    }
                    else
                    {
                        MessageBox.Show("Seleccione cliente y viaje para filtrar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }                
                else
                {
                    // Si no hay ninguno marcado muestra todas las reservas
                    listar();
                }

                // Ocultar columnas
                dgvReservas.Columns["Clientes"].Visible = false;
                dgvReservas.Columns["Viajes"].Visible = false;
                dgvReservas.ClearSelection();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }
}

