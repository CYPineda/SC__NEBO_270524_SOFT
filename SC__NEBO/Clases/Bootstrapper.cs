using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;
using System.IO; //Esta libreria permite gestionar archivos (crear archivos, verificar si existen, etc.)

namespace SC__NEBO.Clases
{
    class Bootstrapper
    {
        Clases.Asistente a = new Clases.Asistente();

        public void CheckFileConfiguration()
        {
            string path = @"C:\DataApp\Configuration.accdb";

            if (File.Exists(path))
            {
                ReadConfiguration();
            }
            else
            {
                a.Advertencia("ERROR AL CARGAR EL ARCHIVO DE CONFIGURACION", "ERROR");

                Application.Exit();
            }
        }

        private void ReadConfiguration()
        {
            string cadena_conexion = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\DataApp\Configuration.accdb;Jet OLEDB:Database Password=957RJ381;";
            //string cadena_conexion = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\DataApp\Configuration.accdb;Jet OLEDB:Database Password=832951DC7;";

            OleDbConnection ConAccess = new OleDbConnection(cadena_conexion);

            try
            {
                string query = "SELECT * FROM INFOSERVER";
                OleDbCommand com = new OleDbCommand(query, ConAccess);
                ConAccess.Open();
                OleDbDataReader reader;

                reader = com.ExecuteReader();

                if (reader.Read())
                {
                    Clases.Env.SERVER = reader["SERVER"].ToString();
                    Clases.Env.USER = reader["USUARIO"].ToString();
                    Clases.Env.PASS = reader["PASSWORD"].ToString();
                    Clases.Env.DB = reader["DATABASE"].ToString();

                    Clases.Conexion.SetConfigurationServer();

                }
                else
                {
                    a.Advertencia("ERROR AL CARGAR LOS DATOS DE CONFIGURACION", "ERROR");
                    Application.Exit();
                }

                //Liberacion del recursos
                reader.Close();
                com.Dispose();
                ConAccess.Close();
            }
            catch (OleDbException error)
            {
                a.Advertencia(error.Message);
            }
        }
    }
}
