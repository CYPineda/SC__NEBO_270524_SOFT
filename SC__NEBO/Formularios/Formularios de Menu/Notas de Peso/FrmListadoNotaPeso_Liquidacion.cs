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
    public partial class FrmListadoNotaPeso_Liquidacion : Form
    {

        Clases.DB db = new Clases.DB();
        Clases.Asistente a = new Clases.Asistente();

        public string COD, NOM;

        public FrmListadoNotaPeso_Liquidacion()
        {
            InitializeComponent();
        }

        private void FrmListadoNotaPeso_Liquidacion_Load(object sender, EventArgs e)
        {
            this.Text = Clases.Env.APPNAME + " | BÚSQUEDA DE NOTAS DE PESO | " + Clases.Auth.user + " | " + Clases.Auth.rol;

            TxtIdSocio.Text = COD;
            TxtSocio.Text = NOM;

            BuscarNotasPeso();
        }

        private void txtBuscar_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                BuscarNotasPeso();
            }
        }

        private void BuscarNotasPeso()
        {
            string ID;

            ID = TxtIdSocio.Text;

            string sql = "SELECT A.ID_NOTA, B.NOMBRE, C.NOMBREFINCA, A.FECHA_NOTA_PESO, E.ESTADO, A.QQ_NETO, " +
                "A.DESCUENTO_HUMEDO, A.QQ_NETO FROM NOTA_DE_PESO A " +
                "INNER JOIN SOCIOS B ON(A.ID_SOCIO = B.ID_SOCIO) " +
                "INNER JOIN FINCAS C ON(A.ID_FINCA = C.IDFINCA) " +
                "INNER JOIN ESTADO_CAFE E ON(A.ID_ESTADO_CAFE = E.ID) " +
                "WHERE A.ID_SOCIO = '" + ID + "' AND A.ESTADO ='PENDIENTE' GROUP BY A.ID_NOTA, B.NOMBRE, C.NOMBREFINCA, A.FECHA_NOTA_PESO, " +
                "E.ESTADO, A.QQ_NETO, A.DESCUENTO_HUMEDO, A.QQ_NETO ORDER BY A.ID_NOTA ASC";

            DataTable data = db.RawSQL(sql);

            DgvData.Rows.Clear();

            string _idnota, _nombre, _finca, _fecha, _estado, _pesobruto, _desc_humedo, _qqnetos;

            int i;
            for (i = 0; i < data.Rows.Count; i++)
            {
                _idnota = data.Rows[i][0].ToString();
                _nombre = data.Rows[i][1].ToString();
                _finca = data.Rows[i][2].ToString();
                _fecha = Convert.ToDateTime(data.Rows[i][3].ToString()).ToShortDateString();
                _estado = data.Rows[i][4].ToString();
                _pesobruto = data.Rows[i][5].ToString();
                _desc_humedo = data.Rows[i][6].ToString();
                _qqnetos = data.Rows[i][7].ToString();

                DgvData.Rows.Add(_idnota, _nombre, _finca, _fecha, _estado, _pesobruto, _desc_humedo, _qqnetos);
            }
            data.Dispose();
        }

        private void DgvData_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (DgvData.Rows.Count > 0)
            {
                string msg = "¿DESEA AGREGAR ESTA NOTA DE PESO?";
                if (a.Pregunta(msg) == true)
                {
                    string idnotapeso = DgvData.CurrentRow.Cells[0].Value.ToString();

                    string stmt = "ESTADO='EN PROCESO'";

                    string condicion = "ID_NOTA='" + idnotapeso + "'";

                    if (db.Update("NOTA_DE_PESO", stmt, condicion) > 0)
                    {
                        Formularios.Formularios_de_Menu.Liquidacion.Frm_Liquidacion form = new Liquidacion.Frm_Liquidacion();
                        form = ((Formularios.Formularios_de_Menu.Liquidacion.Frm_Liquidacion)Owner);
                        form.RecibirNotaPeso(idnotapeso);
                        Close();
                    }
                }
            }
        }

        private void pbSalir_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void BtnBuscar_Click(object sender, EventArgs e)
        {
            BuscarNotasPeso();
        }
    }
}
