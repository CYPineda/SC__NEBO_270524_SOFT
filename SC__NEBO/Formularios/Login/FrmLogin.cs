using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SC__NEBO.Formularios.Login
{
    public partial class FrmLogin : Form
    {

        Clases.Bootstrapper bs = new Clases.Bootstrapper();
        Clases.Auth auth = new Clases.Auth();
        Clases.Asistente h = new Clases.Asistente();

        public FrmLogin()
        {
            InitializeComponent();
        }

        private void BtnLogin_Click(object sender, EventArgs e)
        {
            string usuario, clave;
            usuario = TxtUsuario.Text.Trim();
            clave = auth.MakeHash(TxtClave.Text.Trim());

            bool response = auth.MakeLogin(usuario, clave);

            if (response == true)
            {
                Formularios.Frm_Menu dashboard = new Formularios.Frm_Menu();
                dashboard.Show();
                this.Hide();
            }
        }

        private void FrmLogin_Load(object sender, EventArgs e)
        {
            this.Text = Clases.Env.APPNAME + "LOGIN  ";
            TxtUsuario.Select();
            bs.CheckFileConfiguration();
            auth.RegisterSuperUser();
        }

        private void LblSALIR_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void CbMostrar_CheckedChanged(object sender, EventArgs e)
        {
            if (CbMostrar.Checked == false)
            {
                TxtClave.UseSystemPasswordChar = true;

            }
            else
            {
                TxtClave.UseSystemPasswordChar = false;
            }
        }

        private void TxtClave_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                string usuario, clave;
                usuario = TxtUsuario.Text.Trim();
                clave = auth.MakeHash(TxtClave.Text.Trim());

                bool response = auth.MakeLogin(usuario, clave);

                if (response == true)
                {
                    Formularios.Frm_Menu dashboard = new Formularios.Frm_Menu();
                    dashboard.Show();
                    this.Hide();

                    //Evita el sonido de alerta
                    e.SuppressKeyPress = true;
                }
            }
        }
    }
}
