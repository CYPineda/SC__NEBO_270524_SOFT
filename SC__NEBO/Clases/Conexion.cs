using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace SC__NEBO.Clases
{
    class Conexion
    {

        Clases.Asistente h = new Clases.Asistente();
        static string cadena_de_conexion;
        public static SqlConnection Con;


        public static void SetConfigurationServer()
        {
            cadena_de_conexion = @"Server=" + Env.SERVER + ";Database=" + Env.DB + ";User Id=" + Env.USER + ";Password=" + Env.PASS + ";";
            Con = new SqlConnection(cadena_de_conexion);
            
        }

        
        public void OpenConnection()
        {
            try
            {
                Con.Open();
                
            }
            catch (SqlException error)
            {
                h.Advertencia(error.Message);
            }
        }

        public void CloseConnection()
        {
            try
            {
                Con.Close();

            }
            catch (SqlException error)
            {
                h.Advertencia(error.Message);
            }
        }

        public void EndsConnection()
        {
            if (Con.State == ConnectionState.Open
)
            {
                Con.Close();
            }
        }
    }
}
