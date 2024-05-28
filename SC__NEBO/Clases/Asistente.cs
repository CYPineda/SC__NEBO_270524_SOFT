using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Globalization;

namespace SC__NEBO.Clases
{
    class Asistente
    {
        public void Advertencia(String msg, string title = "ADVERTENCIA")
        {
            MessageBox.Show(msg, title, MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        public void Aprueba(string msg, string title = "")
        {
            MessageBox.Show(msg, title, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public bool Pregunta(string msg)
        {
            bool resp = false;

            if (MessageBox.Show(msg, "CONFIRMACIÓN", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                resp = true;
            }

            return resp;
        }

        public string Clean(string str)
        {
            string strout = ""; //CAdena de Salida

            //Arreglo de Caracteres prohibidos
            string[] forbiddenchars = { "'", "=", "<", ">", ";", "\\", "?", "¡", "¿", "!", "´" };
            int i, j; //Variables para iterar ciclos
            int coinc; //coincidencias

            for (i = 0; i < str.Length; i++)
            {
                coinc = 0;
                for (j = 0; j < forbiddenchars.Length; j++)
                {
                    coinc = str.Substring(i, 1) == forbiddenchars[j] ? coinc + 1 : coinc + 0;
                }
                strout = coinc == 0 ? strout + str.Substring(i, 1) : strout;
            }

            return strout;
        }

        //Resive una cadcena y retorna un valor numerico, sino devuelve 0 
        public double ReturnsNumber(string str)
        {
            double double_value = 0;

            if (double.TryParse(str, out double_value))
            {
                double_value = Convert.ToDouble(str);

            }
            else
            {
                double_value = 0;
            }
            return double_value;
        }

        public Boolean OnlyNumbers(KeyPressEventArgs e)
        {
            Boolean resp = false;
            if (Char.IsNumber(e.KeyChar) || e.KeyChar == (char)8)
            {
                resp = true;
            }
            return resp;
        }

        public Boolean Money(KeyPressEventArgs e)
        {
            Boolean resp = false;
            CultureInfo cc = System.Threading.Thread.CurrentThread.CurrentCulture;
            if (Char.IsNumber(e.KeyChar) || e.KeyChar.ToString() == cc.NumberFormat.NumberDecimalSeparator)
            {
                resp = true;
            }
            return resp;
        }
    }
}
