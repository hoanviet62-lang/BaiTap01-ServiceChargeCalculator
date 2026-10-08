using System;
using System.Drawing;
using System.Windows.Forms;

namespace TechMartProductManager
{
    public class FormBai5 : Form
    {
        private TextBox txtMaDon = new TextBox { Location = new Point(120, 25), Width = 180 };
        private TextBox txtTenKhach = new TextBox { Location = new Point(120, 60), Width = 180 };
        private TextBox txtDiaChi = new TextBox { Location = new Point(120, 95), Width = 180 };
        private ComboBox cboKhuVuc = new ComboBox { Location = new Point(120, 130), Width = 180, DropDownStyle = ComboBoxStyle.DropDownList };
        private RadioButton rdoCOD = new RadioButton { Text = "COD", Location = new Point(120, 165), AutoSize = true, Checked = true };
        private RadioButton rdoBanking = new RadioButton { Text = "Chuyển khoản", Location = new Point(180, 165), AutoSize = true };
        private TextBox txtTongTien = new TextBox { Location = new Point(120, 200), Width = 180 };
        private ComboBox cboTrangThai = new ComboBox { Location = new Point(120, 235), Width = 180, DropDownStyle = ComboBoxStyle.DropDownList };

        private Button btnThem = new Button { Text = "Thêm đơn hàng", Location = new Point(15, 280), Size = new Size(135, 35), BackColor = Color.LightSkyBlue, Font = new Font("Segoe UI", 9, FontStyle.Bold) };
        private Button btnCapNhat = new Button { Text = "Cập nhật", Location = new Point(160, 280), Size = new Size(135, 35) };
        private Button btnXoa = new Button { Text = "Xóa đơn", Location = new Point(15, 325), Size = new Size(135, 35), BackColor = Color.MistyRose };
        private Button btnXoaTatCa = new Button { Text = "Xóa tất cả", Location = new Point(160, 325), Size = new Size(135, 35), BackColor = Color.LightCoral };

        private ListView lsvDonHang = new ListView
        {
            Location = new Point(15, 25),
            Size = new Size(520, 335),
            View = View.Details,
            FullRowSelect = true,
            GridLines = true,
            MultiSelect = false
        };

        public FormBai5()
        {
            Text = "Bài 5: Bảng điều khiển quản lý đơn giao hàng";
            Size = new Size(900, 430);
            StartPosition = FormStartPosition.CenterScreen;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;

            cboKhuVuc.Items.AddRange(new object[] { "Nội thành", "Ngoại thành", "Tỉnh thành khác" });
            cboKhuVuc.SelectedIndex = 0;

            cboTrangThai.Items.AddRange(new object[] { "Chờ xử lý", "Đang giao hàng", "Đã hoàn thành", "Đã hủy" });
            cboTrangThai.SelectedIndex = 0;

            GroupBox grpNhapLieu = new GroupBox { Text = "Thông tin đơn hàng", Location = new Point(15, 10), Size = new Size(310, 375) };
            grpNhapLieu.Controls.AddRange(new Control[] {
                new Label { Text = "Mã đơn hàng:", Location = new Point(15, 28), AutoSize = true }, txtMaDon,
                new Label { Text = "Tên khách hàng:", Location = new Point(15, 63), AutoSize = true }, txtTenKhach,
                new Label { Text = "Địa chỉ giao:", Location = new Point(15, 98), AutoSize = true }, txtDiaChi,
                new Label { Text = "Khu vực:", Location = new Point(15, 133), AutoSize = true }, cboKhuVuc,
                new Label { Text = "Thanh toán:", Location = new Point(15, 167), AutoSize = true }, rdoCOD, rdoBanking,
                new Label { Text = "Tổng tiền:", Location = new Point(15, 203), AutoSize = true }, txtTongTien,
                new Label { Text = "Trạng thái:", Location = new Point(15, 238), AutoSize = true }, cboTrangThai,
                btnThem, btnCapNhat, btnXoa, btnXoaTatCa
            });

            GroupBox grpDanhSach = new GroupBox { Text = "Danh sách đơn giao hàng", Location = new Point(335, 10), Size = new Size(550, 375) };
            lsvDonHang.Columns.Add("Mã đơn", 75);
            lsvDonHang.Columns.Add("Khách hàng", 110);
            lsvDonHang.Columns.Add("Khu vực", 90);
            lsvDonHang.Columns.Add("Thanh toán", 85);
            lsvDonHang.Columns.Add("Tổng tiền", 95);
            lsvDonHang.Columns.Add("Trạng thái", 90);
            grpDanhSach.Controls.Add(lsvDonHang);

            Controls.AddRange(new Control[] { grpNhapLieu, grpDanhSach });

            btnThem.Click += (s, e) => {
                if (!ValidateInput()) return;

                string maDon = txtMaDon.Text.Trim();

                foreach (ListViewItem item in lsvDonHang.Items)
                {
                    if (item.Text.Equals(maDon, StringComparison.OrdinalIgnoreCase))
                    {
                        MessageBox.Show("Mã đơn hàng đã tồn tại trong hệ thống!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtMaDon.Focus();
                        return;
                    }
                }

                ListViewItem newItem = new ListViewItem(maDon);
                newItem.SubItems.Add(txtTenKhach.Text.Trim());
                newItem.SubItems.Add(cboKhuVuc.SelectedItem.ToString());
                newItem.SubItems.Add(rdoCOD.Checked ? "COD" : "Chuyển khoản");
                newItem.SubItems.Add(double.Parse(txtTongTien.Text.Trim()).ToString("N0") + " VNĐ");
                newItem.SubItems.Add(cboTrangThai.SelectedItem.ToString());

                lsvDonHang.Items.Add(newItem);
                ClearInput();
            };

            lsvDonHang.SelectedIndexChanged += (s, e) => {
                if (lsvDonHang.SelectedItems.Count > 0)
                {
                    ListViewItem selected = lsvDonHang.SelectedItems[0];
                    txtMaDon.Text = selected.Text;
                    txtTenKhach.Text = selected.SubItems[1].Text;
                    cboKhuVuc.SelectedItem = selected.SubItems[2].Text;
                    if (selected.SubItems[3].Text == "COD") rdoCOD.Checked = true;
                    else rdoBanking.Checked = true;
                    txtTongTien.Text = selected.SubItems[4].Text.Replace(" VNĐ", "").Replace(",", "").Replace(".", "");
                    cboTrangThai.SelectedItem = selected.SubItems[5].Text;
                    txtMaDon.Enabled = false;
                }
            };

            btnCapNhat.Click += (s, e) => {
                if (lsvDonHang.SelectedItems.Count == 0)
                {
                    MessageBox.Show("Vui lòng chọn 1 đơn hàng trong danh sách để cập nhật!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                if (!ValidateInput(true)) return;

                ListViewItem selected = lsvDonHang.SelectedItems[0];
                selected.SubItems[1].Text = txtTenKhach.Text.Trim();
                selected.SubItems[2].Text = cboKhuVuc.SelectedItem.ToString();
                selected.SubItems[3].Text = rdoCOD.Checked ? "COD" : "Chuyển khoản";
                selected.SubItems[4].Text = double.Parse(txtTongTien.Text.Trim()).ToString("N0") + " VNĐ";
                selected.SubItems[5].Text = cboTrangThai.SelectedItem.ToString();

                MessageBox.Show("Cập nhật đơn hàng thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ClearInput();
            };

            btnXoa.Click += (s, e) => {
                if (lsvDonHang.SelectedItems.Count == 0)
                {
                    MessageBox.Show("Vui lòng chọn 1 đơn hàng cần xóa!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                DialogResult result = MessageBox.Show(
                    $"Bạn có chắc chắn muốn xóa đơn hàng '{lsvDonHang.SelectedItems[0].Text}' không?",
                    "Xác nhận xóa",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question
                );

                if (result == DialogResult.Yes)
                {
                    lsvDonHang.Items.Remove(lsvDonHang.SelectedItems[0]);
                    ClearInput();
                }
            };

            btnXoaTatCa.Click += (s, e) => {
                if (lsvDonHang.Items.Count == 0) return;

                DialogResult result = MessageBox.Show(
                    "Bạn có chắc chắn muốn xóa TOÀN BỘ danh sách đơn hàng không?",
                    "Xác nhận xóa tất cả",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning
                );

                if (result == DialogResult.Yes)
                {
                    lsvDonHang.Items.Clear();
                    ClearInput();
                }
            };
        }

        private bool ValidateInput(bool isUpdate = false)
        {
            if (!isUpdate && string.IsNullOrWhiteSpace(txtMaDon.Text))
            {
                MessageBox.Show("Vui lòng nhập Mã đơn hàng!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMaDon.Focus();
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtTenKhach.Text))
            {
                MessageBox.Show("Vui lòng nhập Tên khách hàng!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTenKhach.Focus();
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtDiaChi.Text))
            {
                MessageBox.Show("Vui lòng nhập Địa chỉ giao hàng!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDiaChi.Focus();
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtTongTien.Text) || !double.TryParse(txtTongTien.Text.Trim(), out _))
            {
                MessageBox.Show("Tổng tiền phải là số hợp lệ!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTongTien.Focus();
                return false;
            }
            return true;
        }

        private void ClearInput()
        {
            txtMaDon.Clear();
            txtTenKhach.Clear();
            txtDiaChi.Clear();
            cboKhuVuc.SelectedIndex = 0;
            rdoCOD.Checked = true;
            txtTongTien.Clear();
            cboTrangThai.SelectedIndex = 0;
            txtMaDon.Enabled = true;
            lsvDonHang.SelectedItems.Clear();
            txtMaDon.Focus();
        }
    }
}