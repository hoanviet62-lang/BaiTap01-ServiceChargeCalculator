using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace TechMartProductManager
{
    public class FormBai2 : Form
    {
        private TextBox txtMaPhieu = new TextBox { Location = new Point(150, 20), Width = 350 };
        private TextBox txtNguoiYeuCau = new TextBox { Location = new Point(150, 55), Width = 350 };
        private DateTimePicker dtpNgayGhiNhan = new DateTimePicker { Location = new Point(150, 90), Width = 350, CustomFormat = "dd/MM/yyyy HH:mm", Format = DateTimePickerFormat.Custom };

        private RadioButton rdoThap = new RadioButton { Text = "Thấp", Location = new Point(20, 22), Checked = true, AutoSize = true };
        private RadioButton rdoTrungBinh = new RadioButton { Text = "Trung bình", Location = new Point(150, 22), AutoSize = true };
        private RadioButton rdoKhanCap = new RadioButton { Text = "Khẩn cấp", Location = new Point(300, 22), AutoSize = true };

        private ComboBox cboLoaiSuCo = new ComboBox { Location = new Point(150, 195), Width = 350, DropDownStyle = ComboBoxStyle.DropDownList };

        private CheckBox chkMayTinhBan = new CheckBox { Text = "Máy tính bàn", Location = new Point(15, 22), AutoSize = true };
        private CheckBox chkLaptop = new CheckBox { Text = "Laptop", Location = new Point(135, 22), AutoSize = true };
        private CheckBox chkMayIn = new CheckBox { Text = "Máy in", Location = new Point(225, 22), AutoSize = true };
        private CheckBox chkDienThoai = new CheckBox { Text = "Điện thoại", Location = new Point(315, 22), AutoSize = true };

        private PictureBox picAnhLoi = new PictureBox { Location = new Point(150, 295), Size = new Size(200, 120), BorderStyle = BorderStyle.FixedSingle, SizeMode = PictureBoxSizeMode.StretchImage };
        private Button btnTaiAnh = new Button { Text = "Tải ảnh lỗi", Location = new Point(365, 295), Size = new Size(135, 32) };
        private Button btnGui = new Button { Text = "Gửi yêu cầu", Location = new Point(150, 435), Size = new Size(120, 35), BackColor = Color.LightSkyBlue, Font = new Font("Segoe UI", 9, FontStyle.Bold) };
        private Button btnNhapLai = new Button { Text = "Nhập lại", Location = new Point(280, 435), Size = new Size(120, 35) };

        public FormBai2()
        {
            Text = "Bài 2: Tiếp nhận & Phân loại sự cố IT";
            Size = new Size(540, 520);
            StartPosition = FormStartPosition.CenterScreen;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;

            cboLoaiSuCo.Items.AddRange(new object[] { "Phần cứng", "Phần mềm", "Mạng", "Tài khoản" });
            cboLoaiSuCo.SelectedIndex = 0;

            GroupBox grpUuTien = new GroupBox { Text = "Mức độ ưu tiên", Location = new Point(20, 125), Size = new Size(480, 55) };
            grpUuTien.Controls.AddRange(new Control[] { rdoThap, rdoTrungBinh, rdoKhanCap });

            GroupBox grpThietBi = new GroupBox { Text = "Thiết bị ảnh hưởng", Location = new Point(20, 230), Size = new Size(480, 55) };
            grpThietBi.Controls.AddRange(new Control[] { chkMayTinhBan, chkLaptop, chkMayIn, chkDienThoai });

            Controls.AddRange(new Control[] {
                new Label { Text = "Mã phiếu:", Location = new Point(20, 23), AutoSize = true }, txtMaPhieu,
                new Label { Text = "Người yêu cầu:", Location = new Point(20, 58), AutoSize = true }, txtNguoiYeuCau,
                new Label { Text = "Ngày ghi nhận:", Location = new Point(20, 93), AutoSize = true }, dtpNgayGhiNhan,
                grpUuTien,
                new Label { Text = "Loại sự cố:", Location = new Point(20, 198), AutoSize = true }, cboLoaiSuCo,
                grpThietBi,
                new Label { Text = "Ảnh chụp lỗi:", Location = new Point(20, 295), AutoSize = true }, picAnhLoi, btnTaiAnh,
                btnGui, btnNhapLai
            });

            btnTaiAnh.Click += (s, e) => {
                using (OpenFileDialog dialog = new OpenFileDialog { Filter = "Hình ảnh (*.jpg;*.png)|*.jpg;*.png", Title = "Chọn ảnh lỗi" })
                {
                    if (dialog.ShowDialog() == DialogResult.OK)
                        picAnhLoi.Image = Image.FromFile(dialog.FileName);
                }
            };

            btnGui.Click += (s, e) => {
                if (string.IsNullOrWhiteSpace(txtMaPhieu.Text))
                {
                    MessageBox.Show("Vui lòng nhập Mã phiếu!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtMaPhieu.Focus();
                    return;
                }
                if (string.IsNullOrWhiteSpace(txtNguoiYeuCau.Text))
                {
                    MessageBox.Show("Vui lòng nhập Người yêu cầu!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtNguoiYeuCau.Focus();
                    return;
                }

                string uuTien = rdoKhanCap.Checked ? "Khẩn cấp" : (rdoTrungBinh.Checked ? "Trung bình" : "Thấp");

                List<string> thietBi = new List<string>();
                if (chkMayTinhBan.Checked) thietBi.Add("Máy tính bàn");
                if (chkLaptop.Checked) thietBi.Add("Laptop");
                if (chkMayIn.Checked) thietBi.Add("Máy in");
                if (chkDienThoai.Checked) thietBi.Add("Điện thoại");

                string strThietBi = thietBi.Count > 0 ? string.Join(", ", thietBi) : "Không có";
                string statusAnh = picAnhLoi.Image != null ? "Đã đính kèm ảnh" : "Chưa có ảnh";

                string summary = $"--- THÔNG TIN PHIẾU YÊU CẦU IT ---\n\n" +
                                 $"• Mã phiếu: {txtMaPhieu.Text.Trim()}\n" +
                                 $"• Người yêu cầu: {txtNguoiYeuCau.Text.Trim()}\n" +
                                 $"• Ngày ghi nhận: {dtpNgayGhiNhan.Value:dd/MM/yyyy HH:mm}\n" +
                                 $"• Mức độ ưu tiên: {uuTien}\n" +
                                 $"• Loại sự cố: {cboLoaiSuCo.SelectedItem}\n" +
                                 $"• Thiết bị ảnh hưởng: {strThietBi}\n" +
                                 $"• Trạng thái ảnh: {statusAnh}";

                MessageBox.Show(summary, "Xác nhận gửi yêu cầu", MessageBoxButtons.OK, MessageBoxIcon.Information);
            };

            btnNhapLai.Click += (s, e) => {
                txtMaPhieu.Clear();
                txtNguoiYeuCau.Clear();
                dtpNgayGhiNhan.Value = DateTime.Now;
                rdoThap.Checked = true;
                cboLoaiSuCo.SelectedIndex = 0;
                chkMayTinhBan.Checked = chkLaptop.Checked = chkMayIn.Checked = chkDienThoai.Checked = false;
                if (picAnhLoi.Image != null)
                {
                    picAnhLoi.Image.Dispose();
                    picAnhLoi.Image = null;
                }
                txtMaPhieu.Focus();
            };
        }
    }
}