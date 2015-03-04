using System;
using System.Windows.Forms;
using System.ComponentModel;

namespace QMDBO
{
    public class ClassOracleUpdate
    {
        private int start;
        private int end;
        private DataGridView dataGridView_temp;
        private string sSQL;
        private BackgroundWorker worker;
        private DoWorkEventArgs e;
        private int count;
        public string obj_name;
        public int typeExecute;

        public ClassOracleUpdate(int start, int end, DataGridView dataGridView, string sSQL, BackgroundWorker worker, DoWorkEventArgs e)
        {
            this.start = start;
            this.end = end;
            this.dataGridView_temp = dataGridView;
            this.sSQL = sSQL;
            this.worker = worker;
            this.e = e;
        }

        public ClassOracleUpdate(string sSQL)
        {
            this.sSQL = sSQL;
        }

        public void toOracle(DataGridViewRow row, string sSQL)
        {
            ClassOracleConnect ora = new ClassOracleConnect();
            string ConnectionString = ora.OracleConnString(
                (row.Cells[2].Value ?? String.Empty).ToString(),
                (row.Cells[3].Value ?? String.Empty).ToString(),
                (row.Cells[4].Value ?? String.Empty).ToString(),
                (row.Cells[5].Value ?? String.Empty).ToString(),
                (row.Cells[6].Value ?? String.Empty).ToString()
                );
                string[] obj_status = ora.OracleQuery(ConnectionString, sSQL, typeExecute, obj_name);
                row.Cells[7].Value = obj_status[0];
                row.Cells[8].Value = obj_status[1];
                row.Cells[9].Value = obj_status[2];
                row.Cells[10].Value = obj_status[3];
        }

        public void go()
        {
            int i = 0;
            count = dataGridView_temp.Rows.Count;

            for (i = this.start; i < this.end; i++)
            {
                if (worker.CancellationPending == true)
                {
                    e.Cancel = true;
                    break;
                }
                else
                {
                    this.toOracle(this.dataGridView_temp.Rows[i], this.sSQL);
                    worker.ReportProgress((100 * ++ClassWork.progress)/this.count);
                }
            }
        }

    }
}
