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
            crud.loadDataGridViewLinks(this.linksCollection, 2, this.linksDataGridView, this.frm);
        }

        private void buttonStart_Click(object sender, EventArgs e)
        {
            ClassOracleConnect ora = new ClassOracleConnect();
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
                    var dict = ora.OracleProcedure(ConnectionString, "PKG_R_TASK.P_DISK_FREE");
                    //MessageBox.Show(res[0].ToString());
                    foreach (var item in dict)
                    {
                        resultsDataGridView.Columns.Add(item.Key, item.Key);
                        resultsDataGridView.Rows.Add(item.Value);
                    }
                    resultsDataGridView.Refresh();
                }
            }
        }

    }
}
