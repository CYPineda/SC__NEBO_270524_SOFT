using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SC__NEBO.Formularios.Formularios_de_Menu.Proveedores
{
    public partial class FrmProveedores : Form
    {

        Clases.DB db = new Clases.DB();
        Clases.Asistente a = new Clases.Asistente();

        int errors;

        string codproveedor, proveedor, rtn, telefono, correo, direccion;

        public FrmProveedores()
        {
            InitializeComponent();
        }

        private void pbSalir_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            btnNuevo.Enabled = false;
            btnGuardar.Enabled = Clases.Auth.save == "S" ? true : false;
            btnActualizar.Enabled = false;
            btnEliminar.Enabled = false;
            btnCancelar.Enabled = true;
            btnRegresar.Enabled = false;

            TxtCodProveedor.Text = "PRV" + db.GetNext("PROVS");

            txtProveedor.Enabled = true;
            txtRTN.Enabled = true;
            txtTelefono.Enabled = true;
            txtCorreo.Enabled = true;
            txtDireccion.Enabled = true;

            txtProveedor.Focus();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            ValidateData();

            string msg = "¿DESEA GUARDAR ESTA FINCA AL SOCIO " + proveedor + "?";

            if (errors == 0)
            {
                if (a.Pregunta(msg) == true)
                {
                    //No se si ya se creo esta tabla
                    string campos = "CODIGO, PROVEEDOR, DIRECCION, RTN, CORREO, TELEFONO";

                    string valores = "'" + codproveedor + "', '" + proveedor + "', '" + direccion + "','" + rtn + "', " +
                        "'" + correo + "','" + telefono + "'";


                    if (db.Save("PROVEEDORES", campos, valores) > 0)
                    {
                        //db.RawSQL(query);
                        db.SetLast("PROVS");
                        a.Aprueba("" + proveedor + " HA SIDO REGISTRADO CON ÉXITO!");
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
                    string id_inca = TxtCodProveedor.Text.Trim();
                    string stmt = "PROVEEDOR='" + proveedor + "',DIRECCION='" + direccion + "',RTN='" + rtn + "'" +
                        ",CORREO='" + correo + "',TELEFONO='" + telefono + "'";

                    string condicion = "CODIGO='" + codproveedor + "'";

                    if (db.Update("PROVEEDORES", stmt, condicion) > 0)
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
                string id_finca = TxtCodProveedor.Text.Trim();

                if (db.Delete("PROVEEDORES", "CODIGO", id_finca) > 0)
                {
                    a.Aprueba("EL PROVEEDOR SE ELIMINÓ CON EXITO");
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

        private void pbSalir_Click_1(object sender, EventArgs e)
        {
            Close();
        }

        private void FrmProveedores_Load(object sender, EventArgs e)
        {
            GetProveedores();
            Boot();
        }

        private void DgvData_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (DgvData.Rows.Count > 0)
            {
                string id = DgvData.CurrentRow.Cells[0].Value.ToString();
                GetInfoProveedor(id);
            }
        }

        private void TxtBuscar_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                BtnSearch.PerformClick();
            }
        }

        private void BtnSearch_Click(object sender, EventArgs e)
        {
            string search = a.Clean(TxtBuscar.Text.Trim());
            GetProveedores(search);
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            GetProveedores();
            TxtBuscar.Text = "";
        }

        private void GetProveedores(string search = "")
        {
            string campos, condicion;
            //campos = "CODIGO, NOMBREORAZON";
            campos = "CODIGO, PROVEEDOR, TELEFONO";


            if (search != "")
            {
                condicion = "PROVEEDOR LIKE '%" + search + "%' AND DEL = 'N'";
            }
            else
            {
                condicion = "DEL ='N'";
            }

            DataTable data = db.Find("PROVEEDORES", campos, condicion);

            DgvData.Rows.Clear();

            int i;
            string _codigo, _proveedor, _telefono;
            for (i = 0; i < data.Rows.Count; i++)
            {
                _codigo = data.Rows[i][0].ToString();
                _proveedor = data.Rows[i][1].ToString();
                _telefono = data.Rows[i][2].ToString();
                DgvData.Rows.Add(_codigo, _proveedor, _telefono);
            }
            //LblResumen.Text = "MOSTRANDO " + data.Rows.Count.ToString() + " DE " + db.Count("EMPLEADOS", "DEL='N'").ToString() + " REGISTROS. ";
            data.Dispose();
        }


        private void GetInfoProveedor(string id)
        {
            string condicion = "CODIGO='" + id + "' AND DEL='N'";
            DataTable proveedor = db.Find("PROVEEDORES", "*", condicion);

            if (proveedor.Rows.Count > 0)
            {
                btnNuevo.Enabled = false;
                btnGuardar.Enabled = false;
                btnActualizar.Enabled = Clases.Auth.update == "S" ? true : false;
                btnEliminar.Enabled = Clases.Auth.delete == "S" ? true : false;
                btnCancelar.Enabled = true;


                DataRow info = proveedor.Rows[0];
                TxtCodProveedor.Text = info["CODIGO"].ToString();
                txtProveedor.Text = info["PROVEEDOR"].ToString();
                txtDireccion.Text = info["DIRECCION"].ToString();
                txtRTN.Text = info["RTN"].ToString();
                txtCorreo.Text = info["CORREO"].ToString();
                txtTelefono.Text = info["TELEFONO"].ToString();

                TxtCodProveedor.Enabled = false;
                txtProveedor.Enabled = Clases.Auth.update == "S" ? true : false;
                txtDireccion.Enabled = Clases.Auth.update == "S" ? true : false;
                txtRTN.Enabled = Clases.Auth.update == "S" ? true : false;
                txtCorreo.Enabled = Clases.Auth.update == "S" ? true : false;
                txtTelefono.Enabled = Clases.Auth.update == "S" ? true : false;
            }
            else
            {
                a.Advertencia("ERROR AL RECUPERAR LOS DATOS DEL REGISTRO SELECCIONADO");

            }
            proveedor.Dispose();
        }

        private void Clear()
        {
            TxtCodProveedor.Clear();
            txtProveedor.Clear();
            txtRTN.Clear();
            txtTelefono.Clear();
            txtCorreo.Clear();
            txtDireccion.Clear();
        }

        public void ValidateData()
        {
            errors = 0;
            codproveedor = a.Clean(TxtCodProveedor.Text.Trim());
            proveedor = a.Clean(txtProveedor.Text.Trim());
            rtn = a.Clean(txtRTN.Text.Trim());
            telefono = a.Clean(txtTelefono.Text.Trim());
            correo = a.Clean(txtCorreo.Text.Trim());
            direccion = a.Clean(txtDireccion.Text.Trim());

            if (codproveedor.Length == 0)
            {
                a.Advertencia("¡EL CODIGO DEL PROVEEDOR ES REQUERIDO!");
                TxtCodProveedor.Text = "";
                TxtCodProveedor.Focus();
                errors++;
                return;
            }

            if (proveedor.Length == 0)
            {
                a.Advertencia("¡EL NOMBRE DEL PROVEEDOR ES REQUERIDO!");
                txtProveedor.Text = "";
                txtProveedor.Focus();
                errors++;
                return;
            }

            if (rtn.Length == 0)
            {
                a.Advertencia("¡EL RTN ES REQUERIDO!");
                txtRTN.Text = "";
                txtRTN.Focus();
                errors++;
                return;
            }

            if (telefono.Length == 0)
            {
                a.Advertencia("¡EL NÚMERO DE TELÉFONO ES REQUERIDO!");
                txtTelefono.Text = "";
                txtTelefono.Focus();
                errors++;
            }


            if (correo.Length == 0)
            {
                a.Advertencia("¡EL CORREO ES REQUERIDO!");
                txtCorreo.Text = "";
                txtCorreo.Focus();
                errors++;
            }

            if (direccion.Length == 0)
            {
                a.Advertencia("¡LA DIRECCIÓN ES REQUERIDA!");
                txtDireccion.Text = "";
                txtDireccion.Focus();
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

            TxtCodProveedor.Enabled = false;
            txtProveedor.Enabled = false;
            txtRTN.Enabled = false;
            txtTelefono.Enabled = false;
            txtCorreo.Enabled = false;
            txtDireccion.Enabled = false;

        }
    }
}
