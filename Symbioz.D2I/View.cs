using Symbioz.Tools.D2I;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Symbioz.Core;
using System.Windows.Forms;
using SSync;

namespace Symbioz.D2I
{
    public partial class View : Form
    {
        private D2IFile File
        {
            get;
            set;
        }
        private Dictionary<int, string> Values
        {
            get;
            set;
        }
        public View()
        {
            InitializeComponent();
            this.dataGridView1.CellMouseClick += OnCellClicked;
        }

        private void OnCellClicked(object sender, DataGridViewCellMouseEventArgs e)
        {
            int id = (int)dataGridView1.Rows[e.RowIndex].Cells[0].Value;
            string value = (string)dataGridView1.Rows[e.RowIndex].Cells[1].Value;

            Edit edit = new Edit(this, id, value);
            edit.Show();

        }

        public void Seti18n(int id, string contentText)
        {
            File.SetText(id, contentText);
            this.Values[id] = contentText;
            BindDataSource((from item in Values select new { Item = item.Key, Price = item.Value }).ToArray());
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.ShowDialog();
            File = new D2IFile(dialog.FileName);
            this.Values = File.GetAllText();
            BindDataSource((from item in Values select new { Item = item.Key, Price = item.Value }).ToArray());

        }
        private void BindDataSource(Array value)
        {
            dataGridView1.DataSource = value;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
        }
        private void button3_Click(object sender, EventArgs e)
        {
            if (searchContent.Text == string.Empty)
            {
                BindDataSource((from item in Values select new { Item = item.Key, Price = item.Value }).ToArray());
                return;
            }
            string search = searchContent.Text.ToLower();
            var results = Array.FindAll(Values.ToArray(), x => x.Value.ToLower().Contains(search));
            BindDataSource(results);

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (new FileInfo(File.FilePath).IsFileLocked())
            {
                var dir = Path.GetDirectoryName(File.FilePath) + "_2.d2i";
                File.Save(dir);
            }
            else
            {
                File.Save();
            }
        }


    }
}
