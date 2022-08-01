using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace scshot_windows
{
    internal static class Program
    {
        /// <summary>
        /// アプリケーションのメイン エントリ ポイントです。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Mutex app_mutex = new Mutex(false, "SCSHOT-WINDOWS");
            if (app_mutex.WaitOne(0, false) == false)
            {
                MessageBox.Show("多重起動はできません。");
                new screenshot(true).Show();
            }
            else
            {
                new background();
                new screenshot().Show();
            }
            Application.Run();
        }
    }
}
