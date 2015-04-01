namespace QMDBO
{
    partial class FormNewTask
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
            System.Windows.Forms.Label categoryIdLabel;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormNewTask));
            this.categoryComboBox = new System.Windows.Forms.ComboBox();
            this.categoryBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.radioButtonProcedure = new System.Windows.Forms.RadioButton();
            this.radioButtonNormal = new System.Windows.Forms.RadioButton();
            categoryIdLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.categoryBindingSource)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // categoryIdLabel
            // 
            categoryIdLabel.AutoSize = true;
            categoryIdLabel.Location = new System.Drawing.Point(28, 69);
            categoryIdLabel.Name = "categoryIdLabel";
            categoryIdLabel.Size = new System.Drawing.Size(60, 13);
            categoryIdLabel.TabIndex = 1;
            categoryIdLabel.Text = "Категория";
            // 
            // categoryComboBox
            // 
            this.categoryComboBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.categoryBindingSource, "CategoryId", true));
            this.categoryComboBox.DataSource = this.categoryBindingSource;
            this.categoryComboBox.DisplayMember = "Name";
            this.categoryComboBox.FormattingEnabled = true;
            this.categoryComboBox.Location = new System.Drawing.Point(22, 85);
            this.categoryComboBox.Name = "categoryComboBox";
            this.categoryComboBox.Size = new System.Drawing.Size(241, 21);
            this.categoryComboBox.TabIndex = 2;
            this.categoryComboBox.ValueMember = "CategoryId";
            // 
            // categoryBindingSource
            // 
            this.categoryBindingSource.DataSource = typeof(QMDBO.Category);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(22, 36);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(241, 20);
            this.textBox1.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(28, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(95, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Название задачи";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(94, 177);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 5;
            this.button1.Text = "ОК";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(188, 177);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 6;
            this.button2.Text = "Отмена";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.radioButtonProcedure);
            this.groupBox1.Controls.Add(this.radioButtonNormal);
            this.groupBox1.Location = new System.Drawing.Point(22, 112);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(241, 43);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Режим";
            // 
            // radioButtonProcedure
            // 
            this.radioButtonProcedure.AutoSize = true;
            this.radioButtonProcedure.Location = new System.Drawing.Point(140, 19);
            this.radioButtonProcedure.Name = "radioButtonProcedure";
            this.radioButtonProcedure.Size = new System.Drawing.Size(80, 17);
            this.radioButtonProcedure.TabIndex = 1;
            this.radioButtonProcedure.TabStop = true;
            this.radioButtonProcedure.Text = "Процедура";
            this.radioButtonProcedure.UseVisualStyleBackColor = true;
            // 
            // radioButtonNormal
            // 
            this.radioButtonNormal.AutoSize = true;
            this.radioButtonNormal.Location = new System.Drawing.Point(6, 19);
            this.radioButtonNormal.Name = "radioButtonNormal";
            this.radioButtonNormal.Size = new System.Drawing.Size(72, 17);
            this.radioButtonNormal.TabIndex = 0;
            this.radioButtonNormal.TabStop = true;
            this.radioButtonNormal.Text = "Обычный";
            this.radioButtonNormal.UseVisualStyleBackColor = true;
            // 
            // FormNewTask
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 212);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(categoryIdLabel);
            this.Controls.Add(this.categoryComboBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(300, 250);
            this.Name = "FormNewTask";
            this.Text = "Новая задача";
            this.Load += new System.EventHandler(this.NewTask_Load);
            ((System.ComponentModel.ISupportInitialize)(this.categoryBindingSource)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.BindingSource categoryBindingSource;
        private System.Windows.Forms.ComboBox categoryComboBox;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton radioButtonProcedure;
        private System.Windows.Forms.RadioButton radioButtonNormal;
    }
}