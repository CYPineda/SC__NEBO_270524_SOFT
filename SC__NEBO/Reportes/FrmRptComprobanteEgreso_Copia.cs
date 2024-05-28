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
    public partial class FrmRptComprobanteEgreso_Copia : Form
    {
        Clases.DB db = new Clases.DB();
        public string codPrestamo;
        public FrmRptComprobanteEgreso_Copia()
        {
            InitializeComponent();
        }

        private void FrmRptComprobanteEgreso_Copia_Load(object sender, EventArgs e)
        {
            Reportes.CR_ComprobanteEgresoPrestamo_Copia egresoCopia = new Reportes.CR_ComprobanteEgresoPrestamo_Copia();
            db.Print(egresoCopia);
            egresoCopia.SetParameterValue("@cod_prestamo", codPrestamo);
            CrvComprobanteEgreso_Copia.ReportSource = egresoCopia;
        }
    }
}
