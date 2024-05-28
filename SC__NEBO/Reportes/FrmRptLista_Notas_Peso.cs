﻿using System;
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
    public partial class FrmRptLista_Notas_Peso : Form
    {
        Clases.DB db = new Clases.DB();

        public string fechaini, fechafin, estadonp, cosecha, nombre;

        public FrmRptLista_Notas_Peso()
        {
            InitializeComponent();
        }

        private void FrmRptLista_Notas_Peso_Load(object sender, EventArgs e)
        {
            Reportes.CR_Lista_Notas_Peso nplista = new CR_Lista_Notas_Peso();
            db.Print(nplista);
            nplista.SetParameterValue("@FECHAINI", fechaini);
            nplista.SetParameterValue("@FECHALIMIT", fechafin);
            nplista.SetParameterValue("@ESTADOP", estadonp);
            nplista.SetParameterValue("@COSECHA", cosecha);
            nplista.SetParameterValue("@NOMBRE", nombre);
            CrvListaNP.ReportSource = nplista;
        }
    }
}
