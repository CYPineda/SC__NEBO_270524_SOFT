using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Runtime.InteropServices;

namespace SC__NEBO.Formularios.Formularios_de_Menu.Clientes
{
    public partial class Frm_ListaClientes_NotaPeso : Form
    {

        Clases.DB db = new Clases.DB();
        Clases.Asistente a = new Clases.Asistente();
        public Frm_ListaClientes_NotaPeso()
        {
            InitializeComponent();
        }

        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();

        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);

        private void Frm_ListaClientes_NotaPeso_Load(object sender, EventArgs e)
        {
            this.Text = Clases.Env.APPNAME + " | LISTA DE SOCIOS | " + Clases.Auth.user + " | " + Clases.Auth.rol;
            GetSocio();
        }

        private void GetSocio(string search = "")
        {
            string campos, condicion;
            campos = "ID_SOCIO, NOMBRE, DNI, TELEFONO, DIRECCION";

            if (search != "")
            {
                condicion = "NOMBRE LIKE '%" + search + "%' AND DEL = 'N'";

            }
            else
            {
                condicion = "DEL = 'N'";
            }

            DataTable data = db.Find("SOCIOS", campos, condicion);

            DgvData.Rows.Clear();

            int i;
            string _id_cliente, _nombre, _dni, _telefono, _direccion;

            for (i = 0; i < data.Rows.Count; i++)
            {
                _id_cliente = data.Rows[i][0].ToString();
                _nombre = data.Rows[i][1].ToString();
                _dni = data.Rows[i][2].ToString();
                _telefono = data.Rows[i][3].ToString();
                _direccion = data.Rows[i][4].ToString();
                DgvData.Rows.Add(_id_cliente, _nombre, _dni, _telefono, _direccion);
            }

            lblResumen.Text = "Mostrando " + data.Rows.Count.ToString() + " registros de " + db.Count("SOCIOS", "DEL = 'N'").ToString();
            data.Dispose();
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

        private void DgvData_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (DgvData.Rows.Count > 0)
            {
                string cod_socio = DgvData.CurrentRow.Cells[0].Value.ToString();
                Formularios.Formularios_de_Menu.Notas_de_Peso.Frm_Notas_Peso prestamos = new Notas_de_Peso.Frm_Notas_Peso();
                prestamos = ((Formularios.Formularios_de_Menu.Notas_de_Peso.Frm_Notas_Peso)Owner);
                prestamos.CodSocio(cod_socio);
                Close();
            }
        }

        private void BtnBuscar_Click(object sender, EventArgs e)
        {
            string search = a.Clean(txtBuscar.Text.Trim());
            GetSocio(search);
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

        private void panel2_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void pbMinimizar_Click(object sender, EventArgs e)
        {

        }
    }
}
