using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SC__NEBO.Formularios.Formularios_de_Menu.Socios
{
    public partial class FrmLista_Socios_Contratos : Form
    {

        Clases.DB db = new Clases.DB();
        Clases.Asistente a = new Clases.Asistente();

        public FrmLista_Socios_Contratos()
        {
            InitializeComponent();
        }

        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();

        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);

        private void FrmLista_Socios_Contratos_Load(object sender, EventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void BtnBuscar_Click(object sender, EventArgs e)
        {
            GetSocioInfo(a.Clean(txtBuscar.Text.Trim()));
        }

        private void GetSocioInfo(string id)
        {
            string campos = "ID_SOCIO, NOMBRE, DNI, TELEFONO, DIRECCION";
            string condicion = "NOMBRE LIKE '%" + id + "%' AND DEL = 'N'";
            DataTable data = db.Find("SOCIOS", campos, condicion);

            DgvData.Rows.Clear();

            string _id_cliente, _nombre, _dni, _telefono, _direccion;

            int i;
            for (i = 0; i < data.Rows.Count; i++)
            {
                _id_cliente = data.Rows[i][0].ToString();
                _nombre = data.Rows[i][1].ToString();
                _dni = data.Rows[i][2].ToString();
                _telefono = data.Rows[i][3].ToString();
                _direccion = data.Rows[i][4].ToString();

                DgvData.Rows.Add(_id_cliente, _nombre, _dni, _telefono, _direccion);
            }

            data.Dispose();
        }

        private void txtBuscar_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                GetSocioInfo(a.Clean(txtBuscar.Text.Trim()));
            }
        }

        private void pbSalir_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void DgvData_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (DgvData.Rows.Count > 0)
            {
                string cod_socio = DgvData.CurrentRow.Cells[0].Value.ToString();
                Formularios.Formularios_de_Menu.Contratos.Frm_Contratos formulario = new Contratos.Frm_Contratos();
                formulario = ((Formularios.Formularios_de_Menu.Contratos.Frm_Contratos)Owner);
                formulario.CodSocio(cod_socio);
                Close();
            }
        }
    }
}
