using System;
using System.Windows.Forms;
using YouthUnionManagement.Forms;

namespace YouthUnionManagement
{
    internal static class Program
    {
        /// <summary>
        /// Điểm khởi đầu chính cho ứng dụng.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new LoginForm());
        }
    }
}
