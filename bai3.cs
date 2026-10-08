using System;
using System.Drawing;
using System.Windows.Forms;

namespace TechMartProductManager
{
    public class FormBai3 : Form
    {
        private TextBox txtMaVT = new TextBox { Location = new Point(110, 30), Width = 180 };
        private TextBox txtTenVT = new TextBox { Location = new Point(110, 70), Width = 180 };
        private ComboBox cboDVT = new ComboBox { Location = new Point(110, 110), Width = 180, DropDownStyle = ComboBoxStyle.DropDownList };
        private TextBox txtDonGia = new TextBox { Location = new Point(110, 150), Width = 180 };

        private Button btnThem = new Button { Text = "Thêm mới", Location = new Point(15, 200), Size = new Size(130, 35), BackColor = Color.LightSkyBlue, Font = new Font("Segoe UI", 9, FontStyle.Bold) };
        private Button btnCapNhat = new Button { Text = "Cập nhật", Location = new Point(160, 200), Size = new Size(130, 35) };
        private Button btnXoa = new Button { Text = "Xóa dòng", Location = new Point(15, 245), Size = new Size(130, 35), BackColor = Color.MistyRose };
        private Button btnXoaTatCa = new Button { Text = "Xóa toàn bộ", Location = new Point(160, 245), Size = new Size(130, 35), BackColor = Color.LightCoral };

        private ListView lsvVatTu = new ListView
        {
            Location = new Point(15, 25),
            Size = new Size(450, 255),
            View = View.Details,
            FullRowSelect = true,
            GridLines = true,
            MultiSelect = false
        };

        public FormBai3()
        {
            Text = "Bài 3: Quản lý danh mục Vật tư / Linh kiện";
            Size = new Size(820, 350);
            StartPosition = FormStartPosition.CenterScreen;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;

            GroupBox grpNhapLieu = new GroupBox { Text = "Thông tin vật tư", Location = new Point(15, 10), Size = new Size(305, 290) };
            cboDVT.Items.AddRange(new object[] { "Cái", "Bộ", "Kg", "Mét" });
            cboDVT.SelectedIndex = 0;

            grpNhapLieu.Controls.AddRange(new Control[] {
                new Label { Text = "Mã vật tư:", Location = new Point(15, 33), AutoSize = true }, txtMaVT,
                new Label { Text = "Tên vật tư:", Location = new Point(15, 73), AutoSize = true }, txtTenVT,
                new Label { Text = "Đơn vị tính:", Location = new Point(15, 113), AutoSize = true }, cboDVT,
                new Label { Text = "Đơn giá nhập:", Location = new Point(15, 153), AutoSize = true }, txtDonGia,
                btnThem, btnCapNhat, btnXoa, btnXoaTatCa
            });

            GroupBox grpDanhSach = new GroupBox { Text = "Danh sách vật tư / linh kiện", Location = new Point(330, 10), Size = new Size(460, 290) };
            lsvVatTu.Columns.Add("Mã VT", 80);
            lsvVatTu.Columns.Add("Tên VT", 160);
            lsvVatTu.Columns.Add("Đơn vị tính", 90);
            lsvVatTu.Columns.Add("Đơn giá", 110);
            grpDanhSach.Controls.Add(lsvVatTu);

            Controls.AddRange(new Control[] { grpNhapLieu, grpDanhSach });

            btnThem.Click += (s, e) => {
                if (!ValidateInput()) return;

                string maVT = txtMaVT.Text.Trim();

                foreach (ListViewItem item in lsvVatTu.Items)
                {
                    if (item.Text.Equals(maVT, StringComparison.OrdinalIgnoreCase))
                    {
                        MessageBox.Show("Mã vật tư đã tồn tại trong danh sách!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtMaVT.Focus();
                        return;
                    }
                }

                ListViewItem newItem = new ListViewItem(maVT);
                newItem.SubItems.Add(txtTenVT.Text.Trim());
                newItem.SubItems.Add(cboDVT.SelectedItem.ToString());
                newItem.SubItems.Add(double.Parse(txtDonGia.Text.Trim()).ToString("N0"));

                lsvVatTu.Items.Add(newItem);
                ClearInput();
            };

            lsvVatTu.SelectedIndexChanged += (s, e) => {
                if (lsvVatTu.SelectedItems.Count > 0)
                {
                    ListViewItem selected = lsvVatTu.SelectedItems[0];
                    txtMaVT.Text = selected.Text;
                    txtTenVT.Text = selected.SubItems[1].Text;
                    cboDVT.SelectedItem = selected.SubItems[2].Text;
                    txtDonGia.Text = selected.SubItems[3].Text.Replace(",", "").Replace(".", "");
                    txtMaVT.Enabled = false;
                }
            };

            btnCapNhat.Click += (s, e) => {
                if (lsvVatTu.SelectedItems.Count == 0)
                {
                    MessageBox.Show("Vui lòng chọn 1 dòng vật tư trong bảng để cập nhật!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                if (!ValidateInput(true)) return;

                ListViewItem selected = lsvVatTu.SelectedItems[0];
                selected.SubItems[1].Text = txtTenVT.Text.Trim();
                selected.SubItems[2].Text = cboDVT.SelectedItem.ToString();
                selected.SubItems[3].Text = double.Parse(txtDonGia.Text.Trim()).ToString("N0");

                MessageBox.Show("Cập nhật thông tin thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ClearInput();
            };

            btnXoa.Click += (s, e) => {
                if (lsvVatTu.SelectedItems.Count == 0)
                {
                    MessageBox.Show("Vui lòng chọn 1 dòng cần xóa!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                DialogResult result = MessageBox.Show(
                    $"Bạn có chắc chắn muốn xóa vật tư mã '{lsvVatTu.SelectedItems[0].Text}' không?",
                    "Xác nhận xóa",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question
                );

                if (result == DialogResult.Yes)
                {
                    lsvVatTu.Items.Remove(lsvVatTu.SelectedItems[0]);
                    ClearInput();
                }
            };

            btnXoaTatCa.Click += (s, e) => {
                if (lsvVatTu.Items.Count == 0) return;

                DialogResult result = MessageBox.Show(
                    "Bạn có chắc chắn muốn xóa TOÀN BỘ danh sách vật tư không?",
                    "Xác nhận xóa tất cả",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning
                );

                if (result == DialogResult.Yes)
                {
                    lsvVatTu.Items.Clear();
                    ClearInput();
                }
            };
        }

        private bool ValidateInput(bool isUpdate = false)
        {
            if (!isUpdate && string.IsNullOrWhiteSpace(txtMaVT.Text))
            {
                MessageBox.Show("Vui lòng nhập Mã vật tư!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMaVT.Focus();
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtTenVT.Text))
            {
                MessageBox.Show("Vui lòng nhập Tên vật tư!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTenVT.Focus();
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtDonGia.Text) || !double.TryParse(txtDonGia.Text.Trim(), out _))
            {
                MessageBox.Show("Đơn giá phải là số hợp lệ!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDonGia.Focus();
                return false;
            }
            return true;
        }

        private void ClearInput()
        {
            txtMaVT.Clear();
            txtTenVT.Clear();
            cboDVT.SelectedIndex = 0;
            txtDonGia.Clear();
            txtMaVT.Enabled = true;
            lsvVatTu.SelectedItems.Clear();
            txtMaVT.Focus();
        }
    }
}