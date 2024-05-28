
namespace SC__NEBO.Formularios.Formularios_de_Menu.Prestamos
{
    partial class FrmHistorialPrestamos
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmHistorialPrestamos));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.txtNomb_Socio = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnRegresar = new System.Windows.Forms.Button();
            this.DgvData = new System.Windows.Forms.DataGridView();
            this.dcNo_Prestamo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DcFecha_Inicial = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DcSocio = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dcTipo_Prestamo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dcMonto = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dcInteres = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dcMonto_Pendiente = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DcEstado = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnBuscar = new System.Windows.Forms.Button();
            this.btnRealizarPago = new System.Windows.Forms.Button();
            this.btnPagare = new System.Windows.Forms.Button();
            this.btnRecibo = new System.Windows.Forms.Button();
            this.btnEstadoCuenta = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.DgvData)).BeginInit();
            this.SuspendLayout();
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(101)))), ((int)(((byte)(158)))));
            this.panel3.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel3.Location = new System.Drawing.Point(1075, 8);
            this.panel3.Margin = new System.Windows.Forms.Padding(4);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(5, 599);
            this.panel3.TabIndex = 113;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(101)))), ((int)(((byte)(158)))));
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1080, 8);
            this.panel2.TabIndex = 112;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(101)))), ((int)(((byte)(158)))));
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 8);
            this.panel1.Margin = new System.Windows.Forms.Padding(4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(5, 599);
            this.panel1.TabIndex = 114;
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(101)))), ((int)(((byte)(158)))));
            this.panel4.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel4.Location = new System.Drawing.Point(5, 602);
            this.panel4.Margin = new System.Windows.Forms.Padding(4);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(1070, 5);
            this.panel4.TabIndex = 115;
            // 
            // txtNomb_Socio
            // 
            this.txtNomb_Socio.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNomb_Socio.Location = new System.Drawing.Point(187, 27);
            this.txtNomb_Socio.MaxLength = 14;
            this.txtNomb_Socio.Name = "txtNomb_Socio";
            this.txtNomb_Socio.Size = new System.Drawing.Size(320, 22);
            this.txtNomb_Socio.TabIndex = 119;
            this.txtNomb_Socio.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtNomb_Socio_KeyDown);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(136, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(50, 16);
            this.label1.TabIndex = 118;
            this.label1.Text = "SOCIO:";
            // 
            // btnRegresar
            // 
            this.btnRegresar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(101)))), ((int)(((byte)(158)))));
            this.btnRegresar.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRegresar.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.btnRegresar.Image = ((System.Drawing.Image)(resources.GetObject("btnRegresar.Image")));
            this.btnRegresar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnRegresar.Location = new System.Drawing.Point(21, 22);
            this.btnRegresar.Name = "btnRegresar";
            this.btnRegresar.Size = new System.Drawing.Size(109, 32);
            this.btnRegresar.TabIndex = 122;
            this.btnRegresar.Text = " REGRESAR";
            this.btnRegresar.UseVisualStyleBackColor = false;
            // 
            // DgvData
            // 
            this.DgvData.AllowUserToAddRows = false;
            this.DgvData.AllowUserToDeleteRows = false;
            this.DgvData.AllowUserToOrderColumns = true;
            this.DgvData.AllowUserToResizeColumns = false;
            this.DgvData.AllowUserToResizeRows = false;
            dataGridViewCellStyle7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(244)))), ((int)(((byte)(244)))));
            this.DgvData.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle7;
            this.DgvData.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(244)))), ((int)(((byte)(244)))));
            this.DgvData.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle8.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle8.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle8.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(101)))), ((int)(((byte)(158)))));
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.DgvData.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle8;
            this.DgvData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DgvData.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dcNo_Prestamo,
            this.DcFecha_Inicial,
            this.DcSocio,
            this.dcTipo_Prestamo,
            this.dcMonto,
            this.dcInteres,
            this.dcMonto_Pendiente,
            this.DcEstado});
            dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle11.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle11.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle11.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle11.SelectionBackColor = System.Drawing.SystemColors.MenuHighlight;
            dataGridViewCellStyle11.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle11.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.DgvData.DefaultCellStyle = dataGridViewCellStyle11;
            this.DgvData.Location = new System.Drawing.Point(21, 64);
            this.DgvData.Name = "DgvData";
            this.DgvData.ReadOnly = true;
            this.DgvData.RowHeadersWidth = 5;
            dataGridViewCellStyle12.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle12.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle12.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle12.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(101)))), ((int)(((byte)(158)))));
            dataGridViewCellStyle12.SelectionForeColor = System.Drawing.Color.White;
            this.DgvData.RowsDefaultCellStyle = dataGridViewCellStyle12;
            this.DgvData.RowTemplate.Height = 28;
            this.DgvData.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.DgvData.Size = new System.Drawing.Size(1037, 495);
            this.DgvData.TabIndex = 123;
            // 
            // dcNo_Prestamo
            // 
            dataGridViewCellStyle9.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle9.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(101)))), ((int)(((byte)(158)))));
            this.dcNo_Prestamo.DefaultCellStyle = dataGridViewCellStyle9;
            this.dcNo_Prestamo.HeaderText = "NO_PRÉSTAMO";
            this.dcNo_Prestamo.MinimumWidth = 8;
            this.dcNo_Prestamo.Name = "dcNo_Prestamo";
            this.dcNo_Prestamo.ReadOnly = true;
            // 
            // DcFecha_Inicial
            // 
            dataGridViewCellStyle10.Format = "d";
            dataGridViewCellStyle10.NullValue = null;
            this.DcFecha_Inicial.DefaultCellStyle = dataGridViewCellStyle10;
            this.DcFecha_Inicial.HeaderText = "FECHA_INICIO";
            this.DcFecha_Inicial.MinimumWidth = 8;
            this.DcFecha_Inicial.Name = "DcFecha_Inicial";
            this.DcFecha_Inicial.ReadOnly = true;
            // 
            // DcSocio
            // 
            this.DcSocio.HeaderText = "SOCIO";
            this.DcSocio.Name = "DcSocio";
            this.DcSocio.ReadOnly = true;
            this.DcSocio.Width = 250;
            // 
            // dcTipo_Prestamo
            // 
            this.dcTipo_Prestamo.HeaderText = "TIPO DE PRÉSTAMO";
            this.dcTipo_Prestamo.MinimumWidth = 8;
            this.dcTipo_Prestamo.Name = "dcTipo_Prestamo";
            this.dcTipo_Prestamo.ReadOnly = true;
            this.dcTipo_Prestamo.Width = 150;
            // 
            // dcMonto
            // 
            this.dcMonto.HeaderText = "MONTO";
            this.dcMonto.MinimumWidth = 8;
            this.dcMonto.Name = "dcMonto";
            this.dcMonto.ReadOnly = true;
            this.dcMonto.Width = 90;
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
            // DcEstado
            // 
            this.DcEstado.HeaderText = "ESTADO";
            this.DcEstado.Name = "DcEstado";
            this.DcEstado.ReadOnly = true;
            // 
            // btnBuscar
            // 
            this.btnBuscar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(101)))), ((int)(((byte)(158)))));
            this.btnBuscar.Image = ((System.Drawing.Image)(resources.GetObject("btnBuscar.Image")));
            this.btnBuscar.Location = new System.Drawing.Point(513, 26);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(28, 25);
            this.btnBuscar.TabIndex = 124;
            this.btnBuscar.UseVisualStyleBackColor = false;
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);
            // 
            // btnRealizarPago
            // 
            this.btnRealizarPago.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(101)))), ((int)(((byte)(158)))));
            this.btnRealizarPago.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRealizarPago.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.btnRealizarPago.Image = ((System.Drawing.Image)(resources.GetObject("btnRealizarPago.Image")));
            this.btnRealizarPago.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnRealizarPago.Location = new System.Drawing.Point(881, 22);
            this.btnRealizarPago.Margin = new System.Windows.Forms.Padding(2);
            this.btnRealizarPago.Name = "btnRealizarPago";
            this.btnRealizarPago.Size = new System.Drawing.Size(177, 32);
            this.btnRealizarPago.TabIndex = 125;
            this.btnRealizarPago.Text = "REALIZAR UN PAGO";
            this.btnRealizarPago.UseVisualStyleBackColor = false;
            this.btnRealizarPago.Click += new System.EventHandler(this.btnRealizarPago_Click);
            // 
            // btnPagare
            // 
            this.btnPagare.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(101)))), ((int)(((byte)(158)))));
            this.btnPagare.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPagare.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.btnPagare.Image = ((System.Drawing.Image)(resources.GetObject("btnPagare.Image")));
            this.btnPagare.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnPagare.Location = new System.Drawing.Point(759, 23);
            this.btnPagare.Margin = new System.Windows.Forms.Padding(2);
            this.btnPagare.Name = "btnPagare";
            this.btnPagare.Size = new System.Drawing.Size(107, 32);
            this.btnPagare.TabIndex = 126;
            this.btnPagare.Text = "  PAGARÉ";
            this.btnPagare.UseVisualStyleBackColor = false;
            // 
            // btnRecibo
            // 
            this.btnRecibo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(101)))), ((int)(((byte)(158)))));
            this.btnRecibo.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRecibo.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.btnRecibo.Image = ((System.Drawing.Image)(resources.GetObject("btnRecibo.Image")));
            this.btnRecibo.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnRecibo.Location = new System.Drawing.Point(636, 22);
            this.btnRecibo.Margin = new System.Windows.Forms.Padding(2);
            this.btnRecibo.Name = "btnRecibo";
            this.btnRecibo.Size = new System.Drawing.Size(107, 32);
            this.btnRecibo.TabIndex = 127;
            this.btnRecibo.Text = " RECIBO";
            this.btnRecibo.UseVisualStyleBackColor = false;
            // 
            // btnEstadoCuenta
            // 
            this.btnEstadoCuenta.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(101)))), ((int)(((byte)(158)))));
            this.btnEstadoCuenta.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEstadoCuenta.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.btnEstadoCuenta.Image = ((System.Drawing.Image)(resources.GetObject("btnEstadoCuenta.Image")));
            this.btnEstadoCuenta.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnEstadoCuenta.Location = new System.Drawing.Point(881, 564);
            this.btnEstadoCuenta.Margin = new System.Windows.Forms.Padding(2);
            this.btnEstadoCuenta.Name = "btnEstadoCuenta";
            this.btnEstadoCuenta.Size = new System.Drawing.Size(177, 32);
            this.btnEstadoCuenta.TabIndex = 128;
            this.btnEstadoCuenta.Text = "ESTADO CUENTA";
            this.btnEstadoCuenta.UseVisualStyleBackColor = false;
            this.btnEstadoCuenta.Click += new System.EventHandler(this.btnEstadoCuenta_Click);
            // 
            // FrmHistorialPrestamos
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.Color.AliceBlue;
            this.ClientSize = new System.Drawing.Size(1080, 607);
            this.Controls.Add(this.btnEstadoCuenta);
            this.Controls.Add(this.btnRecibo);
            this.Controls.Add(this.btnPagare);
            this.Controls.Add(this.btnRealizarPago);
            this.Controls.Add(this.btnBuscar);
            this.Controls.Add(this.DgvData);
            this.Controls.Add(this.btnRegresar);
            this.Controls.Add(this.txtNomb_Socio);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Name = "FrmHistorialPrestamos";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "---";
            this.Load += new System.EventHandler(this.FrmHistorialPrestamos_Load);
            ((System.ComponentModel.ISupportInitialize)(this.DgvData)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.TextBox txtNomb_Socio;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnRegresar;
        private System.Windows.Forms.DataGridView DgvData;
        private System.Windows.Forms.Button btnBuscar;
        private System.Windows.Forms.Button btnRealizarPago;
        private System.Windows.Forms.Button btnPagare;
        private System.Windows.Forms.Button btnRecibo;
        private System.Windows.Forms.DataGridViewTextBoxColumn dcNo_Prestamo;
        private System.Windows.Forms.DataGridViewTextBoxColumn DcFecha_Inicial;
        private System.Windows.Forms.DataGridViewTextBoxColumn DcSocio;
        private System.Windows.Forms.DataGridViewTextBoxColumn dcTipo_Prestamo;
        private System.Windows.Forms.DataGridViewTextBoxColumn dcMonto;
        private System.Windows.Forms.DataGridViewTextBoxColumn dcInteres;
        private System.Windows.Forms.DataGridViewTextBoxColumn dcMonto_Pendiente;
        private System.Windows.Forms.DataGridViewTextBoxColumn DcEstado;
        private System.Windows.Forms.Button btnEstadoCuenta;
    }
}