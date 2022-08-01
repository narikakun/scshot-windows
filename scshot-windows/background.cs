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
        NotifyIcon NotifyIcon1;

        public background()
        {
            InitializeComponent();
            // タスクバーから隠す
            this.ShowInTaskbar = false;
            // NotifyIconを設定
            this.setComponents();
        }

        private void background_Load(object sender, EventArgs e)
        {
            // 非表示
            this.Visible = false;
            this.WindowState = FormWindowState.Minimized;

        }

        private void setComponents()
        {
            this.NotifyIcon1 = new NotifyIcon();
            this.NotifyIcon1.Icon = Properties.Resources._256icon; 
            this.NotifyIcon1.Visible = true;
            this.NotifyIcon1.Text = "scshot";
            this.NotifyIcon1.Click += new EventHandler(NotifyIcon_Click);


            ContextMenuStrip contextMenuStrip = new ContextMenuStrip();
            ToolStripMenuItem toolStripMenuItem_Settings = new ToolStripMenuItem();
            toolStripMenuItem_Settings.Text = "&設定を開く";
            toolStripMenuItem_Settings.Click += OpenSettings;
            contextMenuStrip.Items.Add(toolStripMenuItem_Settings);
            ToolStripMenuItem toolStripMenuItem_Finish = new ToolStripMenuItem();
            toolStripMenuItem_Finish.Text = "&ソフトを終了";
            toolStripMenuItem_Finish.Click += ApplicationExit;
            contextMenuStrip.Items.Add(toolStripMenuItem_Finish);
            NotifyIcon1.ContextMenuStrip = contextMenuStrip;
        }

        private void OpenSettings(object sender, EventArgs e)
        {
            
        }

        private void ApplicationExit(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void NotifyIcon_Click(object sender, EventArgs e)
        {
            if (Control.MouseButtons == MouseButtons.Right) return;
            Form form = new screenshot();
            form.Show();
        }
    }
}
