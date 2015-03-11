using System;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using System.Threading;
using System.Data.Entity;

namespace QMDBO
{
    public partial class Form1 : Form
    {
        DatabaseContext _context;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            _context = new DatabaseContext();
            _context.Categories.Load();
            this.categoryBindingSource.DataSource = _context.Categories.Local.ToBindingList();
            ClassHelper.PopulateComboBox(comboBox1);
        }

        private void buttons_Disable()
        {
            button1.Visible = false;
            button3.Visible = false;
            button4.Visible = true;
            progressBar1.Visible = true;
            progressBar1.Value = 0;
            textBox1.Enabled = false;
            toolStrip1.Enabled = false;
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
            toolStrip1.Enabled = true;
            comboBox1.Enabled = true;
        }

        /* Выбранные */
        private void button1_Click(object sender, EventArgs e)
        {
            int comboBox1_value = Convert.ToInt32(comboBox1.SelectedValue ?? 1);
            if (ClassHelper.QuestionYesNoStart(richTextBox1.Text))
            {
                this.buttons_Disable();
                ClassWork cw = new ClassWork(richTextBox1.Text, textBox1.Text, linksDataGridView, ClassWork.ONE, comboBox1_value);
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
                ClassWork cw = new ClassWork(richTextBox1.Text, textBox1.Text, linksDataGridView, ClassWork.ALL, comboBox1_value);
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

        private void openToolStripButton_Click(object sender, EventArgs e)
        {
            richTextBox1.Clear();
            richTextBox1.Text = ClassOpenFile.OpenFile();
        }

        private void saveToolStripButton_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            saveFileDialog.Filter = "Текстовые файлы (*.txt)|*.txt|CSV-файлы (*.csv)|*.csv|Все файлы (*.*)|*.*";
            if (saveFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                string FileName = saveFileDialog.FileName;
                ClassHelper.writeCSV(linksDataGridView, FileName);
            }
        }

        private void ClearToolStripButton_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow myRow in linksDataGridView.Rows)
            {
                myRow.Cells[7].Value = null;
                myRow.Cells[8].Value = null;
                myRow.Cells[9].Value = null;
                myRow.Cells[10].Value = null;
            }
        }

        // end class
    }
}
