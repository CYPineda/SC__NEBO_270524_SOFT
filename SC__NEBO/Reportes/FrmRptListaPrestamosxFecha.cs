using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SC__NEBO.Reportes
{
    public partial class FrmRptListaPrestamosxFecha : Form
    {
        Clases.DB db = new Clases.DB();
        Clases.Asistente a = new Clases.Asistente();
        public string fechaini, fechafin, estadop, nombre;
        public FrmRptListaPrestamosxFecha()
        {
            InitializeComponent();
        }

        private void pbSalir_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void FrmRptListaPrestamosxFecha_Load(object sender, EventArgs e)
        {
            Reportes.CRListaPrestamosxFecha listap = new CRListaPrestamosxFecha();
            db.Print(listap);
            listap.SetParameterValue("@NOMBRE", nombre);
            listap.SetParameterValue("@FECHAINI", fechaini);
            listap.SetParameterValue("@FECHALIMIT", fechafin);
            listap.SetParameterValue("@ESTADOP", estadop);
            
            CrvListaPrestamosxFecha.ReportSource = listap;
        }
    }
}
