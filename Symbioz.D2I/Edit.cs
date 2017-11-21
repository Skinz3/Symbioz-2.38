using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Symbioz.D2I
{
    public partial class Edit : Form
    {
        View View;

        int id;

        public Edit(View view, int id, string content)
        {
            InitializeComponent();
            this.View = view;
            this.id = id;
            idText.Text = id.ToString();
            contentText.Text = content;

        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.View.Seti18n(id, contentText.Text);
            this.Close();
        }
    }
}
