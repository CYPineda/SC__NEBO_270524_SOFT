
namespace SC__NEBO.Formularios.Formularios_de_Menu.Notas_de_Peso
{
    partial class Frm_Recuperar_Nota_Peso
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_Recuperar_Nota_Peso));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.label1 = new System.Windows.Forms.Label();
            this.btnRecuperar_Nota = new System.Windows.Forms.Button();
            this.DgvData = new System.Windows.Forms.DataGridView();
            this.dcIDNota = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DcSocio = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DcUbicacionFinca = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dcFecha = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dcEstado = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ESTADO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dcPesoBruto = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dcDescuentoHumedo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dcqqNetos = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.DgvData)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(3, 374);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(237, 16);
            this.label1.TabIndex = 133;
            this.label1.Text = "Seleciona un registro para recuperarlo.";
            // 
            // btnRecuperar_Nota
            // 
            this.btnRecuperar_Nota.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(101)))), ((int)(((byte)(158)))));
            this.btnRecuperar_Nota.FlatAppearance.BorderSize = 0;
            this.btnRecuperar_Nota.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(157)))), ((int)(((byte)(143)))));
            this.btnRecuperar_Nota.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRecuperar_Nota.Font = new System.Drawing.Font("Trebuchet MS", 10F, System.Drawing.FontStyle.Bold);
            this.btnRecuperar_Nota.ForeColor = System.Drawing.Color.White;
            this.btnRecuperar_Nota.Image = ((System.Drawing.Image)(resources.GetObject("btnRecuperar_Nota.Image")));
            this.btnRecuperar_Nota.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnRecuperar_Nota.Location = new System.Drawing.Point(1034, 357);
            this.btnRecuperar_Nota.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.btnRecuperar_Nota.Name = "btnRecuperar_Nota";
            this.btnRecuperar_Nota.Size = new System.Drawing.Size(163, 33);
            this.btnRecuperar_Nota.TabIndex = 132;
            this.btnRecuperar_Nota.Text = "    RECUPERAR";
            this.btnRecuperar_Nota.UseVisualStyleBackColor = false;
            this.btnRecuperar_Nota.Click += new System.EventHandler(this.btnRecuperar_Nota_Click);
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
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.DgvData.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.DgvData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DgvData.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dcIDNota,
            this.DcSocio,
            this.DcUbicacionFinca,
            this.dcFecha,
            this.dcEstado,
            this.ESTADO,
            this.dcPesoBruto,
            this.dcDescuentoHumedo,
            this.dcqqNetos});
            this.DgvData.Location = new System.Drawing.Point(6, 9);
            this.DgvData.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.DgvData.Name = "DgvData";
            this.DgvData.ReadOnly = true;
            this.DgvData.RowHeadersWidth = 5;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Century Gothic", 10F);
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(101)))), ((int)(((byte)(158)))));
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.White;
            this.DgvData.RowsDefaultCellStyle = dataGridViewCellStyle4;
            this.DgvData.RowTemplate.Height = 28;
            this.DgvData.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.DgvData.Size = new System.Drawing.Size(1191, 338);
            this.DgvData.TabIndex = 131;
            // 
            // dcIDNota
            // 
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dcIDNota.DefaultCellStyle = dataGridViewCellStyle3;
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
            this.dcEstado.HeaderText = "ENTREGADO";
            this.dcEstado.MinimumWidth = 8;
            this.dcEstado.Name = "dcEstado";
            this.dcEstado.ReadOnly = true;
            // 
            // ESTADO
            // 
            this.ESTADO.HeaderText = "ESTADO";
            this.ESTADO.Name = "ESTADO";
            this.ESTADO.ReadOnly = true;
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
            // Frm_Recuperar_Nota_Peso
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1212, 404);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnRecuperar_Nota);
            this.Controls.Add(this.DgvData);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "Frm_Recuperar_Nota_Peso";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "RECUPERAR NOTAS DE PESO";
            this.Load += new System.EventHandler(this.Frm_Recuperar_Nota_Peso_Load);
            ((System.ComponentModel.ISupportInitialize)(this.DgvData)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnRecuperar_Nota;
        private System.Windows.Forms.DataGridView DgvData;
        private System.Windows.Forms.DataGridViewTextBoxColumn dcIDNota;
        private System.Windows.Forms.DataGridViewTextBoxColumn DcSocio;
        private System.Windows.Forms.DataGridViewTextBoxColumn DcUbicacionFinca;
        private System.Windows.Forms.DataGridViewTextBoxColumn dcFecha;
        private System.Windows.Forms.DataGridViewTextBoxColumn dcEstado;
        private System.Windows.Forms.DataGridViewTextBoxColumn ESTADO;
        private System.Windows.Forms.DataGridViewTextBoxColumn dcPesoBruto;
        private System.Windows.Forms.DataGridViewTextBoxColumn dcDescuentoHumedo;
        private System.Windows.Forms.DataGridViewTextBoxColumn dcqqNetos;
    }
}