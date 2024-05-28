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

namespace SC__NEBO.Formularios.Formularios_de_Menu.Liquidacion
{
    public partial class Frm_Liquidacion : Form
    {

        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();

        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);


        Clases.DB db = new Clases.DB();
        Clases.Asistente a = new Clases.Asistente();
        Clases.Moneda mon = new Clases.Moneda();

        string idcorre = "LIQDA";

        public string CODPREST, ABONO_PRESTAMO, INTERES, INTERES_X_ACUMULAR, INTERES_ACUMULADO, DIAS, TOTALP, CANT_DEBE, DETALLES;

        public Frm_Liquidacion()
        {
            InitializeComponent();
        }

        private void BtnNuevo_Click(object sender, EventArgs e)
        {
            Cosecha();
            NumLiquidacion();

            btnBuscarCliente.Enabled = true;
            btnBuscarNotaPeso.Enabled = true;
            //BtnBuscarPrestamo.Enabled = true;
            BtnAgregarNotaPeso.Enabled = true;
            btnEliminarNotaPeso.Enabled = true;
            //BtnAgregarPrestamo.Enabled = true;
            //BtnEliminarPrestamo.Enabled = true;
            btnListaLiquidacion.Enabled = false;

            Btn_Recuperar_Nota.Enabled = false;
            pbSalir.Enabled = false;

            TxtPrecioPlaza.Enabled = true;

            BtnGuardar.Enabled = true;
            BtnCancelar.Enabled = true;
            BtnNuevo.Enabled = false;
            dtpFecha.Enabled = true;

            TxtAportaciones.Enabled = true;

            TxtCantQQPlaza.Text = "0.00";

            TxtInteres.Enabled = true;
            TxtAbonoCapital.Enabled = true;

        }

        private void NumLiquidacion()
        {
            Int32 numliq, correlativo;

            correlativo = Convert.ToInt32(db.Hook("ULTIMO", "CORRELATIVOS", "IDCORRE='" + idcorre + "'"));

            numliq = correlativo + 1;

            LblNumeroLiquidacion.Text = numliq.ToString();
        }

        string notapesob;
        private void btnBuscarNotaPeso_Click(object sender, EventArgs e)
        {
            string COD, NOM;
            COD = codigo_socios;
            NOM = txtNombre.Text;

            if (TxtNotaPeso.Text != "")
            {
                string msg = "¿DESEA AGREGAR ESTA NOTA DE PESO?";

                if (a.Pregunta(msg) == true)
                {
                    errors = 0;
                    notapesob = a.Clean(TxtNotaPeso.Text.Trim());



                    if (notapesob.Length == 0)
                    {
                        a.Advertencia("¡SELECIONE UNA NOTA DE PESO!");
                        TxtNotaPeso.Text = notapeso;
                        TxtNotaPeso.Focus();
                        errors++;
                        return;
                    }
                    if (errors == 0)
                    {
                        CalculosNotasP();

                        Formularios.Formularios_de_Menu.Notas_de_Peso.FrmListadoNotaPeso_Liquidacion notaPeso = new Formularios.Formularios_de_Menu.Notas_de_Peso.FrmListadoNotaPeso_Liquidacion();
                        this.AddOwnedForm(notaPeso);

                        notaPeso.COD = COD;
                        notaPeso.NOM = NOM;

                        notaPeso.ShowDialog();
                    }
                }

                else
                {
                    string notapeso_descartar = TxtNotaPeso.Text;

                    string stmt = "ESTADO='PENDIENTE'";
                    string condicion = "ID_NOTA='" + notapeso_descartar + "'";

                    if (db.Update("NOTA_DE_PESO", stmt, condicion) > 0)
                    {
                        a.Aprueba("LA NOTA DE PESO HA SIDO DESCARTADA!");
                        TxtNotaPeso.Text = "";
                        TxtTipoCafe.Text = "";
                        TxtCantidadQQ.Text = "";

                        Formularios.Formularios_de_Menu.Notas_de_Peso.FrmListadoNotaPeso_Liquidacion notaPeso = new Formularios.Formularios_de_Menu.Notas_de_Peso.FrmListadoNotaPeso_Liquidacion();
                        this.AddOwnedForm(notaPeso);

                        notaPeso.COD = COD;
                        notaPeso.NOM = NOM;

                        notaPeso.ShowDialog();
                    }
                }
            }
            else
            {
                Formularios.Formularios_de_Menu.Notas_de_Peso.FrmListadoNotaPeso_Liquidacion notaPeso = new Formularios.Formularios_de_Menu.Notas_de_Peso.FrmListadoNotaPeso_Liquidacion();
                this.AddOwnedForm(notaPeso);

                notaPeso.COD = COD;
                notaPeso.NOM = NOM;

                notaPeso.ShowDialog();
            }
        }

        string costoxbene, costotot_fletes;
        public void RecibirNotaPeso(string idnotapeso)
        {
            string condicion = "ID_NOTA='" + idnotapeso + "'";
            //TxtNotaPeso.Text = db.Hook("ID_NOTA", "NOTA_DE_PESO", condicion);
            TxtNotaPeso.Text = idnotapeso;
            dtpFecha.Text = db.Hook("FECHA_NOTA_PESO", "NOTA_DE_PESO", condicion);
            string idestadocafe = db.Hook("ID_ESTADO_CAFE", "NOTA_DE_PESO", condicion);
            string condicion_tipocafe = "ID='" + idestadocafe + "'";

            TxtTipoCafe.Text = db.Hook("ESTADO", "ESTADO_CAFE", condicion_tipocafe);
            TxtCantidadQQ.Text = db.Hook("QQ_NETO", "NOTA_DE_PESO", condicion);
            costoxbene = db.Hook("COSTOXBENEFICIADO", "NOTA_DE_PESO", condicion);
            costotot_fletes = db.Hook("COSTO_TOTAL_FLETES", "NOTA_DE_PESO", condicion);

        }

        public void RecibirPagoPrestamo(string _codp, string _montopagar, string int_acumulado_l, string _interes, string int_x_acumular, string _dias, string _montotot, string _cant_d, string detalles )
        {
            TxtCodPrest.Text = _codp;
            TxtMoneda.Text = _montopagar;
            ABONO_PRESTAMO = _montopagar;
            INTERES_ACUMULADO = int_acumulado_l;
            INTERES = _interes;
            INTERES_X_ACUMULAR = int_x_acumular;
            DIAS = _dias;
            TOTALP = _montotot;
            CANT_DEBE = _cant_d;
            DETALLES = detalles;
        }

        string codigo_cont;
        public void RecibirContrato(string codigo_contrato)
        {
            string condicion = "CODIGO='" + codigo_contrato + "'";
            codigo_cont = codigo_contrato;
            TxtCantDispContrato.Text = db.Hook("CANT_QQ_DISP", "CONTRATOS_CAFE", condicion);
            TxtPrecioContrato.Text = db.Hook("PRECIO", "CONTRATOS_CAFE", condicion);

            CalculosPagos();
        }

        private void BtnAgregarNotaPeso_Click(object sender, EventArgs e)
        {
            ValidateDataProd();
        }

        string codigo_cosecha, cosecha;
        int errors;
        string notapeso, fechanotapeso, tipocafe, cantqq, precioqq, montomoneda;


        private void BtnBuscarPrestamo_Click(object sender, EventArgs e)
        {
            string _id_socio = codigo_socios;

            string _socio = txtNombre.Text;

            Formularios.Formularios_de_Menu.Prestamos.FrmPagoPrestamo_Liquidacion form = new Prestamos.FrmPagoPrestamo_Liquidacion();
            this.AddOwnedForm(form);

            //nombre_socio es la variable publica que recibe la info en el formulario de Frm_Pagos_Prestamos
            form.nombre_socio = _socio;
            form.id_socio = _id_socio;

            form.ShowDialog();
        }

        private void CalculosNotasP()
        {
            string notapeso = TxtNotaPeso.ToString();
            double cantidad, precio, total;

            cantidad = Convert.ToDouble(TxtCantidadQQ.Text);
            double _costoxbene = 0, _costotot_fletes=0;

            _costotot_fletes = Convert.ToDouble(costotot_fletes);
            _costoxbene = Convert.ToDouble(costoxbene);
            DgvNotasPeso.Rows.Add(TxtNotaPeso.Text, dtpFecha.Text, TxtTipoCafe.Text, cantidad.ToString("N2"), _costoxbene.ToString("N2"), _costotot_fletes.ToString("N2"));

            CalculosLiquidacion();
        }




        private void CalculosLiquidacion()
        {
            double _fletes = 0;
            double _beneficiado = 0;
            double _tot_qq = 0;
            double tot = 0;
            //double qq_cont = 0;
            //double qq_plaza = 0;
            int i;
            for (i = 0; i < DgvNotasPeso.Rows.Count; i++)
            {
                //string nota = DgvNotasPeso.Rows[i].Cells[0].Value.ToString();
                //string impuesto = db.Hook("IMPTO", "PRODUCTOS", "CODBARRAS='" + nota + "'");

                _tot_qq += Convert.ToDouble(DgvNotasPeso.Rows[i].Cells[3].Value);
                _beneficiado += Convert.ToDouble(DgvNotasPeso.Rows[i].Cells[4].Value);
                _fletes += Convert.ToDouble(DgvNotasPeso.Rows[i].Cells[5].Value);
            }
            LblFletes.Text = _fletes.ToString("N2");
            LblCostoBeneficiado.Text = _beneficiado.ToString("N2");
            LblTotalDeQQ.Text = _tot_qq.ToString();
            //LblTotal.Text = tot.ToString("N2");

            CalculosPagos();
            //qq_cont = Convert.ToDouble(TxtCantContrato.Text);
            //qq_plaza = _tot_qq - qq_cont;

            //TxtCantQQPlaza.Text = qq_plaza.ToString("N2");
        }


        private void CalculosPagos()
        {
            double _tot_qq = 0;
            double qq_cont = 0;
            double qq_plaza = 0;

            double total_mon=0;
            double qq_cont_disp = 0;
            int i;
            for (i = 0; i < DgvNotasPeso.Rows.Count; i++)
            {
                if (DgvNotasPeso.Rows.Count > 0)
                {
                    _tot_qq += Convert.ToDouble(DgvNotasPeso.Rows[i].Cells[3].Value);
                }
                else
                {
                    _tot_qq = 0;
                }
            }

            qq_cont_disp = Convert.ToDouble(TxtCantDispContrato.Text);
            double tot_cont = 0, tot_plaza = 0;
            double precio_cont=0, precio_plaza=0;

            if (qq_cont_disp > _tot_qq)
            {
                qq_cont = _tot_qq;
                TxtCantContrato.Text= qq_cont.ToString("N2");
                qq_plaza = 0;
                TxtCantQQPlaza.Text = qq_plaza.ToString("N2");

                precio_cont=Convert.ToDouble(TxtPrecioContrato.Text );
                tot_cont = _tot_qq * precio_cont;
                TxtTotalContrato.Text = tot_cont.ToString("N2");
                LblTotal.Text = "0.00";
            }
            else if (_tot_qq==0)
            {
                qq_cont = _tot_qq;

                //TxtCantQQPlaza.Text = _tot_qq.ToString("N2");
                qq_plaza = 0;
                double plaza_ = 0;
                TxtCantQQPlaza.Text = "0.00";

                precio_cont = Convert.ToDouble(TxtPrecioContrato.Text);
                tot_cont = 0;
                TxtTotalContrato.Text = tot_cont.ToString("N2");
                TxtPrecioPlaza.Text = "0.00";
                TxtTotalPlaza.Text= "0.00";

                LblTotal.Text = "0.00";
                LblNetoPagar.Text = "0.00";
            }
            else if (qq_cont_disp == _tot_qq)
            {
                qq_cont = _tot_qq;
                TxtCantContrato.Text = qq_cont.ToString("N2");

                qq_plaza = 0;
                TxtCantQQPlaza.Text = qq_plaza.ToString("N2");

                precio_cont = Convert.ToDouble(TxtPrecioContrato.Text);
                tot_cont = qq_cont * precio_cont;
                TxtTotalContrato.Text = tot_cont.ToString("N2");

                LblTotal.Text = "0.00";

            }
            else
            {
                qq_cont = qq_cont_disp;
                TxtCantContrato.Text = qq_cont.ToString("N2");

                qq_plaza = _tot_qq - qq_cont;
                TxtCantQQPlaza.Text = qq_plaza.ToString("N2");

                precio_cont = Convert.ToDouble(TxtPrecioContrato.Text);
                tot_cont = qq_cont * precio_cont;
                TxtTotalContrato.Text = tot_cont.ToString("N2");

                precio_plaza = Convert.ToDouble(TxtPrecioPlaza.Text);
                tot_plaza = qq_plaza * precio_plaza;
                TxtTotalPlaza.Text = tot_plaza.ToString("N2");

                LblTotal.Text = "0.00";

            }

            if (qq_plaza>0)
            {
                if (precio_plaza > 0)
                {
                    total_mon = tot_cont + tot_plaza;
                    LblTotal.Text = total_mon.ToString("N2");
                    deducciones();
                }
            }else 
            {
                if (precio_plaza == 0)
                {
                    total_mon = tot_cont + tot_plaza;
                    LblTotal.Text = total_mon.ToString("N2");
                    if (total_mon > 0)
                    {
                        deducciones();
                    }

                }
            }
        }


        private void deducciones()
        {
            double flete_liq = 0;
            double beneficiado_liq = 0;
            double prestamo_liq = 0;
            double total_mon = 0;
            double deducciones_liq = 0;
            double totalnetopagar_liq = 0;
            double aportaciones_liq = 0;

            if (double.TryParse(LblFletes.Text, out double flete))
            {
                flete_liq = flete;
            }

            if (double.TryParse(LblCostoBeneficiado.Text, out double beneficiado))
            {
                beneficiado_liq = beneficiado;
            }

            if (double.TryParse(LblTotPrestamos.Text, out double prestamo))
            {
                prestamo_liq = prestamo;
            }

            if (double.TryParse(TxtAportaciones.Text, out double aportaciones))
            {
                aportaciones_liq = aportaciones;
                aport = aportaciones_liq.ToString("N2");
            }

            deducciones_liq = flete_liq + beneficiado_liq + prestamo_liq + aportaciones_liq;

            if (double.TryParse(LblTotal.Text, out double total))
            {
                total_mon = total;
            }

            deduc = deducciones_liq.ToString("N2");
            LblDeducciones.Text = deducciones_liq.ToString("N2");

            if (total_mon > 0)
            {
                totalnetopagar_liq = total_mon - deducciones_liq;
                LblNetoPagar.Text = totalnetopagar_liq.ToString("N2");
            }
        }


        //private void deducciones()
        //{
        //    double flete_liq = 0;
        //    double beneficiado_liq = 0;
        //    double prestamo_liq = 0;
        //    double total_mon = 0;
        //    double deducciones_liq = 0;
        //    double totalnetopagar_liq = 0;
        //    double aportaciones_liq = 0;

        //    flete_liq = Convert.ToDouble(LblFletes.Text);
        //    beneficiado_liq = Convert.ToDouble(LblCostoBeneficiado.Text);
        //    prestamo_liq = Convert.ToDouble(LblTotPrestamos.Text);
        //    aportaciones_liq= Convert.ToDouble(TxtAportaciones.Text);
        //    aport = aportaciones_liq.ToString("N2");

        //    deducciones_liq = flete_liq + beneficiado_liq + prestamo_liq + aportaciones_liq;

        //    total_mon = Convert.ToDouble(LblTotal.Text);

        //    deduc = deducciones_liq.ToString("N2");
        //    LblDeducciones.Text = deducciones_liq.ToString("N2");

        //    if (total_mon > 0)
        //    {
        //        totalnetopagar_liq = total_mon - deducciones_liq;
        //        LblNetoPagar.Text = totalnetopagar_liq.ToString("N2");

        //    }

        //}

        private void LimpiarNotasP()
        {
            TxtNotaPeso.Text = "";
            dtpFecha.Text = "";
            TxtTipoCafe.Text = "";
            TxtCantidadQQ.Text = "";
        }

        private void btnBuscarCliente_Click(object sender, EventArgs e)
        {
            if (txtNombre.Text!="")
            {
                string msg = "¿DESEA CAMBIAR DE SOCIO " + txtNombre.Text + "?";

                if (a.Pregunta(msg) == true)
                {

                    estados_prestamos();
                    estados_notas();
                    Contratos_Estado_();
                    Clear_Estados();

                    Formularios.Formularios_de_Menu.Socios.FrmListadoSocios_Liquidacion form = new Socios.FrmListadoSocios_Liquidacion();
                    this.AddOwnedForm(form);
                    form.Show();
                }
            }
            else
            {
                Formularios.Formularios_de_Menu.Socios.FrmListadoSocios_Liquidacion form = new Socios.FrmListadoSocios_Liquidacion();
                this.AddOwnedForm(form);
                form.Show();
            }


        }

        //private void estados_prestamos_notas()
        //{
        //    if (DgvNotasPeso.SelectedRows.Count > 0)
        //    {
        //        string idnotapeso = DgvNotasPeso.CurrentRow.Cells[0].Value.ToString();

        //        string stmt = "ESTADO='PENDIENTE'";
        //        string condicion = "ID_NOTA='" + idnotapeso + "'";

        //        db.Update("NOTA_DE_PESO", stmt, condicion);

        //    }

        //    if (DgvDataPrestamo.SelectedRows.Count > 0)
        //    {
        //        string idprest = DgvDataPrestamo.CurrentRow.Cells[0].Value.ToString();

        //        string stmt = "ESTADO='ACTIVO'";
        //        string condicion = "PRESTAMO_CODIGO='" + idprest + "'";

        //        db.Update("PRESTAMO", stmt, condicion);

        //    }

        //}

        private void estados_notas()
        {
            if (TxtNotaPeso.Text != "")
            {
                string id_np_e = TxtNotaPeso.Text;


               // string msg = "¿DESEA ELIMINAR LA NOTA DE PESO " + id_np_e + "?";

                //if (a.Pregunta(msg) == true)
                //{
                    string stmt = "ESTADO='PENDIENTE'";
                    string condicion = "ID_NOTA='" + id_np_e + "'";

                    db.Update("NOTA_DE_PESO", stmt, condicion);

                    limpiar_notas();
                //}

                for (int i = 0; i < DgvNotasPeso.Rows.Count; i++)
                {
                    if (DgvNotasPeso.Rows[i].Cells[0].Value != null)
                    {
                        string codpeso = DgvNotasPeso.Rows[i].Cells[0].Value.ToString();

                        if (!string.IsNullOrWhiteSpace(codpeso))
                        {
                            string stmt_ = "ESTADO='PENDIENTE'";
                            string condicion_ = "ID_NOTA='" + codpeso + "'";

                            db.Update("NOTA_DE_PESO", stmt_, condicion_);

                        }
                    }
                }
            }
            else
            {
                for (int i = 0; i < DgvNotasPeso.Rows.Count; i++)
                {
                    if (DgvNotasPeso.Rows[i].Cells[0].Value != null)
                    {
                        string codpeso = DgvNotasPeso.Rows[i].Cells[0].Value.ToString();

                        if (!string.IsNullOrWhiteSpace(codpeso))
                        {
                            string stmt = "ESTADO='PENDIENTE'";
                            string condicion = "ID_NOTA='" + codpeso + "'";

                            db.Update("NOTA_DE_PESO", stmt, condicion);

                        }
                    }
                }
            }
        }

        private void limpiar_notas() 
        {
            TxtNotaPeso.Text = "";
            DtpFechaNotaPeso.Text = "";
            TxtTipoCafe.Text = "";
            TxtCantidadQQ.Text = "";
        }


        private void estados_prestamos()
        {
            for (int i = 0; i < DgvDataPrestamo.Rows.Count; i++)
            {

                if (DgvDataPrestamo.Rows[i].Cells[0].Value != null)
                {
                    string idprest = DgvDataPrestamo.Rows[i].Cells[0].Value.ToString();

                    if (!string.IsNullOrWhiteSpace(idprest))
                    {
                        string stmt = "ESTADO='ACTIVO'";
                        string condicion = "PRESTAMO_CODIGO='" + idprest + "'";

                        db.Update("PRESTAMOS", stmt, condicion);
                    }
                }
            }
        }



        private void Contratos_Estado_()
        {
            string cant_cont = TxtCantContrato.Text;
            if (Convert.ToDouble(cant_cont) > 0)
            {
                    string stmt = "ESTADO='PENDIENTE'";
                    string condicion = "CODIGO='" + codigo_cont + "'";
                    db.Update("CONTRATOS_CAFE", stmt, condicion);
            }
        }






        string codigo_socios;
        public void CodSocio(string cod_socio)
        {
            string condicion = "ID_SOCIO='" + cod_socio + "'";
            codigo_socios = cod_socio;
            txtNombre.Text = db.Hook("NOMBRE", "SOCIOS", condicion);
            txtDireccion.Text = db.Hook("DIRECCION", "SOCIOS", condicion);
        }


        private void BtnCancelar_Click(object sender, EventArgs e)
        {
            string msg = "¿DESEA CANCELAR LA ACCION?";

            if (a.Pregunta(msg) == true)
            {
                estados_notas();
                estados_prestamos();

                Contratos_Estado_();
                Clear();
                Boot();

            }

        }


        private void TxtPrecioPlaza_TextChanged(object sender, EventArgs e)
        {
            //CalculosPagos();

                // Obtener el texto ingresado en el control TxtPrecioPlaza
                string precioTexto = TxtPrecioPlaza.Text;

                // Intentar convertir el texto a un número de punto flotante
                if (double.TryParse(precioTexto, out double precio))
                {
                    CalculosPagos();
                }
                else
                {
                }
        }



        private void BtnGuardar_Click(object sender, EventArgs e)
        {
            ValidateData();
            string msg = "¿DESEA REGISTRAR EN LA BASE DE DATOS LA INFORMACIÓN DE LA LIQUIDACIÓN NO. " + numliqui + "?";

            if (errors == 0)
            {
                if (a.Pregunta(msg) == true)
                {
                    BtnGuardar.Enabled = false;
                    string campos = "NUM_LIQUIDACION, ID_COSECHA, FECHA, ID_SOCIO, CANT_QQ_CONT, PRECIO_QQ_CONT, TOT_CONT, CANT_QQ_PP, PRECIO_QQ_PP, TOT_PP, FLETES, BENEFICIADO, PRESTAMO, TOTAL_QQ, TOTAL_MONEDA, NETO_PAGAR, USUARIO, CODIGO_CONTRATO,APORTACIONES,DEDUCCIONES, FORMA_PAGO_ID, NO_CUENTA, INTERES, ABONO_CAPITAL";

                                                                                               
                    string valores = "'" + numliqui + "', '" + codigo_cosecha + "', '" + fecha + "', '" + codigo_socios + "', '" + a.ReturnsNumber(cant_qq_cont) + "', '" + a.ReturnsNumber(precio_qq_cont) + "', " + a.ReturnsNumber(tot_cont) + ", " +
                                     "" + a.ReturnsNumber(cant_qq_pp) + ", " + a.ReturnsNumber(precio_qq_pp) + ", " + a.ReturnsNumber(tot_pp) + ", " + a.ReturnsNumber(fletes) + "," + a.ReturnsNumber(beneficiado) + "," + a.ReturnsNumber(prestamo) + "," +
                                     "" +  a.ReturnsNumber(total_qq_) + "," + a.ReturnsNumber(total_md) + "," + a.ReturnsNumber(neto_p) + ", '" + Clases.Auth.user + "', '" + codigo_cont + "', " + a.ReturnsNumber(aport) + "," + a.ReturnsNumber(deduc) + ", '" + id_tipo_pago + "','" + no_cuenta + "', " + a.ReturnsNumber(i_prestamo) + "," + a.ReturnsNumber(ca_prestamo) + "";
                                                           
                    if (db.Save("CAB_LIQUIDACION", campos, valores) > 0)
                    {
                        db.SetLast("LIQDA");
                        a.Aprueba("¡LA LIQUIDACIÓN NÚMERO " + numliqui + " SE REGISTRÓ CON ÉXITO!");
                        DetalleLiquidacion();
                        Contratos_Estado_Save();

                        ////if (DgvDataPrestamo.Rows.Count > 0)
                        ////{
                        ////    PrestamoSave();
                        ////}

                        Reportes.FrmRptLiquidacionX InfoLiqui = new Reportes.FrmRptLiquidacionX();
                        InfoLiqui.numliqui = numliqui;
                        InfoLiqui.Show();

                        Reportes.FrmRptLiquidacionX_Copia Copia = new Reportes.FrmRptLiquidacionX_Copia();
                        Copia.numliqui = numliqui;
                        Copia.Show();


                        if (Convert.ToDouble(prestamo) > 0)
                        {
                            string campos_COMP = "NUM_LIQUIDACION, INTERES, ABONO_CAPITAL, CANT_LETRAS";

                            string valores_COMP = "'" + numliqui + "', " + a.ReturnsNumber(i_prestamo) + "," + a.ReturnsNumber(ca_prestamo) + ",'" + mon.enletras(prestamo) + "'";

                            if (db.Save("COM_INGRESO_LIQUI", campos_COMP, valores_COMP) > 0)
                            {
                                Reportes.FrmRptComprobanteIngresoLiquidacion info = new Reportes.FrmRptComprobanteIngresoLiquidacion();
                                info.numliqui = numliqui;
                                info.Show();


                            }
                        }
                        Clear();
                        Boot();
                    }


                }
            }

        }


        //private void PrestamoSave()
        //{
        //    int i;
        //    string _codigo, _montopagar,_intacum,_dias,_int,_totpagar, _int_x_pag,_detalles, _cantdebe;
        //    string campos = "ID_PAGO, PRESTAMO_CODIGO, FECHA, ID_FORMA_PAGO, MONTO_PAGO, INTERES,INTERES_ACUMULADO, DIAS, OBSERVACIONES";
        //    string valores;
        //    string cod_pago;
        //    string _idform = "FPP000001";
        //    for (i = 0; i < DgvDataPrestamo.Rows.Count; i++)
        //    {
        //        cod_pago = "PGS" + db.GetNext("PAGOS");
        //        _codigo = DgvDataPrestamo.Rows[i].Cells[0].Value.ToString();
        //        _montopagar = DgvDataPrestamo.Rows[i].Cells[1].Value.ToString();
        //        _intacum = DgvDataPrestamo.Rows[i].Cells[2].Value.ToString();
        //        _dias = DgvDataPrestamo.Rows[i].Cells[3].Value.ToString();
        //        _int = DgvDataPrestamo.Rows[i].Cells[4].Value.ToString();
        //        _totpagar = DgvDataPrestamo.Rows[i].Cells[5].Value.ToString();
        //        _int_x_pag = DgvDataPrestamo.Rows[i].Cells[6].Value.ToString();
        //        _detalles = DgvDataPrestamo.Rows[i].Cells[7].Value.ToString();
        //        _cantdebe = DgvDataPrestamo.Rows[i].Cells[8].Value.ToString();

        //        if (_int_x_pag==null)
        //        {
        //            _int_x_pag = "0";
        //        }

        //        valores = "'"+cod_pago+"','" + _codigo + "','"+FECHA+"','"+ _idform + "'," + a.ReturnsNumber(_montopagar)+", " + a.ReturnsNumber(_int) + ", " + a.ReturnsNumber(_intacum) + ", " + a.ReturnsNumber(_dias) + ", " + a.ReturnsNumber(_totpagar) + ", '" + _detalles + "'";

        //        db.Save("PAGOS", campos, valores);

        //        db.SetLast("PAGOS");
        //        if (_montopagar == _cantdebe)
        //        {
        //            string stmt = "ESTADO = 'PAGADO'";
        //            string condicion = "PRESTAMO_CODIGO = '" + _codigo + "'";
        //            db.Update("PRESTAMOS", stmt, condicion);
        //        }

        //        if (Convert.ToDouble(_int_x_pag) > 0)
        //        {
        //            string stmt = "INTERES_ACUMULADO=" + _int_x_pag + "";
        //            string condicion = "PRESTAMO_CODIGO='" + _codigo + "'";
        //            db.Update("PRESTAMOS", stmt, condicion);
        //        }

        //        if (Convert.ToDouble(_montopagar) > 0)
        //        {
        //            string condicion = "PRESTAMO_CODIGO = '" + _codigo + "'";
        //            string stmt = "MONTO_PENDIENTE = MONTO_PENDIENTE - " + _montopagar + "";
        //            db.Update("PRESTAMOS", stmt, condicion);
        //        }
        //        string parametro = "FECHA_ULTIMO_PAGO='" + FECHA + "'";
        //        string cond = "PRESTAMO_CODIGO='" + _codigo + "'";
        //        db.Update("PRESTAMOS", parametro, cond);
        //    }
        //}

        private void PrestamoSave()
        {
            int i;
            string _codigo, _montopagar, _intacum, _dias, _int, _totpagar, _int_x_pag, _detalles, _cantdebe;
            string campos = "ID_PAGO, PRESTAMO_CODIGO, FECHA, ID_FORMA_PAGO, MONTO_PAGO, INTERES, INTERES_ACUMULADO, DIAS, TOTAL_PAGO, OBSERVACIONES, NUM_LIQUIDACION";
            string valores;
            string cod_pago;
            string _idform = "FPP000001";
            for (i = 0; i < DgvDataPrestamo.Rows.Count; i++)
            {
                if (DgvDataPrestamo.Rows[i].Cells[0].Value != null && !string.IsNullOrEmpty(DgvDataPrestamo.Rows[i].Cells[0].Value.ToString()))
                {
                    // Procesar solo si la celda no es nula y no está vacía
                    cod_pago = "PGS" + db.GetNext("PAGOS");
                    _codigo = DgvDataPrestamo.Rows[i].Cells[0].Value.ToString();
                    _montopagar = DgvDataPrestamo.Rows[i].Cells[1].Value != null ? DgvDataPrestamo.Rows[i].Cells[1].Value.ToString() : "0";
                    _intacum = DgvDataPrestamo.Rows[i].Cells[2].Value != null ? DgvDataPrestamo.Rows[i].Cells[2].Value.ToString() : "0";
                    _dias = DgvDataPrestamo.Rows[i].Cells[3].Value != null ? DgvDataPrestamo.Rows[i].Cells[3].Value.ToString() : "0";
                    _int = DgvDataPrestamo.Rows[i].Cells[4].Value != null ? DgvDataPrestamo.Rows[i].Cells[4].Value.ToString() : "0";
                    _totpagar = DgvDataPrestamo.Rows[i].Cells[5].Value != null ? DgvDataPrestamo.Rows[i].Cells[5].Value.ToString() : "0";
                    _int_x_pag = DgvDataPrestamo.Rows[i].Cells[6].Value != null ? DgvDataPrestamo.Rows[i].Cells[6].Value.ToString() : "0";
                    _detalles = DgvDataPrestamo.Rows[i].Cells[7].Value != null ? DgvDataPrestamo.Rows[i].Cells[7].Value.ToString() : string.Empty;
                    _cantdebe = DgvDataPrestamo.Rows[i].Cells[8].Value != null ? DgvDataPrestamo.Rows[i].Cells[8].Value.ToString() : "0";

                    if (_int_x_pag == null)
                    {
                        _int_x_pag = "0";
                    }

                    valores = "'" + cod_pago + "','" + _codigo + "','" + fecha + "','" + _idform + "'," + a.ReturnsNumber(_montopagar) + ", " + a.ReturnsNumber(_int) + ", " + a.ReturnsNumber(_intacum) + ", " + a.ReturnsNumber(_dias) + ", " + a.ReturnsNumber(_totpagar) + ", '" + _detalles + "', '" + numliqui + "'";

                    db.Save("PAGOS", campos, valores);

                    db.SetLast("PAGOS");
                    if (_montopagar == _cantdebe)
                    {
                        string stmt = "ESTADO = 'PAGADO'";
                        string condicion = "PRESTAMO_CODIGO = '" + _codigo + "'";
                        db.Update("PRESTAMOS", stmt, condicion);
                    }
                    else
                    {
                        string stmt = "ESTADO = 'ACTIVO'";
                        string condicion = "PRESTAMO_CODIGO = '" + _codigo + "'";
                        db.Update("PRESTAMOS", stmt, condicion);
                    }

                    if (Convert.ToDouble(_int_x_pag) > 0)
                    {
                        string stmt = "INTERES_ACUMULADO=" + Convert.ToDouble(_int_x_pag) + "";
                        string condicion = "PRESTAMO_CODIGO='" + _codigo + "'";
                        db.Update("PRESTAMOS", stmt, condicion);
                    }

                    //if (Convert.ToDouble(_montopagar) > 0)
                    //{
                    //    string condicion = "PRESTAMO_CODIGO = '" + _codigo + "'";
                    //    string stmt = "MONTO_PENDIENTE = MONTO_PENDIENTE - " + _montopagar + "";
                    //    db.Update("PRESTAMOS", stmt, condicion);
                    //}

                    string condicion_ = "PRESTAMO_CODIGO = '" + _codigo + "'";
                    string stmt_ = "MONTO_PENDIENTE = MONTO_PENDIENTE - " + Convert.ToDouble(_montopagar) + "";
                    db.Update("PRESTAMOS", stmt_, condicion_);

                    string parametro = "FECHA_ULTIMO_PAGO='" + fecha + "'";
                    string cond = "PRESTAMO_CODIGO='" + _codigo + "'";
                    db.Update("PRESTAMOS", parametro, cond);
                }
            }
        }






        //private void DetalleLiquidacion()
        //{
        //    if (DgvNotasPeso.Rows.Count > 0)
        //    {
        //        int i;
        //        string _cod, _cantidad_qq;
        //        string campos = "NUM_LIQUIDACION, ID_NOTA, CANT_QQ";
        //        string valores;

        //        for (i = 0; i < DgvNotasPeso.Rows.Count; i++)
        //        {
        //            _cod = DgvNotasPeso.Rows[i].Cells[0].Value.ToString();
        //            _cantidad_qq = DgvNotasPeso.Rows[i].Cells[3].Value.ToString();
        //            valores = "'" + numliqui + "', " + a.ReturnsNumber(_cod) + ", " + a.ReturnsNumber(_cantidad_qq) + "";

        //            db.Save("DETALLE_LIQUIDACION", campos, valores);
        //        }
        //        //RestarExistencias();
        //    }
        //}
        private void Contratos_Estado_Save()
        {
            string total_contrato = TxtTotalContrato.Text;
            if (Convert.ToDouble(tot_cont) > 0)
            {
                if (Convert.ToDouble(cant_qq_cont)== Convert.ToDouble(TxtCantDispContrato.Text))
                {
                    string stmt = "ESTADO='TERMINADO'";
                    string condicion = "CODIGO='" + codigo_cont + "'";
                    db.Update("CONTRATOS_CAFE", stmt, condicion);
                }
                else
                {
                    string stmt = "ESTADO='PENDIENTE'";
                    string condicion = "CODIGO='" + codigo_cont + "'";
                    db.Update("CONTRATOS_CAFE", stmt, condicion);
                }
                Restar_CantDisp_Contratos();
            }
        }
        private void Restar_CantDisp_Contratos()
        {
            string valores, condicion;

            condicion = "CODIGO = '" + codigo_cont + "'";
            valores = "CANT_QQ_DISP = CANT_QQ_DISP - " + Convert.ToDouble(cant_qq_cont) + "";
            db.Update("CONTRATOS_CAFE", valores, condicion);

        }

        private void btnListaNotasPeso_Click(object sender, EventArgs e)
        {
            Formularios.Formularios_de_Menu.Liquidacion.FrmListaLiquidacionGeneral liquidacion = new FrmListaLiquidacionGeneral();
            this.AddOwnedForm(liquidacion);
            liquidacion.Show();
        }

        private void TxtAportaciones_TextChanged(object sender, EventArgs e)
        {

            if (TxtAportaciones.Text == "")
            {
                TxtAportaciones.Text = "0.00";
            }

            // Remover comas y espacios en blanco
            string text = TxtAportaciones.Text.Replace(",", "").Replace(" ", "");

            // Verificar si el texto resultante es un número válido
            if (decimal.TryParse(text, out decimal monto))
            {
                // Formatea el valor como moneda y lo asigna al TextBox
                TxtAportaciones.Text = monto.ToString("N2");
                TxtAportaciones.SelectionStart = TxtAportaciones.Text.Length - 3; // Coloca el cursor a
            }
            deducciones();

        }

        private void BtnBuscarContrato_Click(object sender, EventArgs e)
        {
            string COD, NOM;
            COD = codigo_socios;
            NOM = txtNombre.Text;
            Formularios.Formularios_de_Menu.Contratos.FrmListaContratos_Liquidacion contratos = new Contratos.FrmListaContratos_Liquidacion();
            this.AddOwnedForm(contratos);

            contratos.COD = COD;
            contratos.NOM = NOM;

            contratos.ShowDialog();

            TxtPrecioContrato.Enabled = false;
        }

        private void TxtAportaciones_KeyPress(object sender, KeyPressEventArgs e)
        {
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



        //private void DetalleLiquidacion()
        //{
        //    int i;
        //    string _codigo, _cantidad_qq;
        //    string campos = "NUM_LIQUIDACION, ID_NOTA, CANT_QQ";
        //    string valores;

        //    for (i = 0; i < DgvNotasPeso.Rows.Count; i++)
        //    {
        //        if (DgvNotasPeso.Rows[i].Cells[0].Value != null)
        //        {
        //            _codigo = DgvNotasPeso.Rows[i].Cells[0].Value.ToString();
        //        }
        //        else
        //        {
        //            _codigo = ""; 
        //        }

        //        if (DgvNotasPeso.Rows[i].Cells[3].Value != null)
        //        {
        //            _cantidad_qq = DgvNotasPeso.Rows[i].Cells[3].Value.ToString();
        //        }
        //        else
        //        {
        //            _cantidad_qq = ""; 
        //        }
        //        valores = "'" + numliqui + "', '" + _codigo + "', " + a.ReturnsNumber(_cantidad_qq) + "";
        //        db.Save("DETALLE_LIQUIDACION", campos, valores);

        //        string stmt = "ESTADO='LIQUIDADO'";
        //        string condicion = "ID_NOTA='" + _codigo + "'";
        //        db.Update("NOTA_DE_PESO", stmt, condicion);
        //    }
        //    //RestarExistencias();
        //}


        private void DetalleLiquidacion()
        {
            string campos = "NUM_LIQUIDACION, ID_NOTA, CANT_QQ";

            for (int i = 0; i < DgvNotasPeso.Rows.Count; i++)
            {
                if (DgvNotasPeso.Rows[i].Cells[0].Value != null && DgvNotasPeso.Rows[i].Cells[3].Value != null)
                {
                    string _codigo = DgvNotasPeso.Rows[i].Cells[0].Value.ToString();
                    string _cantidad_qq = DgvNotasPeso.Rows[i].Cells[3].Value.ToString();

                    // Asegúrate de que los valores no estén vacíos antes de la inserción en la base de datos
                    if (!string.IsNullOrWhiteSpace(_codigo) && !string.IsNullOrWhiteSpace(_cantidad_qq))
                    {
                        string valores = "'" + numliqui + "', '" + _codigo + "', " + a.ReturnsNumber(_cantidad_qq) + "";
                        db.Save("DETALLE_LIQUIDACION", campos, valores);

                        string stmt = "ESTADO='LIQUIDADO'";
                        string condicion = "ID_NOTA='" + _codigo + "'";
                        db.Update("NOTA_DE_PESO", stmt, condicion);
                    }
                }
            }
            // RestarExistencias();
        }


        string numliqui, fecha, cant_qq_cont, precio_qq_cont, tot_cont, cant_qq_pp, precio_qq_pp, tot_pp, fletes, beneficiado, prestamo, total_qq_, total_md, neto_p, deduc,aport;

        private void Btn_Recuperar_Nota_Click(object sender, EventArgs e)
        {
            Formularios.Formularios_de_Menu.Notas_de_Peso.Frm_Recuperar_Nota_Peso recuperar = new Formularios.Formularios_de_Menu.Notas_de_Peso.Frm_Recuperar_Nota_Peso();
            recuperar.Show();
        }

        private void TxtInteres_TextChanged(object sender, EventArgs e)
        {
            if (TxtInteres.Text == "")
            {
                TxtInteres.Text = "0.00";
            }

            // Remover comas y espacios en blanco
            string text = TxtInteres.Text.Replace(",", "").Replace(" ", "");

            // Verificar si el texto resultante es un número válido
            if (decimal.TryParse(text, out decimal monto))
            {
                // Formatea el valor como moneda y lo asigna al TextBox
                TxtInteres.Text = monto.ToString("N2");
                TxtInteres.SelectionStart = TxtInteres.Text.Length - 3; // Coloca el cursor a
            }
            TotPrestLiquidacion();
            deducciones();

        }

        private void TxtAbonoCapital_TextChanged(object sender, EventArgs e)
        {
            if (TxtAbonoCapital.Text == "")
            {
                TxtAbonoCapital.Text = "0.00";
            }

            // Remover comas y espacios en blanco
            string text = TxtAbonoCapital.Text.Replace(",", "").Replace(" ", "");

            // Verificar si el texto resultante es un número válido
            if (decimal.TryParse(text, out decimal monto))
            {
                // Formatea el valor como moneda y lo asigna al TextBox
                TxtAbonoCapital.Text = monto.ToString("N2");
                TxtAbonoCapital.SelectionStart = TxtAbonoCapital.Text.Length - 3; // Coloca el cursor a
            }
            TotPrestLiquidacion();
            deducciones();

        }

        private void pbSalir_Click_1(object sender, EventArgs e)
        {
            Close();
        }

        private void BtnEliminarPrestamo_Click(object sender, EventArgs e)
        {
            if (DgvDataPrestamo.SelectedRows.Count > 0)
            {
                // Obtiene el ID del registro seleccionado
                string idprest = DgvDataPrestamo.SelectedRows[0].Cells[0].Value.ToString();

                string msg = "¿DESEA ELIMINAR LA NOTA DE PESO " + idprest + "?";

                if (a.Pregunta(msg) == true)
                {
                    // Elimina la fila seleccionada del DataGridView
                    DgvDataPrestamo.Rows.RemoveAt(DgvDataPrestamo.SelectedRows[0].Index);

                    if (!string.IsNullOrWhiteSpace(idprest))
                    {
                        // Actualiza el estado del registro en la base de datos
                        string stmt = "ESTADO='ACTIVO'";
                        string condicion = "PRESTAMO_CODIGO='" + idprest + "'";
                        db.Update("PRESTAMOS", stmt, condicion);
                    }
                }
            }
        }

        private void btnEliminarNotaPeso_Click(object sender, EventArgs e)
        {
            if (TxtNotaPeso.Text != "")
            {
                string id_np_e = TxtNotaPeso.Text;


                string msg = "¿DESEA ELIMINAR LA NOTA DE PESO " + id_np_e + "?";

                if (a.Pregunta(msg) == true)
                {
                    string stmt = "ESTADO='PENDIENTE'";
                    string condicion = "ID_NOTA='" + id_np_e + "'";

                    db.Update("NOTA_DE_PESO", stmt, condicion);
                }
                CalculosLiquidacion();
                deducciones();

                limpiar_notas();
            }
            else
            {
                
                if (DgvNotasPeso.Rows.Count > 0)
                {


                    if (DgvNotasPeso.SelectedRows.Count > 0)
                    {
                        string nota = DgvNotasPeso.SelectedRows[0].Cells[0].Value.ToString();

                        string msg = "¿DESEA ELIMINAR LA NOTA DE PESO " + nota + "?";

                        if (a.Pregunta(msg) == true)
                        {
                            DgvNotasPeso.Rows.RemoveAt(DgvNotasPeso.SelectedRows[0].Index);

                            if (!string.IsNullOrWhiteSpace(nota))
                            {

                                string stmt = "ESTADO='PENDIENTE'";
                                string condicion = "ID_NOTA='" + nota + "'";

                                db.Update("NOTA_DE_PESO", stmt, condicion);
                            }
                            CalculosLiquidacion();
                            deducciones();

                        }
                    }
                }
                else
                {
                    a.Advertencia("¡NO HAY NOTAS DE PESO POR ELIMINAR!");
                }

            }
        }

        string id_tipo_pago, no_cuenta,i_prestamo,ca_prestamo;
        private void ValidateData()
        {
            errors = 0;
            numliqui = a.Clean(LblNumeroLiquidacion.Text.Trim());
            fecha = a.Clean(dtpFecha.Text.Trim());
            string idsocio = a.Clean(txtNombre.Text.Trim());
            cant_qq_cont = TxtCantContrato.Text;
            precio_qq_cont = TxtPrecioContrato.Text;
            tot_cont = TxtTotalContrato.Text;
            cant_qq_pp = TxtCantQQPlaza.Text; ;
            precio_qq_pp = TxtPrecioPlaza.Text;
            tot_pp = TxtTotalPlaza.Text;
            fletes = LblFletes.Text;
            beneficiado = LblCostoBeneficiado.Text;
            prestamo= LblTotPrestamos.Text;
            total_qq_=LblTotalDeQQ.Text;
            total_md=LblTotal.Text;
            neto_p=LblNetoPagar.Text;
            i_prestamo = TxtInteres.Text;
            ca_prestamo = TxtAbonoCapital.Text;

            id_tipo_pago = cmbForma_Pago.Text != "" ? cmbForma_Pago.SelectedValue.ToString() : "";






            if (numliqui.Length == 0)
            {
                a.Advertencia("¡INGRESAR UN NÚMERO DE LIQUIDACIÓN VÁLIDO!");
                LblNumeroLiquidacion.Text = numliqui;
                LblNumeroLiquidacion.Focus();
                errors++;
                return;
            }
            if (fecha.Length == 0)
            {
                a.Advertencia("¡SELECCIONE UNA FECHA VÁLIDA!");
                dtpFecha.Text = fecha;
                dtpFecha.Focus();
                errors++;
                return;
            }
            if (idsocio.Length == 0)
            {
                a.Advertencia("¡INGRESE UN SOCIO VÁLIDO!");
                txtNombre.Text = idsocio;
                txtNombre.Focus();
                errors++;
                return;
            }
            if (DgvNotasPeso.RowCount == 0)
            {
                a.Advertencia("NO HA INGRESADO NOTAS DE PESO.");
                TxtNotaPeso.Focus();
                errors++;
                return;
            }
            if (TxtNotaPeso.TextLength != 0)
            {
                a.Advertencia("¡INGRESE LA NOTA DE PESO PENDIENTE!");
                TxtNotaPeso.Focus();
                errors++;
                return;
            }

            if (id_tipo_pago.Length == 0)
            {
                a.Advertencia("¡SELECCIONE EL TIPO DE PAGO!");
                cmbForma_Pago.Text = id_tipo_pago;
                cmbForma_Pago.Focus();
                errors++;
            }

            if (id_tipo_pago == "FPG000002")
            {
                string condicion = "ID_SOCIO= '" + codigo_socios + "'";
                no_cuenta = db.Hook("NO_CUENTA", "SOCIOS", condicion);
            }
            else
            {
                no_cuenta = "";
            }

            if (TxtCodPrest.TextLength != 0)
            {
                a.Advertencia("¡INGRESE EL PRÉSTAMO PENDIENTE!");
                TxtCodPrest.Focus();
                errors++;
                return;
            }

            if (total_md.Length == 0)
            {
                a.Advertencia("¡INGRESE EL MÉTODO DE PAGO!");
                errors++;
                return;
            }


            if(Convert.ToDouble(total_md) != Convert.ToDouble(deduc))
            {
                if (neto_p == "0.00")
                {
                    a.Advertencia("¡INGRESE UN MÉTODO DE PAGO VÁLIDO!");
                    LblNetoPagar.Text = neto_p;
                    LblNetoPagar.Focus();
                    errors++;
                    return;
                }
            }            
        }


        public void Cosecha()
        {
            string condicion = "ESTADO='ACTIVO'";
            TxtCosecha.Text = db.Hook("COSECHA", "COSECHAS", condicion);
            codigo_cosecha = db.Hook("ID_COSECHA", "COSECHAS", condicion);
        }

        private void BtnAgregarPrestamo_Click(object sender, EventArgs e)
        {
            ValidateDataPrest();
        }

        private void ValidateDataPrest()
        {
            int errorss = 0;
            string prestamo = a.Clean(TxtCodPrest.Text.Trim());

            if (prestamo.Length == 0)
            {
                a.Advertencia("¡SELECIONE UN PRÉSTAMO!");
                TxtCodPrest.Text = prestamo;
                TxtCodPrest.Focus();
                errorss++;
                return;
            }
            if (errors == 0)
            {
                CalculosPrest();
                LimpiarPrest();
                deducciones();
            }
        }

        private void CalculosPrest()
        {
            string prest = TxtCodPrest.Text;
            DgvDataPrestamo.Rows.Add(prest, ABONO_PRESTAMO, INTERES_ACUMULADO,DIAS, INTERES, TOTALP, INTERES_X_ACUMULAR, DETALLES, CANT_DEBE);
            TotPrestLiquidacion();
        }

        private void TotPrestLiquidacion()
        {
            double tot_prest = 0;
            ////int i;
            ////for (i = 0; i < DgvDataPrestamo.Rows.Count; i++)
            ////{
            ////    tot_prest += Convert.ToDouble(DgvDataPrestamo.Rows[i].Cells[5].Value);
            ////}

            //tot_prest = Convert.ToDouble(TxtInteres.Text) + Convert.ToDouble(TxtAbonoCapital);

            //LblTotPrestamos.Text = tot_prest.ToString("N2");

            double interes = 0;
            double abonoCapital = 0;

            if (double.TryParse(TxtInteres.Text, out interes) && double.TryParse(TxtAbonoCapital.Text, out abonoCapital))
            {
                tot_prest = interes + abonoCapital;
            }
            else
            {
            }

            LblTotPrestamos.Text = tot_prest.ToString("N2");
        }

        private void LimpiarPrest()
        {
            TxtCodPrest.Text = "";
            TxtMoneda.Text = "";
        }

        private void ValidateDataProd()
        {
            errors = 0;
            notapeso = a.Clean(TxtNotaPeso.Text.Trim());
            fechanotapeso = a.Clean(dtpFecha.Text.Trim());
            tipocafe = a.Clean(TxtTipoCafe.Text.Trim());
            cantqq = a.Clean(TxtCantidadQQ.Text.Trim());

            if (notapeso.Length == 0)
            {
                a.Advertencia("¡SELECIONE UNA NOTA DE PESO!");
                TxtNotaPeso.Text = notapeso;
                TxtNotaPeso.Focus();
                errors++;
                return;
            }
            if (errors == 0)
            {
                CalculosNotasP();
                LimpiarNotasP();
                deducciones();
            }
        }

        private void pbSalir_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Formularios.Formularios_de_Menu.Prestamos.Frm_Prestamos_por_Persona form = new Formularios_de_Menu.Prestamos.Frm_Prestamos_por_Persona();
            this.AddOwnedForm(form);
            form.Show();
        }

        private void Frm_Liquidacion_Load(object sender, EventArgs e)
        {
            this.Text = Clases.Env.APPNAME + " | LIQUIDACIÓN | " + Clases.Auth.user + " | " + Clases.Auth.rol;
            Boot();

            TxtCodPrest.Text = CODPREST;
            TxtMoneda.Text = ABONO_PRESTAMO;
            Tipo_Prestamo();

            //CalculosLiquidacion();
        }
        private void Tipo_Prestamo()
        {
            cmbForma_Pago.DataSource = db.Find("FORMA_de_PAGO", "ID_FORMA_PAGO, FORMA_PAGO", "", "FORMA_PAGO");
            cmbForma_Pago.ValueMember = "ID_FORMA_PAGO";
            cmbForma_Pago.DisplayMember = "FORMA_PAGO";
            cmbForma_Pago.SelectedIndex = -1;

        }
        public void Boot()
        {
            BtnNuevo.Enabled = Clases.Auth.save == "S" ? true : false;
            BtnGuardar.Enabled = false;
            BtnCancelar.Enabled = false;
            pbSalir.Enabled = true;
            btnBuscarCliente.Enabled = false;
            btnBuscarNotaPeso.Enabled = false;
            BtnAgregarNotaPeso.Enabled = false;
            btnEliminarNotaPeso.Enabled = false;

            BtnBuscarPrestamo.Enabled = false;
            BtnAgregarPrestamo.Enabled = false;
            BtnEliminarPrestamo.Enabled = false;

            TxtAportaciones.Enabled = false;
            TxtAbonoCapital.Enabled = false;
            TxtInteres.Enabled = false;

            btnListaLiquidacion.Enabled = true;
            Btn_Recuperar_Nota.Enabled = true; 

            txtNombre.Enabled = false;
            txtDireccion.Enabled = false;
            TxtCosecha.Enabled = false;
            dtpFecha.Enabled = false;
            DtpFechaNotaPeso.Enabled = false;

            TxtPrecioPlaza.Enabled = false;

            TxtNotaPeso.Enabled = false;
            TxtTipoCafe.Enabled = false;
            TxtCantidadQQ.Enabled = false;

            DgvNotasPeso.Rows.Clear();
            DgvDataPrestamo.Rows.Clear();
        }

        private void Clear()
        {
            txtNombre.Text = "";
            txtDireccion.Text = "";
            TxtCosecha.Text = "";
            dtpFecha.Text = "";
            DtpFechaNotaPeso.Text = "";

            TxtNotaPeso.Text = "";
            TxtTipoCafe.Text = "";
            TxtCantidadQQ.Text = "";

            TxtCantContrato.Text = "0.00";
            TxtCantDispContrato.Text = "0.00";

            cant_qq_pp = "0.00";
            TxtCantQQPlaza.Text = "0.00";

            TxtPrecioContrato.Text = "0.00";
            TxtTotalContrato.Text = "0.00";
            
            TxtPrecioPlaza.Text = "0.00";
            TxtTotalPlaza.Text = "0.00";




            LblFletes.Text = "0.00";
            LblCostoBeneficiado.Text = "0.00";
            LblNetoPagar.Text = "0.00";
            LblNumeroLiquidacion.Text = "0.00";
            LblTotal.Text = "0.00";
            LblTotalDeQQ.Text = "0.00";
            LblTotPrestamos.Text = "0.00";
            LblDeducciones.Text = "0.00";
            TxtAportaciones.Text = "0.00";
            TxtAbonoCapital.Text = "0.00";
            TxtInteres.Text = "0.00";

            cmbForma_Pago.SelectedIndex = -1;

            DgvNotasPeso.Rows.Clear();
            DgvDataPrestamo.Rows.Clear();
            //deduc = "";
            //aport = "";
            //codigo_socios = "";
            //codigo_cont = "";
        }


        private void Clear_Estados()
        {
            txtNombre.Text = "";
            txtDireccion.Text = "";

            DtpFechaNotaPeso.Text = "";

            TxtNotaPeso.Text = "";
            TxtTipoCafe.Text = "";
            TxtCantidadQQ.Text = "";

            TxtCantContrato.Text = "0.00";
            TxtCantDispContrato.Text = "0.00";
            TxtPrecioContrato.Text = "0.00";
            TxtTotalContrato.Text = "0.00";
            TxtCantQQPlaza.Text = "0.00";
            TxtPrecioPlaza.Text = "0.00";
            TxtTotalPlaza.Text = "0.00";

            LblFletes.Text = "0.00";
            LblCostoBeneficiado.Text = "0.00";
            LblNetoPagar.Text = "0.00";
            LblTotal.Text = "0.00";
            LblTotalDeQQ.Text = "0.00";
            LblTotPrestamos.Text = "0.00";
            LblDeducciones.Text = "0.00";
            TxtAportaciones.Text = "0.00";

            DgvNotasPeso.Rows.Clear();
            DgvDataPrestamo.Rows.Clear();
            //deduc = "";
            //aport = "";
            //codigo_socios = "";
            //codigo_cont = "";
        }
    }
}

