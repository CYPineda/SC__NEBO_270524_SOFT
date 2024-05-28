
namespace SC__NEBO.Reportes
{
    partial class FrmRptComprobanteIngresoLiquidacion
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.CRVComprobanteIngresoLiquidacion = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
            this.SuspendLayout();
            // 
            // CRVComprobanteIngresoLiquidacion
            // 
            this.CRVComprobanteIngresoLiquidacion.ActiveViewIndex = -1;
            this.CRVComprobanteIngresoLiquidacion.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.CRVComprobanteIngresoLiquidacion.Cursor = System.Windows.Forms.Cursors.Default;
            this.CRVComprobanteIngresoLiquidacion.Dock = System.Windows.Forms.DockStyle.Fill;
            this.CRVComprobanteIngresoLiquidacion.Location = new System.Drawing.Point(0, 0);
            this.CRVComprobanteIngresoLiquidacion.Name = "CRVComprobanteIngresoLiquidacion";
            this.CRVComprobanteIngresoLiquidacion.ShowLogo = false;
            this.CRVComprobanteIngresoLiquidacion.Size = new System.Drawing.Size(885, 455);
            this.CRVComprobanteIngresoLiquidacion.TabIndex = 0;
            this.CRVComprobanteIngresoLiquidacion.ToolPanelView = CrystalDecisions.Windows.Forms.ToolPanelViewType.None;
            // 
            // FrmRptComprobanteIngresoLiquidacion
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.Color.AliceBlue;
            this.ClientSize = new System.Drawing.Size(885, 455);
            this.Controls.Add(this.CRVComprobanteIngresoLiquidacion);
            this.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "FrmRptComprobanteIngresoLiquidacion";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "COMPROBANTE DE INGRESO";
            this.Load += new System.EventHandler(this.FrmRptComprobanteIngresoLiquidacion_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private CrystalDecisions.Windows.Forms.CrystalReportViewer CRVComprobanteIngresoLiquidacion;
    }
}