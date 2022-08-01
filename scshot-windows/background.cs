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
        public settings sf = null;
        public screenshot scf = null;

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
            this.NotifyIcon1.MouseDown += new MouseEventHandler(NotifyIcon_Click);
        }

        private void OpenSettings(object sender, EventArgs e)
        {
            
        }

        private void ApplicationExit(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void NotifyIcon_Click(object sender, MouseEventArgs e)
        {
            // 左クリック以外はりたーん
            if (e.Button == MouseButtons.Right)
            {
                if (this.sf == null || this.sf.IsDisposed)
                {
                    this.sf = new settings();
                    sf.Show();
                }
            }
            else if (e.Button == MouseButtons.Left)
            {
                if (this.scf == null || this.scf.IsDisposed)
                {
                    this.scf = new screenshot();
                    scf.Show();
                }
            }
        }
    }
}
