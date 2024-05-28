using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SC__NEBO.Formularios.Formularios_de_Menu.IHCAFE
{
    public partial class FrmListaComprobantesIHCAFE_Socio_X : Form
    {
        Clases.DB db = new Clases.DB();
        Clases.Asistente h = new Clases.Asistente();

        public string cod_socio, socio, rtn, clave_ihcafe, direccion;
        public FrmListaComprobantesIHCAFE_Socio_X()
        {
            InitializeComponent();
        }

        private void FrmListaComprobantesIHCAFE_Socio_X_Load(object sender, EventArgs e)
        {
            LblCosecha.Text = db.Hook("COSECHA", "COSECHAS", "ESTADO='ACTIVO'");
            TxtSocio.Text = socio;
            TxtClaveIhcafe.Text = clave_ihcafe;
            TxtRtn.Text = rtn;
            TxtDireccion.Text = direccion;

            Get_Comprobantes();
        }

        private void Get_Comprobantes()
        {
            string id_socio;

            id_socio = cod_socio;

            string query = "SELECT REF_COMP, CONCAT(ALDEA, ', ', MUNICIPIO, ' ', DEPARTAMENTO), PRECIO_QQ, CANT_QQ_ORO, TOTAL FROM CAB_COMPROBANTE_IHCAFE " +
                "WHERE SOCIO_PRINC = '" + id_socio + "' ";

            DataTable fincas = db.RawSQL(query);
            DgvData.Rows.Clear();

            string _id_finca, _nombre_finca, _cant_mz, _areapord, ubicacion;

            for (int i = 0; i < fincas.Rows.Count; i++)
            {
                _id_finca = fincas.Rows[i][0].ToString();
                _nombre_finca = fincas.Rows[i][1].ToString();
                _cant_mz = fincas.Rows[i][2].ToString();
                _areapord = fincas.Rows[i][3].ToString();
                ubicacion = fincas.Rows[i][4].ToString();

                DgvData.Rows.Add(_id_finca, _nombre_finca, _cant_mz, _areapord, ubicacion);
                fincas.Dispose();
            }

        }

    }
}
