
namespace SC__NEBO.Formularios.Formularios_de_Menu.Prestamos
{
    partial class Frm_Estado_Cuenta
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.DgvData = new System.Windows.Forms.DataGridView();
            this.txtCodSocio = new System.Windows.Forms.TextBox();
            this.txtNombSocio = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.DcFecha_Inicial = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dcNo_Prestamo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DcConcepto = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DcEntregado = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DcAbono = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dcInteres = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.DgvData)).BeginInit();
            this.SuspendLayout();
            // 
            // DgvData
            // 
            this.DgvData.AllowUserToAddRows = false;
            this.DgvData.AllowUserToDeleteRows = false;
            this.DgvData.AllowUserToOrderColumns = true;
            this.DgvData.AllowUserToResizeColumns = false;
            this.DgvData.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(244)))), ((int)(((byte)(244)))));
            this.DgvData.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.DgvData.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(244)))), ((int)(((byte)(244)))));
            this.DgvData.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(101)))), ((int)(((byte)(158)))));
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.DgvData.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.DgvData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DgvData.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.DcFecha_Inicial,
            this.dcNo_Prestamo,
            this.DcConcepto,
            this.DcEntregado,
            this.DcAbono,
            this.dcInteres});
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.MenuHighlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.DgvData.DefaultCellStyle = dataGridViewCellStyle5;
            this.DgvData.Location = new System.Drawing.Point(15, 75);
            this.DgvData.Name = "DgvData";
            this.DgvData.ReadOnly = true;
            this.DgvData.RowHeadersWidth = 5;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(101)))), ((int)(((byte)(158)))));
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.Color.White;
            this.DgvData.RowsDefaultCellStyle = dataGridViewCellStyle6;
            this.DgvData.RowTemplate.Height = 28;
            this.DgvData.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.DgvData.Size = new System.Drawing.Size(853, 457);
            this.DgvData.TabIndex = 124;
            // 
            // txtCodSocio
            // 
            this.txtCodSocio.Enabled = false;
            this.txtCodSocio.Location = new System.Drawing.Point(74, 26);
            this.txtCodSocio.Name = "txtCodSocio";
            this.txtCodSocio.Size = new System.Drawing.Size(100, 26);
            this.txtCodSocio.TabIndex = 125;
            // 
            // txtNombSocio
            // 
            this.txtNombSocio.Enabled = false;
            this.txtNombSocio.Location = new System.Drawing.Point(267, 26);
            this.txtNombSocio.Name = "txtNombSocio";
            this.txtNombSocio.Size = new System.Drawing.Size(288, 26);
            this.txtNombSocio.TabIndex = 126;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(78, 18);
            this.label1.TabIndex = 175;
            this.label1.Text = "CODIGO:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(199, 29);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(76, 18);
            this.label2.TabIndex = 176;
            this.label2.Text = "NOMBRE:";
            // 
            // DcFecha_Inicial
            // 
            dataGridViewCellStyle3.Format = "d";
            dataGridViewCellStyle3.NullValue = null;
            this.DcFecha_Inicial.DefaultCellStyle = dataGridViewCellStyle3;
            this.DcFecha_Inicial.HeaderText = "FECHA";
            this.DcFecha_Inicial.MinimumWidth = 8;
            this.DcFecha_Inicial.Name = "DcFecha_Inicial";
            this.DcFecha_Inicial.ReadOnly = true;
            this.DcFecha_Inicial.Width = 125;
            // 
            // dcNo_Prestamo
            // 
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(101)))), ((int)(((byte)(158)))));
            this.dcNo_Prestamo.DefaultCellStyle = dataGridViewCellStyle4;
            this.dcNo_Prestamo.HeaderText = "CODIGO";
            this.dcNo_Prestamo.MinimumWidth = 8;
            this.dcNo_Prestamo.Name = "dcNo_Prestamo";
            this.dcNo_Prestamo.ReadOnly = true;
            this.dcNo_Prestamo.Width = 125;
            // 
            // DcConcepto
            // 
            this.DcConcepto.HeaderText = "CONCEPTO";
            this.DcConcepto.MinimumWidth = 6;
            this.DcConcepto.Name = "DcConcepto";
            this.DcConcepto.ReadOnly = true;
            this.DcConcepto.Width = 125;
            // 
            // DcEntregado
            // 
            this.DcEntregado.HeaderText = "ENTREGADO";
            this.DcEntregado.MinimumWidth = 6;
            this.DcEntregado.Name = "DcEntregado";
            this.DcEntregado.ReadOnly = true;
            this.DcEntregado.Width = 125;
            // 
            // DcAbono
            // 
            this.DcAbono.HeaderText = "ABONO";
            this.DcAbono.MinimumWidth = 6;
            this.DcAbono.Name = "DcAbono";
            this.DcAbono.ReadOnly = true;
            this.DcAbono.Width = 125;
            // 
            // dcInteres
            // 
            this.dcInteres.HeaderText = "INTERÉS %";
            this.dcInteres.MinimumWidth = 8;
            this.dcInteres.Name = "dcInteres";
            this.dcInteres.ReadOnly = true;
            this.dcInteres.Width = 90;
            // 
            // Frm_Estado_Cuenta
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(892, 563);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtNombSocio);
            this.Controls.Add(this.txtCodSocio);
            this.Controls.Add(this.DgvData);
            this.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "Frm_Estado_Cuenta";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Frm_Estado_Cuenta";
            this.Load += new System.EventHandler(this.Frm_Estado_Cuenta_Load);
            ((System.ComponentModel.ISupportInitialize)(this.DgvData)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView DgvData;
        private System.Windows.Forms.TextBox txtCodSocio;
        private System.Windows.Forms.TextBox txtNombSocio;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridViewTextBoxColumn DcFecha_Inicial;
        private System.Windows.Forms.DataGridViewTextBoxColumn dcNo_Prestamo;
        private System.Windows.Forms.DataGridViewTextBoxColumn DcConcepto;
        private System.Windows.Forms.DataGridViewTextBoxColumn DcEntregado;
        private System.Windows.Forms.DataGridViewTextBoxColumn DcAbono;
        private System.Windows.Forms.DataGridViewTextBoxColumn dcInteres;
    }
}