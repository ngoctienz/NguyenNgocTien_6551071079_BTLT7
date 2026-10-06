using Microsoft.EntityFrameworkCore;
using SunriseHomestay.Models;

namespace C3_SunriseHomestay
{
    public partial class Form1 : Form
    {
        private SunriseHomestayContext db = new SunriseHomestayContext();
        private int selectedMaPhong = 0;
        private string? currentImageFileName = null;

        // DTO hiển thị danh sách phòng kèm ảnh thumbnail và tên loại phòng
        public class PhongViewModel
        {
            public int MaPhong { get; set; }
            public Image? AnhThumbnail { get; set; }
            public string SoPhong { get; set; } = string.Empty;
            public int? TangSo { get; set; }
            public string TenLoai { get; set; } = string.Empty;
            public decimal? GiaMoiDem { get; set; }
            public string TinhTrang { get; set; } = string.Empty;
            public string? HinhAnh { get; set; }
            public int? MaLoai { get; set; }
        }

        public Form1()
        {
            InitializeComponent();
        }

        private async void Form1_Load(object sender, EventArgs e)
        {
            dgvPhong.AutoGenerateColumns = false;
            cboTinhTrang.SelectedIndex = 0;

            await LoadLoaiPhongComboBoxAsync();
            await LoadDataAsync();
        }

        // Lấy đường dẫn thư mục Images của ứng dụng (tạo mới nếu chưa có)
        private string GetImageFolderPath()
        {
            string folder = Path.Combine(Application.StartupPath, "Images");
            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }
            return folder;
        }

        // Đọc ảnh không khóa file (không bị lỗi IOException khi cập nhật/xóa)
        private Image? LoadImageWithoutLock(string? fileName)
        {
            if (string.IsNullOrWhiteSpace(fileName))
                return null;

            string fullPath = Path.Combine(GetImageFolderPath(), fileName);
            if (!File.Exists(fullPath))
            {
                // Kiểm tra thêm thư mục project gốc nếu đang chạy trong chế độ Debug
                string projectPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\Images", fileName);
                if (File.Exists(projectPath))
                {
                    fullPath = projectPath;
                }
                else
                {
                    return null;
                }
            }

            try
            {
                using var fs = new FileStream(fullPath, FileMode.Open, FileAccess.Read);
                using var temp = Image.FromStream(fs);
                return new Bitmap(temp);
            }
            catch
            {
                return null;
            }
        }

        // Nạp dữ liệu Loại phòng cho ComboBox ở Form chính và ComboBox Lọc tìm kiếm
        private async Task LoadLoaiPhongComboBoxAsync()
        {
            try
            {
                var listLoaiPhong = await db.LoaiPhongs.AsNoTracking().ToListAsync();

                // Nạp cho cboLoaiPhong (nhập liệu)
                cboLoaiPhong.DataSource = null;
                cboLoaiPhong.DataSource = listLoaiPhong;
                cboLoaiPhong.DisplayMember = "TenLoai";
                cboLoaiPhong.ValueMember = "MaLoai";

                // Nạp cho cboLocLoaiPhong (tìm kiếm)
                var listLoc = new List<LoaiPhong>
                {
                    new LoaiPhong { MaLoai = 0, TenLoai = "lọc theo loại phòng" }
                };
                listLoc.AddRange(listLoaiPhong);

                cboLocLoaiPhong.DataSource = null;
                cboLocLoaiPhong.DataSource = listLoc;
                cboLocLoaiPhong.DisplayMember = "TenLoai";
                cboLocLoaiPhong.ValueMember = "MaLoai";
                cboLocLoaiPhong.SelectedIndex = 0;

                // Nạp danh sách cho cboLocTinhTrang (tìm kiếm)
                cboLocTinhTrang.Items.Clear();
                cboLocTinhTrang.Items.Add("lọc theo tình trạng");
                cboLocTinhTrang.Items.Add("Trống");
                cboLocTinhTrang.Items.Add("Đang ở");
                cboLocTinhTrang.Items.Add("Đang dọn");
                cboLocTinhTrang.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải loại phòng: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // 5. DataGridView hiển thị danh sách Phòng kèm ảnh thumbnail và tên loại phòng qua Include (Eager Loading)
        private async Task LoadDataAsync(IQueryable<Phong>? customQuery = null)
        {
            try
            {
                var query = customQuery ?? db.Phongs.Include(p => p.MaLoaiNavigation).AsNoTracking();

                var list = await query.ToListAsync();

                var viewList = list.Select(p => new PhongViewModel
                {
                    MaPhong = p.MaPhong,
                    AnhThumbnail = LoadImageWithoutLock(p.HinhAnh),
                    SoPhong = p.SoPhong,
                    TangSo = p.TangSo,
                    TenLoai = p.MaLoaiNavigation?.TenLoai ?? "",
                    GiaMoiDem = p.MaLoaiNavigation?.GiaMoiDem,
                    TinhTrang = p.TinhTrang ?? "",
                    HinhAnh = p.HinhAnh,
                    MaLoai = p.MaLoai
                }).ToList();

                dgvPhong.DataSource = viewList;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải danh sách phòng: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // 4. Khi người dùng bấm "Chọn ảnh": copy file ảnh vào thư mục Images của ứng dụng, chỉ lưu TÊN FILE vào CSDL
        private void btnChonAnh_Click(object sender, EventArgs e)
        {
            using OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Ảnh phòng (*.jpg;*.jpeg;*.png;*.bmp;*.webp)|*.jpg;*.jpeg;*.png;*.bmp;*.webp";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                string fileName = Path.GetFileName(ofd.FileName);
                string destPath = Path.Combine(GetImageFolderPath(), fileName);

                // Copy file vào thư mục Images của ứng dụng nếu chưa có hoặc khác vị trí
                if (!File.Exists(destPath) || destPath != ofd.FileName)
                {
                    File.Copy(ofd.FileName, destPath, true);
                }

                currentImageFileName = fileName;

                picHinhAnh.Image?.Dispose();
                picHinhAnh.Image = LoadImageWithoutLock(fileName);
            }
        }

        // 6. Tích chọn dòng trên DataGridView: đổ dữ liệu + hiển thị đúng ảnh lên PictureBox
        private void dgvPhong_SelectionChanged(object sender, EventArgs e)
        {
            HienThiChiTietDongDangChon();
        }

        private void dgvPhong_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                HienThiChiTietDongDangChon();
            }
        }

        private void HienThiChiTietDongDangChon()
        {
            if (dgvPhong.SelectedRows.Count > 0)
            {
                var row = dgvPhong.SelectedRows[0];
                if (row.DataBoundItem is PhongViewModel item)
                {
                    selectedMaPhong = item.MaPhong;
                    txtSoPhong.Text = item.SoPhong;
                    nudTangSo.Value = item.TangSo ?? 0;
                    if (item.MaLoai.HasValue)
                    {
                        cboLoaiPhong.SelectedValue = item.MaLoai.Value;
                    }
                    if (!string.IsNullOrEmpty(item.TinhTrang))
                    {
                        cboTinhTrang.SelectedItem = item.TinhTrang;
                    }
                    currentImageFileName = item.HinhAnh;

                    picHinhAnh.Image?.Dispose();
                    picHinhAnh.Image = LoadImageWithoutLock(item.HinhAnh);
                }
            }
        }

        // Thêm phòng mới
        private async void btnThem_Click(object sender, EventArgs e)
        {
            string soPhong = txtSoPhong.Text.Trim();
            if (string.IsNullOrWhiteSpace(soPhong))
            {
                MessageBox.Show("Số phòng không được để trống!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtSoPhong.Focus();
                return;
            }

            // Kiểm tra trùng số phòng
            bool exists = await db.Phongs.AnyAsync(x => x.SoPhong.ToLower() == soPhong.ToLower());
            if (exists)
            {
                MessageBox.Show("Số phòng đã tồn tại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtSoPhong.Focus();
                return;
            }

            if (cboLoaiPhong.SelectedValue == null)
            {
                MessageBox.Show("Vui lòng chọn loại phòng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboLoaiPhong.Focus();
                return;
            }

            try
            {
                Phong p = new Phong
                {
                    SoPhong = soPhong,
                    TangSo = (int)nudTangSo.Value,
                    MaLoai = Convert.ToInt32(cboLoaiPhong.SelectedValue),
                    TinhTrang = cboTinhTrang.SelectedItem?.ToString() ?? "Trống",
                    HinhAnh = currentImageFileName
                };

                db.Phongs.Add(p);
                await db.SaveChangesAsync();

                MessageBox.Show("Thêm phòng thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                await LoadDataAsync();
                LamMoi();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi thêm phòng: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Cập nhật (Sửa) phòng
        private async void btnSua_Click(object sender, EventArgs e)
        {
            if (selectedMaPhong <= 0)
            {
                MessageBox.Show("Vui lòng chọn phòng cần sửa từ danh sách!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string soPhong = txtSoPhong.Text.Trim();
            if (string.IsNullOrWhiteSpace(soPhong))
            {
                MessageBox.Show("Số phòng không được để trống!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtSoPhong.Focus();
                return;
            }

            // Kiểm tra trùng số phòng với phòng khác
            bool exists = await db.Phongs.AnyAsync(x => x.SoPhong.ToLower() == soPhong.ToLower() && x.MaPhong != selectedMaPhong);
            if (exists)
            {
                MessageBox.Show("Số phòng đã tồn tại ở phòng khác!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtSoPhong.Focus();
                return;
            }

            if (cboLoaiPhong.SelectedValue == null)
            {
                MessageBox.Show("Vui lòng chọn loại phòng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboLoaiPhong.Focus();
                return;
            }

            try
            {
                var p = await db.Phongs.FindAsync(selectedMaPhong);
                if (p != null)
                {
                    p.SoPhong = soPhong;
                    p.TangSo = (int)nudTangSo.Value;
                    p.MaLoai = Convert.ToInt32(cboLoaiPhong.SelectedValue);
                    p.TinhTrang = cboTinhTrang.SelectedItem?.ToString() ?? "Trống";
                    p.HinhAnh = currentImageFileName;

                    await db.SaveChangesAsync();

                    MessageBox.Show("Cập nhật phòng thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    await LoadDataAsync();
                }
                else
                {
                    MessageBox.Show("Không tìm thấy thông tin phòng cần sửa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi cập nhật phòng: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Xóa phòng
        private async void btnXoa_Click(object sender, EventArgs e)
        {
            if (selectedMaPhong <= 0)
            {
                MessageBox.Show("Vui lòng chọn phòng cần xóa từ danh sách!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var dr = MessageBox.Show($"Bạn có chắc chắn muốn xóa phòng {txtSoPhong.Text}?",
                "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (dr != DialogResult.Yes) return;

            try
            {
                var p = await db.Phongs.FindAsync(selectedMaPhong);
                if (p != null)
                {
                    db.Phongs.Remove(p);
                    await db.SaveChangesAsync();

                    MessageBox.Show("Xóa phòng thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    await LoadDataAsync();
                    LamMoi();
                }
                else
                {
                    MessageBox.Show("Không tìm thấy phòng cần xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi xóa: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Làm mới form
        private async void btnLamMoi_Click(object sender, EventArgs e)
        {
            LamMoi();
            cboLocLoaiPhong.SelectedIndex = 0;
            cboLocTinhTrang.SelectedIndex = 0;
            await LoadDataAsync();
        }

        private void LamMoi()
        {
            selectedMaPhong = 0;
            txtSoPhong.Clear();
            nudTangSo.Value = 0;
            if (cboLoaiPhong.Items.Count > 0)
            {
                cboLoaiPhong.SelectedIndex = 0;
            }
            if (cboTinhTrang.Items.Count > 0)
            {
                cboTinhTrang.SelectedIndex = 0;
            }
            currentImageFileName = null;
            picHinhAnh.Image?.Dispose();
            picHinhAnh.Image = null;
            txtSoPhong.Focus();
        }

        // Mở Form phụ Quản lý Loại phòng
        private async void btnQuanLyLoaiPhong_Click(object sender, EventArgs e)
        {
            using FormLoaiPhong frm = new FormLoaiPhong();
            frm.ShowDialog();

            // Cập nhật lại danh mục loại phòng và danh sách sau khi đóng form loại phòng
            await LoadLoaiPhongComboBoxAsync();
            await LoadDataAsync();
        }

        // 7. Tìm kiếm kết hợp theo Tình trạng phòng (ComboBox) VÀ theo Loại phòng (ComboBox) cùng lúc bằng LINQ Where + Include
        private async void btnTimKiem_Click(object sender, EventArgs e)
        {
            var query = db.Phongs.Include(p => p.MaLoaiNavigation).AsNoTracking().AsQueryable();

            // Lọc theo Loại phòng nếu có chọn
            if (cboLocLoaiPhong.SelectedValue is int maLoai && maLoai > 0)
            {
                query = query.Where(p => p.MaLoai == maLoai);
            }

            // Lọc theo Tình trạng nếu có chọn
            if (cboLocTinhTrang.SelectedIndex > 0 && cboLocTinhTrang.SelectedItem != null)
            {
                string tinhTrang = cboLocTinhTrang.SelectedItem.ToString()!;
                if (tinhTrang != "lọc theo tình trạng")
                {
                    query = query.Where(p => p.TinhTrang == tinhTrang);
                }
            }

            await LoadDataAsync(query);
        }
    }
}
