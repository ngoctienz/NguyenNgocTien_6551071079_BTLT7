namespace C1_QuanLySach
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            lblMaTL = new Label();
            lblTenTL = new Label();
            lblMoTa = new Label();
            lblNgayTao = new Label();
            btnThem = new Button();
            btnSua = new Button();
            btnXoa = new Button();
            btnLamMoi = new Button();
            txtMaTL = new TextBox();
            txtTenTL = new TextBox();
            txtMoTa = new TextBox();
            dgvSach = new DataGridView();
            cMaTL = new DataGridViewTextBoxColumn();
            cTenTL = new DataGridViewTextBoxColumn();
            cMoTa = new DataGridViewTextBoxColumn();
            cSoLuong = new DataGridViewTextBoxColumn();
            cNgayTao = new DataGridViewTextBoxColumn();
            btnTimKiem = new Button();
            txtTimKiem = new TextBox();
            ((System.ComponentModel.ISupportInitialize)dgvSach).BeginInit();
            SuspendLayout();
            // 
            // lblMaTL
            // 
            lblMaTL.AutoSize = true;
            lblMaTL.Location = new Point(30, 36);
            lblMaTL.Name = "lblMaTL";
            lblMaTL.Size = new Size(84, 20);
            lblMaTL.TabIndex = 0;
            lblMaTL.Text = "Mã thể loại";
            // 
            // lblTenTL
            // 
            lblTenTL.AutoSize = true;
            lblTenTL.Location = new Point(30, 97);
            lblTenTL.Name = "lblTenTL";
            lblTenTL.Size = new Size(86, 20);
            lblTenTL.TabIndex = 1;
            lblTenTL.Text = "Tên thể loại";
            // 
            // lblMoTa
            // 
            lblMoTa.AutoSize = true;
            lblMoTa.Location = new Point(30, 151);
            lblMoTa.Name = "lblMoTa";
            lblMoTa.Size = new Size(48, 20);
            lblMoTa.TabIndex = 2;
            lblMoTa.Text = "Mô tả";
            // 
            // lblNgayTao
            // 
            lblNgayTao.AutoSize = true;
            lblNgayTao.Location = new Point(30, 245);
            lblNgayTao.Name = "lblNgayTao";
            lblNgayTao.Size = new Size(73, 20);
            lblNgayTao.TabIndex = 13;
            lblNgayTao.Text = "Ngày tạo:";
            // 
            // btnThem
            // 
            btnThem.Location = new Point(539, 26);
            btnThem.Name = "btnThem";
            btnThem.Size = new Size(94, 32);
            btnThem.TabIndex = 3;
            btnThem.Text = "Thêm";
            btnThem.UseVisualStyleBackColor = true;
            btnThem.Click += btnThem_Click;
            // 
            // btnSua
            // 
            btnSua.Location = new Point(645, 26);
            btnSua.Name = "btnSua";
            btnSua.Size = new Size(94, 32);
            btnSua.TabIndex = 4;
            btnSua.Text = "Sửa";
            btnSua.UseVisualStyleBackColor = true;
            btnSua.Click += btnSua_Click;
            // 
            // btnXoa
            // 
            btnXoa.Location = new Point(751, 26);
            btnXoa.Name = "btnXoa";
            btnXoa.Size = new Size(94, 32);
            btnXoa.TabIndex = 5;
            btnXoa.Text = "Xóa";
            btnXoa.UseVisualStyleBackColor = true;
            btnXoa.Click += btnXoa_Click;
            // 
            // btnLamMoi
            // 
            btnLamMoi.Location = new Point(857, 26);
            btnLamMoi.Name = "btnLamMoi";
            btnLamMoi.Size = new Size(94, 32);
            btnLamMoi.TabIndex = 6;
            btnLamMoi.Text = "Làm mới";
            btnLamMoi.UseVisualStyleBackColor = true;
            btnLamMoi.Click += btnLamMoi_Click;
            // 
            // txtMaTL
            // 
            txtMaTL.Location = new Point(135, 29);
            txtMaTL.Name = "txtMaTL";
            txtMaTL.PlaceholderText = "ReadOnly";
            txtMaTL.ReadOnly = true;
            txtMaTL.Size = new Size(380, 27);
            txtMaTL.TabIndex = 7;
            // 
            // txtTenTL
            // 
            txtTenTL.Location = new Point(135, 85);
            txtTenTL.Name = "txtTenTL";
            txtTenTL.Size = new Size(380, 27);
            txtTenTL.TabIndex = 8;
            // 
            // txtMoTa
            // 
            txtMoTa.Location = new Point(135, 146);
            txtMoTa.Multiline = true;
            txtMoTa.Name = "txtMoTa";
            txtMoTa.Size = new Size(380, 85);
            txtMoTa.TabIndex = 9;
            // 
            // dgvSach
            // 
            dgvSach.AllowUserToAddRows = false;
            dgvSach.AllowUserToDeleteRows = false;
            dgvSach.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvSach.Columns.AddRange(new DataGridViewColumn[] { cMaTL, cTenTL, cMoTa, cSoLuong, cNgayTao });
            dgvSach.Location = new Point(62, 334);
            dgvSach.MultiSelect = false;
            dgvSach.Name = "dgvSach";
            dgvSach.ReadOnly = true;
            dgvSach.RowHeadersWidth = 51;
            dgvSach.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvSach.Size = new Size(853, 240);
            dgvSach.TabIndex = 10;
            dgvSach.SelectionChanged += dgvSach_SelectionChanged;
            // 
            // cMaTL
            // 
            cMaTL.DataPropertyName = "MaTL";
            cMaTL.HeaderText = "Mã TL";
            cMaTL.MinimumWidth = 6;
            cMaTL.Name = "cMaTL";
            cMaTL.ReadOnly = true;
            cMaTL.Width = 90;
            // 
            // cTenTL
            // 
            cTenTL.DataPropertyName = "TenTheLoai";
            cTenTL.HeaderText = "Tên thể loại";
            cTenTL.MinimumWidth = 6;
            cTenTL.Name = "cTenTL";
            cTenTL.ReadOnly = true;
            cTenTL.Width = 150;
            // 
            // cMoTa
            // 
            cMoTa.DataPropertyName = "MoTa";
            cMoTa.HeaderText = "Mô tả";
            cMoTa.MinimumWidth = 6;
            cMoTa.Name = "cMoTa";
            cMoTa.ReadOnly = true;
            cMoTa.Width = 260;
            // 
            // cSoLuong
            // 
            cSoLuong.DataPropertyName = "SoLuongSach";
            cSoLuong.HeaderText = "Số lượng sách";
            cSoLuong.MinimumWidth = 6;
            cSoLuong.Name = "cSoLuong";
            cSoLuong.ReadOnly = true;
            cSoLuong.Width = 130;
            // 
            // cNgayTao
            // 
            cNgayTao.DataPropertyName = "NgayTao";
            cNgayTao.HeaderText = "Ngày tạo";
            cNgayTao.MinimumWidth = 6;
            cNgayTao.Name = "cNgayTao";
            cNgayTao.ReadOnly = true;
            cNgayTao.Width = 170;
            // 
            // btnTimKiem
            // 
            btnTimKiem.Location = new Point(415, 275);
            btnTimKiem.Name = "btnTimKiem";
            btnTimKiem.Size = new Size(100, 32);
            btnTimKiem.TabIndex = 11;
            btnTimKiem.Text = "Tìm kiếm";
            btnTimKiem.UseVisualStyleBackColor = true;
            btnTimKiem.Click += btnTimKiem_Click;
            // 
            // txtTimKiem
            // 
            txtTimKiem.Location = new Point(30, 278);
            txtTimKiem.Name = "txtTimKiem";
            txtTimKiem.Size = new Size(365, 27);
            txtTimKiem.TabIndex = 12;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(977, 595);
            Controls.Add(lblNgayTao);
            Controls.Add(txtTimKiem);
            Controls.Add(btnTimKiem);
            Controls.Add(dgvSach);
            Controls.Add(txtMoTa);
            Controls.Add(txtTenTL);
            Controls.Add(txtMaTL);
            Controls.Add(btnLamMoi);
            Controls.Add(btnXoa);
            Controls.Add(btnSua);
            Controls.Add(btnThem);
            Controls.Add(lblMoTa);
            Controls.Add(lblTenTL);
            Controls.Add(lblMaTL);
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Quản Lý Thể Loại Sách - Tri Thức Books";
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)dgvSach).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblMaTL;
        private Label lblTenTL;
        private Label lblMoTa;
        private Label lblNgayTao;
        private Button btnThem;
        private Button btnSua;
        private Button btnXoa;
        private Button btnLamMoi;
        private TextBox txtMaTL;
        private TextBox txtTenTL;
        private TextBox txtMoTa;
        private DataGridView dgvSach;
        private Button btnTimKiem;
        private TextBox txtTimKiem;
        private DataGridViewTextBoxColumn cMaTL;
        private DataGridViewTextBoxColumn cTenTL;
        private DataGridViewTextBoxColumn cMoTa;
        private DataGridViewTextBoxColumn cSoLuong;
        private DataGridViewTextBoxColumn cNgayTao;
    }
}
