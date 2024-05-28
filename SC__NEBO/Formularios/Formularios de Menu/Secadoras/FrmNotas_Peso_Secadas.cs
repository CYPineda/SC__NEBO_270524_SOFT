using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SC__NEBO.Formularios.Formularios_de_Menu.Secadoras
{
    public partial class FrmNotas_Peso_Secadas : Form
    {

        Clases.DB db = new Clases.DB();
        Clases.Asistente a = new Clases.Asistente();

        public FrmNotas_Peso_Secadas()
        {
            InitializeComponent();
        }

        private void FrmNotas_Peso_Secadas_Load(object sender, EventArgs e)
        {
            this.Text = Clases.Env.APPNAME + " | NOTA DE PESO DISPONIBLES | " + Clases.Auth.user + " | " + Clases.Auth.rol;
            GetNotas_de_Peso();
        }

        //private void BuscarNotaPeso(string search = "")
        //{
        //    string campos, condicion;
        //    campos = " A.ID_NOTA, B.NOMBRE, C.NOMBREFINCA, A.FECHA_NOTA_PESO, A.PESO_BRUTO, " +
        //        "A.QQ_NETO FROM NOTA_DE_PESO A " +
        //        "INNER JOIN SOCIOS B ON(A.ID_SOCIO = B.ID_SOCIO) INNER JOIN FINCAS C ON(A.ID_FINCA = C.IDFINCA) " +
        //        " INNER JOIN ESTADO_CAFE E ON(A.ID_ESTADO_CAFE = E.ID) ";

        //    if (search != "")
        //    {
        //        condicion = "B.NOMBRE LIKE '%" + search + "%' ";

        //    }
        //    else
        //    {
        //        condicion = "A.ESTADO = 'PENDIENTE'";
        //    }

        //    DataTable data = db.Join(campos, condicion, "B.NOMBRE");

        //    DgvData.Rows.Clear();

        //    string _codigo, _socio, _cantqq, _precio, _valort;
        //    int i;

        //    for (i = 0; i < data.Rows.Count; i++)
        //    {
        //        _codigo = data.Rows[i][0].ToString();
        //        _socio = data.Rows[i][1].ToString();
        //        _cantqq = data.Rows[i][2].ToString();
        //        _precio = data.Rows[i][3].ToString();
        //        _valort = data.Rows[i][4].ToString();

        //        DgvData.Rows.Add(_codigo, _socio, _cantqq, _precio, _valort);
        //    }

        //    //lblResumen.Text = "Mostrando " + data.Rows.Count.ToString() + " registros de " + db.Count("SOCIOS", "DEL = 'N'").ToString();
        //    data.Dispose();
        //}

        private void GetNotas_de_Peso(string search = "")
        {
            string campos, condicion;
            
            campos = " A.ID_NOTA, B.NOMBRE, C.UBICACION, A.FECHA_NOTA_PESO, A.PESO_BRUTO, " +
                "A.QQ_NETO FROM NOTA_DE_PESO A " +
                "INNER JOIN SOCIOS B ON(A.ID_SOCIO = B.ID_SOCIO) INNER JOIN FINCAS C ON(A.ID_FINCA = C.IDFINCA) " +
                " INNER JOIN ESTADO_CAFE E ON(A.ID_ESTADO_CAFE = E.ID) ";


            if (search != "")
            {
                condicion = "B.NOMBRE LIKE '%" + search + "%' AND A.ESTADO_SECADO = 'PENDIENTE' AND A.ESTADO != 'NULA'";
            }
            else
            {
                condicion = "A.ESTADO_SECADO = 'PENDIENTE' AND A.ESTADO != 'NULA'";
            }

            DataTable data = db.Join(campos, condicion, "A.ID_NOTA DESC");

            DgvData.Rows.Clear();

            string _idnota, _nombre, _finca, _fecha, _estado, _pesobruto, _desc_humedo, _qqnetos;

            int i;
            for (i = 0; i < data.Rows.Count; i++)
            {
                _idnota = data.Rows[i][0].ToString();
                _nombre = data.Rows[i][1].ToString();
                _finca = data.Rows[i][2].ToString();
                _fecha = Convert.ToDateTime(data.Rows[i][3]).ToShortDateString();
                _pesobruto = data.Rows[i][4].ToString();
                _qqnetos = data.Rows[i][5].ToString();

                DgvData.Rows.Add(_idnota, _nombre, _finca, _fecha, _pesobruto, _qqnetos);
            }

            data.Dispose();
        }

               
        private void DgvData_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (DgvData.SelectedRows.Count > 0)
            {
                string idnotapeso = DgvData.CurrentRow.Cells[0].Value.ToString();

                string stmt = "ESTADO_SECADO='EN PROCESO'";

                string condicion = "ID_NOTA='" + idnotapeso + "'";

                if (db.Update("NOTA_DE_PESO", stmt, condicion) > 0)
                {
                    a.Aprueba("LA NOTA DE PESO SE AGREGÓ CON EXITO!");

                    Formularios.Formularios_de_Menu.Secadoras.FrmControlSecado form = new FrmControlSecado();
                    form = ((Formularios.Formularios_de_Menu.Secadoras.FrmControlSecado)Owner);
                    form.RecibirNotaPeso(idnotapeso);
                    Close();
                }
            }
        }

        private void btnAGGNotaPeso_Click(object sender, EventArgs e)
        {
            Formularios.Formularios_de_Menu.Secadoras.FrmControlSecado form = new FrmControlSecado();
            this.AddOwnedForm(form);
            form.Show();
            this.Hide();
        }

        private void txtBuscar_KeyPress(object sender, KeyPressEventArgs e)
        {
            
        }

        private void BtnBuscar_Click(object sender, EventArgs e)
        {
            GetNotas_de_Peso(a.Clean(txtBuscar.Text.Trim()));
        }

        private void txtBuscar_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                BtnBuscar.PerformClick();
            }
        }
    }
}