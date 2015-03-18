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
    public partial class Form4 : Form
    {
        public Form4()
        {
            InitializeComponent();
        }

        private void Form4_Load(object sender, EventArgs e)
        {
            ListViewItem newList = new ListViewItem("NewItem");
            newList.SubItems.Add(DateTime.Now.ToLongTimeString());
            listView1.Items.Add(newList);
            ListViewItem newList2 = new ListViewItem("NewItem2");
            listView1.Items.Add(newList2);
            ListViewItem newList3 = new ListViewItem("NewItem3");
            listView1.Items.Add(newList3);
        }

        private void listView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            MessageBox.Show(listView1.SelectedItems[0].SubItems[1].Text);
        }

    }
}
