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
    public partial class FrmRptContrato_Socio : Form
    {
        Clases.DB db = new Clases.DB();

        public string codigo_c;
        public FrmRptContrato_Socio()
        {
            InitializeComponent();
        }

        private void FrmRptContrato_Socio_Load(object sender, EventArgs e)
        {
            Reportes.CR_Contratos_Socio csecado = new CR_Contratos_Socio();
            db.Print(csecado);
            csecado.Refresh();
            csecado.SetParameterValue("@CODIGO_CONTRATO", codigo_c);
            CrvContrato_Socio.ReportSource = csecado;
            CrvContrato_Socio.Refresh();
        }
    }
}
