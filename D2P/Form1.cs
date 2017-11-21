using Symbioz.Tools.D2P;
using Symbioz.Tools.SWL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace D2P
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string output = Environment.CurrentDirectory + @"\" + "Output";

            if (!Directory.Exists(output))
            {
                Directory.CreateDirectory(output);
            }
            openFileDialog1.ShowDialog();
            string path = openFileDialog1.FileName;

            D2pFile file = new D2pFile(path);

            var entry = file.GetEntry("music/20100.mp3");

            var frostBottom = File.ReadAllBytes(Environment.CurrentDirectory + "/test.mp3");

            entry.ModifyEntry(frostBottom);

            file.SaveAs(Environment.CurrentDirectory + "/test.d2p");

            return;
            file.ExtractAllFiles(output, true, false);

            output = output + "/SWF/";

            if (!Directory.Exists(output))
            {
                Directory.CreateDirectory(output);
            }

            foreach (var f in Directory.GetFiles(Environment.CurrentDirectory + @"\" + "Output"))
            {
                if (Path.GetExtension(f) == ".swl")
                {
                    SwlFile swl = new SwlFile(f);

                    swl.ExtractSwf(output);
                }
            }

        }
    }
}
