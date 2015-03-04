using System;
using System.Windows.Forms;
using System.IO;

namespace QMDBO
{
    class ClassOpenFile
    {
        public static string OpenFile()
        {
            Stream myStream = null;
            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            openFileDialog1.InitialDirectory = "c:\\";
            openFileDialog1.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            openFileDialog1.FilterIndex = 2;
            openFileDialog1.RestoreDirectory = true;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    if ((myStream = openFileDialog1.OpenFile()) != null)
                    {
                        using (myStream)
                        {
                            StreamReader reader = new StreamReader(myStream, System.Text.Encoding.Default);
                            //StreamReader reader = new StreamReader(myStream, System.Text.Encoding.UTF8);
                            string text = reader.ReadToEnd();
                            return text;
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: Could not read file from disk. Original error: " + ex.Message);
                }
            }
            return null;
        }
    }
}
