using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace scshot_windows
{
    public partial class upload : Form
    {
        Bitmap bitmap1;
        public upload(Bitmap bitmap)
        {
            InitializeComponent();
            // タスクバーから隠す
            this.ShowInTaskbar = false;
            // 非表示
            this.Visible = false;
            this.WindowState = FormWindowState.Minimized;
            bitmap1 = bitmap;
        }

        private void uploadFile(Bitmap bitmap)
        {
            // tmpフォルダーを取得
            string tmpPath = Path.GetTempPath();
            // 保存先
            var filePath = tmpPath + DateTime.Now.ToString("yyyyMMddHHmmss") + ".png";
            // tmpにpngで保存
            bitmap.Save(filePath, ImageFormat.Png);

            WebClient wc = new WebClient();
            byte[] resData = wc.UploadFile("https://scshot.nakn.jp/api/", filePath);
            string resText = Encoding.UTF8.GetString(resData);
            Clipboard.SetText(resText);
            System.Diagnostics.Process.Start(resText);
        }

        private void upload_Load(object sender, EventArgs e)
        {
            uploadFile(bitmap1);
        }
    }
}
