using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SC__NEBO.Formularios.Formularios_de_Menu.Cosechas
{
    public partial class FrmCosechas : Form
    {

        Clases.DB db = new Clases.DB();
        Clases.Asistente a = new Clases.Asistente();

        string descripcion, fechainicio;

        int errors;

        public FrmCosechas()
        {
            InitializeComponent();
        }

        private void pbSalir_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void FrmCosechas_Load(object sender, EventArgs e)
        {
            GetCosechas();
            Boot();
        }

        private void GetCosechas()
        {
            string campos = "COSECHA, REGISTRO, ESTADO";
            string condicion = "";

            DataTable data = db.Find("COSECHAS", campos, condicion);

            DgvData.Rows.Clear();
            string _cosecha, _fecha_inicio, _estado;
            int i;
            for (i = 0; i < data.Rows.Count; i++)
            {
                _cosecha = data.Rows[i][0].ToString();
                _fecha_inicio = Convert.ToDateTime(data.Rows[i][1].ToString()).ToShortDateString();
                _estado = data.Rows[i][2].ToString();

                DgvData.Rows.Add(_cosecha, _fecha_inicio, _estado);
            }
            data.Dispose();
        }

        private void ValidatedData()
        {
            errors = 0;
            descripcion = a.Clean(txtDescripcion.Text.Trim());
            fechainicio = a.Clean(dtpFechaInicio.Text.Trim());

            if (descripcion.Length == 0)
            {
                a.Advertencia("¡INGRESAR LA DESCRIPCIÓN DE LA COSECHA!");
                txtDescripcion.Text = "";
                txtDescripcion.Focus();
                errors++;
                return;
            }

        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            btnNuevo.Enabled = false;
            btnGuardar.Enabled = Clases.Auth.save == "S" ? true : false;
            btnCancelar.Enabled = true;
            //pbSalir.Enabled = false;

            txtDescripcion.Enabled = true;
            dtpFechaInicio.Enabled = true;
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            ValidatedData();

            string msg = "¿DESEA GUARDAR ESTE REGISTRO?";

            if (errors == 0)
            {
                if (a.Pregunta(msg) == true)
                {
                    string campos = "COSECHA, FECHA_INICIO";

                    string valores = "'" + descripcion + "','" + fechainicio + "'";


                    if (db.Save("COSECHAS", campos, valores) > 0)
                    {
                        //db.RawSQL(campos);
                        a.Aprueba("EL REGISTRO SE ALMACENÓ CON ÉXITO!");
                        Clear();
                        Boot();

                    }

                }
            }
        }

        private void Boot()
        {
            btnNuevo.Enabled = Clases.Auth.save == "S" ? true : false;
            btnGuardar.Enabled = false;
            btnCancelar.Enabled = false;
            //pbSalir.Enabled = true;

            txtDescripcion.Enabled = false;
            dtpFechaInicio.Enabled = false;

        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            string msg = "¿DESEA CANCELAR LA OPERCION EN CURSO?";
            if (a.Pregunta(msg) == true)
            {
                Clear();
                Boot();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Formularios.Formularios_de_Menu.Empleados.FrmEmpleados empleado = new Empleados.FrmEmpleados();
            empleado.Show();
            this.Hide();
        }

        private void Clear()
        {
            txtDescripcion.Clear();
            dtpFechaInicio.Text = "";
        }
    }
}
