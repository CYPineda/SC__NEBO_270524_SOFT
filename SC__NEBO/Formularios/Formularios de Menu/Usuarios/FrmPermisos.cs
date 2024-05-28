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
    public partial class FrmPermisos : Form
    {

        Clases.DB db = new Clases.DB();
        Clases.Asistente a = new Clases.Asistente();
        Clases.Auth auth = new Clases.Auth();

        public string username, realname, role;


        private void GetUsuarios()
        {
            string campos = "USUARIO, NOMBRE, ROL";
            string condicion = "USUARIO<>'Admin'AND DEL='N'";

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

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            string msg = "¿DESEA GUARDAR LOS CAMBIOS?";
            if (a.Pregunta(msg) == true)
            {
                int i;
                string _idmodulo, _status, _guardar, _eliminar, _editar, _imprimir, _reimprimir, valoracceso, condicion;
                string guardar, eliminar, editar, imprimir, reimprimir;
                for (i = 0; i < DgvData.Rows.Count; i++)
                {
                    _idmodulo = DgvData.Rows[i].Cells[0].Value.ToString();
                    _status = DgvData.Rows[i].Cells[2].Value.ToString();
                    _guardar = DgvData.Rows[i].Cells[3].Value.ToString();
                    _editar = DgvData.Rows[i].Cells[4].Value.ToString();
                    _eliminar = DgvData.Rows[i].Cells[5].Value.ToString();
                    _imprimir = DgvData.Rows[i].Cells[6].Value.ToString();
                    _reimprimir = DgvData.Rows[i].Cells[7].Value.ToString();

                    valoracceso = _status == "True" ? "S" : "N";
                    guardar = _guardar == "True" ? "S" : "N";
                    editar = _editar == "True" ? "S" : "N";
                    eliminar = _eliminar == "True" ? "S" : "N";
                    imprimir = _imprimir == "True" ? "S" : "N";
                    reimprimir = _reimprimir == "True" ? "S" : "N";


                    condicion = "USUARIO= '" + TxtUsuario.Text.Trim() + "' AND IDMOD= '" + _idmodulo + "'";
                    //db.Update("PERMISOSUSUARIO", "ACCESO='" + valoracceso + "'", condicion);
                    db.Update("PERMISOSUSUARIO", "ACCESO = '" + valoracceso + "', GUARDAR = '" + guardar + "', EDITAR = '" + editar + "', ELIMINAR = '" + eliminar + "', IMPRIMIR = '" + imprimir + "', REIMPRIMIR = '" + reimprimir + "'", condicion);
                }

                a.Aprueba("LOS CAMBIOS SE APLICARON CORRECTAMENTE");
                GetPermisos();
            }
        }

        private void btnRegresar_Click(object sender, EventArgs e)
        {
            Formularios.Formularios_de_Menu.Usuarios.FrmUsuarios frmUsuarios = new Formularios.Formularios_de_Menu.Usuarios.FrmUsuarios();
            frmUsuarios.Show();
            this.Close();
        }

        public FrmPermisos()
        {
            InitializeComponent();
        }

        private void pbSalir_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void FrmPermisos_Load(object sender, EventArgs e)
        {
            TxtUsuario.Text = username;
            TxtNombre.Text = realname;
            TxtRol.Text = role;
            GetPermisos();
        }

        private void GetPermisos()
        {
            string q = "A.IDMOD, B.MODULO, A.ACCESO, A.GUARDAR, A.EDITAR, A.ELIMINAR, A.IMPRIMIR, A.REIMPRIMIR FROM PERMISOSUSUARIO A INNER JOIN MODULOSSISTEMA B " +
                "ON(A.IDMOD = B.IDMOD)";

            string condicion = "A.USUARIO = '" + TxtUsuario.Text.Trim() + "'";

            DataTable data = db.Join(q, condicion, "A.IDMOD");

            DgvData.Rows.Clear();

            string _idmod, _modulo, _acceso, _guardar, _editar, _eliminar, _imprimir, _reimprimir;
            bool status = false;
            bool guardar = false;
            bool editar = false;
            bool eliminar = false;
            bool imprimir = false;
            bool reimprimir = false;


            int i;
            for (i = 0; i < data.Rows.Count; i++)
            {
                _idmod = data.Rows[i][0].ToString();
                _modulo = data.Rows[i][1].ToString();
                _acceso = data.Rows[i][2].ToString();
                _guardar = data.Rows[i][3].ToString();
                _editar = data.Rows[i][4].ToString();
                _eliminar = data.Rows[i][5].ToString();
                _imprimir = data.Rows[i][6].ToString();
                _reimprimir = data.Rows[i][7].ToString();
                //Validar las palomitas de permisos de usuarios
                status = _acceso == "S" ? true : false;
                guardar = _guardar == "S" ? true : false;
                editar = _editar == "S" ? true : false;
                eliminar = _eliminar == "S" ? true : false;
                imprimir = _imprimir == "S" ? true : false;
                reimprimir = _reimprimir == "S" ? true : false;


                DgvData.Rows.Add(_idmod, _modulo, status, guardar, editar, eliminar, imprimir, reimprimir);
            }

        }
    }
}
