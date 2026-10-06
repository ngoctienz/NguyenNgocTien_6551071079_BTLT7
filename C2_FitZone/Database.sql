-- Tạo cơ sở dữ liệu C2_FitZone
CREATE DATABASE C2_FitZone;
GO

USE C2_FitZone;
GO

-- Tạo bảng HoiVien
CREATE TABLE HoiVien (
    MaHV INT IDENTITY(1,1) PRIMARY KEY,
    HoTen NVARCHAR(100) NOT NULL,
    GioiTinh BIT, -- 1 = Nam, 0 = Nữ
    NgaySinh DATE,
    SDT VARCHAR(15),
    Email VARCHAR(100),
    HangThanhVien NVARCHAR(20), -- Basic, VIP, Premium
    NgayDangKy DATETIME DEFAULT GETDATE(),
    TrangThai BIT -- 1 = Đang hoạt động, 0 = Tạm ngưng
);
GO

-- Thêm dữ liệu mẫu
INSERT INTO HoiVien (HoTen, GioiTinh, NgaySinh, SDT, Email, HangThanhVien, NgayDangKy, TrangThai)
VALUES 
(N'Họ tên Vôn', 1, '1999-11-07', '07382735879', 'von@gmail.com', N'Basic', GETDATE(), 1),
(N'Nguyễn Nnh', 1, '1999-11-10', '07382731034', 'nnh@gmail.com', N'Premium', GETDATE(), 1),
(N'Nguyễn Bộ Đỏn', 1, '1999-12-29', '07882773727', 'don@gmail.com', N'Basic', GETDATE(), 1),
(N'Nguyễn Xim', 1, '1999-11-17', '07375856597', 'xim@gmail.com', N'VIP', GETDATE(), 1),
(N'Nguyễn Tương', 0, '1999-01-22', '07887775931', 'tuong@gmail.com', N'Premium', GETDATE(), 1);
GO
