using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SC__NEBO.Formularios.Formularios_de_Menu.Socios
{
    public partial class FrmListadoSocios_Liquidacion : Form
    {

        Clases.DB db = new Clases.DB();
        Clases.Asistente a = new Clases.Asistente();

        public FrmListadoSocios_Liquidacion()
        {
            InitializeComponent();
        }

        private void FrmListadoSocios_Liquidacion_Load(object sender, EventArgs e)
        {

        }

        private void BtnBuscar_Click(object sender, EventArgs e)
        {
            string search = a.Clean(txtBuscar.Text.Trim());
            GetSocio(search);
        }

        private void GetSocio(string search = "")
        {
            string campos, condicion;
            campos = "ID_SOCIO, NOMBRE, DNI, TELEFONO, DIRECCION";

            if (search != "")
            {
                condicion = "NOMBRE LIKE '%" + search + "%' AND DEL = 'N'";

            }
            else
            {
                condicion = "DEL = 'N'";
            }

            DataTable data = db.Find("SOCIOS", campos, condicion);

            DgvData.Rows.Clear();

            int i;
            string _id_cliente, _nombre, _dni, _telefono, _direccion;

            for (i = 0; i < data.Rows.Count; i++)
            {
                _id_cliente = data.Rows[i][0].ToString();
                _nombre = data.Rows[i][1].ToString();
                _dni = data.Rows[i][2].ToString();
                _telefono = data.Rows[i][3].ToString();
                _direccion = data.Rows[i][4].ToString();
                DgvData.Rows.Add(_id_cliente, _nombre, _dni, _telefono, _direccion);
            }

            lblResumen.Text = "Mostrando " + data.Rows.Count.ToString() + " registros de " + db.Count("SOCIOS", "DEL = 'N'").ToString();
            data.Dispose();
        }

        private void DgvData_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (DgvData.Rows.Count > 0)
            {
                string cod_socio = DgvData.CurrentRow.Cells[0].Value.ToString();
                Formularios.Formularios_de_Menu.Liquidacion.Frm_Liquidacion formulario = new Liquidacion.Frm_Liquidacion();
                formulario = ((Formularios.Formularios_de_Menu.Liquidacion.Frm_Liquidacion)Owner);
                formulario.CodSocio(cod_socio);
                Close();
            }
        }

        private void pbSalir_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
