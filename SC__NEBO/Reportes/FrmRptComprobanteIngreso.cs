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
    public partial class FrmRptComprobanteIngreso : Form
    {
        Clases.DB db = new Clases.DB();
        public string codpago;

        public FrmRptComprobanteIngreso()
        {
            InitializeComponent();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void FrmRptComprobanteIngreso_Load(object sender, EventArgs e)
        {

            Reportes.CR_ComprobantePrestamoIngreso ingreso = new Reportes.CR_ComprobantePrestamoIngreso();
            db.Print(ingreso);
            ingreso.SetParameterValue("@id_pago", codpago);
            CrvComprobanteIngreso.ReportSource = ingreso;
        }
    }
}
