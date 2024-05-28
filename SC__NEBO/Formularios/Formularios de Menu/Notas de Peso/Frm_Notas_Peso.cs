using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SC__NEBO.Formularios.Formularios_de_Menu.Notas_de_Peso
{
    public partial class Frm_Notas_Peso : Form
    {

        Clases.DB db = new Clases.DB();
        Clases.Asistente a = new Clases.Asistente();
        Clases.Env env = new Clases.Env();
        int errors;

        string cosecha, direccion, fecha, id_cliente, cliente, id_nota, bodega, fletes, costoxflete, costoxbeneficio, idfinca, fincas, direccionfinca, estadocafe, recepcioncafe, observaciones;

        string peso_bruto, sacos, taraxsaco, desc_tara_lbs, total_lbs;
        string desc_humedo, lbs_netas, qq_netos;

        private void btnBuscarCliente_Click(object sender, EventArgs e)
        {
            Formularios.Formularios_de_Menu.Clientes.Frm_ListaClientes_NotaPeso form = new Clientes.Frm_ListaClientes_NotaPeso();
            this.AddOwnedForm(form);
            form.Show();
        }

        private void txtTaraxSaco_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Calculo_TotalLibras();
            }
        }

        private void btnBuscarFinca_Click(object sender, EventArgs e)
        {
            string _idsocio, _socio;
            _idsocio = txtIdCliente.Text.ToString();
            _socio = txtCliente.Text.ToString();

            Formularios.Formularios_de_Menu.Fincas.ListaFincas_NotaPeso form = new Fincas.ListaFincas_NotaPeso();
            this.AddOwnedForm(form);
            form.idsocio = _idsocio;
            form.socio = _socio;

            form.ShowDialog();
        }

        private void txtDescuento_Humedo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Calculo_QQNetos();
            }
        }

        private void btnListaNotasPeso_Click(object sender, EventArgs e)
        {
            Formularios.Formularios_de_Menu.Notas_de_Peso.Frm_Lista_Nota_Peso notas_peso = new Frm_Lista_Nota_Peso();
            this.AddOwnedForm(notas_peso);
            notas_peso.Show();
        }

        private void btnCancelar_Click_1(object sender, EventArgs e)
        {
            string msg = "¿DESEA CANCELAR LA ACCION?";

            if (a.Pregunta(msg) == true)
            {
                Clear();
                Boot();
            }
        }

        private void btnNuevo_Click_1(object sender, EventArgs e)
        {
            btnNuevo.Enabled = false;
            btnGuardar.Enabled = Clases.Auth.save == "S" ? true : false;
            //btnActualizar.Enabled = false;
            btnEliminar.Enabled = false;
            btnCancelar.Enabled = true;
            //btnRegresar.Enabled = false;
            btnBuscarCliente.Enabled = true;
            btnBuscarFinca.Enabled = true;


            txtIdCliente.Enabled = false;
            txtCliente.Enabled = false;
            txtDireccion.Enabled = false;

            dtpFecha.Enabled = true;

            txtFletes.Enabled = true;
            txtCostoFlete.Enabled = true;
            TxtCostoTotalFletes.Enabled = false;
            cmbBodega.Enabled = true;
            txtCostoxBeneficio.Enabled = true;
            cmbEstado.Enabled = true;
            //cmbRecepcion.Enabled = true;
            txtObservaciones.Enabled = true;

            pbSalir.Enabled = false;
            btnListaNotasPeso.Enabled = false;


            txtPesoBruto.Enabled = true;
            txtSacos.Enabled = true;
            txtTaraxSaco.Enabled = true;
            txtDescuento_Humedo.Enabled = true;
            txtDescTara_LBS.Enabled = true;
            txtObservaciones.Enabled = true;
            Cosecha();
            NumNotaPeso();
        }

        string idcorre = "NOTAP";

        private void txtTaraxSaco_TextChanged(object sender, EventArgs e)
        {
            if (txtTaraxSaco.Text == "")
            {
                txtTaraxSaco.Text = "0";
            }
            Calculo_TotalLibras();
        }

        double _fletes = 0, _cant_flete = 0, _costo_total_flete=0;

        private void txtCostoxBeneficio_TextChanged(object sender, EventArgs e)
        {
            // Remover comas y espacios en blanco
            string text = txtCostoxBeneficio.Text.Replace(",", "").Replace(" ", "");

            // Verificar si el texto resultante es un número válido
            if (decimal.TryParse(text, out decimal costo))
            {
                // Formatea el valor como moneda y lo asigna al TextBox
                txtCostoxBeneficio.Text = costo.ToString("N2");
                txtCostoxBeneficio.SelectionStart = txtCostoxBeneficio.Text.Length - 3; // Coloca el cursor a
            }
        }

        private void txtCostoFlete_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Verifica si la tecla presionada es un número o una coma decimal
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != ',' && e.KeyChar != '.')
            {
                e.Handled = true; // Ignora la tecla si no es un número o una coma
                return;
            }

            // Si se presiona una coma, convierte a punto para trabajar con decimales
            if (e.KeyChar == ',')
            {
                e.KeyChar = '.';
            }

            // Obtiene el texto actual del TextBox sin el carácter presionado
            string textWithoutKey = txtCostoFlete.Text.Remove(txtCostoFlete.SelectionStart, txtCostoFlete.SelectionLength);
            textWithoutKey = textWithoutKey.Insert(txtCostoFlete.SelectionStart, e.KeyChar.ToString());

            // Intenta convertir el texto a un valor decimal
            if (decimal.TryParse(textWithoutKey, out decimal costo))
            {
                // Formatea el valor como moneda y lo asigna al TextBox
                txtCostoFlete.Text = costo.ToString("N2");
                txtCostoFlete.SelectionStart = txtCostoFlete.Text.Length - 3; // Coloca el cursor antes de los decimales
                e.Handled = true; // Indica que ya se manejó la tecla
            }

        }

        private void txtCostoFlete_TextChanged(object sender, EventArgs e)
        {
            //  _costoxben = Convert.ToDouble(txtPesoBruto.Text.ToString());
            double _flete = 0;
            _flete = Convert.ToDouble(txtCostoFlete.Text);
            _cant_flete = Convert.ToDouble(txtFletes.Text);

            _costo_total_flete =_flete*_cant_flete;

            TxtCostoTotalFletes.Text = _costo_total_flete.ToString("N2");

            // Remover comas y espacios en blanco
            string text = txtCostoFlete.Text.Replace(",", "").Replace(" ", "");

            // Verificar si el texto resultante es un número válido
            if (decimal.TryParse(text, out decimal costo))
            {
                // Formatea el valor como moneda y lo asigna al TextBox
                txtCostoFlete.Text = costo.ToString("N2");
                txtCostoFlete.SelectionStart = txtCostoFlete.Text.Length - 3; // Coloca el cursor a
            }
        }

        private void txtDescuento_Humedo_TextChanged(object sender, EventArgs e)
        {
                Calculo_QQNetos();
        }

        private void groupBox4_Enter(object sender, EventArgs e)
        {

        }

        private void txtPesoBruto_TextChanged(object sender, EventArgs e)
        {

            Calculo_TotalLibras();
            Calculo_QQNetos();
         
        }

        private void txtSacos_TextChanged(object sender, EventArgs e)
        {
            Calculo_TotalLibras();
            Calculo_QQNetos();
          
        }

        private void txtPesoBruto_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Obtén el TextBox actual
            TextBox textBox = (TextBox)sender;

            // Obtén el texto actual en el TextBox
            string currentText = textBox.Text;

            // Solo permite números, teclas de control y un punto decimal (si es válido)
            if (!char.IsControl(e.KeyChar) &&
                !char.IsDigit(e.KeyChar) &&
                (e.KeyChar != '.' || currentText.Contains('.')))
            {
                e.Handled = true; // Ignorar la tecla presionada
            }

            // Permite el signo menos solo si está al principio
            if (e.KeyChar == '-' && currentText.Length > 0)
            {
                e.Handled = true; // Ignorar el signo menos
            }
        }

        private void txtSacos_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true; // Ignorar la tecla presionada si no es un número o una tecla de control
            }
        }

        private void txtTaraxSaco_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true; // Ignorar la tecla presionada si no es un número o una tecla de control
            }
        }

        private void txtDescuento_Humedo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true; // Ignorar la tecla presionada si no es un número o una tecla de control
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            Formularios.Formularios_de_Menu.Notas_de_Peso.Frm_Elminar_Nota_Peso frmEliminar = new Formularios.Formularios_de_Menu.Notas_de_Peso.Frm_Elminar_Nota_Peso();
            frmEliminar.Show();
        }

        private void txtFletes_TextChanged(object sender, EventArgs e)
        {
            if (txtFletes.Text == "")
            {
                txtFletes.Text = "0";
            }


            // Remover comas y espacios en blanco
            string text = txtFletes.Text.Replace(",", "").Replace(" ", "");

            // Verificar si el texto resultante es un número válido
            if (decimal.TryParse(text, out decimal monto))
            {
                // Formatea el valor como moneda y lo asigna al TextBox sin decimales
                txtFletes.Text = monto.ToString("N0");
                txtFletes.SelectionStart = txtFletes.Text.Length; // Coloca el cursor al final del texto
            }
        }

        private void NumNotaPeso()
        {
            Int32 numliq, correlativo;
            correlativo = Convert.ToInt32(db.Hook("ULTIMO", "CORRELATIVOS", "IDCORRE='" + idcorre + "'"));
            numliq = correlativo + 1;

            LblNumeroNPESO.Text = numliq.ToString();
        }
        string id_cosecha;
        public void Cosecha()
        {
            string condicion = "ESTADO='ACTIVO'";
            TxtCosecha.Text = db.Hook("COSECHA", "COSECHAS", condicion);
            id_cosecha = db.Hook("ID_COSECHA", "COSECHAS", condicion);
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            ValidateData();

            string msg = "¿DESEA GUARDAR LA NOTA DE PESO No. " + id_nota + "?";

            if (errors == 0)
            {
                if (a.Pregunta(msg) == true)
                {
                    //No se si ya se creo esta tabla
                    string campos = "ID_NOTA, BODEGA, ID_COSECHA, FECHA_NOTA_PESO, ID_SOCIO, ID_FINCA, FLETES, " +
                        " COSTOXFLETE, COSTO_TOTAL_FLETES, COSTOXBENEFICIADO, ID_ESTADO_CAFE, PESO_BRUTO, QQ_BRUTOS, " +
                        "SACOS, TARAXSACO, DESC_TARA_LBS, TOTAL_LBS, " +
                        " DESCUENTO_HUMEDO, LBS_NETAS, QQ_NETO, QQ_NETO_DISP, OBSERVACIONES, USUARIO";

                    string valores = "'" + id_nota + "','" + bodega + "','" + id_cosecha + "','" + fecha + "'," +
                        "'" + id_cliente + "','" + idfinca + "', " + fletes + "," + a.ReturnsNumber(costoxflete) + ","+a.ReturnsNumber(_costo_total_f) +" ," + a.ReturnsNumber(costoxbeneficio) + ", " +
                        "'" + estadocafe + "'," + a.ReturnsNumber(peso_bruto) + "," + qqbrutos + "," + sacos + "," +
                        "" + a.ReturnsNumber(taraxsaco) + "," + a.ReturnsNumber(desc_tara_lbs) + "," + a.ReturnsNumber(total_lbs) + "," +
                        "" + a.ReturnsNumber(desc_humedo) + "," + a.ReturnsNumber(lbs_netas) + "," + a.ReturnsNumber(qq_netos) + ", " +
                        "" + a.ReturnsNumber(qq_netos) + ",'" + observaciones + "','" + Clases.Auth.user + "'";

                    string idnotap=LblNumeroNPESO.Text;

                    if (db.Save("NOTA_DE_PESO", campos, valores) > 0)
                    {
                        //db.RawSQL(campos);
                        db.SetLast("NOTAS_DE_PESO");
                        a.Aprueba("LA NOTA DE PESO DEL SOCIO " + cliente + " HA SIDO REGISTRADA CON ÉXITO!");
                        db.SetLast(idcorre);

                        Reportes.FrmRptNotaPeso notaPeso = new Reportes.FrmRptNotaPeso();
                        notaPeso.codnotapeso= idnotap;
                        notaPeso.Show();

                        Clear();
                        Boot();
                        //Restar_PagoPendiente_de_Prestamo();
                        //if (txtCant_Debe.Text == txtAbono.Text)
                        //{
                        //    Estado();
                        //}

                    }

                }
            }
        }
        string _costo_total_f;
        public void ValidateData()
        {
            errors = 0;
            cosecha = a.Clean(TxtCosecha.Text.Trim());
            direccion = a.Clean(txtDireccion.Text.Trim());
            id_cliente = a.Clean(txtIdCliente.Text.Trim());
            cliente = a.Clean(txtCliente.Text.Trim());
            fecha = a.Clean(dtpFecha.Text.Trim());
            id_nota = a.Clean(LblNumeroNPESO.Text.Trim());
            idfinca = a.Clean(txtIdFinca.Text.Trim());
            direccionfinca = a.Clean(txtFincaDireccion.Text.Trim());
            bodega = cmbBodega.Text != "" ? cmbBodega.SelectedValue.ToString() : "";
            fletes = a.Clean(txtFletes.Text.Trim());
            costoxflete = a.Clean(txtCostoFlete.Text.Trim());
            _costo_total_f= a.Clean(TxtCostoTotalFletes.Text.Trim());
            costoxbeneficio = a.Clean(txtCostoxBeneficio.Text.Trim());
            estadocafe = cmbEstado.Text != "" ? cmbEstado.SelectedValue.ToString() : "";
            observaciones = a.Clean(txtObservaciones.Text.Trim());
            //recepcioncafe = cmbRecepcion.Text != "" ? cmbRecepcion.SelectedValue.ToString() : "";

            peso_bruto = a.Clean(txtPesoBruto.Text.Trim());
            sacos = a.Clean(txtSacos.Text.Trim());
            taraxsaco = a.Clean(txtTaraxSaco.Text.Trim());
            desc_tara_lbs = a.Clean(txtDescTara_LBS.Text.Trim());
            total_lbs = a.Clean(txtTotal_LBS.Text.Trim());
            desc_humedo = a.Clean(txtDescuento_Humedo.Text.Trim());
            lbs_netas = a.Clean(txtLBS_Netas.Text.Trim());
            qq_netos = a.Clean(LblTotal.Text.Trim());

            //dias = a.Clean(txtDias.Text.Trim());
            //interes = a.Clean(txtInteres_Lps.Text.Trim());

            if (cosecha.Length == 0)
            {
                a.Advertencia("¡LA COSECHA, ES REQUERIDA!");
                TxtCosecha.Focus();
                errors++;
                return;
            }

            if (id_cliente.Length == 0)
            {
                a.Advertencia("¡EL NOMBRE DEL SOCIO, ES REQUERIDO!");
                txtIdCliente.Text = "";
                txtIdCliente.Focus();
                errors++;
                return;
            }

            if (fecha.Length == 0)
            {
                a.Advertencia("¡SELECCIONE UNA FECHA FINAL VÁLIDA!");
                dtpFecha.Focus();
                errors++;
                return;
            }

            //if (id_nota.Length == 0)
            //{
            //    a.Advertencia("¡EL ID DE LA NOTA DE PESO, ES REQUERIDO!");
            //    txtIdNota.Text = "";
            //    txtIdNota.Focus();
            //    errors++;
            //}

            if (idfinca.Length == 0)
            {
                a.Advertencia("¡EL ID DE LA FINCA, ES REQUERIDO!");
                txtIdFinca.Text = "";
                txtIdFinca.Focus();
                errors++;
                return;
            }

            if (bodega.Length == 0)
            {
                a.Advertencia("¡ESPECIFICAR LA BODEGA, ES REQUERIDO!");
                cmbBodega.SelectedIndex = -1;
                cmbBodega.Focus();
                errors++;
            }

            if (fletes.Length == 0)
            {
                txtFletes.Text = "0";
                fletes = txtFletes.Text.Trim();
            }

            if (costoxflete.Length == 0)
            {
                txtCostoFlete.Text = "0";
                costoxflete = txtCostoFlete.Text.Trim();
            }

            if (costoxbeneficio.Length == 0)
            {
                txtCostoxBeneficio.Text = "0";
                costoxbeneficio = txtCostoxBeneficio.Text.Trim();
            }

            //if (recepcioncafe.Length == 0)
            //{
            //    a.Advertencia("¡ESPECIFICAR LA RECEPCIÓN DEL CAFÉ, ES REQUERIDO!");
            ////    cmbRecepcion.SelectedIndex = -1;
            ////cmbRecepcion.Focus();
            //    errors++;
            //}

            if (estadocafe.Length == 0)
            {
                a.Advertencia("¡ESPECIFICAR EL ESTADO DEL CAFÉ, ES REQUERIDO!");
                cmbEstado.SelectedIndex = -1;
                cmbEstado.Focus();
                errors++;
            }

            if (peso_bruto.Length == 0)
            {
                a.Advertencia("¡EL PESO BRUTO DEL CAFÉ, ES REQUERIDO!");
                txtPesoBruto.Text = "";
                txtPesoBruto.Focus();
                errors++;
            }

            if (sacos.Length == 0)
            {
                a.Advertencia("¡LA CANTIDAD DE SACOS DEL CAFÉ, ES REQUERIDO!");
                txtSacos.Text = "";
                txtSacos.Focus();
                errors++;
            }

            if (taraxsaco.Length == 0)
            {
                a.Advertencia("¡LA TARA POR SACO, ES REQUERIDA!");
                txtTaraxSaco.Text = "";
                txtTaraxSaco.Focus();
                errors++;
            }

            if (desc_tara_lbs.Length == 0)
            {
                a.Advertencia("¡EL DESCUENTO DE TARA, ES REQUERIDO!");
                txtDescTara_LBS.Text = "";
                txtDescTara_LBS.Focus();
                errors++;
            }

            if (total_lbs.Length == 0)
            {
                a.Advertencia("¡EL TOTAL EN LIBRAS, ES REQUERIDO!");
                txtTotal_LBS.Text = "";
                txtTotal_LBS.Focus();
                errors++;
            }

            if (desc_humedo.Length == 0)
            {
                a.Advertencia("¡EL DESCUENTO DEL CAFÉ HÚMEDO, ES REQUERIDO!");
                txtDescuento_Humedo.Text = "";
                txtDescuento_Humedo.Focus();
                errors++;
            }

            if (lbs_netas.Length == 0)
            {
                a.Advertencia("¡LAS LIBRAS NETAS DEL CAFÉ, SON REQUERIDAS!");
                txtLBS_Netas.Text = "";
                txtLBS_Netas.Focus();
                errors++;
            }

            if (qq_netos.Length == 0)
            {
                a.Advertencia("¡LOS QUINTALES NETOS DEL CAFÉ, SON REQUERIDOS!");
                LblTotal.Text = "";
                LblTotal.Focus();
                errors++;
            }

        }

        private void Frm_Notas_Peso_Load(object sender, EventArgs e)
        {
            this.Text = Clases.Env.APPNAME + " | NOTA DE PESO | " + Clases.Auth.user + " | " + Clases.Auth.rol;
            Boot();

            Metodos_CMB();
        }


        private void Metodos_CMB() 
        {
            Bodegas();
            //Tipo_Recepcion_Cafe();
            Tipo_Estado_Cafe();
        }
        private void Bodegas()
        {
            cmbBodega.DataSource = db.Find("BODEGAS", "ID_BODEGA, NOMBRE", "", "NOMBRE");
            cmbBodega.ValueMember = "ID_BODEGA";
            cmbBodega.DisplayMember = "NOMBRE";
            cmbBodega.SelectedIndex = -1;
        }

        //private void Tipo_Recepcion_Cafe()
        //{
        //    cmbRecepcion.DataSource = db.Find("TIPO_RECEPCION", "ID, TIPO_RECEPCION", "", "TIPO_RECEPCION");
        //    cmbRecepcion.ValueMember = "ID";
        //    cmbRecepcion.DisplayMember = "TIPO_RECEPCION";
        //    cmbRecepcion.SelectedIndex = -1;
        //}

        private void Tipo_Estado_Cafe()
        {
            cmbEstado.DataSource = db.Find("ESTADO_CAFE", "ID, ESTADO", "", "ESTADO");
            cmbEstado.ValueMember = "ID";
            cmbEstado.DisplayMember = "ESTADO";
            cmbEstado.SelectedIndex = -1;
        }


        //Recibe la informacion del socio
        public void CodSocio(string cod_socio)
        {
            string condicion = "ID_SOCIO='" + cod_socio + "'";
            txtIdCliente.Text = cod_socio;
            txtCliente.Text = db.Hook("NOMBRE", "SOCIOS", condicion);
            txtDireccion.Text = db.Hook("DIRECCION", "SOCIOS", condicion);

            txtIdFinca.Text = "";
            txtFinca.Text = "";
            txtFincaDireccion.Text = "";

        }

        //Recibe la informacion de la finca del socio
        public void CodFinca(string cod_finca)
        {
            string condicion = "IDFINCA='" + cod_finca + "'";
            txtIdFinca.Text = cod_finca;
            txtFinca.Text = db.Hook("NOMBREFINCA", "FINCAS", condicion);
            txtFincaDireccion.Text = db.Hook("UBICACION", "FINCAS", condicion);
        }

        private void Boot()
        {
            
            btnNuevo.Enabled = Clases.Auth.save == "S" ? true : false;
            btnGuardar.Enabled = false;
            //btnActualizar.Enabled = false;
            btnEliminar.Enabled = true;
            btnCancelar.Enabled = false;
            pbSalir.Enabled = true;
            btnBuscarCliente.Enabled = false;
            btnBuscarFinca.Enabled = false;

            txtIdCliente.Enabled = false;
            txtCliente.Enabled = false;
            txtDireccion.Enabled = false;
            TxtCosecha.Enabled = false;
            dtpFecha.Enabled = false;


            pbSalir.Enabled = true;


            btnListaNotasPeso.Enabled = true;
            txtIdFinca.Enabled = false;
            txtFinca.Enabled = false;
            txtFincaDireccion.Enabled = false;

            txtFletes.Enabled = false;
            txtCostoFlete.Enabled = false;
            txtCostoxBeneficio.Enabled = false;
            TxtCostoTotalFletes.Enabled = false;
            cmbBodega.Enabled = false;
            cmbEstado.Enabled = false;

            txtPesoBruto.Enabled = false;
            txtSacos.Enabled = false;
            txtTaraxSaco.Enabled = false;
            txtDescTara_LBS.Enabled = false;
            txtTotal_LBS.Enabled = false;
            txtDescuento_Humedo.Enabled = false;
            txtLBS_Netas.Enabled = false;

            txtObservaciones.Enabled = false;
        }

        double _peso_bruto=0, _sacos=0, _taraxsaco=0, _desc_tara_lbs=0, _total_lbs=0;
        double _desc_humedo = 0, _lbs_netas = 0, _qq_netos = 0;
        double qqbrutos = 0;

        private void Calculo_TotalLibras()
        {
            if (txtPesoBruto.Text == "")
            {
                txtPesoBruto.Text = "0";
            }

            _peso_bruto = Convert.ToDouble(txtPesoBruto.Text);

            if (txtSacos.Text == "")
            {
                txtSacos.Text = "0";
            }

            _sacos = Convert.ToDouble(txtSacos.Text);

            if (txtTaraxSaco.Text == "")
            {
                txtTaraxSaco.Text = "0";
            }
            _taraxsaco = Convert.ToDouble(txtTaraxSaco.Text);
                        

            _desc_tara_lbs = _sacos * _taraxsaco;

            txtDescTara_LBS.Text = _desc_tara_lbs.ToString();


            _total_lbs = _peso_bruto - _desc_tara_lbs;

            txtTotal_LBS.Text = _total_lbs.ToString();

            qqbrutos = _peso_bruto / 100;
        }

        private void Calculo_QQNetos() 
        {
            if (txtDescuento_Humedo.Text == "")
            {
                txtDescuento_Humedo.Text = "0";
            }
            _desc_humedo = Convert.ToDouble(txtDescuento_Humedo.Text.ToString());

            _lbs_netas = _total_lbs * (100 - _desc_humedo) / 100;

            txtLBS_Netas.Text = _lbs_netas.ToString();

            _qq_netos = _lbs_netas / 100;

            LblTotal.Text = _qq_netos.ToString();
        }

        public Frm_Notas_Peso()
        {
            InitializeComponent();
        }

        private void pbSalir_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Formularios.Frm_Menu menu = new Frm_Menu();
            menu.Show();
            this.Hide();
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {

        }


        private void Clear()
        {
            TxtCosecha.Clear();
            txtDireccion.Clear();
            dtpFecha.Text = "";
            txtIdCliente.Clear();
            txtCliente.Clear();
            LblNumeroNPESO.Text="";
            txtIdFinca.Clear();
            txtFinca.Clear();
            txtFincaDireccion.Clear();
            cmbBodega.SelectedIndex = -1;
            cmbEstado.SelectedIndex = -1;
            txtCostoxBeneficio.Clear();
            txtCostoFlete.Text="0.00";
            txtFletes.Clear();
            TxtCostoTotalFletes.Clear();
            txtObservaciones.Clear();

            txtPesoBruto.Text="0.00";
            txtSacos.Text="0";
            txtTaraxSaco.Text="0";
            txtDescTara_LBS.Text = "0.00";
            txtTotal_LBS.Text = "0.00";
            txtDescuento_Humedo.Text = "0";
            txtLBS_Netas.Text = "0.00";
            LblTotal.Text = "0.00";

        }
    }
}
