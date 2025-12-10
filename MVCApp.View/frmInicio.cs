using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MVCApp.View
{
    public partial class frmInicio : Form
    {
        public frmInicio()
        {
            InitializeComponent();
        }

        private void btnClientes_Click(object sender, EventArgs e)
        {
            // FORMULARIO DE CLIENTES
            frmClientes f = new frmClientes();
            f.ShowDialog();
        }

        private void btnViajes_Click(object sender, EventArgs e)
        {
            // FORMULARIO DE VIAJES
            frmViajes f = new frmViajes();
            f.ShowDialog();
        }

        private void btnReservas_Click(object sender, EventArgs e)
        {
            // FORMULARIO DE RESERVAS
            frmReservas f = new frmReservas();
            f.ShowDialog();
        }
    }
}
