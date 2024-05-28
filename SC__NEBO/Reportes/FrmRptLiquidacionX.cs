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
    public partial class FrmRptLiquidacionX : Form
    {
        Clases.DB db = new Clases.DB();

        public FrmRptLiquidacionX()
        {
            InitializeComponent();
        }
        public string numliqui;

        private void FrmRptLiquidacionX_Load(object sender, EventArgs e)
        {
            Reportes.CRInfoLiquidacion_x fichaLiquidacion = new Reportes.CRInfoLiquidacion_x();
            db.Print(fichaLiquidacion);
            fichaLiquidacion.SetParameterValue("@numliqui", numliqui);
            CrvReporteLiquidacion.ReportSource = fichaLiquidacion;
        }
    }
}
