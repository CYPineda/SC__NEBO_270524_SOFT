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
    public partial class Frm_Pagos_Prestamos : Form
    {

        Clases.DB db = new Clases.DB();
        Clases.Asistente a = new Clases.Asistente();

        public string id_socio, nombre_socio;


        public Frm_Pagos_Prestamos()
        {
            InitializeComponent();
        }

        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();

        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);

        private void pbSalir_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Frm_Pagos_Prestamos_Load(object sender, EventArgs e)
        {
            this.Text = Clases.Env.APPNAME + " | PAGOS DE PRÉSTAMOS | " + Clases.Auth.user + " | " + Clases.Auth.rol;

            txtIdSocio.Text = id_socio;
            txtNomb_Socio.Text = nombre_socio;
            Get_Prestamo_Socio();
        }

        private void btnSeleccionar_Prestamo_Click(object sender, EventArgs e)
        {
            //Lleva el nombre del socio al formulario donde se hara el abono de prestamos
            string nomb_socio;
            nomb_socio = txtNomb_Socio.Text;

            Formularios.Formularios_de_Menu.Prestamos.Frm_Prestamos_por_Persona form = new Frm_Prestamos_por_Persona();
            this.AddOwnedForm(form);

            form.nombre_socio = nomb_socio;

            //Recolecto el interes del socio que seleccione
            if (DgvData.SelectedCells.Count > 0)
            {
                int rowIndex = DgvData.SelectedCells[5].RowIndex;
                DataGridViewRow selectedRow = DgvData.Rows[rowIndex];
                string interes = Convert.ToString(selectedRow.Cells["dcInteres"].Value);

                int rowIndex2 = DgvData.SelectedCells[4].RowIndex;
                DataGridViewRow selectedRow2 = DgvData.Rows[rowIndex2];
                string cantida_lps = Convert.ToString(selectedRow2.Cells["dcCantidad_LPS"].Value);

                int rowIndex3 = DgvData.SelectedCells[0].RowIndex;
                DataGridViewRow selectedRow3 = DgvData.Rows[rowIndex3];
                string no_prestamo = Convert.ToString(selectedRow3.Cells["dcNo_Prestamo"].Value);

                //int rowIndex4 = DgvData.SelectedCells[1].RowIndex;
                //DataGridViewRow selectedRow4 = DgvData.Rows[rowIndex4];
                //string fecha_inicial = Convert.ToString(selectedRow4.Cells["DcFecha_Inicial"].Value);

                int rowIndex5 = DgvData.SelectedCells[6].RowIndex;
                DataGridViewRow selectedRow5 = DgvData.Rows[rowIndex5];
                string monto_pendiente = Convert.ToString(selectedRow5.Cells["dcMonto_Pendiente"].Value);

                string condicion = "PRESTAMO_CODIGO='" + no_prestamo + "'";
                string fecha_pago_anterior = db.Hook("FECHA_ULTIMO_PAGO", "PRESTAMOS", condicion);

                form.interes = interes;
                form.cantida_lps = cantida_lps;
                form.no_prestamo = no_prestamo;
                form.fecha_anterior = fecha_pago_anterior;
                form.monto_pendi = monto_pendiente;
            }

            form.Show();
        }





        private void btnRegresar_Click(object sender, EventArgs e)
        {
            

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnRegresar_Click_1(object sender, EventArgs e)
        {
            Formularios.Formularios_de_Menu.Prestamos.Frm_Reporte_Prestamo form = new Frm_Reporte_Prestamo();
            this.AddOwnedForm(form);
            form.Show();
            this.Hide();
        }

        private void panel2_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void Get_Prestamo_Socio()
        {
            string id_socio;

            id_socio = txtIdSocio.Text;

            string query = "SELECT A.PRESTAMO_CODIGO, A.FECHA_INICIO, A.FECHA_FINAL, " +
                "C.TIPO_PRESTAMO, A.CANTIDAD_LPS, A.INTERES, A.MONTO_PENDIENTE, A.FECHA_ULTIMO_PAGO  " +
                "FROM PRESTAMOS A INNER JOIN SOCIOS B ON(A.ID_SOCIO = B.ID_SOCIO) " +
                "INNER JOIN TIPO_DE_PRESTAMO C ON(A.ID_TIPO_PRESTAMO = C.ID_TIPO_PRESTAMO) " +
                "INNER JOIN FORMA_DE_PAGO D ON(A.ID_FORMA_PAGO = D.ID_FORMA_PAGO) " +
                "WHERE A.ID_SOCIO = '"+ id_socio + "' AND A.MONTO_PENDIENTE > 0" +
                " GROUP BY A.PRESTAMO_CODIGO, A.FECHA_INICIO, A.FECHA_FINAL, " +
                "C.TIPO_PRESTAMO, A.CANTIDAD_LPS, A.INTERES, A.MONTO_PENDIENTE, A.FECHA_ULTIMO_PAGO ORDER BY A.PRESTAMO_CODIGO ASC";

            DataTable prestamos = db.RawSQL(query);

            DgvData.Rows.Clear();

            string _presta_codigo, _fecha_ini, _fecha_fin, _tip_prestamo, _interes, ultfechapago;
            string _cantidad, _monto_pendiente;

            double TotalDeuda = 0;

            for (int i = 0; i < prestamos.Rows.Count; i++)
            {

                _presta_codigo = prestamos.Rows[i][0].ToString();
                _fecha_ini = Convert.ToDateTime(prestamos.Rows[i][1].ToString()).ToShortDateString();
                _fecha_fin = Convert.ToDateTime(prestamos.Rows[i][2].ToString()).ToShortDateString();
                _tip_prestamo = prestamos.Rows[i][3].ToString();
                _cantidad = prestamos.Rows[i][4].ToString();
                _interes = prestamos.Rows[i][5].ToString();
                _monto_pendiente = prestamos.Rows[i][6].ToString();
                ultfechapago = Convert.ToDateTime(prestamos.Rows[i][7].ToString()).ToShortDateString(); ;

                DgvData.Rows.Add(_presta_codigo, _fecha_ini, _fecha_fin, _tip_prestamo, a.ReturnsNumber(_cantidad).ToString("N2"), _interes, a.ReturnsNumber(_monto_pendiente).ToString("N2"), ultfechapago);
                
                TotalDeuda += a.ReturnsNumber(_cantidad);

                //lblResumen.Text = "La deuda total es de L: " + TotalDeuda.ToString() + ".00 ";
                //lblResumen.Text = TotalDeuda.ToString("N2");
                prestamos.Dispose();

            }

        }

    }
}
