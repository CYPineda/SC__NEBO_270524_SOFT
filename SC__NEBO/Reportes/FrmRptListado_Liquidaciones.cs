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
    public partial class FrmRptListado_Liquidaciones : Form
    {
        Clases.DB db = new Clases.DB();

        public string fechaini, fechafin, estadonp, cosecha, nombre;

        public FrmRptListado_Liquidaciones()
        {
            InitializeComponent();
        }

        private void FrmRptListado_Liquidaciones_Load(object sender, EventArgs e)
        {
            Reportes.CR_Listado_Liquidaciones listado_liquidaciones = new CR_Listado_Liquidaciones();
            db.Print(listado_liquidaciones);
            listado_liquidaciones.SetParameterValue("@FECHAINI", fechaini);
            listado_liquidaciones.SetParameterValue("@FECHALIMIT", fechafin);
            listado_liquidaciones.SetParameterValue("@COSECHA", cosecha);
            listado_liquidaciones.SetParameterValue("@NOMBRE", nombre);
            CrvListado_Liquidaciones.ReportSource = listado_liquidaciones;
        }
    }
}
