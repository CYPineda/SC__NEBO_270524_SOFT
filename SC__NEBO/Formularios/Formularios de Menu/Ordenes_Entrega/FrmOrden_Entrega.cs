using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SC__NEBO.Formularios.Formularios_de_Menu.Ordenes_Entrega
{
    public partial class FrmOrden_Entrega : Form
    {

        Clases.DB db = new Clases.DB();
        Clases.Asistente a = new Clases.Asistente();

        string ordenentrega, fecha, proveedor, descripcion, empleado;

        int errors;

        public FrmOrden_Entrega()
        {
            InitializeComponent();
        }

        private void FrmOrden_Entrega_Load(object sender, EventArgs e)
        {
            this.Text = Clases.Env.APPNAME + " | ORDENES DE ENTREGA | " + Clases.Auth.user + " | " + Clases.Auth.rol;
            Proveedores();
            Empleados();
            Boot();
        }

        private void Proveedores()
        {
            cmbProveedor.DataSource = db.Find("PROVEEDORES", "CODIGO, PROVEEDOR", "", "PROVEEDOR");
            cmbProveedor.ValueMember = "CODIGO";
            cmbProveedor.DisplayMember = "PROVEEDOR";
            cmbProveedor.SelectedIndex = -1;
        }

        private void Empleados()
        {
            cmbEmpleado.DataSource = db.Find("EMPLEADOS", "ID_EMPLEADO, NOMBRE", "DEL='N'", "NOMBRE");
            cmbEmpleado.ValueMember = "ID_EMPLEADO";
            cmbEmpleado.DisplayMember = "NOMBRE";
            cmbEmpleado.SelectedIndex = -1;
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            btnNuevo.Enabled = false;
            btnGuardar.Enabled = Clases.Auth.save == "S" ? true : false;
            btnCancelar.Enabled = true;
            btnRegresar.Enabled = false;
            btnProveedores.Enabled = false;

            txtOrdenEntrega.Text = "ORD" + db.GetNext("ORDEN");

            dtpFecha.Enabled = true;
            cmbProveedor.Enabled = true;
            txtDesripcion.Enabled = true;
            cmbEmpleado.Enabled = true;
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            ValidateData();

            string msg = "¿DESEA GUARDAR ESTA ORDEN DE ENTREGA?";

            if (errors == 0)
            {
                if (a.Pregunta(msg) == true)
                {
                    //No se si ya se creo esta tabla
                    string campos = "COD_ORDEN, DESCRIPCION, COD_PROVEEDOR, ID_EMPLEADO, FECHA";

                    string valores = "'" + ordenentrega + "', '" + descripcion + "', '" + proveedor + "','" + empleado + "','" + fecha + "'";


                    if (db.Save("ORDEN_DE_ENTREGA", campos, valores) > 0)
                    {
                        //db.RawSQL(query);

                        Reportes.FrmRptOrden_Entrega OrdenEntrega = new Reportes.FrmRptOrden_Entrega();
                        OrdenEntrega.codentrega = ordenentrega;
                        OrdenEntrega.Show();


                        db.SetLast("ORDEN");
                        a.Aprueba("LA ORDEN DE ENTREGA HA SIDO REGISTRADA CON ÉXITO!");
                        Clear();
                        Boot();
                    }
                }
            }
        }

        private void pbSalir_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnProveedores_Click(object sender, EventArgs e)
        {
            Formularios.Formularios_de_Menu.Proveedores.FrmProveedores frmProveedores = new Formularios.Formularios_de_Menu.Proveedores.FrmProveedores();
            frmProveedores.Show();
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

        private void Clear()
        {
            txtOrdenEntrega.Clear();
            dtpFecha.Text = "";
            cmbProveedor.SelectedIndex = -1;
            txtDesripcion.Text = "";
            cmbEmpleado.SelectedIndex = -1;
        }

        public void ValidateData()
        {
            errors = 0;
            ordenentrega = a.Clean(txtOrdenEntrega.Text.Trim());
            fecha = a.Clean(dtpFecha.Text.Trim());
            proveedor = cmbProveedor.Text != "" ? cmbProveedor.SelectedValue.ToString() : "";
            descripcion = a.Clean(txtDesripcion.Text.Trim());
            empleado = cmbEmpleado.Text != "" ? cmbEmpleado.SelectedValue.ToString() : "";


            if (ordenentrega.Length == 0)
            {
                a.Advertencia("¡EL CODIGO DE LA ORDEN DE ENTREGA ES REQUERIDO!");
                txtOrdenEntrega.Text = "";
                txtOrdenEntrega.Focus();
                errors++;
                return;
            }

            if (fecha.Length == 0)
            {
                a.Advertencia("¡SELECCIONE UNA FECHA FINAL VÁLIDA!");
                dtpFecha.Text = "";
                dtpFecha.Focus();
                errors++;
                return;
            }


            if (proveedor.Length == 0)
            {
                a.Advertencia("¡SELECCIONE EL PROVEEDOR CORRESPONDIENTE!");
                cmbProveedor.Text = "";
                cmbProveedor.Focus();
                errors++;
            }


            if (descripcion.Length == 0)
            {
                a.Advertencia("¡LA DESCRIPCIÓN DE LA ORDEN ES REQUERIDA!");
                txtDesripcion.Text = "";
                txtDesripcion.Focus();
                errors++;
            }

            if (empleado.Length == 0)
            {
                a.Advertencia("¡SELECCIONE EL EMPLEADO CORRESPONDIENTE!");
                cmbEmpleado.Text = "";
                cmbEmpleado.Focus();
                errors++;
            }

        }

        private void Boot()
        {
            txtOrdenEntrega.Enabled = false;
            btnNuevo.Enabled = Clases.Auth.save == "S" ? true : false;
            btnGuardar.Enabled = false;
            btnCancelar.Enabled = false;
            pbSalir.Enabled = true;

            cmbProveedor.Enabled = false;

            dtpFecha.Enabled = false;

            cmbEmpleado.Enabled = false;
            txtDesripcion.Enabled = false;
            btnProveedores.Enabled = true;

        }
    }
}
