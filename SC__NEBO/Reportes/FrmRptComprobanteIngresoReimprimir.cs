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
    public partial class FrmRptComprobanteIngresoReimprimir : Form
    {
        Clases.DB db = new Clases.DB();
        public string codrecibo;

        public FrmRptComprobanteIngresoReimprimir()
        {
            InitializeComponent();
        }

        private void FrmRptComprobanteIngresoReimprimir_Load(object sender, EventArgs e)
        {
            Reportes.CR_ComprobanteIngresoLiquidacionRemp CompIngresoRemp = new Reportes.CR_ComprobanteIngresoLiquidacionRemp();
            db.Print(CompIngresoRemp);
            CompIngresoRemp.SetParameterValue("@numliqui", codrecibo);
            CrvComprobanteIngresoRemprimir.ReportSource = CompIngresoRemp;
        }
    }
}
