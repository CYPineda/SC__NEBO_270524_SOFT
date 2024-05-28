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

namespace SC__NEBO.Formularios
{
    public partial class Frm_Menu : Form
    {

        Clases.DB db = new Clases.DB();
        Clases.Asistente h = new Clases.Asistente();

        public Frm_Menu()
        {
            InitializeComponent();
            this.FormClosing += MainForm_FormClosing;
        }

        private void btnNotaPeso_Click(object sender, EventArgs e)
        {
            Formularios.Formularios_de_Menu.Notas_de_Peso.Frm_Notas_Peso form = new Formularios_de_Menu.Notas_de_Peso.Frm_Notas_Peso();
            this.AddOwnedForm(form);
            form.Show();

        }

        private void btnClientes_Click(object sender, EventArgs e)
        {
            Formularios.Formularios_de_Menu.Clientes.Frm_Clientes form = new Formularios_de_Menu.Clientes.Frm_Clientes();
            this.AddOwnedForm(form);
            form.Show();
        }

        private void Frm_Menu_Load(object sender, EventArgs e)
        {
            SetAccessUSer();
            SubMenuSecado.Visible = false;
            SubMenuPrestamos.Visible = false;
        }

        private void pbSalir_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Está seguro de cerrar?", "Alerta", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void pbMinimizar_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void btnClientes_Click_1(object sender, EventArgs e)
        {
            //AbrirFormHijo(new Formularios_de_Menu.Clientes.Frm_Clientes());
            Formularios.Formularios_de_Menu.Socios.Frm_Socios form = new Formularios_de_Menu.Socios.Frm_Socios();
            this.AddOwnedForm(form);
            form.Show();
            SubMenuUsuarios.Visible = false;
            this.tmContraerMenu.Start();

            SubMenuUsuarios.Visible = false;
            SubMenuSecado.Location = new Point(44, 360);
        }

        private void btnNotaPeso_Click_1(object sender, EventArgs e)
        {

            Formularios.Formularios_de_Menu.Notas_de_Peso.Frm_Notas_Peso form = new Formularios_de_Menu.Notas_de_Peso.Frm_Notas_Peso();
            this.AddOwnedForm(form);
            form.Show();
            SubMenuUsuarios.Visible = false;

            this.tmContraerMenu.Start();

            SubMenuUsuarios.Visible = false;
            SubMenuSecado.Location = new Point(44, 360);
        }



        private void BtnConfiguracion_Click(object sender, EventArgs e)
        {

            if (SubMenuUsuarios.Visible == true)
            {
                SubMenuUsuarios.Visible = false;
            }
            else
            {
                SubMenuUsuarios.Visible = true;
            }
        }

        private void btnPrestamos_Click(object sender, EventArgs e)
        {
            if (SubMenuPrestamos.Visible == true)
            {
                SubMenuPrestamos.Visible = false;
            }
            else
            {
                SubMenuPrestamos.Visible = true;
            }

            SubMenuUsuarios.Visible = false;
            //SubMenuPrestamos.Location = new Point(44, 360);
        }

        private void btnLquidación_Click(object sender, EventArgs e)
        {
            Formularios.Formularios_de_Menu.Liquidacion.Frm_Liquidacion form = new Formularios_de_Menu.Liquidacion.Frm_Liquidacion();
            this.AddOwnedForm(form);
            form.Show();
            SubMenuUsuarios.Visible = false;

            this.tmContraerMenu.Start();

            SubMenuUsuarios.Visible = false;
            SubMenuSecado.Location = new Point(44, 360);
        }

        private void btnReporte_Click(object sender, EventArgs e)
        {
            //Formularios.Formularios_de_Menu.Reportes.Frm_Reportes form = new Formularios_de_Menu.Reportes.Frm_Reportes();
            //this.AddOwnedForm(form);
            //form.Show();
            SubMenuUsuarios.Visible = false;
        }


        private void pictureBox3_Click(object sender, EventArgs e)
        {

        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panelcontenedor_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pbMenu_Click(object sender, EventArgs e)
        {

        }



        private void button1_Click(object sender, EventArgs e)
        {
      
        }

        private void btnMenu_Click(object sender, EventArgs e)
        {
            if (Menu_Lateral.Width == 219)
            {
                this.tmContraerMenu.Start();
                SubMenuPrestamos.Location = new Point(44, 138);
                SubMenuSecado.Location = new Point(44, 360);
            }
            else if (Menu_Lateral.Width == 44)
            {
                this.tmExpandirMenu.Start();
                SubMenuPrestamos.Location = new Point(219, 138);
                SubMenuSecado.Location = new Point(219, 360);                
            }
        }

        private void tmExpandirMenu_Tick_1(object sender, EventArgs e)
        {
            if (Menu_Lateral.Width >= 219)
                this.tmExpandirMenu.Stop();
            else
                Menu_Lateral.Width = Menu_Lateral.Width + 5;
        }

        private void tmContraerMenu_Tick(object sender, EventArgs e)
        {
            if (Menu_Lateral.Width <= 44)
                this.tmContraerMenu.Stop();
            else
                Menu_Lateral.Width = Menu_Lateral.Width - 5;
        }

        //ABRIR UN FORMULARION DENTRO DEL PANEL
        //private void AbrirFormHijo(object formhijo)
        //{
        //    if (this.panelcontenedor.Controls.Count > 0)
        //        this.panelcontenedor.Controls.RemoveAt(0);
        //    Form fh = formhijo as Form;
        //    fh.TopLevel = false;
        //    fh.Dock = DockStyle.Fill;
        //    this.panelcontenedor.Controls.Add(fh);
        //    this.panelcontenedor.Tag = fh;
        //    fh.Show();

        //}

        private void SetAccessUSer()
        {
            DataTable ModulosSistema = new DataTable();
            string campos, condicion;
            campos = "A.IDMOD, B.MODULO, A.ACCESO, C.COMANDO FROM PERMISOSUSUARIO A INNER JOIN " +
                "MODULOSSISTEMA B ON(A.IDMOD = B.IDMOD) INNER JOIN COMANDOSSISTEMA C ON" +
                "(A.IDMOD = C.IDMOD)";
            condicion = "A.USUARIO='" + Clases.Auth.user + "'"; //NO ESTOY SEGURO CON ENV

            ModulosSistema = db.Join(campos, condicion); //NO ESTOY SEGURO JOIN

            foreach (Button Btn in Menu_Lateral.Controls.OfType<Button>())
            {
                string botonactual = Btn.Name;
                int i, coinc = 0;

                for (i = 0; i < ModulosSistema.Rows.Count; i++)
                {
                    string comando = ModulosSistema.Rows[i][3].ToString();
                    string acces = ModulosSistema.Rows[i][2].ToString();

                    if (botonactual == comando && acces == "S")
                    {
                        coinc++;
                    }
                    Btn.Enabled = coinc > 0 ? true : false;
                }

            }



        }

        private void btnUsuarios_Click(object sender, EventArgs e)
        {
            Formularios.Formularios_de_Menu.Usuarios.FrmUsuarios frm = new Formularios_de_Menu.Usuarios.FrmUsuarios();
            this.AddOwnedForm(frm);
            frm.Show();

            SubMenuUsuarios.Visible = false;
            this.tmContraerMenu.Start();
            SubMenuSecado.Location = new Point(44, 360);
        }

        private void btnCosechas_Click(object sender, EventArgs e)
        {
            Formularios.Formularios_de_Menu.Cosechas.FrmCosechas frm = new Formularios_de_Menu.Cosechas.FrmCosechas();
            this.AddOwnedForm(frm);
            frm.Show();

            SubMenuUsuarios.Visible = false;
            this.tmContraerMenu.Start();
            SubMenuSecado.Location = new Point(44, 360);
        }

        private void btnIHCAFE_Click_1(object sender, EventArgs e)
        {
            Formularios.Formularios_de_Menu.IHCAFE.FrmSocios_IHCAFE form = new Formularios_de_Menu.IHCAFE.FrmSocios_IHCAFE();
            this.AddOwnedForm(form);
            form.Show();
            SubMenuUsuarios.Visible = false;

            this.tmContraerMenu.Start();

            SubMenuUsuarios.Visible = false;
            SubMenuSecado.Location = new Point(44, 360);
        }

        private void btnOrdenEntrega_Click(object sender, EventArgs e)
        {
            Formularios.Formularios_de_Menu.Ordenes_Entrega.FrmOrden_Entrega form = new Formularios_de_Menu.Ordenes_Entrega.FrmOrden_Entrega();
            this.AddOwnedForm(form);
            form.Show();
            SubMenuUsuarios.Visible = false;

            this.tmContraerMenu.Start();

            SubMenuUsuarios.Visible = false;
            SubMenuSecado.Location = new Point(44, 360);
        }

        private void btnProveedores_Click(object sender, EventArgs e)
        {
            Formularios.Formularios_de_Menu.Contratos.Frm_Contratos form = new Formularios_de_Menu.Contratos.Frm_Contratos();
            this.AddOwnedForm(form);
            form.Show();
            SubMenuUsuarios.Visible = false;

            this.tmContraerMenu.Start();

            SubMenuUsuarios.Visible = false;
            SubMenuSecado.Location = new Point(44, 360);
        }

        private void btnControlSecado_Click(object sender, EventArgs e)
        {
            if (SubMenuSecado.Visible == true)
            {
                SubMenuSecado.Visible = false;          
            }
            else
            {
                SubMenuSecado.Visible = true;
            }
        }

        private void btnMaquinasSecado_Click(object sender, EventArgs e)
        {
            Formularios.Formularios_de_Menu.Secadoras.FrmMaquinas form = new Formularios_de_Menu.Secadoras.FrmMaquinas();
            this.AddOwnedForm(form);
            form.Show();

            this.tmContraerMenu.Start();

            SubMenuSecado.Visible = false;
            SubMenuSecado.Location = new Point(44, 360);
        }

        private void btnSecadas_Click(object sender, EventArgs e)
        {
            Formularios.Formularios_de_Menu.Secadoras.FrmControlSecado form = new Formularios_de_Menu.Secadoras.FrmControlSecado();
            this.AddOwnedForm(form);
            form.Show();

            this.tmContraerMenu.Start();

            SubMenuSecado.Visible = false;
            SubMenuSecado.Location = new Point(44, 360);
        }


        private void btnEmpleados_Click(object sender, EventArgs e)
        {
            Formularios.Formularios_de_Menu.Empleados.FrmEmpleados frmEmpleados = new Formularios.Formularios_de_Menu.Empleados.FrmEmpleados();
            frmEmpleados.Show();
        }
        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Verificar si la razón del cierre es el botón de cerrar (X)
            if (e.CloseReason == CloseReason.UserClosing)
            {
                if (Application.OpenForms["Frm_Liquidacion"] != null)
                {

                    // Mostrar un cuadro de diálogo de advertencia al intentar cerrar la pestaña
                    // h.Advertencia("¿ESTA SEGURO QUE DESEA SALIR DE LA APLICACIÓN? PODRIAS PERDER INFORMACION.");
                    DialogResult result = MessageBox.Show("¿ESTA SEGURO QUE DESEA SALIR DE LA APLICACIÓN? CIERRA ANTES SI TIENES PESTAÑAS ABIERTAS.", "ADVERTENCIA", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                    // Si el usuario elige "No", cancelar el cierre del formulario
                    if (result == DialogResult.No)
                    {
                        e.Cancel = true;
                    }
                    else
                    {
                        e.Cancel = true;
                    }
                }

            }
        }

        private void Frm_Menu_FormClosed_1(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void btnNuevoPrestamo_Click(object sender, EventArgs e)
        {
            Formularios.Formularios_de_Menu.Prestamos.Frm_Prestamos form = new Formularios_de_Menu.Prestamos.Frm_Prestamos();
            this.AddOwnedForm(form);
            form.Show();
            SubMenuPrestamos.Visible = false;
            this.tmContraerMenu.Start();
        }

        private void btnListaPrestamos_Click(object sender, EventArgs e)
        {
            Formularios.Formularios_de_Menu.Prestamos.FrmHistorialPrestamos form = new Formularios_de_Menu.Prestamos.FrmHistorialPrestamos();
            this.AddOwnedForm(form);
            form.Show();
            SubMenuPrestamos.Visible = false;
            this.tmContraerMenu.Start();
        }
    }
}
