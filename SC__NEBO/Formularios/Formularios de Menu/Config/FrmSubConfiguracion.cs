using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SC__NEBO.Formularios.Formularios_de_Menu.Config
{
    public partial class FrmSubConfiguracion : Form
    {
        public FrmSubConfiguracion()
        {
            InitializeComponent();
        }

        private void btnInfGeneral_Click(object sender, EventArgs e)
        {
            Formularios.Formularios_de_Menu.Config.FrmInfoGral frm = new Formularios_de_Menu.Config.FrmInfoGral();
            this.AddOwnedForm(frm);
            frm.Show();
            this.Hide();
        }

        private void btnConfigUsuario_Click(object sender, EventArgs e)
        {
            Formularios.Formularios_de_Menu.Usuarios.FrmUsuarios frm = new Usuarios.FrmUsuarios();
            this.AddOwnedForm(frm);
            frm.Show();
            this.Hide();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
