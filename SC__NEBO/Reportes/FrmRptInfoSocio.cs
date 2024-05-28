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
    public partial class FrmRptInfoSocio : Form
    {
        Clases.DB db = new Clases.DB();

        public string codsocio;

        public FrmRptInfoSocio()
        {
            InitializeComponent();
        }

        private void FrmRptInfoSocio_Load(object sender, EventArgs e)
        {
            Reportes.CR_InfoSocio FichaSocio = new Reportes.CR_InfoSocio();
            db.Print(FichaSocio);
            FichaSocio.SetParameterValue("@id_socio", codsocio);
            CrvInfoSocio.ReportSource = FichaSocio;
        }
    }
}
