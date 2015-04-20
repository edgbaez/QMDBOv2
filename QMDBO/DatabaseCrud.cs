using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.Entity;
using System.Data;

namespace QMDBO
{
    public class DatabaseCrud
    {
        DatabaseContext _context;

        public DatabaseCrud()
        {
            _context = new DatabaseContext();
        }

        public void loadCategory(BindingSource categoryBindingSource)
        {
            _context.Categories.Load();
            categoryBindingSource.DataSource = _context.Categories.Local.ToBindingList();
        }

        public void loadDataGridViewLinks(List<ClassLinks> linksCollection, int category, DataGridView dataGridView, MDIParent1 frm)
        {
            linksCollection.Clear();
            var query = from b in _context.Links
                        where b.CategoryId == category
                        select b;
            foreach (var link in query)
            {
                ClassLinks item = new ClassLinks();
                item.select = false;
                item.name = link.Name;
                item.host = link.Host;
                item.port = link.Port;
                item.servicename = link.Servicename;
                item.user = link.User;
                item.pass = link.Pass;
                item.result = null;
                item.object_type = null;
                item.object_status = null;
                item.last_ddl_time = null;
                item.hide_link_id = link.LinkId.ToString();
                linksCollection.Add(item);
                item = null;
            }
            dataGridView.DataSource = null;
            dataGridView.DataSource = linksCollection;
            dataGridView.Columns["hide_link_id"].Visible = false;
            dataGridView.Refresh();
            int count = query.Count();
            if (frm != null && count>0)
            {
                frm.toolStripStatusLabel.Text = "Количество: " + count;
            }
        }

        public void loadLinks(int category, DataGridView dataGridView, MDIParent1 frm)
        {
            var query = from b in _context.Links
                        where b.CategoryId == category
                        select b;
            foreach (var link in query)
            {
                dataGridView.Rows.Add(false, link.Name, link.Host, link.Port, link.Servicename, link.User, link.Pass, link.LinkId.ToString());
            }
            dataGridView.Refresh();
            int count = query.Count();
            if (frm != null && count > 0)
            {
                frm.toolStripStatusLabel.Text = "Количество: " + count;
            }
        }

        public void saveJob(DataGridView dataGridView, MDIParent1 frm, string formName, int category, string sSQL, string objName, int typeExecute = 1)
        {
            int jobID;
            int counter = 1;

            Job job = new Job
            {
                Name = formName,
                CategoryId = category,
                Code = sSQL,
                TypeExecute = typeExecute,
                ObjName = objName
            };
            var originalJob = _context.Jobs.FirstOrDefault(b => b.Name == job.Name);
            if (originalJob != null)
            {
                originalJob.Code = sSQL;
                originalJob.TypeExecute = typeExecute;
                originalJob.ObjName = objName;
                _context.SaveChanges();
                jobID = originalJob.JobId;
            }
            else
            {
                _context.Jobs.Add(job);
                _context.SaveChanges();
                jobID = job.JobId;
            }

            var results = new List<Result>();

            foreach (DataGridViewRow myRow in dataGridView.Rows)
            {
                Result result = new Result
                {
                    JobId = jobID,
                    LinkId = Convert.ToInt32(myRow.Cells["hide_link_id"].Value.ToString()),
                    Content = (myRow.Cells["result"].Value ?? String.Empty).ToString(),
                    Object_type = (myRow.Cells["object_type"].Value ?? String.Empty).ToString(),
                    Object_status = (myRow.Cells["object_status"].Value ?? String.Empty).ToString(),
                    Last_ddl_time = (myRow.Cells["last_ddl_time"].Value ?? String.Empty).ToString(),
                };
                var originalResult = _context.Results.FirstOrDefault(b => b.JobId == result.JobId && b.LinkId == result.LinkId);
                if (originalResult != null)
                {
                    originalResult.Content = result.Content;
                    originalResult.Object_type = result.Object_type;
                    originalResult.Object_status = result.Object_status;
                    originalResult.Last_ddl_time = result.Last_ddl_time;
                }
                else
                {
                    //_context.Results.Add(result);
                    results.Add(result);
                }

                originalResult = null;
                result = null;

                int count_rows = dataGridView.Rows.Count;
                if (frm != null)
                {
                    frm.toolStripStatusLabel.Text = "Сохранение";
                    frm.toolStripProgressBar1.Visible = true;
                    frm.toolStripProgressBar1.Value = (100 * counter++) / count_rows;
                }

            }
            _context.Results.AddRange(results);
            _context.SaveChanges();
            if (frm != null)
            {
                frm.toolStripStatusLabel.Text = job.Name + " сохранена с количеством строк: " + counter;
                frm.toolStripProgressBar1.Visible = false;
            }
        }

        public void loadListViewJobs(ListView listView)
        {
            var query = from b in _context.Jobs
            select b;
            foreach (var job in query)
            {
                ListViewItem newList = new ListViewItem(job.Name);
                newList.SubItems.Add(job.Category.Name);
                newList.SubItems.Add(job.JobId.ToString());
                newList.SubItems.Add(job.CategoryId.ToString());
                newList.SubItems.Add(job.TypeExecute.ToString());
                listView.Items.Add(newList);
            }
        }

        public void loadDataGridViewLinksHistory(List<ClassLinks> linksCollection, int jobId, int CategoryId, DataGridView dataGridView, MDIParent1 frm)
        {
            linksCollection.Clear();
            /* LINQ LEFT OUTER JOIN */
            var query = from a in _context.Links
                        where a.CategoryId == CategoryId
                        join b in
                            (from r in _context.Results where r.JobId == jobId select r)
                        on a.LinkId equals b.LinkId
                        into outer
                        from c in outer.DefaultIfEmpty()
                        select new { a.Name, a.Host, a.Port, a.Servicename, a.User, a.Pass, a.LinkId, 
                            c.Content, c.Object_type, c.Object_status, c.Last_ddl_time};
            foreach (var result in query)
            {
                ClassLinks item = new ClassLinks();
                item.select = false;
                item.name = result.Name;
                item.host = result.Host;
                item.port = result.Port;
                item.servicename = result.Servicename;
                item.user = result.User;
                item.pass = result.Pass;
                item.result = result.Content;
                item.object_type = result.Object_type;
                item.object_status = result.Object_status;
                item.last_ddl_time = result.Last_ddl_time;
                item.hide_link_id = result.LinkId.ToString();
                linksCollection.Add(item);
                item = null;
            }
            dataGridView.DataSource = null;
            dataGridView.DataSource = linksCollection;
            dataGridView.Columns["hide_link_id"].Visible = false;
            dataGridView.Refresh();
            int count = query.Count();
            if (frm != null && count > 0)
            {
                frm.toolStripStatusLabel.Text = "Количество: " + count;
            }
        }

        public void loadTab1(int jobId, RichTextBox richTextBox, TextBox textBox, ComboBox comboBox)
        {
            var job = _context.Jobs.FirstOrDefault(b => b.JobId == jobId);
            if (job != null)
            {
                richTextBox.Text = job.Code;
                textBox.Text = job.ObjName;
                comboBox.SelectedValue = job.TypeExecute;
            }
        }

        public static int getMaxjobId()
        {
            DatabaseContext context = new DatabaseContext();
            int maxJobId = context.Jobs.Max(b => (int?)b.JobId+1) ?? 1;
            context.Dispose();
            return maxJobId;
        }

        public void saveKeys(int JobId, List <ParametersOracle> paramList, int type)
        {
            foreach (ParametersOracle param in paramList)
            {
                Key key = new Key
                {
                    JobId = JobId,
                    Name = param.name,
                    DbType = param.typeName,
                    Size = param.size,
                    InValue = param.value,
                    Type = type
                };
                var originalKey = _context.Keys.FirstOrDefault(b => b.JobId == key.JobId && b.Name == key.Name);
                if (originalKey != null)
                {
                    originalKey.Name = key.Name;
                    originalKey.DbType = key.DbType;
                    originalKey.Size = key.Size;
                    originalKey.InValue = key.InValue;
                    originalKey.Type = key.Type;
                }
                else
                {
                    _context.Keys.Add(key);
                }
                    originalKey = null;

            }
            _context.SaveChanges();
        }

        public int getKeyId(string keyName, int jobId)
        {
            var key = _context.Keys.FirstOrDefault(b => b.JobId == jobId && b.Name == keyName);
            int keyId = key.KeyId;
            key = null;
            return keyId;
        }

        public void saveValues(DataTable table, int jobId)
        {
            string[] excludeColumns = { "name", "linkId", "error" };

            foreach (DataRow dtRow in table.Rows)
            {
                int linkId = Convert.ToInt32(dtRow["linkId"]);

                if (String.IsNullOrEmpty(dtRow["error"].ToString()))
                {

                    foreach (DataColumn dc in table.Columns)
                    {
                        if (!excludeColumns.Contains(dc.ToString()))
                        {
                            int keyId = getKeyId(dc.ToString(), jobId);

                            Value val = new Value
                            {
                                LinkId = linkId,
                                KeyId = keyId,
                                KeyValue = dtRow[dc].ToString()
                            };
                            var originalValue = _context.Values.FirstOrDefault(b => b.LinkId == val.LinkId && b.KeyId == val.KeyId);
                            if (originalValue != null)
                            {
                                originalValue.KeyValue = val.KeyValue;
                            }
                            else
                            {
                                _context.Values.Add(val);
                            }
                        }
                    }
                }
            }
            _context.SaveChanges();
        }

        public void saveJobProcedure(MDIParent1 frm, string formName, int category, string code, List<ParametersOracle> inParamsList, List<ParametersOracle> outParamsList)
        {
            int jobID;
            int typeExecute = 3;

            Job job = new Job
            {
                Name = formName,
                CategoryId = category,
                Code = code,
                TypeExecute = typeExecute
            };
            var originalJob = _context.Jobs.FirstOrDefault(b => b.Name == job.Name);
            if (originalJob != null)
            {
                originalJob.Code = code;
                originalJob.TypeExecute = typeExecute;
                _context.SaveChanges();
                jobID = originalJob.JobId;
            }
            else
            {
                _context.Jobs.Add(job);
                _context.SaveChanges();
                jobID = job.JobId;
            }
            saveKeys(jobID, inParamsList, ParametersOracle.In);
            saveKeys(jobID, outParamsList, ParametersOracle.Out);
            if (frm != null)
            {
                frm.toolStripStatusLabel.Text = "Задача сохранена";
                frm.toolStripProgressBar1.Visible = false;
            }
        }

        public void loadJobProcedure(int jobId, TextBox textBox, DataGridView inDataGridView, DataGridView outDataGridView, DataTable table)
        {
            var job = _context.Jobs.FirstOrDefault(b => b.JobId == jobId);
            if (job != null)
            {
                textBox.Text = job.Code;
            }

            var query = from a in _context.Keys
                        where a.JobId == jobId
                        select a;
            foreach (var key in query)
            {
                if (key.Type == ParametersOracle.In)
                {
                    inDataGridView.Rows.Add(key.Name, key.DbType, key.InValue);
                }
                else if (key.Type == ParametersOracle.Out)
                {
                    outDataGridView.Rows.Add(key.Name, key.DbType, key.Size);
                    table.Columns.Add(key.Name);
                    loadTableValues(table, key.KeyId, key.Name);
                }
            }
        }

        public void loadTableValues(DataTable table, int keyId, string keyName)
        {
            var query = from b in _context.Values
                        where b.KeyId == keyId
                        select b;
            DataRow newRow = table.NewRow();
            foreach (var result in query)
            { 
                newRow["name"] = result.Link.Name;
                newRow["linkId"] = result.LinkId;
                newRow[keyName] = result.KeyValue;
            }
            table.Rows.Add(newRow);
        }

    }
}
