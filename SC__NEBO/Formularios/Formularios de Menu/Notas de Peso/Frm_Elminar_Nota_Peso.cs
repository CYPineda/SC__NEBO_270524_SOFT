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
    public partial class Frm_Elminar_Nota_Peso : Form
    {
        Clases.DB db = new Clases.DB();
        Clases.Asistente a = new Clases.Asistente();

        public Frm_Elminar_Nota_Peso()
        {
            InitializeComponent();
        }

        private void Frm_Elminar_Nota_Peso_Load(object sender, EventArgs e)
        {
            GetNotaPeso();
        }
        private void GetNotaPeso()
        {
            string query = "A.ID_NOTA, B.NOMBRE, C.UBICACION + ' ' + C.MUNICIPIO AS UBICACION, A.FECHA_NOTA_PESO, E.ESTADO, A.ESTADO AS ESTADO_NOTA, A.PESO_BRUTO," +
                " A.DESCUENTO_HUMEDO, A.QQ_NETO FROM NOTA_DE_PESO A" +
                " INNER JOIN SOCIOS B ON(A.ID_SOCIO = B.ID_SOCIO) INNER JOIN FINCAS C ON(A.ID_FINCA = C.IDFINCA) INNER JOIN" +
                " ESTADO_CAFE E ON(A.ID_ESTADO_CAFE = E.ID) INNER JOIN COSECHAS F ON(A.ID_COSECHA = F.ID_COSECHA) WHERE A.ESTADO = 'PENDIENTE'";

            string condicion = "";

            DataTable data = db.Join(query, condicion, "A.ID_NOTA DESC");

            DgvData.Rows.Clear();

            string _idnota, _nombre,  _ubicfinca, _fecha, _estado, _estadoNota, _pesobruto, _desc_humedo, _qqnetos;

            int i;
            for (i = 0; i < data.Rows.Count; i++)
            {
                _idnota = data.Rows[i][0].ToString();
                _nombre = data.Rows[i][1].ToString();
                _ubicfinca = data.Rows[i][2].ToString();
                _fecha = data.Rows[i][3].ToString();
                _estado = data.Rows[i][4].ToString();
                _estadoNota = data.Rows[i][5].ToString();
                _pesobruto = data.Rows[i][6].ToString();
                _desc_humedo = data.Rows[i][7].ToString();
                _qqnetos = data.Rows[i][8].ToString();

                DgvData.Rows.Add(_idnota, _nombre, _ubicfinca, _fecha, _estado, _estadoNota, _pesobruto, _desc_humedo, _qqnetos);
            }
        }


        private void btnEliminar_Nota_Click(object sender, EventArgs e)
        {
            if (DgvData.Rows.Count > 0)
            {
                string codnotapeso = DgvData.CurrentRow.Cells[0].Value.ToString();
                string nombre = DgvData.CurrentRow.Cells[1].Value.ToString();
                string msg = "¿DESA ELMINAR LA NOTA DE PESO N° " + codnotapeso + " DEL SOCIO " + nombre + " ?";

                if (a.Pregunta(msg) == true)
                {
                    if (db.Delete_Nota_Peso("NOTA_DE_PESO", "ID_NOTA", codnotapeso) > 0)
                    {
                        a.Aprueba("!LA NOTA DE PESO SE ELIMINÓ CORRECTAMENTE¡");
                        GetNotaPeso();
                    }
                }

            }
        }
    }
}
