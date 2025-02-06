namespace Prueba_de_editor_PDF
{
    partial class Form_Ejemplo2
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
            this.dataGridViewProductos = new System.Windows.Forms.DataGridView();
            this.btnGenerarFactura = new System.Windows.Forms.Button();
            this.webBrowserPreview = new System.Windows.Forms.WebBrowser();
            this.btnExportarPDF = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewProductos)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridViewProductos
            // 
            this.dataGridViewProductos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewProductos.Location = new System.Drawing.Point(12, 12);
            this.dataGridViewProductos.Name = "dataGridViewProductos";
            this.dataGridViewProductos.Size = new System.Drawing.Size(388, 426);
            this.dataGridViewProductos.TabIndex = 0;
            // 
            // btnGenerarFactura
            // 
            this.btnGenerarFactura.Location = new System.Drawing.Point(419, 415);
            this.btnGenerarFactura.Name = "btnGenerarFactura";
            this.btnGenerarFactura.Size = new System.Drawing.Size(75, 23);
            this.btnGenerarFactura.TabIndex = 1;
            this.btnGenerarFactura.Text = "GenerarFactura";
            this.btnGenerarFactura.UseVisualStyleBackColor = true;
            this.btnGenerarFactura.Click += new System.EventHandler(this.btnGenerarFactura_Click);
            // 
            // webBrowserPreview
            // 
            this.webBrowserPreview.Location = new System.Drawing.Point(419, 12);
            this.webBrowserPreview.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowserPreview.Name = "webBrowserPreview";
            this.webBrowserPreview.Size = new System.Drawing.Size(369, 397);
            this.webBrowserPreview.TabIndex = 2;
            // 
            // btnExportarPDF
            // 
            this.btnExportarPDF.Enabled = false;
            this.btnExportarPDF.Location = new System.Drawing.Point(500, 415);
            this.btnExportarPDF.Name = "btnExportarPDF";
            this.btnExportarPDF.Size = new System.Drawing.Size(75, 23);
            this.btnExportarPDF.TabIndex = 3;
            this.btnExportarPDF.Text = "ExportarPDF";
            this.btnExportarPDF.UseVisualStyleBackColor = true;
            this.btnExportarPDF.Click += new System.EventHandler(this.btnExportarPDF_Click);
            // 
            // Form_Ejemplo2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnExportarPDF);
            this.Controls.Add(this.webBrowserPreview);
            this.Controls.Add(this.btnGenerarFactura);
            this.Controls.Add(this.dataGridViewProductos);
            this.Name = "Form_Ejemplo2";
            this.Text = "Form_Ejemplo2";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewProductos)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridViewProductos;
        private System.Windows.Forms.Button btnGenerarFactura;
        private System.Windows.Forms.WebBrowser webBrowserPreview;
        private System.Windows.Forms.Button btnExportarPDF;
    }
}