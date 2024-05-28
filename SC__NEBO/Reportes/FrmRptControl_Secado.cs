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
    public partial class FrmRptControl_Secado : Form
    {
        Clases.DB db = new Clases.DB();

        public string codsecado;
        public FrmRptControl_Secado()
        {
            InitializeComponent();
        }

        private void FrmRptControl_Secado_Load(object sender, EventArgs e)
        {
            Reportes.CRControlSecado csecado = new CRControlSecado();
            db.Print(csecado);
            csecado.SetParameterValue("@COD_SECADO", codsecado);
            CrvControlSecado.ReportSource = csecado;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
