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
    public partial class FrmMaquinas : Form
    {
        Clases.DB db = new Clases.DB();
        Clases.Asistente a = new Clases.Asistente();

        int errors;

        string idsecadora, secadora, capacidad;

        public FrmMaquinas()
        {
            InitializeComponent();
        }

        private void FrmOrdenSecado_Load(object sender, EventArgs e)
        {
            GetMaquinas();
            Boot();
        }

        private void GetMaquinas(string search = "")
        {
            string campos, condicion;
            //campos = "CODIGO, NOMBREORAZON";
            campos = "ID, SECADORA, CAPACIDAD_QQ";


            if (search != "")
            {
                condicion = "SECADORA LIKE '%" + search + "%' AND DEL = 'N'";
            }
            else
            {
                condicion = "DEL ='N'";
            }

            DataTable data = db.Find("MAQUINAS_SECADORAS", campos, condicion);

            DgvData.Rows.Clear();

            int i;
            string _codigo, _secadora, _capacidad;
            for (i = 0; i < data.Rows.Count; i++)
            {
                _codigo = data.Rows[i][0].ToString();
                _secadora = data.Rows[i][1].ToString();
                _capacidad = data.Rows[i][2].ToString();
                DgvData.Rows.Add(_codigo, _secadora, _capacidad);
            }

            data.Dispose();
        }

        private void GetInfoMaquinas(string id)
        {
            string condicion = "ID='" + id + "' AND DEL='N'";
            DataTable finca = db.Find("MAQUINAS_SECADORAS", "*", condicion);

            if (finca.Rows.Count > 0)
            {
                btnNuevo.Enabled = false;
                btnGuardar.Enabled = false;
                btnActualizar.Enabled = Clases.Auth.update == "S" ? true : false;
                btnEliminar.Enabled = Clases.Auth.delete == "S" ? true : false;
                btnCancelar.Enabled = true;


                DataRow info = finca.Rows[0];
                txtIdSecadora.Text = info["ID"].ToString();
                txtSecadora.Text = info["SECADORA"].ToString();
                txtCapacidad.Text = info["CAPACIDAD_QQ"].ToString();

                txtIdSecadora.Enabled = false;
                txtSecadora.Enabled = Clases.Auth.update == "S" ? true : false;
                txtCapacidad.Enabled = Clases.Auth.update == "S" ? true : false;

            }
            else
            {
                a.Advertencia("ERROR AL RECUPERAR LOS DATOS DEL REGISTRO SELECCIONADO");

            }
            finca.Dispose();
        }

        private void Clear()
        {
            txtIdSecadora.Clear();
            txtSecadora.Clear();
            txtCapacidad.Clear();
        }

        private void pbSalir_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            string msg = "¿DESEA CANCELAR LA ACCION?";

            if (a.Pregunta(msg) == true)
            {
                Clear();
                Boot();
            }
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            btnNuevo.Enabled = false;
            btnGuardar.Enabled = Clases.Auth.save == "S" ? true : false;
            btnActualizar.Enabled = false;
            btnEliminar.Enabled = false;
            btnCancelar.Enabled = true;
            //btnRegresar.Enabled = false;

            txtIdSecadora.Text = "MQS" + db.GetNext("MAQSC");


            txtIdSecadora.Enabled = false;
            txtSecadora.Enabled = true;
            txtCapacidad.Enabled = true;

            //txtSecadora.Focus();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            ValidateData();

            string msg = "¿DESEA GUARDAR ESTE REGISTRO?";

            if (errors == 0)
            {
                if (a.Pregunta(msg) == true)
                {
                    //No se si ya se creo esta tabla
                    string campos = "ID, SECADORA, CAPACIDAD_QQ";

                    string valores = "'" + idsecadora + "','" + secadora + "', '" + capacidad + "'";


                    if (db.Save("MAQUINAS_SECADORAS", campos, valores) > 0)
                    {
                        db.SetLast("MAQSC");
                        a.Aprueba("EL REGISTRO SE HA SIDO REGISTRADO CON ÉXITO!");
                        Clear();
                        Boot();
                    }
                }
            }
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            ValidateData();

            if (errors == 0)
            {
                string msg = "DESEA GUARDAR LOS CAMBIOS DEL REGISTRO SELECCIONADO";
                if (a.Pregunta(msg) == true)
                {
                    string id_sec = txtIdSecadora.Text.Trim();
                    string stmt = "SECADORA='" + secadora + "',CAPACIDAD_QQ='" + capacidad + "'";

                    string condicion = "ID='" + idsecadora + "'";

                    if (db.Update("MAQUINAS_SECADORAS", stmt, condicion) > 0)
                    {
                        a.Aprueba("LOS CAMBIOS SE APLICARON CORRECTAMENTE!");
                        Clear();
                        Boot();
                    }
                }
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            string msg = "¿DESEA ELIMINAR EL REGISTRO SELECCIONADO?";

            if (a.Pregunta(msg) == true)
            {
                string id_secadora = txtIdSecadora.Text.Trim();

                if (db.Delete("MAQUINAS_SECADORAS", "ID", id_secadora) > 0)
                {
                    a.Aprueba("EL REGISTRO SE ELIMINÓ CON EXITO");
                    Clear();
                    Boot();
                }
            }
        }

        private void DgvData_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (DgvData.Rows.Count > 0)
            {
                string id = DgvData.CurrentRow.Cells[0].Value.ToString();
                GetInfoMaquinas(id);
            }
        }

        public void ValidateData()
        {
            errors = 0;
            secadora = a.Clean(txtSecadora.Text.Trim());
            capacidad = a.Clean(txtCapacidad.Text.Trim());
            idsecadora = a.Clean(txtIdSecadora.Text.Trim());

            if (idsecadora.Length == 0)
            {
                a.Advertencia("¡EL CODIGO DE LA SECADORA ES REQUERIDA!");
                txtIdSecadora.Text = "";
                txtIdSecadora.Focus();
                errors++;
                return;
            }

            if (secadora.Length == 0)
            {
                a.Advertencia("¡EL NOMBRE DE LA SECADORA ES REQUERIDO!");
                txtSecadora.Text = "";
                txtSecadora.Focus();
                errors++;
                return;
            }

            if (capacidad.Length == 0)
            {
                a.Advertencia("¡LA CAPACIDAD ES REQUERIDA!");
                txtCapacidad.Text = "";
                txtCapacidad.Focus();
                errors++;
                return;
            }

        }

        private void Boot()
        {
            btnNuevo.Enabled = Clases.Auth.save == "S" ? true : false;
            btnGuardar.Enabled = false;
            btnActualizar.Enabled = false;
            btnEliminar.Enabled = false;
            btnCancelar.Enabled = false;
            pbSalir.Enabled = true;

            txtIdSecadora.Enabled = false;
            txtSecadora.Enabled = false;
            txtCapacidad.Enabled = false;

        }
    }
}
