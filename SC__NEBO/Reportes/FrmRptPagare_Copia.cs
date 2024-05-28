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
    public partial class FrmRptPagare_Copia : Form
    {
        Clases.DB db = new Clases.DB();
        public string codPrestamo;

        public FrmRptPagare_Copia()
        {
            InitializeComponent();
        }

        private void FrmRptPagare_Copia_Load(object sender, EventArgs e)
        {
            Reportes.CR_Pagare_Copia PagareCopia = new Reportes.CR_Pagare_Copia();
            db.Print(PagareCopia);
            PagareCopia.SetParameterValue("@cod_prestamo", codPrestamo);
            CrvPagare.ReportSource = PagareCopia;
        }
    }
}
