using System;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Media;
using Color = System.Drawing.Color;
using Pen = System.Drawing.Pen;

namespace scshot_windows
{
    public partial class screenshot : Form
    {
        Point mouseDownPosition; //ドラッグを開始したマウスの位置
        Point mouseDragPosition; //現在ドラッグ中のマウスの位置
        bool isMouseDown = false;
        Pen selectPen; //ドラッグ中の四角形の描画に使用するペン

        public screenshot()
        {
            InitializeComponent();
            // ロード中は非表示
            this.Hide();
            // タスクバーから隠す
            this.ShowInTaskbar = false;
            // フルスクリーンにする
            this.FormBorderStyle = FormBorderStyle.None;
            // 一番手前に持ってくる
            this.TopMost = true;
            // ウィンドウを半透明
            this.AllowTransparency = true;
            this.BackColor = Color.White;
            this.Opacity = 0.2;
            // 描写イベント
            pictureBox1.Paint += pictureBox1_Paint;
            pictureBox1.MouseDown += pictureBox1_MouseDown;
            pictureBox1.MouseMove += pictureBox1_MouseMove;
            pictureBox1.MouseUp += pictureBox1_MouseUp;
            this.KeyPress += form_keyPress;
        }

        private void form_keyPress(object sender, KeyPressEventArgs e)
        {
            if ( e.KeyChar == (char)Keys.Escape ) {
                this.Close();
            }
        }

        private void screenshot_Load(object sender, EventArgs e)
        {
            // ドラック中の線用
            selectPen = new Pen(Color.Yellow, 10);
            selectPen.DashStyle = System.Drawing.Drawing2D.DashStyle.Solid;
            windowRegion();
            // 再表示
            this.Show();
        }

        private void windowRegion()
        {
            // ウィンドウのサイズを設定
            System.Drawing.Drawing2D.GraphicsPath path = new System.Drawing.Drawing2D.GraphicsPath();
            var activeRect = new Rectangle();
            activeRect.X = Math.Min(mouseDownPosition.X, mouseDragPosition.X);
            activeRect.Y = Math.Min(mouseDownPosition.Y, mouseDragPosition.Y);
            activeRect.Width = Math.Abs(mouseDragPosition.X - mouseDownPosition.X);
            activeRect.Height = Math.Abs(mouseDragPosition.Y - mouseDownPosition.Y);
            if (isMouseDown) {
                // マウスダウン中にその部分を切り抜く
                path.AddRectangle(activeRect);
            }
            foreach (Screen s in Screen.AllScreens)
            {
                // 複数ディスプレイ対応用
                path.AddRectangle(new Rectangle(s.Bounds.X, s.Bounds.Y, s.Bounds.Width, s.Bounds.Height));
            }
            this.Region = new Region(path);
            RectangleF rectangleF = path.GetBounds();
            this.Size = new Size((int)rectangleF.Width, (int)rectangleF.Height);
            this.SetBounds((int)rectangleF.X, (int)rectangleF.Y, (int)rectangleF.Width, (int)rectangleF.Height, BoundsSpecified.Size);
            this.Location = new Point((int)rectangleF.X, (int)rectangleF.Y);
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            if (Control.MouseButtons != MouseButtons.Left) return;
            windowRegion();
            //ドラック中の四角形の座標を計算
            var activeRect = new Rectangle();
            activeRect.X = Math.Min(mouseDownPosition.X, mouseDragPosition.X)-5;
            activeRect.Y = Math.Min(mouseDownPosition.Y, mouseDragPosition.Y)-5;
            activeRect.Width = Math.Abs(mouseDragPosition.X - mouseDownPosition.X)+10;
            activeRect.Height = Math.Abs(mouseDragPosition.Y - mouseDownPosition.Y)+10;
            //ドラッグ中の四角形を描画
            e.Graphics.DrawRectangle(selectPen, activeRect);
        }


        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            // マウスがクリックされた場合
            mouseDownPosition = e.Location;
            isMouseDown = true;
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            // マウスが移動した場合
            mouseDragPosition = e.Location;
            pictureBox1.Invalidate();
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            // マウスが話された場合
            isMouseDown = false;
            pictureBox1.Invalidate();
            var activeRect = new Rectangle();
            activeRect.X = Math.Min(mouseDownPosition.X, mouseDragPosition.X);
            activeRect.Y = Math.Min(mouseDownPosition.Y, mouseDragPosition.Y);
            activeRect.Width = Math.Abs(mouseDragPosition.X - mouseDownPosition.X);
            activeRect.Height = Math.Abs(mouseDragPosition.Y - mouseDownPosition.Y);
            //Bitmapの作成
            if (activeRect.Width <= 0 || activeRect.Height <= 0) return;
            Bitmap bmp = new Bitmap(activeRect.Width, activeRect.Height);
            //Graphicsの作成
            Graphics g = Graphics.FromImage(bmp);
            //画面全体をコピーする
            g.CopyFromScreen(new Point(activeRect.X, activeRect.Y), new Point(0,0), bmp.Size);
            //解放
            g.Dispose();

            //表示
            Form form = new upload(bmp);
            this.Close();
            form.Show();
        }
    }
}
