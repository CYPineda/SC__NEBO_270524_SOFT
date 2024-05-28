using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SC__NEBO.Formularios.Formularios_de_Menu.Clientes
{
    public partial class Frm_Clientes : Form
    {

        Clases.DB db = new Clases.DB();
        Clases.Asistente a = new Clases.Asistente();

        int errors;

        string idcliente, nombre, apellido, direccion, dni, rtn, estadocivil, direccion_finca, claveihcafe, telefono, no_cuenta, municipio, depto;

        private void txtIdCliente_TextChanged(object sender, EventArgs e)
        {
            if (txtIdCliente.Text.Trim().Length == 5)
            {
                string id = a.Clean(txtIdCliente.Text.Trim());
                CheckIfIdExist(id);
            }
            else
            {
                lblMsg.Visible = false;
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
                    string id_cliente = txtIdCliente.Text.Trim();
                    string stmt = "NOMBRES='" + nombre + "',APELLIDOS='" + apellido + "',DNI='"+dni+"',RTN='"+rtn+"',DIRECCION='" + direccion + "',DIRECCION_FINCA='" + direccion_finca + "',TELEFONO='" + telefono + "'," +
                        "ESTADO_CIVIL='" + estadocivil + "',CLAVE_IHCAFE='" + claveihcafe + "',NO_CUENTA='" + no_cuenta + "',MUNICIPIO='" + municipio + "',DEPARTAMENTO='" + depto + "'";
                    string condicion = "ID_CLIENTE='" + id_cliente + "'";

                    if (db.Update("CLIENTES", stmt, condicion) > 0)
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

            string msg = "¿DESEA GUARDAR EL CLIENTE " + apellido + "?";

            if (errors == 0)
            {
                if (a.Pregunta(msg) == true)
                {
                    string campos = "ID_CLIENTE, NOMBRES, APELLIDOS,DNI, RTN, DIRECCION, DIRECCION_FINCA, TELEFONO, ESTADO_CIVIL, CLAVE_IHCAFE, NO_CUENTA, MUNICIPIO, DEPARTAMENTO";

                    string valores = "'" + idcliente + "', '" + nombre + "','" + apellido + "','" + dni + "', '" + rtn +"','" + direccion + "','" + direccion_finca + "','" + telefono + "', '" + estadocivil + "', '" + claveihcafe + "', '"+no_cuenta+"','"+municipio+ "''" + depto + "',";


                    if (db.Save("CLIENTES", campos, valores) > 0)
                    {
                        //db.RawSQL(query);
                        db.SetLast("CLIENT");
                        a.Aprueba("EL CLIENTE " + apellido + " HA SIDO REGISTRADO CON ÉXITO!");
                        //db.SetLast(idcorre);
                        Clear();
                        Boot();
                    }
                }
            }
        }

        private void Clear()
        {
            txtIdCliente.Clear();
            txtNombre.Clear();
            txtApellido.Clear();
            txtDNI.Clear();
            txtRTN.Clear();
            cmbEstadoCivil.SelectedIndex = -1;
            txtDireccion.Clear();
            txtDireccion_Finca.Clear();
            txtClaveIHCAFE.Clear();
            txtTelefono.Clear();
            txtNo_Cuenta.Clear();
            txtMunicipio.Clear();
            txtDepto.Clear();
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Boot()
        {
            btnNuevo.Enabled = Clases.Auth.save == "S" ? true : false;
            btnGuardar.Enabled = false;
            btnActualizar.Enabled = false;
            btnEliminar.Enabled = false;
            btnCancelar.Enabled = false;
            pbSalir.Enabled = true;

            txtIdCliente.Enabled = false;
            txtNombre.Enabled = false;
            txtApellido.Enabled = false;
            txtDNI.Enabled = false;
            txtRTN.Enabled = false;
            cmbEstadoCivil.Enabled = false;
            txtDireccion.Enabled = false;
            txtDireccion_Finca.Enabled = false;
            txtClaveIHCAFE.Enabled = false;
            txtTelefono.Enabled = false;
            txtNo_Cuenta.Enabled = false;
            txtMunicipio.Enabled = false;
            txtDepto.Enabled = false;

            lblMsg.Visible = false;

            //DgvData.Rows.Clear();
            //txtBuscar.Clear();
            //txtBuscar.Focus();

        }

        public void ValidateData()
        {
            errors = 0;
            idcliente = a.Clean(txtIdCliente.Text.Trim());
            nombre = a.Clean(txtNombre.Text.Trim());
            apellido = a.Clean(txtApellido.Text.Trim());
            dni = a.Clean(txtDNI.Text.Trim());
            rtn = a.Clean(txtRTN.Text.Trim());
            estadocivil= a.Clean(cmbEstadoCivil.Text.Trim());
            direccion = a.Clean(txtDireccion.Text.Trim());
            direccion_finca = a.Clean(txtDireccion_Finca.Text.Trim());
            claveihcafe = a.Clean(txtClaveIHCAFE.Text.Trim());
            telefono = txtTelefono.Text.Trim();
            no_cuenta = a.Clean(txtNo_Cuenta.Text.Trim());
            municipio = a.Clean(txtMunicipio.Text.Trim());
            depto = a.Clean(txtDepto.Text.Trim());

            if (idcliente.Length == 0)
            {
                a.Advertencia("¡INGRESAR EL ID DEL CLIENTE!");
                txtIdCliente.Text = "";
                txtIdCliente.Focus();
                errors++;
                return;
            }

            if (nombre.Length == 0)
            {
                a.Advertencia("¡INGRESAR EL NOMBRE DEL CLIENTE!");
                txtNombre.Text = "";
                txtNombre.Focus();
                errors++;
            }

            if (apellido.Length == 0)
            {
                a.Advertencia("¡INGRESAR EL APELLIDO DEL CLIENTE!");
                txtApellido.Text = "";
                txtApellido.Focus();
                errors++;
            }
                        
            if (dni.Length == 0)
            {
                a.Advertencia("¡INGRESAR EL DNI DEL CLIENTE!");
                txtDNI.Text = "";
                txtDNI.Focus();
                errors++;
            }

            if (rtn.Length == 0)
            {
                a.Advertencia("¡INGRESAR EL RTN DEL CLIENTE!");
                txtRTN.Text = "";
                txtRTN.Focus();
                errors++;
            }

            if (estadocivil.Length == 0)
            {
                a.Advertencia("¡SELECCIONE EL ESTADO CIVIL DEL CLIENTE!");
                cmbEstadoCivil.Text = "";
                cmbEstadoCivil.Focus();
                errors++;
            }
            if (direccion.Length == 0)
            {
                a.Advertencia("¡INGRESAR LA DIRECCION!");
                txtDireccion.Text = "";
                txtDireccion.Focus();
                errors++;
            }

            if (direccion_finca.Length == 0)
            {
                a.Advertencia("¡INGRESAR LA DIRECCION!");
                txtDireccion.Text = "";
                txtDireccion.Focus();
                errors++;
            }

            if (claveihcafe.Length == 0)
            {
                a.Advertencia("¡INGRESAR LA CLAVE DE IHCAFE!");
                txtClaveIHCAFE.Text = "";
                txtClaveIHCAFE.Focus();
                errors++;
            }

            if (txtTelefono.MaskFull == false)
            {
                a.Advertencia("¡INGRESAR EL NUMERO DE TELEFONO!");
                txtTelefono.Text = telefono;
                txtTelefono.Focus();
                errors++;
                return;
            }

            if (no_cuenta.Length == 0)
            {
                a.Advertencia("¡INGRESAR EL NUMERO DE CUENTA!");
                txtNo_Cuenta.Text = "";
                txtNo_Cuenta.Focus();
                errors++;
            }

            if (municipio.Length == 0)
            {
                a.Advertencia("¡INGRESAR EL MUNICIPIO!");
                txtMunicipio.Text = "";
                txtMunicipio.Focus();
                errors++;
            }

            if (depto.Length == 0)
            {
                a.Advertencia("¡INGRESAR EL DEPARTAMENTO!");
                txtDepto.Text = "";
                txtDepto.Focus();
                errors++;
            }

        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            btnNuevo.Enabled = false;
            btnGuardar.Enabled = Clases.Auth.save == "S" ? true : false;
            btnActualizar.Enabled = false;
            btnEliminar.Enabled = false;
            btnCancelar.Enabled = true;
            btnRegresar.Enabled = false;

            txtIdCliente.Text = "CLT" + db.GetNext("CLIEN");

            txtIdCliente.Enabled = false;
            txtNombre.Enabled = true;
            txtApellido.Enabled = true;
            txtDireccion.Enabled = true;
            txtDireccion_Finca.Enabled = true;
            txtDNI.Enabled = true;
            txtRTN.Enabled = true;
            cmbEstadoCivil.Enabled = true;
            txtClaveIHCAFE.Enabled = true;
            txtTelefono.Enabled = true;
            txtNo_Cuenta.Enabled = true;
            txtMunicipio.Enabled = true;
            txtDepto.Enabled = true;

            txtNombre.Focus();
        }

        //private void GetDocente(string search = "")
        //{
        //    string campos, condicion;
        //    campos = "IDDOCENTE, NOMBRE + ' ' + APELLIDOS, DIRECCION";

        //    if (search != "")
        //    {
        //        condicion = "NOMBRE LIKE '%" + search + "%' AND DEL = 'N'";

        //    }
        //    else
        //    {
        //        condicion = "DEL = 'N'";
        //    }

        //    DataTable data = db.Find("DOCENTES", campos, condicion);

        //    DgvData.Rows.Clear();

        //    int i;
        //    string _iddocente, _docente, _direccion;

        //    for (i = 0; i < data.Rows.Count; i++)
        //    {
        //        iddocente = data.Rows[i][0].ToString();
        //        _docente = data.Rows[i][1].ToString();
        //        _direccion = data.Rows[i][2].ToString();
        //        DgvData.Rows.Add(iddocente, _docente, _direccion);
        //    }

        //    lblResumen.Text = "Mostrando " + data.Rows.Count.ToString() + "registros de " + db.Count("DOCENTES", "DEL = 'N'").ToString();
        //    data.Dispose();
        //}

       
        private void txtIdCliente_Validating(object sender, CancelEventArgs e)
        {
            string id = a.Clean(txtIdCliente.Text.Trim());
            CheckIfIdExist(id);
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

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            string msg = "¿DESEA ELIMINAR EL REGISTRO SELECCIONADO?";

            if (a.Pregunta(msg) == true)
            {
                string id_cliente = txtIdCliente.Text.Trim();

                if (db.Delete("CLIENTES", "ID_CLIENTE", id_cliente) > 0)
                {
                    a.Aprueba("EL DOCENTE SE ELIMINÓ CON EXITO");
                    Clear();
                    Boot();
                }
            }
        }

        public Frm_Clientes()
        {
            InitializeComponent();
        }

        private void CheckIfIdExist(string id)
        {
            if (txtIdCliente.Text.Trim().Length == 5)
            {
                if (db.VerifyIfExists("CLIENTES", "ID_CLIENTE", id) == true)
                {
                    lblMsg.Text = "EL CODIGO QUE INTENTA ASIGNAR YA EXISTE. PERTENECE A " + db.Hook("NOMBRE", "CLIENTES", "ID_CLIENTE = '" + a.Clean(txtIdCliente.Text.Trim()) + "'");
                    lblMsg.Visible = true;

                }
                else
                {
                    lblMsg.Visible = false;
                }
            }
        }

        private void pbSalir_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Formularios.Frm_Menu menu = new Frm_Menu();
            menu.Show();
            this.Hide();
        }

        private void btnListaClientes_Click(object sender, EventArgs e)
        {
            Formularios.Formularios_de_Menu.Clientes.Frm_Lista_Clientes form = new Formularios_de_Menu.Clientes.Frm_Lista_Clientes();
            this.AddOwnedForm(form);
            form.Show();
        }

        private void Frm_Clientes_Load(object sender, EventArgs e)
        {
            Boot();
        }
                
    }
}
