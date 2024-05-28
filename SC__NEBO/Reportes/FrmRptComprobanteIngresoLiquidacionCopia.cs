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
    public partial class FrmRptComprobanteIngresoLiquidacionCopia : Form
    {
        Clases.DB db = new Clases.DB();
        public string codrecibo;

        public FrmRptComprobanteIngresoLiquidacionCopia()
        {
            InitializeComponent();
        }

        private void FrmRptComprobanteIngresoLiquidacionCopia_Load(object sender, EventArgs e)
        {
            Reportes.CR_ComprobanteIngresoLiquidacionCopia CompIngresoCopia = new Reportes.CR_ComprobanteIngresoLiquidacionCopia();
            db.Print(CompIngresoCopia);
            CompIngresoCopia.SetParameterValue("@numliqui", codrecibo);
            CrvComprobanteIngresoCopia.ReportSource = CompIngresoCopia;
        }
    }
}
