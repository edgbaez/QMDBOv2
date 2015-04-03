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
    public partial class Form5 : Form
    {
        DatabaseCrud crud;
        private List<ClassLinks> linksCollection;
        MDIParent1 frm;
        DataTable table;
        private int categoryId;

        public Form5(int categoryId)
        {
            InitializeComponent();
            this.categoryId = categoryId;
        }

        private void Form5_Load(object sender, EventArgs e)
        {
            table = new DataTable();
            table.Columns.Add("name");
            table.Columns.Add("error"); 
            ClassHelper.dridComboBoxOracleDbType(this.InType);
            ClassHelper.dridComboBoxOracleDbType(this.OutType);
            frm = this.MdiParent as MDIParent1;
            linksCollection = new List<ClassLinks>();
            crud = new DatabaseCrud();
            crud.loadDataGridViewLinks(this.linksCollection, this.categoryId, this.linksDataGridView, this.frm);
        }

        private void buttonStart_Click(object sender, EventArgs e)
        {
            if (ClassHelper.QuestionYesNoStart(textBox1.Text))
            {

                ClassOracleConnect ora = new ClassOracleConnect();

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

                /* для теста */
                //ParametersOracle inTest = new ParametersOracle();
                //inTest.name = "dDATE";
                //inTest.typeName = "Date";
                ////inTest.value = "31.03.2015";
                //inParamsList.Add(inTest);
                //ParametersOracle inTest2 = new ParametersOracle();
                //inTest2.name = "nNN";
                //inTest2.typeName = "Double";
                //inTest2.value = "2";
                //inParamsList.Add(inTest2);
                //ParametersOracle outTest = new ParametersOracle();
                //outTest.name = "nOFOUND";
                //outTest.typeName = "Double";
                //outTest.size = 17;
                //outParamsList.Add(outTest);
                //ParametersOracle outTest2 = new ParametersOracle();
                //outTest2.name = "nOERR";
                //outTest2.typeName = "Double";
                //outTest2.size = 17;
                //outParamsList.Add(outTest2);
                //textBox1.Text = "P_R_SYNC_LOG_RETURN";

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


                foreach (DataGridViewRow row in linksDataGridView.Rows)
                {
                    if (Convert.ToBoolean(row.Cells[0].Value))
                    {
                        string ConnectionString = ora.OracleConnString(
                            (row.Cells[2].Value ?? String.Empty).ToString(),
                            (row.Cells[3].Value ?? String.Empty).ToString(),
                            (row.Cells[4].Value ?? String.Empty).ToString(),
                            (row.Cells[5].Value ?? String.Empty).ToString(),
                            (row.Cells[6].Value ?? String.Empty).ToString()
                            );

                        var resultList = ora.OracleProcedure(ConnectionString, textBox1.Text, inParamsList, outParamsList);

                        if (resultList.Count > 0)
                        {
                            //DatabaseCrud crud = new DatabaseCrud();
                            //crud.saveKeys(1, inParamsList);
                            //crud.saveKeys(1, outParamsList);
                            //crud.saveValues(Convert.ToInt32(row.Cells["hide_link_id"].Value.ToString()), 1, resultList);

                            string name = (row.Cells[1].Value ?? String.Empty).ToString();

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
                        resultsDataGridView.DataSource = table;
                        resultsDataGridView.Refresh();

                    } /* end if row.Cells[0] true */
                } /* end foreach DataGridViewRow */
            } /* end QuestionYesNoStart */
        } /* end buttonStart_Click */

        private void сохранитьToolStripButton_Click(object sender, EventArgs e)
        {
            crud.saveJobProcedure(this.frm, this.Text, this.categoryId, this.textBox1.Text);
        }

    }
}
