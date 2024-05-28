using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SC__NEBO.Formularios.Formularios_de_Menu.Notas_de_Peso
{
    public partial class Frm_Lista_Nota_Peso : Form
    {

        Clases.DB db = new Clases.DB();
        Clases.Asistente a = new Clases.Asistente();

        public Frm_Lista_Nota_Peso()
        {
            InitializeComponent();
        }

        private void pbSalir_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void txtBuscar_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                GetNotaPesoInfo(a.Clean(txtBuscar.Text.Trim()));
            }
        }

        private void Frm_Lista_Nota_Peso_Load(object sender, EventArgs e)
        {
            this.Text = Clases.Env.APPNAME + " | CONTROL DE NOTAS DE PESO | " + Clases.Auth.user + " | " + Clases.Auth.rol;

            Lista_Cosechas();
            Cosecha();
            cosecha = cmbCosecha.Text;
        }

        //Traer la cosecha y llenar el cmb
        string id_cosecha, cosecha;
        public void Cosecha()
        {
            string condicion = "ESTADO='ACTIVO'";
            cmbCosecha.Text = db.Hook("COSECHA", "COSECHAS", condicion);
            id_cosecha = db.Hook("ID_COSECHA", "COSECHAS", condicion);
        }

        private void GetNotaPeso_Estado()
        {

            string query = " A.ID_NOTA, B.NOMBRE, C.NOMBREFINCA, C.UBICACION +' '+ C.MUNICIPIO AS UBICACION, A.FECHA_NOTA_PESO, " +
                "E.ESTADO, A.PESO_BRUTO, A.DESCUENTO_HUMEDO, A.QQ_NETO FROM NOTA_DE_PESO A " +
                "INNER JOIN SOCIOS B ON(A.ID_SOCIO = B.ID_SOCIO) INNER JOIN FINCAS C ON(A.ID_FINCA = C.IDFINCA) " +
                "INNER JOIN ESTADO_CAFE E ON(A.ID_ESTADO_CAFE = E.ID) ";

            string condicion = "";

            if (rbtnDeposito.Checked == true)
            {
                condicion = " A.ESTADO = 'DEPÓSITO' GROUP BY " +
                    "A.ID_NOTA, B.NOMBRE, C.NOMBREFINCA, C.UBICACION +' '+ C.MUNICIPIO, A.FECHA_NOTA_PESO, E.ESTADO, A.PESO_BRUTO, " +
                    "A.DESCUENTO_HUMEDO, A.QQ_NETO";
                //"ORDER BY B.NOMBRE DESC";

            }
            else if (rbtnLiquidado.Checked == true)
            {
                condicion = " A.ESTADO = 'LIQUIDADO' GROUP BY " +
                    "A.ID_NOTA, B.NOMBRE, C.NOMBREFINCA, C.UBICACION +' '+ C.MUNICIPIO, A.FECHA_NOTA_PESO, E.ESTADO, A.PESO_BRUTO, " +
                    "A.DESCUENTO_HUMEDO, A.QQ_NETO";

            }
            else
            {
                condicion = "";
            }


            DataTable data = db.Join(query, condicion, "A.ID_NOTA DESC");

            DgvData.Rows.Clear();

            string _idnota, _nombre, _finca, _ubicfinca, _fecha, _estado, _pesobruto, _desc_humedo, _qqnetos;

            int i;
            for (i = 0; i < data.Rows.Count; i++)
            {
                _idnota = data.Rows[i][0].ToString();
                _nombre = data.Rows[i][1].ToString();
                _finca = data.Rows[i][2].ToString();
                _ubicfinca = data.Rows[i][3].ToString();
                _fecha = Convert.ToDateTime(data.Rows[i][4].ToString()).ToShortDateString();
                _estado = data.Rows[i][5].ToString();
                _pesobruto = data.Rows[i][6].ToString();
                _desc_humedo = data.Rows[i][7].ToString();
                _qqnetos = data.Rows[i][8].ToString();

                DgvData.Rows.Add(_idnota, _nombre, _finca, _ubicfinca, _fecha, _estado, _pesobruto, _desc_humedo, _qqnetos);
            }

            data.Dispose();

        }

        private void SumaQQ_Netos()
        {
            double total_qqnetos = 0, total_qqhumedos=0;

            for (int i = 0; i < DgvData.Rows.Count; i++)
            {
                total_qqhumedos += Convert.ToDouble(DgvData.Rows[i].Cells[6].Value.ToString());
                total_qqnetos += Convert.ToDouble(DgvData.Rows[i].Cells[8].Value.ToString());
            }

            lblQQHumedos.Text = total_qqhumedos.ToString("N2");
            lblQQNetos.Text = total_qqnetos.ToString("N2");
        }

        string fechai, fechaf, nombre_, cosecha_;
        private void GetNotaPesoInfo(string id="")
        {
            fechai = dtpFechaIncial.Value.ToString("dd-MM-yyyy");
            fechaf = dtpFechaFinal.Value.ToString("dd-MM-yyyy");
            cosecha_ = cmbCosecha.Text;


            string campos = "A.ID_NOTA, B.NOMBRE, C.NOMBREFINCA, C.UBICACION +' '+ C.MUNICIPIO AS UBICACION, A.FECHA_NOTA_PESO, E.ESTADO, A.PESO_BRUTO, " +
                "A.DESCUENTO_HUMEDO, A.QQ_NETO FROM NOTA_DE_PESO A " +
                "INNER JOIN SOCIOS B ON(A.ID_SOCIO = B.ID_SOCIO) INNER JOIN FINCAS C ON(A.ID_FINCA = C.IDFINCA) INNER JOIN " +
                "ESTADO_CAFE E ON(A.ID_ESTADO_CAFE = E.ID) INNER JOIN COSECHAS F ON(A.ID_COSECHA = F.ID_COSECHA)";

            string condicion;

            //if (id != "")
            //{
            //    condicion = "B.NOMBRE LIKE '%" + id + "%' AND A.FECHA_NOTA_PESO BETWEEN '" + fechai + "' AND '" + fechaf + "' GROUP BY " +
            //        "A.ID_NOTA, B.NOMBRE, C.NOMBREFINCA, C.UBICACION +' '+ C.MUNICIPIO, A.FECHA_NOTA_PESO, E.ESTADO, A.PESO_BRUTO, " +
            //    "A.DESCUENTO_HUMEDO, A.QQ_NETO";
            //    condicion = "A.DESCRIPCION LIKE '%" + nombre + "%'";
            //}
            //else
            //{
            //    condicion = "A.FECHA_NOTA_PESO BETWEEN '" + fechai + "' AND '" + fechaf + "' GROUP BY " +
            //        "A.ID_NOTA, B.NOMBRE, C.NOMBREFINCA, C.UBICACION +' '+ C.MUNICIPIO, A.FECHA_NOTA_PESO, E.ESTADO, A.PESO_BRUTO, " +
            //    "A.DESCUENTO_HUMEDO, A.QQ_NETO";

            //}

            if (rbtnDeposito.Checked == true)
            {
                condicion = "B.NOMBRE LIKE '%" + id + "%' AND A.FECHA_NOTA_PESO BETWEEN '" + fechai + "' AND '" + fechaf + "' AND A.ESTADO = 'PENDIENTE' AND F.COSECHA = '"+ cosecha_ + "' GROUP BY " +
                    "A.ID_NOTA, B.NOMBRE, C.NOMBREFINCA, C.UBICACION +' '+ C.MUNICIPIO, A.FECHA_NOTA_PESO, E.ESTADO, A.PESO_BRUTO, " +
                "A.DESCUENTO_HUMEDO, A.QQ_NETO ";
                nombre_ = id;
            }
            else if (rbtnLiquidado.Checked == true)
            {
                condicion = "B.NOMBRE LIKE '%" + id + "%' AND A.FECHA_NOTA_PESO BETWEEN '" + fechai + "' AND '" + fechaf + "' AND A.ESTADO = 'LIQUIDADO' AND F.COSECHA = '" + cosecha_ + "' GROUP BY " +
                    "A.ID_NOTA, B.NOMBRE, C.NOMBREFINCA, C.UBICACION +' '+ C.MUNICIPIO, A.FECHA_NOTA_PESO, E.ESTADO, A.PESO_BRUTO, " +
                "A.DESCUENTO_HUMEDO, A.QQ_NETO ";
                nombre_ = id;
            }
            else
            {
                condicion = "B.NOMBRE LIKE '%" + id + "%' AND A.FECHA_NOTA_PESO BETWEEN '" + fechai + "' AND '" + fechaf + "' AND F.COSECHA = '" + cosecha_ + "' AND A.ESTADO != 'NULA' GROUP BY " +
                    "A.ID_NOTA, B.NOMBRE, C.NOMBREFINCA, C.UBICACION +' '+ C.MUNICIPIO, A.FECHA_NOTA_PESO, E.ESTADO, A.PESO_BRUTO, " +
                "A.DESCUENTO_HUMEDO, A.QQ_NETO";
            }

            DataTable data = db.Join(campos, condicion, "A.ID_NOTA DESC");

            DgvData.Rows.Clear();

            string _idnota, _nombre, _finca, _ubicfinca, _fecha, _estado, _pesobruto, _desc_humedo, _qqnetos;

            int i;
            for (i = 0; i < data.Rows.Count; i++)
            {
                _idnota = data.Rows[i][0].ToString();
                _nombre = data.Rows[i][1].ToString();
                _finca = data.Rows[i][2].ToString();
                _ubicfinca = data.Rows[i][3].ToString();
                _fecha = data.Rows[i][4].ToString();
                _estado = data.Rows[i][5].ToString();
                _pesobruto = data.Rows[i][6].ToString();
                _desc_humedo = data.Rows[i][7].ToString();
                _qqnetos = data.Rows[i][8].ToString();

                DgvData.Rows.Add(_idnota, _nombre, _finca, _ubicfinca, _fecha, _estado, _pesobruto, _desc_humedo, _qqnetos);
            }


            if (rbtnDeposito.Checked)
            {
                lblRecuento.Text = "Mostrando " + data.Rows.Count.ToString() + " registros de " + db.Count("NOTA_DE_PESO", "ESTADO = 'PENDIENTE'").ToString();
            }
            else if (rbtnLiquidado.Checked)
            {
                lblRecuento.Text = "Mostrando " + data.Rows.Count.ToString() + " registros de " + db.Count("NOTA_DE_PESO", "ESTADO = 'LIQUIDADO'").ToString();
            }
            else
            {
                lblRecuento.Text = "Mostrando " + data.Rows.Count.ToString() + " registros de " + db.Count("NOTA_DE_PESO", "").ToString();
            }
            data.Dispose();
        }

        private void Lista_Cosechas()
        {
            cmbCosecha.DataSource = db.Find("COSECHAS", "ID_COSECHA, COSECHA", "", "COSECHA");
            cmbCosecha.ValueMember = "ID_COSECHA";
            cmbCosecha.DisplayMember = "COSECHA";
            cmbCosecha.SelectedIndex = -1;

        }

        //private void GetCosecha(string id)
        //{
        //    int cosechas = 0;

        //    if (cmbCosecha.SelectedValue != null && int.TryParse(cmbCosecha.SelectedValue.ToString(), out cosechas))
        //    {

        //        string campos = "A.ID_NOTA, B.NOMBRE, C.NOMBREFINCA, C.UBICACION +' '+ C.MUNICIPIO AS UBICACION, A.FECHA_NOTA_PESO, E.ESTADO, A.PESO_BRUTO, " +
        //        "A.DESCUENTO_HUMEDO, A.QQ_NETO FROM NOTA_DE_PESO A " +
        //        "INNER JOIN SOCIOS B ON(A.ID_SOCIO = B.ID_SOCIO) INNER JOIN FINCAS C ON(A.ID_FINCA = C.IDFINCA) INNER JOIN " +
        //        "ESTADO_CAFE E ON(A.ID_ESTADO_CAFE = E.ID) " +
        //        "INNER JOIN COSECHAS F ON(A.ID_COSECHA = F.ID_COSECHA) ";

        //    string condicion;

        //    if (id != "")
        //    {
        //        condicion = "A.ID_COSECHA = " + cosechas + " GROUP BY " +
        //    "A.ID_NOTA, B.NOMBRE, C.NOMBREFINCA, C.UBICACION +' '+ C.MUNICIPIO, A.FECHA_NOTA_PESO, E.ESTADO, A.PESO_BRUTO, " +
        //    "A.DESCUENTO_HUMEDO, A.QQ_NETO, A.ID_COSECHA";

        //    txtBuscar.Text = "";
        //            //condicion = "A.ID_COSECHA = '" + cosechas + "' GROUP BY " +
        //            //    "A.ID_NOTA, B.NOMBRE, C.NOMBREFINCA, A.FECHA_NOTA_PESO, D.TIPO_RECEPCION, E.ESTADO, A.PESO_BRUTO, " +
        //            //"A.DESCUENTO_HUMEDO, A.QQ_NETO, A.ID_COSECHA";
        //            //condicion = "A.DESCRIPCION LIKE '%" + nombre + "%'";
        //        }
        //    else
        //    {
        //        condicion = "A.ID_COSECHA WHEN '" + cosechas + "'GROUP BY " +
        //            "A.ID_NOTA, B.NOMBRE, C.NOMBREFINCA, C.UBICACION +' '+ C.MUNICIPIO, A.FECHA_NOTA_PESO, E.ESTADO, A.PESO_BRUTO, " +
        //        "A.DESCUENTO_HUMEDO, A.QQ_NETO";

        //            txtBuscar.Text = "";
        //        }
            
        //    DataTable data = db.Join(campos, condicion, "A.ID_NOTA DESC");
            
        //    DgvData.Rows.Clear();

        //    string _idnota, _nombre, _finca, _ubicfinca, _fecha, _estado, _pesobruto, _desc_humedo, _qqnetos;

        //    int i;
        //    for (i = 0; i < data.Rows.Count; i++)
        //    {
        //        _idnota = data.Rows[i][0].ToString();
        //        _nombre = data.Rows[i][1].ToString();
        //        _finca = data.Rows[i][2].ToString();
        //        _ubicfinca = data.Rows[i][3].ToString();
        //        _fecha = data.Rows[i][4].ToString();
        //        _estado = data.Rows[i][5].ToString();
        //        _pesobruto = data.Rows[i][6].ToString();
        //        _desc_humedo = data.Rows[i][7].ToString();
        //        _qqnetos = data.Rows[i][8].ToString();

        //        DgvData.Rows.Add(_idnota, _nombre, _finca, _ubicfinca, _fecha, _estado, _pesobruto, _desc_humedo, _qqnetos);
        //    }

        //    data.Dispose();
        //    }
        //}

        //private void GetNotaPesoInfo(string id)
        //{
        //    string fechai, fechaf;
        //    fechai = dtpFechaIncial.Text;
        //    fechaf = dtpFechaFinal.Text;


        //    string condicion;
        //    string campos;
        //    if (id != "")
        //    {
        //        campos = "A.ID_NOTA, B.NOMBRE, C.NOMBREFINCA, A.FECHA_NOTA_PESO, D.TIPO_RECEPCION, E.ESTADO, A.PESO_BRUTO, " +
        //        "A.DESCUENTO_HUMEDO, A.QQ_NETO FROM NOTA_DE_PESO A " +
        //        "INNER JOIN SOCIOS B ON(A.ID_SOCIO = B.ID_SOCIO) INNER JOIN FINCAS C ON(A.ID_FINCA = C.IDFINCA) INNER JOIN " +
        //        "TIPO_RECEPCION D ON(A.ID_RECEPCION_CAFE = D.ID) INNER JOIN ESTADO_CAFE E ON(A.ID_ESTADO_CAFE = E.ID)" +
        //        "WHERE B.NOMBRE LIKE '%" + id + "%' AND A.FECHA_NOTA_PESO BETWEEN '" + fechai + "' AND '" + fechaf + "' ORDER BY " +
        //            "B.NOMBRE, C.NOMBREFINCA, A.FECHA_NOTA_PESO, D.TIPO_RECEPCION, E.ESTADO, A.PESO_BRUTO, " +
        //        "A.DESCUENTO_HUMEDO, A.QQ_NETO";

        //    }

        //    else
        //    {
        //        condicion = "";

        //    }

        //    DataTable data = db.RawSQL(campos);

        //    DgvData.Rows.Clear();

        //    string _idnota, _nombre, _finca, _fecha, _recepcion, _estado, _pesobruto, _desc_humedo, _qqnetos;

        //    int i;
        //    for (i = 0; i < data.Rows.Count; i++)
        //    {
        //        _idnota = data.Rows[i][0].ToString();
        //        _nombre = data.Rows[i][1].ToString();
        //        _finca = data.Rows[i][2].ToString();
        //        _fecha = data.Rows[i][3].ToString();
        //        _recepcion = data.Rows[i][4].ToString();
        //        _estado = data.Rows[i][5].ToString();
        //        _pesobruto = data.Rows[i][6].ToString();
        //        _desc_humedo = data.Rows[i][7].ToString();
        //        _qqnetos = data.Rows[i][8].ToString();

        //        DgvData.Rows.Add(_idnota, _nombre, _finca, _fecha, _recepcion, _estado, _pesobruto, _desc_humedo, _qqnetos);
        //    }

        //    data.Dispose();
        //}


        private void dtpFechaFinal_KeyDown(object sender, KeyEventArgs e)
        {
            //if (e.KeyCode == Keys.Enter)
            //{
            //    GetNotaPesoInfo(a.Clean(txtBuscar.Text.Trim()));
            //}
        }

        private void BtnBuscar_Click(object sender, EventArgs e)
        {
            GetNotaPesoInfo(a.Clean(txtBuscar.Text.Trim()));
           
            SumaQQ_Netos();
        }

        private void cmbCosecha_SelectedIndexChanged(object sender, EventArgs e)
        {
            //rbtnDeposito.Checked = false;
            //rbtnLiquidado.Checked = false;
            //GetCosecha(a.Clean(cmbCosecha.Text.Trim()));
            //Lista_Cosechas();
        }

        private void rbtnDeposito_CheckedChanged(object sender, EventArgs e)
        {
            //if (rbtnDeposito.Checked)
            //{
            //    GetNotaPeso_Estado();
            //}
        }


        private void DgvData_DoubleClick(object sender, EventArgs e)
        {
            if (DgvData.Rows.Count > 0)
            {
                string codnotapeso = DgvData.CurrentRow.Cells[0].Value.ToString();
                Reportes.FrmRptNotaPeso_Copia NotaCopia = new Reportes.FrmRptNotaPeso_Copia();
                NotaCopia.codnota = codnotapeso;
                NotaCopia.Show();
            }
        }

        private void BtnReimprimirNotaOriginal_Click(object sender, EventArgs e)
        {
            if (DgvData.Rows.Count > 0)
            {
                string codnotapeso = DgvData.CurrentRow.Cells[0].Value.ToString();
                Reportes.FrmNotaPesoRemOriginal NotaRempOriginal = new Reportes.FrmNotaPesoRemOriginal();
                NotaRempOriginal.codnota = codnotapeso;
                NotaRempOriginal.Show();
            }
        }

        private void rbtnLiquidado_CheckedChanged(object sender, EventArgs e)
        {
            //if (rbtnLiquidado.Checked)
            //{
            //    GetNotaPeso_Estado();
            //}
        }

        private void btnLista_Click(object sender, EventArgs e)
        {
            if (rbtnDeposito.Checked == true)
            {
                Reportes.FrmRptLista_Notas_Peso listaP = new Reportes.FrmRptLista_Notas_Peso();
                listaP.fechaini = fechai;
                listaP.fechafin = fechaf;
                listaP.estadonp = "PENDIENTE";
                listaP.cosecha = cosecha_;
                listaP.nombre = nombre_;

                listaP.Show();
            }
            else if (rbtnLiquidado.Checked == true)
            {
                Reportes.FrmRptLista_Notas_Peso listaP = new Reportes.FrmRptLista_Notas_Peso();
                listaP.fechaini = fechai;
                listaP.fechafin = fechaf;
                listaP.estadonp = "LIQUIDADO";
                listaP.cosecha = cosecha_;
                listaP.nombre = nombre_;


                listaP.Show();
            }
            else
            {
                Reportes.FrmRptLista_Notas_Peso listaP = new Reportes.FrmRptLista_Notas_Peso();
                listaP.fechaini = fechai;
                listaP.fechafin = fechaf;
                listaP.cosecha = cosecha;
                listaP.estadonp = null;
                listaP.nombre = null;


                listaP.Show();
            }
        }

        private void btnRecibo_Click(object sender, EventArgs e)
        {
            if (DgvData.Rows.Count > 0)
            {
                string codnotapeso = DgvData.CurrentRow.Cells[0].Value.ToString();
                Reportes.FrmRptNotaPeso_Copia NotaCopia = new Reportes.FrmRptNotaPeso_Copia();
                NotaCopia.codnota = codnotapeso;
                NotaCopia.Show();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

            if (rbtnDeposito.Checked == true)
            {
                //Reportes.FrmRptListadoNotasPeso listadoNotasPeso = new Reportes.FrmRptListadoNotasPeso();
                //listadoNotasPeso.fechaini = fechai;
                //listadoNotasPeso.fechafin = fechaf;
                //listadoNotasPeso.estadop = "ACTIVO";
                //listadoNotasPeso.nombre = nombre_;

                //listadoNotasPeso.Show();
            }
            else if (rbtnLiquidado.Checked == true)
            {
                //Reportes.FrmRptListaPrestamosxFecha listaP = new Reportes.FrmRptListaPrestamosxFecha();
                //listaP.fechaini = fechai;
                //listaP.fechafin = fechaf;
                //listaP.estadop = "PAGADO";
                //listaP.nombre = nombre_;

                //listaP.Show();
            }
        }
    }
}
