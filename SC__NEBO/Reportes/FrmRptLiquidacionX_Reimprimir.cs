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
    public partial class FrmRptLiquidacionX_Reimprimir : Form
    {
        Clases.DB db = new Clases.DB();
        public string numliqui;

        public FrmRptLiquidacionX_Reimprimir()
        {
            InitializeComponent();
        }

        private void FrmRptLiquidacionX_Reimprimir_Load(object sender, EventArgs e)
        {
            Reportes.CRInfoLiquidacion_Remprmir fichaliquidacionReimprimir = new Reportes.CRInfoLiquidacion_Remprmir();
            db.Print(fichaliquidacionReimprimir);
            fichaliquidacionReimprimir.SetParameterValue("@numliqui", numliqui);
            CrvReporteLiquidacionReimprimir.ReportSource = fichaliquidacionReimprimir;
        }
    }
}
