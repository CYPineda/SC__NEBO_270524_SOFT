using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SC__NEBO.Formularios.Formularios_de_Menu.IHCAFE
{
    public partial class ListaFinca_Ihcafe : Form
    {
        Clases.DB db = new Clases.DB();
        Clases.Asistente a = new Clases.Asistente();

        public string idsocio, socio;

        public ListaFinca_Ihcafe()
        {
            InitializeComponent();
        }

        private void pbSalir_Click(object sender, EventArgs e)
        {
            Close();

        }

        private void Get_Finca()
        {
            string id_socio;

            id_socio = txtIdCliente.Text;

            string query = "SELECT IDFINCA, NOMBREFINCA, CANTMANZ, AREAPROD, UBICACION FROM FINCAS " +
                "WHERE IDSOCIO = '" + id_socio + "' ";

            DataTable fincas = db.RawSQL(query);
            DgvData.Rows.Clear();

            string _id_finca, _nombre_finca, _cant_mz, _areapord, ubicacion;

            for (int i = 0; i < fincas.Rows.Count; i++)
            {
                _id_finca = fincas.Rows[i][0].ToString();
                _nombre_finca = fincas.Rows[i][1].ToString();
                _cant_mz = fincas.Rows[i][2].ToString();
                _areapord = fincas.Rows[i][3].ToString();
                ubicacion = fincas.Rows[i][4].ToString();

                DgvData.Rows.Add(_id_finca, _nombre_finca, _cant_mz, _areapord, ubicacion);
                fincas.Dispose();
            }
           
        }

        private void DgvData_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (DgvData.Rows.Count>0)
            {
                string cod_finca = DgvData.CurrentRow.Cells[0].Value.ToString();
                Formularios.Formularios_de_Menu.IHCAFE.Frm_ComprobantesIHCAFE comp = new Formularios.Formularios_de_Menu.IHCAFE.Frm_ComprobantesIHCAFE();
                comp = ((Formularios.Formularios_de_Menu.IHCAFE.Frm_ComprobantesIHCAFE)Owner);
                comp.CodFinca(cod_finca);
                Close();
            }
        }

        private void ListaFinca_Ihcafe_Load(object sender, EventArgs e)
        {
            this.Text = Clases.Env.APPNAME + " | LISTA DE FINCAS DEL PRODUCTOR | " + Clases.Auth.user + " | " + Clases.Auth.rol;


            txtCliente.Text = socio;
            txtIdCliente.Text = idsocio;

            Get_Finca();
        }
    }
}
