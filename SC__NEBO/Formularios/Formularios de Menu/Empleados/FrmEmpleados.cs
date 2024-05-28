using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SC__NEBO.Formularios.Formularios_de_Menu.Empleados
{
    public partial class FrmEmpleados : Form
    {
        Clases.DB db = new Clases.DB();
        Clases.Asistente a = new Clases.Asistente();

        int errors;

        string idempleado, nombre, dni, rtn, estadocivil, telefono, nocuenta, direccion, cargo, correo;

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            string msg = "¿DESEA ELIMINAR EL REGISTRO SELECCIONADO?";

            if (a.Pregunta(msg) == true)
            {
                string id_empleado = txtIdEmpleado.Text.Trim();

                if (db.Delete("EMPLEADOS", "ID_EMPLEADO", id_empleado) > 0)
                {
                    a.Aprueba("EL EMPLEADO SE ELIMINÓ CON EXITO");
                    Clear();
                    Boot();
                }
            }
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

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            ValidateData();

            if (errors == 0)
            {
                string msg = "DESEA GUARDAR LOS CAMBIOS DEL REGISTRO SELECCIONADO";
                if (a.Pregunta(msg) == true)
                {
                    string id_inca = txtIdEmpleado.Text.Trim();
                    string stmt = "NOMBRE='" + nombre + "',DNI='" + dni + "',RTN='" + rtn + "',DIRECCION='" + direccion + "' " +
                        ",ESTADO_CIVIL='" + estadocivil + "',TELEFONO='" + telefono + "',NO_CUENTA='" + nocuenta + "',ID_AREA=" + cargo + " " +
                        ",CORREO='" + correo + "'";

                    string condicion = "ID_EMPLEADO='" + idempleado + "'";

                    if (db.Update("EMPLEADOS", stmt, condicion) > 0)
                    {
                        a.Aprueba("LOS CAMBIOS SE APLICARON CORRECTAMENTE!");
                        Clear();
                        Boot();
                    }
                }
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            ValidateData();

            string msg = "¿DESEA REGISTRAR A " + nombre + "?";

            if (errors == 0)
            {
                if (a.Pregunta(msg) == true)
                {
                    //No se si ya se creo esta tabla
                    string campos = "ID_EMPLEADO, NOMBRE, DNI, RTN, DIRECCION, ESTADO_CIVIL, TELEFONO, NO_CUENTA, ID_AREA, CORREO";

                    string valores = "'" + idempleado + "', '" + nombre + "', '" + dni + "', '" + rtn + "','" + direccion + "','" + estadocivil + "', " +
                        "'" + telefono + "', '" + nocuenta + "', " + cargo + ", '" + correo + "'";


                    if (db.Save("EMPLEADOS", campos, valores) > 0)
                    {
                        //db.RawSQL(query);
                        db.SetLast("EMPLS");
                        a.Aprueba("" + nombre + " HA SIDO REGISTRADO CON ÉXITO!");
                        Clear();
                        Boot();
                    }
                }
            }
        }

        private void DgvData_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (DgvData.Rows.Count > 0)
            {
                string id = DgvData.CurrentRow.Cells[0].Value.ToString();
                GetInfoEmpleados(id);
            }
        }

        private void pbSalir_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void TxtBuscar_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                BtnSearch.PerformClick();
            }
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            GetEmpleados();
            TxtBuscar.Text = "";
        }

        private void BtnSearch_Click(object sender, EventArgs e)
        {
            string search = a.Clean(TxtBuscar.Text.Trim());
            GetEmpleados(search);
        }

        private void pbMenu_Click(object sender, EventArgs e)
        {

        }


        private void btnNuevo_Click(object sender, EventArgs e)
        {
            btnNuevo.Enabled = false;
            btnGuardar.Enabled = Clases.Auth.save == "S" ? true : false;
            btnActualizar.Enabled = false;
            btnEliminar.Enabled = false;
            btnCancelar.Enabled = true;
            btnRegresar.Enabled = false;

            txtIdEmpleado.Text = "EMP" + db.GetNext("EMPLS"); 

            txtNombre.Enabled = true;
            txtDireccion.Enabled = true;
            txtDNI.Enabled = true;
            txtRTN.Enabled = true;
            txtNoCuenta.Enabled = true;
            txtTelefono.Enabled = true;
            cmbEstadoCivil.Enabled = true;
            cmbCargo.Enabled = true;
            txtCorreo.Enabled = true;

            //txtNombre.Focus();
        }

        public FrmEmpleados()
        {
            InitializeComponent();
        }

        private void FrmEmpleados_Load(object sender, EventArgs e)
        {
            this.Text = Clases.Env.APPNAME + " | REGISTRO DE EMPLEADOS| " + Clases.Auth.user + " | " + Clases.Auth.rol;
            Boot();
            Tipo_Cargo();
            GetEmpleados();
        }

        private void Tipo_Cargo()
        {
            cmbCargo.DataSource = db.Find("AREAS_EMPLEADOS", "ID, AREA", "", "AREA");
            cmbCargo.ValueMember = "ID";
            cmbCargo.DisplayMember = "AREA";
            cmbCargo.SelectedIndex = -1;
        }

        private void GetEmpleados(string search = "")
        {
            string campos, condicion;
            campos = "A.ID_EMPLEADO, A.NOMBRE, B.AREA AS AREA  FROM EMPLEADOS " +
                "A INNER JOIN AREAS_EMPLEADOS B ON(A.ID_AREA = B.ID)";

            if (search != "")
            {
                condicion = "A.NOMBRE LIKE '%" + search + "%' AND A.DEL = 'N'";

            }
            else
            {
                condicion = "A.DEL = 'N'";
            }

            DataTable data = db.Join(campos, condicion, "A.NOMBRE");

            DgvData.Rows.Clear();

            int i;
            string _id_empleado, _nombre, _area;

            for (i = 0; i < data.Rows.Count; i++)
            {
                _id_empleado = data.Rows[i][0].ToString();
                _nombre = data.Rows[i][1].ToString();
                _area = data.Rows[i][2].ToString();
                DgvData.Rows.Add(_id_empleado, _nombre, _area);
            }

            //lblResumen.Text = "Mostrando " + data.Rows.Count.ToString() + " registros de " + db.Count("SOCIOS", "DEL = 'N'").ToString();
            data.Dispose();
        }

        private void GetInfoEmpleados(string id)
        {
            string condicion = "ID_EMPLEADO='" + id + "' AND DEL='N'";
            DataTable cliente = db.Find("EMPLEADOS", "*", condicion);

            if (cliente.Rows.Count > 0)
            {
                btnNuevo.Enabled = false;
                btnGuardar.Enabled = false;
                btnActualizar.Enabled = Clases.Auth.update == "S" ? true : false;
                btnEliminar.Enabled = Clases.Auth.delete == "S" ? true : false;
                btnCancelar.Enabled = true;


                DataRow info = cliente.Rows[0];
                txtIdEmpleado.Text = info["ID_EMPLEADO"].ToString();
                txtNombre.Text = info["NOMBRE"].ToString();
                txtDNI.Text = info["DNI"].ToString();
                txtRTN.Text = info["RTN"].ToString();
                txtDireccion.Text = info["DIRECCION"].ToString();
                cmbEstadoCivil.Text = info["ESTADO_CIVIL"].ToString();
                txtTelefono.Text = info["TELEFONO"].ToString();
                txtNoCuenta.Text = info["NO_CUENTA"].ToString();
                cmbCargo.SelectedValue = info["ID_AREA"].ToString();
                txtCorreo.Text = info["CORREO"].ToString();

                txtIdEmpleado.Enabled = false;
                txtNombre.Enabled = Clases.Auth.update == "S" ? true : false;
                txtDNI.Enabled = Clases.Auth.update == "S" ? true : false;
                txtRTN.Enabled = Clases.Auth.update == "S" ? true : false;
                txtDireccion.Enabled = Clases.Auth.update == "S" ? true : false;
                cmbEstadoCivil.Enabled = Clases.Auth.update == "S" ? true : false;
                txtTelefono.Enabled = Clases.Auth.update == "S" ? true : false;
                txtNoCuenta.Enabled = Clases.Auth.update == "S" ? true : false;
                cmbCargo.Enabled = Clases.Auth.update == "S" ? true : false;
                txtCorreo.Enabled = Clases.Auth.update == "S" ? true : false;

                //TxtConfirmar.Enabled = true;
            }
            else
            {
                a.Advertencia("ERROR AL RECUPERAR LOS DATOS DEL REGISTRO SELECCIONADO");

            }
            cliente.Dispose();
        }

        private void Clear()
        {
            txtIdEmpleado.Clear();
            txtNombre.Clear();
            txtDNI.Clear();
            txtRTN.Clear();
            txtTelefono.Clear();
            txtNoCuenta.Clear();
            txtDireccion.Clear();
            cmbCargo.SelectedIndex = -1;
            cmbEstadoCivil.SelectedIndex = -1;
            txtCorreo.Clear();
        }

        public void ValidateData()
        {
            errors = 0;
            idempleado = a.Clean(txtIdEmpleado.Text.Trim());
            nombre = a.Clean(txtNombre.Text.Trim());
            dni = a.Clean(txtDNI.Text.Trim());
            rtn = a.Clean(txtRTN.Text.Trim());
            telefono = a.Clean(txtTelefono.Text.Trim());
            nocuenta = a.Clean(txtNoCuenta.Text.Trim());
            direccion = a.Clean(txtDireccion.Text.Trim());
            //cargo = a.Clean(cmbCargo.Text.Trim());
            cargo = cmbCargo.Text != "" ? cmbCargo.SelectedValue.ToString() : "";
            estadocivil = a.Clean(cmbEstadoCivil.Text.Trim());
            correo = a.Clean(txtCorreo.Text.Trim());

            if (idempleado.Length == 0)
            {
                a.Advertencia("¡EL CODIGO DEL EMPLEADO ES REQUERIDO!");
                txtIdEmpleado.Text = "";
                txtIdEmpleado.Focus();
                errors++;
                return;
            }

            if (nombre.Length == 0)
            {
                a.Advertencia("¡EL NOMBRE DEL EMPLEADO ES REQUERIDO!");
                txtNombre.Text = "";
                txtNombre.Focus();
                errors++;
                return;
            }

            if (direccion.Length == 0)
            {
                a.Advertencia("¡LA DIRECCIÓN DEL EMPLEADO ES REQUERIDA!");
                txtDireccion.Text = "";
                txtDireccion.Focus();
                errors++;
                return;
            }

            if (dni.Length == 0)
            {
                a.Advertencia("¡EL DNI DEL EMPLEADO ES REQUERIDO!");
                txtDNI.Text = "";
                txtDNI.Focus();
                errors++;
            }


            if (rtn.Length == 0)
            {
                a.Advertencia("¡EL RTN DEL EMPLEADO ES REQUERIDO!");
                txtRTN.Text = "";
                txtRTN.Focus();
                errors++;
            }

            if (estadocivil.Length == 0)
            {
                a.Advertencia("¡EL ESTADO CIVIL DEL EMPLEADO ES REQUERIDO!");
                cmbEstadoCivil.Text = "";
                cmbEstadoCivil.Focus();
                errors++;
            }

            if (telefono.Length == 0)
            {
                a.Advertencia("¡EL NÚMERO DE TELÉFONO DEL EMPLEADO ES REQUERIDO!");
                txtTelefono.Text = "";
                txtTelefono.Focus();
                errors++;
            }

            if (nocuenta.Length == 0)
            {
                a.Advertencia("¡EL NÚMERO DE CUENTA DEL EMPLEADO ES REQUERIDO!");
                txtNoCuenta.Text = "";
                txtNoCuenta.Focus();
                errors++;
            }

            if (direccion.Length == 0)
            {
                a.Advertencia("¡LA DIRECIÓN DEL EMPLEADO ES REQUERIDA!");
                txtDireccion.Text = "";
                txtDireccion.Focus();
                errors++;
            }

            if (cargo.Length == 0)
            {
                a.Advertencia("¡EL CARGO DEL EMPLEADO ES REQUERIDO!");
                cmbCargo.Text = "";
                cmbCargo.Focus();
                errors++;
            }

            if (correo.Length == 0)
            {
                a.Advertencia("¡EL CORREO DEL EMPLEADO ES REQUERIDO!");
                txtCorreo.Text = "";
                txtCorreo.Focus();
                errors++;
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

            txtIdEmpleado.Enabled = false;
            txtNombre.Enabled = false;
            txtDireccion.Enabled = false;
            txtDNI.Enabled = false;
            txtRTN.Enabled = false;
            txtTelefono.Enabled = false;
            txtNoCuenta.Enabled = false;
            cmbEstadoCivil.Enabled = false;
            cmbCargo.Enabled = false;
            txtCorreo.Enabled = false;

            //lblMsg.Visible = false;

            //DgvData.Rows.Clear();
            //txtBuscar.Clear();
            //txtBuscar.Focus();

        }
    }
}
