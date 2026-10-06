using System.Data;
using System.Text.RegularExpressions;
using FitZone.Models;
using Microsoft.EntityFrameworkCore;

namespace C2_FitZone
{
    public partial class Form1 : Form
    {
        private readonly FitZoneContext _context;
        private int _selectedMaHV = 0;

        public Form1()
        {
            InitializeComponent();
            _context = new FitZoneContext();
        }

        private async void Form1_Load(object sender, EventArgs e)
        {
            cboHangThanhVien.SelectedIndex = 0;
            cboLocHangThanhVien.SelectedIndex = 0;
            dtpNgaySinh.Value = DateTime.Today.AddYears(-20);
            await LoadDataAsync();
        }

        private async Task LoadDataAsync(IQueryable<HoiVien>? customQuery = null)
        {
            var query = customQuery ?? _context.HoiViens.AsNoTracking();

            var list = await query
                .OrderBy(h => h.MaHv)
                .Select(h => new
                {
                    h.MaHv,
                    h.HoTen,
                    GioiTinh = h.GioiTinh == true ? "Nam" : (h.GioiTinh == false ? "Nữ" : ""),
                    NgaySinh = h.NgaySinh.HasValue ? h.NgaySinh.Value.ToString("dd/MM/yyyy") : "",
                    h.Sdt,
                    h.HangThanhVien,
                    TrangThai = h.TrangThai == true ? "Đang hoạt động" : "Tạm ngưng"
                })
                .ToListAsync();

            dgvHoiVien.DataSource = list;

            if (dgvHoiVien.Columns["MaHv"] is DataGridViewColumn colMaHv)
                colMaHv.HeaderText = "Mã HV";
            if (dgvHoiVien.Columns["HoTen"] is DataGridViewColumn colHoTen)
                colHoTen.HeaderText = "Họ tên";
            if (dgvHoiVien.Columns["GioiTinh"] is DataGridViewColumn colGioiTinh)
                colGioiTinh.HeaderText = "Giới tính";
            if (dgvHoiVien.Columns["NgaySinh"] is DataGridViewColumn colNgaySinh)
                colNgaySinh.HeaderText = "Ngày sinh";
            if (dgvHoiVien.Columns["Sdt"] is DataGridViewColumn colSdt)
                colSdt.HeaderText = "SĐT";
            if (dgvHoiVien.Columns["HangThanhVien"] is DataGridViewColumn colHangThanhVien)
                colHangThanhVien.HeaderText = "Hạng thành viên";
            if (dgvHoiVien.Columns["TrangThai"] is DataGridViewColumn colTrangThai)
                colTrangThai.HeaderText = "Trạng thái";
        }

        private void dgvHoiVien_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex >= dgvHoiVien.Rows.Count)
                return;

            var row = dgvHoiVien.Rows[e.RowIndex];
            if (row.Cells["MaHv"].Value == null)
                return;

            _selectedMaHV = Convert.ToInt32(row.Cells["MaHv"].Value);
            var hv = _context.HoiViens.AsNoTracking().FirstOrDefault(h => h.MaHv == _selectedMaHV);
            if (hv == null)
                return;

            txtHoTen.Text = hv.HoTen;
            txtSDT.Text = hv.Sdt ?? "";
            txtEmail.Text = hv.Email ?? "";

            if (hv.GioiTinh == true)
                rdoNam.Checked = true;
            else if (hv.GioiTinh == false)
                rdoNu.Checked = true;
            else
            {
                rdoNam.Checked = false;
                rdoNu.Checked = false;
            }

            if (hv.NgaySinh.HasValue)
            {
                dtpNgaySinh.Value = hv.NgaySinh.Value.ToDateTime(TimeOnly.MinValue);
            }
            else
            {
                dtpNgaySinh.Value = DateTime.Today.AddYears(-20);
            }

            cboHangThanhVien.SelectedItem = hv.HangThanhVien;

            chkTrangThai.Checked = hv.TrangThai == true;
        }

        private bool ValidateInput()
        {
            if (string.IsNullOrWhiteSpace(txtHoTen.Text))
            {
                MessageBox.Show("Họ tên không được để trống!", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtHoTen.Focus();
                return false;
            }

            string sdt = txtSDT.Text.Trim();
            if (string.IsNullOrWhiteSpace(sdt) || !Regex.IsMatch(sdt, @"^\d{9,11}$"))
            {
                MessageBox.Show("Số điện thoại chỉ được chứa chữ số và phải có từ 9 đến 11 ký tự!", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtSDT.Focus();
                return false;
            }

            string email = txtEmail.Text.Trim();
            if (string.IsNullOrWhiteSpace(email) || !email.Contains('@'))
            {
                MessageBox.Show("Email phải chứa ký tự '@'!", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtEmail.Focus();
                return false;
            }

            DateTime ngaySinh = dtpNgaySinh.Value.Date;
            DateTime homNay = DateTime.Today;
            int tuoi = homNay.Year - ngaySinh.Year;
            if (ngaySinh > homNay.AddYears(-tuoi))
            {
                tuoi--;
            }

            if (tuoi < 15)
            {
                MessageBox.Show("Tuổi hội viên (tính từ ngày sinh đến hiện tại) phải từ 15 tuổi trở lên mới được đăng ký!", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                dtpNgaySinh.Focus();
                return false;
            }

            if (cboHangThanhVien.SelectedIndex == -1)
            {
                MessageBox.Show("Vui lòng chọn hạng thành viên!", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboHangThanhVien.Focus();
                return false;
            }

            return true;
        }

        private async void btnThem_Click(object sender, EventArgs e)
        {
            if (!ValidateInput())
                return;

            var hv = new HoiVien
            {
                HoTen = txtHoTen.Text.Trim(),
                GioiTinh = rdoNam.Checked ? true : (rdoNu.Checked ? false : null),
                NgaySinh = DateOnly.FromDateTime(dtpNgaySinh.Value),
                Sdt = txtSDT.Text.Trim(),
                Email = txtEmail.Text.Trim(),
                HangThanhVien = cboHangThanhVien.SelectedItem?.ToString(),
                NgayDangKy = DateTime.Now,
                TrangThai = chkTrangThai.Checked
            };

            _context.HoiViens.Add(hv);
            await _context.SaveChangesAsync();

            MessageBox.Show("Thêm hội viên thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            await LoadDataAsync();
            ResetForm();
        }

        private async void btnSua_Click(object sender, EventArgs e)
        {
            if (_selectedMaHV == 0)
            {
                MessageBox.Show("Vui lòng chọn một dòng hội viên trên bảng để sửa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!ValidateInput())
                return;

            var hv = await _context.HoiViens.FindAsync(_selectedMaHV);
            if (hv == null)
            {
                MessageBox.Show("Không tìm thấy hội viên cần sửa trong cơ sở dữ liệu!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            hv.HoTen = txtHoTen.Text.Trim();
            hv.GioiTinh = rdoNam.Checked ? true : (rdoNu.Checked ? false : null);
            hv.NgaySinh = DateOnly.FromDateTime(dtpNgaySinh.Value);
            hv.Sdt = txtSDT.Text.Trim();
            hv.Email = txtEmail.Text.Trim();
            hv.HangThanhVien = cboHangThanhVien.SelectedItem?.ToString();
            hv.TrangThai = chkTrangThai.Checked;

            await _context.SaveChangesAsync();

            MessageBox.Show("Cập nhật hội viên thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            await LoadDataAsync();
            ResetForm();
        }

        private async void btnXoa_Click(object sender, EventArgs e)
        {
            if (_selectedMaHV == 0)
            {
                MessageBox.Show("Vui lòng chọn một dòng hội viên trên bảng để xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var hv = await _context.HoiViens.FindAsync(_selectedMaHV);
            if (hv == null)
            {
                MessageBox.Show("Không tìm thấy hội viên cần xóa trong cơ sở dữ liệu!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var confirm = MessageBox.Show($"Bạn có chắc chắn muốn xóa hội viên '{hv.HoTen}' (Mã: {hv.MaHv}) không?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirm == DialogResult.Yes)
            {
                _context.HoiViens.Remove(hv);
                await _context.SaveChangesAsync();

                MessageBox.Show("Xóa hội viên thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                await LoadDataAsync();
                ResetForm();
            }
        }

        private async void btnLamMoi_Click(object sender, EventArgs e)
        {
            ResetForm();
            await LoadDataAsync();
        }

        private async void btnTimKiem_Click(object sender, EventArgs e)
        {
            string ten = txtTimKiemHoTen.Text.Trim();
            string hang = cboLocHangThanhVien.SelectedItem?.ToString() ?? "";

            var query = _context.HoiViens.AsNoTracking();

            // Tìm kiếm kết hợp 2 điều kiện cùng lúc: theo Họ tên (Contains) VÀ theo Hạng thành viên (ComboBox lọc riêng)
            // Minh họa LINQ Where với nhiều điều kiện nối bằng toán tử &&
            query = query.Where(hv =>
                (string.IsNullOrEmpty(ten) || hv.HoTen.Contains(ten)) &&
                (string.IsNullOrEmpty(hang) || hang == "Tất cả" || hv.HangThanhVien == hang)
            );

            await LoadDataAsync(query);
        }

        private void ResetForm()
        {
            txtHoTen.Clear();
            txtSDT.Clear();
            txtEmail.Clear();
            rdoNam.Checked = true;
            rdoNu.Checked = false;
            dtpNgaySinh.Value = DateTime.Today.AddYears(-20);
            cboHangThanhVien.SelectedIndex = 0;
            chkTrangThai.Checked = true;
            _selectedMaHV = 0;
            txtTimKiemHoTen.Clear();
            cboLocHangThanhVien.SelectedIndex = 0;
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            _context.Dispose();
            base.OnFormClosing(e);
        }
    }
}
