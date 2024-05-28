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
    public partial class FrmPagoPrestamo_Liquidacion : Form
    {

        Clases.DB db = new Clases.DB();
        Clases.Asistente a = new Clases.Asistente();

        public string id_socio, nombre_socio;
        double abono;
        string no_prestamo, fecha_anterior;
        string cantidad_lps, interes, monto_pendi;

        int errors;

        string id_pago, fecha_pago, id_tipo_pago, formapago, cant_pagar, dias, interes_porcentaje, detalles;

        private void btnSeleccionar_Prestamo_Click(object sender, EventArgs e)
        {
            //Recolecto el interes del socio que seleccione
            if (DgvData.SelectedCells.Count > 0)
            {
                int rowIndex = DgvData.SelectedCells[5].RowIndex;
                DataGridViewRow selectedRow = DgvData.Rows[rowIndex];
                int interes = Convert.ToInt32(selectedRow.Cells["dcInteres"].Value);

                int rowIndex2 = DgvData.SelectedCells[4].RowIndex;
                DataGridViewRow selectedRow2 = DgvData.Rows[rowIndex2];
                string cantida_lps = Convert.ToString(selectedRow2.Cells["dcCantidad_LPS"].Value);

                int rowIndex3 = DgvData.SelectedCells[0].RowIndex;
                DataGridViewRow selectedRow3 = DgvData.Rows[rowIndex3];
                string no_prestamo = Convert.ToString(selectedRow3.Cells["dcNo_Prestamo"].Value);

                int rowIndex4 = DgvData.SelectedCells[1].RowIndex;
                DataGridViewRow selectedRow4 = DgvData.Rows[rowIndex4];
                string fecha_inicial = Convert.ToString(selectedRow4.Cells["DcFecha_Inicial"].Value);

                int rowIndex5 = DgvData.SelectedCells[6].RowIndex;
                DataGridViewRow selectedRow5 = DgvData.Rows[rowIndex5];
                string monto_pendiente = Convert.ToString(selectedRow5.Cells["dcMonto_Pendiente"].Value);

                int rowIndex6 = DgvData.SelectedCells[7].RowIndex;
                DataGridViewRow selectedRow6 = DgvData.Rows[rowIndex6];
                double int_acum = Convert.ToDouble(selectedRow6.Cells["dcInteres_Acum"].Value);

                txtInteres.Text = interes.ToString("N2");
                txtCapital_Inicial.Text = cantida_lps;
                txtCod_Prestamo.Text = no_prestamo;
                dtpFecha_Inicio.Text = fecha_inicial;
                txtCant_Debe.Text = monto_pendiente;
                TxtInt_Acum.Text =int_acum.ToString("N2");
                CalcularDias();
                Calcular_InteresporDia();
            }

            //form.ShowDialog();
            //this.Hide();
        }

        double interess = 0;


        private void pbSalir_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void txtTotal_a_Pagar_TextChanged(object sender, EventArgs e)
        {
            CalculoPago();

            if (txtTotal_a_Pagar.Text == "")
            {
                txtTotal_a_Pagar.Text = "0.00";
            }


            // Remover comas y espacios en blanco
            string text = txtTotal_a_Pagar.Text.Replace(",", "").Replace(" ", "");

            // Verificar si el texto resultante es un número válido
            if (decimal.TryParse(text, out decimal monto))
            {
                // Formatea el valor como moneda y lo asigna al TextBox
                txtTotal_a_Pagar.Text = monto.ToString("N2");
                txtTotal_a_Pagar.SelectionStart = txtTotal_a_Pagar.Text.Length - 3; // Coloca el cursor a
            }
        }

        private void txtTotal_a_Pagar_KeyPress(object sender, KeyPressEventArgs e)
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
            string textWithoutKey = txtTotal_a_Pagar.Text.Remove(txtTotal_a_Pagar.SelectionStart, txtTotal_a_Pagar.SelectionLength);
            textWithoutKey = textWithoutKey.Insert(txtTotal_a_Pagar.SelectionStart, e.KeyChar.ToString());

            // Intenta convertir el texto a un valor decimal
            if (decimal.TryParse(textWithoutKey, out decimal monto))
            {
                // Formatea el valor como moneda y lo asigna al TextBox
                txtTotal_a_Pagar.Text = monto.ToString("N2");
                txtTotal_a_Pagar.SelectionStart = txtTotal_a_Pagar.Text.Length - 3; // Coloca el cursor antes de los decimales
                e.Handled = true; // Indica que ya se manejó la tecla
            }

        }

        private void btnPagar_Click(object sender, EventArgs e)
        {
            ValidateData();
            Formularios.Formularios_de_Menu.Liquidacion.Frm_Liquidacion liquidacion = new Liquidacion.Frm_Liquidacion();
            liquidacion = ((Formularios.Formularios_de_Menu.Liquidacion.Frm_Liquidacion)Owner);

            string Cprest_l = txtCod_Prestamo.Text;
            string totp_l = txtTotal_a_Pagar.Text;
            string abonopag_l = txtAbono.Text;

            string int_acumulado_l = TxtInt_Acum.Text;
            string int_x_acumular = Txt_Int_X_Acumular.Text;
            string cant_debe = txtCant_Debe.Text;
            string detalles = TxtDetalles.Text;

            //string interes_l = TxtInteres_Lps.Text;
            // string dias_l = txt

            if (Cprest_l != "")
            {

                        string stmt = "ESTADO='EN PROCESO'";
                        string condicion = "PRESTAMO_CODIGO='" + Cprest_l + "'";
                        db.Update("PRESTAMOS", stmt, condicion);

            }

            liquidacion.RecibirPagoPrestamo(Cprest_l, abonopag_l,int_acumulado_l, interes,int_x_acumular, dias, totp_l, cant_debe, detalles);
            Close();

            
        }

        private void dtpFecha_Pago_KeyDown(object sender, KeyEventArgs e)
        {
            CalculoPago();
        }

        private void txtAbono_KeyPress(object sender, KeyPressEventArgs e)
        {
        }

        private void dtpFecha_Pago_ValueChanged(object sender, EventArgs e)
        {
            CalcularDias();
            Calcular_InteresporDia();
            CalculoPago();
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

            if (int_acumulado>0)
            {
                abono_prestamo = tot_pag_prest - int_acumulado - interes;

                if (abono_prestamo < 0)
                {
                    int_x_acumular = abono_prestamo*-1;
                    Txt_Int_X_Acumular.Text = int_x_acumular.ToString("N2");
                    txtAbono.Text = 0.ToString("N2");

                }
                else
                {
                    Txt_Int_X_Acumular.Text = 0.ToString("N2");
                    txtAbono.Text = abono_prestamo.ToString("N2");
                }

            }else
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


        public FrmPagoPrestamo_Liquidacion()
        {
            InitializeComponent();
        }

        private void FrmPagoPrestamo_Liquidacion_Load(object sender, EventArgs e)
        {
            txtIdSocio.Text = id_socio;
            txtNomb_Socio.Text = nombre_socio;
            this.Text = Clases.Env.APPNAME + " | BÚSQUEDA DE PAGOS DE PRÉSTAMO | " + Clases.Auth.user + " | " + Clases.Auth.rol;

            //txtInteres.Text = interes;
            Get_Prestamo_Socio();
            //Calcular_InteresporDia();

            CalcularDias();
            //Calcular_InteresporDia();
            txtAbono.Text = abono.ToString("N2");

            Calcular_InteresporDia();
            CalculoPago();

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

        private void Get_Prestamo_Socio()
        {
            string id_socio;

            id_socio = txtIdSocio.Text;

            string query = "SELECT A.PRESTAMO_CODIGO, A.FECHA_INICIO, A.FECHA_FINAL, " +
                "C.TIPO_PRESTAMO, A.CANTIDAD_LPS, A.INTERES, A.MONTO_PENDIENTE, A.INTERES_ACUMULADO " +
                "FROM PRESTAMOS A INNER JOIN SOCIOS B ON(A.ID_SOCIO = B.ID_SOCIO) " +
                "INNER JOIN TIPO_DE_PRESTAMO C ON(A.ID_TIPO_PRESTAMO = C.ID_TIPO_PRESTAMO) " +
                "WHERE A.ID_SOCIO = '" + id_socio + "' AND A.MONTO_PENDIENTE > 0 " +
                "GROUP BY A.PRESTAMO_CODIGO, A.FECHA_INICIO, A.FECHA_FINAL, " +
                "C.TIPO_PRESTAMO, A.CANTIDAD_LPS, A.INTERES, A.MONTO_PENDIENTE, A.INTERES_ACUMULADO ORDER BY A.PRESTAMO_CODIGO ASC";

            DataTable prestamos = db.RawSQL(query);

            DgvData.Rows.Clear();

            string _presta_codigo, _fecha_ini, _fecha_fin, _tip_prestamo, _interes, _int_acum;
            string _cantidad, _monto_pendiente;

            for (int i = 0; i < prestamos.Rows.Count; i++)
            {
                _presta_codigo = prestamos.Rows[i][0].ToString();
                _fecha_ini = Convert.ToDateTime(prestamos.Rows[i][1].ToString()).ToShortDateString();
                _fecha_fin = Convert.ToDateTime(prestamos.Rows[i][2].ToString()).ToShortDateString();
                _tip_prestamo = prestamos.Rows[i][3].ToString();
                _cantidad = prestamos.Rows[i][4].ToString();
                _interes = prestamos.Rows[i][5].ToString();
                _monto_pendiente = prestamos.Rows[i][6].ToString();
                _int_acum = prestamos.Rows[i][7].ToString();

                DgvData.Rows.Add(_presta_codigo, _fecha_ini, _fecha_fin, _tip_prestamo, a.ReturnsNumber(_cantidad).ToString("N2"), _interes, a.ReturnsNumber(_monto_pendiente).ToString("N2"), _int_acum);

                //lblResumen.Text = TotalDeuda.ToString("N2");
                prestamos.Dispose();
            }
        }
        public void ValidateData()
        {
            errors = 0;
            no_prestamo = a.Clean(txtCod_Prestamo.Text.Trim());
            fecha_anterior = a.Clean(dtpFecha_Inicio.Text.Trim());
            fecha_pago = a.Clean(dtpFecha_Pago.Text.Trim());
            nombre_socio = a.Clean(txtNomb_Socio.Text.Trim());
            cantidad_lps = txtCapital_Inicial.Text;
            monto_pendi = a.Clean(txtCant_Debe.Text.Trim());
            interes_porcentaje = txtInteres.Text;
            cant_pagar = a.Clean(txtAbono.Text.Trim());
            detalles = a.Clean(TxtDetalles.Text.Trim());

            dias = a.Clean(txtDias.Text.Trim());
            interes = a.Clean(TxtInteres_Lps.Text.Trim());

            if (fecha_pago.Length == 0)
            {
                a.Advertencia("¡SELECCIONE UNA FECHA FINAL VÁLIDA!");
                dtpFecha_Pago.Focus();
                errors++;
                return;
            }

            //Si ambas fechas son iguales mostrar el mensaje
            if (fecha_anterior.Equals(fecha_pago))
            {
                a.Advertencia("¡LA FECHA ANTERIOR Y LA FECHA DE PAGO SON LAS MISMAS!");
                dtpFecha_Pago.Focus();
                errors++;
                return;
            }


            if (no_prestamo.Length == 0)
            {
                a.Advertencia("¡CÓDIGO DE PRÉSTAMO VÁLIDO!");
                txtCod_Prestamo.Text = "";
                txtCod_Prestamo.Focus();
                errors++;
                return;
            }

            if (nombre_socio.Length == 0)
            {
                a.Advertencia("¡EL NOMBRE DEL SOCIO ES REQUERIDO!");
                txtNomb_Socio.Text = "";
                txtNomb_Socio.Focus();
                errors++;
                return;
            }

            if (cantidad_lps.Length == 0)
            {
                a.Advertencia("¡LA CANTIDAD DE LEMPIRAS CORRESPONDIENTE AL PRÉSTAMO ES REQUERIDA!");
                txtCapital_Inicial.Text = "";
                txtCapital_Inicial.Focus();
                errors++;
            }


            if (monto_pendi.Length == 0)
            {
                a.Advertencia("¡LA CANTIDAD DE LEMPIRAS PENDIENTE ES REQUERIDA!");
                txtCant_Debe.Text = "";
                txtCant_Debe.Focus();
                errors++;
            }

            if (interes_porcentaje.Length == 0)
            {
                a.Advertencia("¡EL INTERÉS PORCENTUAL ES REQUERIDO!");
                txtInteres.Text = "";
                txtInteres.Focus();
                errors++;
                return;
            }

            if (cant_pagar.Length == 0)
            {
                a.Advertencia("¡INGRESE LA CANTIDAD DE LEMPIRAS CORRESPONDIENTE A PAGAR!");
                txtAbono.Text = "";
                txtAbono.Focus();
                errors++;
            }

            if (dias.Length == 0)
            {
                a.Advertencia("¡LA CANTIDAD DE DÍAS ES REQUERIDA!");
                txtDias.Text = "";
                txtDias.Focus();
                errors++;
            }

            if (interes.Length == 0)
            {
                a.Advertencia("¡EL INTERÉS CALCULADO HASTA EL MOMENTO ES REQUERIDO!");
                TxtInteres_Lps.Text = "";
                TxtInteres_Lps.Focus();
                errors++;
            }


            if (detalles.Length == 0)
            {
                TxtDetalles.Text = "Ninguno";

            }

        }
    }
}
