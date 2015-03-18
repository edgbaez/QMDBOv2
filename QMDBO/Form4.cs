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
    public partial class Form4 : Form
    {
        public Form4()
        {
            InitializeComponent();
        }

        private void Form4_Load(object sender, EventArgs e)
        {
            DatabaseCrud crud = new DatabaseCrud();
            crud.loadListViewJobs(this.listView1);
        }

        private void listView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            int jobId = Convert.ToInt32(listView1.SelectedItems[0].SubItems[1].Text);
            string jobName = listView1.SelectedItems[0].SubItems[0].Text;

            if (ClassHelper.IsFormAlreadyOpen(typeof(Form1)) == null)
            {
                Form1 childForm = new Form1(jobId);
                childForm.MdiParent = this.MdiParent;
                childForm.Text = jobName;
                childForm.WindowState = FormWindowState.Maximized;
                childForm.Show();
            }
        }

    }
}
