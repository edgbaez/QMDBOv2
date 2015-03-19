using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace QMDBO
{
    public class DataGridViewSortOrder
    {

        public static void dataGridViewColumnHeaderOrder(DataGridView dataGridView1, DataGridViewCellMouseEventArgs e, List<ClassLinks> linksCollection)
        {
            Type valType = dataGridView1.Columns[e.ColumnIndex].ValueType;
            if (valType != typeof(bool))
            {
                string strColumnName = dataGridView1.Columns[e.ColumnIndex].Name;
                SortOrder strSortOrder = getSortOrder(dataGridView1, e.ColumnIndex);

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

        public static SortOrder getSortOrder(DataGridView dataGridView1, int columnIndex)
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

    }
}
