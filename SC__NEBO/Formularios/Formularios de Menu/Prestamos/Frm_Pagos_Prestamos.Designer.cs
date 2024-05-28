
namespace SC__NEBO.Formularios.Formularios_de_Menu.Prestamos
{
    partial class Frm_Pagos_Prestamos
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_Pagos_Prestamos));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panel2 = new System.Windows.Forms.Panel();
            this.txtNomb_Socio = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnSeleccionar_Prestamo = new System.Windows.Forms.Button();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.txtIdSocio = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnRegresar = new System.Windows.Forms.Button();
            this.DgvData = new System.Windows.Forms.DataGridView();
            this.dcNo_Prestamo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DcFecha_Inicial = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DcFecha_Final = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dcTipo_Prestamo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dcCantidad_LPS = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dcInteres = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dcMonto_Pendiente = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DcUltimoPago = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.DgvData)).BeginInit();
            this.SuspendLayout();
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(101)))), ((int)(((byte)(158)))));
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Margin = new System.Windows.Forms.Padding(2);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1133, 5);
            this.panel2.TabIndex = 17;
            this.panel2.Paint += new System.Windows.Forms.PaintEventHandler(this.panel2_Paint);
            this.panel2.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panel2_MouseDown);
            // 
            // txtNomb_Socio
            // 
            this.txtNomb_Socio.Enabled = false;
            this.txtNomb_Socio.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNomb_Socio.Location = new System.Drawing.Point(392, 20);
            this.txtNomb_Socio.Margin = new System.Windows.Forms.Padding(2);
            this.txtNomb_Socio.MaxLength = 14;
            this.txtNomb_Socio.Name = "txtNomb_Socio";
            this.txtNomb_Socio.Size = new System.Drawing.Size(215, 22);
            this.txtNomb_Socio.TabIndex = 85;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(341, 22);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(50, 16);
            this.label1.TabIndex = 84;
            this.label1.Text = "SOCIO:";
            // 
            // btnSeleccionar_Prestamo
            // 
            this.btnSeleccionar_Prestamo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(101)))), ((int)(((byte)(158)))));
            this.btnSeleccionar_Prestamo.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.btnSeleccionar_Prestamo.Image = ((System.Drawing.Image)(resources.GetObject("btnSeleccionar_Prestamo.Image")));
            this.btnSeleccionar_Prestamo.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSeleccionar_Prestamo.Location = new System.Drawing.Point(944, 15);
            this.btnSeleccionar_Prestamo.Margin = new System.Windows.Forms.Padding(2);
            this.btnSeleccionar_Prestamo.Name = "btnSeleccionar_Prestamo";
            this.btnSeleccionar_Prestamo.Size = new System.Drawing.Size(172, 32);
            this.btnSeleccionar_Prestamo.TabIndex = 102;
            this.btnSeleccionar_Prestamo.Text = "REALIZAR UN PAGO";
            this.btnSeleccionar_Prestamo.UseVisualStyleBackColor = false;
            this.btnSeleccionar_Prestamo.Click += new System.EventHandler(this.btnSeleccionar_Prestamo_Click);
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(101)))), ((int)(((byte)(158)))));
            this.panel3.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel3.Location = new System.Drawing.Point(0, 5);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(5, 398);
            this.panel3.TabIndex = 112;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(101)))), ((int)(((byte)(158)))));
            this.panel1.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel1.Location = new System.Drawing.Point(1128, 5);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(5, 398);
            this.panel1.TabIndex = 113;
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(101)))), ((int)(((byte)(158)))));
            this.panel4.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel4.Location = new System.Drawing.Point(5, 398);
            this.panel4.Margin = new System.Windows.Forms.Padding(2);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(1123, 5);
            this.panel4.TabIndex = 114;
            // 
            // txtIdSocio
            // 
            this.txtIdSocio.Enabled = false;
            this.txtIdSocio.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtIdSocio.Location = new System.Drawing.Point(237, 20);
            this.txtIdSocio.Margin = new System.Windows.Forms.Padding(2);
            this.txtIdSocio.MaxLength = 14;
            this.txtIdSocio.Name = "txtIdSocio";
            this.txtIdSocio.Size = new System.Drawing.Size(89, 22);
            this.txtIdSocio.TabIndex = 116;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(169, 22);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(68, 16);
            this.label2.TabIndex = 115;
            this.label2.Text = "ID_SOCIO:";
            // 
            // btnRegresar
            // 
            this.btnRegresar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(101)))), ((int)(((byte)(158)))));
            this.btnRegresar.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.btnRegresar.Image = ((System.Drawing.Image)(resources.GetObject("btnRegresar.Image")));
            this.btnRegresar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnRegresar.Location = new System.Drawing.Point(19, 14);
            this.btnRegresar.Margin = new System.Windows.Forms.Padding(2);
            this.btnRegresar.Name = "btnRegresar";
            this.btnRegresar.Size = new System.Drawing.Size(109, 32);
            this.btnRegresar.TabIndex = 117;
            this.btnRegresar.Text = " REGRESAR";
            this.btnRegresar.UseVisualStyleBackColor = false;
            this.btnRegresar.Click += new System.EventHandler(this.btnRegresar_Click_1);
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
            this.DgvData.BackgroundColor = System.Drawing.SystemColors.Control;
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
            this.dcNo_Prestamo,
            this.DcFecha_Inicial,
            this.DcFecha_Final,
            this.dcTipo_Prestamo,
            this.dcCantidad_LPS,
            this.dcInteres,
            this.dcMonto_Pendiente,
            this.DcUltimoPago});
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.MenuHighlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.DgvData.DefaultCellStyle = dataGridViewCellStyle6;
            this.DgvData.Location = new System.Drawing.Point(19, 54);
            this.DgvData.Margin = new System.Windows.Forms.Padding(2);
            this.DgvData.Name = "DgvData";
            this.DgvData.ReadOnly = true;
            this.DgvData.RowHeadersWidth = 5;
            dataGridViewCellStyle7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(244)))), ((int)(((byte)(244)))));
            dataGridViewCellStyle7.Font = new System.Drawing.Font("Century Gothic", 10F);
            dataGridViewCellStyle7.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(101)))), ((int)(((byte)(158)))));
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.Color.White;
            this.DgvData.RowsDefaultCellStyle = dataGridViewCellStyle7;
            this.DgvData.RowTemplate.Height = 28;
            this.DgvData.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.DgvData.Size = new System.Drawing.Size(1097, 330);
            this.DgvData.TabIndex = 124;
            // 
            // dcNo_Prestamo
            // 
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(101)))), ((int)(((byte)(158)))));
            this.dcNo_Prestamo.DefaultCellStyle = dataGridViewCellStyle3;
            this.dcNo_Prestamo.HeaderText = "NO_PRÉSTAMO";
            this.dcNo_Prestamo.MinimumWidth = 8;
            this.dcNo_Prestamo.Name = "dcNo_Prestamo";
            this.dcNo_Prestamo.ReadOnly = true;
            this.dcNo_Prestamo.Width = 150;
            // 
            // DcFecha_Inicial
            // 
            dataGridViewCellStyle4.Format = "d";
            dataGridViewCellStyle4.NullValue = null;
            this.DcFecha_Inicial.DefaultCellStyle = dataGridViewCellStyle4;
            this.DcFecha_Inicial.HeaderText = "FECHA_INICIO";
            this.DcFecha_Inicial.MinimumWidth = 8;
            this.DcFecha_Inicial.Name = "DcFecha_Inicial";
            this.DcFecha_Inicial.ReadOnly = true;
            this.DcFecha_Inicial.Width = 150;
            // 
            // DcFecha_Final
            // 
            dataGridViewCellStyle5.Format = "d";
            dataGridViewCellStyle5.NullValue = null;
            this.DcFecha_Final.DefaultCellStyle = dataGridViewCellStyle5;
            this.DcFecha_Final.HeaderText = "FECHA_FINAL";
            this.DcFecha_Final.MinimumWidth = 8;
            this.DcFecha_Final.Name = "DcFecha_Final";
            this.DcFecha_Final.ReadOnly = true;
            this.DcFecha_Final.Width = 150;
            // 
            // dcTipo_Prestamo
            // 
            this.dcTipo_Prestamo.HeaderText = "TIPO DE PRÉSTAMO";
            this.dcTipo_Prestamo.MinimumWidth = 8;
            this.dcTipo_Prestamo.Name = "dcTipo_Prestamo";
            this.dcTipo_Prestamo.ReadOnly = true;
            this.dcTipo_Prestamo.Width = 150;
            // 
            // dcCantidad_LPS
            // 
            this.dcCantidad_LPS.HeaderText = "CANTIDAD LPS";
            this.dcCantidad_LPS.MinimumWidth = 8;
            this.dcCantidad_LPS.Name = "dcCantidad_LPS";
            this.dcCantidad_LPS.ReadOnly = true;
            this.dcCantidad_LPS.Width = 130;
            // 
            // dcInteres
            // 
            this.dcInteres.HeaderText = "INTERÉS %";
            this.dcInteres.MinimumWidth = 8;
            this.dcInteres.Name = "dcInteres";
            this.dcInteres.ReadOnly = true;
            this.dcInteres.Width = 90;
            // 
            // dcMonto_Pendiente
            // 
            this.dcMonto_Pendiente.HeaderText = "MONTO PENDIENTE";
            this.dcMonto_Pendiente.MinimumWidth = 8;
            this.dcMonto_Pendiente.Name = "dcMonto_Pendiente";
            this.dcMonto_Pendiente.ReadOnly = true;
            this.dcMonto_Pendiente.Width = 150;
            // 
            // DcUltimoPago
            // 
            this.DcUltimoPago.HeaderText = "ULTIMO PAGO";
            this.DcUltimoPago.MinimumWidth = 8;
            this.DcUltimoPago.Name = "DcUltimoPago";
            this.DcUltimoPago.ReadOnly = true;
            this.DcUltimoPago.Width = 120;
            // 
            // Frm_Pagos_Prestamos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(1133, 403);
            this.Controls.Add(this.DgvData);
            this.Controls.Add(this.btnRegresar);
            this.Controls.Add(this.txtIdSocio);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.btnSeleccionar_Prestamo);
            this.Controls.Add(this.txtNomb_Socio);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.panel2);
            this.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "Frm_Pagos_Prestamos";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "---";
            this.Load += new System.EventHandler(this.Frm_Pagos_Prestamos_Load);
            ((System.ComponentModel.ISupportInitialize)(this.DgvData)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TextBox txtNomb_Socio;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnSeleccionar_Prestamo;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.TextBox txtIdSocio;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnRegresar;
        private System.Windows.Forms.DataGridView DgvData;
        private System.Windows.Forms.DataGridViewTextBoxColumn dcNo_Prestamo;
        private System.Windows.Forms.DataGridViewTextBoxColumn DcFecha_Inicial;
        private System.Windows.Forms.DataGridViewTextBoxColumn DcFecha_Final;
        private System.Windows.Forms.DataGridViewTextBoxColumn dcTipo_Prestamo;
        private System.Windows.Forms.DataGridViewTextBoxColumn dcCantidad_LPS;
        private System.Windows.Forms.DataGridViewTextBoxColumn dcInteres;
        private System.Windows.Forms.DataGridViewTextBoxColumn dcMonto_Pendiente;
        private System.Windows.Forms.DataGridViewTextBoxColumn DcUltimoPago;
    }
}