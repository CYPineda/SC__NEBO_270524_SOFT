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
    public partial class FrmRptPagare : Form
    {
        Clases.DB db = new Clases.DB();
        public string codprestamo;

        public FrmRptPagare()
        {
            InitializeComponent();
        }

        private void FrmRptPagare_Load(object sender, EventArgs e)
        {
            Reportes.CR_Pagare pagare = new CR_Pagare();
            db.Print(pagare);
            pagare.SetParameterValue("@cod_prestamo", codprestamo);
            CrvPagare.ReportSource = pagare;
        }

        private void pbSalir_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
