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
    public partial class FrmNotaPesoRemOriginal : Form
    {
        Clases.DB db = new Clases.DB();
        public string codnota;

        public FrmNotaPesoRemOriginal()
        {
            InitializeComponent();
        }

        private void FrmNotaPesoRemOriginal_Load(object sender, EventArgs e)
        {
            Reportes.CR_NotaPesoRemOriginal NotaRemOriginal = new Reportes.CR_NotaPesoRemOriginal(); 
            db.Print(NotaRemOriginal);
            NotaRemOriginal.SetParameterValue("@id_nota", codnota);
            CrvNotaPesoRempCopia.ReportSource = NotaRemOriginal;
        }
    }
}
