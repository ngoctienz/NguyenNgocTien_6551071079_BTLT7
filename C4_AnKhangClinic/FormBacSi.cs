using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.EntityFrameworkCore;
using AnKhangClinic.Models;

namespace C4_AnKhangClinic
{
    public partial class FormBacSi : Form
    {
        private readonly AnKhangClinicContext _context;
        private int? _selectedMaBs = null;

        public FormBacSi()
        {
            InitializeComponent();
            _context = new AnKhangClinicContext();
        }

        private async void FormBacSi_Load(object sender, EventArgs e)
        {
            await LoadDataAsync();
        }

        private async Task LoadDataAsync()
        {
            var list = await _context.BacSis
                .Select(b => new
                {
                    b.MaBs,
                    b.HoTen,
                    b.ChuyenKhoa,
                    b.Sdt
                })
                .ToListAsync();

            dgvBacSi.DataSource = list;

            if (dgvBacSi.Columns["MaBs"] != null)
                dgvBacSi.Columns["MaBs"].HeaderText = "Mã BS";
            if (dgvBacSi.Columns["HoTen"] != null)
                dgvBacSi.Columns["HoTen"].HeaderText = "Họ tên";
            if (dgvBacSi.Columns["ChuyenKhoa"] != null)
                dgvBacSi.Columns["ChuyenKhoa"].HeaderText = "Chuyên khoa";
            if (dgvBacSi.Columns["Sdt"] != null)
                dgvBacSi.Columns["Sdt"].HeaderText = "SĐT";
        }

        private void dgvBacSi_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex >= dgvBacSi.Rows.Count) return;

            var row = dgvBacSi.Rows[e.RowIndex];
            _selectedMaBs = Convert.ToInt32(row.Cells["MaBs"].Value);
            txtHoTen.Text = row.Cells["HoTen"].Value?.ToString() ?? "";
            txtChuyenKhoa.Text = row.Cells["ChuyenKhoa"].Value?.ToString() ?? "";
            txtSDT.Text = row.Cells["Sdt"].Value?.ToString() ?? "";
        }

        private async void btnThem_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtHoTen.Text))
            {
                MessageBox.Show("Vui lòng nhập họ tên bác sĩ!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtHoTen.Focus();
                return;
            }

            var bs = new BacSi
            {
                HoTen = txtHoTen.Text.Trim(),
                ChuyenKhoa = txtChuyenKhoa.Text.Trim(),
                Sdt = txtSDT.Text.Trim()
            };

            _context.BacSis.Add(bs);
            await _context.SaveChangesAsync();
            MessageBox.Show("Thêm bác sĩ thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

            LamMoi();
            await LoadDataAsync();
        }

        private async void btnSua_Click(object sender, EventArgs e)
        {
            if (_selectedMaBs == null)
            {
                MessageBox.Show("Vui lòng chọn bác sĩ cần sửa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(txtHoTen.Text))
            {
                MessageBox.Show("Vui lòng nhập họ tên bác sĩ!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtHoTen.Focus();
                return;
            }

            var bs = await _context.BacSis.FindAsync(_selectedMaBs.Value);
            if (bs != null)
            {
                bs.HoTen = txtHoTen.Text.Trim();
                bs.ChuyenKhoa = txtChuyenKhoa.Text.Trim();
                bs.Sdt = txtSDT.Text.Trim();

                await _context.SaveChangesAsync();
                MessageBox.Show("Cập nhật thông tin bác sĩ thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                LamMoi();
                await LoadDataAsync();
            }
        }

        private async void btnXoa_Click(object sender, EventArgs e)
        {
            if (_selectedMaBs == null)
            {
                MessageBox.Show("Vui lòng chọn bác sĩ cần xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var confirm = MessageBox.Show(
                "Bạn có chắc chắn muốn xóa bác sĩ này?",
                "Xác nhận",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (confirm != DialogResult.Yes) return;

            bool coLichKham = await _context.LichKhams.AnyAsync(l => l.MaBs == _selectedMaBs.Value);
            if (coLichKham)
            {
                MessageBox.Show("Không thể xóa bác sĩ đã có lịch hẹn khám!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var bs = await _context.BacSis.FindAsync(_selectedMaBs.Value);
            if (bs != null)
            {
                _context.BacSis.Remove(bs);
                await _context.SaveChangesAsync();
                MessageBox.Show("Xóa bác sĩ thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                LamMoi();
                await LoadDataAsync();
            }
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            LamMoi();
        }

        private void LamMoi()
        {
            _selectedMaBs = null;
            txtHoTen.Clear();
            txtChuyenKhoa.Clear();
            txtSDT.Clear();
            dgvBacSi.ClearSelection();
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
