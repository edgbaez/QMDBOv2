using System;
using System.Windows.Forms;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace QMDBO
{
    class ClassWorkProcedure
    {
        private DataGridView linksDataGridView;
        private DataTable table;
        private List<ParametersOracle> inParamsList;
        private List<ParametersOracle> outParamsList;
        private string procedureName;
        public const int SELECTED = 1;
        public const int ALL = 2;
        private int type = 0;

        public ClassWorkProcedure(DataGridView dataGridView, DataTable table, List<ParametersOracle> inParamsList, List<ParametersOracle> outParamsList, string procedureName, int type)
        {
            this.linksDataGridView = dataGridView;
            this.table = table;
            this.inParamsList = inParamsList;
            this.outParamsList = outParamsList;
            this.procedureName = procedureName;
            this.type = type;
        }

        public void start_work(BackgroundWorker worker, DoWorkEventArgs e)
        {
            ClassOracleConnect ora = new ClassOracleConnect();
            IEnumerable<DataGridViewRow> lRows;
            if (this.type == ALL) {
                lRows = from DataGridViewRow row in linksDataGridView.Rows
                        select row;
            }
            else {
                lRows = from DataGridViewRow row in linksDataGridView.Rows
                                                     where Convert.ToBoolean(row.Cells[0].Value) == true
                                                     select row;
            }
            int count = lRows.Count();
            //Объект - блокировка для разграничения доступа
            object _LogLock = new object();
            //Количество потоков берутся из настроек программы
            int thread = Convert.ToInt32(Properties.Settings.Default.Thread);
            //Массив счетчиков для создаваемых потоков
            int[] _Counts = new int[thread];
            //Общий счетчик - разделяемый ресурс
            int _SharedCount = 0;

            Parallel.ForEach(lRows, new ParallelOptions { MaxDegreeOfParallelism = thread }, (DataGridViewRow row, ParallelLoopState state) =>
            {
                if (worker.CancellationPending == true)
                {
                    e.Cancel = true;
                    state.Stop();
                }
                        string ConnectionString = ora.OracleConnString(
                            (row.Cells[2].Value ?? String.Empty).ToString(),
                            (row.Cells[3].Value ?? String.Empty).ToString(),
                            (row.Cells[4].Value ?? String.Empty).ToString(),
                            (row.Cells[5].Value ?? String.Empty).ToString(),
                            (row.Cells[6].Value ?? String.Empty).ToString()
                            );
                        string name = (row.Cells[1].Value ?? String.Empty).ToString();
                        int linkId = Convert.ToInt32(row.Cells["ColumnLinkId"].Value.ToString());

                        var resultList = ora.OracleProcedure(ConnectionString, procedureName, inParamsList, outParamsList);

                        //Потокобезопасная модификация с использованием блокировки 
                        lock (_LogLock)
                        {
                            if (resultList.Count > 0)
                            {
                                DataRow drFound = table.Select("name = '" + name + "'").FirstOrDefault();
                                if (drFound != null)
                                {
                                    foreach (var item in resultList)
                                    {
                                        if (item.name != null)
                                        {
                                            drFound[item.name] = item.value;
                                        }
                                    }
                                }
                                else
                                {
                                    DataRow newRow = table.NewRow();
                                    newRow["name"] = name;
                                    newRow["linkId"] = linkId;
                                    foreach (var item in resultList)
                                    {
                                        if (item.name != null)
                                        {
                                            newRow[item.name] = item.value;
                                        }
                                    }
                                    table.Rows.Add(newRow);
                                }
                            }
                        }
                        //Каждый поток имеет свой счетчик, поэтому разграничение доступа не требуется
                        //_Counts[i]++;
                        //Потокобезопасная операция инкремента (_SharedCount++)
                        //Операции Interlocked являются атомарными и не требуют блокировок типа lock() {}
                        //Это позволяет избежать потерь в производительности, связанных с блокировками
                        Interlocked.Increment(ref _SharedCount);
                        worker.ReportProgress((100 * _SharedCount) / count);
                } );
        }


    }
}
