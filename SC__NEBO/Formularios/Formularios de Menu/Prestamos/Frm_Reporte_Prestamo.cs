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

namespace SC__NEBO.Formularios.Formularios_de_Menu.Prestamos
{
    public partial class Frm_Reporte_Prestamo : Form
    {

        Clases.Asistente a = new Clases.Asistente();
        Clases.DB db = new Clases.DB();

        public Frm_Reporte_Prestamo()
        {
            InitializeComponent();
        }

        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();

        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);


        private void Frm_Reporte_Prestamo_Load(object sender, EventArgs e)
        {
            this.Text = Clases.Env.APPNAME + " | REPORTE DE PRÉSTAMOS | " + Clases.Auth.user + " | " + Clases.Auth.rol;

            GetSocio();
            SumaLps();
        }

        private void SumaLps()
        {
            double total_lps = 0;

            for (int i = 0; i < DgvData.Rows.Count; i++)
            {
                total_lps += Convert.ToDouble(DgvData.Rows[i].Cells[2].Value.ToString());
            }

            lblTotal.Text = total_lps.ToString("N2");
        }

        //Mostrar el monto de prestamos de cada cliente
        private void GetSocio()
        {
            String query = "SELECT A.ID_SOCIO,  B.NOMBRE AS SOCIO, " +
                "SUM(ALL A.MONTO_PENDIENTE) AS RESTA FROM PRESTAMOS A INNER JOIN SOCIOS B " +
                "ON(B.ID_SOCIO = A.ID_SOCIO) WHERE  A.MONTO_PENDIENTE > 0 AND A.DEL = 'N'" +
                "GROUP BY A.ID_SOCIO, B.NOMBRE ORDER BY RESTA DESC ";


            DataTable reporte = db.RawSQL(query);


            string _idsocio, _socio, _acumulado;
            int i;

            DgvData.Rows.Clear();

            for (i = 0; i < reporte.Rows.Count; i++)
            {
                _idsocio = reporte.Rows[i][0].ToString();
                _socio = reporte.Rows[i][1].ToString();
                //_fechainicio = reporte.Rows[i][2].ToString();
                _acumulado = reporte.Rows[i][2].ToString();

                DgvData.Rows.Add(_idsocio, _socio, a.ReturnsNumber(_acumulado).ToString("N2"));
            }

            lblTotal.Text = "Mostrando " + reporte.Rows.Count.ToString() + " registros de " + db.Count("PRESTAMOS", "DEL = 'N'").ToString();

            reporte.Dispose();

        }

        //Buscar con nombre y fecha
        private void GetPrestamoInfo(string id)
        {
            string campos = " A.ID_SOCIO,  B.NOMBRE AS SOCIO, " +
                "SUM(ALL A.MONTO_PENDIENTE) AS RESTA FROM PRESTAMOS A INNER JOIN SOCIOS B " +
                "ON(B.ID_SOCIO = A.ID_SOCIO) ";

            string condicion;

            if (id != "")
            {
                condicion = "A.MONTO_PENDIENTE > 0 AND B.NOMBRE LIKE '%" + id + "%'  GROUP BY " +
                    "A.ID_SOCIO,  B.NOMBRE, A.MONTO_PENDIENTE";
            }
            else
            {
                condicion = "";

            }

            DataTable data = db.Join(campos, condicion, "B.NOMBRE");

            DgvData.Rows.Clear();

            string _idsocio, _nombre, _montop;

            int i;
            for (i = 0; i < data.Rows.Count; i++)
            {
                _idsocio = data.Rows[i][0].ToString();
                _nombre = data.Rows[i][1].ToString();
                _montop = data.Rows[i][2].ToString();

                DgvData.Rows.Add(_idsocio, _nombre, _montop);
            }

            data.Dispose();
        }

        private void panel2_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }


        //LLeva al formulario de Frm_Pagos_Prestamos
        private void DgvData_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            //string _socio = DgvData.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();

            //Independiente de la columa que se seleccione mostrará la 1ra columa de esa fila
            string _id_socio = DgvData.Rows[e.RowIndex].Cells[0].Value.ToString();

            //Independiente de la columa que se seleccione mostrará la 3ra columa de esa fila
            string _socio = DgvData.Rows[e.RowIndex].Cells[1].Value.ToString();

            Formularios.Formularios_de_Menu.Prestamos.Frm_Pagos_Prestamos form = new Frm_Pagos_Prestamos();
            this.AddOwnedForm(form);

            //nombre_socio es la variable publica que recibe la info en el formulario de Frm_Pagos_Prestamos
            form.nombre_socio = _socio;
            form.id_socio = _id_socio;
            form.Show();
            this.Close();
        }

        private void btnRegresar_Click(object sender, EventArgs e)
        {

        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            GetPrestamoInfo(a.Clean(txtNombre.Text.Trim()));
        }

        private void btnFactura_Click(object sender, EventArgs e)
        {
            Formularios.Formularios_de_Menu.Prestamos.Frm_Prestamos_General form = new Formularios_de_Menu.Prestamos.Frm_Prestamos_General();
            this.AddOwnedForm(form);
            form.Show();
            this.Hide();
        }

        private void pbSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void DgvData_CellDoubleClick_1(object sender, DataGridViewCellEventArgs e)
        {
            //string _socio = DgvData.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();

            //Independiente de la columa que se seleccione mostrará la 1ra columa de esa fila
            string _id_socio = DgvData.Rows[e.RowIndex].Cells[0].Value.ToString();

            //Independiente de la columa que se seleccione mostrará la 3ra columa de esa fila
            string _socio = DgvData.Rows[e.RowIndex].Cells[1].Value.ToString();

            Formularios.Formularios_de_Menu.Prestamos.Frm_Pagos_Prestamos form = new Frm_Pagos_Prestamos();
            this.AddOwnedForm(form);

            //nombre_socio es la variable publica que recibe la info en el formulario de Frm_Pagos_Prestamos
            form.nombre_socio = _socio;
            form.id_socio = _id_socio;

            form.ShowDialog();
        }

        private void btnNuevo_Prestamo_Click(object sender, EventArgs e)
        {
            Formularios.Formularios_de_Menu.Prestamos.Frm_Prestamos form = new Frm_Prestamos();
            this.AddOwnedForm(form);
            form.Show();
            this.Hide();
        }

        private void txtNombre_KeyDown(object sender, KeyEventArgs e)
        {
            //if (e.KeyCode == Keys.Enter)
            //{
            //    GetPrestamoInfo(a.Clean(txtNombre.Text.Trim()));
            //}
            
            if (e.KeyCode == Keys.Enter)
            {
                btnBuscar.PerformClick();
            }
        }

        private void btnHistorial_Click(object sender, EventArgs e)
        {
            Formularios.Formularios_de_Menu.Prestamos.FrmHistorialPrestamos form = new FrmHistorialPrestamos();
            this.AddOwnedForm(form);
            form.Show();
            this.Hide();
        }
    }
}
