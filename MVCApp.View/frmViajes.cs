using System;
using System.Windows.Forms;
using MVCApp.Controller;
using MVCApp.Model;

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
            dgvViajes.CurrentCell = null;
        }

        // Crear viaje
        private void btnCrear_Click(object sender, EventArgs e)
        {
            try
            {
                var v = new Viajes
                {
                    Destino = txtDestino.Text,
                    Precio = decimal.Parse(txtPrecio.Text),
                    PlazasDisponibles = int.Parse(txtPlazas.Text)
                };

                api.Crear(v);
                listar();
                limpiarFormulario();
                MessageBox.Show("Viaje creado correctamente");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }

        // Editar viaje
        private void btnEditar_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvViajes.CurrentRow != null)
                {
                    var v = (Viajes)dgvViajes.CurrentRow.DataBoundItem;
                    v.Destino = txtDestino.Text;
                    v.Precio = decimal.Parse(txtPrecio.Text);
                    v.PlazasDisponibles = int.Parse(txtPlazas.Text);

                    api.Editar(v);
                    listar();
                    MessageBox.Show("Viaje editado correctamente");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }

        // Eliminar viaje
        private void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvViajes.CurrentRow != null)
                {
                    int id = ((Viajes)dgvViajes.CurrentRow.DataBoundItem).IdViaje;
                    api.Eliminar(id);
                    listar();
                    limpiarFormulario();
                    MessageBox.Show("Viaje eliminado correctamente");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }

        // Al seleccionar un viaje en el DataGridView
        private void dgvViajes_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvViajes.CurrentRow != null)
            {
                var selectV = (Viajes)dgvViajes.CurrentRow.DataBoundItem;
                if (selectV != null)
                {
                    txtDestino.Text = selectV.Destino;
                    txtPrecio.Text = selectV.Precio.ToString();
                    txtPlazas.Text = selectV.PlazasDisponibles.ToString();
                }
            }
        }
    }
}
