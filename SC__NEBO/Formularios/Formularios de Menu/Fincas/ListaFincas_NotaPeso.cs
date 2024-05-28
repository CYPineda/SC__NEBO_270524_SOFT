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
    public partial class ListaFincas_NotaPeso : Form
    {
        Clases.DB db = new Clases.DB();
        Clases.Asistente a = new Clases.Asistente();

        public string idsocio, socio;

        public ListaFincas_NotaPeso()
        {
            InitializeComponent();
        }

        private void ListaFincas_NotaPeso_Load(object sender, EventArgs e)
        {
            this.Text = Clases.Env.APPNAME + " | LISTA DE FINCAS DEL PRODUCTOR | " + Clases.Auth.user + " | " + Clases.Auth.rol;

            txtIdCliente.Text = idsocio;
            txtCliente.Text = socio;

            Get_Prestamo_Socio();
        }

        private void DgvData_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (DgvData.Rows.Count > 0)
            {
                string cod_finca = DgvData.CurrentRow.Cells[0].Value.ToString();
                Formularios.Formularios_de_Menu.Notas_de_Peso.Frm_Notas_Peso finca = new Notas_de_Peso.Frm_Notas_Peso();
                finca = ((Formularios.Formularios_de_Menu.Notas_de_Peso.Frm_Notas_Peso)Owner);
                finca.CodFinca(cod_finca);
                Close();
            }
        }

        private void pbSalir_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void pbSalir_Click_1(object sender, EventArgs e)
        {
            Close();
        }

        private void Get_Prestamo_Socio()
        {
            string id_socio;

            id_socio = txtIdCliente.Text;

            string query = "SELECT IDFINCA, NOMBREFINCA, CANTMANZ, AREAPROD, UBICACION FROM FINCAS " +
                "WHERE IDSOCIO = '" + id_socio + "' ";
            
            DataTable prestamos = db.RawSQL(query);

            DgvData.Rows.Clear();

            string idfinca, _finca, _cantmanz, _AreaProd, _Ubicacion;

            for (int i = 0; i < prestamos.Rows.Count; i++)
            {
                idfinca = prestamos.Rows[i][0].ToString();
                _finca = prestamos.Rows[i][1].ToString();
                _cantmanz = prestamos.Rows[i][2].ToString();
                _AreaProd = prestamos.Rows[i][3].ToString();
                _Ubicacion = prestamos.Rows[i][4].ToString();

                DgvData.Rows.Add(idfinca, _finca, _cantmanz, _AreaProd, _Ubicacion);

                //lblResumen.Text = "La deuda total es de L: " + TotalDeuda.ToString() + ".00 ";
                //lblResumen.Text = TotalDeuda.ToString("N2");
                prestamos.Dispose();

            }

        }
    }
}
