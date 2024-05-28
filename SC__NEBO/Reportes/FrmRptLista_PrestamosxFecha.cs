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
    public partial class FrmRptLista_PrestamosxFecha : Form
    {
        Clases.DB db = new Clases.DB();

        public string fechaini, fechafin, estadop, nombre;

        public FrmRptLista_PrestamosxFecha()
        {
            InitializeComponent();
        }

        private void FrmRptLista_PrestamosxFecha_Load(object sender, EventArgs e)
        {
            Reportes.CRListaPrestamosxFecha presxF = new CRListaPrestamosxFecha();
            db.Print(presxF);
            presxF.SetParameterValue("@FECHAINI", fechaini);
            presxF.SetParameterValue("@FECHALIMIT", fechafin);
            presxF.SetParameterValue("@ESTADOP", estadop);
            presxF.SetParameterValue("@NOMBRE", nombre);
            CrvPrestamoxFecha.ReportSource = presxF;
        }
    }
}
