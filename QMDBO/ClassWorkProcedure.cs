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

            //Объект - блокировка для разграничения доступа
            object _LogLock = new object();
            int thread = Convert.ToInt32(Properties.Settings.Default.Thread);

            Parallel.ForEach(lRows, new ParallelOptions { MaxDegreeOfParallelism = thread }, row =>
            {
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

                        if (resultList.Count > 0)
                        {
                            // использованием блокировки 
                            lock (_LogLock)
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
                } );
        }


    }
}
