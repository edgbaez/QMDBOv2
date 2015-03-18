﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.Entity;

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

        public void loadDataGridViewLinks(List<ClassLinks> linksCollection, ComboBox categoryComboBox, DataGridView dataGridView, MDIParent1 frm)
        {
            int category = Convert.ToInt32(categoryComboBox.SelectedValue);

            linksCollection = new List<ClassLinks>();
            int totalItems = 0;
            var query = from b in _context.Links
                        where b.CategoryId == category
                        select b;
            foreach (var link in query)
            {
                totalItems++;
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
            if (frm != null)
            {
                frm.toolStripStatusLabel.Text = "Количество: " + totalItems;
            }
        }

        public void saveJob(DataGridView dataGridView, MDIParent1 frm, string formName, int category=0)
        {
            int jobID;
            int counter = 1;

            Job job = new Job
            {
                Name = formName,
                CategoryId = category
            };
            var originalJob = _context.Jobs.FirstOrDefault(b => b.Name == job.Name);
            if (originalJob != null)
            {
                jobID = originalJob.JobId;
            }
            else
            {
                _context.Jobs.Add(job);
                _context.SaveChanges();
                jobID = job.JobId;
            }

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
                    _context.Results.Add(result);
                }
                _context.SaveChanges();

                originalResult = null;
                result = null;

                if (frm != null)
                {
                    frm.toolStripStatusLabel.Text = "Сохранено строк: " + counter++;
                }

            }
            if (frm != null)
            {
                frm.toolStripStatusLabel.Text = job.Name + " сохранена с количеством строк: " + counter;
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
                listView.Items.Add(newList);
            }
        }

        public void loadDataGridViewLinksHistory(List<ClassLinks> linksCollection, int jobId, int CategoryId, DataGridView dataGridView, MDIParent1 frm)
        {
            linksCollection = new List<ClassLinks>();
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
            if (frm != null)
            {
                frm.toolStripStatusLabel.Text = "Количество: " + query.Count();
            }
        }

    }
}
