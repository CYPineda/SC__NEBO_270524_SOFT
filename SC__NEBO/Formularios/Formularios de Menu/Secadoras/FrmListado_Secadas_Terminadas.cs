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
    public partial class FrmListado_Secadas_Terminadas : Form
    {

        Clases.DB db = new Clases.DB();
        Clases.Asistente a = new Clases.Asistente();

        public FrmListado_Secadas_Terminadas()
        {
            InitializeComponent();
        }

        private void FrmListado_Secadas_Terminadas_Load(object sender, EventArgs e)
        {
            this.Text = Clases.Env.APPNAME + " | SECADAS FINALIZADAS | " + Clases.Auth.user + " | " + Clases.Auth.rol;
            GetSecadas_Terminadas();
        }

        string fechai, fechaf;
        private void GetSecadas_Terminadas(string search = "")
        {
            string campos, condicion;

            fechai = dtpFechaIncial.Value.ToString("dd-MM-yyyy");
            fechaf = dtpFechaFinal.Value.ToString("dd-MM-yyyy");

            campos = " A.COD_SECADO, C.SECADORA, A.FECHA_INICIO, A.FECHA_FINAL, A.QQ_BRUTOS_HUMEDO, A.QQ_NETOS_HUMEDO, A.TOLVA_ALMACENAMIENTO, " +
                "A.RENDIMIENTO " +
                "FROM CAB_CONTROL_SECADO A INNER JOIN EMPLEADOS B ON(A.EMPLEADO = B.ID_EMPLEADO) " +
                "INNER JOIN MAQUINAS_SECADORAS C ON(A.ID_SECADORA = C.ID) ";


            if (search != "")
            {
                condicion = "A.COD_SECADO LIKE '%" + search + "%' AND A.FECHA_INICIO BETWEEN '" + fechai + "' AND '" + fechaf + "' AND A.ESTADO = 'SECADA'";
            }
            else
            {
                condicion = "A.FECHA_INICIO BETWEEN '" + fechai + "' AND '" + fechaf + "' AND A.ESTADO = 'SECADA'";
            }

            DataTable data = db.Join(campos, condicion, "A.COD_SECADO DESC");

            DgvData.Rows.Clear();

            string _cod_secado, _secadora, _fechainicio, _fechafinal, _qqbrutos, _qqnetos, _tolva, _rendimiento;

            int i;
            for (i = 0; i < data.Rows.Count; i++)
            {
                _cod_secado = data.Rows[i][0].ToString();
                _secadora = data.Rows[i][1].ToString();
                _fechainicio = data.Rows[i][2].ToString();
                _fechafinal = data.Rows[i][3].ToString();
                _qqbrutos = data.Rows[i][4].ToString();
                _qqnetos = data.Rows[i][5].ToString();
                _tolva = data.Rows[i][6].ToString();
                _rendimiento = data.Rows[i][7].ToString();

                DgvData.Rows.Add(_cod_secado, _secadora, _fechainicio, _fechafinal, _qqbrutos, _qqnetos, _tolva, _rendimiento);
            }

            data.Dispose();
        }

        private void SumaQQ_Netos()
        {
            double total_qqnetos = 0;

            for (int i = 0; i < DgvData.Rows.Count; i++)
            {
                total_qqnetos += Convert.ToDouble(DgvData.Rows[i].Cells[7].Value.ToString());
            }

            lblQQRendimiento.Text = total_qqnetos.ToString();
        }

        private void BtnBuscar_Click(object sender, EventArgs e)
        {
            GetSecadas_Terminadas(a.Clean(txtBuscar.Text.Trim()));
            SumaQQ_Netos();
        }

        private void btnRegresar_Click(object sender, EventArgs e)
        {
            Formularios.Formularios_de_Menu.Secadoras.FrmControlSecado form = new FrmControlSecado();
            this.AddOwnedForm(form);
            form.Show();
            this.Hide();
        }
    }
}
