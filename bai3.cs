using System;
using System.Drawing;
using System.Windows.Forms;

namespace ServiceChargeCalculator
{
    public class Form1 : Form
    {
        private TextBox txtDonGia;
        private TextBox txtSoLuong;
        private TextBox txtGiamGia;
        private Label lblTongTien;
        private Button btnTinhTien;
        private Button btnLamMoi;

        public Form1()
        {
            InitializeComponentCustom();
        }

        private void InitializeComponentCustom()
        {
            this.Text = "Bài 1: Tính Cước Dịch Vụ & Giảm Giá";
            this.Size = new Size(460, 320);
            this.StartPosition = FormStartPosition.CenterScreen;

            
            Label lbl1 = new Label() { Text = "Đơn giá dịch vụ:", Location = new Point(25, 30), AutoSize = true };
            Label lbl2 = new Label() { Text = "Số lượng khách:", Location = new Point(25, 70), AutoSize = true };
            Label lbl3 = new Label() { Text = "Mã giảm giá (%):", Location = new Point(25, 110), AutoSize = true };

            
            txtDonGia = new TextBox() { Location = new Point(190, 27), Width = 200, TabIndex = 1 };
            txtSoLuong = new TextBox() { Location = new Point(190, 67), Width = 200, TabIndex = 2 };
            txtGiamGia = new TextBox() { Text = "0", Location = new Point(190, 107), Width = 200, TabIndex = 3 };

            
            lblTongTien = new Label() { Text = "Tổng tiền: 0 VNĐ", Location = new Point(25, 155), Font = new Font("Arial", 11, FontStyle.Bold), AutoSize = true, ForeColor = Color.Blue };

            
            btnTinhTien = new Button() { Text = "Tính tiền", Location = new Point(190, 200), Width = 95, Height = 32, TabIndex = 4 };
            btnTinhTien.Click += BtnTinhTien_Click;

            btnLamMoi = new Button() { Text = "Làm mới", Location = new Point(295, 200), Width = 95, Height = 32, TabIndex = 5 };
            btnLamMoi.Click += BtnLamMoi_Click;

            this.Controls.AddRange(new Control[] { lbl1, txtDonGia, lbl2, txtSoLuong, lbl3, txtGiamGia, lblTongTien, btnTinhTien, btnLamMoi });
        }

        private void BtnTinhTien_Click(object sender, EventArgs e)
        {
            if (!double.TryParse(txtDonGia.Text, out double donGia) || donGia < 0)
            {
                MessageBox.Show("Vui lòng nhập Đơn giá hợp lệ (số thực dương).", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDonGia.Focus();
                return;
            }

            if (!int.TryParse(txtSoLuong.Text, out int soLuong) || soLuong < 0)
            {
                MessageBox.Show("Vui lòng nhập Số lượng khách hợp lệ (số nguyên dương).", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtSoLuong.Focus();
                return;
            }

            if (!double.TryParse(txtGiamGia.Text, out double giamGia) || giamGia < 0 || giamGia > 100)
            {
                MessageBox.Show("Vui lòng nhập % Giảm giá hợp lệ (từ 0 đến 100).", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtGiamGia.Focus();
                return;
            }

            double tongTien = (donGia * soLuong) * ((100.0 - giamGia) / 100.0);
            lblTongTien.Text = $"Tổng tiền: {tongTien:N0} VNĐ";
        }

        private void BtnLamMoi_Click(object sender, EventArgs e)
        {
            txtDonGia.Clear();
            txtSoLuong.Clear();
            txtGiamGia.Text = "0";
            lblTongTien.Text = "Tổng tiền: 0 VNĐ";
            txtDonGia.Focus();
        }
    }
}