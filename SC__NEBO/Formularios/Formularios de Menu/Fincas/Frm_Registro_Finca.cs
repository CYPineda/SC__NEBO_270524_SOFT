using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SC__NEBO.Formularios.Formularios_de_Menu.Fincas
{
    public partial class Frm_Registro_Finca : Form
    {

        Clases.Asistente a = new Clases.Asistente();
        Clases.DB db = new Clases.DB();

        public string id_socio, socio;

        public Frm_Registro_Finca()
        {
            InitializeComponent();
        }

        private void btnRegresar_Click(object sender, EventArgs e)
        {

        }

        private void Frm_Registro_Finca_Load(object sender, EventArgs e)
        {
            txtIdSocio.Text = id_socio;
            txtNomb_Socio.Text = socio;


        }
    }
}
