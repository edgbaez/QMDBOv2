using System;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using System.Threading;
using System.Data.Entity;
using System.Collections.Generic;

namespace QMDBO
{
    public partial class Form1 : Form
    {
        DatabaseCrud crud;
        private List<ClassLinks> linksCollection;
        MDIParent1 frm;
        private string formName;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            crud = new DatabaseCrud();
            crud.loadCategory(this.categoryBindingSource);
            LoadDataIntoGrid();
            ClassHelper.PopulateComboBox(comboBox1);
            frm = this.MdiParent as MDIParent1;
            formName = this.Text;
        }

        private void LoadDataIntoGrid()
        {
            crud.loadDataIntoGrid(this.linksCollection, this.categoryComboBox, this.dataGridView1, this.frm);
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
            this.Text = formName + " - " + e.ProgressPercentage.ToString() + "%";
            if (frm != null)
            {
                frm.toolStripStatusLabel.Text = e.ProgressPercentage.ToString() + "%";
            }
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            this.buttons_Enable();
            this.Text = formName + " - " + ClassHelper.CompletedText(e);
            MDIParent1 frm = this.MdiParent as MDIParent1;
            if (frm != null)
            {
                frm.toolStripStatusLabel.Text = ClassHelper.CompletedText(e);
            }
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
            this.buttons_Disable();
            crud.saveJob(this.dataGridView1, this.frm, this.formName);
            this.buttons_Enable();
        }

        private void ClearToolStripButton_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow myRow in dataGridView1.Rows)
            {
                myRow.Cells[7].Value = null;
                myRow.Cells[8].Value = null;
                myRow.Cells[9].Value = null;
                myRow.Cells[10].Value = null;
            }
        }

        private void dataGridView1_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            Type valType = dataGridView1.Columns[e.ColumnIndex].ValueType;
            if (valType != typeof(bool))
            {
                string strColumnName = dataGridView1.Columns[e.ColumnIndex].Name;
                SortOrder strSortOrder = getSortOrder(e.ColumnIndex);

                if (strSortOrder == SortOrder.Ascending)
                {
                    linksCollection = linksCollection.OrderBy(x => typeof(ClassLinks).GetProperty(strColumnName).GetValue(x, null)).ToList();
                }
                else
                {
                    linksCollection = linksCollection.OrderByDescending(x => typeof(ClassLinks).GetProperty(strColumnName).GetValue(x, null)).ToList();
                }
                dataGridView1.DataSource = linksCollection;
                dataGridView1.Columns[e.ColumnIndex].HeaderCell.SortGlyphDirection = strSortOrder;
            }
        }

        private SortOrder getSortOrder(int columnIndex)
        {
            if (dataGridView1.Columns[columnIndex].HeaderCell.SortGlyphDirection == SortOrder.None ||
                dataGridView1.Columns[columnIndex].HeaderCell.SortGlyphDirection == SortOrder.Descending)
            {
                dataGridView1.Columns[columnIndex].HeaderCell.SortGlyphDirection = SortOrder.Ascending;
                return SortOrder.Ascending;
            }
            else
            {
                dataGridView1.Columns[columnIndex].HeaderCell.SortGlyphDirection = SortOrder.Descending;
                return SortOrder.Descending;
            }
        }

        private void categoryComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadDataIntoGrid();
        }

        private void ExpToolStripButton_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            saveFileDialog.Filter = "CSV-файлы (*.csv)|*.csv|Текстовые файлы (*.txt)|*.txt|Все файлы (*.*)|*.*";
            if (saveFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                string FileName = saveFileDialog.FileName;
                ClassHelper.writeCSV(dataGridView1, FileName);
            }
        }

    }
}
