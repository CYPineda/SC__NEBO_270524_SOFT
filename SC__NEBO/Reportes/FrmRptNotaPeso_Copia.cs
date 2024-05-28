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
    public partial class FrmRptNotaPeso_Copia : Form
    {
        Clases.DB db = new Clases.DB();
        public string codnota;

        public FrmRptNotaPeso_Copia()
        {
            InitializeComponent();
        }

        private void FrmRptNotaPeso_Copia_Load(object sender, EventArgs e)
        {
            Reportes.CR_NotaPeso_Copia notacopia = new Reportes.CR_NotaPeso_Copia();
            db.Print(notacopia);
            notacopia.SetParameterValue("@id_nota", codnota);
            CrvNotaPeso_Copia.ReportSource = notacopia;
        }
    }
}
