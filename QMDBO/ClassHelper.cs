using System;
using System.Windows.Forms;
using System.Linq;
using System.IO;
using System.Collections.Generic;

namespace QMDBO
{
    public class ClassHelper
    {
        public static bool QuestionYesNoStart(string txt)
        {
            if (string.IsNullOrEmpty(txt))
            {
                MessageBox.Show("Что обновлять?", "Пусто");
            }
            else
            {
                var result = MessageBox.Show("Обновить?", "Подтверждение",
                                             MessageBoxButtons.YesNo,
                                             MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    return true;
                }
            }
            return false;
        }

        public static string CompletedText(System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled == true)
            {
                return "Отменено";
            }
            else if (e.Error != null)
            {
                return "Ошибка: " + e.Error.Message;
            }
            else
            {
                return "Готово";
            }
        }

        public static void DragDropOpenFile(RichTextBox richTextBox, DragEventArgs e)
        {
            string[] filenames = e.Data.GetData(DataFormats.FileDrop) as string[];
            string filetype = System.IO.Path.GetExtension(filenames[0]);
            string[] ext = new string[] { ".txt", ".sql", ".prc", ".fnc", ".trg", ".pck" };
            if (ext.Contains(filetype))
            {
                richTextBox.LoadFile(filenames[0], RichTextBoxStreamType.PlainText);
            }
            else if (filetype == ".rtf")
            {
                richTextBox.LoadFile(filenames[0], RichTextBoxStreamType.RichText);
            }
            else
            {
                MessageBox.Show("Тип файла не поддерживается");
            }
        }

        public static Form IsFormAlreadyOpen(Type FormType)
        {
            foreach (Form OpenForm in Application.OpenForms)
            {
                if (OpenForm.GetType() == FormType)
                {
                    OpenForm.BringToFront();
                    return OpenForm;
                }
            }

            return null;
        }

        public static void writeCSV(DataGridView gridIn, string outputFile)
        {
            //test to see if the DataGridView has any rows
            if (gridIn.RowCount > 0)
            {
                string value = "";
                DataGridViewRow dr = new DataGridViewRow();
                StreamWriter swOut = new StreamWriter(outputFile);

                //write header rows to csv
                for (int i = 0; i <= gridIn.Columns.Count - 1; i++)
                {
                    if (i > 0)
                    {
                        swOut.Write(";");
                    }
                    swOut.Write(gridIn.Columns[i].HeaderText);
                }

                swOut.WriteLine();

                //write DataGridView rows to csv
                for (int j = 0; j <= gridIn.Rows.Count - 1; j++)
                {
                    if (j > 0)
                    {
                        swOut.WriteLine();
                    }

                    dr = gridIn.Rows[j];

                    for (int i = 0; i <= gridIn.Columns.Count - 1; i++)
                    {
                        if (i > 0)
                        {
                            swOut.Write(";");
                        }
                        value = (dr.Cells[i].Value ?? String.Empty).ToString();
                        //replace comma's with spaces
                        value = value.Replace(',', ' ');
                        //replace embedded newlines with spaces
                        value = value.Replace(Environment.NewLine, " ");

                        swOut.Write(value);
                    }
                }
                swOut.Close();
            }
        }

        public static void PopulateComboBox(ComboBox comboBox1, int selectedIndex=0)
        {
            var dict = new Dictionary<int, string>();
            dict.Add(1, "NonQuery");
            dict.Add(2, "Scalar");

            comboBox1.DataSource = new BindingSource(dict, null);
            comboBox1.DisplayMember = "Value";
            comboBox1.ValueMember = "Key";
            comboBox1.SelectedIndex = selectedIndex;
        }

        enum MyEnum
        {
            Char,
            Date,
            Decimal,
            Double,
            Int64,
            Varchar2,
        };

        public static void dridComboBoxOracleDbType(DataGridViewComboBoxColumn comboBox)
        {
            comboBox.ValueType = typeof(MyEnum);
            comboBox.DataSource = Enum.GetValues(typeof(MyEnum));
        }

        public static void AddContextMenu(RichTextBox rtb)
        {
            if (rtb.ContextMenuStrip == null)
            {
                ContextMenuStrip cms = new ContextMenuStrip { ShowImageMargin = true };
                ToolStripMenuItem tsmiCut = new ToolStripMenuItem("Вырезать");
                tsmiCut.Image = Properties.Resources.Cut_icon;
                tsmiCut.Click += (sender, e) => rtb.Cut();
                cms.Items.Add(tsmiCut);
                ToolStripMenuItem tsmiCopy = new ToolStripMenuItem("Копировать");
                tsmiCopy.Image = Properties.Resources.Copy_icon;
                tsmiCopy.Click += (sender, e) => rtb.Copy();
                cms.Items.Add(tsmiCopy);
                ToolStripMenuItem tsmiPaste = new ToolStripMenuItem("Вставить");
                tsmiPaste.Image = Properties.Resources.Paste_icon;
                tsmiPaste.Click += (sender, e) => rtb.Paste();
                cms.Items.Add(tsmiPaste);
                rtb.ContextMenuStrip = cms;
            }
        }

    }
}