namespace C3_SunriseHomestay
{
    partial class FormLoaiPhong
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
            lblMaLoai = new Label();
            lblTenLoai = new Label();
            lblGiaMoiDem = new Label();
            lblMoTa = new Label();
            txtMaLoai = new TextBox();
            txtTenLoai = new TextBox();
            txtGiaMoiDem = new TextBox();
            txtMoTa = new TextBox();
            btnThem = new Button();
            btnSua = new Button();
            btnXoa = new Button();
            btnLamMoi = new Button();
            dgvLoaiPhong = new DataGridView();
            cMaLoai = new DataGridViewTextBoxColumn();
            cTenLoai = new DataGridViewTextBoxColumn();
            cGiaMoiDem = new DataGridViewTextBoxColumn();
            cMoTa = new DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)dgvLoaiPhong).BeginInit();
            SuspendLayout();
            // 
            // lblMaLoai
            // 
            lblMaLoai.AutoSize = true;
            lblMaLoai.Location = new Point(25, 25);
            lblMaLoai.Name = "lblMaLoai";
            lblMaLoai.Size = new Size(62, 20);
            lblMaLoai.TabIndex = 0;
            lblMaLoai.Text = "Mã loại:";
            // 
            // lblTenLoai
            // 
            lblTenLoai.AutoSize = true;
            lblTenLoai.Location = new Point(25, 65);
            lblTenLoai.Name = "lblTenLoai";
            lblTenLoai.Size = new Size(64, 20);
            lblTenLoai.TabIndex = 2;
            lblTenLoai.Text = "Tên loại:";
            // 
            // lblGiaMoiDem
            // 
            lblGiaMoiDem.AutoSize = true;
            lblGiaMoiDem.Location = new Point(25, 105);
            lblGiaMoiDem.Name = "lblGiaMoiDem";
            lblGiaMoiDem.Size = new Size(69, 20);
            lblGiaMoiDem.TabIndex = 4;
            lblGiaMoiDem.Text = "Giá/đêm:";
            // 
            // lblMoTa
            // 
            lblMoTa.AutoSize = true;
            lblMoTa.Location = new Point(25, 145);
            lblMoTa.Name = "lblMoTa";
            lblMoTa.Size = new Size(51, 20);
            lblMoTa.TabIndex = 6;
            lblMoTa.Text = "Mô tả:";
            // 
            // txtMaLoai
            // 
            txtMaLoai.Location = new Point(110, 22);
            txtMaLoai.Name = "txtMaLoai";
            txtMaLoai.ReadOnly = true;
            txtMaLoai.Size = new Size(250, 27);
            txtMaLoai.TabIndex = 1;
            // 
            // txtTenLoai
            // 
            txtTenLoai.Location = new Point(110, 62);
            txtTenLoai.Name = "txtTenLoai";
            txtTenLoai.Size = new Size(250, 27);
            txtTenLoai.TabIndex = 3;
            // 
            // txtGiaMoiDem
            // 
            txtGiaMoiDem.Location = new Point(110, 102);
            txtGiaMoiDem.Name = "txtGiaMoiDem";
            txtGiaMoiDem.Size = new Size(250, 27);
            txtGiaMoiDem.TabIndex = 5;
            // 
            // txtMoTa
            // 
            txtMoTa.Location = new Point(110, 142);
            txtMoTa.Multiline = true;
            txtMoTa.Name = "txtMoTa";
            txtMoTa.Size = new Size(250, 65);
            txtMoTa.TabIndex = 7;
            // 
            // btnThem
            // 
            btnThem.Location = new Point(380, 22);
            btnThem.Name = "btnThem";
            btnThem.Size = new Size(90, 32);
            btnThem.TabIndex = 8;
            btnThem.Text = "Thêm";
            btnThem.UseVisualStyleBackColor = true;
            btnThem.Click += btnThem_Click;
            // 
            // btnSua
            // 
            btnSua.Location = new Point(380, 65);
            btnSua.Name = "btnSua";
            btnSua.Size = new Size(90, 32);
            btnSua.TabIndex = 9;
            btnSua.Text = "Sửa";
            btnSua.UseVisualStyleBackColor = true;
            btnSua.Click += btnSua_Click;
            // 
            // btnXoa
            // 
            btnXoa.Location = new Point(380, 108);
            btnXoa.Name = "btnXoa";
            btnXoa.Size = new Size(90, 32);
            btnXoa.TabIndex = 10;
            btnXoa.Text = "Xóa";
            btnXoa.UseVisualStyleBackColor = true;
            btnXoa.Click += btnXoa_Click;
            // 
            // btnLamMoi
            // 
            btnLamMoi.Location = new Point(380, 151);
            btnLamMoi.Name = "btnLamMoi";
            btnLamMoi.Size = new Size(90, 32);
            btnLamMoi.TabIndex = 11;
            btnLamMoi.Text = "Làm mới";
            btnLamMoi.UseVisualStyleBackColor = true;
            btnLamMoi.Click += btnLamMoi_Click;
            // 
            // dgvLoaiPhong
            // 
            dgvLoaiPhong.AllowUserToAddRows = false;
            dgvLoaiPhong.AllowUserToDeleteRows = false;
            dgvLoaiPhong.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvLoaiPhong.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvLoaiPhong.Columns.AddRange(new DataGridViewColumn[] { cMaLoai, cTenLoai, cGiaMoiDem, cMoTa });
            dgvLoaiPhong.Location = new Point(25, 225);
            dgvLoaiPhong.MultiSelect = false;
            dgvLoaiPhong.Name = "dgvLoaiPhong";
            dgvLoaiPhong.ReadOnly = true;
            dgvLoaiPhong.RowHeadersWidth = 51;
            dgvLoaiPhong.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvLoaiPhong.Size = new Size(445, 230);
            dgvLoaiPhong.TabIndex = 12;
            dgvLoaiPhong.SelectionChanged += dgvLoaiPhong_SelectionChanged;
            // 
            // cMaLoai
            // 
            cMaLoai.DataPropertyName = "MaLoai";
            cMaLoai.FillWeight = 50F;
            cMaLoai.HeaderText = "Mã loại";
            cMaLoai.MinimumWidth = 6;
            cMaLoai.Name = "cMaLoai";
            cMaLoai.ReadOnly = true;
            // 
            // cTenLoai
            // 
            cTenLoai.DataPropertyName = "TenLoai";
            cTenLoai.FillWeight = 80F;
            cTenLoai.HeaderText = "Tên loại";
            cTenLoai.MinimumWidth = 6;
            cTenLoai.Name = "cTenLoai";
            cTenLoai.ReadOnly = true;
            // 
            // cGiaMoiDem
            // 
            cGiaMoiDem.DataPropertyName = "GiaMoiDem";
            cGiaMoiDem.FillWeight = 70F;
            cGiaMoiDem.HeaderText = "Giá/đêm";
            cGiaMoiDem.MinimumWidth = 6;
            cGiaMoiDem.Name = "cGiaMoiDem";
            cGiaMoiDem.ReadOnly = true;
            // 
            // cMoTa
            // 
            cMoTa.DataPropertyName = "MoTa";
            cMoTa.FillWeight = 100F;
            cMoTa.HeaderText = "Mô tả";
            cMoTa.MinimumWidth = 6;
            cMoTa.Name = "cMoTa";
            cMoTa.ReadOnly = true;
            // 
            // FormLoaiPhong
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(495, 475);
            Controls.Add(dgvLoaiPhong);
            Controls.Add(btnLamMoi);
            Controls.Add(btnXoa);
            Controls.Add(btnSua);
            Controls.Add(btnThem);
            Controls.Add(txtMoTa);
            Controls.Add(txtGiaMoiDem);
            Controls.Add(txtTenLoai);
            Controls.Add(txtMaLoai);
            Controls.Add(lblMoTa);
            Controls.Add(lblGiaMoiDem);
            Controls.Add(lblTenLoai);
            Controls.Add(lblMaLoai);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "FormLoaiPhong";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Quản Lý Loại Phòng";
            Load += FormLoaiPhong_Load;
            ((System.ComponentModel.ISupportInitialize)dgvLoaiPhong).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblMaLoai;
        private Label lblTenLoai;
        private Label lblGiaMoiDem;
        private Label lblMoTa;
        private TextBox txtMaLoai;
        private TextBox txtTenLoai;
        private TextBox txtGiaMoiDem;
        private TextBox txtMoTa;
        private Button btnThem;
        private Button btnSua;
        private Button btnXoa;
        private Button btnLamMoi;
        private DataGridView dgvLoaiPhong;
        private DataGridViewTextBoxColumn cMaLoai;
        private DataGridViewTextBoxColumn cTenLoai;
        private DataGridViewTextBoxColumn cGiaMoiDem;
        private DataGridViewTextBoxColumn cMoTa;
    }
}
