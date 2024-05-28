using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Microsoft.VisualBasic;

using System.Runtime.InteropServices;

namespace SC__NEBO.Formularios.Formularios_de_Menu.Prestamos
{
    public partial class Frm_Prestamos_por_Persona : Form
    {
        Clases.Moneda mon = new Clases.Moneda();
        Clases.DB db = new Clases.DB();
        Clases.Asistente a = new Clases.Asistente();

        public string no_prestamo, fecha_anterior, nombre_socio;
        public string cantida_lps, interes, monto_pendi;
        int errors;

        string id_pago, fecha_pago, id_tipo_pago, formapago, cant_pagar, dias, interes_porcentaje, detalles, totalpagar;

        public Frm_Prestamos_por_Persona()
        {
            InitializeComponent();
        }

        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();

        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);

        private void Frm_Prestamos_por_Persona_Load(object sender, EventArgs e)
        {
            txtCod_Prestamo.Text = no_prestamo;
            dtpFecha_Inicio.Text = fecha_anterior;
            txtNomb_Socio.Text = nombre_socio;

            txtIdPago.Text = "PGS" + db.GetNext("PAGOS");

            txtCant_Debe.Text = monto_pendi;
            txtCapital_Inicial.Text = cantida_lps;
            txtInteres.Text = interes;
            Prest(no_prestamo);
            Tipo_Prestamo();
            CalcularDias();
            Calcular_InteresporDia();
            txtAbono.Text = abono.ToString("N2");
            
        }

        public void Prest(string COD_PRES)
        {
            string condicion = "PRESTAMO_CODIGO='" + COD_PRES + "'";
            dtpFecha_Inicio.Text = db.Hook("FECHA_ULTIMO_PAGO", "PRESTAMOS", condicion);

        }

        //Procedimiento para restar el pago al monto pendiente en la tabla de prestamos.
        private void Restar_PagoPendiente_de_Prestamo()
        {
            string valores, condicion;

            condicion = "PRESTAMO_CODIGO = '" + no_prestamo + "'";
            valores = "MONTO_PENDIENTE = MONTO_PENDIENTE - " + abono + "";
            db.Update("PRESTAMOS", valores, condicion);

        }

        private void btnPagar_Click(object sender, EventArgs e)
        {
            ValidateData();

            string msg = "¿DESEA GUARDAR ESTE PRÉSTAMO AL SOCIO " + nombre_socio + "?";

            if (errors == 0)
            {
                if (a.Pregunta(msg) == true)
                {
                    //No se si ya se creo esta tabla
                    string campos = "ID_PAGO, PRESTAMO_CODIGO, FECHA, ID_FORMA_PAGO, MONTO_PAGO, INTERES, DIAS, CANTIDAD_EN_LETRAS, OBSERVACIONES";

                    string valores = "'" + id_pago + "', '" + no_prestamo + "','" + fecha_pago + "','" + id_tipo_pago + "','" + abono + "'," + a.ReturnsNumber(interes) + "," + dias + ",'" + mon.enletras(totalpagar) + "','" + detalles + "'";

                    string stmt= "FECHA_ULTIMO_PAGO='"+fecha_pago+"'";
                    string condicion= "PRESTAMO_CODIGO='"+no_prestamo+"'";

                    if (db.Save("PAGOS", campos, valores) > 0)
                    {
                        //db.RawSQL(campos);
                        db.SetLast("PAGOS");
                        a.Aprueba("EL PAGO DEL SOCIO " + nombre_socio + " HA SIDO REGISTRADO CON ÉXITO!");
                        //Clear();
                        Boot();
                        Restar_PagoPendiente_de_Prestamo();
                        if (txtCant_Debe.Text == txtAbono.Text)
                        {
                            Estado();
                        }
                        db.Update("PRESTAMOS", stmt, condicion);

                        Reportes.FrmRptComprobanteIngreso ingreso = new Reportes.FrmRptComprobanteIngreso();
                        ingreso.codpago = id_pago;
                        ingreso.Show();

                        Close();
                    }

                }
            }
        }

        private void Estado()
        {
            string valores, condicion;

            condicion = "PRESTAMO_CODIGO = '" + no_prestamo + "'";
            valores = "ESTADO = 'PAGADO'";
            db.Update("PRESTAMOS", valores, condicion);

        }

        private void Boot()
        {
            txtCod_Prestamo.Enabled = false;
            //btnNuevo.Enabled = Clases.Auth.save == "S" ? true : false;
            txtAbono.Enabled = false;

            dtpFecha_Inicio.Enabled = false;
            dtpFecha_Pago.Enabled = true;            

            //lblMsg.Visible = false;

        }


        private void txtAbono_KeyPress(object sender, KeyPressEventArgs e)
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
            string textWithoutKey = txtAbono.Text.Remove(txtAbono.SelectionStart, txtAbono.SelectionLength);
            textWithoutKey = textWithoutKey.Insert(txtAbono.SelectionStart, e.KeyChar.ToString());

            // Intenta convertir el texto a un valor decimal
            if (decimal.TryParse(textWithoutKey, out decimal monto))
            {
                // Formatea el valor como moneda y lo asigna al TextBox
                txtAbono.Text = monto.ToString("N2");
                txtAbono.SelectionStart = txtAbono.Text.Length - 3; // Coloca el cursor antes de los decimales
                e.Handled = true; // Indica que ya se manejó la tecla
            }

        }

        private void txtAbono_Leave(object sender, EventArgs e)
        {
            if (txtAbono.Text == "")
            {
                txtAbono.Text = "0.00";
            }
        }

        private void dtpFecha_Pago_ValueChanged(object sender, EventArgs e)
        {
            CalcularDias();
            CalculodePago();
            Calcular_InteresporDia();

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void dtpFecha_Pago_KeyUp(object sender, KeyEventArgs e)
        {
            CalcularDias();
            CalculodePago();
            Calcular_InteresporDia();
        }

        private void dtpFecha_Pago_KeyDown(object sender, KeyEventArgs e)
        {
            CalculodePago();
        }

        //MOSTRAR LAS FORMAS DE PAGOS en el combobox
        private void Tipo_Prestamo()
        {
            cmbForma_Pago.DataSource = db.Find("FORMA_de_PAGO", "ID_FORMA_PAGO, FORMA_PAGO", "", "FORMA_PAGO");
            cmbForma_Pago.ValueMember = "ID_FORMA_PAGO";
            cmbForma_Pago.DisplayMember = "FORMA_PAGO";
            cmbForma_Pago.SelectedIndex = -1;
           
        }

        double abono;
        private void txtAbono_TextChanged(object sender, EventArgs e)
        {

            CalculodePago();

            // Remover comas y espacios en blanco
            string text = txtAbono.Text.Replace(",", "").Replace(" ", "");

            // Verificar si el texto resultante es un número válido
            if (decimal.TryParse(text, out decimal monto))
            {
                // Formatea el valor como moneda y lo asigna al TextBox
                txtAbono.Text = monto.ToString("N2");
                txtAbono.SelectionStart = txtAbono.Text.Length - 3; // Coloca el cursor antes de los decimales
            }
        }

        private void CalculodePago()
        {
            double interes = 0, total_a_pagar = 0;

            if (txtAbono.Text == "")
            {
                txtAbono.Text = "0.00";
            }

            abono = Convert.ToDouble(txtAbono.Text.ToString());
            interes = Convert.ToDouble(txtInteres_Lps.Text.ToString());

            total_a_pagar = abono + interes;

            txtTotal_a_Pagar.Text = total_a_pagar.ToString("N2");
        }

        public void ValidateData()
        {
            errors = 0;
            id_pago = a.Clean(txtIdPago.Text.Trim());
            no_prestamo = a.Clean(txtCod_Prestamo.Text.Trim());
            fecha_anterior = a.Clean(dtpFecha_Inicio.Text.Trim());
            fecha_pago = a.Clean(dtpFecha_Pago.Text.Trim());
            nombre_socio = a.Clean(txtNomb_Socio.Text.Trim());
            cantida_lps = txtCapital_Inicial.Text;
            monto_pendi = a.Clean(txtCant_Debe.Text.Trim());
            interes_porcentaje = txtInteres.Text;
            cant_pagar = a.Clean(txtAbono.Text.Trim());
            detalles = a.Clean(TxtDetalles.Text.Trim());
            totalpagar = a.Clean(txtTotal_a_Pagar.Text.Trim()); 

            id_tipo_pago = cmbForma_Pago.Text != "" ? cmbForma_Pago.SelectedValue.ToString() : "";
            dias = a.Clean(txtDias.Text.Trim());
            interes = a.Clean(txtInteres_Lps.Text.Trim());

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

            if (cantida_lps.Length == 0)
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
                txtInteres_Lps.Text = "";
                txtInteres_Lps.Focus();
                errors++;
            }

            Double montopen = Convert.ToDouble(txtCant_Debe.Text.ToString());
            Double montopagar = Convert.ToDouble(txtAbono.Text.ToString());

            if (montopagar > montopen)
            {
                a.Advertencia("LA CANTIDAD A DE LEMPIRAS PAGAR ES MAYOR A LA CANTIDAD DE LEMPIRAS QUE SE DEBE");
                txtAbono.Focus();
                errors++;
            }

            if (detalles.Length == 0)
            {
                TxtDetalles.Text = "NINGUNO";

            }

        }

        private void CalcularDias()
        {
            //DateTime f_inicial = dtpFecha_Inicio.Value; // Fecha de hoy
            //DateTime f_final = dtpFecha_Pago.Value; // Fecha seleccionada por el usuario
            //int cantidad_dias = (int)(f_final - f_inicial).TotalDays;

            //txtDias.Text = cantidad_dias.ToString(); // Muestra la cantidad de días en un TextBox

            DateTime fechaInicio = dtpFecha_Inicio.Value;
            DateTime fechaFin = dtpFecha_Pago.Value;

            int dias = db.Days360(fechaInicio, fechaFin); // Devuelve 360
            txtDias.Text = dias.ToString();

        }

        private void Calcular_InteresporDia()
        {
            //int cant_dias;
            double cant_diass =0;
            double interess =0;
            double cant_lempiras=0;

            cant_diass = Convert.ToInt32(txtDias.Text.ToString());
            interess = Convert.ToInt32(txtInteres.Text.ToString());
            cant_lempiras = Convert.ToDouble(txtCapital_Inicial.Text.ToString());

            //Intereses
            double interes_pago = ((cant_lempiras * (interess / 100))/360)* cant_diass;

            //Aqui se obtiene los intereses hasta la fecha
            txtInteres_Lps.Text = interes_pago.ToString("N2");


        }

        private void panel2_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            //DateTime f_inicial = dtpFecha_Inicio.Value; // Fecha de hoy
            //DateTime f_final = dtpFecha_Pago.Value; // Fecha seleccionada por el usuario
            //int cantidad_dias = (int)(f_final - f_inicial).TotalDays;

            //txtDias.Text = cantidad_dias.ToString(); // Muestra la cantidad de días en un TextBox

            CalcularDias();
            Calcular_InteresporDia();
        }

        private void pbSalir_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void label11_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

    }
}
