using System;
using System.Windows.Forms;

namespace TechMartProductManager
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FormBai2()); // Chỉ định chạy Form Bài 2
        }
    }
}