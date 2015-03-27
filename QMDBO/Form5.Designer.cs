namespace QMDBO
{
    partial class Form5
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
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.inDataGridView = new System.Windows.Forms.DataGridView();
            this.InParam = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.InType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.InValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.outDataGridView = new System.Windows.Forms.DataGridView();
            this.OutParam = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.OutType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.OutValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.linksDataGridView = new System.Windows.Forms.DataGridView();
            this.resultsDataGridView = new System.Windows.Forms.DataGridView();
            this.buttonStart = new System.Windows.Forms.Button();
            this.buttonStartAll = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.inDataGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.outDataGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.linksDataGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.resultsDataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(136, 12);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(218, 20);
            this.textBox1.TabIndex = 0;
            // 
            // inDataGridView
            // 
            this.inDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.inDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.InParam,
            this.InType,
            this.InValue});
            this.inDataGridView.Location = new System.Drawing.Point(12, 38);
            this.inDataGridView.Name = "inDataGridView";
            this.inDataGridView.Size = new System.Drawing.Size(343, 150);
            this.inDataGridView.TabIndex = 1;
            // 
            // InParam
            // 
            this.InParam.HeaderText = "Param";
            this.InParam.Name = "InParam";
            // 
            // InType
            // 
            this.InType.HeaderText = "Type";
            this.InType.Name = "InType";
            // 
            // InValue
            // 
            this.InValue.HeaderText = "Value";
            this.InValue.Name = "InValue";
            // 
            // outDataGridView
            // 
            this.outDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.outDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.OutParam,
            this.OutType,
            this.OutValue});
            this.outDataGridView.Location = new System.Drawing.Point(369, 38);
            this.outDataGridView.Name = "outDataGridView";
            this.outDataGridView.Size = new System.Drawing.Size(343, 150);
            this.outDataGridView.TabIndex = 2;
            // 
            // OutParam
            // 
            this.OutParam.HeaderText = "Param";
            this.OutParam.Name = "OutParam";
            // 
            // OutType
            // 
            this.OutType.HeaderText = "Type";
            this.OutType.Name = "OutType";
            // 
            // OutValue
            // 
            this.OutValue.HeaderText = "Value";
            this.OutValue.Name = "OutValue";
            // 
            // linksDataGridView
            // 
            this.linksDataGridView.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.linksDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.linksDataGridView.Location = new System.Drawing.Point(12, 201);
            this.linksDataGridView.Name = "linksDataGridView";
            this.linksDataGridView.Size = new System.Drawing.Size(700, 140);
            this.linksDataGridView.TabIndex = 3;
            // 
            // resultsDataGridView
            // 
            this.resultsDataGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.resultsDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.resultsDataGridView.Location = new System.Drawing.Point(12, 352);
            this.resultsDataGridView.Name = "resultsDataGridView";
            this.resultsDataGridView.Size = new System.Drawing.Size(700, 140);
            this.resultsDataGridView.TabIndex = 4;
            // 
            // buttonStart
            // 
            this.buttonStart.Location = new System.Drawing.Point(552, 9);
            this.buttonStart.Name = "buttonStart";
            this.buttonStart.Size = new System.Drawing.Size(75, 23);
            this.buttonStart.TabIndex = 5;
            this.buttonStart.Text = "Start";
            this.buttonStart.UseVisualStyleBackColor = true;
            this.buttonStart.Click += new System.EventHandler(this.buttonStart_Click);
            // 
            // buttonStartAll
            // 
            this.buttonStartAll.Location = new System.Drawing.Point(633, 9);
            this.buttonStartAll.Name = "buttonStartAll";
            this.buttonStartAll.Size = new System.Drawing.Size(75, 23);
            this.buttonStartAll.TabIndex = 6;
            this.buttonStartAll.Text = "StartAll";
            this.buttonStartAll.UseVisualStyleBackColor = true;
            // 
            // Form5
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(719, 502);
            this.Controls.Add(this.buttonStartAll);
            this.Controls.Add(this.buttonStart);
            this.Controls.Add(this.resultsDataGridView);
            this.Controls.Add(this.linksDataGridView);
            this.Controls.Add(this.outDataGridView);
            this.Controls.Add(this.inDataGridView);
            this.Controls.Add(this.textBox1);
            this.MinimumSize = new System.Drawing.Size(735, 540);
            this.Name = "Form5";
            this.Text = "Выполнить процедуру";
            this.Load += new System.EventHandler(this.Form5_Load);
            ((System.ComponentModel.ISupportInitialize)(this.inDataGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.outDataGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.linksDataGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.resultsDataGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.DataGridView inDataGridView;
        private System.Windows.Forms.DataGridViewTextBoxColumn InParam;
        private System.Windows.Forms.DataGridViewTextBoxColumn InType;
        private System.Windows.Forms.DataGridViewTextBoxColumn InValue;
        private System.Windows.Forms.DataGridView outDataGridView;
        private System.Windows.Forms.DataGridViewTextBoxColumn OutParam;
        private System.Windows.Forms.DataGridViewTextBoxColumn OutType;
        private System.Windows.Forms.DataGridViewTextBoxColumn OutValue;
        private System.Windows.Forms.DataGridView linksDataGridView;
        private System.Windows.Forms.DataGridView resultsDataGridView;
        private System.Windows.Forms.Button buttonStart;
        private System.Windows.Forms.Button buttonStartAll;
    }
}