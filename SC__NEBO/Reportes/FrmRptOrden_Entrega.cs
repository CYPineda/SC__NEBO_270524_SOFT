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
    public partial class FrmRptOrden_Entrega : Form
    {
        Clases.DB db = new Clases.DB();
        public string codentrega;

        public FrmRptOrden_Entrega()
        {
            InitializeComponent();
        }

        private void FrmRptOrden_Entrega_Load(object sender, EventArgs e)
        {
            Reportes.CR_Orden_Entrega orden = new CR_Orden_Entrega();
            db.Print(orden);
            orden.SetParameterValue("@COD_ORDEN", codentrega);
            CrvOrden_Entrega.ReportSource = orden;
        }
    }
}
