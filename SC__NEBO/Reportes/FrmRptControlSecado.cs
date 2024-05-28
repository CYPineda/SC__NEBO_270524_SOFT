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
    public partial class FrmRptControlSecado : Form
    {
        Clases.DB db = new Clases.DB();

        public string codsecado;

        public FrmRptControlSecado()
        {
            InitializeComponent();
        }

        private void pbSalir_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void FrmRptControlSecado_Load(object sender, EventArgs e)
        {
            Reportes.CRControlSecado controlsecado = new CRControlSecado();
            db.Print(controlsecado);
            controlsecado.SetParameterValue("@COD_SECADO", codsecado);
            CrvControlSecado.ReportSource = controlsecado;
        }
    }
}
