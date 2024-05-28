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
    public partial class Frm_Prestamos_General : Form
    {

        Clases.DB db = new Clases.DB();
        Clases.Asistente a = new Clases.Asistente();
        public Frm_Prestamos_General()
        {
            InitializeComponent();
        }

        private void btnReporte_Click(object sender, EventArgs e)
        {
            if (rbtnActivo.Checked == true)
            {
                Reportes.FrmRptLista_PrestamosxFecha listaP = new Reportes.FrmRptLista_PrestamosxFecha();
                listaP.fechaini = fechai;
                listaP.fechafin = fechaf;
                listaP.estadop = "ACTIVO";
                listaP.nombre = nombre_;

                listaP.Show();
            }
            else if(rbtPagado.Checked == true)
            {
                Reportes.FrmRptLista_PrestamosxFecha listaP = new Reportes.FrmRptLista_PrestamosxFecha();
                listaP.fechaini = fechai;
                listaP.fechafin = fechaf;
                listaP.estadop = "PAGADO";
                listaP.nombre = nombre_;

                listaP.Show();
            }
            else if (rbtnTodos.Checked == true)
            {
                Reportes.FrmRptLista_PrestamosxFecha listaP = new Reportes.FrmRptLista_PrestamosxFecha();
                listaP.fechaini = fechai;
                listaP.fechafin = fechaf;

                listaP.nombre = nombre_;

                listaP.Show();
            }
            else
            {
                Reportes.FrmRptLista_PrestamosxFecha listaP = new Reportes.FrmRptLista_PrestamosxFecha();
                listaP.fechaini = fechai;
                listaP.fechafin = fechaf;
                listaP.estadop = null;
                listaP.nombre = null;

                listaP.Show();
            }
            
        }

        private void pbSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Frm_Prestamos_General_Load(object sender, EventArgs e)
        {
            GetPrestamo();
            this.Text = Clases.Env.APPNAME + " | FORMULARIO GENERAL DE PRÉSTAMOS | " + Clases.Auth.user + " | " + Clases.Auth.rol;
            //Estado_Prestamo();
        }

        //Mostrar el monto de prestamos de cada cliente
        private void GetPrestamo()
        {
            String query = "SELECT A.PRESTAMO_CODIGO AS CÓDIGO,  B.NOMBRE AS SOCIO, A.FECHA_INICIO AS FECHA, " +
                "A.CANTIDAD_LPS AS MONTO, A.INTERES AS INTERÉS, A.ESTADO FROM PRESTAMOS A INNER JOIN " +
                "SOCIOS B ON(B.ID_SOCIO = A.ID_SOCIO) " +
                "GROUP BY A.PRESTAMO_CODIGO, B.NOMBRE,  A.FECHA_INICIO, A.CANTIDAD_LPS, A.INTERES, A.ESTADO " +
                "ORDER BY A.PRESTAMO_CODIGO DESC";

            DataTable reporte = db.RawSQL(query);


            string _cod_prestamo, _socio, _fecha, _monto, _interes, _estado;
            int i;

            DgvData.Rows.Clear();

            for (i = 0; i < reporte.Rows.Count; i++)
            {
                _cod_prestamo = reporte.Rows[i][0].ToString();
                _socio = reporte.Rows[i][1].ToString();
                _fecha = Convert.ToDateTime(reporte.Rows[i][2].ToString()).ToShortDateString();
                _monto = reporte.Rows[i][3].ToString();
                _interes = reporte.Rows[i][4].ToString();
                _estado = reporte.Rows[i][5].ToString();

                DgvData.Rows.Add(_cod_prestamo, _socio, _fecha, a.ReturnsNumber(_monto).ToString("N2"), _interes, _estado);

                if (_estado == "PAGADO")
                {

                    DgvData.Rows[i].DefaultCellStyle.ForeColor = Color.FromArgb(21, 93, 39);
                    DgvData.Rows[i].DefaultCellStyle.SelectionForeColor = Color.FromArgb(253, 253, 253);
                    DgvData.Rows[i].DefaultCellStyle.SelectionBackColor = Color.FromArgb(45, 198, 83);
                }
            }

            //lblResumen.Text = "Mostrando " + reporte.Rows.Count.ToString() + " registros de " + db.Count("PRESTAMOS", "DEL = 'N'").ToString();

            reporte.Dispose();

        }

        //private void GetPrestamo_Pagado ()
        //{

        //    string query = " A.PRESTAMO_CODIGO AS CÓDIGO,  B.NOMBRE AS SOCIO, A.FECHA_INICIO AS FECHA, " +
        //        "A.CANTIDAD_LPS AS MONTO, A.INTERES AS INTERÉS FROM PRESTAMOS A INNER JOIN " +
        //        "SOCIOS B ON(B.ID_SOCIO = A.ID_SOCIO) ";
        //        //"GROUP BY A.PRESTAMO_CODIGO, B.NOMBRE,  A.FECHA_INICIO, A.CANTIDAD_LPS, A.INTERES " +
        //        //"ORDER BY B.NOMBRE DESC ";

        //    string condicion="";

        //    if (rbtnPagado.Checked == true)
        //    {
        //        condicion = " A.ESTADO = 'PAGADO' GROUP BY " +
        //            "A.PRESTAMO_CODIGO, B.NOMBRE,  A.FECHA_INICIO, A.CANTIDAD_LPS, A.INTERES ";

        //        //lblResumen.Text = "Mostrando " + db.Count("PRESTAMOS", "ESTADO = 'PAGADO'").ToString() + " registros de " + DgvData.Rows.Count.ToString();
        //        //"ORDER BY B.NOMBRE DESC";
        //    }
        //    else if (rbtnActivo.Checked == true)
        //    {
        //        condicion = " A.ESTADO = 'ACTIVO' ";
        //        //lblResumen.Text = "Mostrando " + db.Count("PRESTAMOS", "ESTADO = 'ACTIVO'").ToString() + " registros de " + DgvData.Rows.Count.ToString();

        //    }
        //    else
        //    {
        //        condicion = "";
        //    }


        //    DataTable reporte = db.Join(query, condicion, "A.PRESTAMO_CODIGO DESC");


        //    string _cod_prestamo, _socio, _fecha, _monto, _interes;
        //    int i;

        //    DgvData.Rows.Clear();

        //    for (i = 0; i < reporte.Rows.Count; i++)
        //    {
        //        _cod_prestamo = reporte.Rows[i][0].ToString();
        //        _socio = reporte.Rows[i][1].ToString();
        //        _fecha = Convert.ToDateTime(reporte.Rows[i][2].ToString()).ToShortDateString();
        //        _monto = reporte.Rows[i][3].ToString();
        //        _interes = reporte.Rows[i][4].ToString();

        //        DgvData.Rows.Add(_cod_prestamo, _socio, _fecha, _monto, _interes);
        //    }

        //    reporte.Dispose();

        //}



        //private void GetPrestamo_Estado()
        //{
        //    string fechai_, fechaf_;
        //    fechai_ = dtpFechaIncial.Value.ToString("yyyy-MM-dd");
        //    fechaf_ = dtpFechaFinal.Value.ToString("yyyy-MM-dd");

        //    string query = " A.PRESTAMO_CODIGO AS CÓDIGO,  B.NOMBRE AS SOCIO, A.FECHA_INICIO AS FECHA, " +
        //        "A.CANTIDAD_LPS AS MONTO, A.INTERES AS INTERÉS, A.ESTADO FROM PRESTAMOS A INNER JOIN " +
        //        "SOCIOS B ON(B.ID_SOCIO = A.ID_SOCIO) ";

        //    string condicion = "";

        //    if (rbtnActivo.Checked == true)
        //    {
        //        condicion = " A.ESTADO ='ACTIVO' GROUP BY " +
        //            "A.PRESTAMO_CODIGO,  B.NOMBRE, A.FECHA_INICIO, A.CANTIDAD_LPS, A.INTERES, A.ESTADO";
        //    }
        //    else if (rbtnTodos.Checked == true)
        //    {
        //        condicion = " A.ESTADO ='PAGADO' GROUP BY " +
        //            "A.PRESTAMO_CODIGO,  B.NOMBRE, A.FECHA_INICIO, A.CANTIDAD_LPS, A.INTERES, A.ESTADO";
        //    }
        //    else
        //    {
        //        condicion = "";
        //    }

        //    DataTable reporte = db.Join(query, condicion, "A.PRESTAMO_CODIGO DESC");


        //    string _cod_prestamo, _socio, _fecha, _monto, _interes, _estado;
        //    int i;

        //    DgvData.Rows.Clear();

        //    for (i = 0; i < reporte.Rows.Count; i++)
        //    {
        //        _cod_prestamo = reporte.Rows[i][0].ToString();
        //        _socio = reporte.Rows[i][1].ToString();
        //        _fecha = Convert.ToDateTime(reporte.Rows[i][2].ToString()).ToShortDateString();
        //        _monto = reporte.Rows[i][3].ToString();
        //        _interes = reporte.Rows[i][4].ToString();
        //        _estado = reporte.Rows[i][5].ToString();

        //        DgvData.Rows.Add(_cod_prestamo, _socio, _fecha, _monto, _interes, _estado);
        //    }

        //    reporte.Dispose();
        //}

        string fechai, fechaf, nombre_;
        private void GetPrestamo_Nombre(string id)
        {

            fechai = dtpFechaIncial.Value.ToString("yyyy-MM-dd");
            fechaf = dtpFechaFinal.Value.ToString("yyyy-MM-dd");

            string query = " A.PRESTAMO_CODIGO AS CÓDIGO,  B.NOMBRE AS SOCIO, A.FECHA_INICIO AS FECHA, " +
                "A.CANTIDAD_LPS AS MONTO, A.INTERES AS INTERÉS, A.ESTADO FROM PRESTAMOS A INNER JOIN " +
                "SOCIOS B ON(B.ID_SOCIO = A.ID_SOCIO) ";

            string condicion = "";

            if (rbtnTodos.Checked == true)
            {
                condicion = " NOMBRE LIKE '%" + id + "%' AND A.FECHA_INICIO BETWEEN '" + fechai + "' AND '" + fechaf + "' GROUP BY " +
                    "A.PRESTAMO_CODIGO,  B.NOMBRE, A.FECHA_INICIO, A.CANTIDAD_LPS, A.INTERES, A.ESTADO";
                nombre_ = id;
            }
            else if (rbtnActivo.Checked == true)
            {
                condicion = " NOMBRE LIKE '%" + id + "%' AND A.FECHA_INICIO BETWEEN '" + fechai + "' AND '" + fechaf + "' AND A.ESTADO = 'ACTIVO' GROUP BY " +
                    "A.PRESTAMO_CODIGO,  B.NOMBRE, A.FECHA_INICIO, A.CANTIDAD_LPS, A.INTERES, A.ESTADO";
                nombre_ = id;
            }
            else if (rbtPagado.Checked == true)
            {
                condicion = " NOMBRE LIKE '%" + id + "%' AND A.FECHA_INICIO BETWEEN '" + fechai + "' AND '" + fechaf + "' AND A.ESTADO = 'PAGADO' GROUP BY " +
                    "A.PRESTAMO_CODIGO,  B.NOMBRE, A.FECHA_INICIO, A.CANTIDAD_LPS, A.INTERES, A.ESTADO";
                nombre_ = id;
            }
            else
            {
                condicion = "";
            }


            //if (id != "")
            //{
            //    condicion = "NOMBRE LIKE '%" + id + "%' AND A.FECHA_INICIO BETWEEN '" + fechai + "' AND '" + fechaf + "' AND A.ESTADO ='ACTIVO' GROUP BY " +
            //    "A.PRESTAMO_CODIGO,  B.NOMBRE, A.FECHA_INICIO, A.CANTIDAD_LPS, A.INTERES";
            //}
            //else
            //{
            //    condicion = " A.FECHA_INICIO BETWEEN '" + fechai + "' AND '" + fechaf + "' AND A.ESTADO ='PAGADO' GROUP BY " +
            //    "A.PRESTAMO_CODIGO,  B.NOMBRE, A.FECHA_INICIO, A.CANTIDAD_LPS, A.INTERES";
            //}

            DataTable reporte = db.Join(query, condicion, "A.PRESTAMO_CODIGO DESC");


            string _cod_prestamo, _socio, _fecha, _monto, _interes, _estado;
            int i;

            DgvData.Rows.Clear();

            for (i = 0; i < reporte.Rows.Count; i++)
            {
                _cod_prestamo = reporte.Rows[i][0].ToString();
                _socio = reporte.Rows[i][1].ToString();
                _fecha = Convert.ToDateTime(reporte.Rows[i][2].ToString()).ToShortDateString();
                _monto = reporte.Rows[i][3].ToString();
                _interes = reporte.Rows[i][4].ToString();
                _estado = reporte.Rows[i][5].ToString();

                DgvData.Rows.Add(_cod_prestamo, _socio, _fecha, _monto, _interes, _estado);

                if (_estado == "PAGADO")
                {

                    DgvData.Rows[i].DefaultCellStyle.ForeColor = Color.FromArgb(21, 93, 39);
                    DgvData.Rows[i].DefaultCellStyle.SelectionForeColor = Color.FromArgb(253, 253, 253);
                    DgvData.Rows[i].DefaultCellStyle.SelectionBackColor = Color.FromArgb(45, 198, 83);
                }
            }

            reporte.Dispose();
        }

        private void DgvData_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {

        }


        private void BtnBuscar_Click(object sender, EventArgs e)
        {
            //if (e.KeyCode == Keys.Enter)
            //{
            //    GetPrestamoInfo();
            //}
        }



        private void dtpFechaFinal_CloseUp(object sender, EventArgs e)
        {
            //GetPrestamo_Estado();
        }

        private void txtBuscar_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                GetPrestamo_Nombre(a.Clean(txtBuscar.Text.Trim()));
                rbtnActivo.Checked = false;
                rbtnTodos.Checked = false;
            }
        }

        private void dtpFechaFinal_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                GetPrestamo_Nombre(a.Clean(txtBuscar.Text.Trim()));
            }
        }

        private void BtnBuscar_Click_1(object sender, EventArgs e)
        {
            GetPrestamo_Nombre(a.Clean(txtBuscar.Text.Trim()));
        }

        private void dtpFechaIncial_CloseUp(object sender, EventArgs e)
        {
            //GetPrestamo_Estado();
        }
    }
}
