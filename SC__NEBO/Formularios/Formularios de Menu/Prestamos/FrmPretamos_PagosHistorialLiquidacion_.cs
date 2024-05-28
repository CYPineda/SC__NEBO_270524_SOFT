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
    public partial class FrmPretamos_PagosHistorialLiquidacion_ : Form
    {
        Clases.DB db = new Clases.DB();
        Clases.Asistente a = new Clases.Asistente();

        public FrmPretamos_PagosHistorialLiquidacion_()
        {
            InitializeComponent();
        }

        private void FrmPretamos_PagosHistorialLiquidacion__Load(object sender, EventArgs e)
        {
            this.Text = Clases.Env.APPNAME + " | HISTORIAL DE PAGOS POR LIQUIDACIÓN | " + Clases.Auth.user + " | " + Clases.Auth.rol;

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

        private void Lista_Cosechas()
        {
            cmbCosecha.DataSource = db.Find("COSECHAS", "ID_COSECHA, COSECHA", "", "COSECHA");
            cmbCosecha.ValueMember = "ID_COSECHA";
            cmbCosecha.DisplayMember = "COSECHA";
            cmbCosecha.SelectedIndex = -1;
        }

        private void BtnBuscar_Click(object sender, EventArgs e)
        {
            GetLiquidacionInfo(a.Clean(txtBuscar.Text.Trim()));

            SumaQQ_Netos();

        }

        string fechai, fechaf, nombre_, cosecha_;

        private void btnLista_Click(object sender, EventArgs e)
        {

        }

        private void btnRecibo_Click(object sender, EventArgs e)
        {
            if (DgvData.Rows.Count > 0)
            {
                string codrecibo = DgvData.CurrentRow.Cells[1].Value.ToString();
                Reportes.FrmRptComprobanteIngresoLiquidacionCopia CompIngresoCopia = new Reportes.FrmRptComprobanteIngresoLiquidacionCopia();
                CompIngresoCopia.codrecibo = codrecibo;
                CompIngresoCopia.Show();
            }
        }

        private void BtnReimprimirNotaOriginal_Click(object sender, EventArgs e)
        {
            if (DgvData.Rows.Count > 0)
            {
                string codrecibo = DgvData.CurrentRow.Cells[1].Value.ToString();
                Reportes.FrmRptComprobanteIngresoReimprimir CompIngesoRem = new Reportes.FrmRptComprobanteIngresoReimprimir();
                CompIngesoRem.codrecibo = codrecibo;
                CompIngesoRem.Show();
            }
        }

        private void GetLiquidacionInfo(string id = "")
        {
            fechai = dtpFechaIncial.Value.ToString("dd-MM-yyyy");
            fechaf = dtpFechaFinal.Value.ToString("dd-MM-yyyy");
            cosecha_ = cmbCosecha.Text;


            string campos = "G.ID, A.NUM_LIQUIDACION, B.NOMBRE, A.FECHA,A.ABONO_CAPITAL,A.INTERES,A.PRESTAMO " +
                "FROM CAB_LIQUIDACION A INNER JOIN SOCIOS B ON(A.ID_SOCIO = B.ID_SOCIO) INNER JOIN " +
                "COSECHAS F ON(A.ID_COSECHA = F.ID_COSECHA) INNER JOIN COM_INGRESO_LIQUI G ON " +
                "(A.NUM_LIQUIDACION = G.NUM_LIQUIDACION)";

            string condicion = "B.NOMBRE LIKE '%" + id + "%' AND A.FECHA BETWEEN '" + fechai + "' AND '" + fechaf + "' AND F.COSECHA = '" + cosecha_ + "' AND A.PRESTAMO > 0 GROUP BY " +
                    "G.ID, A.NUM_LIQUIDACION, B.NOMBRE, A.FECHA,A.ABONO_CAPITAL,A.INTERES,A.PRESTAMO ";
            nombre_ = id;
            DataTable data = db.Join(campos, condicion, "A.NUM_LIQUIDACION DESC");

            DgvData.Rows.Clear();

            string _numbComprobante, _numliqui, _nombre, _fecha, _montocap, _interes, _total;

            int i;
            for (i = 0; i < data.Rows.Count; i++)
            {
                _numbComprobante = data.Rows[i][0].ToString();
                _numliqui = data.Rows[i][1].ToString();
                _nombre = data.Rows[i][2].ToString();
                _fecha = Convert.ToDateTime(data.Rows[i][3].ToString()).ToShortDateString();
                _montocap = data.Rows[i][4].ToString();
                _interes = data.Rows[i][5].ToString();
                _total = data.Rows[i][6].ToString();

                DgvData.Rows.Add(_numbComprobante, _numliqui, _nombre, _fecha, a.ReturnsNumber(_montocap).ToString("N2"), a.ReturnsNumber(_interes).ToString("N2"), a.ReturnsNumber(_total).ToString("N2"));
            }

            lblRecuento.Text = "Mostrando " + data.Rows.Count.ToString() + " registros de " + db.Count("CAB_LIQUIDACION", "PRESTAMO > 0").ToString();

            data.Dispose();
        }

        private void SumaQQ_Netos()
        {
            double total_mont = 0, total_interes = 0, tot_prest = 0;

            for (int i = 0; i < DgvData.Rows.Count; i++)
            {
                total_mont += Convert.ToDouble(DgvData.Rows[i].Cells[4].Value.ToString());
                total_interes += Convert.ToDouble(DgvData.Rows[i].Cells[5].Value.ToString());
                tot_prest += Convert.ToDouble(DgvData.Rows[i].Cells[6].Value.ToString());
            }

            LblMontoCapital.Text = total_mont.ToString("N2");
            LblInteres.Text = total_interes.ToString("N2");
            LblTotPagar.Text = tot_prest.ToString("N2");
        }
    }
}
