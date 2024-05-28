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
    public partial class FrmRptLiquidacionX_Copia : Form
    {
        public FrmRptLiquidacionX_Copia()
        {
            InitializeComponent();
        }

        Clases.DB db = new Clases.DB();

        public string @numliqui;
        private void FrmRptLiquidacionX_Copia_Load(object sender, EventArgs e)
        {
            Reportes.CRInfoLiquidacion_x_Copia fichaLiquidacion = new Reportes.CRInfoLiquidacion_x_Copia();
            db.Print(fichaLiquidacion);
            fichaLiquidacion.SetParameterValue("@numliqui", numliqui);
            CrvReporteLiquidacion.ReportSource = fichaLiquidacion;
        }
    }
}
