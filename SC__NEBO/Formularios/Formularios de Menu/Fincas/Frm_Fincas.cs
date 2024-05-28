using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Runtime.InteropServices;

namespace SC__NEBO.Formularios.Formularios_de_Menu.Fincas
{
    public partial class Frm_Fincas : Form
    {
        Clases.DB db = new Clases.DB();
        Clases.Asistente a = new Clases.Asistente();

        public string idsocio, socio;

        int errors;

        string cod_finca, nombre, direccion, cant_mnz, area_prod, municipio, depto;

        public Frm_Fincas()
        {
            InitializeComponent();
        }

        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();

        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);

        private void pbSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Frm_Fincas_Load(object sender, EventArgs e)
        {
            txtIdSocio.Text = idsocio;
            txtNomb_Socio.Text = socio;

            string finca = a.Clean(txtIdSocio.Text.Trim());

            GetFincas(finca);
            Boot();
        }

        private void btnRegistrarFinca_Click(object sender, EventArgs e)
        {
            Formularios.Formularios_de_Menu.Fincas.Frm_Registro_Finca form = new Frm_Registro_Finca();
            this.AddOwnedForm(form);

            form.id_socio = idsocio;
            form.socio = socio;

            form.ShowDialog();
        }

        private void pbSalir_Click_1(object sender, EventArgs e)
        {
            Close();
        }

        private void panel2_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void GetInfoFinca(string id)
        {
            string condicion = "NOMBREFINCA='" + id + "' AND DEL='N'";
            DataTable finca = db.Find("FINCAS", "*", condicion);

            if (finca.Rows.Count > 0)
            {
                btnNuevo.Enabled = false;
                btnGuardar.Enabled = false;
                btnActualizar.Enabled = Clases.Auth.update == "S" ? true : false;
                btnEliminar.Enabled = Clases.Auth.delete == "S" ? true : false;
                btnCancelar.Enabled = true;


                DataRow info = finca.Rows[0];
                TxtCodFinca.Text = info["IDFINCA"].ToString();
                TxtNombreFinca.Text = info["NOMBREFINCA"].ToString();
                TxtDireccion_Finca.Text = info["UBICACION"].ToString();
                TxtCantMzn.Text = info["CANTMANZ"].ToString();
                TxtAreaProd.Text = info["AREAPROD"].ToString();
                txtDepartamento.Text = info["DEPARTAMENTO"].ToString();
                txtMunicipio.Text = info["MUNICIPIO"].ToString();

                TxtCodFinca.Enabled = false;
                TxtNombreFinca.Enabled = Clases.Auth.update == "S" ? true : false;
                TxtDireccion_Finca.Enabled = Clases.Auth.update == "S" ? true : false;
                TxtCantMzn.Enabled = Clases.Auth.update == "S" ? true : false;
                TxtAreaProd.Enabled = Clases.Auth.update == "S" ? true : false;
                txtDepartamento.Enabled = Clases.Auth.update == "S" ? true : false;
                txtMunicipio.Enabled = Clases.Auth.update == "S" ? true : false;
            }
            else
            {
                a.Advertencia("ERROR AL RECUPERAR LOS DATOS DEL REGISTRO SELECCIONADO");

            }
            finca.Dispose();
        }


        private void btnGuardar_Click(object sender, EventArgs e)
        {
            ValidateData();

            string msg = "¿DESEA GUARDAR ESTA FINCA AL SOCIO " + socio + "?";

            if (errors == 0)
            {
                if (a.Pregunta(msg) == true)
                {
                    //No se si ya se creo esta tabla
                    string campos = "IDFINCA, NOMBREFINCA, IDSOCIO, UBICACION, DEPARTAMENTO, MUNICIPIO, CANTMANZ, AREAPROD";

                    string valores = "'" + cod_finca + "', '" + nombre + "', '" + idsocio + "','" + direccion + "', " +
                        "'" + depto + "','" + municipio+ "','" + cant_mnz + "','" + area_prod + "'";


                    if (db.Save("FINCAS", campos, valores) > 0)
                    {
                        //db.RawSQL(query);
                        db.SetLast("FINCA");
                        a.Aprueba("LA FINCA DEL SOCIO " + socio + " HA SIDO REGISTRADO CON ÉXITO!");
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
                    string id_inca = TxtCodFinca.Text.Trim();
                    string stmt = "NOMBREFINCA='" + nombre + "',IDSOCIO='" + idsocio + "',UBICACION='" + direccion + "'" +
                        ",DEPARTAMENTO='" + depto + "',MUNICIPIO='" + municipio + "',CANTMANZ='" + cant_mnz + "' " +
                        ",AREAPROD='" + area_prod + "'";

                    string condicion = "IDSOCIO='" + idsocio + "'";

                    if (db.Update("FINCAS", stmt, condicion) > 0)
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
                string id_finca = TxtCodFinca.Text.Trim();

                if (db.Delete("FINCAS", "IDFINCA", id_finca) > 0)
                {
                    a.Aprueba("LA FINCA SE ELIMINÓ CON EXITO");
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

        private void DgvData_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (DgvData.Rows.Count > 0)
            {
                string id = DgvData.CurrentRow.Cells[0].Value.ToString();
                GetInfoFinca(id);
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

            TxtCodFinca.Text = "FNC" + db.GetNext("FINCA");

            TxtNombreFinca.Enabled = true;
            TxtDireccion_Finca.Enabled = true;
            TxtCantMzn.Enabled = true;
            TxtAreaProd.Enabled = true;
            txtDepartamento.Enabled = true;
            txtMunicipio.Enabled = true;

            TxtNombreFinca.Focus();
        }

        public void GetFincas(string finca)
        {
            string condicion = "IDSOCIO='" + finca + "' AND DEL='N'";

            DataTable reporte = db.Find("FINCAS", "NOMBREFINCA, CANTMANZ, UBICACION", condicion, "IDFINCA");

            DgvData.Rows.Clear();

            string _nombre, _cantmanz, _ubicacion;

            double total = 0;
            int i;

            for (i = 0; i < reporte.Rows.Count; i++)
            {
                _nombre = reporte.Rows[i][0].ToString();
                _cantmanz = reporte.Rows[i][1].ToString();
                _ubicacion = reporte.Rows[i][2].ToString();

                DgvData.Rows.Add(_nombre, _cantmanz, _ubicacion);
            }

            reporte.Dispose();

            //LblToTotal.Text = total.ToString();
        }

        private void Clear()
        {
            TxtCodFinca.Clear();
            TxtNombreFinca.Clear();
            TxtDireccion_Finca.Clear();
            TxtCantMzn.Clear();
            TxtAreaProd.Clear();
            txtDepartamento.Clear();
            txtMunicipio.Clear();
        }

        public void ValidateData()
        {
            errors = 0;
            cod_finca = a.Clean(TxtCodFinca.Text.Trim());
            nombre = a.Clean(TxtNombreFinca.Text.Trim());
            direccion = a.Clean(TxtDireccion_Finca.Text.Trim());
            cant_mnz = a.Clean(TxtCantMzn.Text.Trim());
            area_prod = a.Clean(TxtAreaProd.Text.Trim());
            depto = a.Clean(txtDepartamento.Text.Trim());
            municipio = a.Clean(txtMunicipio.Text.Trim());

            if (cod_finca.Length == 0)
            {
                a.Advertencia("¡EL CODIGO DE LA FINCA ES REQUERIDO!");
                TxtCodFinca.Text = "";
                TxtCodFinca.Focus();
                errors++;
                return;
            }

            if (nombre.Length == 0)
            {
                a.Advertencia("¡EL NOMBRE DE LA FINCA ES REQUERIDO!");
                TxtNombreFinca.Text = "";
                TxtNombreFinca.Focus();
                errors++;
                return;
            }

            if (direccion.Length == 0)
            {
                a.Advertencia("¡LA DIRECCIÓN DE LA FINCA ES REQUERIDA!");
                TxtDireccion_Finca.Text = "";
                TxtDireccion_Finca.Focus();
                errors++;
                return;
            }

            if (cant_mnz.Length == 0)
            {
                a.Advertencia("¡LA CANTIDAD DE MANZANAS SON REQUERIDAS!");
                TxtCantMzn.Text = "";
                TxtCantMzn.Focus();
                errors++;
            }


            if (area_prod.Length == 0)
            {
                a.Advertencia("¡EL AREA DE PRODUCCIÓN ES REQUERIDA!");
                TxtAreaProd.Text = "";
                TxtAreaProd.Focus();
                errors++;
            }

            if (depto.Length == 0)
            {
                a.Advertencia("¡EL DEPARTAMENTO ES REQUERIDO!");
                txtDepartamento.Text = "";
                txtDepartamento.Focus();
                errors++;
            }


            if (municipio.Length == 0)
            {
                a.Advertencia("¡EL MUNICIPIO ES REQUERIDO!");
                txtMunicipio.Text = "";
                txtMunicipio.Focus();
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
            //pbSalir.Enabled = true;

            TxtCodFinca.Enabled = false;
            TxtNombreFinca.Enabled = false;
            TxtDireccion_Finca.Enabled = false;
            TxtCantMzn.Enabled = false;
            TxtAreaProd.Enabled = false;
            txtDepartamento.Enabled = false;
            txtMunicipio.Enabled = false;

            //lblMsg.Visible = false;

            //DgvData.Rows.Clear();
            //txtBuscar.Clear();
            //txtBuscar.Focus();

        }
    }
}
