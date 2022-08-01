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
    public partial class background : Form
    {
        NotifyIcon notifyIcon;

        public background()
        {
            InitializeComponent();
            // フォームを表示しない
            this.Hide();
            // タスクバーから隠す
            this.ShowInTaskbar = false;
            this.setComponents();
        }

        private void background_Load(object sender, EventArgs e)
        {

        }

        private void setComponents()
        {
            notifyIcon = new NotifyIcon();
            notifyIcon.Visible = true;
            notifyIcon.Text = "scshot";


            ContextMenuStrip contextMenuStrip = new ContextMenuStrip();
            ToolStripMenuItem toolStripMenuItem = new ToolStripMenuItem();
            toolStripMenuItem.Text = "&終了";
            toolStripMenuItem.Click += ToolStripMenuItem_Click;
            contextMenuStrip.Items.Add(toolStripMenuItem);
            notifyIcon.ContextMenuStrip = contextMenuStrip;


            // NotifyIconのクリックイベント
            notifyIcon.Click += NotifyIcon_Click;
        }


        private void ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }


        private void NotifyIcon_Click(object sender, EventArgs e)
        {
            Form form = new screenshot();
            form.Show();
        }
    }
}
