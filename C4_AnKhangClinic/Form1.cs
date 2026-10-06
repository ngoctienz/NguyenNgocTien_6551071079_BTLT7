using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.EntityFrameworkCore;
using AnKhangClinic.Models;

namespace C4_AnKhangClinic
{
    public partial class Form1 : Form
    {
        private readonly AnKhangClinicContext _context;
        private int? _selectedMaLich = null;

        public Form1()
        {
            InitializeComponent();
            _context = new AnKhangClinicContext();
        }

        private async void Form1_Load(object sender, EventArgs e)
        {
            dtpNgayKham.Value = DateTime.Today;
            dtpGioKham.Value = DateTime.Now;
            dtpTuNgay.Value = DateTime.Today.AddDays(-30);
            dtpDenNgay.Value = DateTime.Today.AddDays(30);

            if (cboTrangThai.Items.Count > 0)
                cboTrangThai.SelectedIndex = 0;

            await LoadComboBoxBacSiAsync();
            await LoadDanhSachLichKhamAsync();
        }

        private async Task LoadComboBoxBacSiAsync()
        {
            var dsBacSi = await _context.BacSis
                .Select(b => new
                {
                    MaBs = b.MaBs,
                    Display = $"BS. {b.HoTen} - {b.ChuyenKhoa}"
                })
                .ToListAsync();

            cboBacSi.DataSource = dsBacSi;
            cboBacSi.DisplayMember = "Display";
            cboBacSi.ValueMember = "MaBs";

            var locList = new List<object>
            {
                new { MaBs = 0, Display = "-- Tất cả bác sĩ --" }
            };
            locList.AddRange(dsBacSi.Cast<object>());

            cboLocBacSi.DataSource = locList;
            cboLocBacSi.DisplayMember = "Display";
            cboLocBacSi.ValueMember = "MaBs";
        }

        private async Task LoadDanhSachLichKhamAsync(IQueryable<LichKham>? customQuery = null)
        {
            var query = customQuery ?? _context.LichKhams.Include(x => x.MaBsNavigation);

            var data = await query
                .OrderByDescending(x => x.NgayKham)
                .ThenBy(x => x.GioKham)
                .Select(x => new
                {
                    MaLich = x.MaLich,
                    TenBenhNhan = x.TenBenhNhan,
                    SDT = x.Sdt,
                    NgayKham = x.NgayKham.HasValue ? x.NgayKham.Value.ToString("dd/MM/yyyy") : "",
                    GioKham = x.GioKham,
                    BacSi = x.MaBsNavigation != null ? $"BS. {x.MaBsNavigation.HoTen} - {x.MaBsNavigation.ChuyenKhoa}" : "",
                    ChuyenKhoa = x.MaBsNavigation != null ? x.MaBsNavigation.ChuyenKhoa : "",
                    TrangThai = x.TrangThai,
                    RawNgayKham = x.NgayKham,
                    MaBs = x.MaBs
                })
                .ToListAsync();

            dgvLichKham.DataSource = data;
            ConfigureDgvColumns();
        }

        private void ConfigureDgvColumns()
        {
            if (dgvLichKham.Columns["MaLich"] != null)
                dgvLichKham.Columns["MaLich"].HeaderText = "Mã lịch";
            if (dgvLichKham.Columns["TenBenhNhan"] != null)
                dgvLichKham.Columns["TenBenhNhan"].HeaderText = "Tên bệnh nhân";
            if (dgvLichKham.Columns["SDT"] != null)
                dgvLichKham.Columns["SDT"].HeaderText = "SĐT";
            if (dgvLichKham.Columns["NgayKham"] != null)
                dgvLichKham.Columns["NgayKham"].HeaderText = "Ngày khám";
            if (dgvLichKham.Columns["GioKham"] != null)
                dgvLichKham.Columns["GioKham"].HeaderText = "Giờ khám";
            if (dgvLichKham.Columns["BacSi"] != null)
                dgvLichKham.Columns["BacSi"].HeaderText = "Bác sĩ";
            if (dgvLichKham.Columns["ChuyenKhoa"] != null)
                dgvLichKham.Columns["ChuyenKhoa"].HeaderText = "Chuyên khoa";
            if (dgvLichKham.Columns["TrangThai"] != null)
                dgvLichKham.Columns["TrangThai"].HeaderText = "Trạng thái";

            if (dgvLichKham.Columns["RawNgayKham"] != null)
                dgvLichKham.Columns["RawNgayKham"].Visible = false;
            if (dgvLichKham.Columns["MaBs"] != null)
                dgvLichKham.Columns["MaBs"].Visible = false;
        }

        private void dgvLichKham_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex >= dgvLichKham.Rows.Count) return;

            var row = dgvLichKham.Rows[e.RowIndex];
            _selectedMaLich = Convert.ToInt32(row.Cells["MaLich"].Value);
            txtTenBenhNhan.Text = row.Cells["TenBenhNhan"].Value?.ToString() ?? "";
            txtSDT.Text = row.Cells["SDT"].Value?.ToString() ?? "";

            if (row.Cells["RawNgayKham"].Value is DateOnly d)
            {
                dtpNgayKham.Value = d.ToDateTime(TimeOnly.MinValue);
            }
            else if (DateTime.TryParse(row.Cells["NgayKham"].Value?.ToString(), out DateTime dt))
            {
                dtpNgayKham.Value = dt;
            }

            string gioStr = row.Cells["GioKham"].Value?.ToString() ?? "";
            if (TimeOnly.TryParse(gioStr, out TimeOnly t))
            {
                dtpGioKham.Value = DateTime.Today.Add(t.ToTimeSpan());
            }
            else if (DateTime.TryParse(gioStr, out DateTime dtGio))
            {
                dtpGioKham.Value = dtGio;
            }

            if (row.Cells["MaBs"].Value is int maBs)
            {
                cboBacSi.SelectedValue = maBs;
            }

            string trangThai = row.Cells["TrangThai"].Value?.ToString() ?? "Chờ khám";
            int idx = cboTrangThai.Items.IndexOf(trangThai);
            if (idx >= 0) cboTrangThai.SelectedIndex = idx;
        }

        private bool ValidateInput()
        {
            if (string.IsNullOrWhiteSpace(txtTenBenhNhan.Text))
            {
                MessageBox.Show("Vui lòng nhập tên bệnh nhân!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTenBenhNhan.Focus();
                return false;
            }

            if (cboBacSi.SelectedValue == null || (int)cboBacSi.SelectedValue <= 0)
            {
                MessageBox.Show("Vui lòng chọn bác sĩ!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboBacSi.Focus();
                return false;
            }

            if (dtpNgayKham.Value.Date < DateTime.Today)
            {
                MessageBox.Show("Không được đặt lịch khám vào ngày trong quá khứ!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                dtpNgayKham.Focus();
                return false;
            }

            return true;
        }

        private async void btnThem_Click(object sender, EventArgs e)
        {
            if (!ValidateInput()) return;

            var lk = new LichKham
            {
                TenBenhNhan = txtTenBenhNhan.Text.Trim(),
                Sdt = txtSDT.Text.Trim(),
                NgayKham = DateOnly.FromDateTime(dtpNgayKham.Value.Date),
                GioKham = dtpGioKham.Value.ToString("HH:mm"),
                MaBs = (int)cboBacSi.SelectedValue,
                TrangThai = cboTrangThai.SelectedItem?.ToString() ?? "Chờ khám"
            };

            _context.LichKhams.Add(lk);
            await _context.SaveChangesAsync();
            MessageBox.Show("Thêm lịch khám thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

            LamMoiForm();
            await LoadDanhSachLichKhamAsync();
        }

        private async void btnSua_Click(object sender, EventArgs e)
        {
            if (_selectedMaLich == null)
            {
                MessageBox.Show("Vui lòng chọn lịch khám cần sửa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!ValidateInput()) return;

            var lk = await _context.LichKhams.FindAsync(_selectedMaLich.Value);
            if (lk != null)
            {
                lk.TenBenhNhan = txtTenBenhNhan.Text.Trim();
                lk.Sdt = txtSDT.Text.Trim();
                lk.NgayKham = DateOnly.FromDateTime(dtpNgayKham.Value.Date);
                lk.GioKham = dtpGioKham.Value.ToString("HH:mm");
                lk.MaBs = (int)cboBacSi.SelectedValue;
                lk.TrangThai = cboTrangThai.SelectedItem?.ToString() ?? "Chờ khám";

                await _context.SaveChangesAsync();
                MessageBox.Show("Cập nhật lịch khám thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                LamMoiForm();
                await LoadDanhSachLichKhamAsync();
            }
        }

        private async void btnXoa_Click(object sender, EventArgs e)
        {
            if (_selectedMaLich == null)
            {
                MessageBox.Show("Vui lòng chọn lịch khám cần xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var confirm = MessageBox.Show(
                "Bạn có chắc chắn muốn xóa lịch khám này?",
                "Xác nhận",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (confirm == DialogResult.Yes)
            {
                var lk = await _context.LichKhams.FindAsync(_selectedMaLich.Value);
                if (lk != null)
                {
                    _context.LichKhams.Remove(lk);
                    await _context.SaveChangesAsync();
                    MessageBox.Show("Xóa lịch khám thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    LamMoiForm();
                    await LoadDanhSachLichKhamAsync();
                }
            }
        }

        private async void btnLamMoi_Click(object sender, EventArgs e)
        {
            LamMoiForm();
            await LoadDanhSachLichKhamAsync();
        }

        private async void btnTimKiem_Click(object sender, EventArgs e)
        {
            var tuNgay = DateOnly.FromDateTime(dtpTuNgay.Value.Date);
            var denNgay = DateOnly.FromDateTime(dtpDenNgay.Value.Date);

            if (tuNgay > denNgay)
            {
                MessageBox.Show("Từ ngày không được lớn hơn Đến ngày!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int selectedMaBs = (cboLocBacSi.SelectedValue is int id) ? id : 0;

            IQueryable<LichKham> query = _context.LichKhams.Include(x => x.MaBsNavigation);

            if (selectedMaBs > 0)
            {
                query = query.Where(x => x.NgayKham >= tuNgay && x.NgayKham <= denNgay && x.MaBs == selectedMaBs);
            }
            else
            {
                query = query.Where(x => x.NgayKham >= tuNgay && x.NgayKham <= denNgay);
            }

            await LoadDanhSachLichKhamAsync(query);
        }

        private async void btnQuanLyBacSi_Click(object sender, EventArgs e)
        {
            using (var formBS = new FormBacSi())
            {
                formBS.ShowDialog();
            }
            await LoadComboBoxBacSiAsync();
            await LoadDanhSachLichKhamAsync();
        }

        private void LamMoiForm()
        {
            _selectedMaLich = null;
            txtTenBenhNhan.Clear();
            txtSDT.Clear();
            dtpNgayKham.Value = DateTime.Today;
            dtpGioKham.Value = DateTime.Now;
            if (cboBacSi.Items.Count > 0) cboBacSi.SelectedIndex = 0;
            if (cboTrangThai.Items.Count > 0) cboTrangThai.SelectedIndex = 0;
            dgvLichKham.ClearSelection();
        }
    }
}
