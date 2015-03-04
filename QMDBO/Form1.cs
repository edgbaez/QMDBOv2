using System;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using System.Threading;

namespace QMDBO
{
    public partial class Form1 : Form
    {
        public string path = "links.csv";

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = ClassLinks.LoadLinksListFromFile(path);
            ClassHelper.PopulateComboBox(comboBox1);
            label3.Text = "Загружен файл " + path;
        }

        private void buttons_Disable()
        {
            button1.Visible = false;
            button3.Visible = false;
            button4.Visible = true;
            progressBar1.Visible = true;
            progressBar1.Value = 0;
            textBox1.Enabled = false;
            menuStrip1.Enabled = false;
            comboBox1.Enabled = false;
        }

        private void buttons_Enable()
        {
            button1.Visible = true;
            button3.Visible = true;
            button4.Visible = false;
            progressBar1.Visible = false;
            progressBar1.Value = 0;
            textBox1.Enabled = true;
            menuStrip1.Enabled = true;
            comboBox1.Enabled = true;
        }

        /* Выбранные */
        private void button1_Click(object sender, EventArgs e)
        {
            int comboBox1_value = Convert.ToInt32(comboBox1.SelectedValue ?? 1);
            if (ClassHelper.QuestionYesNoStart(richTextBox1.Text))
            {
                this.buttons_Disable();
                ClassWork cw = new ClassWork(richTextBox1.Text, textBox1.Text, dataGridView1, ClassWork.ONE, comboBox1_value);
                backgroundWorker1.RunWorkerAsync(cw);
            }
        }

        /* Все */
        private void button3_Click(object sender, EventArgs e)
        {
            int comboBox1_value = Convert.ToInt32(comboBox1.SelectedValue ?? 1);
            if (ClassHelper.QuestionYesNoStart(richTextBox1.Text))
            {
                this.buttons_Disable();
                ClassWork cw = new ClassWork(richTextBox1.Text, textBox1.Text, dataGridView1, ClassWork.ALL, comboBox1_value);
                backgroundWorker1.RunWorkerAsync(cw);
            }
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            ClassWork cw = (ClassWork)e.Argument;
            cw.start_work(backgroundWorker1, e);
        }

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBar1.Value = e.ProgressPercentage;
            this.Text = e.ProgressPercentage.ToString() + "%";
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            this.buttons_Enable();
            this.Text = ClassHelper.CompletedText(e);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (backgroundWorker1.WorkerSupportsCancellation == true)
            {
                backgroundWorker1.CancelAsync();
            }
        }

        void richTextBox1_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Copy;
        }

        void richTextBox1_DragDrop(object sender, DragEventArgs e)
        {
            ClassHelper.DragDropOpenFile(richTextBox1, e);
        }

        private void openToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            richTextBox1.Clear();
            richTextBox1.Text = ClassOpenFile.OpenFile();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ClassHelper.IsFormAlreadyOpen(typeof(Form2)) == null)
            {
                Form2 fm2 = new Form2();
                fm2.ShowDialog();
            }
        }

        private void clearStatusToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow myRow in dataGridView1.Rows)
            {
                myRow.Cells[6].Value = null;
                myRow.Cells[7].Value = null;
                myRow.Cells[8].Value = null;
                myRow.Cells[9].Value = null;
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ClassHelper.writeCSV(dataGridView1, "result.csv");
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutBox1 ab = new AboutBox1();
            ab.ShowDialog();
        }

        // end class
    }
}
