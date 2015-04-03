using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QMDBO
{
    public partial class MDIParent1 : Form
    {
        public MDIParent1()
        {
            InitializeComponent();
        }

        private void ShowNewForm(object sender, EventArgs e)
        {
            if (ClassHelper.IsFormAlreadyOpen(typeof(FormNewTask)) == null)
            {
                FormNewTask childForm = new FormNewTask();
                childForm.MdiParent = this;
                childForm.Show();
            }
        }

        private void OpenFile(object sender, EventArgs e)
        {
            if (ClassHelper.IsFormAlreadyOpen(typeof(Form4)) == null)
            {
                Form4 childForm = new Form4();
                childForm.MdiParent = this;
                childForm.WindowState = FormWindowState.Maximized;
                childForm.Show();
            }
        }


        private void ExitToolsStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void CutToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void CopyToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void PasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void ToolBarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            toolStrip.Visible = toolBarToolStripMenuItem.Checked;
        }

        private void StatusBarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            statusStrip.Visible = statusBarToolStripMenuItem.Checked;
        }

        private void CascadeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.Cascade);
        }

        private void TileVerticalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileVertical);
        }

        private void TileHorizontalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileHorizontal);
        }

        private void ArrangeIconsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.ArrangeIcons);
        }

        private void CloseAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Form childForm in MdiChildren)
            {
                childForm.Close();
            }
        }

        private void optionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ClassHelper.IsFormAlreadyOpen(typeof(Form2)) == null)
            {
                Form2 childForm = new Form2();
                childForm.MdiParent = this;
                childForm.Show();
            }
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutBox1 ab = new AboutBox1();
            ab.ShowDialog();
        }

        private void linksToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ClassHelper.IsFormAlreadyOpen(typeof(Form3)) == null)
            {
                Form3 childForm = new Form3();
                childForm.MdiParent = this;
                childForm.WindowState = FormWindowState.Maximized;
                childForm.Show();
            }
        }

        private void SettingsToolStripButton_Click(object sender, EventArgs e)
        {
            if (ClassHelper.IsFormAlreadyOpen(typeof(Form2)) == null)
            {
                Form2 childForm = new Form2();
                childForm.MdiParent = this;
                childForm.Show();
            }
        }

        private void linksToolStripButton2_Click(object sender, EventArgs e)
        {
            if (ClassHelper.IsFormAlreadyOpen(typeof(Form3)) == null)
            {
                Form3 childForm = new Form3();
                childForm.MdiParent = this;
                childForm.WindowState = FormWindowState.Maximized;
                childForm.Show();
            }
        }

        private void searchToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ProcessStartInfo sInfo = new ProcessStartInfo("http://mazdik.ru/?p=716");
            Process.Start(sInfo);
        }

        private void toolStripStatusLabel_TextChanged(object sender, EventArgs e)
        {
            statusStrip.Refresh();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            if (ClassHelper.IsFormAlreadyOpen(typeof(Form4)) == null)
            {
                Form4 childForm = new Form4();
                childForm.MdiParent = this;
                childForm.WindowState = FormWindowState.Maximized;
                childForm.Show();
            }
        }

    }
}
