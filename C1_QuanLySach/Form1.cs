using C1_QuanLySach.Models;
using Microsoft.EntityFrameworkCore;

namespace C1_QuanLySach
{
    public partial class Form1 : Form
    {
        private QuanLySachContext db = new QuanLySachContext();

        public Form1()
        {
            InitializeComponent();
        }

        private async void Form1_Load(object sender, EventArgs e)
        {
            dgvSach.AutoGenerateColumns = false;
            await LoadDataAsync();
        }

        // Tải dữ liệu lên DataGridView
        private async Task LoadDataAsync()
        {
            try
            {
                var list = await db.TheLoaiSach.AsNoTracking().ToListAsync();
                dgvSach.DataSource = list;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // 3. Sự kiện SelectionChanged của DataGridView: tự động đổ dữ liệu lên các điều khiển
        private void dgvSach_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvSach.SelectedRows.Count > 0)
            {
                var row = dgvSach.SelectedRows[0];
                if (row.DataBoundItem is TheLoaiSach tl)
                {
                    txtMaTL.Text = tl.MaTL.ToString();
                    txtTenTL.Text = tl.TenTheLoai ?? "";
                    txtMoTa.Text = tl.MoTa ?? "";
                    lblNgayTao.Text = "Ngày tạo: " + (tl.NgayTao.HasValue ? tl.NgayTao.Value.ToString("dd/MM/yyyy HH:mm:ss") : "");
                }
                else
                {
                    txtMaTL.Text = row.Cells["cMaTL"].Value?.ToString() ?? "";
                    txtTenTL.Text = row.Cells["cTenTL"].Value?.ToString() ?? "";
                    txtMoTa.Text = row.Cells["cMoTa"].Value?.ToString() ?? "";
                    var ngayTaoVal = row.Cells["cNgayTao"].Value;
                    lblNgayTao.Text = "Ngày tạo: " + (ngayTaoVal != null && DateTime.TryParse(ngayTaoVal.ToString(), out DateTime dt)
                        ? dt.ToString("dd/MM/yyyy HH:mm:ss") : "");
                }
            }
        }

        // 4 & 7. Chức năng Thêm mới
        private async void btnThem_Click(object sender, EventArgs e)
        {
            string tenTL = txtTenTL.Text.Trim();

            // Validate: Không được bỏ trống Tên thể loại
            if (string.IsNullOrWhiteSpace(tenTL))
            {
                MessageBox.Show("Tên thể loại không được để trống!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTenTL.Focus();
                return;
            }

            // Validate: Không cho phép trùng Tên thể loại khi Thêm mới
            bool exists = await db.TheLoaiSach.AnyAsync(x => x.TenTheLoai.ToLower() == tenTL.ToLower());
            if (exists)
            {
                MessageBox.Show("Tên thể loại đã tồn tại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTenTL.Focus();
                return;
            }

            try
            {
                TheLoaiSach newTLS = new TheLoaiSach
                {
                    TenTheLoai = tenTL,
                    MoTa = string.IsNullOrWhiteSpace(txtMoTa.Text) ? null : txtMoTa.Text.Trim(),
                    SoLuongSach = 0,
                    NgayTao = DateTime.Now
                };

                db.TheLoaiSach.Add(newTLS);
                await db.SaveChangesAsync();

                MessageBox.Show("Thêm thể loại thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                await LoadDataAsync();
                LamMoi();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi thêm thể loại: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // 4 & 7. Chức năng Cập nhật (Sửa)
        private async void btnSua_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtMaTL.Text) || !int.TryParse(txtMaTL.Text, out int maTL))
            {
                MessageBox.Show("Vui lòng chọn thể loại cần sửa từ danh sách!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string tenTL = txtTenTL.Text.Trim();

            // Validate: Không được bỏ trống Tên thể loại
            if (string.IsNullOrWhiteSpace(tenTL))
            {
                MessageBox.Show("Tên thể loại không được để trống!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTenTL.Focus();
                return;
            }

            // Validate: Không trùng tên với thể loại khác
            bool exists = await db.TheLoaiSach.AnyAsync(x => x.TenTheLoai.ToLower() == tenTL.ToLower() && x.MaTL != maTL);
            if (exists)
            {
                MessageBox.Show("Tên thể loại đã tồn tại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTenTL.Focus();
                return;
            }

            try
            {
                var tl = await db.TheLoaiSach.FindAsync(maTL);
                if (tl != null)
                {
                    tl.TenTheLoai = tenTL;
                    tl.MoTa = string.IsNullOrWhiteSpace(txtMoTa.Text) ? null : txtMoTa.Text.Trim();

                    await db.SaveChangesAsync();

                    MessageBox.Show("Cập nhật thể loại thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    await LoadDataAsync();
                }
                else
                {
                    MessageBox.Show("Không tìm thấy thể loại cần sửa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi cập nhật thể loại: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // 5. Chức năng Xóa (Xác nhận Yes/No, bắt lỗi DbUpdateException khi bị ràng buộc khóa ngoại)
        private async void btnXoa_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtMaTL.Text) || !int.TryParse(txtMaTL.Text, out int maTL))
            {
                MessageBox.Show("Vui lòng chọn thể loại cần xóa từ danh sách!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var dialogResult = MessageBox.Show(
                "Bạn có chắc chắn muốn xóa thể loại này không?",
                "Xác nhận xóa",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (dialogResult == DialogResult.Yes)
            {
                try
                {
                    var tl = await db.TheLoaiSach.FindAsync(maTL);
                    if (tl != null)
                    {
                        db.TheLoaiSach.Remove(tl);
                        await db.SaveChangesAsync();

                        MessageBox.Show("Xóa thể loại thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        await LoadDataAsync();
                        LamMoi();
                    }
                    else
                    {
                        MessageBox.Show("Không tìm thấy thể loại cần xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                catch (DbUpdateException)
                {
                    db.ChangeTracker.Clear();
                    MessageBox.Show(
                        "Không thể xóa vì thể loại này đang được sách khác tham chiếu!",
                        "Lỗi ràng buộc dữ liệu",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi xóa thể loại: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // Làm mới dữ liệu và ô nhập
        private async void btnLamMoi_Click(object sender, EventArgs e)
        {
            LamMoi();
            await LoadDataAsync();
        }

        private void LamMoi()
        {
            txtMaTL.Text = "";
            txtTenTL.Text = "";
            txtMoTa.Text = "";
            txtTimKiem.Text = "";
            lblNgayTao.Text = "Ngày tạo:";
            dgvSach.ClearSelection();
        }

        // 6. Tìm kiếm theo Tên thể loại dùng LINQ (Where + Contains)
        private async void btnTimKiem_Click(object sender, EventArgs e)
        {
            string keyword = txtTimKiem.Text.Trim();
            var query = db.TheLoaiSach.AsNoTracking();

            if (!string.IsNullOrEmpty(keyword))
            {
                query = query.Where(tl => tl.TenTheLoai.Contains(keyword));
            }

            var list = await query.ToListAsync();
            dgvSach.DataSource = list;
        }
    }
}
