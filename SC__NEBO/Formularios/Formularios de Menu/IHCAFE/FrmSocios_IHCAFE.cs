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
    public partial class FrmSocios_IHCAFE : Form
    {

        Clases.DB db = new Clases.DB();
        Clases.Asistente a = new Clases.Asistente();

        public FrmSocios_IHCAFE()
        {
            InitializeComponent();
        }

        private void FrmSocios_IHCAFE_Load(object sender, EventArgs e)
        {
            GetSocio();
        }

        private void GetSocio(string search = "")
        {
            string campos, condicion;
            campos = "ID_SOCIO, NOMBRE, DNI, DIRECCION, CLAVE_IHCAFE";

            if (search != "")
            {
                condicion = $"NOMBRE LIKE '% '{search}'%' AND CLAVE_IHCAFE != '-  -' AND CLAVE_IHCAFE != ''  AND CLAVE_IHCAFE != '00-00-00000'";

            }
            else
            {
                condicion = "CLAVE_IHCAFE != '-  -' AND CLAVE_IHCAFE != ''  AND CLAVE_IHCAFE != '00-00-00000'";
            }

            DataTable data = db.Find("SOCIOS", campos, condicion);

            DgvData.Rows.Clear();

            int i;
            string _id_cliente, _nombre, _dni, _telefono, _direccion;

            for (i = 0; i < data.Rows.Count; i++)
            {
                _id_cliente = data.Rows[i][0].ToString();
                _nombre = data.Rows[i][1].ToString();
                _dni = data.Rows[i][2].ToString();
                _telefono = data.Rows[i][3].ToString();
                _direccion = data.Rows[i][4].ToString();
                DgvData.Rows.Add(_id_cliente, _nombre, _dni, _telefono, _direccion);
            }

            //lblResumen.Text = "Mostrando " + data.Rows.Count.ToString() + " registros de " + db.Count("SOCIOS", "DEL = 'N'").ToString();
            data.Dispose();
        }

        private void BtnBuscar_Click(object sender, EventArgs e)
        {
            string search = a.Clean(txtBuscar.Text.Trim());
            GetSocio(search);
        }

        private void txtBuscar_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                GetSocio(a.Clean(txtBuscar.Text.Trim()));
            }
        }

        private void BtnComprobante_Click(object sender, EventArgs e)
        {
            Formularios.Formularios_de_Menu.IHCAFE.FrmListaComprobantesIHCAFE_Socio_X ihcafe = new Formularios.Formularios_de_Menu.IHCAFE.FrmListaComprobantesIHCAFE_Socio_X();
            string codigo_socio = DgvData.CurrentRow.Cells[0].Value.ToString();
            string aldea= db.Hook("DIRECCION", "SOCIOS", $"ID_SOCIO='{codigo_socio}'");
            string municipio= db.Hook("MUNICIPIO", "SOCIOS", $"ID_SOCIO='{codigo_socio}'");
            string departamento= db.Hook("DEPARTAMENTO", "SOCIOS", $"ID_SOCIO='{codigo_socio}'");

            ihcafe.cod_socio = codigo_socio;
            ihcafe.socio = db.Hook("NOMBRE","SOCIOS",$"ID_SOCIO='{codigo_socio}'");
            ihcafe.rtn = db.Hook("RTN", "SOCIOS", $"ID_SOCIO='{codigo_socio}'");
            ihcafe.clave_ihcafe = db.Hook("CLAVE_IHCAFE", "SOCIOS", $"ID_SOCIO='{codigo_socio}'");
            
            ihcafe.direccion = aldea+", "+ municipio + ", " + departamento;
            

            if (db.CheckIfExists("CAB_COMPROBANTE_IHCAFE",$"SOCIO_PRINC='{codigo_socio}'")==false)
            {
                string cosecha_id = db.Hook("ID_COSECHA", "COSECHAS", "ESTADO='ACTIVO'");
                string fields = "SOCIO_PRINC, COSECHA_ID, ALDEA, MUNICIPIO, DEPARTAMENTO, PRECIO_QQ, CANT_QQ_ORO, CANT_QQ_LETRAS, TOTAL, TOTAL_LETRAS";
                string values = $"'{codigo_socio}',{ a.ReturnsNumber(cosecha_id)},'{aldea}','{municipio}','{departamento}',0,0,'cero',0,'cero'";
                
                if (db.Save("CAB_COMPROBANTE_IHCAFE", fields, values,true)>0) 
                {
                    int ref_comp = db.GetAfterId("CAB_COMPROBANTE_IHCAFE");

                    DataTable data = db.Find("CAB_LIQUIDACION","*", $"ID_SOCIO='{codigo_socio}'");

                    double num_liq;
                    double total_qq=0, precio_cont=0, precio_plaza=0,precio_prom=0, total_mon=0;
                    double precio_qq_prom_t=0, cant_qq_oro_t=0, tot_mon_t=0;
                    foreach (DataRow rows in data.Rows)
                    {
                        num_liq = a.ReturnsNumber(rows["NUM_LIQUIDACION"].ToString());
                        precio_cont = a.ReturnsNumber(rows["PRECIO_QQ_CONT"].ToString());
                        precio_plaza = a.ReturnsNumber(rows["PRECIO_QQ_PP"].ToString());
                        total_qq = a.ReturnsNumber(rows["TOTAL_QQ"].ToString())/1.25;
                        total_mon = a.ReturnsNumber(rows["TOTAL_MONEDA"].ToString());

                        precio_prom = (precio_plaza > 0 && precio_cont > 0) ? precio_prom = precio_cont + precio_plaza / 2 : precio_cont + precio_plaza;
                        
                        precio_qq_prom_t += precio_prom;
                        cant_qq_oro_t += total_qq;
                        tot_mon_t += total_mon;

                        string fields_dt = "REF_COMP, COD_SOC, NUM_LIQ, PRECIO, CANT_QQ, TOTAL";
                        string values_dt = $"{ref_comp},'{codigo_socio}',{num_liq},{precio_prom},{total_qq},{total_mon}";
                        db.Save("DETALLE_COMPROBANTE_IHCAFE", fields_dt, values_dt);

                        
                    }
                    string up_fields = $"PRECIO_QQ={precio_prom}, CANT_QQ_ORO={cant_qq_oro_t}, CANT_QQ_LETRAS='{cant_qq_oro_t}', " +
                        $"TOTAL={tot_mon_t}, TOTAL_LETRAS = '{tot_mon_t}'";
                    db.Update("CAB_COMPROBANTE_IHCAFE", up_fields, $"REF_COMP={ref_comp}");

                }
                
            }
            this.AddOwnedForm(ihcafe);
            ihcafe.Show();
        }

        private void BtnAgregarAsociado_Click(object sender, EventArgs e)
        {
            Formularios.Formularios_de_Menu.IHCAFE.Frm_Lista_Socios_Adicionales ihcafe = new Formularios.Formularios_de_Menu.IHCAFE.Frm_Lista_Socios_Adicionales();
            this.AddOwnedForm(ihcafe);
            ihcafe.Show();
        }
    }
}
