using System;
using System.Windows.Forms;
using System.Threading;
using System.ComponentModel;

namespace QMDBO
{
    class ClassWork
    {
        private string sSQL;
        private string obj_name;
        private DataGridView dataGridView;
        private int type;
        private int typeExecute;
        public const int ONE = 1;
        public const int ALL = 2;
        public static int progress;

        public ClassWork(string sSQL, string obj_name, DataGridView dataGridView, int type, int typeExecute)
        {
            this.sSQL = sSQL;
            this.obj_name = obj_name;
            this.dataGridView = dataGridView;
            this.type = type;
            this.typeExecute = typeExecute;
        }

        private int count_selected()
        {
            int i = 0;
            foreach (DataGridViewRow row in dataGridView.Rows)
            {
                if (Convert.ToBoolean(row.Cells[0].Value))
                {
                    i++;
                }
            }
            return i;
        }

        public void start_work(BackgroundWorker worker, DoWorkEventArgs e)
        {
            if (this.type == ALL) { this.start_work_all(worker, e); }
            else { this.start_work_one(worker, e); }
        }

        public void start_work_one(BackgroundWorker worker, DoWorkEventArgs e)
        {
            int i = 1;
            foreach (DataGridViewRow row in dataGridView.Rows)
            {
                if (worker.CancellationPending == true)
                {
                    e.Cancel = true;
                    break;
                }
                else
                {
                    if (Convert.ToBoolean(row.Cells[0].Value))
                    {
                        ClassOracleUpdate tou = new ClassOracleUpdate(sSQL);
                        tou.obj_name = this.obj_name;
                        tou.typeExecute = this.typeExecute;
                        tou.toOracle(row, sSQL);
                        worker.ReportProgress((100 * i++) / this.count_selected());
                    }
                }
            }
        }

        public void start_work_all(BackgroundWorker worker, DoWorkEventArgs e)
        {
            progress = 0;
            int thread = Convert.ToInt32(Properties.Settings.Default.Thread);
            int start = 0;
            int end = 0;
            int total = dataGridView.Rows.Count;
            int pagination = (int)Math.Round((decimal)total / thread);
            thread = (thread > total) ? total : thread;
            Thread[] threads = new Thread[thread];
            for (int l = 0; l < thread; l++)
            {
                if (l == 0)
                {
                    start = 0;
                    end = pagination;
                }
                else if (l == thread - 1)
                {
                    start = start + pagination;
                    end = total;
                }
                else
                {
                    start = start + pagination;
                    end = start + pagination;
                }
                ClassOracleUpdate tou = new ClassOracleUpdate(start, end, dataGridView, sSQL, worker, e);
                tou.obj_name = this.obj_name;
                tou.typeExecute = this.typeExecute;
                threads[l] = new Thread(new ThreadStart(() => tou.go()));
            }
            foreach (Thread t in threads)
            {
                t.Start();
            }
            foreach (Thread t in threads)
            {
                t.Join();
            }
        }

    }
}
