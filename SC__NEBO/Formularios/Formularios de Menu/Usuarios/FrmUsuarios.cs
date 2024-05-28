using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SC__NEBO.Formularios.Formularios_de_Menu.Usuarios
{
    public partial class FrmUsuarios : Form
    {

        Clases.DB db = new Clases.DB();
        Clases.Asistente a = new Clases.Asistente();
        Clases.Auth auth = new Clases.Auth();

        int errors;
        string usuario, contrasena, confirmar, nombre, correo, sexo, fechanac, rol;

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            string msg = "¿DESA ELMINAR EL REGISTRO SELECCIONADO?";

            if (a.Pregunta(msg) == true)
            {
                string idusuario = TxtUsuario.Text.Trim();
                if (db.Delete_Estado("USUARIOS", "USUARIO", idusuario) > 0)
                {
                    a.Aprueba("EL USUARIO SE ELIMINO CON EXITO!");
                    Clear();
                    Boot();
                    GetUsuarios();
                }
            }
        }

        private void BtnGestionarPermisos_Click(object sender, EventArgs e)
        {
            if (DgvData.Rows.Count > 0)
            {
                string username, realname, role;
                username = DgvData.CurrentRow.Cells[0].Value.ToString();
                realname = DgvData.CurrentRow.Cells[1].Value.ToString();
                role = DgvData.CurrentRow.Cells[2].Value.ToString();

                Formularios.Formularios_de_Menu.Usuarios.FrmPermisos frmPermisos = new Formularios.Formularios_de_Menu.Usuarios.FrmPermisos();
                this.AddOwnedForm(frmPermisos);
                frmPermisos.username = username;
                frmPermisos.realname = realname;
                frmPermisos.role = role;
                frmPermisos.ShowDialog();
            }
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            btnNuevo.Enabled = false;
            btnGuardar.Enabled = Clases.Auth.save == "S" ? true : false;
            btnActualizar.Enabled = false;
            btnEliminar.Enabled = true;
            btnCancelar.Enabled = true;
            pbSalir.Enabled = false;

            TxtUsuario.Enabled = true;
            TxtNombre.Enabled = true;
            TxtContrasena.Enabled = true;
            TxtConfirmar.Enabled = true;
            TxtCorreo.Enabled = true;
            CmbRol.Enabled = true;
            CmbSexo.Enabled = true;
            DtpFechaNac.Enabled = true;
        }

        private void DgvData_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (DgvData.Rows.Count > 0)
            {
                string id = DgvData.CurrentRow.Cells[0].Value.ToString();
                GetInfoUsuario(id);
            }
        }

        public FrmUsuarios()
        {
            InitializeComponent();
        }

        private void FrmUsuarios_Load(object sender, EventArgs e)
        {
            Boot();
            GetUsuarios();
        }

        private void GetUsuarios()
        {
            string campos = "USUARIO, NOMBRE, ROL";
            string condicion = "USUARIO<>'Admin'AND ESTADO='ACTIVO'";

            DataTable data = db.Find("USUARIOS", campos, condicion);

            DgvData.Rows.Clear();
            string _usuario, _nombre, _rol;
            int i;
            for (i = 0; i < data.Rows.Count; i++)
            {
                _usuario = data.Rows[i][0].ToString();
                _nombre = data.Rows[i][1].ToString();
                _rol = data.Rows[i][2].ToString();

                DgvData.Rows.Add(_usuario, _nombre, _rol);
            }
            data.Dispose();
        }

        private void pbSalir_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void DgvData_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            ValidatedData();

            string campos, valores;
            campos = "USUARIO, CLAVE, NOMBRE,CORREO, FECHANAC, SEXO, ROL";
            valores = "'" + usuario + "','" +auth.MakeHash(contrasena) + "','" + nombre + "','" + correo + "','" + fechanac + "','" + sexo + "','" + rol + "'";

            if (errors == 0)
            {

                if (db.Save("USUARIOS", campos, valores) > 0)
                {
                    //registramos los permisos del usuario
                    string q = "INSERT INTO PERMISOSUSUARIO(USUARIO,IDMOD) SELECT '" + usuario + "'," +
                        "IDMOD FROM MODULOSSISTEMA";

                    db.RawSQL(q);
                    a.Aprueba("EL USUARIO SE REGISTRO CON EXITO");

                    GetUsuarios();
                    Clear();
                    Boot();
                }

            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            string msg = "¿DESEA CANCELAR LA OPERCION EN CURSO?";
            if (a.Pregunta(msg) == true)
            {
                Clear();
                Boot();
            }
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            ValidatedData();
            if (errors == 0)
            {
                string msg = "DESEA GUARDAR LOS CAMBIOS DEL REGISTRO SELECCIONADO";
                if (a.Pregunta(msg) == true)
                {
                    string idusuario = TxtUsuario.Text.Trim();
                    string stmt = "CLAVE='" + auth.MakeHash(contrasena) + "',NOMBRE='" + nombre + "',CORREO='" + correo + "'," +
                        "FECHANAC='" + fechanac + "',SEXO='" + sexo + "',ROL='" + rol + "'";
                    string condicion = "USUARIO='" + idusuario + "'";

                    if (db.Update("USUARIOS", stmt, condicion) > 0)
                    {
                        a.Aprueba("LOS CAMBIOS SE APLICARON CORRECTAMENTE!");
                        Clear();
                        Boot();
                        GetUsuarios();
                    }
                }
            }
        }


        private void ValidatedData()
        {
            errors = 0;
            usuario = a.Clean(TxtUsuario.Text.Trim());
            contrasena = a.Clean(TxtContrasena.Text.Trim());
            confirmar = a.Clean(TxtConfirmar.Text.Trim());
            nombre = a.Clean(TxtNombre.Text.Trim());
            correo = a.Clean(TxtCorreo.Text.Trim());
            sexo = a.Clean(CmbSexo.Text.Trim());
            fechanac = a.Clean(DtpFechaNac.Text.Trim());
            rol = a.Clean(CmbRol.Text.Trim());

            if (usuario.Length == 0)
            {
                a.Advertencia("¡INGRESAR UN CÓDIGO VÁLIDO!");
                TxtUsuario.Text = usuario;
                TxtUsuario.Focus();
                errors++;
                return;
            }

            if (contrasena.Length == 0)
            {
                a.Advertencia("¡INGRESAR UNA CONTRASEÑA VALIDA!");
                TxtContrasena.Text = contrasena;
                TxtContrasena.Focus();
                errors++;
                return;
            }

            if (confirmar.Length == 0)
            {
                a.Advertencia("¡LAS CONTRASEÑAS NO PARECEN COINCIDIR!");
                TxtConfirmar.Text = contrasena;
                TxtConfirmar.Focus();
                errors++;
                return;
            }

            if (contrasena != confirmar)
            {
                a.Advertencia("¡LAS CONTRASEÑAS NO COINCIDEN!");
                TxtContrasena.Text = "";
                TxtConfirmar.Text = "";
                TxtContrasena.Focus();
                errors++;
                return;
            }


            if (nombre.Length == 0)
            {
                a.Advertencia("¡INGRESA UN NOMBRE VALIDO!");
                TxtNombre.Text = nombre;
                TxtNombre.Focus();
                errors++;
                return;
            }

            if (sexo.Length == 0)
            {
                a.Advertencia("¡INGRESA EL SEXO!");
                CmbSexo.Text = sexo;
                CmbSexo.Focus();
                errors++;
                return;
            }

            if (fechanac.Length == 0)
            {
                a.Advertencia("¡INGRESA UNA FECHA NACIMIENTO VALIDA!");
                DtpFechaNac.Text = fechanac;
                DtpFechaNac.Focus();
                errors++;
                return;
            }

            if (rol.Length == 0)
            {
                a.Advertencia("¡INGRESA UNA FECHA NACIMIENTO VALIDA!");
                CmbRol.Text = rol;
                CmbRol.Focus();
                errors++;
                return;
            }

        }

        private void Boot()
        {
            btnNuevo.Enabled = Clases.Auth.save == "S" ? true : false;
            btnGuardar.Enabled = false;
            btnActualizar.Enabled = false;
            btnEliminar.Enabled = false;
            btnCancelar.Enabled = false;
            pbSalir.Enabled = true;


            TxtUsuario.Enabled = false;
            TxtNombre.Enabled = false;
            TxtContrasena.Enabled = false;
            TxtConfirmar.Enabled = false;
            TxtCorreo.Enabled = false;
            CmbRol.Enabled = false;
            CmbSexo.Enabled = false;
            DtpFechaNac.Enabled = false;

        }

        private void Clear()
        {
            TxtUsuario.Clear();
            TxtNombre.Clear();
            TxtContrasena.Clear();
            TxtConfirmar.Clear();
            TxtCorreo.Clear();
            CmbRol.SelectedIndex = -1;
            CmbSexo.SelectedIndex = -1;
        }

        private void GetInfoUsuario(string id)
        {
            string condicion = "USUARIO='" + id + "' AND ESTADO='ACTIVO'";
            DataTable cliente = db.Find("USUARIOS", "*", condicion);

            if (cliente.Rows.Count > 0)
            {
                btnNuevo.Enabled = false;
                btnGuardar.Enabled = false;
                btnActualizar.Enabled = Clases.Auth.update == "S" ? true : false;
                btnEliminar.Enabled = Clases.Auth.delete == "S" ? true : false;
                btnCancelar.Enabled = true;


                DataRow info = cliente.Rows[0];
                TxtUsuario.Text = info["USUARIO"].ToString();
                //TxtContrasena.Text = info["CLAVE"].ToString();
                TxtNombre.Text = info["NOMBRE"].ToString();
                TxtCorreo.Text = info["CORREO"].ToString();
                CmbSexo.Text = info["SEXO"].ToString();
                DtpFechaNac.Text = info["FECHANAC"].ToString();
                CmbRol.Text = info["ROL"].ToString();


                TxtUsuario.Enabled = false;
                TxtContrasena.Enabled = Clases.Auth.update == "S" ? true : false;
                TxtNombre.Enabled = Clases.Auth.update == "S" ? true : false;
                TxtCorreo.Enabled = Clases.Auth.update == "S" ? true : false;
                DtpFechaNac.Enabled = Clases.Auth.update == "S" ? true : false;
                CmbRol.Enabled = Clases.Auth.update == "S" ? true : false;
                CmbSexo.Enabled = Clases.Auth.update == "S" ? true : false;
                TxtConfirmar.Enabled = true;
            }
            else
            {
                a.Advertencia("ERROR AL RECUPERAR LOS DATOS DEL REGISTRO SELECCIONADO");

            }
            cliente.Dispose();
        }
    }
}
