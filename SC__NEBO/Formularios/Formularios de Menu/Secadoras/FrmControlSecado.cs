using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SC__NEBO.Formularios.Formularios_de_Menu.Secadoras
{
    public partial class FrmControlSecado : Form
    {

        Clases.DB db = new Clases.DB();
        Clases.Asistente a = new Clases.Asistente();

        string codigo_cosecha;
        string no_secado, cosecha, empleado, secadora, bodega, qq_brutos_h, tolva_seco, qq_secos, rendimiento, temperatura, porcentaje_humedad;
        string fecha_inicio, fecha_final;

        public string _codigo_cosecha, _no_secado, _idcosecha, _cosecha, _bodegaid, _bodega, _empleado, _secadora, _qq_brutos_h, _fecha_inicio;

        int errors;

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            btnNuevo.Enabled = false;
            btnGuardar.Enabled = Clases.Auth.save == "S" ? true : false;
            btnActualizar.Enabled = false;
            btnCancelar.Enabled = true;
            btnAgregarNP.Enabled = true;
            BtnBuscar.Enabled = true;
            btnEliminar.Enabled = true;

            pbSalir.Enabled = false;

            Cosecha();
            txtNoSecado.Text = "CTS" + db.GetNext("CTSEC");

            dtpFechaInicio.Enabled = true;
            dtpFechaFinal.Enabled = false;

            txtNoSecado.Enabled = false;
            txtCosecha.Enabled = false;
            cmbSecadora.Enabled = true;
            cmbBodega.Enabled = true;
            cmbEmpleado.Enabled = true;
            cmbTolva_Almacen.Enabled = false;
            txtQQSecos.Enabled = false;
            txtRendimiento.Enabled = false;
            txtTemperatura.Enabled = false;
            txtPor_Humedad.Enabled = false;

            dtpFechaAggNotaPeso.Enabled = true;

            cbxSecado_Completo.Enabled = true;

            btnListaSecada.Enabled = false;
            btnSecadasCompletas.Enabled = false;

        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            ValidateData();

            string msg = "¿DESEA REGISTRAR ESTE CONTROL DE SECADO?";

            if (errors == 0)
            {
                if (a.Pregunta(msg) == true)
                {
                    //string campos = " COD_SECADO, ID_COSECHA, EMPLEADO, ID_SECADORA, FECHA_INICIO, QQ_BRUTOS_HUMEDO, " +
                    //    "FECHA_FINAL, TOLVA_ALMACENAMIENTO, ALMACEN, QQ_SECOS, PORCENTAJE_HUMEDAD, TEMPERATURA, RENDIMIENTO";

                    //string valores = " '" + no_secado + "', " + codigo_cosecha + ", '" + empleado + "', '" + secadora + "', '" + fecha_inicio + "', " + qq_brutos_h + ", '" + fecha_final + "', " +
                    //    "" + tolva_seco + ", " + bodega + ", " + qq_secos + ", " + porcentaje_humedad + ", " + temperatura + ", " +
                    //    "" + rendimiento + " ";
                    
                    DateTime fecha_i, fecha_f;
                    if (cbxSecado_Completo.Checked == true)
                    {
                        string campos, valores;
                        campos = " COD_SECADO, ID_COSECHA, EMPLEADO, ID_SECADORA, FECHA_INICIO, QQ_BRUTOS_HUMEDO, QQ_NETOS_HUMEDO, " +
                           "FECHA_FINAL, TOLVA_ALMACENAMIENTO, ALMACEN, QQ_SECOS, PORCENTAJE_HUMEDAD, TEMPERATURA, RENDIMIENTO, ESTADO, USUARIO";
                        
                        valores = " '" + no_secado + "', " + codigo_cosecha + ", '" + empleado + "', '" + secadora + "', '" + fecha_inicio + " " + DateTime.Now.ToString("HH:mm:ss") + "', " + qq_brutos_h + ", " + Convert.ToString(qq_netos_) + ", '" + fecha_final + " " + DateTime.Now.ToString("HH:mm:ss") + "', " +
                           "" + tolva_seco + ", " + bodega + ", " + qq_secos + ", " + porcentaje_humedad + ", " + temperatura + ", " +
                           "" + rendimiento + ", 'SECADA', '" + Clases.Auth.user + "' ";

                        if (db.Save("CAB_CONTROL_SECADO", campos, valores) > 0)
                        {
                            //db.RawSQL(query);
                            DetalleControlSecado();
                            db.SetLast("CTSEC");
                            a.Aprueba("REGISTRO INGRESADO CON ÉXITO!");
                            Clear();
                            Boot();

                            Reportes.FrmRptControl_Secado controlsecado = new Reportes.FrmRptControl_Secado();
                            controlsecado.codsecado = no_secado;
                            controlsecado.Show();

                        }
                    }
                    else
                    {
                        string campos, valores;
                        campos = " COD_SECADO, ID_COSECHA, EMPLEADO, ID_SECADORA, FECHA_INICIO, QQ_BRUTOS_HUMEDO, " +
                       "FECHA_FINAL, ALMACEN ";

                        valores = " '" + no_secado + "', " + codigo_cosecha + ", '" + empleado + "', '" + secadora + "', " +
                           "'" + fecha_inicio + " " + DateTime.Now.ToString("HH:mm:ss") + "', " + qq_brutos_h + ", '" + fecha_final + " " + DateTime.Now.ToString("HH:mm:ss") + "',  " + bodega + "";

                        if (db.Save("CAB_CONTROL_SECADO", campos, valores) > 0)
                        {
                            //db.RawSQL(query);
                            DetalleControlSecado();
                            db.SetLast("CTSEC");
                            a.Aprueba("REGISTRO INGRESADO CON ÉXITO!");
                            Clear();
                            Boot();

                        }
                    }
                }
            }
        }

        private void Metodos_cmb()
        {
            Tipo_Empleado();
            Tipo_Secadora();
            Tipo_Bodega();
        }

        private void Tipo_Empleado()
        {
            cmbEmpleado.DataSource = db.Find("EMPLEADOS", "ID_EMPLEADO, NOMBRE", "", "NOMBRE");
            cmbEmpleado.ValueMember = "ID_EMPLEADO";
            cmbEmpleado.DisplayMember = "NOMBRE";
            cmbEmpleado.SelectedIndex = -1;
        }

        string notapeso, fechanotapeso;
        private void btnAgregarNP_Click(object sender, EventArgs e)
        {
            string msg = "¿DESEA AGREGAR ESTA NOTA DE PESO?";

                if (a.Pregunta(msg) == true)
                {

                    errors = 0;
                    notapeso = a.Clean(txtNoNotaPeso.Text.Trim());
                    fechanotapeso = a.Clean(dtpFechaAggNotaPeso.Text.Trim());

                    if (notapeso.Length == 0)
                    {
                        a.Advertencia("¡SELECIONE UNA NOTA DE PESO!");
                        txtNoNotaPeso.Text = notapeso;
                        txtNoNotaPeso.Focus();
                        errors++;
                        return;
                    }
                    if (errors == 0)
                    {
                        CalculosNotasP();
                        LimpiarNotasP();
                        SumaQQ_NP();
                    }

                txtNoNotaPeso.Text = "";
                txtSocio.Text = "";
                txtQQ.Text = "";

                btnCancelar.Enabled = true;

            }
                else
                {
                string notapeso_descartar = txtNoNotaPeso.Text;

                    string stmt = "ESTADO_SECADO='PENDIENTE'";
                    string condicion = "ID_NOTA='" + notapeso_descartar + "'";

                if (db.Update("NOTA_DE_PESO", stmt, condicion) > 0)
                {
                    a.Aprueba("LA NOTA DE PESO HA SIDO DESCARTADA!");
                    txtNoNotaPeso.Text = "";
                    txtSocio.Text = "";
                    txtQQ.Text = "";
                }
            }
        }

        //----------------------------------------------------------------
        private void CalculosNotasP()
        {
            string notapeso = txtNoNotaPeso.ToString();

            DgvData.Rows.Add(dtpFechaAggNotaPeso.Text, txtNoNotaPeso.Text, socio_, qqbrutos_, qqnetos_);
        }
        private void LimpiarNotasP()
        {
            txtNoNotaPeso.Text = "";
            dtpFechaAggNotaPeso.Text = "";
        }

        private void SumaQQ_NP()
        {
            double total_qqhumedos = 0;
            double total_qqnetos = 0;

            for (int i = 0; i < DgvData.Rows.Count; i++)
            {
                total_qqhumedos += Convert.ToDouble(DgvData.Rows[i].Cells[3].Value.ToString());
                total_qqnetos += Convert.ToDouble(DgvData.Rows[i].Cells[4].Value.ToString());
            }

            lblQQBrutosH.Text = total_qqhumedos.ToString();
            lblQQ_NetosH.Text = total_qqnetos.ToString();
        }
        //----------------------------------------------------------------

        private void label19_Click(object sender, EventArgs e)
        {

        }

        private void cbxSecado_Completo_CheckedChanged(object sender, EventArgs e)
        {
            if (cbxSecado_Completo.Checked == true)
            {
                dtpFechaFinal.Enabled = true;
                txtPor_Humedad.Enabled = true;
                txtRendimiento.Enabled = false;
                txtTemperatura.Enabled = true;
                cmbTolva_Almacen.Enabled = true;
                txtQQSecos.Enabled = true;
            }
            else
            {
                dtpFechaFinal.Enabled = false;
                txtPor_Humedad.Enabled = false;
                txtRendimiento.Enabled = false;
                txtTemperatura.Enabled = false;
                cmbTolva_Almacen.Enabled = false;
                txtQQSecos.Enabled = false;
            }
        }

        double qq_secos_=0, qq_netos_ = 0, rendimiento_;

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (txtNoNotaPeso.Text != "")
            {
                string id_np_e = txtNoNotaPeso.Text;


                string msg = "¿DESEA ELIMINAR LA NOTA DE PESO " + id_np_e + "?";

                if (a.Pregunta(msg) == true)
                {
                    string stmt = "ESTADO_SECADO='PENDIENTE'";
                    string condicion = "ID_NOTA='" + id_np_e + "'";

                    db.Update("NOTA_DE_PESO", stmt, condicion);
                }
                Limpiar_notas();
            }
            else
            {
                if (DgvData.SelectedRows.Count > 0)
                {
                    string nota = DgvData.SelectedRows[0].Cells[1].Value.ToString();

                    string msg = "¿DESEA ELIMINAR LA NOTA DE PESO " + nota + "?";

                    if (a.Pregunta(msg) == true)
                    {
                        if (db.VerifyIfExists("DETALLE_CONTROL_SECADO", "ID_NOTA", nota) == false)
                        {
                            DgvData.Rows.RemoveAt(DgvData.SelectedRows[0].Index);

                            if (!string.IsNullOrWhiteSpace(nota))
                            {

                                string stmt = "ESTADO_SECADO='PENDIENTE'";
                                string condicion = "ID_NOTA='" + nota + "'";

                                db.Update("NOTA_DE_PESO", stmt, condicion);
                            }
                        }
                        else
                        {
                            a.Advertencia("ESTA NOTA DE PESO YA ESTA EN PROCESO DE SECADO.");
                        }
                    }
                }
            }
        }

        private void Limpiar_notas() 
        {
            txtNoNotaPeso.Text = "";
            txtSocio.Text = "";
            txtQQ.Text = "";
            dtpFechaAggNotaPeso.Text = "";
        }

        private void btnSecadasCompletas_Click(object sender, EventArgs e)
        {
            Formularios.Formularios_de_Menu.Secadoras.FrmListado_Secadas_Terminadas form = new FrmListado_Secadas_Terminadas();
            this.AddOwnedForm(form);
            form.Show();
            this.Hide();
        }

        private void CalculoRendimiento()
        {
            if (!string.IsNullOrEmpty(txtQQSecos.Text) && !string.IsNullOrEmpty(lblQQ_NetosH.Text))
            {
                qq_secos_ = Convert.ToDouble(txtQQSecos.Text);
                qq_netos_ = Convert.ToDouble(lblQQ_NetosH.Text);
                rendimiento_ = qq_secos_ - qq_netos_;

                txtRendimiento.Text = rendimiento_.ToString();
            }
            else
            {
                txtRendimiento.Text = string.Empty;
            }
        }

        private void txtQQSecos_TextChanged(object sender, EventArgs e)
        {
            CalculoRendimiento();
        }

        private void BtnBuscar_Click(object sender, EventArgs e)
        {
            
            if (txtNoNotaPeso.Text != "")
            {
                    string msg = "¿DESEA AGREGAR ESTA NOTA DE PESO?";

                    if (a.Pregunta(msg) == true)
                    {

                        errors = 0;
                        notapeso = a.Clean(txtNoNotaPeso.Text.Trim());
                        fechanotapeso = a.Clean(dtpFechaAggNotaPeso.Text.Trim());

                        if (notapeso.Length == 0)
                        {
                            a.Advertencia("¡SELECIONE UNA NOTA DE PESO!");
                            txtNoNotaPeso.Text = notapeso;
                            txtNoNotaPeso.Focus();
                            errors++;
                            return;
                        }
                        if (errors == 0)
                        {
                            CalculosNotasP();
                            LimpiarNotasP();
                            SumaQQ_NP();

                            Formularios.Formularios_de_Menu.Secadoras.FrmNotas_Peso_Secadas form = new FrmNotas_Peso_Secadas();
                            this.AddOwnedForm(form);
                            form.ShowDialog();
                        }

                    }
                            else
                            {
                                string notapeso_descartar = txtNoNotaPeso.Text;

                                string stmt = "ESTADO_SECADO='PENDIENTE'";
                                string condicion = "ID_NOTA='" + notapeso_descartar + "'";

                                if (db.Update("NOTA_DE_PESO", stmt, condicion) > 0)
                                {
                                    a.Aprueba("LA NOTA DE PESO HA SIDO DESCARTADA!");
                                    txtNoNotaPeso.Text = "";
                                    txtSocio.Text = "";
                                    txtQQ.Text = "";

                                    Formularios.Formularios_de_Menu.Secadoras.FrmNotas_Peso_Secadas form = new FrmNotas_Peso_Secadas();
                                    this.AddOwnedForm(form);
                                    form.ShowDialog();
                                }
                            }
            }
            else
            {
                Formularios.Formularios_de_Menu.Secadoras.FrmNotas_Peso_Secadas form = new FrmNotas_Peso_Secadas();
                this.AddOwnedForm(form);
                form.ShowDialog();
            }
        }

        private void Tipo_Secadora()
        {
            cmbSecadora.DataSource = db.Find("MAQUINAS_SECADORAS", "ID, SECADORA", "DEL = 'N'", "SECADORA");
            cmbSecadora.ValueMember = "ID";
            cmbSecadora.DisplayMember = "SECADORA";
            cmbSecadora.SelectedIndex = -1;
        }

        private void Tipo_Bodega()
        {
            cmbBodega.DataSource = db.Find("BODEGAS", "ID_BODEGA, NOMBRE", "", "NOMBRE");
            cmbBodega.ValueMember = "ID_BODEGA";
            cmbBodega.DisplayMember = "NOMBRE";
            cmbBodega.SelectedIndex = -1;
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            ValidateData();

            if (errors == 0)
            {
                string msg = "DESEA GUARDAR LOS CAMBIOS DEL REGISTRO SELECCIONADO";
                if (a.Pregunta(msg) == true)
                {
                    if (cbxSecado_Completo.Checked == true)
                    {
                        //Si ambas fechas son iguales mostrar el mensaje
                        if (fecha_inicio.Equals(fecha_final))
                        {
                            a.Advertencia("¡LA FECHA INICIAL Y LA FECHA FINAL SON LAS MISMAS!");
                            dtpFechaInicio.Focus();
                            errors++;
                            return;
                        }

                        string id_inca = txtNoSecado.Text.Trim();
                        string stmt = "ID_COSECHA=" + _idcosecha + ",EMPLEADO='" + empleado + "',ID_SECADORA='" + secadora + "' " +
                            ",QQ_BRUTOS_HUMEDO=" + qq_brutos_h + ", QQ_NETOS_HUMEDO = " + qq_netos_ + ", FECHA_FINAL='" + fecha_final + " " + DateTime.Now.ToString("HH:mm:ss") + "' " +
                            ",TOLVA_ALMACENAMIENTO=" + tolva_seco + " ,ALMACEN=" + bodega + ",QQ_SECOS=" + qq_secos + "" +
                            ",PORCENTAJE_HUMEDAD=" + porcentaje_humedad + ",TEMPERATURA=" + temperatura + ",RENDIMIENTO=" + rendimiento + ",ESTADO='SECADA', USUARIO='" + Clases.Auth.user + "'";


                        string condicion = "COD_SECADO='" + no_secado + "'";

                        if (db.Update("CAB_CONTROL_SECADO", stmt, condicion) > 0)
                        {
                            a.Aprueba("LOS CAMBIOS SE APLICARON CORRECTAMENTE!");

                            Actualizar_Contro_Secado();
                            DetalleControlSecado();
                            Reportes.FrmRptControl_Secado controlsecado = new Reportes.FrmRptControl_Secado();
                            controlsecado.codsecado = no_secado;
                            controlsecado.Show();

                            Clear();
                            Boot();
                        }
                    }
                    else
                    {

                        string id_inca = txtNoSecado.Text.Trim();
                        string stmt = "ID_COSECHA=" + _idcosecha + ",EMPLEADO='" + empleado + "',ID_SECADORA='" + secadora + "' " +
                            ",QQ_BRUTOS_HUMEDO=" + qq_brutos_h + ",FECHA_FINAL='" + fecha_final + " " + DateTime.Now.ToString("HH:mm:ss") + "' " +
                            ",ALMACEN=" + bodega + "";

                        string condicion = "COD_SECADO='" + no_secado + "'";
                        
                        if (db.Update("CAB_CONTROL_SECADO", stmt, condicion) > 0)
                        {
                            a.Aprueba("LOS CAMBIOS SE APLICARON CORRECTAMENTE!");
                            Actualizar_Contro_Secado();
                            Clear();
                            Boot();
                        }
                    }
                    
                }
            }
        }

        private void Actualizar_Contro_Secado()
        {
            if (DgvData.SelectedRows.Count > 0)
            {
                for (int i = 0; i < DgvData.Rows.Count; i++)
                {
                    string id = DgvData.Rows[i].Cells[1].Value.ToString();
                    CheckIfIdExists(id);
                }
            }
        }

        private void CheckIfIdExists(string id)
        {
            for (int i = 0; i < DgvData.Rows.Count; i++)
            {
                string campos = "COD_SECADO, ID_NOTA, FECHA";

                id = DgvData.Rows[i].Cells[1].Value.ToString();
                string _fecha = DgvData.Rows[i].Cells[0].Value.ToString();

                string valores = "'" + no_secado + "', " + id + ", '" + _fecha + " " + DateTime.Now.ToString("HH:mm:ss") + "'";
                
                if (db.VerifyIfExists("DETALLE_CONTROL_SECADO", "ID_NOTA", id) != true)
                {
                db.Save("DETALLE_CONTROL_SECADO", campos, valores);
                }
                else
                {
                }
            }
        }


    //Metodo que recive la informacion de la nota de peso
    string qqbrutos_, qqnetos_, idsocio_, socio_, fecha_;
        public void RecibirNotaPeso(string idnotapeso)
        {
            string condicion = "ID_NOTA='" + idnotapeso + "'";
            
            txtNoNotaPeso.Text = idnotapeso;
            qqbrutos_ = db.Hook("QQ_BRUTOS", "NOTA_DE_PESO", condicion);
            qqnetos_ = db.Hook("QQ_NETO", "NOTA_DE_PESO", condicion);
            idsocio_ = db.Hook("ID_SOCIO", "NOTA_DE_PESO", condicion);
            fecha_ = db.Hook("FECHA_NOTA_PESO", "NOTA_DE_PESO", condicion);
            socio_ = db.Hook("NOMBRE", "SOCIOS", "ID_SOCIO = '"+ idsocio_ + "'");

            btnCancelar.Enabled = false;
            pbSalir.Enabled = false;
            btnSecadasCompletas.Enabled = false;

            txtSocio.Text = socio_;
            txtQQ.Text = qqbrutos_;
            dtpFechaAggNotaPeso.Text = fecha_;

        }

        private void btnListaSecada_Click(object sender, EventArgs e)
        {
            Formularios.Formularios_de_Menu.Secadoras.FrmLista_Control_Secada form = new FrmLista_Control_Secada();
            this.AddOwnedForm(form);
            form.Show();
            this.Hide();
        }
        

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            string msg = "¿DESEA CANCELAR LA ACCION?";
            
            if (a.Pregunta(msg) == true)
            {
                if (DgvData.SelectedRows.Count > 0)
                { 
                    string codsec_condicion = txtNoSecado.Text;
                    
                    if (db.VerifyIfExists("CAB_CONTROL_SECADO", "COD_SECADO", codsec_condicion) == false)
                    {
                        for (int i = 0; i < DgvData.Rows.Count; i++)
                        {
                            string idnotapeso = DgvData.Rows[i].Cells[1].Value.ToString();

                            string stmt = "ESTADO_SECADO='PENDIENTE'";
                            string condicion = "ID_NOTA='" + idnotapeso + "'";

                            db.Update("NOTA_DE_PESO", stmt, condicion);
                        }

                    }
                    else
                    {
                        for (int i = 0; i < DgvData.Rows.Count; i++)
                        {
                            string idnotapeso = DgvData.Rows[i].Cells[1].Value.ToString();
                            if (db.VerifyIfExists("DETALLE_CONTROL_SECADO", "ID_NOTA", idnotapeso) == false)
                            {
                                string stmt = "ESTADO_SECADO='PENDIENTE'";
                                string condicion = "ID_NOTA='" + idnotapeso + "'";

                                db.Update("NOTA_DE_PESO", stmt, condicion);
                            }
                            else
                            {
                                string stmt = "ESTADO_SECADO='EN PROCESO'";
                                string condicion = "ID_NOTA='" + idnotapeso + "'";

                                db.Update("NOTA_DE_PESO", stmt, condicion);
                            }
                        }
                    }
                }
                Clear();
                Boot();
            
            }
            else
            {
                a.Advertencia("HAY UNA NOTA DE PESO PENDIENTE DE AGREGAR");
            }
        }

        private void pbSalir_Click(object sender, EventArgs e)
        {
            Close();
        }

        public FrmControlSecado()
        {
            InitializeComponent();
        }

        private void FrmControlSecado_Load(object sender, EventArgs e)
        {
            Boot();
            Metodos_cmb();

            txtNoSecado.Text = _no_secado;
            txtSocio.Text = socio_;
            txtQQ.Text = qqbrutos_;
            dtpFechaInicio.Text = _fecha_inicio;

            txtCosecha.Text = _cosecha;
            cmbSecadora.Text = _secadora;
            cmbEmpleado.Text = _empleado;
            cmbBodega.Text = _bodega;

            TolvasDisponibles();
            GetControl_Notas_Almacenadas(txtNoSecado.Text);
        }

        //LLenar el combobox de tolvas disponibles
        private void TolvasDisponibles()
        {
            List<string> opciones = new List<string>
            {
                "1",
                "2",
            };
            cmbTolva_Almacen.DataSource = opciones;
            cmbTolva_Almacen.SelectedIndex = -1;
        }

        private void DetalleControlSecado()
        {
            int i;
            string _id_notapeso, _fecha;
            string campos = "COD_SECADO, ID_NOTA, FECHA";
            string valores;

            
            string condicion_estado;

            for (i = 0; i < DgvData.Rows.Count; i++)
            {
                _fecha = DgvData.Rows[i].Cells[0].Value.ToString();
                _id_notapeso = DgvData.Rows[i].Cells[1].Value.ToString();

                valores = "'" + no_secado + "', " + _id_notapeso + ", '" + _fecha + "'";

                condicion_estado = "ID_NOTA='" + _id_notapeso + "'";
                db.Save("DETALLE_CONTROL_SECADO", campos, valores);

                if (cbxSecado_Completo.Checked == true)
                {
                    string parametro = "ESTADO_SECADO='SECADA'";
                    db.Update("NOTA_DE_PESO", parametro, condicion_estado);
                }
                else
                {
                    string parametro = "ESTADO_SECADO='EN PROCESO'";
                    db.Update("NOTA_DE_PESO", parametro, condicion_estado);
                }
                
            }
            
        }


        private void GetControl_Notas_Almacenadas(string cod_secado_ = "")
        {
            string campos, condicion;

            campos = " A.FECHA, A.ID_NOTA, B.NOMBRE, C.QQ_BRUTOS, C.QQ_NETO FROM DETALLE_CONTROL_SECADO A " +
                " INNER JOIN NOTA_DE_PESO C " +
                "ON(A.ID_NOTA = C.ID_NOTA) INNER JOIN SOCIOS B ON(C.ID_SOCIO = B.ID_SOCIO) ";


            if (cod_secado_ != "")
            {
                condicion = "A.COD_SECADO = '" + cod_secado_ + "' ";

                DataTable data = db.Join(campos, condicion, "A.FECHA");

                DgvData.Rows.Clear();

                string _notapeso_, _nombre, _qqbruto, _qqnetos, _fecha_actu;

                int i;
                for (i = 0; i < data.Rows.Count; i++)
                {
                    _fecha_actu = Convert.ToDateTime(data.Rows[i][0].ToString()).ToShortDateString();
                    _notapeso_ = data.Rows[i][1].ToString();
                    _nombre = data.Rows[i][2].ToString();
                    _qqbruto = data.Rows[i][3].ToString();
                    _qqnetos = data.Rows[i][4].ToString();

                    DgvData.Rows.Add(_fecha_actu, _notapeso_, _nombre, _qqbruto, _qqnetos);

                    btnSecadasCompletas.Enabled = false;
                    btnListaSecada.Enabled = false;

                    SumaQQ_NP();

                }

                data.Dispose();
                Boot();

                dtpFechaFinal.Enabled = false;
                txtPor_Humedad.Enabled = false;
                txtTemperatura.Enabled = false;
                txtQQSecos.Enabled = false;
                cmbTolva_Almacen.Enabled = false;
            }


            
        }


        //Mostrar la Cosechaen un textbox
        public void Cosecha()
        {
            string condicion = "ESTADO='ACTIVO'";
            txtCosecha.Text = db.Hook("COSECHA", "COSECHAS", condicion);
            codigo_cosecha = db.Hook("ID_COSECHA", "COSECHAS", condicion);
        }

        public void ValidateData()
        {
            errors = 0;

            fecha_inicio = a.Clean(dtpFechaInicio.Text.Trim());
            fecha_final = a.Clean(dtpFechaFinal.Text.Trim());
            no_secado = a.Clean(txtNoSecado.Text.Trim());
            cosecha = a.Clean(txtCosecha.Text.Trim());
            bodega = cmbBodega.Text != "" ? cmbBodega.SelectedValue.ToString() : "";
            empleado = cmbEmpleado.Text != "" ? cmbEmpleado.SelectedValue.ToString() : "";
            secadora = cmbSecadora.Text != "" ? cmbSecadora.SelectedValue.ToString() : "";
            qq_brutos_h = a.Clean(lblQQBrutosH.Text.Trim());
            tolva_seco = cmbTolva_Almacen.Text != "" ? cmbTolva_Almacen.SelectedValue.ToString() : "";
            qq_secos = a.Clean(txtQQSecos.Text.Trim());
            rendimiento = a.Clean(txtRendimiento.Text.Trim());
            temperatura = a.Clean(txtTemperatura.Text.Trim());
            porcentaje_humedad = a.Clean(txtPor_Humedad.Text.Trim());

            if (no_secado.Length == 0)
            {
                a.Advertencia("¡EL NÚMERO DE SECADO ES REQUERIDO!");
                txtNoSecado.Text = "";
                txtNoSecado.Focus();
                errors++;
                return;
            }

            if (cosecha.Length == 0)
            {
                a.Advertencia("¡ESPECIFICAR LA COSECHA ES REQUERIDO!");
                txtCosecha.Text = "";
                txtCosecha.Focus();
                errors++;
                return;
            }

            if (bodega.Length == 0)
            {
                a.Advertencia("¡SELECCIONAR LA BODEGA ES REQUERIDO!");
                cmbBodega.Text = "";
                cmbBodega.Focus();
                errors++;
                return;
            }

            if (empleado.Length == 0)
            {
                a.Advertencia("¡SELECCIONAR EL EMPLEADO ES REQUERIDO!");
                cmbEmpleado.Text = "";
                cmbEmpleado.Focus();
                errors++;
                return;
            }

            if (secadora.Length == 0)
            {
                a.Advertencia("¡SELECCIONAR LA SECADORA ES REQUERIDO!");
                cmbSecadora.Text = "";
                cmbSecadora.Focus();
                errors++;
                return;
            }

            if (qq_brutos_h.Length == 0)
            {
                a.Advertencia("¡LOS QUINTALES BRUTOS HÚMEDO REQUERIDO!");
                errors++;
                return;
            }


            //if (tolva_seco.Length == 0)
            //{
            //    a.Advertencia("¡LA TOLVA SECO ES REQUERIDA!");
            //    cmbTolva_Almacen.Text = "";
            //    cmbTolva_Almacen.Focus();
            //    errors++;
            //    return;
            //}

            //if (qq_secos.Length == 0)
            //{
            //    a.Advertencia("¡LOS QUINTALES SECOS SON REQUERIDOS!");
            //    txtQQSecos.Text = "";
            //    txtQQSecos.Focus();
            //    errors++;
            //    return;
            //}

            //if (rendimiento.Length == 0)
            //{
            //    a.Advertencia("¡EL RENDIMIENTO ES REQUERIDO!");
            //    txtQQBrutosH.Text = "";
            //    txtQQBrutosH.Focus();
            //    errors++;
            //    return;
            //}

            //if (temperatura.Length == 0)
            //{
            //    a.Advertencia("¡LA TEMPERATURA ES REQUERIDA!");
            //    txtTemperatura.Text = "";
            //    txtTemperatura.Focus();
            //    errors++;
            //    return;
            //}

            //if (porcentaje_humedad.Length == 0)
            //{
            //    a.Advertencia("¡EL PORCENTAJE DE HUMEDAD ES REQUERIDO!");
            //    txtPor_Humedad.Text = "";
            //    txtPor_Humedad.Focus();
            //    errors++;
            //    return;
            //}
        }

        private void Boot()
        {
            btnNuevo.Enabled = Clases.Auth.save == "S" ? true : false;
            btnGuardar.Enabled = false;
            btnCancelar.Enabled = false;
            pbSalir.Enabled = true;
            

            dtpFechaInicio.Enabled = false;

            txtNoSecado.Enabled = false;

            if (txtNoSecado.Text != "")
            {
                btnNuevo.Enabled = false;
                btnActualizar.Enabled = true;
                BtnBuscar.Enabled = true;
                btnAgregarNP.Enabled = true;
                btnCancelar.Enabled = true;
                btnEliminar.Enabled = true;

                txtQQSecos.Enabled = true;
                cmbTolva_Almacen.Enabled = true;
                txtTemperatura.Enabled = true;
                txtPor_Humedad.Enabled = true;

                dtpFechaFinal.Enabled = true;
                dtpFechaAggNotaPeso.Enabled = true;

                cbxSecado_Completo.Enabled = true;

                btnListaSecada.Enabled = false;
                

            }
            else
            {
                btnActualizar.Enabled = false;
                btnAgregarNP.Enabled = false;
                BtnBuscar.Enabled = false;

                txtQQSecos.Enabled = false;
                cmbTolva_Almacen.Enabled = false;
                txtTemperatura.Enabled = false;
                txtPor_Humedad.Enabled = false;

                dtpFechaFinal.Enabled = false;
                dtpFechaAggNotaPeso.Enabled = false;

                cbxSecado_Completo.Enabled = false;
                btnEliminar.Enabled = false;

                btnSecadasCompletas.Enabled = true;
                btnListaSecada.Enabled = true;

            }

            txtCosecha.Enabled = false;
            cmbSecadora.Enabled = false;
            cmbBodega.Enabled = false;
            cmbEmpleado.Enabled = false;
            txtRendimiento.Enabled = false;

            txtNoNotaPeso.Enabled = false;
            txtSocio.Enabled = false;
            txtQQ.Enabled = false;
            //DgvData.Rows.Clear();

        }

        private void Clear()
        {
            dtpFechaInicio.Text = "";
            dtpFechaFinal.Text = "";

            txtNoSecado.Clear();
            txtCosecha.Clear();
            cmbBodega.SelectedIndex = -1;
            cmbSecadora.SelectedIndex = -1;
            cmbEmpleado.SelectedIndex = -1;
            cmbTolva_Almacen.SelectedIndex = -1;
            txtQQSecos.Clear();
            txtRendimiento.Clear();
            txtTemperatura.Clear();
            txtPor_Humedad.Clear();

            btnAgregarNP.Enabled = false;
            DgvData.Rows.Clear();
        }

    }
}
