using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SC__NEBO.Formularios.Formularios_de_Menu.Contratos
{
    public partial class FrmListaContratos_Liquidacion : Form
    {

        Clases.DB db = new Clases.DB();
        Clases.Asistente a = new Clases.Asistente();

        public string COD, NOM;

        public FrmListaContratos_Liquidacion()
        {
            InitializeComponent();
        }

        private void DgvData_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (DgvData.Rows.Count > 0)
            {
                string msg = "¿DESEA AGREGAR ESTE CONTRATO?";
                if (a.Pregunta(msg) == true)
                {
                    string codigo_contrato = DgvData.CurrentRow.Cells[0].Value.ToString();

                    string stmt = "ESTADO='PENDIENTE'";

                    string condicion = "CODIGO='" + codigo_contrato + "'";

                    if (db.Update("CONTRATOS_CAFE", stmt, condicion) > 0)
                    {
                        Formularios.Formularios_de_Menu.Liquidacion.Frm_Liquidacion form = new Liquidacion.Frm_Liquidacion();
                        form = ((Formularios.Formularios_de_Menu.Liquidacion.Frm_Liquidacion)Owner);
                        form.RecibirContrato(codigo_contrato);
                        Close();
                    }
                }
            }
        }

        private void FrmListaContratos_Liquidacion_Load(object sender, EventArgs e)
        {
            this.Text = Clases.Env.APPNAME + " | BÚSQUEDA DE CONTRATOS | " + Clases.Auth.user + " | " + Clases.Auth.rol;

            TxtIdSocio.Text = COD;
            TxtSocio.Text = NOM;

            BuscarContratos();
        }

        private void BuscarContratos()
        {
            string ID;

            ID = TxtIdSocio.Text;

            string sql = "SELECT A.CODIGO, B.NOMBRE, A.FECHA_INICIO, A.FECHA_LIMITE, A.PRECIO, A.CANT_QQ_DISP, A.VALOR_TOTAL " +
                " FROM CONTRATOS_CAFE A " +
                "INNER JOIN SOCIOS B ON(A.CODIGO_SOCIOS = B.ID_SOCIO) " +
                "INNER JOIN COSECHAS C ON(A.ID_COSECHAS=C.ID_COSECHA) " +
                "WHERE A.CODIGO_SOCIOS = '" + ID + "' AND A.ESTADO ='PENDIENTE' AND C.ESTADO='ACTIVO' AND A.CANT_QQ_DISP>0 GROUP BY A.CODIGO, B.NOMBRE, A.FECHA_INICIO, A.FECHA_LIMITE, A.PRECIO, A.CANT_QQ_DISP, A.VALOR_TOTAL " +
                "ORDER BY A.CODIGO ASC";

            DataTable data = db.RawSQL(sql);

            DgvData.Rows.Clear();

            string _codigo, _nombre, _fecha_inicio, _fecha_limite, _precio, _cantqq, _valortotal;

            int i;
            for (i = 0; i < data.Rows.Count; i++)
            {
                _codigo = data.Rows[i][0].ToString();
                _nombre = data.Rows[i][1].ToString();
                _fecha_inicio = Convert.ToDateTime(data.Rows[i][2].ToString()).ToShortDateString();
                _fecha_limite = Convert.ToDateTime(data.Rows[i][3].ToString()).ToShortDateString();
                _precio = data.Rows[i][4].ToString();
                _cantqq = data.Rows[i][5].ToString();
                _valortotal = data.Rows[i][6].ToString();

                DgvData.Rows.Add(_codigo, _nombre, _fecha_inicio, _fecha_limite, _precio, _cantqq, _valortotal);
            }
            data.Dispose();
        }
    }
}
