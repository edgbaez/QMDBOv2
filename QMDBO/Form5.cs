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
    }
}
