using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Security.Cryptography;

namespace SC__NEBO.Clases
{
    class Auth
    {
        Clases.DB db = new DB();
        Clases.Asistente a = new Asistente();

        //Datos del usuario logueado
        public static string user;
        public static string realname;
        public static string rol;

        //Estos seran los permisos del usuario
        public static string save, update, delete, print, reprint;

        //Ciframos la clave del Usuario
        public string MakeHash(string password)
        {
            string hash = "";

            //Encaegado de crear un cifrado seguro
            SHA512 sha512 = new SHA512CryptoServiceProvider();
            byte[] inputbytes = (new UnicodeEncoding()).GetBytes(password);
            byte[] result = sha512.ComputeHash(inputbytes);
            hash = Convert.ToBase64String(result);
            return hash;
        }

        //Seeder - Registrar SuperUser
        public void RegisterSuperUser()
        {
            if (db.VerifyIfExists("USUARIOS", "USUARIO", "Admin") == false)
            {
                string campos, valores, hash;
                hash = MakeHash("123456");
                campos = "USUARIO, CLAVE, NOMBRE, CORREO, FECHANAC, SEXO, ROL";
                valores = "'Admin','" + hash + "','CINDY PINEDA','cindypineda262@gmail.com'," +
                    "'05/12/2001','MUJER','ADMINISTRADOR'";

                if (db.Save("USUARIOS", campos, valores) > 0)
                {
                    SetPermisosAdmin();
                }


            }

        }


        private void SetPermisosAdmin()
        {
            string campos = "USUARIO, GUARDAR, ACTUALIZAR, ELIMINAR, IMPRIMIR, REIMPRIMIR";

            string valores = "'Admin', 'S', 'S', 'S', 'S', 'S'";

            db.Save("PERMISOS", campos, valores);
        }


        //Metodo que verifica si el usuario puede iniciar sesion
        public bool MakeLogin(string username, string password)
        {
            bool response = false;
            string condition = "USUARIO='" + username + "'";
            DataTable Data = db.Find("USUARIOS", "*", condition);
            if (Data.Rows.Count > 0)
            {
                DataRow info = Data.Rows[0];
                string usuario = info["USUARIO"].ToString();
                string clave = info["CLAVE"].ToString();
                string estado = info["ESTADO"].ToString();

                if (username == usuario && password == clave && estado == "ACTIVO")
                {
                    user = username;
                    realname = info["NOMBRE"].ToString();
                    rol = info["ROL"].ToString();

                    GetPermisosUser(user);

                    response = true;
                }
                else
                {
                    a.Advertencia("EL NOMBRE DE USUARIO Y/O LA CLAVE QUE INGRESÓ PARECEN SER INCORRECTAS.\n\nREVISE!");
                }
            }
            else
            {
                a.Advertencia("EL NOMBRE DE USUARIO QUE INGRESÓ NO EXISTE!.");
            }

            Data.Dispose();
            return response;

        }


        private void GetPermisosUser(string user)
        {
            string condicion = "USUARIO = '" + user + "'";
            DataTable data = db.Find("PERMISOSUSUARIO", "*", condicion);

            if (data.Rows.Count > 0)
            {
                DataRow info = data.Rows[0];
                save = info["GUARDAR"].ToString();
                update = info["EDITAR"].ToString(); //CAMBIE EN LA BASE DE DATOS POR ACTUALIZAR POR EDITAR
                delete = info["ELIMINAR"].ToString();
                print = info["IMPRIMIR"].ToString();
                reprint = info["REIMPRIMIR"].ToString();
            }
            else
            {
                a.Advertencia("ERROR AL RECUPERAR LOS PERMISOS DEL USUARIO " + user);
            }
        }
    }
}
