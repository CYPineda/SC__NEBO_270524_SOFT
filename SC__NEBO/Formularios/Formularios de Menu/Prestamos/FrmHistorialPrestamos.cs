using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SC__NEBO.Formularios.Formularios_de_Menu.Prestamos
{
    public partial class FrmHistorialPrestamos : Form
    {

        Clases.DB db = new Clases.DB();
        Clases.Asistente a = new Clases.Asistente();

        public FrmHistorialPrestamos()
        {
            InitializeComponent();
        }

        private void pbSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FrmHistorialPrestamos_Load(object sender, EventArgs e)
        {
            Get_Prestamos();
            this.Text = Clases.Env.APPNAME + " | HISTORIAL DE PRÉSTAMOS | " + Clases.Auth.user + " | " + Clases.Auth.rol;

        }

        private void Get_Prestamos()
        {

            string query = "SELECT A.PRESTAMO_CODIGO, A.FECHA_INICIO, B.NOMBRE, " +
                "C.TIPO_PRESTAMO, A.CANTIDAD_LPS, A.INTERES, A.MONTO_PENDIENTE, A.ESTADO  " +
                "FROM PRESTAMOS A INNER JOIN SOCIOS B ON(A.ID_SOCIO = B.ID_SOCIO) " +
                "INNER JOIN TIPO_DE_PRESTAMO C ON(A.ID_TIPO_PRESTAMO = C.ID_TIPO_PRESTAMO) " +
                "INNER JOIN FORMA_DE_PAGO D ON(A.ID_FORMA_PAGO = D.ID_FORMA_PAGO) " +
                " GROUP BY A.PRESTAMO_CODIGO, A.FECHA_INICIO, B.NOMBRE, " +
                "C.TIPO_PRESTAMO, A.CANTIDAD_LPS, A.INTERES, A.MONTO_PENDIENTE, A.ESTADO ORDER BY A.PRESTAMO_CODIGO DESC";

            DataTable prestamos = db.RawSQL(query);

            DgvData.Rows.Clear();

            string _presta_codigo, _fecha_ini, _nombre, _tip_prestamo, _estado;
            string _cantidad, _interes, _monto_pendiente;

            double TotalDeuda = 0;

            for (int i = 0; i < prestamos.Rows.Count; i++)
            {

                _presta_codigo = prestamos.Rows[i][0].ToString();
                _fecha_ini = Convert.ToDateTime(prestamos.Rows[i][1].ToString()).ToShortDateString();
                _nombre = prestamos.Rows[i][2].ToString();
                _tip_prestamo = prestamos.Rows[i][3].ToString();
                _cantidad = prestamos.Rows[i][4].ToString();
                _interes = prestamos.Rows[i][5].ToString();
                _monto_pendiente = prestamos.Rows[i][6].ToString();
                _estado = prestamos.Rows[i][7].ToString(); ;

                DgvData.Rows.Add(_presta_codigo, _fecha_ini, _nombre, _tip_prestamo, a.ReturnsNumber(_cantidad).ToString("N2"), a.ReturnsNumber(_interes).ToString(), a.ReturnsNumber(_monto_pendiente).ToString("N2"), _estado);

                TotalDeuda += a.ReturnsNumber(_cantidad);
                if (_estado == "PAGADO")
                {

                    DgvData.Rows[i].DefaultCellStyle.ForeColor = Color.FromArgb(21, 93, 39);
                    DgvData.Rows[i].DefaultCellStyle.SelectionForeColor = Color.FromArgb(253, 253, 253);
                    DgvData.Rows[i].DefaultCellStyle.SelectionBackColor = Color.FromArgb(45, 198, 83);

                    //TotalUtilidades += _utilidad;
                    //clientesvip++;
                }
                //lblResumen.Text = "La deuda total es de L: " + TotalDeuda.ToString() + ".00 ";
                //lblResumen.Text = TotalDeuda.ToString("N2");
                prestamos.Dispose();

            }

        }


        private void GetPrestamoInfo(string id)
        {
            string campos = " A.PRESTAMO_CODIGO, A.FECHA_INICIO, B.NOMBRE, " +
                "C.TIPO_PRESTAMO, A.CANTIDAD_LPS, A.INTERES, A.MONTO_PENDIENTE, A.ESTADO  " +
                "FROM PRESTAMOS A INNER JOIN SOCIOS B ON(A.ID_SOCIO = B.ID_SOCIO) " +
                "INNER JOIN TIPO_DE_PRESTAMO C ON(A.ID_TIPO_PRESTAMO = C.ID_TIPO_PRESTAMO) " +
                "INNER JOIN FORMA_DE_PAGO D ON(A.ID_FORMA_PAGO = D.ID_FORMA_PAGO) ";

            string condicion;

            if (id != "")
            {
                condicion = " B.NOMBRE LIKE '%" + id + "%'  GROUP BY " +
                    "A.PRESTAMO_CODIGO, A.FECHA_INICIO, B.NOMBRE, C.TIPO_PRESTAMO, A.CANTIDAD_LPS, " +
                    "A.INTERES, A.MONTO_PENDIENTE, A.ESTADO";
            }
            else
            {
                condicion = "";

            }

            DataTable prestamos = db.Join(campos, condicion, "B.NOMBRE");

            DgvData.Rows.Clear();

            string _presta_codigo, _fecha_ini, _nombre, _tip_prestamo, _estado;
            string _cantidad, _interes, _monto_pendiente;

            for (int i = 0; i < prestamos.Rows.Count; i++)
            {

                _presta_codigo = prestamos.Rows[i][0].ToString();
                _fecha_ini = Convert.ToDateTime(prestamos.Rows[i][1].ToString()).ToShortDateString();
                _nombre = prestamos.Rows[i][2].ToString();
                _tip_prestamo = prestamos.Rows[i][3].ToString();
                _cantidad = prestamos.Rows[i][4].ToString();
                _interes = prestamos.Rows[i][5].ToString();
                _monto_pendiente = prestamos.Rows[i][6].ToString();
                _estado = prestamos.Rows[i][7].ToString(); ;

                DgvData.Rows.Add(_presta_codigo, _fecha_ini, _nombre, _tip_prestamo, a.ReturnsNumber(_cantidad).ToString("N2"), a.ReturnsNumber(_interes).ToString(), a.ReturnsNumber(_monto_pendiente).ToString("N2"), _estado);

                if (_estado == "PAGADO")
                {

                    DgvData.Rows[i].DefaultCellStyle.ForeColor = Color.FromArgb(21, 93, 39);
                    DgvData.Rows[i].DefaultCellStyle.SelectionForeColor = Color.FromArgb(253, 253, 253);
                    DgvData.Rows[i].DefaultCellStyle.SelectionBackColor = Color.FromArgb(45, 198, 83);

                    //TotalUtilidades += _utilidad;
                    //clientesvip++;
                }
            }

            prestamos.Dispose();
        }

        private void txtNomb_Socio_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnBuscar.PerformClick();
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            GetPrestamoInfo(a.Clean(txtNomb_Socio.Text.Trim()));
        }

        private void btnRealizarPago_Click(object sender, EventArgs e)
        {
            //Formularios.Formularios_de_Menu.Prestamos.Frm_Prestamos_por_Persona form = new Frm_Prestamos_por_Persona();
            //this.AddOwnedForm(form);

            Formularios.Formularios_de_Menu.Prestamos.FrmPagoXPrestamo form = new Formularios.Formularios_de_Menu.Prestamos.FrmPagoXPrestamo();
            this.AddOwnedForm(form);
            if (DgvData.SelectedCells.Count > 0)
            {
                int rowIndex = DgvData.SelectedCells[5].RowIndex;
                DataGridViewRow selectedRow = DgvData.Rows[rowIndex];
                string interes = Convert.ToString(selectedRow.Cells["dcInteres"].Value);

                int rowIndex2 = DgvData.SelectedCells[4].RowIndex;
                DataGridViewRow selectedRow2 = DgvData.Rows[rowIndex2];
                string cantida_lps = Convert.ToString(selectedRow2.Cells["dcMonto"].Value);

                int rowIndex4 = DgvData.SelectedCells[2].RowIndex;
                DataGridViewRow selectedRow4 = DgvData.Rows[rowIndex4];
                string socio_nomb = Convert.ToString(selectedRow4.Cells["DcSocio"].Value);

                int rowIndex3 = DgvData.SelectedCells[0].RowIndex;
                DataGridViewRow selectedRow3 = DgvData.Rows[rowIndex3];
                string no_prestamo = Convert.ToString(selectedRow3.Cells["dcNo_Prestamo"].Value);

                int rowIndex5 = DgvData.SelectedCells[6].RowIndex;
                DataGridViewRow selectedRow5 = DgvData.Rows[rowIndex5];
                string monto_pendiente = Convert.ToString(selectedRow5.Cells["dcMonto_Pendiente"].Value);

                int rowIndex6 = DgvData.SelectedCells[7].RowIndex;
                DataGridViewRow selectedRow6 = DgvData.Rows[rowIndex5];
                string estado = Convert.ToString(selectedRow6.Cells["DcEstado"].Value);

                string condicion = "PRESTAMO_CODIGO='" + no_prestamo + "'";
                string fecha_pago_anterior = db.Hook("FECHA_INICIO_PAGO", "PRESTAMOS", condicion);

                form.nombre_socio = socio_nomb;
                form.interes = interes;
                form.cantidad_lps = cantida_lps;
                form.no_prestamo = no_prestamo;
                form.fecha_inicial = fecha_pago_anterior;
                form.monto_pendiente = monto_pendiente;
                form.int_acum = db.Hook("INTERES_ACUMULADO", "PRESTAMOS", $"PRESTAMO_CODIGO='{no_prestamo}'");
                form.id_socio = db.Hook("ID_SOCIO","PRESTAMOS",$"PRESTAMO_CODIGO='{no_prestamo}'");
                form.abono = 0.ToString("N2");


                if (estado == "ACTIVO")
                {
                    form.Show();
                }
                else
                {
                    a.Advertencia("ESTE PRESTAMO YA FUE CANCELADO");
                }
            }


        }

        private void btnRegresar_Click(object sender, EventArgs e)
        {
            Formularios.Formularios_de_Menu.Prestamos.FrmHistorialPrestamos form = new FrmHistorialPrestamos();
            this.AddOwnedForm(form);
            form.Show();
            this.Hide();
        }

        private void btnRecibo_Click(object sender, EventArgs e)
        {
            if (DgvData.Rows.Count > 0)
            {
                string codpagare = DgvData.CurrentRow.Cells[0].Value.ToString();
                Reportes.FrmRptComprobanteEgreso_Copia EgresoCopia = new Reportes.FrmRptComprobanteEgreso_Copia();
                EgresoCopia.codPrestamo = codpagare;
                EgresoCopia.Show();
            }
        }

        private void btnPagare_Click(object sender, EventArgs e)
        {
            if (DgvData.Rows.Count > 0)
            {
                string codpagare = DgvData.CurrentRow.Cells[0].Value.ToString();
                Reportes.FrmRptPagare_Copia PagareCopia = new Reportes.FrmRptPagare_Copia();
                PagareCopia.codPrestamo = codpagare;
                PagareCopia.Show();
            }
        }

        private void btnEstadoCuenta_Click(object sender, EventArgs e)
        {
            Formularios.Formularios_de_Menu.Prestamos.Frm_Estado_Cuenta form = new Formularios.Formularios_de_Menu.Prestamos.Frm_Estado_Cuenta();
            this.AddOwnedForm(form);
            if (DgvData.SelectedCells.Count > 0)
            {

                int rowIndex4 = DgvData.SelectedCells[2].RowIndex;
                DataGridViewRow selectedRow4 = DgvData.Rows[rowIndex4];
                string socio_nomb = Convert.ToString(selectedRow4.Cells["DcSocio"].Value);

                int rowIndex3 = DgvData.SelectedCells[0].RowIndex;
                DataGridViewRow selectedRow3 = DgvData.Rows[rowIndex3];
                string no_prestamo = Convert.ToString(selectedRow3.Cells["dcNo_Prestamo"].Value);

                form.nombre_socio = socio_nomb;

                form.no_prestamo = no_prestamo;

                form.cod_socio = db.Hook("ID_SOCIO", "PRESTAMOS", $"PRESTAMO_CODIGO='{no_prestamo}'");

                form.Show();
            }
        }
    }
}
