using Symbioz.Tools.D2P;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;
using System.Diagnostics;

namespace Symbioz.DofusMusic
{
    public partial class Options : Form
    {
        private D2pFile D2PFile
        {
            get;
            set;
        }
        private string EntryName
        {
            get;
            set;
        }
        private string ContainerName
        {
            get;
            set;
        }
        public Options(D2pFile file, string entryName, string containerName)
        {
            this.D2PFile = file;
            this.EntryName = entryName;
            this.ContainerName = containerName;
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                var entry = D2PFile.GetEntry(EntryName);
                string path = Form1.MUSIC_PATH + entry.FileName;
                byte[] data = D2PFile.ReadFile(entry);
                File.WriteAllBytes(path, data);
                Process.Start(path);
                this.Close();
            }
            catch
            {
                MessageBox.Show("Impossible d'ouvrir ce fichier actuellement, il est probablement utilisé par un autre processus.","Nope");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "Fichier MP3 | *.mp3";
            openFileDialog1.ShowDialog();
            string path = openFileDialog1.FileName;

            if (path != string.Empty)
            {
                try
                {
                    D2pFile file = new D2pFile(Path.GetDirectoryName(D2PFile.FilePath) + "/" + ContainerName);
                    var data = File.ReadAllBytes(path);
                    file.GetEntry(EntryName).ModifyEntry(data);
                    file.Save();
                    MessageBox.Show("Le fichier a bien été modifié.");
                    this.Close();
                }
                catch
                {
                    MessageBox.Show("Impossible de modifier le fichier.");
                }
            }


        }
    }
}
