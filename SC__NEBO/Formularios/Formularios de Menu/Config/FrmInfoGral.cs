using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SC__NEBO.Formularios.Formularios_de_Menu.Config
{
    public partial class FrmInfoGral : Form
    {

        Clases.DB db = new Clases.DB();
        Clases.Asistente h = new Clases.Asistente();

        string rtn, nombreorazon, eslogan, direccion, ciudad, telefonos, correo, website, redes, logo;

        private void pbSalir_Click(object sender, EventArgs e)
        {
            Close();
        }

        int errors;


        private void btnGuardar_Click(object sender, EventArgs e)
        {
            ValidateData();
            if (errors == 0)
            {
                string campos = "RTN, NOMBREORAZONSOCIAL, ESLOGAN, DIRECCION,CIUDAD, " +
                    "TELEFONOS,CORREO,WEBSITE,REDES_SOC,LOGO";
                string valores = "'" + rtn + "', '" + nombreorazon + "','" + eslogan + "','" + direccion + "'," +
                    "'" + ciudad + "','" + telefonos + "','" + correo + "','" + website + "','" + redes + "','" + logo + "'";

                if (db.Save("INFOGRAL", campos, valores) > 0)
                {
                    h.Aprueba("¡LOS DATOS SE REGISTRARON CON ÉXITO!");
                    CheckInfoGral();
                }

            }
        }

        public FrmInfoGral()
        {
            InitializeComponent();
        }

        private void FrmInfoGral_Load(object sender, EventArgs e)
        {
            CheckInfoGral();
        }


        private void CheckInfoGral()
        {
            DataTable data = db.Find("INFOGRAL", "*");
            if (data.Rows.Count > 0)
            {
                btnGuardar.Enabled = false;
                btnActualizar.Enabled = true;
                btnEliminar.Enabled = true;
                btnCancelar.Enabled = true;
                pbSalir.Enabled = true;

                DataRow info = data.Rows[0];
                txtRtn.Text = info["RTN"].ToString();
                txtNombreORazon.Text = info["NOMBREORAZONSOCIAL"].ToString();
                txtEslogan.Text = info["ESLOGAN"].ToString();
                txtDireccion.Text = info["DIRECCION"].ToString();

                txtCiudad.Text = info["CIUDAD"].ToString();
                txtTelefono.Text = info["TELEFONOS"].ToString();
                txtCorreo.Text = info["CORREO"].ToString();
                txtSitioWeb.Text = info["WEBSITE"].ToString();
                txtRedesSociales.Text = info["REDES_SOC"].ToString();
                txtLogo.Text = info["LOGO"].ToString();
            }
            else
            {
                btnGuardar.Enabled = true;
                btnActualizar.Enabled = false;
                btnEliminar.Enabled = false;
                btnCancelar.Enabled = true;
                pbSalir.Enabled = true;
            }

        }


        private void ValidateData()
        {
            errors = 0;
            rtn = txtRtn.Text.Trim();
            nombreorazon = h.Clean(txtNombreORazon.Text.Trim());
            eslogan = h.Clean(txtEslogan.Text.Trim());
            direccion = h.Clean(txtDireccion.Text.Trim());
            ciudad = h.Clean(txtCiudad.Text.Trim());
            telefonos = h.Clean(txtTelefono.Text.Trim()); ;
            correo = h.Clean(txtCorreo.Text.Trim()); ;
            website = h.Clean(txtSitioWeb.Text.Trim());
            redes = h.Clean(txtRedesSociales.Text.Trim());
            logo = txtLogo.Text.Trim();

            if (rtn.Length < 14)
            {
                h.Advertencia("¡INGRESAR UN NO RTN VÁLIDO!");
                txtRtn.Text = rtn;
                txtRtn.Focus();
                errors++;
                return;
            }
            if (nombreorazon.Length == 0)
            {
                h.Advertencia("¡EL NOMBRE O RAZÓN SOCIAL ES REQUERIDO!");
                txtNombreORazon.Text = nombreorazon;
                txtNombreORazon.Focus();
                errors++;
                return;
            }
            if (eslogan.Length == 0)
            {
                h.Advertencia("¡EL ESLOGAN ES REQUERIDO!");
                txtEslogan.Text = eslogan;
                txtEslogan.Focus();
                errors++;
                return;
            }
            if (direccion.Length == 0)
            {
                h.Advertencia("¡LA DIRECCIÓN ES REQUERIDA!");
                txtDireccion.Text = direccion;
                txtDireccion.Focus();
                errors++;
                return;
            }

            if (ciudad.Length == 0)
            {
                h.Advertencia("¡LA CIUDAD ES REQUERIDA!");
                txtCiudad.Text = ciudad;
                txtCiudad.Focus();
                errors++;
                return;
            }
            if (telefonos.Length == 0)
            {
                h.Advertencia("¡EL TELEFONO ES REQUERIDO!");
                txtTelefono.Text = telefonos;
                txtTelefono.Focus();
                errors++;
                return;
            }
            if (correo.Length == 0)
            {
                h.Advertencia("¡EL CORREO ES REQUERIDO!");
                txtCorreo.Text = correo;
                txtCorreo.Focus();
                errors++;
                return;
            }
            if (website.Length == 0)
            {
                h.Advertencia("¡EL SITIO WEB ES REQUERIDO!");
                txtSitioWeb.Text = website;
                txtSitioWeb.Focus();
                errors++;
                return;
            }
            if (redes.Length == 0)
            {
                h.Advertencia("¡LA RED SOCIAL ES REQUERIDA!");
                txtRedesSociales.Text = redes;
                txtRedesSociales.Focus();
                errors++;
                return;
            }
        }
    }
}
