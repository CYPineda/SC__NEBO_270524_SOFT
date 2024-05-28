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
    public partial class FrmRptComprobanteEgreso : Form
    {
        Clases.DB db = new Clases.DB();
        public string codprestamo;

        public FrmRptComprobanteEgreso()
        {
            InitializeComponent();
        }

        private void FrmRptComprobanteEgreso_Load(object sender, EventArgs e)
        {
            Reportes.CR_ComprobanteEgresoPrestamo egreso = new Reportes.CR_ComprobanteEgresoPrestamo();
            db.Print(egreso);
            egreso.SetParameterValue("@cod_prestamo", codprestamo);
            CrvComprobanteEgreso.ReportSource = egreso;
        }
    }
}
