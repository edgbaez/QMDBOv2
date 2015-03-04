using System;
using System.Windows.Forms;

namespace QMDBO
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.Thread = this.numericUpDown1.Value;
            Properties.Settings.Default.Save();
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            this.numericUpDown1.Value = Properties.Settings.Default.Thread;
        }
    }
}
