
namespace SC__NEBO.Formularios.Formularios_de_Menu.Notas_de_Peso
{
    partial class Frm_Lista_Nota_Peso
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_Lista_Nota_Peso));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panel2 = new System.Windows.Forms.Panel();
            this.pbMinimizar = new System.Windows.Forms.PictureBox();
            this.pbSalir = new System.Windows.Forms.PictureBox();
            this.label11 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.DgvData = new System.Windows.Forms.DataGridView();
            this.dcIDNota = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DcSocio = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dcFinca = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DcUbicacionFinca = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dcFecha = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dcEstado = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dcPesoBruto = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dcDescuentoHumedo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dcqqNetos = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.txtBuscar = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.dtpFechaIncial = new System.Windows.Forms.DateTimePicker();
            this.dtpFechaFinal = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblQQNetos = new System.Windows.Forms.Label();
            this.lblRecuento = new System.Windows.Forms.Label();
            this.cmbCosecha = new System.Windows.Forms.ComboBox();
            this.rbtnLiquidado = new System.Windows.Forms.RadioButton();
            this.rbtnDeposito = new System.Windows.Forms.RadioButton();
            this.btnRecibo = new System.Windows.Forms.Button();
            this.BtnBuscar = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.lblQQHumedos = new System.Windows.Forms.Label();
            this.btnLista = new System.Windows.Forms.Button();
            this.BtnReimprimirNotaOriginal = new System.Windows.Forms.Button();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbMinimizar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbSalir)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DgvData)).BeginInit();
            this.SuspendLayout();
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(101)))), ((int)(((byte)(158)))));
            this.panel2.Controls.Add(this.pbMinimizar);
            this.panel2.Controls.Add(this.pbSalir);
            this.panel2.Controls.Add(this.label11);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1340, 27);
            this.panel2.TabIndex = 16;
            // 
            // pbMinimizar
            // 
            this.pbMinimizar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pbMinimizar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pbMinimizar.Image = ((System.Drawing.Image)(resources.GetObject("pbMinimizar.Image")));
            this.pbMinimizar.Location = new System.Drawing.Point(1283, 5);
            this.pbMinimizar.Margin = new System.Windows.Forms.Padding(2);
            this.pbMinimizar.Name = "pbMinimizar";
            this.pbMinimizar.Size = new System.Drawing.Size(18, 18);
            this.pbMinimizar.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pbMinimizar.TabIndex = 35;
            this.pbMinimizar.TabStop = false;
            // 
            // pbSalir
            // 
            this.pbSalir.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pbSalir.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pbSalir.Image = ((System.Drawing.Image)(resources.GetObject("pbSalir.Image")));
            this.pbSalir.Location = new System.Drawing.Point(1315, 5);
            this.pbSalir.Margin = new System.Windows.Forms.Padding(2);
            this.pbSalir.Name = "pbSalir";
            this.pbSalir.Size = new System.Drawing.Size(18, 18);
            this.pbSalir.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pbSalir.TabIndex = 34;
            this.pbSalir.TabStop = false;
            this.pbSalir.Click += new System.EventHandler(this.pbSalir_Click);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Century Gothic", 10F, System.Drawing.FontStyle.Bold);
            this.label11.ForeColor = System.Drawing.SystemColors.Control;
            this.label11.Location = new System.Drawing.Point(637, 3);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(192, 17);
            this.label11.TabIndex = 33;
            this.label11.Text = "LISTADO DE NOTAS DE PESO";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(101)))), ((int)(((byte)(158)))));
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(7, 511);
            this.panel1.Margin = new System.Windows.Forms.Padding(4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1326, 6);
            this.panel1.TabIndex = 125;
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(101)))), ((int)(((byte)(158)))));
            this.panel4.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel4.Location = new System.Drawing.Point(1333, 27);
            this.panel4.Margin = new System.Windows.Forms.Padding(4);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(7, 490);
            this.panel4.TabIndex = 124;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(101)))), ((int)(((byte)(158)))));
            this.panel3.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel3.Location = new System.Drawing.Point(0, 27);
            this.panel3.Margin = new System.Windows.Forms.Padding(4);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(7, 490);
            this.panel3.TabIndex = 123;
            // 
            // DgvData
            // 
            this.DgvData.AllowUserToAddRows = false;
            this.DgvData.AllowUserToDeleteRows = false;
            this.DgvData.AllowUserToOrderColumns = true;
            this.DgvData.AllowUserToResizeColumns = false;
            this.DgvData.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.AliceBlue;
            this.DgvData.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.DgvData.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.DgvData.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleVertical;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.DgvData.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.DgvData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DgvData.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dcIDNota,
            this.DcSocio,
            this.dcFinca,
            this.DcUbicacionFinca,
            this.dcFecha,
            this.dcEstado,
            this.dcPesoBruto,
            this.dcDescuentoHumedo,
            this.dcqqNetos});
            this.DgvData.Location = new System.Drawing.Point(24, 76);
            this.DgvData.Margin = new System.Windows.Forms.Padding(2);
            this.DgvData.Name = "DgvData";
            this.DgvData.ReadOnly = true;
            this.DgvData.RowHeadersWidth = 5;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Century Gothic", 10F);
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(101)))), ((int)(((byte)(158)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.White;
            this.DgvData.RowsDefaultCellStyle = dataGridViewCellStyle3;
            this.DgvData.RowTemplate.Height = 28;
            this.DgvData.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.DgvData.Size = new System.Drawing.Size(1290, 400);
            this.DgvData.TabIndex = 126;
            this.DgvData.DoubleClick += new System.EventHandler(this.DgvData_DoubleClick);
            // 
            // dcIDNota
            // 
            this.dcIDNota.HeaderText = "ID NOTA";
            this.dcIDNota.MinimumWidth = 8;
            this.dcIDNota.Name = "dcIDNota";
            this.dcIDNota.ReadOnly = true;
            this.dcIDNota.Width = 110;
            // 
            // DcSocio
            // 
            this.DcSocio.HeaderText = "SOCIO";
            this.DcSocio.MinimumWidth = 8;
            this.DcSocio.Name = "DcSocio";
            this.DcSocio.ReadOnly = true;
            this.DcSocio.Width = 250;
            // 
            // dcFinca
            // 
            this.dcFinca.HeaderText = "FINCA";
            this.dcFinca.MinimumWidth = 8;
            this.dcFinca.Name = "dcFinca";
            this.dcFinca.ReadOnly = true;
            this.dcFinca.Width = 220;
            // 
            // DcUbicacionFinca
            // 
            this.DcUbicacionFinca.HeaderText = "UBICACIÓN";
            this.DcUbicacionFinca.Name = "DcUbicacionFinca";
            this.DcUbicacionFinca.ReadOnly = true;
            this.DcUbicacionFinca.Width = 150;
            // 
            // dcFecha
            // 
            this.dcFecha.HeaderText = "FECHA";
            this.dcFecha.MinimumWidth = 8;
            this.dcFecha.Name = "dcFecha";
            this.dcFecha.ReadOnly = true;
            // 
            // dcEstado
            // 
            this.dcEstado.HeaderText = "ESTADO";
            this.dcEstado.MinimumWidth = 8;
            this.dcEstado.Name = "dcEstado";
            this.dcEstado.ReadOnly = true;
            // 
            // dcPesoBruto
            // 
            this.dcPesoBruto.HeaderText = "PESO BRUTO";
            this.dcPesoBruto.MinimumWidth = 8;
            this.dcPesoBruto.Name = "dcPesoBruto";
            this.dcPesoBruto.ReadOnly = true;
            this.dcPesoBruto.Width = 110;
            // 
            // dcDescuentoHumedo
            // 
            this.dcDescuentoHumedo.HeaderText = "DESC. HÚMEDO";
            this.dcDescuentoHumedo.MinimumWidth = 8;
            this.dcDescuentoHumedo.Name = "dcDescuentoHumedo";
            this.dcDescuentoHumedo.ReadOnly = true;
            this.dcDescuentoHumedo.Width = 130;
            // 
            // dcqqNetos
            // 
            this.dcqqNetos.HeaderText = "QQ NETOS";
            this.dcqqNetos.MinimumWidth = 8;
            this.dcqqNetos.Name = "dcqqNetos";
            this.dcqqNetos.ReadOnly = true;
            this.dcqqNetos.Width = 110;
            // 
            // txtBuscar
            // 
            this.txtBuscar.Location = new System.Drawing.Point(83, 41);
            this.txtBuscar.Margin = new System.Windows.Forms.Padding(2);
            this.txtBuscar.Name = "txtBuscar";
            this.txtBuscar.Size = new System.Drawing.Size(259, 23);
            this.txtBuscar.TabIndex = 128;
            this.txtBuscar.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtBuscar_KeyDown);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Century Gothic", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(25, 43);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(61, 18);
            this.label5.TabIndex = 127;
            this.label5.Text = "Buscar:";
            // 
            // dtpFechaIncial
            // 
            this.dtpFechaIncial.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpFechaIncial.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFechaIncial.Location = new System.Drawing.Point(407, 40);
            this.dtpFechaIncial.Margin = new System.Windows.Forms.Padding(2);
            this.dtpFechaIncial.Name = "dtpFechaIncial";
            this.dtpFechaIncial.Size = new System.Drawing.Size(103, 22);
            this.dtpFechaIncial.TabIndex = 130;
            // 
            // dtpFechaFinal
            // 
            this.dtpFechaFinal.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpFechaFinal.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFechaFinal.Location = new System.Drawing.Point(516, 40);
            this.dtpFechaFinal.Margin = new System.Windows.Forms.Padding(2);
            this.dtpFechaFinal.Name = "dtpFechaFinal";
            this.dtpFechaFinal.Size = new System.Drawing.Size(103, 22);
            this.dtpFechaFinal.TabIndex = 131;
            this.dtpFechaFinal.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dtpFechaFinal_KeyDown);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Century Gothic", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(346, 42);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 18);
            this.label1.TabIndex = 132;
            this.label1.Text = "Fecha:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Century Gothic", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(829, 40);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(79, 18);
            this.label2.TabIndex = 133;
            this.label2.Text = "Cosecha:";
            // 
            // lblQQNetos
            // 
            this.lblQQNetos.AutoSize = true;
            this.lblQQNetos.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblQQNetos.Location = new System.Drawing.Point(1269, 483);
            this.lblQQNetos.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblQQNetos.Name = "lblQQNetos";
            this.lblQQNetos.Size = new System.Drawing.Size(32, 16);
            this.lblQQNetos.TabIndex = 136;
            this.lblQQNetos.Text = "0.00";
            // 
            // lblRecuento
            // 
            this.lblRecuento.AutoSize = true;
            this.lblRecuento.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRecuento.Location = new System.Drawing.Point(25, 483);
            this.lblRecuento.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblRecuento.Name = "lblRecuento";
            this.lblRecuento.Size = new System.Drawing.Size(23, 16);
            this.lblRecuento.TabIndex = 135;
            this.lblRecuento.Text = "---";
            // 
            // cmbCosecha
            // 
            this.cmbCosecha.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCosecha.FormattingEnabled = true;
            this.cmbCosecha.Location = new System.Drawing.Point(913, 38);
            this.cmbCosecha.Name = "cmbCosecha";
            this.cmbCosecha.Size = new System.Drawing.Size(101, 24);
            this.cmbCosecha.TabIndex = 137;
            this.cmbCosecha.SelectedIndexChanged += new System.EventHandler(this.cmbCosecha_SelectedIndexChanged);
            // 
            // rbtnLiquidado
            // 
            this.rbtnLiquidado.AutoSize = true;
            this.rbtnLiquidado.Location = new System.Drawing.Point(723, 40);
            this.rbtnLiquidado.Margin = new System.Windows.Forms.Padding(2);
            this.rbtnLiquidado.Name = "rbtnLiquidado";
            this.rbtnLiquidado.Size = new System.Drawing.Size(98, 20);
            this.rbtnLiquidado.TabIndex = 138;
            this.rbtnLiquidado.TabStop = true;
            this.rbtnLiquidado.Text = "LIQUIDADO";
            this.rbtnLiquidado.UseVisualStyleBackColor = true;
            this.rbtnLiquidado.CheckedChanged += new System.EventHandler(this.rbtnLiquidado_CheckedChanged);
            // 
            // rbtnDeposito
            // 
            this.rbtnDeposito.AutoSize = true;
            this.rbtnDeposito.Location = new System.Drawing.Point(625, 40);
            this.rbtnDeposito.Margin = new System.Windows.Forms.Padding(2);
            this.rbtnDeposito.Name = "rbtnDeposito";
            this.rbtnDeposito.Size = new System.Drawing.Size(88, 20);
            this.rbtnDeposito.TabIndex = 139;
            this.rbtnDeposito.TabStop = true;
            this.rbtnDeposito.Text = "DEPOSITO";
            this.rbtnDeposito.UseVisualStyleBackColor = true;
            this.rbtnDeposito.CheckedChanged += new System.EventHandler(this.rbtnDeposito_CheckedChanged);
            // 
            // btnRecibo
            // 
            this.btnRecibo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(101)))), ((int)(((byte)(158)))));
            this.btnRecibo.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRecibo.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.btnRecibo.Image = ((System.Drawing.Image)(resources.GetObject("btnRecibo.Image")));
            this.btnRecibo.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnRecibo.Location = new System.Drawing.Point(1159, 35);
            this.btnRecibo.Margin = new System.Windows.Forms.Padding(2);
            this.btnRecibo.Name = "btnRecibo";
            this.btnRecibo.Size = new System.Drawing.Size(76, 32);
            this.btnRecibo.TabIndex = 128;
            this.btnRecibo.Text = "      COPIA";
            this.btnRecibo.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnRecibo.UseVisualStyleBackColor = false;
            this.btnRecibo.Click += new System.EventHandler(this.btnRecibo_Click);
            // 
            // BtnBuscar
            // 
            this.BtnBuscar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(101)))), ((int)(((byte)(158)))));
            this.BtnBuscar.Image = ((System.Drawing.Image)(resources.GetObject("BtnBuscar.Image")));
            this.BtnBuscar.Location = new System.Drawing.Point(1017, 34);
            this.BtnBuscar.Margin = new System.Windows.Forms.Padding(2);
            this.BtnBuscar.Name = "BtnBuscar";
            this.BtnBuscar.Size = new System.Drawing.Size(32, 32);
            this.BtnBuscar.TabIndex = 129;
            this.BtnBuscar.UseVisualStyleBackColor = false;
            this.BtnBuscar.Click += new System.EventHandler(this.BtnBuscar_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(1179, 483);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(71, 16);
            this.label4.TabIndex = 140;
            this.label4.Text = "QQ NETOS:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(953, 483);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(102, 16);
            this.label6.TabIndex = 142;
            this.label6.Text = "PESO BRUTO LBS:";
            // 
            // lblQQHumedos
            // 
            this.lblQQHumedos.AutoSize = true;
            this.lblQQHumedos.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblQQHumedos.Location = new System.Drawing.Point(1074, 483);
            this.lblQQHumedos.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblQQHumedos.Name = "lblQQHumedos";
            this.lblQQHumedos.Size = new System.Drawing.Size(32, 16);
            this.lblQQHumedos.TabIndex = 141;
            this.lblQQHumedos.Text = "0.00";
            // 
            // btnLista
            // 
            this.btnLista.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(101)))), ((int)(((byte)(158)))));
            this.btnLista.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLista.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.btnLista.Image = ((System.Drawing.Image)(resources.GetObject("btnLista.Image")));
            this.btnLista.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnLista.Location = new System.Drawing.Point(1243, 35);
            this.btnLista.Margin = new System.Windows.Forms.Padding(2);
            this.btnLista.Name = "btnLista";
            this.btnLista.Size = new System.Drawing.Size(70, 32);
            this.btnLista.TabIndex = 143;
            this.btnLista.Text = "   LISTA";
            this.btnLista.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnLista.UseVisualStyleBackColor = false;
            this.btnLista.Click += new System.EventHandler(this.btnLista_Click);
            // 
            // BtnReimprimirNotaOriginal
            // 
            this.BtnReimprimirNotaOriginal.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(101)))), ((int)(((byte)(158)))));
            this.BtnReimprimirNotaOriginal.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnReimprimirNotaOriginal.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.BtnReimprimirNotaOriginal.Image = ((System.Drawing.Image)(resources.GetObject("BtnReimprimirNotaOriginal.Image")));
            this.BtnReimprimirNotaOriginal.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.BtnReimprimirNotaOriginal.Location = new System.Drawing.Point(1056, 35);
            this.BtnReimprimirNotaOriginal.Margin = new System.Windows.Forms.Padding(2);
            this.BtnReimprimirNotaOriginal.Name = "BtnReimprimirNotaOriginal";
            this.BtnReimprimirNotaOriginal.Size = new System.Drawing.Size(97, 32);
            this.BtnReimprimirNotaOriginal.TabIndex = 144;
            this.BtnReimprimirNotaOriginal.Text = "ORIGINAL";
            this.BtnReimprimirNotaOriginal.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.BtnReimprimirNotaOriginal.UseVisualStyleBackColor = false;
            this.BtnReimprimirNotaOriginal.Click += new System.EventHandler(this.BtnReimprimirNotaOriginal_Click);
            // 
            // Frm_Lista_Nota_Peso
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(244)))), ((int)(((byte)(244)))));
            this.ClientSize = new System.Drawing.Size(1340, 517);
            this.Controls.Add(this.BtnReimprimirNotaOriginal);
            this.Controls.Add(this.btnLista);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.lblQQHumedos);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.btnRecibo);
            this.Controls.Add(this.rbtnLiquidado);
            this.Controls.Add(this.rbtnDeposito);
            this.Controls.Add(this.cmbCosecha);
            this.Controls.Add(this.lblQQNetos);
            this.Controls.Add(this.lblRecuento);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dtpFechaFinal);
            this.Controls.Add(this.dtpFechaIncial);
            this.Controls.Add(this.BtnBuscar);
            this.Controls.Add(this.txtBuscar);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.DgvData);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Frm_Lista_Nota_Peso";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.Frm_Lista_Nota_Peso_Load);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbMinimizar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbSalir)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DgvData)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.DataGridView DgvData;
        private System.Windows.Forms.Button BtnBuscar;
        private System.Windows.Forms.TextBox txtBuscar;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DateTimePicker dtpFechaIncial;
        private System.Windows.Forms.DateTimePicker dtpFechaFinal;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblQQNetos;
        private System.Windows.Forms.Label lblRecuento;
        private System.Windows.Forms.PictureBox pbMinimizar;
        private System.Windows.Forms.PictureBox pbSalir;
        private System.Windows.Forms.ComboBox cmbCosecha;
        private System.Windows.Forms.RadioButton rbtnLiquidado;
        private System.Windows.Forms.RadioButton rbtnDeposito;
        private System.Windows.Forms.DataGridViewTextBoxColumn dcIDNota;
        private System.Windows.Forms.DataGridViewTextBoxColumn DcSocio;
        private System.Windows.Forms.DataGridViewTextBoxColumn dcFinca;
        private System.Windows.Forms.DataGridViewTextBoxColumn DcUbicacionFinca;
        private System.Windows.Forms.DataGridViewTextBoxColumn dcFecha;
        private System.Windows.Forms.DataGridViewTextBoxColumn dcEstado;
        private System.Windows.Forms.DataGridViewTextBoxColumn dcPesoBruto;
        private System.Windows.Forms.DataGridViewTextBoxColumn dcDescuentoHumedo;
        private System.Windows.Forms.DataGridViewTextBoxColumn dcqqNetos;
        private System.Windows.Forms.Button btnRecibo;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lblQQHumedos;
        private System.Windows.Forms.Button btnLista;
        private System.Windows.Forms.Button BtnReimprimirNotaOriginal;
    }
}