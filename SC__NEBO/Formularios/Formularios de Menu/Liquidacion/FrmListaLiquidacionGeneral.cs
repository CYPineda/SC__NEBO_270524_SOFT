using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SC__NEBO.Formularios.Formularios_de_Menu.Liquidacion
{
    public partial class FrmListaLiquidacionGeneral : Form
    {
        Clases.DB db = new Clases.DB();
        Clases.Asistente a = new Clases.Asistente();


        public FrmListaLiquidacionGeneral()
        {
            InitializeComponent();
        }

        private void FrmListaLiquidacionGeneral_Load(object sender, EventArgs e)
        {
            this.Text = Clases.Env.APPNAME + " | LISTA DE LIQUIDACIÓN | " + Clases.Auth.user + " | " + Clases.Auth.rol;

            Lista_Cosechas();
            Cosecha();
            cosecha = cmbCosecha.Text;
        }


        string id_cosecha, cosecha;
        public void Cosecha()
        {
            string condicion = "ESTADO='ACTIVO'";
            cmbCosecha.Text = db.Hook("COSECHA", "COSECHAS", condicion);
            id_cosecha = db.Hook("ID_COSECHA", "COSECHAS", condicion);
        }

        private void BtnBuscar_Click(object sender, EventArgs e)
        {
            GetLiquidacionInfo(a.Clean(txtBuscar.Text.Trim()));

            SumaQQ_Netos();
        }

        private void Lista_Cosechas()
        {
            cmbCosecha.DataSource = db.Find("COSECHAS", "ID_COSECHA, COSECHA", "", "COSECHA");
            cmbCosecha.ValueMember = "ID_COSECHA";
            cmbCosecha.DisplayMember = "COSECHA";
            cmbCosecha.SelectedIndex = -1;

        }

        string fechai, fechaf, nombre_, cosecha_;

        private void DgvData_DoubleClick(object sender, EventArgs e)
        {
            if (DgvData.Rows.Count > 0)
            {
                string cod = DgvData.CurrentRow.Cells[0].Value.ToString();
                Reportes.FrmRptLiquidacionX_Copia Copia = new Reportes.FrmRptLiquidacionX_Copia();
                Copia.numliqui = cod;
                Copia.Show();
            }
        }

        private void DgvData_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void DgvData_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void BtnPagosLiqui_Click(object sender, EventArgs e)
        {
            Formularios.Formularios_de_Menu.Prestamos.FrmPretamos_PagosHistorialLiquidacion_ liquidacion = new Prestamos.FrmPretamos_PagosHistorialLiquidacion_();
            this.AddOwnedForm(liquidacion);
            liquidacion.Show();

        }

        private void BtnReimprimirNotaOriginal_Click(object sender, EventArgs e)
        {

            if (DgvData.Rows.Count > 0)
            {
                string cod = DgvData.CurrentRow.Cells[0].Value.ToString();
                Reportes.FrmRptLiquidacionX_Reimprimir reimprimir = new Reportes.FrmRptLiquidacionX_Reimprimir();
                reimprimir.numliqui = cod;
                reimprimir.Show();
            }
        }

        private void btnLista_Click(object sender, EventArgs e)
        {
            if (txtBuscar.Text != "")
            {
                Reportes.FrmRptListado_Liquidaciones listaP = new Reportes.FrmRptListado_Liquidaciones();
                listaP.fechaini = fechai;
                listaP.fechafin = fechaf;
                listaP.cosecha = cosecha;
                listaP.nombre = nombre_;

                listaP.Show();
            }
            else
            {
                Reportes.FrmRptListado_Liquidaciones listaP = new Reportes.FrmRptListado_Liquidaciones();
                listaP.fechaini = fechai;
                listaP.fechafin = fechaf;
                listaP.cosecha = cosecha;
                listaP.nombre = null;

                listaP.Show();
            }

        }

        private void btnRecibo_Click(object sender, EventArgs e)
        {
            if (DgvData.Rows.Count > 0)
            {
                string cod = DgvData.CurrentRow.Cells[0].Value.ToString();
                Reportes.FrmRptLiquidacionX_Copia Copia = new Reportes.FrmRptLiquidacionX_Copia();
                Copia.numliqui = cod;
                Copia.Show();
            }
        }

        private void GetLiquidacionInfo(string id = "")
        {
            fechai = dtpFechaIncial.Value.ToString("dd-MM-yyyy");
            fechaf = dtpFechaFinal.Value.ToString("dd-MM-yyyy");
            cosecha_ = cmbCosecha.Text;


            string campos = "A.NUM_LIQUIDACION, B.NOMBRE, A.FECHA,A.CANT_QQ_CONT,A.PRECIO_QQ_CONT,A.CANT_QQ_PP,A.PRECIO_QQ_PP,  A.TOTAL_QQ, A.TOTAL_MONEDA,A.DEDUCCIONES," +
                "A.NETO_PAGAR FROM CAB_LIQUIDACION A " +
                "INNER JOIN SOCIOS B ON(A.ID_SOCIO = B.ID_SOCIO) INNER JOIN " +
                " COSECHAS F ON(A.ID_COSECHA = F.ID_COSECHA)";

            string condicion = "B.NOMBRE LIKE '%" + id + "%' AND A.FECHA BETWEEN '" + fechai + "' AND '" + fechaf + "' AND F.COSECHA = '" + cosecha_ + "' GROUP BY " +
                    "A.NUM_LIQUIDACION, B.NOMBRE, A.FECHA,A.CANT_QQ_CONT,A.PRECIO_QQ_CONT,A.CANT_QQ_PP,A.PRECIO_QQ_PP, A.TOTAL_QQ, A.TOTAL_MONEDA, A.DEDUCCIONES, A.NETO_PAGAR ";
            nombre_ = id;
            DataTable data = db.Join(campos, condicion, "A.NUM_LIQUIDACION DESC");

            DgvData.Rows.Clear();

            string _numliqui, _nombre, _fecha,_cantcont,_preciocont, _cantppplaza,_precioplaza, _deducciones, _cantqq,_subtotal,  _totpagar;

            int i;
            for (i = 0; i < data.Rows.Count; i++)
            {
                _numliqui = data.Rows[i][0].ToString();
                _nombre = data.Rows[i][1].ToString();
                _fecha = Convert.ToDateTime(data.Rows[i][2].ToString()).ToShortDateString();
                _cantcont = data.Rows[i][3].ToString();
                _preciocont = data.Rows[i][4].ToString();
                _cantppplaza = data.Rows[i][5].ToString();
                _precioplaza = data.Rows[i][6].ToString();
                _cantqq = data.Rows[i][7].ToString();
                _subtotal = data.Rows[i][8].ToString();
                _deducciones = data.Rows[i][9].ToString();
                _totpagar = data.Rows[i][10].ToString();

                DgvData.Rows.Add(_numliqui, _nombre, _fecha, a.ReturnsNumber(_cantcont).ToString("N2"), a.ReturnsNumber(_preciocont).ToString("N2"), a.ReturnsNumber(_cantppplaza).ToString("N2"), a.ReturnsNumber(_precioplaza).ToString("N2"),  _cantqq, a.ReturnsNumber(_subtotal).ToString("N2") , a.ReturnsNumber(_deducciones).ToString("N2"), a.ReturnsNumber(_totpagar).ToString("N2") );
            }

            lblRecuento.Text = "Mostrando " + data.Rows.Count.ToString() + " registros de " + db.Count("CAB_LIQUIDACION", "").ToString();

            data.Dispose();
        }

        private void SumaQQ_Netos()
        {
            double total_QQ = 0, total_PAGAR = 0 , ded=0, tot_sub=0, prom_ventas;

            for (int i = 0; i < DgvData.Rows.Count; i++)
            {
                total_QQ += Convert.ToDouble(DgvData.Rows[i].Cells[7].Value.ToString());
                tot_sub += Convert.ToDouble(DgvData.Rows[i].Cells[8].Value.ToString());
                ded += Convert.ToDouble(DgvData.Rows[i].Cells[9].Value.ToString());
                total_PAGAR += Convert.ToDouble(DgvData.Rows[i].Cells[10].Value.ToString());
            }

            prom_ventas = tot_sub / total_QQ;

            LblQQ.Text = total_QQ.ToString("N2");
            LblSubTotal.Text = tot_sub.ToString("N2");
            LblDeduc.Text = ded.ToString("N2");
            LblTotPagar.Text = total_PAGAR.ToString("N2");
            LblPromedioVenta.Text = prom_ventas.ToString("N2");

        }



    }
}
