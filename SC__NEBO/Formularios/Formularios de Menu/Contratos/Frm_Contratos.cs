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

namespace SC__NEBO.Formularios.Formularios_de_Menu.Contratos
{
    public partial class Frm_Contratos : Form
    {

        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();

        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);


        Clases.DB db = new Clases.DB();
        Clases.Asistente a = new Clases.Asistente();

        int errors;

        string codigo, id_cosechas, fecha_inicio, fecha_limite, codigo_socios, socio, id_bodegas, precioc, cant_qqc, valor_totalc;

        public Frm_Contratos()
        {
            InitializeComponent();
        }

        private void Frm_Contratos_Load(object sender, EventArgs e)
        {
            this.Text = Clases.Env.APPNAME + " | REGISTRO DE CONTRATOS | " + Clases.Auth.user + " | " + Clases.Auth.rol;
            Boot();

            GetContrato();
        }

        private void socio_dgv()
        {

            string id_socio = TxtSocio.Text;
            //for (int i = 0; i < DgvData.Rows.Count; i++)
            //{
            //    _id_socio = DgvData.Rows[i].Cells[1]
            //}


        }

        private void GetInfoContratos(string id)
        {
            string condicion = "CODIGO='" + id + "' AND ESTADO='PENDIENTE'";
            DataTable cliente = db.Find("CONTRATOS_CAFE", "*", condicion);

            if (cliente.Rows.Count > 0)
            {
                btnNuevo.Enabled = false;
                btnGuardar.Enabled = false;

                btnCancelar.Enabled = true;


                DataRow info = cliente.Rows[0];
                TxtCodigo.Text = info["CODIGO"].ToString();

                //TRAER LA COSECHA
                string i_cosecha = db.Hook("ID_COSECHAS", "CONTRATOS_CAFE", condicion);
                string condicion__ = "ID_COSECHA='" + i_cosecha + "'";
                txtCosecha.Text = db.Hook("COSECHA", "COSECHAS", condicion__);

                DtpFechaInicio.Text = info["FECHA_INICIO"].ToString();
                DtpFechaLimite.Text = info["FECHA_LIMITE"].ToString();

                string i_socio = db.Hook("CODIGO_SOCIOS", "CONTRATOS_CAFE", condicion);
                string condicion_ = "ID_SOCIO='" + i_socio + "'";
                TxtSocio.Text = db.Hook("NOMBRE", "SOCIOS", condicion_);
                TxtDireccion.Text = db.Hook("DIRECCION", "SOCIOS", condicion_);

                //TRAER LA BODEGA
                CmbBodega.SelectedValue = info["ID_BODEGAS"].ToString();
                CmbBodega.Enabled = Clases.Auth.update == "S" ? true : false;

                TxtPrecio.Text = info["PRECIO"].ToString();

                TxtCantQQ.Text = info["CANT_QQ"].ToString();

                TxtTotal.Text = info["VALOR_TOTAL"].ToString();


                TxtCodigo.Enabled = false;
                DtpFechaInicio.Enabled = Clases.Auth.update == "S" ? true : false;
                DtpFechaLimite.Enabled = Clases.Auth.update == "S" ? true : false;

                TxtSocio.Enabled = Clases.Auth.update == "S" ? true : false;
                
                TxtPrecio.Enabled = Clases.Auth.update == "S" ? true : false;

                TxtCantQQ.Enabled = Clases.Auth.update == "S" ? true : false;
                TxtTotal.Enabled = Clases.Auth.update == "S" ? true : false;

                TxtSocio.Enabled = false;
                TxtDireccion.Enabled = false;
            }
            else
            {
                a.Advertencia("ERROR AL RECUPERAR LOS DATOS DEL REGISTRO SELECCIONADO");

            }
            cliente.Dispose();
        }

        private void GetContrato()
        {
            String query = "SELECT A.CODIGO,  B.NOMBRE, A.CANT_QQ, A.PRECIO, VALOR_TOTAL, " +
                "A.ESTADO FROM CONTRATOS_CAFE A INNER JOIN SOCIOS B ON(A.CODIGO_SOCIOS = B.ID_SOCIO) " +
                "INNER JOIN COSECHAS C ON(A.ID_COSECHAS = C.ID_COSECHA) ORDER BY A.CODIGO DESC ";


            DataTable reporte = db.RawSQL(query);

            string _codigo, _socio, _cantqq, _precio, _valort;
            int i;

            DgvData.Rows.Clear();

            for (i = 0; i < reporte.Rows.Count; i++)
            {
                _codigo = reporte.Rows[i][0].ToString();
                _socio = reporte.Rows[i][1].ToString();
                _cantqq = reporte.Rows[i][2].ToString();
                _precio = reporte.Rows[i][3].ToString();
                _valort = reporte.Rows[i][4].ToString();

                DgvData.Rows.Add(_codigo, _socio, _cantqq, _precio, _valort);
            }

            reporte.Dispose();
        }

        private void BuscarContratos(string search = "")
        {
            string campos, condicion;
            campos = " A.CODIGO,  B.NOMBRE, A.CANT_QQ, A.PRECIO, VALOR_TOTAL, " +
                "A.ESTADO FROM CONTRATOS_CAFE A INNER JOIN SOCIOS B ON(A.CODIGO_SOCIOS = B.ID_SOCIO) " +
                "INNER JOIN COSECHAS C ON(A.ID_COSECHAS = C.ID_COSECHA) ";

            if (search != "")
            {
                condicion = "B.NOMBRE LIKE '%" + search + "%' ";

            }
            else
            {
                condicion = "A.ESTADO = 'PENDIENTE'";
            }

            DataTable data = db.Join(campos, condicion, "B.NOMBRE");

            DgvData.Rows.Clear();

            string _codigo, _socio, _cantqq, _precio, _valort;
            int i;

            for (i = 0; i < data.Rows.Count; i++)
            {
                _codigo = data.Rows[i][0].ToString();
                _socio = data.Rows[i][1].ToString();
                _cantqq = data.Rows[i][2].ToString();
                _precio = data.Rows[i][3].ToString();
                _valort = data.Rows[i][4].ToString();

                DgvData.Rows.Add(_codigo, _socio, _cantqq, _precio, _valort);
            }

            //lblResumen.Text = "Mostrando " + data.Rows.Count.ToString() + " registros de " + db.Count("SOCIOS", "DEL = 'N'").ToString();
            data.Dispose();
        }

        public void CodSocio(string cod_socio)
        {
            string condicion = "ID_SOCIO='" + cod_socio + "'";
            codigo_socios = cod_socio;
            TxtSocio.Text = db.Hook("NOMBRE", "SOCIOS", condicion);
            TxtDireccion.Text = db.Hook("DIRECCION", "SOCIOS", condicion);
        }

        private void GetBodega()
        {
            CmbBodega.DataSource = db.Find("BODEGAS", "ID_BODEGA, NOMBRE", "", "NOMBRE");
            CmbBodega.ValueMember = "ID_BODEGA";
            CmbBodega.DisplayMember = "NOMBRE";
            CmbBodega.SelectedIndex = -1;
        }

        private void pbSalir_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnListaContratos_Click(object sender, EventArgs e)
        {
            Formularios.Formularios_de_Menu.Contratos.FrmListaContratos_Liquidacion liqui = new FrmListaContratos_Liquidacion();
            liqui.ShowDialog();
            this.Hide();
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
            BuscarContratos(a.Clean(txtNombre.Text.Trim()));
        }

        private void DgvData_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            //if (DgvData.Rows.Count > 0)
            //{
            //    string id = DgvData.CurrentRow.Cells[0].Value.ToString();
            //    GetInfoContratos(id);
            //}
        }

        private void DgvData_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        string codigo_cosecha;

        private void BtnBuscarSocio_Click_1(object sender, EventArgs e)
        {
            Formularios.Formularios_de_Menu.Socios.FrmLista_Socios_Contratos form = new Socios.FrmLista_Socios_Contratos();
            this.AddOwnedForm(form);
            form.ShowDialog();
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

        private void TxtCantQQ_TextChanged_1(object sender, EventArgs e)
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

            TxtTotal.Text = valorT_.ToString();
        }

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

        private void TxtTotal_TextChanged(object sender, EventArgs e)
        {
            // Remover comas y espacios en blanco
            string text = TxtTotal.Text.Replace(",", "").Replace(" ", "");

            // Verificar si el texto resultante es un número válido
            if (decimal.TryParse(text, out decimal monto))
            {
                // Formatea el valor como moneda y lo asigna al TextBox
                TxtTotal.Text = monto.ToString("N2");
                TxtTotal.SelectionStart = TxtTotal.Text.Length - 3; // Coloca el cursor antes de los decimales
            }
        }

        public void Cosecha()
        {
            string condicion = "ESTADO='ACTIVO'";
            txtCosecha.Text = db.Hook("COSECHA", "COSECHAS", condicion);
            codigo_cosecha = db.Hook("ID_COSECHA", "COSECHAS", condicion);
        }

        private void Boot()
        {
            btnNuevo.Enabled = true;
            btnGuardar.Enabled = false;
            btnCancelar.Enabled = false;
            BtnBuscarSocio.Enabled = false;

            txtCosecha.Enabled = false;

            TxtSocio.Enabled = false;
            TxtDireccion.Enabled = false;
            CmbBodega.Enabled = false;
            DtpFechaInicio.Enabled = false;
            DtpFechaLimite.Enabled = false;
            TxtPrecio.Enabled = false;
            TxtCantQQ.Enabled = false;
            TxtTotal.Enabled = false;

            GetBodega();

        }

        private void BtnReimprimirNotaOriginal_Click(object sender, EventArgs e)
        {
            if (DgvData.Rows.Count > 0)
            {
                string codcontrato = DgvData.CurrentRow.Cells[0].Value.ToString();
                Reportes.FrmRptContrato_Socio contrato = new Reportes.FrmRptContrato_Socio();
                contrato.codigo_c = codcontrato;
                contrato.Show();
            }
        }

        // , , , , , , , ;

        private void ValidateData()
        {
            errors = 0;
            codigo = a.Clean(TxtCodigo.Text.Trim());
            id_cosechas = a.Clean(txtCosecha.Text.Trim());
            //codigo_socios = a.Clean(TxtSocio.Text.Trim());
            fecha_inicio = a.Clean(DtpFechaInicio.Text.Trim());
            fecha_limite = a.Clean(DtpFechaLimite.Text.Trim());
            // codigo_socios = a.Clean(codigo.Text.Trim());
            id_bodegas = CmbBodega.Text != "" ? CmbBodega.SelectedValue.ToString() : "";
            precioc = a.Clean(TxtPrecio.Text.Trim());
            cant_qqc = a.Clean(TxtCantQQ.Text.Trim());
            valor_totalc = a.Clean(TxtTotal.Text.Trim());


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


        private void Clear()
        {
            TxtCodigo.Text = "";
            txtCosecha.Text = "";
            TxtSocio.Text = "";
            TxtDireccion.Text = "";
            CmbBodega.SelectedIndex = -1;
            DtpFechaInicio.Text = "";
            DtpFechaLimite.Text = "";
            TxtPrecio.Text = "";
            TxtCantQQ.Text = "";
            TxtTotal.Text = "";
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            btnNuevo.Enabled = false;
            btnGuardar.Enabled = true;
            btnCancelar.Enabled = true;


            TxtCodigo.Text = "CTR" + db.GetNext("CONTR");

            TxtCodigo.Enabled = false;

            TxtSocio.Enabled = false;
            TxtDireccion.Enabled = false;
            CmbBodega.Enabled = true;
            DtpFechaInicio.Enabled = true;
            DtpFechaLimite.Enabled = true;
            TxtPrecio.Enabled = true;
            TxtCantQQ.Enabled = true;
            //  TxtTotal.Enabled = true;
            BtnBuscarSocio.Enabled = true;

            Cosecha();
            TxtCodigo.Focus();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
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

                        

                    }
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

        private void TxtCantQQ_TextChanged(object sender, EventArgs e)
        {
            double precio = 0, cant_qq = 0, valor_total = 0;
            //double cant = 0, total = 0;

            if (TxtCantQQ.Text == "")
            {
                TxtCantQQ.Text = "1";
            }

            if (TxtPrecio.Text == "")
            {
                TxtPrecio.Text = "1";
            }

            precio = Convert.ToDouble(TxtPrecio.Text.ToString());
            cant_qq = Convert.ToDouble(TxtCantQQ.Text.ToString());

            valor_total = precio * cant_qq;

            TxtTotal.Text = valor_total.ToString("N2");
        }

        private void TxtPrecio_Leave(object sender, EventArgs e)
        {
            if (TxtPrecio.Text == "")
            {
                TxtPrecio.Text = "0.00";
            }
        }

        private void TxtCantQQ_Leave(object sender, EventArgs e)
        {
            if (TxtCantQQ.Text == "")
            {
                TxtCantQQ.Text = "1";
            }
        }

        private void BtnBuscarSocio_Click(object sender, EventArgs e)
        {
            Formularios.Formularios_de_Menu.Socios.FrmLista_Socios_Contratos form = new Socios.FrmLista_Socios_Contratos();
            this.AddOwnedForm(form);
            form.Show();
        }
    }
}
