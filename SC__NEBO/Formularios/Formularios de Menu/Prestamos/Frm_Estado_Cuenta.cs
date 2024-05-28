using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SC__NEBO.Formularios.Formularios_de_Menu.Prestamos
{
    public partial class Frm_Estado_Cuenta : Form
    {
        Clases.DB db = new Clases.DB();
        Clases.Asistente a = new Clases.Asistente();

        public string no_prestamo, cod_socio, nombre_socio;

        public Frm_Estado_Cuenta()
        {
            InitializeComponent();
        }

        private void Frm_Estado_Cuenta_Load(object sender, EventArgs e)
        {
            txtCodSocio.Text = cod_socio;
            txtNombSocio.Text = nombre_socio;

            a.Advertencia(cod_socio);
            Get_Prestamos();
        }

        private void Get_Prestamos()
        {
            // Consulta SQL para obtener los datos de préstamos y pagos
            string query = @"SELECT 
    FECHA_INICIO_PRESTAMO,
    CODIGO,
    CONCEPTO,
    ENTREGADO_PRESTAMO,
    ABONO_PAGO,
    INTERES_PRESTAMO
FROM (
    SELECT 
        A.FECHA_INICIO AS FECHA_INICIO_PRESTAMO, 
        A.PRESTAMO_CODIGO AS CODIGO, 
        NULL AS CONCEPTO,
        A.CANTIDAD_LPS AS ENTREGADO_PRESTAMO,
        NULL AS ABONO_PAGO,
        A.INTERES AS INTERES_PRESTAMO,
        'PRESTAMO' AS TIPO, -- Agregar una columna para indicar el tipo (préstamo o pago)
        1 AS ORDEN
    FROM 
        PRESTAMOS A
    WHERE 
        A.ID_SOCIO = '"+cod_socio+"'"+
    @"UNION ALL
    SELECT
        B.FECHA AS FECHA_INICIO_PRESTAMO,
        B.ID_PAGO AS CODIGO,
        'PAGO' AS CONCEPTO,
        NULL AS ENTREGADO_PRESTAMO,
        B.MONTO_PAGO AS ABONO_PAGO,
        B.INTERES AS INTERES_PRESTAMO,
        'PAGO' AS TIPO,
        2 AS ORDEN
    FROM
        PAGOS B
    INNER JOIN
        PRESTAMOS A ON A.PRESTAMO_CODIGO = B.PRESTAMO_CODIGO
    WHERE
        A.ID_SOCIO = '"+cod_socio+"'"+
@") AS result
ORDER BY
	FECHA_INICIO_PRESTAMO,
    CODIGO, -- Ordenar por el código del préstamo o pago
	CONCEPTO,
    CASE WHEN TIPO = 'PAGO' THEN 0 ELSE 1 END, -- Poner los préstamos antes que los pagos
    ORDEN;"; // Primero por fecha y luego por código de préstamo

            // Ejecutar la consulta y obtener los resultados en un DataTable
            DataTable data = db.RawSQL(query);

            // Limpiar el DataGridView antes de agregar nuevos datos
            DgvData.Rows.Clear();

            // Iterar sobre los resultados y agregarlos al DataGridView
            for (int i = 0; i < data.Rows.Count; i++)
            {
                string fecha = Convert.ToDateTime(data.Rows[i][0]).ToShortDateString();
                string codigo = data.Rows[i][1].ToString();
                string entregado = data.Rows[i][3].ToString(); // Cantidad entregada en el préstamo
                string abono = data.Rows[i][4].ToString(); // Monto del pago
                string interes = data.Rows[i][5].ToString();

                // Determinar el concepto (Pago o Préstamo)
                //string concepto = string.Empty;

                //// Verificar si hay fecha de pago para determinar si es un pago o un préstamo
                //if (data.Rows[i][3] != DBNull.Value)
                //{
                //    // Si hay fecha de pago, significa que es un pago
                //    concepto = "PRESTAMO";
                //    interes = data.Rows[i][5].ToString(); // Interés del pago
                //}
                //else
                //{
                //    // Si no hay fecha de pago, significa que es un préstamo
                //    concepto = "PAGO";
                //    interes = data.Rows[i][5].ToString(); // Interés del préstamo
                //}
                string concepto = (data.Rows[i][2] != DBNull.Value) ? "PAGO" : "PRESTAMO";

                // Agregar la fila al DataGridView
                DgvData.Rows.Add(fecha, codigo, concepto, entregado, abono, interes);
            }



        }
    }
}
