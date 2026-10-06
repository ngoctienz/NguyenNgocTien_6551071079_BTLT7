namespace C3_SunriseHomestay
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            lblSoPhong = new Label();
            lblTangSo = new Label();
            lblLoaiPhong = new Label();
            lblTinhTrang = new Label();
            txtSoPhong = new TextBox();
            nudTangSo = new NumericUpDown();
            cboLoaiPhong = new ComboBox();
            cboTinhTrang = new ComboBox();
            picHinhAnh = new PictureBox();
            btnChonAnh = new Button();
            btnThem = new Button();
            btnSua = new Button();
            btnXoa = new Button();
            btnLamMoi = new Button();
            btnQuanLyLoaiPhong = new Button();
            cboLocLoaiPhong = new ComboBox();
            cboLocTinhTrang = new ComboBox();
            btnTimKiem = new Button();
            dgvPhong = new DataGridView();
            cMaPhong = new DataGridViewTextBoxColumn();
            cAnh = new DataGridViewImageColumn();
            cSoPhong = new DataGridViewTextBoxColumn();
            cTang = new DataGridViewTextBoxColumn();
            cLoaiPhong = new DataGridViewTextBoxColumn();
            cGia = new DataGridViewTextBoxColumn();
            cTinhTrang = new DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)nudTangSo).BeginInit();
            ((System.ComponentModel.ISupportInitialize)picHinhAnh).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvPhong).BeginInit();
            SuspendLayout();
            // 
            // lblSoPhong
            // 
            lblSoPhong.AutoSize = true;
            lblSoPhong.Location = new Point(15, 12);
            lblSoPhong.Name = "lblSoPhong";
            lblSoPhong.Size = new Size(74, 20);
            lblSoPhong.TabIndex = 0;
            lblSoPhong.Text = "Số phòng";
            // 
            // lblTangSo
            // 
            lblTangSo.AutoSize = true;
            lblTangSo.Location = new Point(155, 12);
            lblTangSo.Name = "lblTangSo";
            lblTangSo.Size = new Size(58, 20);
            lblTangSo.TabIndex = 2;
            lblTangSo.Text = "Tầng số";
            // 
            // lblLoaiPhong
            // 
            lblLoaiPhong.AutoSize = true;
            lblLoaiPhong.Location = new Point(275, 12);
            lblLoaiPhong.Name = "lblLoaiPhong";
            lblLoaiPhong.Size = new Size(84, 20);
            lblLoaiPhong.TabIndex = 4;
            lblLoaiPhong.Text = "Loại phòng";
            // 
            // lblTinhTrang
            // 
            lblTinhTrang.AutoSize = true;
            lblTinhTrang.Location = new Point(420, 12);
            lblTinhTrang.Name = "lblTinhTrang";
            lblTinhTrang.Size = new Size(76, 20);
            lblTinhTrang.TabIndex = 6;
            lblTinhTrang.Text = "Tình trạng";
            // 
            // txtSoPhong
            // 
            txtSoPhong.Location = new Point(15, 38);
            txtSoPhong.Name = "txtSoPhong";
            txtSoPhong.Size = new Size(125, 27);
            txtSoPhong.TabIndex = 1;
            // 
            // nudTangSo
            // 
            nudTangSo.Location = new Point(155, 38);
            nudTangSo.Maximum = new decimal(new int[] { 100, 0, 0, 0 });
            nudTangSo.Name = "nudTangSo";
            nudTangSo.Size = new Size(105, 27);
            nudTangSo.TabIndex = 3;
            // 
            // cboLoaiPhong
            // 
            cboLoaiPhong.DropDownStyle = ComboBoxStyle.DropDownList;
            cboLoaiPhong.FormattingEnabled = true;
            cboLoaiPhong.Location = new Point(275, 38);
            cboLoaiPhong.Name = "cboLoaiPhong";
            cboLoaiPhong.Size = new Size(130, 28);
            cboLoaiPhong.TabIndex = 5;
            // 
            // cboTinhTrang
            // 
            cboTinhTrang.DropDownStyle = ComboBoxStyle.DropDownList;
            cboTinhTrang.FormattingEnabled = true;
            cboTinhTrang.Items.AddRange(new object[] { "Trống", "Đang ở", "Đang dọn" });
            cboTinhTrang.Location = new Point(420, 38);
            cboTinhTrang.Name = "cboTinhTrang";
            cboTinhTrang.Size = new Size(130, 28);
            cboTinhTrang.TabIndex = 7;
            // 
            // picHinhAnh
            // 
            picHinhAnh.BorderStyle = BorderStyle.FixedSingle;
            picHinhAnh.Location = new Point(565, 12);
            picHinhAnh.Name = "picHinhAnh";
            picHinhAnh.Size = new Size(100, 75);
            picHinhAnh.SizeMode = PictureBoxSizeMode.Zoom;
            picHinhAnh.TabIndex = 8;
            picHinhAnh.TabStop = false;
            // 
            // btnChonAnh
            // 
            btnChonAnh.Location = new Point(565, 93);
            btnChonAnh.Name = "btnChonAnh";
            btnChonAnh.Size = new Size(100, 30);
            btnChonAnh.TabIndex = 9;
            btnChonAnh.Text = "Chọn ảnh...";
            btnChonAnh.UseVisualStyleBackColor = true;
            btnChonAnh.Click += btnChonAnh_Click;
            // 
            // btnThem
            // 
            btnThem.Location = new Point(710, 22);
            btnThem.Name = "btnThem";
            btnThem.Size = new Size(78, 32);
            btnThem.TabIndex = 10;
            btnThem.Text = "Thêm";
            btnThem.UseVisualStyleBackColor = true;
            btnThem.Click += btnThem_Click;
            // 
            // btnSua
            // 
            btnSua.Location = new Point(796, 22);
            btnSua.Name = "btnSua";
            btnSua.Size = new Size(78, 32);
            btnSua.TabIndex = 11;
            btnSua.Text = "Sửa";
            btnSua.UseVisualStyleBackColor = true;
            btnSua.Click += btnSua_Click;
            // 
            // btnXoa
            // 
            btnXoa.Location = new Point(882, 22);
            btnXoa.Name = "btnXoa";
            btnXoa.Size = new Size(78, 32);
            btnXoa.TabIndex = 12;
            btnXoa.Text = "Xóa";
            btnXoa.UseVisualStyleBackColor = true;
            btnXoa.Click += btnXoa_Click;
            // 
            // btnLamMoi
            // 
            btnLamMoi.Location = new Point(968, 22);
            btnLamMoi.Name = "btnLamMoi";
            btnLamMoi.Size = new Size(80, 32);
            btnLamMoi.TabIndex = 13;
            btnLamMoi.Text = "Làm mới";
            btnLamMoi.UseVisualStyleBackColor = true;
            btnLamMoi.Click += btnLamMoi_Click;
            // 
            // btnQuanLyLoaiPhong
            // 
            btnQuanLyLoaiPhong.Location = new Point(710, 62);
            btnQuanLyLoaiPhong.Name = "btnQuanLyLoaiPhong";
            btnQuanLyLoaiPhong.Size = new Size(164, 30);
            btnQuanLyLoaiPhong.TabIndex = 14;
            btnQuanLyLoaiPhong.Text = "QL Loại phòng...";
            btnQuanLyLoaiPhong.UseVisualStyleBackColor = true;
            btnQuanLyLoaiPhong.Click += btnQuanLyLoaiPhong_Click;
            // 
            // cboLocLoaiPhong
            // 
            cboLocLoaiPhong.DropDownStyle = ComboBoxStyle.DropDownList;
            cboLocLoaiPhong.FormattingEnabled = true;
            cboLocLoaiPhong.Location = new Point(15, 135);
            cboLocLoaiPhong.Name = "cboLocLoaiPhong";
            cboLocLoaiPhong.Size = new Size(220, 28);
            cboLocLoaiPhong.TabIndex = 15;
            // 
            // cboLocTinhTrang
            // 
            cboLocTinhTrang.DropDownStyle = ComboBoxStyle.DropDownList;
            cboLocTinhTrang.FormattingEnabled = true;
            cboLocTinhTrang.Location = new Point(245, 135);
            cboLocTinhTrang.Name = "cboLocTinhTrang";
            cboLocTinhTrang.Size = new Size(220, 28);
            cboLocTinhTrang.TabIndex = 16;
            // 
            // btnTimKiem
            // 
            btnTimKiem.Location = new Point(475, 134);
            btnTimKiem.Name = "btnTimKiem";
            btnTimKiem.Size = new Size(100, 30);
            btnTimKiem.TabIndex = 17;
            btnTimKiem.Text = "Tìm kiếm";
            btnTimKiem.UseVisualStyleBackColor = true;
            btnTimKiem.Click += btnTimKiem_Click;
            // 
            // dgvPhong
            // 
            dgvPhong.AllowUserToAddRows = false;
            dgvPhong.AllowUserToDeleteRows = false;
            dgvPhong.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgvPhong.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvPhong.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvPhong.Columns.AddRange(new DataGridViewColumn[] { cMaPhong, cAnh, cSoPhong, cTang, cLoaiPhong, cGia, cTinhTrang });
            dgvPhong.Location = new Point(15, 175);
            dgvPhong.MultiSelect = false;
            dgvPhong.Name = "dgvPhong";
            dgvPhong.ReadOnly = true;
            dgvPhong.RowHeadersWidth = 35;
            dgvPhong.RowTemplate.Height = 65;
            dgvPhong.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvPhong.Size = new Size(1033, 410);
            dgvPhong.TabIndex = 18;
            dgvPhong.CellClick += dgvPhong_CellClick;
            dgvPhong.SelectionChanged += dgvPhong_SelectionChanged;
            // 
            // cMaPhong
            // 
            cMaPhong.DataPropertyName = "MaPhong";
            cMaPhong.FillWeight = 55F;
            cMaPhong.HeaderText = "Mã phòng";
            cMaPhong.MinimumWidth = 6;
            cMaPhong.Name = "cMaPhong";
            cMaPhong.ReadOnly = true;
            // 
            // cAnh
            // 
            cAnh.DataPropertyName = "AnhThumbnail";
            cAnh.FillWeight = 85F;
            cAnh.HeaderText = "Ảnh";
            cAnh.ImageLayout = DataGridViewImageCellLayout.Zoom;
            cAnh.MinimumWidth = 6;
            cAnh.Name = "cAnh";
            cAnh.ReadOnly = true;
            // 
            // cSoPhong
            // 
            cSoPhong.DataPropertyName = "SoPhong";
            cSoPhong.FillWeight = 75F;
            cSoPhong.HeaderText = "Số phòng";
            cSoPhong.MinimumWidth = 6;
            cSoPhong.Name = "cSoPhong";
            cSoPhong.ReadOnly = true;
            // 
            // cTang
            // 
            cTang.DataPropertyName = "TangSo";
            cTang.FillWeight = 50F;
            cTang.HeaderText = "Tầng";
            cTang.MinimumWidth = 6;
            cTang.Name = "cTang";
            cTang.ReadOnly = true;
            // 
            // cLoaiPhong
            // 
            cLoaiPhong.DataPropertyName = "TenLoai";
            cLoaiPhong.FillWeight = 90F;
            cLoaiPhong.HeaderText = "Loại phòng";
            cLoaiPhong.MinimumWidth = 6;
            cLoaiPhong.Name = "cLoaiPhong";
            cLoaiPhong.ReadOnly = true;
            // 
            // cGia
            // 
            cGia.DataPropertyName = "GiaMoiDem";
            cGia.FillWeight = 80F;
            cGia.HeaderText = "Giá/đêm";
            cGia.MinimumWidth = 6;
            cGia.Name = "cGia";
            cGia.ReadOnly = true;
            // 
            // cTinhTrang
            // 
            cTinhTrang.DataPropertyName = "TinhTrang";
            cTinhTrang.FillWeight = 80F;
            cTinhTrang.HeaderText = "Tình trạng";
            cTinhTrang.MinimumWidth = 6;
            cTinhTrang.Name = "cTinhTrang";
            cTinhTrang.ReadOnly = true;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1065, 600);
            Controls.Add(dgvPhong);
            Controls.Add(btnTimKiem);
            Controls.Add(cboLocTinhTrang);
            Controls.Add(cboLocLoaiPhong);
            Controls.Add(btnQuanLyLoaiPhong);
            Controls.Add(btnLamMoi);
            Controls.Add(btnXoa);
            Controls.Add(btnSua);
            Controls.Add(btnThem);
            Controls.Add(btnChonAnh);
            Controls.Add(picHinhAnh);
            Controls.Add(cboTinhTrang);
            Controls.Add(cboLoaiPhong);
            Controls.Add(nudTangSo);
            Controls.Add(txtSoPhong);
            Controls.Add(lblTinhTrang);
            Controls.Add(lblLoaiPhong);
            Controls.Add(lblTangSo);
            Controls.Add(lblSoPhong);
            MinimumSize = new Size(1000, 500);
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Quản Lý Phòng - Sunrise Homestay";
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)nudTangSo).EndInit();
            ((System.ComponentModel.ISupportInitialize)picHinhAnh).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvPhong).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblSoPhong;
        private Label lblTangSo;
        private Label lblLoaiPhong;
        private Label lblTinhTrang;
        private TextBox txtSoPhong;
        private NumericUpDown nudTangSo;
        private ComboBox cboLoaiPhong;
        private ComboBox cboTinhTrang;
        private PictureBox picHinhAnh;
        private Button btnChonAnh;
        private Button btnThem;
        private Button btnSua;
        private Button btnXoa;
        private Button btnLamMoi;
        private Button btnQuanLyLoaiPhong;
        private ComboBox cboLocLoaiPhong;
        private ComboBox cboLocTinhTrang;
        private Button btnTimKiem;
        private DataGridView dgvPhong;
        private DataGridViewTextBoxColumn cMaPhong;
        private DataGridViewImageColumn cAnh;
        private DataGridViewTextBoxColumn cSoPhong;
        private DataGridViewTextBoxColumn cTang;
        private DataGridViewTextBoxColumn cLoaiPhong;
        private DataGridViewTextBoxColumn cGia;
        private DataGridViewTextBoxColumn cTinhTrang;
    }
}
