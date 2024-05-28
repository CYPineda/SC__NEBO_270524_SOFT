using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SC__NEBO.Formularios.Formularios_de_Menu.Contratos
{
    public partial class FrmContratoxSocio : Form
    {

        Clases.DB db = new Clases.DB();
        Clases.Asistente a = new Clases.Asistente();

        string codigo, codigo_cosecha, id_cosechas, fecha_inicio, fecha_limite, codigo_socios, id_bodegas, precioc, cant_qqc, valor_totalc;

        private void BtnGuardar_Click(object sender, EventArgs e)
        {
            ValidateData();

            string msg = "¿DESEA REGISTRAR EL CONTRATO DEL SOCIO " + socio + "?";

            if (errors == 0)
            {
                if (a.Pregunta(msg) == true)
                {
                    string campos = "CODIGO, ID_COSECHAS, FECHA_INICIO, FECHA_LIMITE,  CODIGO_SOCIOS, ID_BODEGAS, PRECIO, CANT_QQ, CANT_QQ_DISP, VALOR_TOTAL";
                    string valores = "'" + codigo + "', '" + codigo_cosecha + "','" + fecha_inicio + "', '" + fecha_limite + "', '" + codigo_socios + "', " +
                        "'" + id_bodegas + "'," + a.ReturnsNumber(precioc) + "," + a.ReturnsNumber(cant_qqc) + "," + a.ReturnsNumber(cant_qqc) + "," + a.ReturnsNumber(valor_totalc) + "";


                    if (db.Save("CONTRATOS_CAFE", campos, valores) > 0)
                    {
                        a.Aprueba("¡EL CONTRATO DEL SOCIO " + socio + " SE REGISTRÓ CON ÉXITO!");
                        db.SetLast("CONTR");

                        Reportes.FrmRptContrato_Socio contra_socio = new Reportes.FrmRptContrato_Socio();
                        contra_socio.codigo_c = codigo;
                        contra_socio.Show();

                        Clear();
                        Boot();

                        

                        //this.Close();
                    }
                }
            }
        }

        double precio = 0, cant_qq = 0, valor_total = 0;

        private void TxtCantQQ_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Verifica si la tecla presionada es un número, una coma o un punto, o una tecla de control (por ejemplo, retroceso)
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.' && e.KeyChar != ',')
            {
                // Si no es un número, coma, punto ni una tecla de control, suprime el carácter
                e.Handled = true;
            }



            // Reemplaza la coma con un punto si se ingresó una coma
            if (e.KeyChar == ',')
            {
                e.KeyChar = '.';
            }

            // Asegura que solo haya un punto decimal
            TextBox textBox = (TextBox)sender;
            if ((e.KeyChar == '.' || e.KeyChar == ',') && textBox.Text.Contains("."))
            {
                e.Handled = true;
            }

        }

        private void TxtPrecio_KeyPress(object sender, KeyPressEventArgs e)
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
        }

        private void TxtValorTotal_TextChanged(object sender, EventArgs e)
        {
            // Remover comas y espacios en blanco
            string text = TxtValorTotal.Text.Replace(",", "").Replace(" ", "");

            // Verificar si el texto resultante es un número válido
            if (decimal.TryParse(text, out decimal monto))
            {
                // Formatea el valor como moneda y lo asigna al TextBox
                TxtValorTotal.Text = monto.ToString("N2");
                TxtValorTotal.SelectionStart = TxtValorTotal.Text.Length - 3; // Coloca el cursor antes de los decimales
            }
        }

        private void TxtPrecio_TextChanged(object sender, EventArgs e)
        {
            // Remover comas y espacios en blanco
            string text = TxtPrecio.Text.Replace(",", "").Replace(" ", "");

            // Verificar si el texto resultante es un número válido
            if (decimal.TryParse(text, out decimal monto))
            {
                // Formatea el valor como moneda y lo asigna al TextBox
                TxtPrecio.Text = monto.ToString("N2");
                TxtPrecio.SelectionStart = TxtPrecio.Text.Length - 3; // Coloca el cursor antes de los decimales
            }

        }

        private void TxtCantQQ_TextChanged(object sender, EventArgs e)
        {
            Calcular_ValorTotal();

            if (TxtCantQQ.Text == "")
            {
                TxtCantQQ.Text = "0.00";
            }

            // Remover comas y espacios en blanco
            string text = TxtCantQQ.Text.Replace(",", "").Replace(" ", "");

            // Verificar si el texto resultante es un número válido
            if (decimal.TryParse(text, out decimal monto))
            {
                // Formatea el valor como moneda y lo asigna al TextBox
                TxtCantQQ.Text = monto.ToString();


            }
        }

        public string idsocio, socio;

        int errors;

        public FrmContratoxSocio()
        {
            InitializeComponent();
        }

        private void FrmContratoxSocio_Load(object sender, EventArgs e)
        {
            this.Text = Clases.Env.APPNAME + " | REGISTRO DE CONTRATOS | " + Clases.Auth.user + " | " + Clases.Auth.rol;

            TxtCodigo.Text = "CTR" + db.GetNext("CONTR");

            TxtSocio.Text = socio;
            Cosecha();
            Direccion_Socio();
            GetBodega();

            Boot();
        }

        
        private void Calcular_ValorTotal() 
        {
            double cantidadqq_ = 0, precio_ = 0, valorT_ = 0;
            if (TxtCantQQ.Text == "")
            {
                TxtCantQQ.Text = "0";
            }

            cantidadqq_ = Convert.ToDouble(TxtCantQQ.Text);

            if (TxtPrecio.Text == "")
            {
                TxtPrecio.Text = "0.00";
            }
            precio_ = Convert.ToDouble(TxtPrecio.Text);

            valorT_ = cantidadqq_ * precio_;

            TxtValorTotal.Text = valorT_.ToString();
        }

        private void Boot()
        {
            //btnNuevo.Enabled = true;
            //btnGuardar.Enabled = false;
            //btnCancelar.Enabled = false;
            //BtnBuscarSocio.Enabled = false;

            TxtCosecha.Enabled = false;

            TxtSocio.Enabled = false;
            TxtDireccion.Enabled = false;
            CmbBodega.Enabled = true;
            DtpFechaInicio.Enabled = true;
            DtpFechaLimite.Enabled = true;
            TxtPrecio.Enabled = true;
            TxtCantQQ.Enabled = true;
            TxtValorTotal.Enabled = false;

            GetBodega();

        }

        private void Clear()
        {
            TxtCodigo.Text = "";
            TxtCosecha.Text = "";
            TxtSocio.Text = "";
            TxtDireccion.Text = "";
            CmbBodega.SelectedIndex = -1;
            DtpFechaInicio.Text = "";
            DtpFechaLimite.Text = "";
            TxtPrecio.Text = "";
            TxtCantQQ.Text = "";
            TxtValorTotal.Text = "";
        }

        public void Direccion_Socio()
        {
            string condicion = "ID_SOCIO='"+idsocio+"'";
            TxtDireccion.Text = db.Hook("DIRECCION", "SOCIOS", condicion);
            codigo_socios = db.Hook("ID_SOCIO", "SOCIOS", condicion);
        }

        private void GetBodega()
        {
            CmbBodega.DataSource = db.Find("BODEGAS", "ID_BODEGA, NOMBRE", "", "NOMBRE");
            CmbBodega.ValueMember = "ID_BODEGA";
            CmbBodega.DisplayMember = "NOMBRE";
            CmbBodega.SelectedIndex = -1;
        }

        public void Cosecha()
        {
            string condicion = "ESTADO='ACTIVO'";
            TxtCosecha.Text = db.Hook("COSECHA", "COSECHAS", condicion);
            codigo_cosecha = db.Hook("ID_COSECHA", "COSECHAS", condicion);
        }

        private void ValidateData()
        {
            errors = 0;
            codigo = a.Clean(TxtCodigo.Text.Trim());
            id_cosechas = a.Clean(TxtCosecha.Text.Trim());
            //codigo_socios = a.Clean(TxtSocio.Text.Trim());
            fecha_inicio = a.Clean(DtpFechaInicio.Text.Trim());
            fecha_limite = a.Clean(DtpFechaLimite.Text.Trim());
            // codigo_socios = a.Clean(codigo.Text.Trim());
            id_bodegas = CmbBodega.Text != "" ? CmbBodega.SelectedValue.ToString() : "";
            precioc = a.Clean(TxtPrecio.Text.Trim());
            cant_qqc = a.Clean(TxtCantQQ.Text.Trim());
            valor_totalc = a.Clean(TxtValorTotal.Text.Trim());


            //Si ambas fechas son iguales mostrar el mensaje
            if (fecha_inicio.Equals(fecha_limite))
            {
                a.Advertencia("¡LA FECHA ANTERIOR Y LA FECHA DE PAGO SON LAS MISMAS!");
                DtpFechaLimite.Focus();
                errors++;
                return;
            }

            if (codigo.Length == 0)
            {
                a.Pregunta("¡INGRESAR UN CÓDIGO VÁLIDO!");
                TxtCodigo.Text = codigo;
                TxtCodigo.Focus();
                errors++;
                return;
            }
            //if (codigo_socios.Length == 0)
            //{
            //    a.Advertencia("¡SELECCIONE UN SOCIO!");
            //    TxtSocio.Text = socio;
            //    TxtSocio.Focus();
            //    errors++;
            //}
            if (fecha_inicio.Length == 0)
            {
                a.Advertencia("¡SELECCIONE UNA FECHA VÁLIDA!");
                DtpFechaInicio.Text = fecha_inicio;
                DtpFechaInicio.Focus();
                errors++;
                return;
            }
            if (fecha_limite.Length == 0)
            {
                a.Advertencia("¡SELECCIONE UNA FECHA VÁLIDA!");
                DtpFechaLimite.Text = fecha_limite;
                DtpFechaLimite.Focus();
                errors++;
                return;
            }
            if (id_bodegas.Length == 0)
            {
                a.Advertencia("¡SELECCIONE UNA BODEGA!");
                CmbBodega.Text = id_bodegas;
                CmbBodega.Focus();
                errors++;
            }
            if (precioc.Length == 0)
            {
                a.Advertencia("¡INGRESE UN PRECIO POR QQ DE CAFÉ!");
                TxtPrecio.Text = "";
                TxtPrecio.Focus();
                errors++;
            }
            if (cant_qqc.Length == 0)
            {
                a.Advertencia("¡INGRESE UNA CANTIDAD DE QQ DE CAFÉ!");
                TxtCantQQ.Text = "";
                TxtCantQQ.Focus();
                errors++;
            }

        }
    }
}
