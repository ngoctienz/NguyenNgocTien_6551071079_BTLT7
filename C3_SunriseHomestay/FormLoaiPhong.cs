using Microsoft.EntityFrameworkCore;
using SunriseHomestay.Models;

namespace C3_SunriseHomestay
{
    public partial class FormLoaiPhong : Form
    {
        private SunriseHomestayContext db = new SunriseHomestayContext();
        private int selectedMaLoai = 0;

        public FormLoaiPhong()
        {
            InitializeComponent();
        }

        private async void FormLoaiPhong_Load(object sender, EventArgs e)
        {
            dgvLoaiPhong.AutoGenerateColumns = false;
            await LoadDataAsync();
        }

        // Tải danh sách loại phòng lên DataGridView
        public async Task LoadDataAsync()
        {
            try
            {
                var list = await db.LoaiPhongs.AsNoTracking().ToListAsync();
                dgvLoaiPhong.DataSource = list;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Đổ dữ liệu khi chọn dòng trên DataGridView
        private void dgvLoaiPhong_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvLoaiPhong.SelectedRows.Count > 0)
            {
                var row = dgvLoaiPhong.SelectedRows[0];
                if (row.DataBoundItem is LoaiPhong lp)
                {
                    selectedMaLoai = lp.MaLoai;
                    txtMaLoai.Text = lp.MaLoai.ToString();
                    txtTenLoai.Text = lp.TenLoai ?? "";
                    txtGiaMoiDem.Text = lp.GiaMoiDem?.ToString("0.##") ?? "";
                    txtMoTa.Text = lp.MoTa ?? "";
                }
            }
        }

        // Thêm mới Loại phòng
        private async void btnThem_Click(object sender, EventArgs e)
        {
            string tenLoai = txtTenLoai.Text.Trim();
            if (string.IsNullOrWhiteSpace(tenLoai))
            {
                MessageBox.Show("Tên loại phòng không được để trống!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTenLoai.Focus();
                return;
            }

            // Kiểm tra trùng tên loại phòng
            bool exists = await db.LoaiPhongs.AnyAsync(x => x.TenLoai.ToLower() == tenLoai.ToLower());
            if (exists)
            {
                MessageBox.Show("Tên loại phòng này đã tồn tại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTenLoai.Focus();
                return;
            }

            decimal? giaMoiDem = null;
            if (!string.IsNullOrWhiteSpace(txtGiaMoiDem.Text))
            {
                if (!decimal.TryParse(txtGiaMoiDem.Text.Trim(), out decimal gia) || gia < 0)
                {
                    MessageBox.Show("Giá mỗi đêm phải là số hợp lệ >= 0!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtGiaMoiDem.Focus();
                    return;
                }
                giaMoiDem = gia;
            }

            try
            {
                LoaiPhong lp = new LoaiPhong
                {
                    TenLoai = tenLoai,
                    GiaMoiDem = giaMoiDem,
                    MoTa = string.IsNullOrWhiteSpace(txtMoTa.Text) ? null : txtMoTa.Text.Trim()
                };

                db.LoaiPhongs.Add(lp);
                await db.SaveChangesAsync();

                MessageBox.Show("Thêm loại phòng thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                await LoadDataAsync();
                LamMoi();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi thêm loại phòng: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Cập nhật Loại phòng
        private async void btnSua_Click(object sender, EventArgs e)
        {
            if (selectedMaLoai <= 0)
            {
                MessageBox.Show("Vui lòng chọn loại phòng cần sửa từ danh sách!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string tenLoai = txtTenLoai.Text.Trim();
            if (string.IsNullOrWhiteSpace(tenLoai))
            {
                MessageBox.Show("Tên loại phòng không được để trống!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTenLoai.Focus();
                return;
            }

            // Kiểm tra trùng tên với loại phòng khác
            bool exists = await db.LoaiPhongs.AnyAsync(x => x.TenLoai.ToLower() == tenLoai.ToLower() && x.MaLoai != selectedMaLoai);
            if (exists)
            {
                MessageBox.Show("Tên loại phòng này đã tồn tại ở bản ghi khác!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTenLoai.Focus();
                return;
            }

            decimal? giaMoiDem = null;
            if (!string.IsNullOrWhiteSpace(txtGiaMoiDem.Text))
            {
                if (!decimal.TryParse(txtGiaMoiDem.Text.Trim(), out decimal gia) || gia < 0)
                {
                    MessageBox.Show("Giá mỗi đêm phải là số hợp lệ >= 0!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtGiaMoiDem.Focus();
                    return;
                }
                giaMoiDem = gia;
            }

            try
            {
                var lp = await db.LoaiPhongs.FindAsync(selectedMaLoai);
                if (lp != null)
                {
                    lp.TenLoai = tenLoai;
                    lp.GiaMoiDem = giaMoiDem;
                    lp.MoTa = string.IsNullOrWhiteSpace(txtMoTa.Text) ? null : txtMoTa.Text.Trim();

                    await db.SaveChangesAsync();
                    MessageBox.Show("Cập nhật loại phòng thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    await LoadDataAsync();
                }
                else
                {
                    MessageBox.Show("Không tìm thấy loại phòng để cập nhật!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi cập nhật loại phòng: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Xóa Loại phòng
        private async void btnXoa_Click(object sender, EventArgs e)
        {
            if (selectedMaLoai <= 0)
            {
                MessageBox.Show("Vui lòng chọn loại phòng cần xóa từ danh sách!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var dr = MessageBox.Show($"Bạn có chắc chắn muốn xóa loại phòng '{txtTenLoai.Text}'?",
                "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (dr != DialogResult.Yes) return;

            try
            {
                var lp = await db.LoaiPhongs.FindAsync(selectedMaLoai);
                if (lp != null)
                {
                    db.LoaiPhongs.Remove(lp);
                    await db.SaveChangesAsync();

                    MessageBox.Show("Xóa loại phòng thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    await LoadDataAsync();
                    LamMoi();
                }
                else
                {
                    MessageBox.Show("Không tìm thấy loại phòng cần xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (DbUpdateException)
            {
                MessageBox.Show("Không thể xóa loại phòng này vì đang có phòng trực thuộc!", "Cảnh báo ràng buộc", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi xóa: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Làm mới form
        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            LamMoi();
        }

        private void LamMoi()
        {
            selectedMaLoai = 0;
            txtMaLoai.Clear();
            txtTenLoai.Clear();
            txtGiaMoiDem.Clear();
            txtMoTa.Clear();
            txtTenLoai.Focus();
        }
    }
}
