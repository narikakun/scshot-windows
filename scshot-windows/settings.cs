using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace scshot_windows
{
    public partial class settings : Form
    {
        public settings()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.autoClipboard = checkBox1.Checked;
            Properties.Settings.Default.autoOpen = checkBox2.Checked;
            this.Close();
        }

        private void settings_Load(object sender, EventArgs e)
        {
            checkBox1.Checked = Properties.Settings.Default.autoClipboard;
            checkBox2.Checked = Properties.Settings.Default.autoOpen;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
