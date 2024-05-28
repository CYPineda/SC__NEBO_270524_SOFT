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
    public partial class FrmRptComprobanteIngresoLiquidacion : Form
    {
        Clases.DB db = new Clases.DB();
        public FrmRptComprobanteIngresoLiquidacion()
        {
            InitializeComponent();
        }
        public string numliqui;

        private void FrmRptComprobanteIngresoLiquidacion_Load(object sender, EventArgs e)
        {
            Reportes.CRComprobanteIngresoLiquidacion ficha = new Reportes.CRComprobanteIngresoLiquidacion();
            db.Print(ficha);
            ficha.SetParameterValue("@numliqui", numliqui);
            CRVComprobanteIngresoLiquidacion.ReportSource = ficha;
        }
    }
}
