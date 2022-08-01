using System;
using System.Collections.Generic;
using System.Linq;
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
            new background();
            Application.Run();
        }
    }
}
