using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SC__NEBO.Formularios.Formularios_de_Menu.Prestamos
{
    public partial class FrmPagoXPrestamo : Form
    {
        Clases.Asistente h = new Clases.Asistente();
        Clases.DB db = new Clases.DB();

        public string id_socio, nombre_socio, abono, interes, cantidad_lps, no_prestamo, fecha_inicial, monto_pendiente, int_acum;
        int errors;
        string fecha_anterior, fecha_pago, monto_pendi, interes_porcentaje, cant_pagar, detalles, dias, Int_x_Acum;

        private void btnPagar_Click(object sender, EventArgs e)
        {
            ValidateData();
            string Cprest_l = txtCod_Prestamo.Text;
            double totp_l = Convert.ToDouble(txtTotal_a_Pagar.Text.Trim());
            string abonopag_l = txtAbono.Text;

            string int_acumulado_l = TxtInt_Acum.Text;
            double int_x_acumular = Convert.ToDouble(Txt_Int_X_Acumular.Text.Trim());
            double cant_debe = Convert.ToDouble(txtCant_Debe.Text.Trim());
            string detalles = TxtDetalles.Text;
            string fecha = dtpFecha_Pago.Text;
            //string interes_l = TxtInteres_Lps.Text;
            // string dias_l = txt

            PrestamoSave(fecha,Cprest_l, abonopag_l, int_acumulado_l, interes, int_x_acumular.ToString(), dias, totp_l.ToString(), cant_debe.ToString(), detalles);

            Close();
        }


        private void PrestamoSave(string FECHA, string _codigo, string _montopagar, string _int, string _intacum, string _dias,  string _totpagar, string _int_x_pag, string _cantdebe, string _detalles)
        {
            int i;
            //string _codigo, _montopagar, _intacum, _dias, _int, _totpagar, _int_x_pag, _detalles, _cantdebe;
            string campos = "ID_PAGO, PRESTAMO_CODIGO, FECHA, ID_FORMA_PAGO, MONTO_PAGO, INTERES,INTERES_ACUMULADO, DIAS,TOTAL_PAGO, OBSERVACIONES";
            string valores;
            string cod_pago;
            string _idform = "FPP000001";

                cod_pago = "PGS" + db.GetNext("PAGOS");
   

                if (_int_x_pag == null)
                {
                    _int_x_pag = "0";
                }

                valores = "'" + cod_pago + "','" + _codigo + "','" + FECHA + "','" + _idform + "'," + h.ReturnsNumber(_montopagar) + ", " + h.ReturnsNumber(_int) + ", " + h.ReturnsNumber(_intacum) + ", " + h.ReturnsNumber(_dias) + ", " + h.ReturnsNumber(_totpagar) + ", '" + _detalles + "'";

                db.Save("PAGOS", campos, valores, true);

                db.SetLast("PAGOS");
                if (_montopagar == _cantdebe)
                {
                    string stmt = "ESTADO = 'PAGADO'";
                    string condicion = "PRESTAMO_CODIGO = '" + _codigo + "'";
                    db.Update("PRESTAMOS", stmt, condicion);
                }

                if (Convert.ToDouble(_int_x_pag) > 0)
                {
                    string stmt = "INTERES_ACUMULADO=" + _int_x_pag + "";
                    string condicion = "PRESTAMO_CODIGO='" + _codigo + "'";
                    db.Update("PRESTAMOS", stmt, condicion);
                }

                if (Convert.ToDouble(_montopagar) > 0)
                {
                    string condicion = "PRESTAMO_CODIGO = '" + _codigo + "'";
                    string stmt = "MONTO_PENDIENTE = MONTO_PENDIENTE - " + _montopagar + "";
                    db.Update("PRESTAMOS", stmt, condicion);
                }
                string parametro = "FECHA_ULTIMO_PAGO='" + FECHA + "'";
                string cond = "PRESTAMO_CODIGO='" + _codigo + "'";
                db.Update("PRESTAMOS", parametro, cond);
            //}
        }










        public void ValidateData()
        {
            errors = 0;
            no_prestamo = h.Clean(txtCod_Prestamo.Text.Trim());
            fecha_anterior = h.Clean(dtpFecha_Inicio.Text.Trim());
            fecha_pago = h.Clean(dtpFecha_Pago.Text.Trim());
            nombre_socio = h.Clean(txtNomb_Socio.Text.Trim());
            cantidad_lps = txtCapital_Inicial.Text;
            monto_pendi = h.Clean(txtCant_Debe.Text.Trim());
            interes_porcentaje = txtInteres.Text;
            cant_pagar = h.Clean(txtAbono.Text.Trim());
            detalles = h.Clean(TxtDetalles.Text.Trim());

            Int_x_Acum = h.Clean(Txt_Int_X_Acumular.Text.Trim());

            dias = h.Clean(txtDias.Text.Trim());
            interes = h.Clean(TxtInteres_Lps.Text.Trim());

            if (fecha_pago.Length == 0)
            {
                h.Advertencia("¡SELECCIONE UNA FECHA FINAL VÁLIDA!");
                dtpFecha_Pago.Focus();
                errors++;
                return;
            }

            if (fecha_anterior.Equals(fecha_pago))
            {
                h.Advertencia("¡LA FECHA ANTERIOR Y LA FECHA DE PAGO SON LAS MISMAS!");
                dtpFecha_Pago.Focus();
                errors++;
                return;
            }


            if (no_prestamo.Length == 0)
            {
                h.Advertencia("¡CÓDIGO DE PRÉSTAMO VÁLIDO!");
                txtCod_Prestamo.Text = "";
                txtCod_Prestamo.Focus();
                errors++;
                return;
            }

            if (nombre_socio.Length == 0)
            {
                h.Advertencia("¡EL NOMBRE DEL SOCIO ES REQUERIDO!");
                txtNomb_Socio.Text = "";
                txtNomb_Socio.Focus();
                errors++;
                return;
            }

            if (cantidad_lps.Length == 0)
            {
                h.Advertencia("¡LA CANTIDAD DE LEMPIRAS CORRESPONDIENTE AL PRÉSTAMO ES REQUERIDA!");
                txtCapital_Inicial.Text = "";
                txtCapital_Inicial.Focus();
                errors++;
            }


            if (monto_pendi.Length == 0)
            {
                h.Advertencia("¡LA CANTIDAD DE LEMPIRAS PENDIENTE ES REQUERIDA!");
                txtCant_Debe.Text = "";
                txtCant_Debe.Focus();
                errors++;
            }

            if (interes_porcentaje.Length == 0)
            {
                h.Advertencia("¡EL INTERÉS PORCENTUAL ES REQUERIDO!");
                txtInteres.Text = "";
                txtInteres.Focus();
                errors++;
                return;
            }

            if (cant_pagar.Length == 0)
            {
                h.Advertencia("¡INGRESE LA CANTIDAD DE LEMPIRAS CORRESPONDIENTE A PAGAR!");
                txtAbono.Text = "";
                txtAbono.Focus();
                errors++;
            }

            if (dias.Length == 0)
            {
                h.Advertencia("¡LA CANTIDAD DE DÍAS ES REQUERIDA!");
                txtDias.Text = "";
                txtDias.Focus();
                errors++;
            }

            if (interes.Length == 0)
            {
                h.Advertencia("¡EL INTERÉS CALCULADO HASTA EL MOMENTO ES REQUERIDO!");
                TxtInteres_Lps.Text = "";
                TxtInteres_Lps.Focus();
                errors++;
            }

            if (Int_x_Acum.Length == 0)
            {
                h.Advertencia("¡EL INTERES POR ACUMULADO ES REQUERIDO!");
                Txt_Int_X_Acumular.Text = "";
                Txt_Int_X_Acumular.Focus();
            }


            if (detalles.Length == 0)
            {
                TxtDetalles.Text = "Ninguno";

            }

        }

        private void txtTotal_a_Pagar_TextChanged(object sender, EventArgs e)
        {
            CalculoPago();

            if (txtTotal_a_Pagar.Text == "")
            {
                txtTotal_a_Pagar.Text = "0.00";
            }

            string text = txtTotal_a_Pagar.Text.Replace(",", "").Replace(" ", "");

            if (decimal.TryParse(text, out decimal monto))
            {
                txtTotal_a_Pagar.Text = monto.ToString("N2");
                txtTotal_a_Pagar.SelectionStart = txtTotal_a_Pagar.Text.Length - 3;
            }
        }

        private void dtpFecha_Pago_ValueChanged(object sender, EventArgs e)
        {
            CalcularDias();
            Calcular_InteresporDia();
            CalculoPago();
        }

        public FrmPagoXPrestamo()
        {
            InitializeComponent();
        }

        private void FrmPagoXPrestamo_Load(object sender, EventArgs e)
        {
            txtIdSocio.Text = id_socio;
            txtNomb_Socio.Text = nombre_socio;
            this.Text = Clases.Env.APPNAME + " | BÚSQUEDA DE PAGOS DE PRÉSTAMO | " + Clases.Auth.user + " | " + Clases.Auth.rol;
            txtAbono.Text = abono;
            txtInteres.Text = interes;
            txtCapital_Inicial.Text = cantidad_lps;
            txtCod_Prestamo.Text = no_prestamo;
            dtpFecha_Inicio.Text = fecha_inicial;
            txtCant_Debe.Text = monto_pendiente;
            TxtInt_Acum.Text = int_acum;
            CalcularDias();
            Calcular_InteresporDia();
            //CalculoPago();
            txtTotal_a_Pagar.Text = 0.ToString("N2");
        }


        private void CalcularDias()
        {
            DateTime fechaInicio = dtpFecha_Inicio.Value;
            DateTime fechaFin = dtpFecha_Pago.Value;

            int dias = db.Days360(fechaInicio, fechaFin); // Devuelve 360
            txtDias.Text = dias.ToString();

        }

        private void Calcular_InteresporDia()
        {
            if (txtDias.Text == "" && txtInteres.Text == "" && txtCapital_Inicial.Text == "")
            {
                txtDias.Text = "0";
                txtInteres.Text = "0";
                txtCapital_Inicial.Text = "0.00";
            }
            else
            {
                //int cant_dias;
                double cant_diass = 0;
                double interess = 0;
                double cant_lempiras = 0;

                cant_diass = Convert.ToDouble(txtDias.Text);

                interess = Convert.ToDouble(txtInteres.Text);
                cant_lempiras = Convert.ToDouble(txtCapital_Inicial.Text);

                //Intereses
                double interes_pago = ((cant_lempiras * (interess / 100)) / 360) * cant_diass;

                //Aqui se obtiene los intereses hasta la fecha
                TxtInteres_Lps.Text = interes_pago.ToString("N2");

            }
        }


        private void CalculoPago()
        {
            double interes = 0, abono_prestamo = 0, int_acumulado;

            if (txtTotal_a_Pagar.Text == "")
            {
                txtTotal_a_Pagar.Text = "0.00";
            }
            int_acumulado = Convert.ToDouble(TxtInt_Acum.Text);
            double tot_pag_prest = Convert.ToDouble(txtTotal_a_Pagar.Text);
            interes = Convert.ToDouble(TxtInteres_Lps.Text);
            double int_x_acumular = 0;

            if (int_acumulado > 0)
            {
                abono_prestamo = tot_pag_prest - int_acumulado - interes;

                if (abono_prestamo < 0)
                {
                    int_x_acumular = abono_prestamo * -1;
                    Txt_Int_X_Acumular.Text = int_x_acumular.ToString("N2");
                    txtAbono.Text = 0.ToString("N2");

                }
                else
                {
                    Txt_Int_X_Acumular.Text = 0.ToString("N2");
                    txtAbono.Text = abono_prestamo.ToString("N2");
                }

            }
            else
            {
                abono_prestamo = tot_pag_prest - interes;
                if (abono_prestamo < 0)
                {
                    int_x_acumular = abono_prestamo * -1;
                    Txt_Int_X_Acumular.Text = int_x_acumular.ToString("N2");
                    txtAbono.Text = 0.ToString("N2");

                }
                else
                {
                    Txt_Int_X_Acumular.Text = 0.ToString("N2");
                    txtAbono.Text = abono_prestamo.ToString("N2");
                }
            }


        }
    }
}
