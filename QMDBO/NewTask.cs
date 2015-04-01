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
    public partial class NewTask : Form
    {
        DatabaseCrud crud;

        public NewTask()
        {
            InitializeComponent();
        }

        private void NewTask_Load(object sender, EventArgs e)
        {
            crud = new DatabaseCrud();
            crud.loadCategory(this.categoryBindingSource);
            DateTime thisDay = DateTime.Now;
            this.textBox1.Text = "Задача " + DatabaseCrud.getMaxjobId() + " от " + thisDay.ToString("g");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
