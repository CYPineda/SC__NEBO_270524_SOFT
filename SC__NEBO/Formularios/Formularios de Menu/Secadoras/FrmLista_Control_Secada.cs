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
    public partial class FrmLista_Control_Secada : Form
    {
        Clases.DB db = new Clases.DB();
        Clases.Asistente a = new Clases.Asistente();

        string cod_secado, idcosecha, cosecha, nombre, secadora, fecha_inicio, idbodega;

        private void pbSalir_Click_1(object sender, EventArgs e)
        {
            Close();
        }

        private void BtnBuscar_Click(object sender, EventArgs e)
        {
            GetControl_Notas_de_Peso(a.Clean(txtBuscar.Text.Trim()));
        }

        private void DgvData_CellDoubleClick_1(object sender, DataGridViewCellEventArgs e)
        {
            //Recolecto el interes del socio que seleccione
            if (DgvData.Rows.Count > 0)
            {
                cod_secado = DgvData.CurrentRow.Cells[0].Value.ToString();
                cosecha = DgvData.CurrentRow.Cells[1].Value.ToString();
                nombre = DgvData.CurrentRow.Cells[2].Value.ToString();
                secadora = DgvData.CurrentRow.Cells[3].Value.ToString();
                fecha_inicio = DgvData.CurrentRow.Cells[4].Value.ToString();

                Formularios.Formularios_de_Menu.Secadoras.FrmControlSecado form = new FrmControlSecado();
                this.AddOwnedForm(form);

                form._no_secado = cod_secado;
                //form._cosecha = cosecha;
                form._empleado = nombre;
                form._secadora = secadora;
                form._fecha_inicio = fecha_inicio;

                idbodega = db.Hook("ALMACEN", "CAB_CONTROL_SECADO", "COD_SECADO='" + cod_secado + "'");
                form._bodegaid = idbodega;
                form._bodega = db.Hook("NOMBRE", "BODEGAS", "ID_BODEGA='" + idbodega + "'");

                idcosecha = db.Hook("ID_COSECHA", "CAB_CONTROL_SECADO", "COD_SECADO='" + cod_secado + "'");
                form._idcosecha = idcosecha;
                form._cosecha = db.Hook("COSECHA", "COSECHAS", "ID_COSECHA='" + idcosecha + "'");

                
                form.Show();
                this.Hide();
            }
        }

        private void btnRegresar_Click(object sender, EventArgs e)
        {
            Formularios.Formularios_de_Menu.Secadoras.FrmControlSecado form = new FrmControlSecado();
            this.AddOwnedForm(form);
            form.Show();
            this.Hide();
        }

        private void pbSalir_Click(object sender, EventArgs e)
        {
            Close();
        }

        public FrmLista_Control_Secada()
        {
            InitializeComponent();
        }

        private void FrmLista_Control_Secada_Load(object sender, EventArgs e)
        {
            this.Text = Clases.Env.APPNAME + " | LISTA DE NOTAS DE PESO | " + Clases.Auth.user + " | " + Clases.Auth.rol;
            GetControl_Notas_de_Peso();
        }


 
        private void GetControl_Notas_de_Peso(string search = "")
        {
            string campos, condicion;

            campos = " A.COD_SECADO, B.COSECHA, C.NOMBRE, D.SECADORA, A.FECHA_INICIO FROM CAB_CONTROL_SECADO A " +
                "INNER JOIN COSECHAS B ON(A.ID_COSECHA = B.ID_COSECHA) INNER JOIN EMPLEADOS C " +
                "ON(A.EMPLEADO = C.ID_EMPLEADO) INNER JOIN MAQUINAS_SECADORAS D ON(A.ID_SECADORA = D.ID) ";


            if (search != "")
            {
                condicion = "A.COD_SECADO LIKE '%" + search + "%' AND A.ESTADO = 'EN PROCESO'";
            }
            else
            {
                condicion = "A.ESTADO= 'EN PROCESO'";
            }

            DataTable data = db.Join(campos, condicion, "D.SECADORA");

            DgvData.Rows.Clear();

            string _cod_secado, _cosecha, _nombre, _secadora, _fecha;

            int i;
            for (i = 0; i < data.Rows.Count; i++)
            {
                _cod_secado = data.Rows[i][0].ToString();
                _cosecha = data.Rows[i][1].ToString();
                _nombre = data.Rows[i][2].ToString();
                _secadora = data.Rows[i][3].ToString();
                _fecha = data.Rows[i][4].ToString();

                DgvData.Rows.Add(_cod_secado, _cosecha, _nombre, _secadora, _fecha);
            }

            data.Dispose();
        }

        
    }
}
