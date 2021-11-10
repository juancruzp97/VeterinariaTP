
namespace ReportesVeterinaria
{
    partial class frmReporte
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource12 = new Microsoft.Reporting.WinForms.ReportDataSource();
            this.sPREPORTEATENCIONESBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.cboCliente = new System.Windows.Forms.ComboBox();
            this.cboMascota = new System.Windows.Forms.ComboBox();
            this.chbCliente = new System.Windows.Forms.CheckBox();
            this.chbMascota = new System.Windows.Forms.CheckBox();
            this.btnConsultar = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.dtHasta = new System.Windows.Forms.DateTimePicker();
            this.dtDesde = new System.Windows.Forms.DateTimePicker();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.SP_REPORTE_ATENCIONESBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.Reportes = new ReportesVeterinaria.Reportes();
            this.SP_REPORTE_ATENCIONESTableAdapter = new ReportesVeterinaria.ReportesTableAdapters.SP_REPORTE_ATENCIONESTableAdapter();
            this.rpReporte = new Microsoft.Reporting.WinForms.ReportViewer();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            ((System.ComponentModel.ISupportInitialize)(this.sPREPORTEATENCIONESBindingSource)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SP_REPORTE_ATENCIONESBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Reportes)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // cboCliente
            // 
            this.cboCliente.FormattingEnabled = true;
            this.cboCliente.Location = new System.Drawing.Point(158, 72);
            this.cboCliente.Name = "cboCliente";
            this.cboCliente.Size = new System.Drawing.Size(121, 21);
            this.cboCliente.TabIndex = 20;
            this.cboCliente.SelectionChangeCommitted += new System.EventHandler(this.cboCliente_SelectedIndexChanged);
            // 
            // cboMascota
            // 
            this.cboMascota.FormattingEnabled = true;
            this.cboMascota.Location = new System.Drawing.Point(471, 74);
            this.cboMascota.Name = "cboMascota";
            this.cboMascota.Size = new System.Drawing.Size(121, 21);
            this.cboMascota.TabIndex = 19;
            // 
            // chbCliente
            // 
            this.chbCliente.AutoSize = true;
            this.chbCliente.Location = new System.Drawing.Point(91, 74);
            this.chbCliente.Name = "chbCliente";
            this.chbCliente.Size = new System.Drawing.Size(61, 17);
            this.chbCliente.TabIndex = 18;
            this.chbCliente.Text = "Cliente:";
            this.chbCliente.UseVisualStyleBackColor = true;
            this.chbCliente.CheckedChanged += new System.EventHandler(this.chbCliente_CheckedChanged_1);
            // 
            // chbMascota
            // 
            this.chbMascota.AutoSize = true;
            this.chbMascota.Location = new System.Drawing.Point(395, 76);
            this.chbMascota.Name = "chbMascota";
            this.chbMascota.Size = new System.Drawing.Size(70, 17);
            this.chbMascota.TabIndex = 17;
            this.chbMascota.Text = "Mascota:";
            this.chbMascota.UseVisualStyleBackColor = true;
            this.chbMascota.CheckedChanged += new System.EventHandler(this.chbMascota_CheckedChanged_1);
            // 
            // btnConsultar
            // 
            this.btnConsultar.Location = new System.Drawing.Point(653, 30);
            this.btnConsultar.Name = "btnConsultar";
            this.btnConsultar.Size = new System.Drawing.Size(75, 65);
            this.btnConsultar.TabIndex = 16;
            this.btnConsultar.Text = "Consultar";
            this.btnConsultar.UseVisualStyleBackColor = true;
            this.btnConsultar.Click += new System.EventHandler(this.btnConsultar_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(320, 34);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(69, 13);
            this.label2.TabIndex = 15;
            this.label2.Text = "Fecha hasta:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 34);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(72, 13);
            this.label1.TabIndex = 14;
            this.label1.Text = "Fecha desde:";
            // 
            // dtHasta
            // 
            this.dtHasta.Location = new System.Drawing.Point(395, 30);
            this.dtHasta.Name = "dtHasta";
            this.dtHasta.Size = new System.Drawing.Size(200, 20);
            this.dtHasta.TabIndex = 13;
            // 
            // dtDesde
            // 
            this.dtDesde.Location = new System.Drawing.Point(91, 30);
            this.dtDesde.Name = "dtDesde";
            this.dtDesde.Size = new System.Drawing.Size(200, 20);
            this.dtDesde.TabIndex = 12;
            this.dtDesde.Value = new System.DateTime(2021, 1, 9, 15, 57, 0, 0);
            // 
            // groupBox1
            // 
            this.groupBox1.AutoSize = true;
            this.groupBox1.Controls.Add(this.dtDesde);
            this.groupBox1.Controls.Add(this.dtHasta);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.cboCliente);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.cboMascota);
            this.groupBox1.Controls.Add(this.btnConsultar);
            this.groupBox1.Controls.Add(this.chbCliente);
            this.groupBox1.Controls.Add(this.chbMascota);
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(793, 114);
            this.groupBox1.TabIndex = 24;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Filtros:";
            // 
            // SP_REPORTE_ATENCIONESBindingSource
            // 
            this.SP_REPORTE_ATENCIONESBindingSource.DataMember = "SP_REPORTE_ATENCIONES";
            this.SP_REPORTE_ATENCIONESBindingSource.DataSource = this.Reportes;
            // 
            // Reportes
            // 
            this.Reportes.DataSetName = "Reportes";
            this.Reportes.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // SP_REPORTE_ATENCIONESTableAdapter
            // 
            this.SP_REPORTE_ATENCIONESTableAdapter.ClearBeforeFill = true;
            // 
            // rpReporte
            // 
            this.rpReporte.AutoSize = true;
            this.rpReporte.Dock = System.Windows.Forms.DockStyle.Fill;
            reportDataSource12.Name = "DataSet1";
            reportDataSource12.Value = this.sPREPORTEATENCIONESBindingSource;
            this.rpReporte.LocalReport.DataSources.Add(reportDataSource12);
            this.rpReporte.LocalReport.ReportEmbeddedResource = "ReportesVeterinaria.IReporte.rdlc";
            this.rpReporte.Location = new System.Drawing.Point(3, 16);
            this.rpReporte.Name = "rpReporte";
            this.rpReporte.ServerReport.BearerToken = null;
            this.rpReporte.Size = new System.Drawing.Size(787, 298);
            this.rpReporte.TabIndex = 25;
            // 
            // groupBox2
            // 
            this.groupBox2.AutoSize = true;
            this.groupBox2.Controls.Add(this.rpReporte);
            this.groupBox2.Location = new System.Drawing.Point(3, 121);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(793, 317);
            this.groupBox2.TabIndex = 26;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "groupBox2";
            // 
            // frmReporte
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "frmReporte";
            this.Text = "Reportes";
            ((System.ComponentModel.ISupportInitialize)(this.sPREPORTEATENCIONESBindingSource)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SP_REPORTE_ATENCIONESBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Reportes)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ComboBox cboCliente;
        private System.Windows.Forms.ComboBox cboMascota;
        private System.Windows.Forms.CheckBox chbCliente;
        private System.Windows.Forms.CheckBox chbMascota;
        private System.Windows.Forms.Button btnConsultar;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dtHasta;
        private System.Windows.Forms.DateTimePicker dtDesde;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.BindingSource SP_REPORTE_ATENCIONESBindingSource;
        private Reportes Reportes;
        private ReportesTableAdapters.SP_REPORTE_ATENCIONESTableAdapter SP_REPORTE_ATENCIONESTableAdapter;
        private Microsoft.Reporting.WinForms.ReportViewer rpReporte;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.BindingSource sPREPORTEATENCIONESBindingSource;
    }
}

