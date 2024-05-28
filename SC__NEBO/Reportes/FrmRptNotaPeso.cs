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
    public partial class FrmRptNotaPeso : Form
    {
        Clases.DB db = new Clases.DB();
        public string codnotapeso;
        public FrmRptNotaPeso()
        {
            InitializeComponent();
        }

        private void FrmRptNotaPeso_Load(object sender, EventArgs e)
        {
            Reportes.CR_NotaPeso notaPeso = new CR_NotaPeso();
            db.Print(notaPeso);
            notaPeso.SetParameterValue("@id_nota", codnotapeso);
            CrvNotaPeso.ReportSource = notaPeso;
        }

        private void pbSalir_Click(object sender, EventArgs e)
        {
           Close();
        }
    }
}
