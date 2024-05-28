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
    public partial class FrmRptListadoSocios : Form
    {
        Clases.DB db = new Clases.DB();
        public FrmRptListadoSocios()
        {
            InitializeComponent();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void FrmRptListadoSocios_Load(object sender, EventArgs e)
        {
            Reportes.CR_Listado_Socios listado_Socios = new Reportes.CR_Listado_Socios();
            db.Print(listado_Socios);
            listado_Socios.Refresh();
            CrvListadoSocios.ReportSource = listado_Socios;
            CrvListadoSocios.Refresh();
        }
    }
}
