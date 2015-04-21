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
    public partial class Form5 : Form
    {
        DatabaseCrud crud;
        MDIParent1 frm;
        private List<ClassLinks> linksCollection;
        private int categoryId;
        private int jobId;
        private DataTable table = new DataTable();
        private BindingSource bindingSource1 = new BindingSource();
        private string formName;
        Stopwatch timer;

        public Form5(int categoryId, int jobId = 0)
        {
            InitializeComponent();
            this.categoryId = categoryId;
            this.jobId = jobId;
        }

        private void Form5_Load(object sender, EventArgs e)
        {
            crud = new DatabaseCrud();
            formName = this.Text;
            table.Columns.Add("name");
            table.Columns.Add("linkId");
            table.Columns.Add("error");
            table.RowChanged += new DataRowChangeEventHandler(Row_Changed);
            bindingSource1.DataSource = table;
            resultsDataGridView.DataSource = bindingSource1;
            resultsDataGridView.Columns["linkId"].Visible = false;
            if (this.jobId > 0)
            {
                crud.loadJobProcedure(this.jobId, this.textBox1, inDataGridView, outDataGridView, this.table);
            }
            ClassHelper.dridComboBoxOracleDbType(this.InType);
            ClassHelper.dridComboBoxOracleDbType(this.OutType);
            frm = this.MdiParent as MDIParent1;
            linksCollection = new List<ClassLinks>();
            crud.loadLinks(this.categoryId, this.linksDataGridView, this.frm);
            timer = new Stopwatch();
        }

        private void Row_Changed(object sender, DataRowChangeEventArgs e)
        {
            //this.resultsDataGridView.Invalidate();
            this.resultsDataGridView.Invoke(new MethodInvoker(() =>
            {
                resultsDataGridView.Refresh();
            })); ;
        }


        private List<ParametersOracle> createInParamsList()
        {
            List<ParametersOracle> inParamsList = new List<ParametersOracle>();
            foreach (DataGridViewRow inRow in inDataGridView.Rows)
            {
                if (inRow.Cells[0].Value != null)
                {
                    ParametersOracle inParam = new ParametersOracle();
                    inParam.name = (inRow.Cells[0].Value ?? String.Empty).ToString();
                    inParam.typeName = (inRow.Cells[1].Value ?? String.Empty).ToString();
                    inParam.value = (inRow.Cells[2].Value ?? String.Empty).ToString();
                    inParamsList.Add(inParam);
                    inParam = null;
                }
            }
            return inParamsList;
        }

        private List<ParametersOracle> createOutParamsList()
        {
            List<ParametersOracle> outParamsList = new List<ParametersOracle>();
            foreach (DataGridViewRow outRow in outDataGridView.Rows)
            {
                if (outRow.Cells[0].Value != null)
                {
                    ParametersOracle outParam = new ParametersOracle();
                    outParam.name = (outRow.Cells[0].Value ?? String.Empty).ToString();
                    outParam.typeName = (outRow.Cells[1].Value ?? String.Empty).ToString();
                    outParam.size = ClassHelper.TryToInt32(outRow.Cells[2].Value);
                    outParamsList.Add(outParam);
                    outParam = null;
                }
            }
            return outParamsList;
        }

        private void saveToolStripButton_Click(object sender, EventArgs e)
        {
            List<ParametersOracle> inParamsList = createInParamsList();
            List<ParametersOracle> outParamsList = createOutParamsList();
            crud.saveJobProcedure(this.frm, this.Text, this.categoryId, this.textBox1.Text, inParamsList, outParamsList);
            crud.saveValues(this.table, this.jobId);
        }

        private void expToolStripButton_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            saveFileDialog.Filter = "CSV-файлы (*.csv)|*.csv|Текстовые файлы (*.txt)|*.txt|Все файлы (*.*)|*.*";
            if (saveFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                string FileName = saveFileDialog.FileName;
                ClassHelper.writeCSV(resultsDataGridView, FileName);
            }
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            ClassWorkProcedure cw = (ClassWorkProcedure)e.Argument;
            cw.start_work(backgroundWorker1, e);
        }

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBar1.Value = e.ProgressPercentage;
            this.Text = formName + " - " + e.ProgressPercentage.ToString() + "%";
            if (frm != null)
            {
                frm.toolStripStatusLabel.Text = "Выполнено: " + e.ProgressPercentage.ToString() + "%";
            }
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            timer.Stop();
            this.buttons_Enable();
            this.Text = formName + " - " + ClassHelper.CompletedText(e);
            string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                timer.Elapsed.Hours, timer.Elapsed.Minutes, timer.Elapsed.Seconds,
                timer.Elapsed.Milliseconds / 10);
            if (frm != null)
            {
                frm.toolStripStatusLabel.Text = ClassHelper.CompletedText(e) + " Затраченное время: " + elapsedTime;
            }
        }

        private void startJob(int type) {
                List<ParametersOracle> inParamsList = createInParamsList();
                List<ParametersOracle> outParamsList = createOutParamsList();

                List<string> columnNames = new List<string>();
                columnNames = (from dc in table.Columns.Cast<DataColumn>()
                               select dc.ColumnName).ToList();

                foreach (ParametersOracle outParam in outParamsList)
                {
                    if (!columnNames.Contains(outParam.name))
                    {
                        table.Columns.Add(outParam.name);
                    }
                }
                ClassWorkProcedure cw = new ClassWorkProcedure(this.linksDataGridView, this.table, inParamsList, outParamsList, this.textBox1.Text, type);
                backgroundWorker1.RunWorkerAsync(cw);
        }

        private void buttons_Disable()
        {
            progressBar1.Visible = true;
            buttonStop.Visible = true;
            progressBar1.Value = 0;
            textBox1.Enabled = false;
            toolStrip1.Enabled = false;
            buttonStart.Enabled = false;
            buttonStartAll.Enabled = false;
        }

        private void buttons_Enable()
        {
            progressBar1.Visible = false;
            buttonStop.Visible = false;
            progressBar1.Value = 0;
            textBox1.Enabled = true;
            toolStrip1.Enabled = true;
            buttonStart.Enabled = true;
            buttonStartAll.Enabled = true;
        }

        private void buttonStart_Click(object sender, EventArgs e)
        {
            if (ClassHelper.QuestionYesNoStart(textBox1.Text))
            {
                timer.Reset();
                timer.Start();
                this.buttons_Disable();
                startJob(ClassWorkProcedure.SELECTED);
            }
        }

        private void buttonStartAll_Click(object sender, EventArgs e)
        {
            if (ClassHelper.QuestionYesNoStart(textBox1.Text))
            {
                timer.Reset();
                timer.Start();
                this.buttons_Disable();
                startJob(ClassWorkProcedure.ALL);
            }
        }

        private void buttonStop_Click(object sender, EventArgs e)
        {
            if (backgroundWorker1.WorkerSupportsCancellation == true)
            {
                backgroundWorker1.CancelAsync();
            }
            timer.Stop();
        }

    }
}
