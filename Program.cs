using System;
using System.Drawing;
using System.Windows.Forms;

namespace TechMartProductManager
{
    public class FormMenu : Form
    {
        public FormMenu()
        {
            Text = "MENU TỔNG HỢP BÀI TẬP WINDOWS FORMS";
            Size = new Size(420, 390);
            StartPosition = FormStartPosition.CenterScreen;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;

            Label lblTitle = new Label
            {
                Text = "CHỌN BÀI TẬP ĐỂ KHỞI CHẠY",
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                Location = new Point(75, 20),
                AutoSize = true,
                ForeColor = Color.DarkBlue
            };
            Controls.Add(lblTitle);

            Button btn1 = CreateButton("Bài 1: Service Charge Calculator", 60);
            Button btn2 = CreateButton("Bài 2: Tiếp nhận & Phân loại sự cố IT", 110);
            Button btn3 = CreateButton("Bài 3: Quản lý danh mục Vật tư / Linh kiện", 160);
            Button btn4 = CreateButton("Bài 4: Sơ đồ chọn vị trí chỗ ngồi / Đặt bàn", 210);
            Button btn5 = CreateButton("Bài 5: Quản lý đơn giao hàng (Dashboard)", 260);

            btn1.Click += (s, e) => new ServiceChargeCalculator.Form1().ShowDialog();
            btn2.Click += (s, e) => new FormBai2().ShowDialog();
            btn3.Click += (s, e) => new FormBai3().ShowDialog();
            btn4.Click += (s, e) => new FormBai4().ShowDialog();
            btn5.Click += (s, e) => new FormBai5().ShowDialog();

            Controls.AddRange(new Control[] { btn1, btn2, btn3, btn4, btn5 });
        }

        private Button CreateButton(string text, int top)
        {
            return new Button
            {
                Text = text,
                Location = new Point(30, top),
                Size = new Size(345, 40),
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                BackColor = Color.LightSkyBlue,
                FlatStyle = FlatStyle.System
            };
        }
    }

    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FormMenu());
        }
    }
}