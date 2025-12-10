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
            //ClienteAPI api = new ClienteAPI();
            //dgvClientes.DataSource = api.Listar();

        }

        private void btnClientes_Click(object sender, EventArgs e)
        {
            frmClientes f = new frmClientes();
            f.ShowDialog();
        }

        private void btnViajes_Click(object sender, EventArgs e)
        {
            frmViajes f = new frmViajes();
            f.ShowDialog();
        }

        private void btnReservas_Click(object sender, EventArgs e)
        {
            frmReservas f = new frmReservas();
            f.ShowDialog();
        }
    }
}
