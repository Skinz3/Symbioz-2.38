using Symbioz.Tools.D2P;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Symbioz.DofusMusic
{
    public partial class Form1 : Form
    {
        public static string MUSIC_PATH = Environment.CurrentDirectory + "/Ouput/";

        private List<D2PEntryDescription> Values
        {
            get;
            set;
        }
        private D2pFile D2PFile
        {
            get;
            set;
        }
        public Form1()
        {
            Values = new List<D2PEntryDescription>();
            InitializeComponent();
            this.gridView.CellMouseDoubleClick += OnCellClicked;
            if (Directory.Exists(MUSIC_PATH) == false)
            {
                Directory.CreateDirectory(MUSIC_PATH);
            }
        }
        private void BindDataSource(Array value)
        {
            gridView.DataSource = value;
            gridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
        }

        private void OnCellClicked(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.ColumnIndex == 1)
            {
                try
                {
                    string containerName = (string)gridView.Rows[e.RowIndex].Cells[0].Value;
                    string entryName = (string)gridView.Rows[e.RowIndex].Cells[1].Value;
                    Options option = new Options(D2PFile, entryName, containerName);
                    option.Show();
                }
                catch
                {

                }

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
            string path = openFileDialog1.FileName;

            if (path != string.Empty)
            {
                D2PFile = new D2pFile(path);
                foreach (var entry in D2PFile.Entries)
                {
                    int soundId = 0;
                    int.TryParse(Path.GetFileNameWithoutExtension(entry.FileName), out soundId);
                    Values.Add(new D2PEntryDescription(entry.FullFileName, entry.Container.FilePath, D2OConstants.GetRelativeSubarea(soundId)));
                }
                BindDataSource(Values.ToArray());

            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (searchContent.Text == string.Empty)
            {
                BindDataSource(Values.ToArray());
                return;
            }
            string search = searchContent.Text.ToLower();
            var results = Array.FindAll(Values.ToArray(), x => x.ContainerFileName.ToLower().Contains(search) || x.FileName.ToLower().Contains(search) || x.Informations.ToLower().Contains(search));
            BindDataSource(results);
        }
    }
}
