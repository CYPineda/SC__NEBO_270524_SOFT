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

namespace SC__NEBO.Formularios.Formularios_de_Menu.Prestamos
{
    public partial class Frm_Prestamos : Form
    {
        public Frm_Prestamos()
        {
            InitializeComponent();
        }

        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();

        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);


        Clases.DB db = new Clases.DB();
        Clases.Asistente a = new Clases.Asistente();
        Clases.Moneda mon = new Clases.Moneda(); 

        int errors;

        string prestamo_codigo, fecha_inicio, fecha_final;
        string cliente,id_socio, identidad, telefono;

        string id_tipo_prestamo, id_plazo, cantidad_lps, interes, forma_pago, interes_moratorio, garantia, num_cuotas, entregado_por, observaciones;

        DateTime fechaDentroDeUnMes;

        private void btnRegresar_Click(object sender, EventArgs e)
        {
            Formularios.Formularios_de_Menu.Prestamos.Frm_Reporte_Prestamo form = new Formularios_de_Menu.Prestamos.Frm_Reporte_Prestamo();
            this.AddOwnedForm(form);
            form.Show();
            this.Hide();
        }

        private void btnBuscar_Cliente_Click(object sender, EventArgs e)
        {
            Formularios.Formularios_de_Menu.Clientes.Frm_Lista_Clientes lista_clientes = new Clientes.Frm_Lista_Clientes();
            this.AddOwnedForm(lista_clientes);
            lista_clientes.Show();
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
                string id_pres = txtPrestamo_Codigo.Text.Trim();

                if (db.Delete("PRESTAMOS", "PRESTAMO_CODIGO", id_pres) > 0)
                {
                    a.Aprueba("EL PRÉSTAMO SE ELIMINÓ CON EXITO");
                    Clear();
                    Boot();
                    GetSocio();
                }
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            ValidateData();

            string msg = "¿DESEA GUARDAR ESTE PRÉSTAMO AL SOCIO " + cliente + "?";

            if (errors == 0)
            {
                if (a.Pregunta(msg) == true)
                {
                    fechaDentroDeUnMes = Convert.ToDateTime(fecha_inicio).AddDays(30);
                    double cant_lps;

                    //No se si ya se creo esta tabla
                    string campos = "PRESTAMO_CODIGO, FECHA_INICIO, FECHA_INICIO_PAGO, FECHA_ULTIMO_PAGO, FECHA_FINAL, ID_SOCIO, ID_PLAZO, ID_TIPO_PRESTAMO," +
                        " CANTIDAD_LPS, CANTIDAD_EN_LETRAS, INTERES, ID_FORMA_PAGO, MONTO_PENDIENTE, GARANTIA, ENTREGADO_POR, INTERES_ACUMULADO, OBSERVACIONES";

                    cantidad_lps = cantidad_lps.Replace(",", "");
                    cant_lps = double.Parse(cantidad_lps);

                    double int_acum = cant_lps * (Convert.ToDouble(interes)/100);

                    string valores = "'" + prestamo_codigo + "', '" + fecha_inicio + "', '" + fechaDentroDeUnMes + "', '" + fecha_inicio + "','" + fecha_final + "','" + id_socio + "','" + id_plazo + "'," +
                        "'" + id_tipo_prestamo + "', " + cant_lps + ", '" + mon.enletras(cantidad_lps) + "','" + interes + "', '" + forma_pago + "','" + cantidad_lps + "'," +
                        "'" + garantia + "', '" + Clases.Auth.user + "',"+ int_acum + ", '" + observaciones + "'";
                    
                    string cod_prestamo = prestamo_codigo;
                    if (db.Save("PRESTAMOS", campos, valores) > 0)
                    {
                        //db.RawSQL(query);
                        db.SetLast("PREST");
                        a.Aprueba("EL PRÉSTAMO DEL SOCIO " + cliente + " HA SIDO REGISTRADO CON ÉXITO!");

                        Reportes.FrmRptPagare pagare = new Reportes.FrmRptPagare();
                        pagare.codprestamo = cod_prestamo;
                        pagare.Show();

                        Reportes.FrmRptComprobanteEgreso egreso = new Reportes.FrmRptComprobanteEgreso();
                        egreso.codprestamo = cod_prestamo;
                        egreso.Show();


                        Clear();
                        Boot();
                        GetSocio();
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
                    fechaDentroDeUnMes = Convert.ToDateTime(fecha_inicio).AddDays(30);
                    double cant_lps;

                    string id_cliente = txtPrestamo_Codigo.Text.Trim();

                    cantidad_lps = cantidad_lps.Replace(",", "");
                    cant_lps = double.Parse(cantidad_lps);
                    string stmt = "FECHA_INICIO='" + fecha_inicio + "', FECHA_ULTIMO_PAGO='" + fecha_inicio + "', FECHA_FINAL='" + fecha_final + "', ID_SOCIO='" + id_socio + "', " +
                    "ID_PLAZO='" + id_plazo + "', " +
                    "ID_TIPO_PRESTAMO='" + id_tipo_prestamo + "', CANTIDAD_LPS=" + cant_lps + ", INTERES='" + interes + "', " +
                    "ID_FORMA_PAGO='" + forma_pago + "', MONTO_PENDIENTE=" + cantidad_lps + ", GARANTIA='" + garantia + "', " +
                    "ENTREGADO_POR='" + Clases.Auth.user + "', OBSERVACIONES= '"+observaciones+"'";


                    string condicion = "PRESTAMO_CODIGO='" + id_cliente + "'";
                    string cod_prestamo = prestamo_codigo;
                    if (db.Update("PRESTAMOS", stmt, condicion) > 0)
                    {
                        a.Aprueba("LOS CAMBIOS SE APLICARON CORRECTAMENTE!");

                        Reportes.FrmRptPagare pagare = new Reportes.FrmRptPagare();
                        pagare.codprestamo = cod_prestamo;
                        pagare.Show();

                        Reportes.FrmRptComprobanteEgreso egreso = new Reportes.FrmRptComprobanteEgreso();
                        egreso.codprestamo = cod_prestamo;
                        egreso.Show();

                        Clear();
                        Boot();
                        GetSocio();
                    }
                }
            }
        }

        private void pbSalir_Click(object sender, EventArgs e)
        {
            Close();
        }

        int numero_cuotas=0;
        private void dtpFecha_Final_ValueChanged(object sender, EventArgs e)
        {
            DateTime f_inicial = dtpFecha_Inicio.Value; // Fecha de hoy
            DateTime f_final = dtpFecha_Final.Value; // Fecha seleccionada por el usuario
            int cantidad_dias = (int)(f_final - f_inicial).TotalDays;

            //txtNum_Cuotas.Text = cantidad_dias.ToString(); // Muestra la cantidad de días en un TextBox

        }

        private void dtpFecha_Inicio_ValueChanged(object sender, EventArgs e)
        {
            //DateTime f_inicial = DateTime.Today; // Fecha de hoy
            DateTime f_inicial = dtpFecha_Inicio.Value; // Fecha de hoy
            DateTime f_final = dtpFecha_Final.Value; // Fecha seleccionada por el usuario
            int cantidad_cuotas = f_final.Year - f_inicial.Year; // Resta los años de ambas fechas

            if (f_inicial.Day < f_final.Day || (f_inicial.Month == f_final.Month && f_inicial.Day < f_final.Day))
            {
                cantidad_cuotas++; // Resta un año si la fecha actual es menor que la fecha seleccionada
            }

            //txtNum_Cuotas.Text = cantidad_cuotas.ToString(); // Muestra la cantidad de años en un TextBox

            numero_cuotas = cantidad_cuotas;
        }

        private void cmbTipo_Prestamo_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cmbPlazo_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            if (cmbPlazo.SelectedIndex == 0)
            {
                dtpFecha_Final.MaxDate = DateTime.Today.AddYears(1);
            }
            else
            {
                dtpFecha_Final.MaxDate = new DateTime(9998, 12, 31);
            }
            //else if (cmbPlazo.SelectedIndex == 1)
            //{
            //    dtpFecha_Final.MaxDate = DateTime.MaxValue;

            //}
                        
        }

        private void txtNombre_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnBuscar.PerformClick();
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            BuscarPrestamo(a.Clean(txtNombre.Text.Trim()));
        }

        private void DgvData_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (DgvData.Rows.Count > 0)
            {
                string id = DgvData.CurrentRow.Cells[0].Value.ToString();
                GetInfoPrestamos(id);
            }
        }

        private void GetInfoPrestamos(string id)
        {
            string condicion = "PRESTAMO_CODIGO='" + id + "' AND DEL='N'";
            DataTable cliente = db.Find("PRESTAMOS", "*", condicion);

            if (cliente.Rows.Count > 0)
            {
                btnNuevo.Enabled = false;
                btnGuardar.Enabled = false;
                btnActualizar.Enabled = Clases.Auth.update == "S" ? true : false;
                btnEliminar.Enabled = Clases.Auth.delete == "S" ? true : false;
                btnCancelar.Enabled = true;


                DataRow info = cliente.Rows[0];
                txtPrestamo_Codigo.Text = info["PRESTAMO_CODIGO"].ToString();
                dtpFecha_Inicio.Text = info["FECHA_INICIO"].ToString();
                dtpFecha_Final.Text = info["FECHA_FINAL"].ToString();
                TxtIdSocio.Text = info["ID_SOCIO"].ToString();
                
                //Traer la informacion del socio
                string id_socio = TxtIdSocio.Text;
                string condicion_="ID_SOCIO='"+id_socio+"'";

                txtSocio.Text = db.Hook("NOMBRE", "SOCIOS", condicion_);
                txtIdentidad.Text = db.Hook("DNI", "SOCIOS", condicion_);
                txtTelefono.Text = db.Hook("TELEFONO", "SOCIOS", condicion_);
                txtDireccion.Text = db.Hook("DIRECCION", "SOCIOS", condicion_);


                cmbPlazo.SelectedValue = info["ID_PLAZO"].ToString();
                cmbTipo_Prestamo.SelectedValue = info["ID_TIPO_PRESTAMO"].ToString();
                txtCantidad_Lps.Text = info["CANTIDAD_LPS"].ToString();
                txtIntereses.Text = info["INTERES"].ToString();
                cmbForma_Pago.SelectedValue = info["ID_FORMA_PAGO"].ToString();
                cmbGarantia.Text = info["GARANTIA"].ToString();
                txtObservaciones.Text = info["OBSERVACIONES"].ToString();


                txtPrestamo_Codigo.Enabled = false;
                dtpFecha_Inicio.Enabled = Clases.Auth.update == "S" ? true : false;
                dtpFecha_Final.Enabled = Clases.Auth.update == "S" ? true : false;
                TxtIdSocio.Enabled = Clases.Auth.update == "S" ? true : false;
                txtDireccion.Enabled = Clases.Auth.update == "S" ? true : false;
                cmbPlazo.Enabled = Clases.Auth.update == "S" ? true : false;
                cmbTipo_Prestamo.Enabled = Clases.Auth.update == "S" ? true : false;
                txtCantidad_Lps.Enabled = Clases.Auth.update == "S" ? true : false;
                txtIntereses.Enabled = Clases.Auth.update == "S" ? true : false;
                cmbForma_Pago.Enabled = Clases.Auth.update == "S" ? true : false;
                cmbGarantia.Enabled = Clases.Auth.update == "S" ? true : false;

                txtNombre.Enabled = Clases.Auth.update == "S" ? true : false;
                txtIdentidad.Enabled = Clases.Auth.update == "S" ? true : false;
                txtTelefono.Enabled = Clases.Auth.update == "S" ? true : false;
                txtDireccion.Enabled = Clases.Auth.update == "S" ? true : false;
                txtObservaciones.Enabled = Clases.Auth.update == "S" ? true : false;

                TxtIdSocio.Enabled = false;
                txtIdentidad.Enabled = false;
                txtTelefono.Enabled = false;
                txtDireccion.Enabled = false;
            }
            else
            {
                a.Advertencia("ERROR AL RECUPERAR LOS DATOS DEL REGISTRO SELECCIONADO");

            }
            cliente.Dispose();
        }

        private void BtnImprimir_Click(object sender, EventArgs e)
        {
            //Reportes.Frm_Rpt_ListaPrestamos lista = new Reportes.Frm_Rpt_ListaPrestamos();
            //lista.Show();

        }

        double cant;

        private void txtCantidad_Lps_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Permitir solo dígitos, el punto decimal y la tecla de retroceso
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != '.' && e.KeyChar != ',' && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true;
                return;
            }

            // Convertir la coma en punto
            if (e.KeyChar == ',')
            {
                e.KeyChar = '.';
            }

            // Si se presiona un dígito y hay texto seleccionado, reemplazarlo con el dígito presionado
            if (char.IsDigit(e.KeyChar) && txtCantidad_Lps.SelectionLength > 0)
            {
                int selectionStart = txtCantidad_Lps.SelectionStart;
                string newText = txtCantidad_Lps.Text.Remove(selectionStart, txtCantidad_Lps.SelectionLength);
                newText = newText.Insert(selectionStart, e.KeyChar.ToString());
                txtCantidad_Lps.Text = newText;
                txtCantidad_Lps.SelectionStart = selectionStart + 1;
                e.Handled = true;
                return;
            }

            // Agregar automáticamente comas como separadores de miles
            if (char.IsDigit(e.KeyChar))
            {
                // Obtener el texto actual y el índice de la selección
                string text = txtCantidad_Lps.Text;
                int selectionStart = txtCantidad_Lps.SelectionStart;

                // Insertar el dígito presionado en la posición adecuada
                text = text.Insert(selectionStart, e.KeyChar.ToString());

                // Remover todas las comas y obtener la parte entera
                string[] parts = text.Replace(",", "").Split('.');

                // Formatear la parte entera con comas como separadores de miles
                parts[0] = string.Format("{0:#,0}", long.Parse(parts[0]));

                // Reconstruir el texto con las comas agregadas
                txtCantidad_Lps.Text = string.Join(".", parts);

                // Mover el cursor al final del texto
                txtCantidad_Lps.SelectionStart = txtCantidad_Lps.Text.Length;

                e.Handled = true;
            }

        }

        private void txtCantidad_Lps_TextChanged(object sender, EventArgs e)
        {
            //// Remover comas y espacios en blanco
            //string text = txtCantidad_Lps.Text.Replace(",", "").Replace(" ", "");

            //// Verificar si el texto resultante es un número válido
            //if (decimal.TryParse(text, out decimal monto))
            //{
            //    // Formatea el valor como moneda y lo asigna al TextBox
            //    txtCantidad_Lps.Text = monto.ToString("#,0.##"); // Esto permitirá al menos dos cifras después del punto decimal
            //    txtCantidad_Lps.SelectionStart = txtCantidad_Lps.Text.Length; // Coloca el cursor al final del texto
            //}
        }

        private void panel2_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            btnNuevo.Enabled = false;
            btnGuardar.Enabled = Clases.Auth.save == "S" ? true : false;
            btnActualizar.Enabled = false;
            btnEliminar.Enabled = false;
            btnCancelar.Enabled = true;
            btnRegresar.Enabled = false;

            txtPrestamo_Codigo.Text = "PRT" + db.GetNext("PREST");

            btnBuscar_Cliente.Enabled = true;
            dtpFecha_Inicio.Enabled = true;
            dtpFecha_Final.Enabled = true;
            txtPrestamo_Codigo.Enabled = false;
            cmbTipo_Prestamo.Enabled = true;
            cmbPlazo.Enabled = true;
            txtCantidad_Lps.Enabled = true;
            txtIntereses.Enabled = true;
            //txtNum_Cuotas.Enabled = false;
            cmbGarantia.Enabled = true;
            cmbForma_Pago.Enabled = true;
            txtObservaciones.Enabled = true;

            //txtNombre.Focus();
        }

        private void Frm_Prestamos_Load(object sender, EventArgs e)
        {
            Boot();
            Datos_Prestamo();
            //habilitaPlazo();
            GetSocio();

            txtCantidad_Lps.Text = cant.ToString("N2");
        }

        private void GetSocio()
        {
            String query = "SELECT A.PRESTAMO_CODIGO, B.NOMBRE, A.FECHA_INICIO, A.CANTIDAD_LPS" +
                " FROM PRESTAMOS A INNER JOIN SOCIOS B " +
                "ON(B.ID_SOCIO = A.ID_SOCIO) WHERE  A.MONTO_PENDIENTE > 0 AND A.FECHA_INICIO = A.FECHA_ULTIMO_PAGO AND A.DEL = 'N'" +
                "GROUP BY A.PRESTAMO_CODIGO, B.NOMBRE, A.FECHA_INICIO, A.CANTIDAD_LPS, " +
                "B.NOMBRE ORDER BY A.PRESTAMO_CODIGO DESC ";


            DataTable reporte = db.RawSQL(query);

            string _idprestamo, _socio, _fechaini, _monto;
            int i;

            DgvData.Rows.Clear();

            for (i = 0; i < reporte.Rows.Count; i++)
            {
                _idprestamo = reporte.Rows[i][0].ToString();
                _socio = reporte.Rows[i][1].ToString();
                _fechaini = Convert.ToDateTime(reporte.Rows[i][2]).ToShortDateString();
                _monto = reporte.Rows[i][3].ToString();
                
                DgvData.Rows.Add(_idprestamo, _socio, _fechaini, a.ReturnsNumber(_monto).ToString("N2"));
            }

            reporte.Dispose();
        }


        private void BuscarPrestamo(string search = "")
        {
            string campos, condicion;
            campos = " A.PRESTAMO_CODIGO, B.NOMBRE, A.FECHA_INICIO, A.CANTIDAD_LPS" +
                " FROM PRESTAMOS A INNER JOIN SOCIOS B " +
                "ON(B.ID_SOCIO = A.ID_SOCIO) ";

            if (search != "")
            {
                condicion = "B.NOMBRE LIKE '%" + search + "%' AND A.FECHA_INICIO = A.FECHA_ULTIMO_PAGO";

            }
            else
            {
                condicion = "A.DEL = 'N'";
            }

            DataTable data = db.Join(campos, condicion, "B.NOMBRE");

            DgvData.Rows.Clear();

            int i;
            string _id_empleado, _nombre, _telefono, _area;

            for (i = 0; i < data.Rows.Count; i++)
            {
                _id_empleado = data.Rows[i][0].ToString();
                _nombre = data.Rows[i][1].ToString();
                _telefono = data.Rows[i][2].ToString();
                _area = data.Rows[i][3].ToString();
                DgvData.Rows.Add(_id_empleado, _nombre, _telefono, _area);
            }

            //lblResumen.Text = "Mostrando " + data.Rows.Count.ToString() + " registros de " + db.Count("SOCIOS", "DEL = 'N'").ToString();
            data.Dispose();
        }

        private void Datos_Prestamo()
        {
            Tipo_Prestamo();
            Forma_Pago();
            Tipo_Plazo();
        }

        //MOSTRAR LOS TIPOS DE PRESTAMOS en el combobox
        private void Tipo_Prestamo()
        {
            cmbTipo_Prestamo.DataSource = db.Find("TIPO_DE_PRESTAMO", "ID_TIPO_PRESTAMO, TIPO_PRESTAMO", "", "TIPO_PRESTAMO");
            cmbTipo_Prestamo.ValueMember = "ID_TIPO_PRESTAMO";
            cmbTipo_Prestamo.DisplayMember = "TIPO_PRESTAMO";
            cmbTipo_Prestamo.SelectedIndex = -1;
        }

        //MOSTRAR LOS TIPOS DE PRESTAMOS en el combobox
        private void Forma_Pago()
        {
            cmbForma_Pago.DataSource = db.Find("FORMA_DE_PAGO", "ID_FORMA_PAGO, FORMA_PAGO", "", "FORMA_PAGO");
            cmbForma_Pago.ValueMember = "ID_FORMA_PAGO";
            cmbForma_Pago.DisplayMember = "FORMA_PAGO";
            cmbForma_Pago.SelectedIndex = -1;
        }

        //MOSTRAR LOS TIPOS DE PLAZOS DE PRESTAMOS en el combobox
        private void Tipo_Plazo()
        {
            cmbPlazo.DataSource = db.Find("PLAZO_PRESTAMO", "ID_PLAZO, PLAZO", "", "PLAZO");
            cmbPlazo.ValueMember = "ID_PLAZO";
            cmbPlazo.DisplayMember = "PLAZO";
            cmbPlazo.SelectedIndex = -1;
        }

        //private void habilitaPlazo()
        //{
        //    //string opcionSeleccionada = cmbPlazo.SelectedItem.ToString();

        //    //if (cmbPlazo.SelectedIndexChanged == "")
        //    //{

        //    //}

        //}

        public void CodSocio(string cod_socio)
        {
            string condicion = "ID_SOCIO='" + cod_socio + "'";
            TxtIdSocio.Text = cod_socio;
            txtSocio.Text = db.Hook("NOMBRE", "SOCIOS", condicion);
            txtIdentidad.Text = db.Hook("DNI", "SOCIOS", condicion);
            txtTelefono.Text = db.Hook("TELEFONO", "SOCIOS", condicion);
            txtDireccion.Text = db.Hook("DIRECCION", "SOCIOS", condicion);

        }


        private void Clear()
        {
            TxtIdSocio.Clear();

            txtPrestamo_Codigo.Clear();
            dtpFecha_Inicio.Text="";
            dtpFecha_Final.Text="";
            txtSocio.Clear();
            txtIdentidad.Clear();
            txtTelefono.Clear();
            txtDireccion.Clear();
            txtObservaciones.Clear();

            cmbPlazo.SelectedIndex = -1;

            cmbTipo_Prestamo.SelectedIndex = -1;
            txtCantidad_Lps.Clear();
            txtIntereses.Clear();
            //txtInteres_Moratorio.Clear();
            cmbGarantia.SelectedIndex = -1;
            cmbForma_Pago.SelectedIndex = -1;
        }

        public void ValidateData()
        {
            errors = 0;
            prestamo_codigo = a.Clean(txtPrestamo_Codigo.Text.Trim());
            fecha_inicio = a.Clean(dtpFecha_Inicio.Text.Trim());
            fecha_final = a.Clean(dtpFecha_Final.Text.Trim());
            cliente = a.Clean(txtSocio.Text.Trim());
            id_socio = a.Clean(TxtIdSocio.Text.Trim());
            identidad = a.Clean(txtIdentidad.Text.Trim());
            telefono = a.Clean(txtTelefono.Text.Trim());
            fecha_inicio = a.Clean(dtpFecha_Inicio.Text.Trim());
            fecha_final = a.Clean(dtpFecha_Final.Text.Trim());

            id_tipo_prestamo = cmbTipo_Prestamo.Text != "" ? cmbTipo_Prestamo.SelectedValue.ToString() : "";
            // id_tipo_prestamo = a.Clean(cmbTipo_Prestamo.Text.Trim());
            id_plazo = cmbPlazo.Text != "" ? cmbPlazo.SelectedValue.ToString() : "";
          //  id_plazo = a.Clean(cmbPlazo.Text.Trim());
            cantidad_lps = a.Clean(txtCantidad_Lps.Text.Trim());
            interes = txtIntereses.Text.Trim();
            garantia = a.Clean(cmbGarantia.Text.Trim());
            forma_pago = cmbForma_Pago.Text != "" ? cmbForma_Pago.SelectedValue.ToString() : "";
            //forma_pago = a.Clean(cmbForma_Pago.Text.Trim());
            num_cuotas = a.Clean(cmbForma_Pago.Text.Trim());
            observaciones = a.Clean(txtObservaciones.Text.Trim());

            //Si ambas fechas son iguales mostrar el mensaje
            if (fecha_inicio.Equals(fecha_final))
            {
                a.Advertencia("¡LA FECHA INICIAL Y LA FECHA FINAL SON LAS MISMAS!");
                dtpFecha_Final.Focus();
                errors++;
                return;
            }


            if (prestamo_codigo.Length == 0)
            {
                a.Advertencia("¡INGRESAR UN CÓDIGO DE PRÉSTAMO VÁLIDO!");
                txtPrestamo_Codigo.Text = "";
                txtPrestamo_Codigo.Focus();
                errors++;
                return;
            }

            if (fecha_final.Length == 0)
            {
                a.Advertencia("¡SELECCIONE UNA FECHA FINAL VÁLIDA!");
                dtpFecha_Final.Text = fecha_inicio;
                dtpFecha_Final.Focus();
                errors++;
                return;
            }

            if (cliente.Length == 0)
            {
                a.Advertencia("¡SELECCIONE EL NOMBRE DEL CLIENTE CORRESPONDIENTE!");
                txtSocio.Text = "";
                txtSocio.Focus();
                errors++;
            }


            if (id_tipo_prestamo.Length == 0)
            {
                a.Advertencia("¡SELECCIONE EL TIPO DE PRÉSTAMO CORRESPONDIENTE!");
                cmbTipo_Prestamo.Text = "";
                cmbTipo_Prestamo.Focus();
                errors++;
            }

            if (id_plazo.Length == 0)
            {
                a.Advertencia("¡SELECCIONE EL PLAZO!");
                cmbPlazo.Text = fecha_inicio;
                cmbPlazo.Focus();
                errors++;
                return;
            }

            if (cantidad_lps.Length == 0)
            {
                a.Advertencia("¡INGRESAR LA CANTIDAD EN LEMPIRAS!");
                txtCantidad_Lps.Text = "";
                txtCantidad_Lps.Focus();
                errors++;
            }

            if (interes.Length == 0)
            {
                a.Advertencia("¡INGRESAR EL INTERÉS CORRESPONDIENTE!");
                txtIntereses.Text = "";
                txtIntereses.Focus();
                errors++;
            }

            if (forma_pago.Length == 0)
            {
                a.Advertencia("¡INGRESAR LA FORMA DE PAGO!");
                cmbForma_Pago.Text = "";
                cmbForma_Pago.Focus();
                errors++;
            }

            if (garantia.Length == 0)
            {
                a.Advertencia("¡SELECCIONE EL TIPO DE GARANTÍA!");
                cmbGarantia.Text = "";
                cmbGarantia.Focus();
                errors++;
            }

            if (forma_pago.Length == 0)
            {
                a.Advertencia("¡SELECCIONE LA FORMA DE PAGO!");
                cmbForma_Pago.Text = "";
                cmbForma_Pago.Focus();
                errors++;
            }

            if (observaciones.Length == 0)
            {
                txtObservaciones.Text = "NINGUNA";
                observaciones = txtObservaciones.Text.Trim();
            }

        }

        private void Boot()
        {
            txtPrestamo_Codigo.Enabled = false;
            btnNuevo.Enabled = Clases.Auth.save == "S" ? true : false;
            btnGuardar.Enabled = false;
            btnActualizar.Enabled = false;
            btnEliminar.Enabled = false;
            btnCancelar.Enabled = false;
            pbSalir.Enabled = true;
            btnBuscar_Cliente.Enabled = false;

            TxtIdSocio.Enabled = false;
            txtSocio.Enabled = false;
            txtIdentidad.Enabled = false;
            txtTelefono.Enabled = false;
            txtDireccion.Enabled = false;

            dtpFecha_Inicio.Enabled = false;
            dtpFecha_Final.Enabled = false;

            cmbTipo_Prestamo.Enabled = false;
            cmbPlazo.Enabled = false;
            txtCantidad_Lps.Enabled = false;
            txtIntereses.Enabled = false;
            //txtNum_Cuotas.Enabled = false;
            cmbGarantia.Enabled = false;
            cmbForma_Pago.Enabled = false;
            txtObservaciones.Enabled = false;
        }                
    }
}
