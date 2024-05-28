using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using CrystalDecisions.Shared;
using CrystalDecisions.CrystalReports.Engine;

namespace SC__NEBO.Clases
{
    class DB : Conexion
    {
        Clases.Asistente a = new Clases.Asistente();

        string query;
        SqlCommand com;
        SqlDataReader reader;
        DataTable recordset;

        
        //Ejecuta una consulta sql Cruda
        public Int32 RawSQL(string Query, bool debug = false)
        {
            Int32 ra = 0;

            if (debug == true)
            {
                a.Aprueba(Query);
            }

            try
            {
                com = new SqlCommand(Query, Con);
                OpenConnection();
                ra = com.ExecuteNonQuery();

                com.Dispose();
                CloseConnection();
            }
            catch (SqlException error)
            {
                a.Advertencia(error.Message);
            }
            finally
            {
                EndsConnection();
            }
            return ra;
        }

        public DataTable RawSQL(string query)
        {
            recordset = new DataTable();
            try
            {
                com = new SqlCommand(query, Con);
                OpenConnection();
                reader = com.ExecuteReader();
                recordset.Load(reader);

                reader.Close();
                com.Dispose();
                CloseConnection();
            }
            catch (SqlException e)
            {
                a.Advertencia(e.Message);
            }

            finally
            {
                EndsConnection();
            }
            return recordset;
        }

        public void Print(ReportDocument reporte)
        {
            try
            {
                ConnectionInfo Con = new ConnectionInfo();
                Tables _tables = reporte.Database.Tables;

                Con.ServerName = Clases.Env.SERVER;
                Con.DatabaseName = Clases.Env.DB;
                Con.UserID = Clases.Env.USER;
                Con.Password = Clases.Env.PASS;

                foreach (CrystalDecisions.CrystalReports.Engine.Table _table in _tables)
                {
                    TableLogOnInfo loginfo = _table.LogOnInfo;
                    loginfo.ConnectionInfo = Con;
                    _table.ApplyLogOnInfo(loginfo);
                }
            }
            catch (CrystalReportsException error)
            {
                a.Advertencia(error.Message);
            }
            finally
            {

            }
        }        //Metodo Sum, utilizado para traer la suma de un campo
        //public double Sum(string Campo, string tabla, string Condicion)
        //{
        //    double result = 0;
        //    try
        //    {
        //        string Query = "SELECT SUM(" + Campo + ") AS Result FROM " + tabla + " " + Condicion;
        //        com = new SqlCommand(Query, Con);
        //        SqlDataReader Recordset;
        //        OpenConnection();
        //        Recordset = com.ExecuteReader();

        //        if (Recordset.Read())
        //        {
        //            result = h.ReturnsNumber(Recordset["Result"].ToString());
        //        }
        //        else
        //        {
        //            a.Advertencia("ERROR AL PROCESAR LOS DATOS!");
        //        }

        //        Recordset.Close();
        //        com.Dispose();
        //        CloseConnection();
        //    }
        //    catch (SqlException error)
        //    {
        //        h.Warning(error.Message);
        //    }
        //    finally
        //    {
        //        EndsConnection();
        //    }
        //    return result;

        //}
        //fin metodo Sum


        //Clase para calcular años contables
        public int Days360(DateTime fechaInicio, DateTime fechaFin)
        {
            int diaInicio = fechaInicio.Day;
            int mesInicio = fechaInicio.Month;
            int anioInicio = fechaInicio.Year;
            int diaFin = fechaFin.Day;
            int mesFin = fechaFin.Month;
            int anioFin = fechaFin.Year;

            // Si el día final es el último día del mes, se cambia al día 30
            if (diaFin == DateTime.DaysInMonth(anioFin, mesFin))
            {
                diaFin = 30;
            }

            // Si el día inicial es el último día del mes, se cambia al día 30
            if (diaInicio == DateTime.DaysInMonth(anioInicio, mesInicio))
            {
                diaInicio = 30;
            }

            // Si la fecha final es el 31 de diciembre y el día inicial es mayor a 30, se cambia al día 30
            if (diaInicio > 30 && mesInicio == 12 && diaFin == 31 && mesFin == 12)
            {
                diaInicio = 30;
            }

            int dias = 360 * (anioFin - anioInicio) + 30 * (mesFin - mesInicio) + (diaFin - diaInicio);

            return dias;
        }
        //Fin Clase para calcular años contables


        //public void Llenar(TextBox txtCosecha)
        //{
        //    string query = "SELECT TOP 1 ID_COSECHA FROM COSECHAS ORDER BY ID_COSECHA DESC";

        //    try
        //    {
        //        using (this)
        //        using (SqlCommand command = new SqlCommand(query, this)) 
        //        {
        //            this.OpenConnection();
        //            object result = command.ExecuteScalar();
        //            txtCosecha.Text = result != null ? result.ToString() : "No se encontraron registros";
        //        }

        //    }
        //    catch (SqlException error)
        //    {
        //        a.Advertencia(error.Message);
        //    }
        //    finally
        //    {
        //        EndsConnection();
        //    }

        //}

        //public void Llenar(TextBox txtCosecha, SqlConnection connection)
        //{
        //    string query = "SELECT TOP 1 ID_COSECHA FROM COSECHAS ORDER BY ID_COSECHA DESC";

        //    try
        //    {
        //        using (SqlCommand command = new SqlCommand(query, connection))
        //        {
        //            connection.Open();
        //            object result = command.ExecuteScalar();
        //            txtCosecha.Text = result != null ? result.ToString() : "No se encontraron registros";
        //        }
        //    }
        //    catch (SqlException error)
        //    {
        //        a.Advertencia(error.Message);
        //    }
        //    finally
        //    {
        //        connection.Close();
        //    }
        //}



        //Verifica si un registro ya existe en la tabla x
        public bool CheckIfExists(string Tabla, string condition, bool debug = false)
        {
            bool Response = false;
            query = "SELECT * FROM " + Tabla + " WHERE " + condition;

            if (debug == true)
            {
                a.Advertencia(query);
            }

            try
            {
                com = new SqlCommand(query, Con);
                OpenConnection();
                reader = com.ExecuteReader();

                if (reader.Read())
                {
                    Response = true;
                }

                reader.Close();
                com.Dispose();
                CloseConnection();
            }
            catch (SqlException error)
            {
                a.Advertencia(error.Message);
            }
            finally
            {
                EndsConnection();
            }
            return Response;
        }

        //Verifica si un codigo ya existe
        public bool VerifyIfExists(string Tabla, string Pk, string Id)
        {
            bool Response = false;
            try
            {
                string Query = "SELECT * FROM " + Tabla + " WHERE " + Pk + "='" + Id + "'";

                com = new SqlCommand(Query, Con);
                SqlDataReader TableReader;
                OpenConnection();
                TableReader = com.ExecuteReader();

                if (TableReader.Read())
                {
                    Response = true;
                }

                TableReader.Close();
                com.Dispose();
                CloseConnection();
            }
            catch (SqlException error)
            {
                a.Advertencia(error.Message);
            }
            finally
            {
                EndsConnection();
            }
            return Response;
        }

        //Metodo Hook, utilizado para traer un solo campo de la base de datos
        public string Hook(string Campo, string Tabla, string Condicion, bool debug = false)
        {
            string Fish = "";

            query = "SELECT " + Campo + " AS FISH FROM " + Tabla + " WHERE " + Condicion;

            if (debug == true)
            {
                a.Aprueba(query);
            }

            try
            {
                com = new SqlCommand(query, Con);
                SqlDataReader Recordset;
                OpenConnection();
                Recordset = com.ExecuteReader();
                if (Recordset.Read())
                {
                    Fish = Recordset["FISH"].ToString();
                }

                Recordset.Close();
                com.Dispose();
                CloseConnection();

            }
            catch (SqlException error)
            {
                a.Advertencia(error.Message);

            }
            finally
            {
                EndsConnection();
            }
            return Fish;
        }
        //fin metodo hook

        //Metodo GetNextId, utilizado para generar ids numericos
        public string GetNextId(string idmodule)
        {
            string id = "";
            string correlativo = "";
            string condition;

            condition = "IDCORRE = '" + idmodule + "'";

            correlativo = Hook("ULTIMO + 1", "CORRELATIVOS", condition);

            double response = a.ReturnsNumber(correlativo);
            if (response > 0 && response <= 9)
            {
                id = "0000000" + response.ToString();
            }
            else if (response >= 10 && response <= 99)
            {
                id = "000000" + response.ToString();
            }
            else if (response >= 100 && response <= 999)
            {
                id = "00000" + response.ToString();
            }
            else if (response >= 1000 && response <= 9999)
            {
                id = "0000" + response.ToString();
            }
            else if (response >= 10000 && response <= 99999)
            {
                id = "000" + response.ToString();
            }
            else if (response >= 100000 && response <= 999999)
            {
                id = "00" + response.ToString();
            }
            else if (response >= 1000000 && response <= 999999999)
            {
                id = "0" + response.ToString();
            }
            else
            {
                id = response.ToString();
            }

            return id;
        }
        //fin GetNextId

        //Metodo GetNext, Utilizado para Generar Correlativos
        public string GetNext(string Id)
        {
            string Next = "";
            string condition;
            condition = "WHERE IDCORRE ='" + Id + "'";


            string Query = "SELECT ULTIMO + 1 AS NEXT FROM CORRELATIVOS " + condition;
            try
            {
                com = new SqlCommand(Query, Con);
                OpenConnection();
                SqlDataReader DrNext;
                DrNext = com.ExecuteReader();

                Int32 Corre = 0;
                if (DrNext.Read())
                {
                    Corre = Convert.ToInt32(DrNext["NEXT"].ToString());
                }
                else
                {
                    a.Advertencia("ERROR AL GENERAR EL CORRELATIVO, CONTACTAR AL ADMINISTRADOR");
                }

                DrNext.Close();
                com.Dispose();
                CloseConnection();

                if (Corre >= 1 && Corre <= 9)
                {
                    Next = "00000" + Corre.ToString();
                }
                else if (Corre >= 10 && Corre <= 99)
                {
                    Next = "0000" + Corre.ToString();
                }
                else if (Corre >= 100 && Corre <= 999)
                {
                    Next = "000" + Corre.ToString();
                }
                else if (Corre >= 1000 && Corre <= 9999)
                {
                    Next = "00" + Corre.ToString();
                }
                else if (Corre >= 10000 && Corre <= 99999)
                {
                    Next = "0" + Corre.ToString();
                }
                else if (Corre >= 100000 && Corre <= 999999)
                {
                    Next = Corre.ToString();
                }
                else
                {
                    a.Advertencia("ERROR AL GENERAR EL CORRELATIVO, HA LLEGADO AL MÁXIMO!");
                }
            }
            catch (SqlException error)
            {
                a.Advertencia(error.Message);
            }
            finally
            {
                EndsConnection();
            }

            return Next;
        }
        //Fin Metodo GetNext



        //Metodo SetLast, Utilizado para Establecer el ultimo Correlativo Asignado
        public void SetLast(string Id)
        {
            string condition;
            condition = "WHERE IDCORRE='" + Id + "'";


            string Query = "Update Correlativos set Ultimo = Ultimo + 1 where IdCorre='" + Id + "'";

            try
            {
                com = new SqlCommand(Query, Con);
                OpenConnection();
                com.ExecuteNonQuery();

                com.Dispose();
                CloseConnection();
            }
            catch (SqlException error)
            {
                a.Advertencia(error.Message);
            }
            finally
            {
                EndsConnection();
            }
        }
        //Fin Metodo SetLast;

        //Ejecuta el procedimiento almacenado encargado de crear el backup
        public void MakeBackUp(string msg = "")
        {
            try
            {
                com = new SqlCommand("MakeBackUp", Con);
                com.CommandType = CommandType.StoredProcedure;
                OpenConnection();
                com.ExecuteNonQuery();
                com.Dispose();
                CloseConnection();

                if (msg != "")
                {
                    a.Aprueba(msg);
                }

            }
            catch (SqlException error)
            {
                a.Advertencia(error.Message);
            }
            finally
            {
                EndsConnection();
            }
        }

        //Metodo Save
        //inserta registros en la base de datos
        public int Save(string table, string fields, string values, bool debug = false)
        {
            int ra = 0; //rowsaffected
            query = "INSERT INTO " + table + "(" + fields + ") VALUES(" + values + ")";

            if (debug == true)
            {
                a.Aprueba(query);
            }

            try
            {
                com = new SqlCommand(query, Con);
                OpenConnection();

                ra = com.ExecuteNonQuery();

                com.Dispose();
                CloseConnection();

            }
            catch (SqlException error)
            {
                a.Advertencia(error.Message);
            }
            finally
            {
                EndsConnection();
            }

            return ra;
        }
        //fin Metodo Save

        //Metodo Update
        //Actualizar los registros de la base de datos
        public int Update(string table, string values, string condition = "", bool debug = false)
        {
            int ra = 0; //rowsaffected
            if (condition == "")
            {
                query = "UPDATE " + table + " SET " + values;
            }
            else
            {
                query = "UPDATE " + table + " SET " + values + " WHERE " + condition;
            }

            if (debug == true)
            {
                a.Aprueba(query);
            }

            try
            {
                com = new SqlCommand(query, Con);
                OpenConnection();

                ra = com.ExecuteNonQuery();

                com.Dispose();
                CloseConnection();
            }
            catch (SqlException error)
            {
               a.Advertencia(error.Message);
            }
            finally
            {
                EndsConnection();
            }
            return ra;
        }
        //Actualiza registros existentes
        //Fin Metodo Update

        //Metodo Destroy
        //Elimina registros de la base de datos
        public int Destroy(string table, string condition = "")
        {
            int ra = 0;
            if (condition == "")
            {
                query = "DELETE FROM " + table;
            }
            else
            {
                query = "DELETE FROM " + table + " WHERE " + condition;
            }

            try
            {
                com = new SqlCommand(query, Con);
                OpenConnection();

                ra = com.ExecuteNonQuery();

                com.Dispose();
                CloseConnection();

            }
            catch (SqlException error)
            {
                a.Advertencia(error.Message);
            }
            finally
            {
                EndsConnection();
            }
            return ra;
        }
        //Fin Metodo Destroy

        //Metodo Delete
        //Marca como eliminado el registro seleccionado
        public int Delete(string table, string pk, string value)
        {
            int rd = 0;
            try
            {
                query = "UPDATE " + table + " SET DEL='S' WHERE " + pk + "='" + value + "'";
                com = new SqlCommand(query, Con);
                OpenConnection();

                rd = com.ExecuteNonQuery();

                com.Dispose();
                CloseConnection();
            }
            catch (SqlException error)
            {
                a.Advertencia(error.Message);
            }
            finally
            {
                EndsConnection();
            }

            return rd;
        }
        //Fin Delete

        public int Delete_Estado(string table, string pk, string value)
        {
            int rd = 0;
            try
            {
                query = "UPDATE " + table + " SET ESTADO='INACTIVO' WHERE " + pk + "='" + value + "'";
                com = new SqlCommand(query, Con);
                OpenConnection();

                rd = com.ExecuteNonQuery();

                com.Dispose();
                CloseConnection();
            }
            catch (SqlException error)
            {
                a.Advertencia(error.Message);
            }
            finally
            {
                EndsConnection();
            }

            return rd;
        }
        //Metodo Delete
        //Marca como eliminado el registro seleccionado de nota de peso
        public int Delete_Nota_Peso(string table, string pk, string value)
        {
            int rd = 0;
            try
            {
                query = "UPDATE " + table + " SET ESTADO='NULA' WHERE " + pk + "='" + value + "'";
                com = new SqlCommand(query, Con);
                OpenConnection();

                rd = com.ExecuteNonQuery();

                com.Dispose();
                CloseConnection();
            }
            catch (SqlException error)
            {
                a.Advertencia(error.Message);
            }
            finally
            {
                EndsConnection();
            }

            return rd;
        }

        //Marca como eliminado el registro seleccionado de nota de peso
        public int Recuperar_Nota(string table, string pk, string value)
        {
            int rd = 0;
            try
            {
                query = "UPDATE " + table + " SET ESTADO='PENDIENTE' WHERE " + pk + "='" + value + "'";
                com = new SqlCommand(query, Con);
                OpenConnection();

                rd = com.ExecuteNonQuery();

                com.Dispose();
                CloseConnection();
            }
            catch (SqlException error)
            {
                a.Advertencia(error.Message);
            }
            finally
            {
                EndsConnection();
            }

            return rd;
        }


        //Metodo Delete
        //Marca como eliminado el registro seleccionado
        public int Delete2(string table, string condition = "")
        {
            int rd = 0;
            query = "UPDATE " + table + " SET DEL='S' WHERE " + condition;

            try
            {

                com = new SqlCommand(query, Con);
                OpenConnection();

                rd = com.ExecuteNonQuery();

                com.Dispose();
                CloseConnection();
            }
            catch (SqlException error)
            {
                a.Advertencia(error.Message);
            }
            finally
            {
                EndsConnection();
            }

            return rd;
        }
        //Fin Delete


        public DataTable Find(string table, string fields, string condition = "", string orderby = "", bool debug = false)
        {
            recordset = new DataTable();

            if (condition == "" && orderby == "")
            {
                query = "SELECT " + fields + " FROM " + table;
            }
            else if (condition != "" && orderby == "")
            {
                query = "SELECT " + fields + " FROM " + table + " WHERE " + condition;
            }
            else if (condition == "" && orderby != "")
            {
                query = "SELECT " + fields + " FROM " + table + " ORDER BY " + orderby;
            }
            else if (condition != "" && orderby != "")
            {
                query = "SELECT " + fields + " FROM " + table + " WHERE " + condition + " ORDER BY " + orderby;
            }

            if (debug == true)
            {
                a.Advertencia(query);
            }

            try
            {
                com = new SqlCommand(query, Con);

                OpenConnection();
                reader = com.ExecuteReader();
                recordset.Load(reader);

                reader.Close();
                com.Dispose();
                CloseConnection();
            }
            catch (SqlException error)
            {
                a.Advertencia(error.Message);
            }
            finally
            {
                EndsConnection();
            }
            return recordset;
        }

        //Metodo JoinTables
        //Combinar los datos de más tablas
        //fin JoinTables

        public DataTable Join(string data, string condition = "", string orderby = "", bool debug = false)
        {
            recordset = new DataTable();
            if (condition == "" && orderby == "")
            {
                query = "SELECT " + data;
            }
            else if (condition != "" && orderby == "")
            {
                query = "SELECT " + data + " WHERE " + condition;
            }
            else if (condition != "" && orderby != "")
            {
                query = "SELECT " + data + " WHERE " + condition + " ORDER BY " + orderby;
            }
            else if (condition == "" && orderby != "")
            {
                query = "SELECT " + data + " ORDER BY " + orderby;
            }

            if (debug == true)
            {
                a.Advertencia(query);
            }

            try
            {
                com = new SqlCommand(query, Con);
                OpenConnection();
                reader = com.ExecuteReader();
                recordset.Load(reader);

                reader.Close();
                com.Dispose();
                CloseConnection();
            }
            catch (SqlException error)
            {
                a.Advertencia(error.Message);
            }
            finally
            {
                EndsConnection();
            }
            return recordset;
        }
        //Fin JoinTables

        //Metodo Count
        //Determina el numero de filas que cumplen una condicion
        public Int32 Count(string tablename, string condition = "", bool debug = false)
        {
            Int32 NumRows = 0;
            if (condition == "")
            {
                query = "SELECT COUNT(*) AS NUMROWS FROM " + tablename;
            }
            else
            {
                query = "SELECT COUNT(*) AS NUMROWS FROM " + tablename + " WHERE " + condition;
            }

            if (debug == true)
            {
                a.Advertencia(query);
            }

            try
            {
                com = new SqlCommand(query, Con);
                OpenConnection();
                reader = com.ExecuteReader();

                if (reader.Read())
                {
                    NumRows = Convert.ToInt16(reader["NUMROWS"].ToString());
                }
                else
                {
                    a.Advertencia("ERROR FATAL AL PROCESAR EL COMANDO ESPECIFICADO");
                }
                reader.Close();
                com.Dispose();
                CloseConnection();
            }
            catch (SqlException error)
            {
                a.Advertencia(error.Message);
            }
            finally
            {
                EndsConnection();
            }
            return NumRows;
        }
        //Fin GetNumRows


        //Metodo GetLastId
        public Int32 GetLastId(string Table)
        {
            int LastId = 0;
            try
            {
                string Query = "Select IDENT_CURRENT('" + Table + "') as LastId";
                SqlCommand Com = new SqlCommand(Query, Con);
                OpenConnection();
                LastId = Convert.ToInt32(Com.ExecuteScalar());

                Com.Dispose();
                CloseConnection();
            }
            catch (SqlException error)
            {
                a.Advertencia(error.Message);
            }
            finally
            {
                EndsConnection();
            }
            return LastId;
        }
        //Fin Metodo GetLastId


        public Int32 GetAfterId(string Table)
        {
            int AfterId = 0;
            try
            {
                string Query = "Select IDENT_CURRENT('" + Table + "') as AfterId";
                SqlCommand Com = new SqlCommand(Query, Con);
                OpenConnection();
                AfterId = Convert.ToInt32(Com.ExecuteScalar());

                Com.Dispose();
                CloseConnection();
            }
            catch (SqlException error)
            {
                a.Advertencia(error.Message);
            }
            finally
            {
                EndsConnection();
            }
            return AfterId;
        }

    }
}
