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

        public Form5()
        {
            InitializeComponent();
        }

        private void Form5_Load(object sender, EventArgs e)
        {
            frm = this.MdiParent as MDIParent1;
            linksCollection = new List<ClassLinks>();
            crud = new DatabaseCrud();
            crud.loadDataGridViewLinks(this.linksCollection, 1, this.linksDataGridView, this.frm);
        }

        private void buttonStart_Click(object sender, EventArgs e)
        {
            ClassOracleConnect ora = new ClassOracleConnect();

            List<InParametersOracle> inParamsList = new List<InParametersOracle>();
            foreach (DataGridViewRow inRow in inDataGridView.Rows)
            {
                if (inRow.Cells[0].Value != null)
                {
                    InParametersOracle inParam = new InParametersOracle();
                    inParam.name = (inRow.Cells[0].Value ?? String.Empty).ToString();
                    inParam.typeName = (inRow.Cells[1].Value ?? String.Empty).ToString();
                    inParam.value = (inRow.Cells[2].Value ?? String.Empty).ToString();
                    inParamsList.Add(inParam);
                    inParam = null;
                }
            }

            List<OutParametersOracle> outParamsList = new List<OutParametersOracle>();
            foreach (DataGridViewRow outRow in outDataGridView.Rows)
            {
                if (outRow.Cells[0].Value != null)
                {
                    OutParametersOracle outParam = new OutParametersOracle();
                    outParam.name = (outRow.Cells[0].Value ?? String.Empty).ToString();
                    outParam.typeName = (outRow.Cells[1].Value ?? String.Empty).ToString();
                    outParam.size = Convert.ToInt32((outRow.Cells[2].Value ?? 1));
                    outParamsList.Add(outParam);
                    outParam = null;
                }
            }

            /* для теста */
            InParametersOracle inTest = new InParametersOracle();
            inTest.name = "sDISK";
            inTest.typeName = "Char";
            inTest.value = "D";
            inParamsList.Add(inTest);
            OutParametersOracle outTest = new OutParametersOracle();
            outTest.name = "sOUT";
            outTest.typeName = "Varchar2";
            outTest.size = 4000;
            outParamsList.Add(outTest);
            textBox1.Text = "PKG_R_TASK.P_DISK_FREE";

            if (resultsDataGridView.ColumnCount==0) {
                resultsDataGridView.Columns.Add("name", "name");
                foreach (OutParametersOracle outParam in outParamsList)
                {
                resultsDataGridView.Columns.Add(outParam.name, outParam.name);
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

                    foreach (var item in resultList)
                    {
                        resultsDataGridView.Rows.Add(row.Cells[2].Value, item.value);
                    }
                    resultsDataGridView.Refresh();
                }
            }
        }

    }
}
