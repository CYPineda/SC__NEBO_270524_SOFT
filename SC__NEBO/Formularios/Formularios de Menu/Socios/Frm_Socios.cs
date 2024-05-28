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
    public partial class Frm_Socios : Form
    {
        Clases.DB db = new Clases.DB();
        Clases.Asistente h = new Clases.Asistente();

        string codigo, dni, rtn, nombre, correo, municipio, departamento, direccion, tel, estado_civil, tipo_socio, clave_ihcafe, no_cuenta, id_finca, nombrefinca, direccion_finca, cantmanz, areaprod;
        string municipio_finca, deptofinca;

        private void cbxRegistrarfinca_CheckedChanged(object sender, EventArgs e)
        {
            if (cbxRegistrarfinca.Checked == true)
            {
                TxtNombreFinca.Enabled = true;
                TxtDireccion_Finca.Enabled = true;
                TxtCantMzn.Enabled = true;
                TxtAreaProd.Enabled = true;
                TxtMunicipioFinca.Enabled = true;
                TxtDeptoFinca.Enabled = true;
            }
            else
            {
                TxtNombreFinca.Enabled = false;
                TxtDireccion_Finca.Enabled = false;
                TxtCantMzn.Enabled = false;
                TxtAreaProd.Enabled = false;
                TxtMunicipioFinca.Enabled = false;
                TxtDeptoFinca.Enabled = false;
            }
        }

        private void BtnCancelar_Click(object sender, EventArgs e)
        {

            string msg = "¿DESEA CANCELAR LA ACCION?";

            if (h.Pregunta(msg) == true)
            {
                Clear();
                Boot();
            }
        }

        private void BtnActualizar_Click(object sender, EventArgs e)
        {
            ValidateData();

            if (errors == 0)
            {
                string msg = "DESEA GUARDAR LOS CAMBIOS DEL REGISTRO SELECCIONADO";
                if (h.Pregunta(msg) == true)
                {
                    string id_socio = TxtIdSocio.Text.Trim();
                    string stmt = "NOMBRE='" + nombre + "',DNI='" + dni + "',RTN='" + rtn + "'" +
                        ",TIPO_SOCIO_ID='" + tipo_socio + "',ESTADO_CIVIL='" + estado_civil + "',TELEFONO='" + tel + "' " +
                        ",MUNICIPIO='" + municipio + "',DEPARTAMENTO='" + departamento + "',DIRECCION='" + direccion + "',CLAVE_IHCAFE='" + clave_ihcafe + "'" +
                        ",NO_CUENTA='" + no_cuenta + "',CORREO='" + correo + "'";

                    string condicion = "ID_SOCIO='" + id_socio + "'";

                    if (db.Update("SOCIOS", stmt, condicion) > 0)
                    {
                        h.Aprueba("LOS CAMBIOS SE APLICARON CORRECTAMENTE!");
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
                GetInfoSocio(id);
              //GetTipoSocio();
            }
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            string msg = "¿DESEA CANCELAR LA OPERCION EN CURSO?";
            if (h.Pregunta(msg) == true)
            {
                DgvData.Rows.Clear();
                Clear();
                Boot();
            }
        }

        private void btnRegresar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void BtnImprimirProductos_Click(object sender, EventArgs e)
        {

            if (DgvData.Rows.Count > 0)
            {
                string codSocio = DgvData.CurrentRow.Cells[0].Value.ToString();
                Reportes.FrmRptInfoSocio infoSocio = new Reportes.FrmRptInfoSocio();
                infoSocio.codsocio = codSocio;
                infoSocio.Show();
            }
        }

        private void TxtNombre_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnListadoSocios_Click(object sender, EventArgs e)
        {
            Reportes.FrmRptListadoSocios lista = new Reportes.FrmRptListadoSocios();
            lista.Show();
        }

        private void BtnEliminar_Click(object sender, EventArgs e)
        {
            string msg = "¿DESA ELMINAR EL REGISTRO SELECCIONADO?";

            if (h.Pregunta(msg) == true)
            {
                string idsocio = TxtIdSocio.Text.Trim();
                if (db.Delete("SOCIOS", "ID_SOCIO", idsocio) > 0)
                {
                    h.Aprueba("EL CLIENTE SE ELIMINO CON EXITO!");
                    Clear();
                    Boot();
                }
            }
        }

        private void pbSalir_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnContratos_Click(object sender, EventArgs e)
        {
            if (DgvData.Rows.Count > 0)
            {
                string _idsocio, _socio;
                _idsocio = DgvData.CurrentRow.Cells[0].Value.ToString();
                _socio = DgvData.CurrentRow.Cells[1].Value.ToString();

                Formularios.Formularios_de_Menu.Contratos.FrmContratoxSocio form = new Contratos.FrmContratoxSocio();
                this.AddOwnedForm(form);
                form.idsocio = _idsocio;
                form.socio = _socio;

                form.ShowDialog();
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
            string search = h.Clean(TxtBuscar.Text.Trim());
            GetFincas(search);
        }

        private void GetFincas(string search = "")
        {
            //CAMBIAR JOINSQL
            string joinsql = " ID_SOCIO, NOMBRE FROM SOCIOS ";

            string condicion;

            if (search != "")
            {
                condicion = " NOMBRE LIKE '%" + search + "%' AND DEL ='N' ";
            }
            else
            {
                 condicion = "DEL='N'";
            }

            DataTable data = db.Join(joinsql, condicion, "NOMBRE");

            DgvData.Rows.Clear();

            string _idsocio, _socio;
            int i;

            for (i = 0; i < data.Rows.Count; i++)
            {
                _idsocio = data.Rows[i][0].ToString();
                _socio = data.Rows[i][1].ToString();


                DgvData.Rows.Add(_idsocio, _socio);

            }
            LblResumen.Text = "MOSTRANDO " + data.Rows.Count.ToString() + " DE " + db.Count("SOCIOS", "DEL='N'").ToString() + " REGISTROS. ";
            data.Dispose();

        }

        private void BtnFinca_Click(object sender, EventArgs e)
        {
            if (DgvData.Rows.Count > 0)
            {
                string _idsocio, _socio;
                _idsocio = DgvData.CurrentRow.Cells[0].Value.ToString();
                _socio = DgvData.CurrentRow.Cells[1].Value.ToString();

                Formularios.Formularios_de_Menu.Fincas.Frm_Fincas form = new Fincas.Frm_Fincas();
                this.AddOwnedForm(form);
                form.idsocio = _idsocio;
                form.socio = _socio;

                form.ShowDialog();
            }

        }

        private void BtnGuardar_Click(object sender, EventArgs e)
        {
            ValidateData();
            string msg = "¿DESEA REGISTRAR EN LA BASE DE DATOS LA INFORMACIÓN DEL SOCIO " + nombre + "?";

            if (errors == 0)
            {
                if (h.Pregunta(msg) == true)
                {
                    string campos = "ID_SOCIO, NOMBRE,DNI, RTN, TIPO_SOCIO_ID,  ESTADO_CIVIL, TELEFONO,MUNICIPIO, DEPARTAMENTO,DIRECCION, CLAVE_IHCAFE, NO_CUENTA, CORREO ";
                    string valores = "'" + codigo + "', '" + nombre + "','" + dni + "', '" + rtn + "', '" + tipo_socio + "', '" + estado_civil + "','" + tel + "','" + municipio + "','" + departamento + "','" + direccion + "', '" + clave_ihcafe + "', '" + no_cuenta + "', '" + correo + "'";

                    if (db.Save("SOCIOS", campos, valores) > 0)
                    {
                        h.Aprueba("¡EL SOCIO " + nombre + " SE REGISTRÓ CON ÉXITO!");

                        db.SetLast("SOCIO");

                        Finca();
                        Clear();
                        Boot();

                    }
                }
            }
        }

        int errors;

        public Frm_Socios()
        {
            InitializeComponent();
        }

        private void Frm_Socios_Load(object sender, EventArgs e)
        {
            this.Text = Clases.Env.APPNAME + " | REGISTRO DE SOCIOS | " + Clases.Auth.user + " | " + Clases.Auth.rol;
            Boot();
        }

        private void Boot()
        {
            BtnNuevo.Enabled = Clases.Auth.save == "S" ? true : false;
            BtnGuardar.Enabled = false;
            BtnActualizar.Enabled = false;
            BtnEliminar.Enabled = false;
            btnListadoSocios.Enabled = true;
            BtnCancelar.Enabled = false;
            //pbSalir.Enabled = true;

            //ChkAutoGen.Enabled = false;

            TxtIdSocio.Enabled = false;
            TxtNombre.Enabled = false;
            TxtDNI.Enabled = false;
            TxtRTN.Enabled = false;
            CmbEstadoCivil.Enabled = false;
            cmbTipoSocio.Enabled = false;
            TxtTelefono.Enabled = false;
            TxtMunicipio.Enabled = false;
            TxtDepto.Enabled = false;
            TxtDireccion.Enabled = false;
            TxtClaveIHCAFE.Enabled = false;
            TxtNo_Cuenta.Enabled = false;
            txtCorreo.Enabled = false;

            TxtNombreFinca.Enabled = false;
            TxtDireccion_Finca.Enabled = false;
            TxtCantMzn.Enabled = false;
            TxtAreaProd.Enabled = false;
            TxtMunicipioFinca.Enabled = false;
            TxtDeptoFinca.Enabled = false;
            cbxRegistrarfinca.Enabled = false;
            btnContratos.Enabled = Clases.Auth.update == "S" ? true : false;
            BtnFinca.Enabled = Clases.Auth.save == "S" ? true : false;

            //DgvData.Rows.Clear();
            TxtBuscar.Clear();
            TxtBuscar.Focus();
            DgvData.Rows.Clear();
            GetTipoSocio();
        }

        private void Finca()
        {
            if (cbxRegistrarfinca.Checked == true)
            {
                string camposLO = "IDFINCA, NOMBREFINCA, IDSOCIO, UBICACION, DEPARTAMENTO, MUNICIPIO, CANTMANZ, AREAPROD";
                string valoresLO = "'" + id_finca + "', '" + nombrefinca + "', '" + codigo + "','" + direccion_finca + "', " +
                    "'" + deptofinca + "', '" + municipio_finca + "', " + cantmanz + ", " + areaprod + "";

                if (db.Save("FINCAS", camposLO, valoresLO) > 0)
                {
                    h.Aprueba("¡LA FINCA DE " + nombre + " SE REGISTRÓ CON ÉXITO!");

                    db.SetLast("FINCA");
                    Clear();
                    Boot();
                }
            }
        }

        private void Clear()
        {
            TxtIdSocio.Clear();
            TxtNombre.Clear();
            TxtDNI.Clear();
            TxtRTN.Clear();
            CmbEstadoCivil.SelectedIndex = -1;
            cmbTipoSocio.SelectedIndex = -1;
            TxtTelefono.Clear();
            TxtMunicipio.Clear();
            TxtDepto.Clear();
            TxtDireccion.Clear();
            TxtClaveIHCAFE.Clear();
            TxtNo_Cuenta.Clear();
            txtCorreo.Clear();

            //no se si se pone
            TxtCodFinca.Clear();


            TxtNombreFinca.Clear();
            TxtDireccion_Finca.Clear();
            TxtCantMzn.Clear();
            TxtAreaProd.Clear();
            TxtDeptoFinca.Clear();
            TxtMunicipioFinca.Clear();
            cbxRegistrarfinca.Checked = false;

        }

        private void BtnNuevo_Click(object sender, EventArgs e)
        {
            BtnNuevo.Enabled = false;
            BtnGuardar.Enabled = Clases.Auth.save == "S" ? true : false;
            BtnActualizar.Enabled = false;
            BtnEliminar.Enabled = false;
            BtnCancelar.Enabled = true;
            btnListadoSocios.Enabled = false;
            //pbSalir.Enabled = false;
            btnContratos.Enabled = Clases.Auth.update == "S" ? true : false;
            TxtIdSocio.Text = "SOC" + db.GetNext("SOCIO");

            TxtIdSocio.Enabled = false;
            TxtNombre.Enabled = true;
            TxtDNI.Enabled = true;
            TxtRTN.Enabled = true;
            CmbEstadoCivil.Enabled = true;
            cmbTipoSocio.Enabled = true;
            TxtTelefono.Enabled = true;
            TxtMunicipio.Enabled = true;
            TxtDepto.Enabled = true;
            TxtDireccion.Enabled = true;
            TxtClaveIHCAFE.Enabled = true;
            TxtNo_Cuenta.Enabled = true;
            txtCorreo.Enabled = true;
            cbxRegistrarfinca.Enabled = true;

            TxtCodFinca.Text = "FNC" + db.GetNext("FINCA");

            TxtNombreFinca.Enabled = false;
            TxtDireccion_Finca.Enabled = false;
            TxtCantMzn.Enabled = false;
            TxtAreaProd.Enabled = false;
            GetTipoSocio();
        }


        private void ValidateData()
        {
            errors = 0;
            codigo = h.Clean(TxtIdSocio.Text.Trim());
            nombre = h.Clean(TxtNombre.Text.Trim());
            dni = TxtDNI.Text.Trim();
            rtn = h.Clean(TxtRTN.Text.Trim());
            estado_civil = CmbEstadoCivil.Text.Trim();
            tipo_socio = cmbTipoSocio.Text != "" ? cmbTipoSocio.SelectedValue.ToString() : "";
            tel = TxtTelefono.Text.Trim();
            municipio = h.Clean(TxtMunicipio.Text.Trim());
            departamento = h.Clean(TxtDepto.Text.Trim());
            direccion = h.Clean(TxtDireccion.Text.Trim());
            clave_ihcafe = h.Clean(TxtClaveIHCAFE.Text.Trim());
            no_cuenta = h.Clean(TxtNo_Cuenta.Text.Trim());
            correo = h.Clean(txtCorreo.Text.Trim());

            //no se si se pone
            id_finca = h.Clean(TxtCodFinca.Text.Trim());


            nombrefinca = h.Clean(TxtNombreFinca.Text.Trim());
            direccion_finca = h.Clean(TxtDireccion_Finca.Text.Trim());
            cantmanz = h.Clean(TxtCantMzn.Text.Trim());
            areaprod = h.Clean(TxtAreaProd.Text.Trim());

            deptofinca = h.Clean(TxtDeptoFinca.Text.Trim());
            municipio_finca = h.Clean(TxtMunicipioFinca.Text.Trim());


            if (codigo.Length == 0)
            {
                h.Pregunta("¡INGRESAR UN CÓDIGO VÁLIDO!");
                TxtIdSocio.Text = codigo;
                TxtIdSocio.Focus();
                errors++;
                return;
            }
            if (nombre.Length == 0)
            {
                h.Advertencia("¡INGRESAR EL NOMBRE DEL SOCIO!");
                TxtNombre.Text = nombre;
                TxtNombre.Focus();
                errors++;
            }

            if (dni.Length == 0)
            {
                h.Advertencia("¡INGRESAR EL DNI DEL SOCIO!");
                TxtDNI.Text = dni;
                TxtDNI.Focus();
                errors++;
            }

            //if (rtn.Length == 0)
            //{
            //    h.Advertencia("¡INGRESAR EL RTN DEL SOCIO!");
            //    TxtRTN.Text = rtn;
            //    TxtRTN.Focus();
            //    errors++;
            //}

            if (estado_civil.Length == 0)
            {
                h.Advertencia("¡SELECCIONE EL ESTADO CIVIL DEL SOCIO!");
                CmbEstadoCivil.Text = estado_civil;
                CmbEstadoCivil.Focus();
                errors++;
            }

            if (tipo_socio.Length == 0)
            {
                h.Advertencia("¡SELECCIONE EL TIPO DE SOCIO!");
                cmbTipoSocio.Text = tipo_socio;
                cmbTipoSocio.Focus();
                errors++;
            }

            if (direccion.Length == 0)
            {
                h.Advertencia("¡INGRESAR LA DIRECCIÓN DEL SOCIO!");
                TxtDireccion.Text = direccion;
                TxtDireccion.Focus();
                errors++;
            }

       

            //if (clave_ihcafe.Length == 0)
            //{
            //    h.Advertencia("¡INGRESAR LA CLAVE DE IHCAFE!");
            //    TxtClaveIHCAFE.Text = clave_ihcafe;
            //    TxtClaveIHCAFE.Focus();
            //    errors++;
            //}

            if (TxtTelefono.MaskFull == false)
            {
                tel="0000-0000";
                TxtTelefono.Text = tel;
            }


            if (municipio.Length == 0)
            {
                h.Advertencia("¡INGRESAR EL MUNICIPIO!");
                TxtMunicipio.Text =municipio;
                TxtMunicipio.Focus();
                errors++;
            }

            if (departamento.Length == 0)
            {
                h.Advertencia("¡INGRESAR EL DEPARTAMENTO!");
                TxtDepto.Text = departamento;
                TxtDepto.Focus();
                errors++;
            }
            if (correo.Length == 0)
            {
                correo = "sincorreo@gmail.com";
                txtCorreo.Text = correo;
            }

            if (cbxRegistrarfinca.Checked == true)
            {
                if (nombrefinca.Length == 0)
                {
                    h.Advertencia("¡INGRESE EL NOMBRE DE LA FINCA!");
                    TxtNombreFinca.Text = nombrefinca;
                    TxtNombreFinca.Focus();
                    errors++;
                    return;
                }
                if (cantmanz.Length == 0)
                {
                    h.Advertencia("¡INGRESE EL NOMBRE DE LA FINCA!");
                    TxtCantMzn.Text = cantmanz;
                    TxtCantMzn.Focus();
                    errors++;
                    return;
                }

                if (direccion_finca.Length == 0)
                {
                    h.Advertencia("¡INGRESE EL NOMBRE DE LA FINCA!");
                    TxtDireccion_Finca.Text = direccion_finca;
                    TxtDireccion_Finca.Focus();
                    errors++;
                    return;
                }
                if (areaprod.Length == 0)
                {
                    h.Advertencia("¡INGRESE EL NOMBRE DE LA FINCA!");
                    TxtAreaProd.Text = areaprod;
                    TxtAreaProd.Focus();
                    errors++;
                    return;
                }

                if (municipio_finca.Length == 0)
                {
                    h.Advertencia("¡INGRESE EL MUNICIPIO DE LA FINCA!");
                    TxtMunicipioFinca.Text = municipio_finca;
                    TxtMunicipioFinca.Focus();
                    errors++;
                    return;
                }
                if (deptofinca.Length == 0)
                {
                    h.Advertencia("¡INGRESE EL DEPARTAMENTO DE LA FINCA!");
                    TxtDeptoFinca.Text = deptofinca;
                    TxtDeptoFinca.Focus();
                    errors++;
                    return;
                }
            }


        }

        private void GetTipoSocio()
        {
            cmbTipoSocio.DataSource = db.Find("TIPO_SOCIO", "TIPO_SOCIO_ID,TIPO_SOCIO", "", "TIPO_SOCIO");
            cmbTipoSocio.ValueMember = "TIPO_SOCIO_ID";
            cmbTipoSocio.DisplayMember = "TIPO_SOCIO";
            cmbTipoSocio.SelectedIndex = -1;
        }

        private void GetInfoSocio(string id)
        {
            string condicion = "ID_SOCIO='" + id + "' AND DEL='N'";
            DataTable socio = db.Find("SOCIOS", "*", condicion);

            if (socio.Rows.Count > 0)
            {
                BtnNuevo.Enabled = false;
                BtnGuardar.Enabled = false;
                BtnActualizar.Enabled = Clases.Auth.update == "S" ? true : false;
                BtnEliminar.Enabled = Clases.Auth.delete == "S" ? true : false;
                BtnCancelar.Enabled = true;


                DataRow info = socio.Rows[0];
                TxtIdSocio.Text = info["ID_SOCIO"].ToString();
                TxtNombre.Text = info["NOMBRE"].ToString();
                TxtDNI.Text = info["DNI"].ToString();
                TxtRTN.Text = info["RTN"].ToString();
                cmbTipoSocio.SelectedValue = info["TIPO_SOCIO_ID"].ToString();
                CmbEstadoCivil.Text = info["ESTADO_CIVIL"].ToString();
                TxtTelefono.Text = info["TELEFONO"].ToString();
                TxtMunicipio.Text = info["MUNICIPIO"].ToString();
                TxtDepto.Text = info["DEPARTAMENTO"].ToString();
                TxtDireccion.Text = info["DIRECCION"].ToString();
                TxtClaveIHCAFE.Text = info["CLAVE_IHCAFE"].ToString();
                TxtNo_Cuenta.Text = info["NO_CUENTA"].ToString();
                txtCorreo.Text = info["CORREO"].ToString();

                TxtIdSocio.Enabled = false;
                TxtNombre.Enabled = Clases.Auth.update == "S" ? true : false;
                TxtDNI.Enabled = Clases.Auth.update == "S" ? true : false;
                TxtRTN.Enabled = Clases.Auth.update == "S" ? true : false;
                cmbTipoSocio.Enabled = Clases.Auth.update == "S" ? true : false;
                CmbEstadoCivil.Enabled = Clases.Auth.update == "S" ? true : false;
                TxtTelefono.Enabled = Clases.Auth.update == "S" ? true : false;
                TxtMunicipio.Enabled = Clases.Auth.update == "S" ? true : false;
                TxtDepto.Enabled = Clases.Auth.update == "S" ? true : false;
                TxtDireccion.Enabled = Clases.Auth.update == "S" ? true : false;
                TxtClaveIHCAFE.Enabled = Clases.Auth.update == "S" ? true : false;
                TxtNo_Cuenta.Enabled = Clases.Auth.update == "S" ? true : false;
                txtCorreo.Enabled = Clases.Auth.update == "S" ? true : false;
            }
            else
            {
                h.Advertencia("ERROR AL RECUPERAR LOS DATOS DEL REGISTRO SELECCIONADO");

            }
            socio.Dispose();
        }
    }
}
