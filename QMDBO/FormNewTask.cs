using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QMDBO
{
    public partial class FormNewTask : Form
    {
        DatabaseCrud crud;

        public FormNewTask()
        {
            InitializeComponent();
        }

        private void NewTask_Load(object sender, EventArgs e)
        {
            crud = new DatabaseCrud();
            crud.loadCategory(this.categoryBindingSource);
            DateTime thisDay = DateTime.Now;
            this.textBox1.Text = "Задача " + DatabaseCrud.getMaxjobId() + " от " + thisDay.ToString("g");
            radioButtonNormal.Checked = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int categoryId = Convert.ToInt32(categoryComboBox.SelectedValue);

            if (this.radioButtonProcedure.Checked == true)
            {
                if (ClassHelper.IsFormAlreadyOpen(typeof(Form5)) == null)
                {
                    Form5 childForm = new Form5(categoryId);
                    childForm.MdiParent = this.MdiParent;
                    childForm.Text = this.textBox1.Text;
                    childForm.WindowState = FormWindowState.Maximized;
                    childForm.Show();
                }
            }
            else
            {
                if (ClassHelper.IsFormAlreadyOpen(typeof(Form1)) == null)
                {
                    Form1 childForm = new Form1(categoryId);
                    childForm.MdiParent = this.MdiParent;
                    childForm.Text = this.textBox1.Text;
                    childForm.WindowState = FormWindowState.Maximized;
                    childForm.Show();
                }
            }
            this.Close();
        }
    }
}
