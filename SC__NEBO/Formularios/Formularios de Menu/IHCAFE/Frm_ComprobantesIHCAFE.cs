using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SC__NEBO.Formularios.Formularios_de_Menu.IHCAFE
{
    public partial class Frm_ComprobantesIHCAFE : Form
    {
        Clases.DB db = new Clases.DB();
        Clases.Asistente a = new Clases.Asistente();
        public Frm_ComprobantesIHCAFE()
        {
            InitializeComponent();
        }

        private void btnListaClientes_Click(object sender, EventArgs e)
        {

        }

        private void Frm_ComprobantesIHCAFE_Load(object sender, EventArgs e)
        {
            this.Text = Clases.Env.APPNAME + " | COMPROBANTES DEL IHCAFE | " + Clases.Auth.user + " | " + Clases.Auth.rol;
            Boot();
        }

        string id_cosecha;
        public void Cosecha()
        {
            string condicion = "ESTADO='ACTIVO'";
            TxtCosecha.Text = db.Hook("COSECHA", "COSECHAS", condicion);
            id_cosecha = db.Hook("ID_COSECHA", "COSECHAS", condicion);
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            btnNuevo.Enabled = false;
            btnGuardar.Enabled = Clases.Auth.save == "S" ? true : false;
            btnEliminar.Enabled = false;
            btnCancelar.Enabled = true;
            btnBuscar_Cliente.Enabled = true;
            btn_Buscar_Finca.Enabled = true;


            txtIdCliente.Enabled = false;
            txtCliente.Enabled = false;
            txtClave.Enabled = false;
            txtRTN.Enabled = false;
            txtDomicilio.Enabled = false;
            txtTelefono.Enabled = false;
            txtMunicipio.Enabled = false;
            txtDepto.Enabled = false;

            txtIdfinca.Enabled = false;
            txtFinca.Enabled = false;
            txtUbicac_Finca.Enabled = false;
            txtMunicipio_Finca.Enabled = false;
            txtDepto_Finca.Enabled = false;
            TxtCosecha.Enabled = false;


            dtpFecha_Compra.Enabled = true;
            Cosecha();
            NumNotaPeso();
        }

        string idcorre = "IHCAF";

        private void NumNotaPeso()
        {
            Int32 numcomp, correlativo;
            correlativo = Convert.ToInt32(db.Hook("ULTIMO", "CORRELATIVOS", "IDCORRE='" + idcorre + "'"));
            numcomp = correlativo + 1;

            LblNumeroComprobante.Text = numcomp.ToString();
        }

        private void Boot()
        {

            btnNuevo.Enabled = Clases.Auth.save == "S" ? true : false;
            btnGuardar.Enabled = false;
            //btnActualizar.Enabled = false;
            btnEliminar.Enabled = true;
            btnCancelar.Enabled = false;
            pbSalir.Enabled = true;
            btnBuscar_Cliente.Enabled = false;
            btn_Buscar_Finca.Enabled = false;


            txtIdCliente.Enabled = false;
            txtCliente.Enabled = false;
            txtClave.Enabled = false;
            txtRTN.Enabled = false;
            txtDomicilio.Enabled = false;
            txtTelefono.Enabled = false;
            txtMunicipio.Enabled = false;
            txtDepto.Enabled = false;

            txtIdfinca.Enabled = false; 
            txtFinca.Enabled = false;
            txtUbicac_Finca.Enabled = false;
            txtMunicipio_Finca.Enabled = false;
            txtDepto_Finca.Enabled = false;
            TxtCosecha.Enabled = false;
            dtpFecha_Compra.Enabled = false;


        }

        private void btnBuscar_Cliente_Click(object sender, EventArgs e)
        {
            Formularios.Formularios_de_Menu.IHCAFE.Frm_Lista_Socios ListaSocios = new Formularios.Formularios_de_Menu.IHCAFE.Frm_Lista_Socios();
            this.AddOwnedForm(ListaSocios);
            ListaSocios.Show();
        }

        //Recibe la informacion del socio
        public void CodSocio(string cod_socio)
        {
            string condicion = "ID_SOCIO='" + cod_socio + "'";
            txtIdCliente.Text = cod_socio;
            txtCliente.Text = db.Hook("NOMBRE", "SOCIOS", condicion);
            txtRTN.Text = db.Hook("RTN", "SOCIOS", condicion);
            txtDomicilio.Text = db.Hook("DIRECCION", "SOCIOS", condicion);
            txtDepto.Text = db.Hook("DEPARTAMENTO", "SOCIOS", condicion);
            txtMunicipio.Text = db.Hook("MUNICIPIO", "SOCIOS", condicion);
            txtTelefono.Text = db.Hook("TELEFONO", "SOCIOS", condicion);
            txtClave.Text = db.Hook("CLAVE_IHCAFE", "SOCIOS", condicion);

        }

        private void btn_Buscar_Finca_Click(object sender, EventArgs e)
        {
            string _codsocio, _socio;
            _codsocio = txtIdCliente.Text.ToString();
            _socio = txtCliente.Text.ToString();

            Formularios.Formularios_de_Menu.IHCAFE.ListaFinca_Ihcafe form = new Formularios.Formularios_de_Menu.IHCAFE.ListaFinca_Ihcafe();
            this.AddOwnedForm(form);
            form.idsocio = _codsocio;
            form.socio = _socio;
            form.ShowDialog();
        }

        //Recibe la informacion de la finca 

        public void CodFinca (string cod_finca)
        {
            string condicion = "IDFINCA='"+cod_finca+"'";
            txtIdfinca.Text = cod_finca;
            txtFinca.Text = db.Hook("NOMBREFINCA", "FINCAS", condicion);
            txtUbicac_Finca.Text = db.Hook("UBICACION", "FINCAS", condicion);
            txtMunicipio_Finca.Text = db.Hook("MUNICIPIO", "FINCAS", condicion);
            txtDepto_Finca.Text = db.Hook("DEPARTAMENTO", "FINCAS", condicion);

        }

        private void btnBuscarNotaPeso_Click(object sender, EventArgs e)
        {
            Formularios.Formularios_de_Menu.IHCAFE.Frm_Lista_Socios_Adicionales adicionales = new Formularios.Formularios_de_Menu.IHCAFE.Frm_Lista_Socios_Adicionales();
            adicionales.ShowDialog();
        }

    }
}
