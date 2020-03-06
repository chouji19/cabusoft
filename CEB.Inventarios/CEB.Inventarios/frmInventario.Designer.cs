namespace CEB.Inventarios
{
    partial class frmInventario
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
            ((System.ComponentModel.ISupportInitialize)(this.dbProductos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.vProductosBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // reportViewer1
            // 
            this.reportViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
            reportDataSource1.Name = "dsProductos";
            reportDataSource1.Value = this.vProductosBindingSource;
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource1);
            this.reportViewer1.LocalReport.ReportEmbeddedResource = "CEB.Inventarios.rptInventario.rdlc";
            this.reportViewer1.Location = new System.Drawing.Point(0, 0);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.Size = new System.Drawing.Size(886, 593);
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
            // frmInventario
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(886, 593);
            this.Controls.Add(this.reportViewer1);
            this.Name = "frmInventario";
            this.Text = "frmInventario";
            this.Load += new System.EventHandler(this.frmInventario_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dbProductos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.vProductosBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        private System.Windows.Forms.BindingSource vProductosBindingSource;
        private dbProductos dbProductos;
        private dbProductosTableAdapters.vProductosTableAdapter vProductosTableAdapter;
    }
}