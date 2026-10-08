using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace TechMartProductManager
{
    public class FormBai4 : Form
    {
        private TextBox txtHoTen = new TextBox { Location = new Point(130, 25), Width = 180 };
        private TextBox txtSDT = new TextBox { Location = new Point(130, 65), Width = 180 };
        private DateTimePicker dtpNgayDat = new DateTimePicker { Location = new Point(130, 105), Width = 180, CustomFormat = "dd/MM/yyyy HH:mm", Format = DateTimePickerFormat.Custom };

        private Label lblDanhSachChon = new Label { Location = new Point(130, 150), AutoSize = true, Text = "Chưa chọn", Font = new Font("Segoe UI", 9, FontStyle.Bold) };
        private Label lblTongTien = new Label { Location = new Point(130, 185), AutoSize = true, Text = "0 VNĐ", Font = new Font("Segoe UI", 10, FontStyle.Bold), ForeColor = Color.DarkRed };

        private Button btnDatCho = new Button { Text = "Xác nhận đặt chỗ", Location = new Point(15, 225), Size = new Size(140, 38), BackColor = Color.LightSkyBlue, Font = new Font("Segoe UI", 9, FontStyle.Bold) };
        private Button btnHuyCho = new Button { Text = "Hủy chọn", Location = new Point(165, 225), Size = new Size(145, 38), BackColor = Color.MistyRose };

        private Panel pnlGhe = new Panel { Location = new Point(15, 25), Size = new Size(385, 240), BorderStyle = BorderStyle.FixedSingle };

        private List<Button> listGhe = new List<Button>();
        private const int GIA_GHE = 100000;

        public FormBai4()
        {
            Text = "Bài 4: Sơ đồ chọn vị trí chỗ ngồi / Đặt bàn hẹn giờ";
            Size = new Size(780, 330);
            StartPosition = FormStartPosition.CenterScreen;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;

            GroupBox grpSoDo = new GroupBox { Text = "Sơ đồ chỗ ngồi (Xanh: Trống | Vàng: Chọn | Đỏ: Đã đặt)", Location = new Point(15, 10), Size = new Size(415, 275) };
            
            int rows = 4;
            int cols = 5;
            int btnWidth = 65;
            int btnHeight = 45;
            int gap = 10;

            for (int r = 0; r < rows; r++)
            {
                for (int c = 0; c < cols; c++)
                {
                    char rowChar = (char)('A' + r);
                    string seatName = $"{rowChar}{c + 1}";

                    Button btn = new Button
                    {
                        Text = seatName,
                        Size = new Size(btnWidth, btnHeight),
                        Location = new Point(12 + c * (btnWidth + gap), 20 + r * (btnHeight + gap)),
                        BackColor = Color.LightGreen,
                        Tag = "TRONG"
                    };

                    if ((r == 0 && c == 1) || (r == 2 && c == 3))
                    {
                        btn.BackColor = Color.IndianRed;
                        btn.ForeColor = Color.White;
                        btn.Tag = "DA_DAT";
                    }

                    btn.Click += BtnGhe_Click;
                    pnlGhe.Controls.Add(btn);
                    listGhe.Add(btn);
                }
            }

            grpSoDo.Controls.Add(pnlGhe);

            GroupBox grpThongTin = new GroupBox { Text = "Thông tin đặt chỗ", Location = new Point(440, 10), Size = new Size(320, 275) };
            grpThongTin.Controls.AddRange(new Control[] {
                new Label { Text = "Họ và tên:", Location = new Point(15, 28), AutoSize = true }, txtHoTen,
                new Label { Text = "Số điện thoại:", Location = new Point(15, 68), AutoSize = true }, txtSDT,
                new Label { Text = "Thời gian đặt:", Location = new Point(15, 108), AutoSize = true }, dtpNgayDat,
                new Label { Text = "Vị trí đã chọn:", Location = new Point(15, 150), AutoSize = true }, lblDanhSachChon,
                new Label { Text = "Tổng tiền:", Location = new Point(15, 185), AutoSize = true }, lblTongTien,
                btnDatCho, btnHuyCho
            });

            Controls.AddRange(new Control[] { grpSoDo, grpThongTin });

            btnDatCho.Click += (s, e) => {
                if (string.IsNullOrWhiteSpace(txtHoTen.Text))
                {
                    MessageBox.Show("Vui lòng nhập Họ và tên!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtHoTen.Focus();
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtSDT.Text))
                {
                    MessageBox.Show("Vui lòng nhập Số điện thoại!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtSDT.Focus();
                    return;
                }

                List<string> selectedSeats = GetSelectedSeats();
                if (selectedSeats.Count == 0)
                {
                    MessageBox.Show("Vui lòng chọn ít nhất 1 vị trí ghế trống!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string summary = $"--- XÁC NHẬN ĐẶT CHỖ THÀNH CÔNG ---\n\n" +
                                 $"• Khách hàng: {txtHoTen.Text.Trim()}\n" +
                                 $"• Số điện thoại: {txtSDT.Text.Trim()}\n" +
                                 $"• Thời gian: {dtpNgayDat.Value:dd/MM/yyyy HH:mm}\n" +
                                 $"• Vị trí ghế: {string.Join(", ", selectedSeats)}\n" +
                                 $"• Tổng thanh toán: {lblTongTien.Text}";

                MessageBox.Show(summary, "Thông báo thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);

                foreach (Button btn in listGhe)
                {
                    if (btn.Tag.ToString() == "DANG_CHON")
                    {
                        btn.BackColor = Color.IndianRed;
                        btn.ForeColor = Color.White;
                        btn.Tag = "DA_DAT";
                    }
                }

                txtHoTen.Clear();
                txtSDT.Clear();
                dtpNgayDat.Value = DateTime.Now;
                UpdateCalculator();
            };

            btnHuyCho.Click += (s, e) => {
                foreach (Button btn in listGhe)
                {
                    if (btn.Tag.ToString() == "DANG_CHON")
                    {
                        btn.BackColor = Color.LightGreen;
                        btn.Tag = "TRONG";
                    }
                }
                UpdateCalculator();
            };
        }

        private void BtnGhe_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            if (btn == null) return;

            if (btn.Tag.ToString() == "DA_DAT")
            {
                MessageBox.Show("Vị trí ghế này đã có người đặt!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (btn.Tag.ToString() == "TRONG")
            {
                btn.BackColor = Color.Gold;
                btn.Tag = "DANG_CHON";
            }
            else if (btn.Tag.ToString() == "DANG_CHON")
            {
                btn.BackColor = Color.LightGreen;
                btn.Tag = "TRONG";
            }

            UpdateCalculator();
        }

        private List<string> GetSelectedSeats()
        {
            List<string> list = new List<string>();
            foreach (Button btn in listGhe)
            {
                if (btn.Tag.ToString() == "DANG_CHON")
                {
                    list.Add(btn.Text);
                }
            }
            return list;
        }

        private void UpdateCalculator()
        {
            List<string> selectedSeats = GetSelectedSeats();
            if (selectedSeats.Count == 0)
            {
                lblDanhSachChon.Text = "Chưa chọn";
                lblTongTien.Text = "0 VNĐ";
            }
            else
            {
                lblDanhSachChon.Text = string.Join(", ", selectedSeats);
                long total = selectedSeats.Count * GIA_GHE;
                lblTongTien.Text = $"{total:N0} VNĐ";
            }
        }
    }
}