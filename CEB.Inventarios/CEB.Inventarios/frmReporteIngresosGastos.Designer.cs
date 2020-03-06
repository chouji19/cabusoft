namespace CEB.Inventarios
{
    partial class frmReporteIngresosGastos
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
            this.components = new System.ComponentModel.Container();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource1 = new Microsoft.Reporting.WinForms.ReportDataSource();
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.dbProductos = new CEB.Inventarios.dbProductos();
            this.vProductosBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.vProductosTableAdapter = new CEB.Inventarios.dbProductosTableAdapters.vProductosTableAdapter();
            this.button3 = new System.Windows.Forms.Button();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.txtDpFechaFin = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.txtDpFechaIni = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.dsIngresosGastos = new CEB.Inventarios.dsIngresosGastos();
            this.vIngresosGastosBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.vIngresosGastosTableAdapter = new CEB.Inventarios.dsIngresosGastosTableAdapters.vIngresosGastosTableAdapter();
            ((System.ComponentModel.ISupportInitialize)(this.dbProductos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.vProductosBindingSource)).BeginInit();
            this.tableLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dsIngresosGastos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.vIngresosGastosBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // reportViewer1
            // 
            reportDataSource1.Name = "dsIngresosGastos";
            reportDataSource1.Value = this.vIngresosGastosBindingSource;
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource1);
            this.reportViewer1.LocalReport.ReportEmbeddedResource = "CEB.Inventarios.rptIngresosGastos.rdlc";
            this.reportViewer1.Location = new System.Drawing.Point(3, 78);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.Size = new System.Drawing.Size(901, 549);
            this.reportViewer1.TabIndex = 0;
            // 
            // dbProductos
            // 
            this.dbProductos.DataSetName = "dbProductos";
            this.dbProductos.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // vProductosBindingSource
            // 
            this.vProductosBindingSource.DataMember = "vProductos";
            this.vProductosBindingSource.DataSource = this.dbProductos;
            // 
            // vProductosTableAdapter
            // 
            this.vProductosTableAdapter.ClearBeforeFill = true;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(411, 49);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 36;
            this.button3.Text = "Bucar";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 4;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 26.51757F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 73.48243F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 56F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 133F));
            this.tableLayoutPanel2.Controls.Add(this.txtDpFechaFin, 3, 0);
            this.tableLayoutPanel2.Controls.Add(this.label2, 2, 0);
            this.tableLayoutPanel2.Controls.Add(this.txtDpFechaIni, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel2.Location = new System.Drawing.Point(272, 12);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(354, 31);
            this.tableLayoutPanel2.TabIndex = 35;
            // 
            // txtDpFechaFin
            // 
            this.txtDpFechaFin.CustomFormat = "yyyy-MM-dd";
            this.txtDpFechaFin.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.txtDpFechaFin.Location = new System.Drawing.Point(223, 3);
            this.txtDpFechaFin.Name = "txtDpFechaFin";
            this.txtDpFechaFin.Size = new System.Drawing.Size(109, 20);
            this.txtDpFechaFin.TabIndex = 29;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(167, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(40, 26);
            this.label2.TabIndex = 28;
            this.label2.Text = "Fecha Hasta:";
            // 
            // txtDpFechaIni
            // 
            this.txtDpFechaIni.CustomFormat = "yyyy-MM-dd";
            this.txtDpFechaIni.DropDownAlign = System.Windows.Forms.LeftRightAlignment.Right;
            this.txtDpFechaIni.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.txtDpFechaIni.Location = new System.Drawing.Point(46, 3);
            this.txtDpFechaIni.Name = "txtDpFechaIni";
            this.txtDpFechaIni.Size = new System.Drawing.Size(109, 20);
            this.txtDpFechaIni.TabIndex = 27;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(37, 31);
            this.label1.TabIndex = 0;
            this.label1.Text = "Fecha Desde:";
            // 
            // dsIngresosGastos
            // 
            this.dsIngresosGastos.DataSetName = "dsIngresosGastos";
            this.dsIngresosGastos.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // vIngresosGastosBindingSource
            // 
            this.vIngresosGastosBindingSource.DataMember = "vIngresosGastos";
            this.vIngresosGastosBindingSource.DataSource = this.dsIngresosGastos;
            // 
            // vIngresosGastosTableAdapter
            // 
            this.vIngresosGastosTableAdapter.ClearBeforeFill = true;
            // 
            // frmReporteIngresosGastos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(905, 628);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.tableLayoutPanel2);
            this.Controls.Add(this.reportViewer1);
            this.Name = "frmReporteIngresosGastos";
            this.Text = "frmReporteIngresosGastos";
            this.Load += new System.EventHandler(this.frmReporteIngresosGastos_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dbProductos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.vProductosBindingSource)).EndInit();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dsIngresosGastos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.vIngresosGastosBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        private System.Windows.Forms.BindingSource vProductosBindingSource;
        private dbProductos dbProductos;
        private dbProductosTableAdapters.vProductosTableAdapter vProductosTableAdapter;
        private System.Windows.Forms.BindingSource vIngresosGastosBindingSource;
        private dsIngresosGastos dsIngresosGastos;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.DateTimePicker txtDpFechaFin;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker txtDpFechaIni;
        private System.Windows.Forms.Label label1;
        private dsIngresosGastosTableAdapters.vIngresosGastosTableAdapter vIngresosGastosTableAdapter;
    }
}