USE master
GO

-- Kiểm tra và ngắt kết nối cũ để đảm bảo xóa được DB/Table
IF EXISTS (SELECT name FROM master.dbo.sysdatabases WHERE name = N'QuanLySinhVien')
BEGIN
    ALTER DATABASE QuanLySinhVien SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
END
GO

IF NOT EXISTS (SELECT name FROM master.dbo.sysdatabases WHERE name = N'QuanLySinhVien')
BEGIN
    CREATE DATABASE QuanLySinhVien;
END
GO

ALTER DATABASE QuanLySinhVien SET MULTI_USER;
GO

USE QuanLySinhVien
GO

-- --- XÓA BẢNG CŨ ---
IF OBJECT_ID('dbo.TrangThaiHoc', 'U') IS NOT NULL DROP TABLE TrangThaiHoc;
IF OBJECT_ID('dbo.TongKetNam', 'U') IS NOT NULL DROP TABLE TongKetNam;
IF OBJECT_ID('dbo.XepLoaiHocKy', 'U') IS NOT NULL DROP TABLE XepLoaiHocKy;
IF OBJECT_ID('dbo.Diem', 'U') IS NOT NULL DROP TABLE Diem;
IF OBJECT_ID('dbo.SinhVien', 'U') IS NOT NULL DROP TABLE SinhVien;
IF OBJECT_ID('dbo.ChuongTrinhDaoTao', 'U') IS NOT NULL DROP TABLE ChuongTrinhDaoTao;
IF OBJECT_ID('dbo.HocPhan', 'U') IS NOT NULL DROP TABLE HocPhan;
IF OBJECT_ID('dbo.Lop', 'U') IS NOT NULL DROP TABLE Lop;
IF OBJECT_ID('dbo.Khoa', 'U') IS NOT NULL DROP TABLE Khoa;
IF OBJECT_ID('dbo.Users', 'U') IS NOT NULL DROP TABLE Users;
IF OBJECT_ID('dbo.NhanVien', 'U') IS NOT NULL DROP TABLE NhanVien;
GO

-- --- TẠO BẢNG MỚI ---

-- 1. Bảng Nhân Viên
CREATE TABLE NhanVien (
    MaNV VARCHAR(10) NOT NULL PRIMARY KEY,
    HoTen NVARCHAR(100) NOT NULL,
    NgaySinh DATE,
    ChucVu NVARCHAR(50) NOT NULL,
    CONSTRAINT CK_NhanVien_ChucVu CHECK (ChucVu IN (N'Hiệu trưởng', N'Trưởng khoa', N'Giáo viên'))
);

-- 2. Bảng Users
CREATE TABLE Users (
    Username VARCHAR(50) NOT NULL PRIMARY KEY,
    Password VARCHAR(100) NOT NULL, 
    Role NVARCHAR(50) NOT NULL 
);

-- 3. Bảng Khoa
CREATE TABLE Khoa (
    MaKhoa VARCHAR(10) NOT NULL PRIMARY KEY, 
    TenKhoa NVARCHAR(100) NOT NULL,
    MaTruongKhoa VARCHAR(10) NOT NULL, 
    FOREIGN KEY (MaTruongKhoa) REFERENCES NhanVien(MaNV)
);

-- 4. Bảng Lớp
CREATE TABLE Lop (
    MaLop VARCHAR(10) NOT NULL PRIMARY KEY, 
    SiSo INT NOT NULL DEFAULT 50, 
    MaKhoa VARCHAR(10) NOT NULL,
    KhoaSo INT NOT NULL, 
    MaGV VARCHAR(10) NOT NULL, 
    FOREIGN KEY (MaKhoa) REFERENCES Khoa(MaKhoa) ON DELETE CASCADE ON UPDATE CASCADE,
    FOREIGN KEY (MaGV) REFERENCES NhanVien(MaNV)
);

-- 5. Bảng Học Phần
CREATE TABLE HocPhan (
    MaHP CHAR(6) NOT NULL PRIMARY KEY, 
    TenHP NVARCHAR(100) NOT NULL,
    SoTinChi INT NOT NULL CHECK (SoTinChi >= 1),
    MaKhoa VARCHAR(10) NOT NULL, 
    NamHoc NVARCHAR(50), 
    LoaiHP NVARCHAR(30), 
    CONSTRAINT CK_HocPhan_MaHP CHECK (MaHP LIKE '[A-Z][A-Z][A-Z][0-9][0-9][0-9]'),
    FOREIGN KEY (MaKhoa) REFERENCES Khoa(MaKhoa)
);

-- 6. Bảng Chương Trình Đào Tạo
CREATE TABLE ChuongTrinhDaoTao (
    KhoaSo INT NOT NULL, 
    MaHP CHAR(6) NOT NULL,
    HocKy INT NOT NULL, 
    PRIMARY KEY (KhoaSo, MaHP), 
    FOREIGN KEY (MaHP) REFERENCES HocPhan(MaHP)
);

-- 7. Bảng Sinh Viên
CREATE TABLE SinhVien (
    STT INT IDENTITY(1,1), 
    MaSV CHAR(7) PRIMARY KEY CHECK (MaSV LIKE '[A-Z][A-Z][0-9][0-9][0-9][0-9][0-9]'), 
    HoLot NVARCHAR(25) NOT NULL,
    TenSV NVARCHAR(10) NOT NULL,
    NgaySinh DATE NOT NULL, 
    Phai NVARCHAR(3) Default N'Nam' check (Phai in (N'Nam',N'Nữ')),
    SDT VARCHAR(10) NOT NULL UNIQUE, 
    QueQuan NVARCHAR(50),
    DiaChi NVARCHAR(200), 
    MaLop VARCHAR(10), 
    FOREIGN KEY (MaLop) REFERENCES Lop(MaLop)
);

-- 8. Bảng Điểm
CREATE TABLE Diem (
    MaSV CHAR(7) NOT NULL, 
    MaHP CHAR(6) NOT NULL,
    HocKy INT NOT NULL,
    NamHoc CHAR(5) NOT NULL, 
    DiemTongKet DECIMAL(4, 2),
    PRIMARY KEY (MaSV, MaHP, HocKy, NamHoc),
    FOREIGN KEY (MaSV) REFERENCES SinhVien(MaSV),
    FOREIGN KEY (MaHP) REFERENCES HocPhan(MaHP),
    CONSTRAINT CK_Diem_DiemTongKet CHECK (DiemTongKet >= 0 AND DiemTongKet <= 10),
    CONSTRAINT CK_Diem_HocKy CHECK (HocKy > 0),
    CONSTRAINT CK_Diem_NamHoc CHECK (NamHoc LIKE '[0-9][0-9]-[0-9][0-9]')
);

-- 9. Bảng Trạng Thái Học
CREATE TABLE TrangThaiHoc (
    MaSV CHAR(7) NOT NULL PRIMARY KEY, 
    Nam INT NOT NULL, 
    TrangThai NVARCHAR(50), 
    FOREIGN KEY (MaSV) REFERENCES SinhVien(MaSV)
);

-- 10. Bảng Xếp Loại Học Kỳ
CREATE TABLE XepLoaiHocKy (
    MaSV CHAR(7) NOT NULL, 
    HocKy INT NOT NULL,
    NamHoc CHAR(5) NOT NULL,
    GPA_HocKy DECIMAL(4, 2), 
    GPA_TichLuy DECIMAL(4, 2), 
    XepLoaiHocLuc NVARCHAR(30), 
    DiemRenLuyen INT, 
    XepLoaiRenLuyen NVARCHAR(30), 
    PRIMARY KEY (MaSV, HocKy, NamHoc),
    FOREIGN KEY (MaSV) REFERENCES SinhVien(MaSV),
    CONSTRAINT CK_XLHK_NamHoc CHECK (NamHoc LIKE '[0-9][0-9]-[0-9][0-9]'),
    CONSTRAINT CK_XLHK_GPA_HK CHECK (GPA_HocKy >= 0 AND GPA_HocKy <= 4.0),
    CONSTRAINT CK_XLHK_GPA_TL CHECK (GPA_TichLuy >= 0 AND GPA_TichLuy <= 4.0)
);

-- 11. Bảng Tổng Kết Năm
CREATE TABLE TongKetNam (
    MaSV CHAR(7) NOT NULL, 
    NamHoc CHAR(5) NOT NULL,
    GPA_NamHoc DECIMAL(4, 2),
    GPA_TichLuy DECIMAL(4, 2),
    XepLoaiHocLuc NVARCHAR(30),
    KetQua NVARCHAR(30) NOT NULL, 
    PRIMARY KEY (MaSV, NamHoc),
    FOREIGN KEY (MaSV) REFERENCES SinhVien(MaSV),
    CONSTRAINT CK_TKN_NamHoc CHECK (NamHoc LIKE '[0-9][0-9]-[0-9][0-9]'),
    CONSTRAINT CK_TKN_GPA_Nam CHECK (GPA_NamHoc >= 0 AND GPA_NamHoc <= 4.0),
    CONSTRAINT CK_TKN_GPA_TL CHECK (GPA_TichLuy >= 0 AND GPA_TichLuy <= 4.0)
);
GO

-- --- CHÈN DỮ LIỆU MẪU ---

INSERT INTO NhanVien (MaNV, HoTen, NgaySinh, ChucVu) VALUES
('NV001', N'Võ Văn Hùng', '1970-01-01', N'Hiệu trưởng'),
('NV002', N'Trần Minh Tuấn', '1975-05-15', N'Trưởng khoa'),
('NV003', N'Nguyễn Thị Thu Thủy', '1978-08-20', N'Trưởng khoa'),
('NV004', N'Phạm Quốc Khánh', '1980-02-10', N'Trưởng khoa'),
('NV005', N'Lê Thị Mai Hương', '1982-11-20', N'Trưởng khoa'),
('NV006', N'Đỗ Minh Trí', '1979-03-30', N'Trưởng khoa'),
('NV007', N'Hoàng Thanh Tùng', '1976-07-12', N'Trưởng khoa'),
('NV008', N'Ngô Văn Long', '1977-09-05', N'Trưởng khoa'),
('NV009', N'Lê Thị Thanh Tâm', '1985-02-10', N'Giáo viên'),
('NV010', N'Phạm Văn Hưng', '1990-11-20', N'Giáo viên'),
('NV011', N'Nguyễn Hoàng Nam', '1988-01-01', N'Giáo viên'),
('NV012', N'Trần Thị Kim Chi', '1989-02-02', N'Giáo viên'),
('NV013', N'Lê Văn Dũng', '1990-03-03', N'Giáo viên'),
('NV014', N'Phạm Thị Lan Anh', '1991-04-04', N'Giáo viên'),
('NV015', N'Hoàng Văn Phúc', '1992-05-05', N'Giáo viên'),
('NV016', N'Vũ Thị Hồng Nhung', '1993-06-06', N'Giáo viên'),
('NV017', N'Đặng Văn Lâm', '1994-07-07', N'Giáo viên'),
('NV018', N'Bùi Thị Thu Hà', '1995-08-08', N'Giáo viên'),
('NV019', N'Đỗ Văn Quang', '1996-09-09', N'Giáo viên'),
('NV020', N'Hồ Thị Minh Khai', '1997-10-10', N'Giáo viên');
GO

INSERT INTO Users (Username, Password, Role)
SELECT MaNV, RIGHT(MaNV, 3), CASE ChucVu WHEN N'Hiệu trưởng' THEN 'Admin' WHEN N'Trưởng khoa' THEN 'TruongKhoa' ELSE 'GiaoVien' END FROM NhanVien;
GO

INSERT INTO Khoa (MaKhoa, TenKhoa, MaTruongKhoa) VALUES 
('CNTT', N'Công nghệ Thông tin', 'NV002'),
('KT', N'Kinh tế - Quản trị', 'NV003'),
('NN', N'Ngoại ngữ', 'NV004'),
('DL', N'Du lịch & Khách sạn', 'NV005'),
('LUAT', N'Luật', 'NV006'),
('YD', N'Y Dược', 'NV007'),
('XD', N'Xây dựng', 'NV008');
GO

INSERT INTO Lop (MaLop, SiSo, MaKhoa, KhoaSo, MaGV) VALUES
('DH23IT01', 60, 'CNTT', 23, 'NV009'),
('DH23PM', 55, 'CNTT', 23, 'NV010'),
('DH24MMT', 50, 'CNTT', 24, 'NV011'),
('DH23QT', 45, 'KT', 23, 'NV012'),
('DH23KT', 52, 'KT', 23, 'NV013'),
('DH24NH', 48, 'KT', 24, 'NV014'),
('DH23AV', 55, 'NN', 23, 'NV015'),
('DH23TQ', 40, 'NN', 23, 'NV016'),
('DH23KS', 50, 'DL', 23, 'NV017'),
('DH23LH', 45, 'DL', 23, 'NV018'),
('DH23LKT', 55, 'LUAT', 23, 'NV019'),
('DH23LDS', 50, 'LUAT', 23, 'NV020'),
('DH23YK', 40, 'YD', 23, 'NV009'),
('DH23DU', 45, 'YD', 23, 'NV010'),
('DH23XD', 60, 'XD', 23, 'NV011');
GO

INSERT INTO HocPhan (MaHP, TenHP, SoTinChi, MaKhoa, NamHoc, LoaiHP) VALUES
('ITT001', N'Lập trình C#', 4, 'CNTT', N'2024-2025', N'Bắt buộc'),
('ITT002', N'Cơ sở dữ liệu', 3, 'CNTT', N'2024-2025', N'Tiên quyết'),
('ITT003', N'CTDL & Giải thuật', 3, 'CNTT', N'2023-2024', N'Bắt buộc'),
('KTT004', N'Kinh tế vi mô', 3, 'KT', N'2024-2025', N'Bắt buộc'),
('KTT005', N'Nguyên lý Kế toán', 3, 'KT', N'2024-2025', N'Tiên quyết'),
('NNT006', N'Tiếng Anh chuyên ngành', 2, 'NN', N'2025-2026', N'Tự chọn'),
('LTT007', N'Pháp luật đại cương', 2, 'LUAT', N'2023-2024', N'Bắt buộc'),
('KTT008', N'Quản trị học', 3, 'KT', N'2024-2025', N'Bắt buộc'),
('CHT009', N'Triết học Mác-Lênin', 3, 'CNTT', N'2023-2024', N'Bắt buộc'),
('DLT010', N'Marketing căn bản', 3, 'DL', N'2025-2026', N'Tự chọn'),
('DLT011', N'Kỹ năng giao tiếp', 2, 'DL', N'2024-2025', N'Tự chọn'),
('ITT012', N'Lập trình Web', 4, 'CNTT', N'2025-2026', N'Bắt buộc'),
('ITT013', N'Mạng máy tính', 3, 'CNTT', N'2024-2025', N'Bắt buộc'),
('YDT014', N'Giải phẫu người', 4, 'YD', N'2024-2025', N'Tiên quyết'),
('XDT015', N'Vẽ kỹ thuật', 3, 'XD', N'2024-2025', N'Bắt buộc');
GO

INSERT INTO SinhVien (MaSV, HoLot, TenSV, NgaySinh, Phai, SDT, QueQuan, DiaChi, MaLop) VALUES
('CN23001', N'Nguyễn Văn', N'An', '2003-01-15', N'Nam', '0901110001', N'Hà Nội', N'18 Ung Văn Khiêm, LX', 'DH23IT01'),
('CN23002', N'Trần Thị', N'Bình', '2003-05-20', N'Nữ', '0901110002', N'Nghệ An', N'KTX ĐH An Giang', 'DH23IT01'),
('CN23003', N'Lê Văn', N'Chính', '2003-11-30', N'Nam', '0901110003', N'TP. Hồ Chí Minh', N'Huyện Chợ Mới, AG', 'DH23IT01'),
('CN23004', N'Phạm Hoàng', N'Dũng', '2003-02-14', N'Nam', '0901110004', N'Đồng Tháp', N'25 Võ Thị Sáu, LX', 'DH23PM'),
('CN23005', N'Đỗ Thị', N'Em', '2003-08-08', N'Nữ', '0901110005', N'An Giang', N'Đường Lý Thái Tổ, LX', 'DH23PM'),
('CN23006', N'Ngô Văn', N'Fong', '2003-09-09', N'Nam', '0901110006', N'Cần Thơ', N'Phường Mỹ Bình, LX', 'DH23PM'),
('CN23007', N'Bùi Thị', N'Gấm', '2003-10-10', N'Nữ', '0901110007', N'Kiên Giang', N'Đường Hùng Vương, LX', 'DH24MMT'),
('CN23008', N'Hồ Văn', N'Hải', '2003-12-12', N'Nam', '0901110008', N'Vĩnh Long', N'Phường Mỹ Xuyên, LX', 'DH24MMT'),
('KT23001', N'Phạm Thu', N'Dung', '2003-08-10', N'Nữ', '0902220001', N'Lâm Đồng', N'12 Trần Hưng Đạo, LX', 'DH23QT'),
('KT23002', N'Hoàng Minh', N'Hải', '2003-03-05', N'Nam', '0902220002', N'Cà Mau', N'P. Hưng Lợi, Cần Thơ', 'DH23QT'),
('KT23003', N'Nguyễn Thị', N'Lan', '2003-04-15', N'Nữ', '0902220003', N'Bạc Liêu', N'KTX ĐH An Giang', 'DH23KT'),
('KT23004', N'Trần Văn', N'Khánh', '2003-06-20', N'Nam', '0902220004', N'Sóc Trăng', N'Đường Tôn Đức Thắng, LX', 'DH23KT'),
('KT23005', N'Lê Thị', N'Mai', '2003-07-25', N'Nữ', '0902220005', N'Trà Vinh', N'Phường Đông Xuyên, LX', 'DH24NH'),
('NN23001', N'Đỗ Văn', N'Nam', '2003-09-05', N'Nam', '0903330001', N'Bến Tre', N'Đường Nguyễn Huệ, LX', 'DH23AV'),
('NN23002', N'Ngô Thị', N'Oanh', '2003-10-10', N'Nữ', '0903330002', N'Tiền Giang', N'Phường Mỹ Long, LX', 'DH23AV'),
('NN23003', N'Hoàng Văn', N'Phúc', '2003-11-15', N'Nam', '0903330003', N'Long An', N'Đường Hà Hoàng Hổ, LX', 'DH23TQ'),
('NN23004', N'Vũ Thị', N'Quyên', '2003-12-20', N'Nữ', '0903330004', N'Tây Ninh', N'Phường Mỹ Hòa, LX', 'DH23TQ'),
('DL23001', N'Đặng Văn', N'Sơn', '2003-01-25', N'Nam', '0904440001', N'Bình Dương', N'Đường Phạm Cự Lượng, LX', 'DH23KS'),
('DL23002', N'Bùi Thị', N'Thanh', '2003-02-28', N'Nữ', '0904440002', N'Đồng Nai', N'Phường Bình Khánh, LX', 'DH23KS'),
('DL23003', N'Đỗ Văn', N'Uy', '2003-03-05', N'Nam', '0904440003', N'Bà Rịa - Vũng Tàu', N'Đường Ung Văn Khiêm, LX', 'DH23LH'),
('DL23004', N'Hồ Thị', N'Vân', '2003-04-10', N'Nữ', '0904440004', N'Bình Phước', N'KTX ĐH An Giang', 'DH23LH'),
('LU23001', N'Nguyễn Văn', N'Xô', '2003-05-15', N'Nam', '0905550001', N'Đắk Lắk', N'Phường Mỹ Quý, LX', 'DH23LKT'),
('LU23002', N'Trần Thị', N'Yến', '2003-06-20', N'Nữ', '0905550002', N'Gia Lai', N'Đường Trần Hưng Đạo, LX', 'DH23LDS'),
('YD23001', N'Lê Văn', N'Zũng', '2003-07-25', N'Nam', '0906660001', N'Kon Tum', N'Đường Lê Lợi, LX', 'DH23YK'),
('YD23002', N'Phạm Thị', N'Ánh', '2003-08-30', N'Nữ', '0906660002', N'Đà Nẵng', N'Phường Mỹ Thạnh, LX', 'DH23YK'),
('YD23003', N'Hoàng Văn', N'Bảo', '2003-09-05', N'Nam', '0906660003', N'Quảng Nam', N'Đường Nguyễn Thái Học, LX', 'DH23DU'),
('YD23004', N'Vũ Thị', N'Cúc', '2003-10-10', N'Nữ', '0906660004', N'Quảng Ngãi', N'Phường Mỹ Thới, LX', 'DH23DU'),
('XD23001', N'Ngô Văn', N'Đức', '2003-11-15', N'Nam', '0907770001', N'Bình Định', N'Đường Thoại Ngọc Hầu, LX', 'DH23XD'),
('XD23002', N'Đặng Thị', N'Hạnh', '2003-12-20', N'Nữ', '0907770002', N'Phú Yên', N'KTX ĐH An Giang', 'DH23XD'),
('XD23003', N'Bùi Văn', N'Khôi', '2003-01-05', N'Nam', '0907770003', N'Khánh Hòa', N'Đường Nguyễn Trãi, LX', 'DH23XD');
GO

INSERT INTO Diem (MaSV, MaHP, HocKy, NamHoc, DiemTongKet) VALUES
-- HỌC KỲ 1
('CN23001', 'ITT001', 1, '23-24', 8.5), ('CN23001', 'ITT002', 1, '23-24', 7.0), ('CN23001', 'CHT009', 1, '23-24', 9.0),
('CN23002', 'ITT001', 1, '23-24', 6.5), ('CN23002', 'ITT002', 1, '23-24', 8.0), ('CN23002', 'CHT009', 1, '23-24', 7.5),
('CN23003', 'ITT001', 1, '23-24', 9.0), ('CN23003', 'ITT002', 1, '23-24', 8.5), ('CN23003', 'CHT009', 1, '23-24', 8.0),
('CN23004', 'ITT001', 1, '23-24', 5.5), ('CN23004', 'ITT002', 1, '23-24', 6.0), ('CN23004', 'CHT009', 1, '23-24', 6.5),
('CN23005', 'ITT001', 1, '23-24', 7.0), ('CN23005', 'ITT002', 1, '23-24', 7.5), ('CN23005', 'CHT009', 1, '23-24', 7.0),
('CN23006', 'ITT001', 1, '23-24', 8.0), ('CN23006', 'ITT002', 1, '23-24', 8.0), ('CN23006', 'CHT009', 1, '23-24', 8.0),
('CN23007', 'ITT001', 1, '23-24', 9.5), ('CN23007', 'ITT002', 1, '23-24', 9.0), ('CN23007', 'CHT009', 1, '23-24', 9.5),
('CN23008', 'ITT001', 1, '23-24', 6.0), ('CN23008', 'ITT002', 1, '23-24', 5.5), ('CN23008', 'CHT009', 1, '23-24', 6.0),
('KT23001', 'KTT004', 1, '23-24', 7.5), ('KT23001', 'KTT005', 1, '23-24', 8.0), ('KT23001', 'KTT008', 1, '23-24', 7.5),
('KT23002', 'KTT004', 1, '23-24', 8.5), ('KT23002', 'KTT005', 1, '23-24', 7.0), ('KT23002', 'KTT008', 1, '23-24', 8.0),
('KT23003', 'KTT004', 1, '23-24', 9.0), ('KT23003', 'KTT005', 1, '23-24', 9.5), ('KT23003', 'KTT008', 1, '23-24', 9.0),
('KT23004', 'KTT004', 1, '23-24', 6.5), ('KT23004', 'KTT005', 1, '23-24', 6.0), ('KT23004', 'KTT008', 1, '23-24', 6.5),
('KT23005', 'KTT004', 1, '23-24', 7.0), ('KT23005', 'KTT005', 1, '23-24', 7.5), ('KT23005', 'KTT008', 1, '23-24', 7.0),
('NN23001', 'NNT006', 1, '23-24', 8.0), ('NN23001', 'CHT009', 1, '23-24', 7.5), ('NN23001', 'LTT007', 1, '23-24', 8.0),
('NN23002', 'NNT006', 1, '23-24', 9.0), ('NN23002', 'CHT009', 1, '23-24', 8.5), ('NN23002', 'LTT007', 1, '23-24', 9.0),
('NN23003', 'NNT006', 1, '23-24', 6.0), ('NN23003', 'CHT009', 1, '23-24', 6.5), ('NN23003', 'LTT007', 1, '23-24', 6.0),
('NN23004', 'NNT006', 1, '23-24', 7.5), ('NN23004', 'CHT009', 1, '23-24', 7.0), ('NN23004', 'LTT007', 1, '23-24', 7.5),
('DL23001', 'DLT010', 1, '23-24', 8.0), ('DL23001', 'DLT011', 1, '23-24', 8.5), ('DL23001', 'CHT009', 1, '23-24', 8.0),
('DL23002', 'DLT010', 1, '23-24', 7.0), ('DL23002', 'DLT011', 1, '23-24', 7.5), ('DL23002', 'CHT009', 1, '23-24', 7.0),
('DL23003', 'DLT010', 1, '23-24', 6.5), ('DL23003', 'DLT011', 1, '23-24', 6.0), ('DL23003', 'CHT009', 1, '23-24', 6.5),
('DL23004', 'DLT010', 1, '23-24', 9.0), ('DL23004', 'DLT011', 1, '23-24', 9.5), ('DL23004', 'CHT009', 1, '23-24', 9.0),
('LU23001', 'LTT007', 1, '23-24', 8.5), ('LU23001', 'CHT009', 1, '23-24', 8.0), ('LU23001', 'NNT006', 1, '23-24', 8.5),
('LU23002', 'LTT007', 1, '23-24', 7.5), ('LU23002', 'CHT009', 1, '23-24', 7.0), ('LU23002', 'NNT006', 1, '23-24', 7.5),
('YD23001', 'YDT014', 1, '23-24', 9.0), ('YD23001', 'CHT009', 1, '23-24', 8.5), ('YD23001', 'NNT006', 1, '23-24', 9.0),
('YD23002', 'YDT014', 1, '23-24', 8.0), ('YD23002', 'CHT009', 1, '23-24', 8.0), ('YD23002', 'NNT006', 1, '23-24', 8.0),
('YD23003', 'YDT014', 1, '23-24', 6.5), ('YD23003', 'CHT009', 1, '23-24', 7.0), ('YD23003', 'NNT006', 1, '23-24', 6.5),
('YD23004', 'YDT014', 1, '23-24', 7.5), ('YD23004', 'CHT009', 1, '23-24', 7.5), ('YD23004', 'NNT006', 1, '23-24', 7.5),
('XD23001', 'XDT015', 1, '23-24', 8.5), ('XD23001', 'CHT009', 1, '23-24', 8.0), ('XD23001', 'NNT006', 1, '23-24', 8.5),
('XD23002', 'XDT015', 1, '23-24', 9.0), ('XD23002', 'CHT009', 1, '23-24', 8.5), ('XD23002', 'NNT006', 1, '23-24', 9.0),
('XD23003', 'XDT015', 1, '23-24', 7.0), ('XD23003', 'CHT009', 1, '23-24', 7.5), ('XD23003', 'NNT006', 1, '23-24', 7.0),

-- CNTT: CTDL, Mạng máy tính, Pháp luật
('CN23001', 'ITT003', 2, '23-24', 8.0), ('CN23001', 'ITT013', 2, '23-24', 7.5), ('CN23001', 'LTT007', 2, '23-24', 8.5),
('CN23002', 'ITT003', 2, '23-24', 7.0), ('CN23002', 'ITT013', 2, '23-24', 6.5), ('CN23002', 'LTT007', 2, '23-24', 8.0),
('CN23003', 'ITT003', 2, '23-24', 8.5), ('CN23003', 'ITT013', 2, '23-24', 8.0), ('CN23003', 'LTT007', 2, '23-24', 9.0),
('CN23004', 'ITT003', 2, '23-24', 6.0), ('CN23004', 'ITT013', 2, '23-24', 5.5), ('CN23004', 'LTT007', 2, '23-24', 6.5),
('CN23005', 'ITT003', 2, '23-24', 7.5), ('CN23005', 'ITT013', 2, '23-24', 7.0), ('CN23005', 'LTT007', 2, '23-24', 7.5),
('CN23006', 'ITT003', 2, '23-24', 8.0), ('CN23006', 'ITT013', 2, '23-24', 7.5), ('CN23006', 'LTT007', 2, '23-24', 8.0),
('CN23007', 'ITT003', 2, '23-24', 9.0), ('CN23007', 'ITT013', 2, '23-24', 9.5), ('CN23007', 'LTT007', 2, '23-24', 9.0),
('CN23008', 'ITT003', 2, '23-24', 5.5), ('CN23008', 'ITT013', 2, '23-24', 6.0), ('CN23008', 'LTT007', 2, '23-24', 6.0),
-- KT: Marketing, Giao tiếp, Pháp luật
('KT23001', 'DLT010', 2, '23-24', 7.0), ('KT23001', 'DLT011', 2, '23-24', 7.5), ('KT23001', 'LTT007', 2, '23-24', 8.0),
('KT23002', 'DLT010', 2, '23-24', 8.0), ('KT23002', 'DLT011', 2, '23-24', 8.5), ('KT23002', 'LTT007', 2, '23-24', 7.5),
('KT23003', 'DLT010', 2, '23-24', 9.0), ('KT23003', 'DLT011', 2, '23-24', 9.0), ('KT23003', 'LTT007', 2, '23-24', 9.5),
('KT23004', 'DLT010', 2, '23-24', 6.0), ('KT23004', 'DLT011', 2, '23-24', 6.5), ('KT23004', 'LTT007', 2, '23-24', 6.0),
('KT23005', 'DLT010', 2, '23-24', 7.5), ('KT23005', 'DLT011', 2, '23-24', 7.0), ('KT23005', 'LTT007', 2, '23-24', 7.5),
-- NN: Giao tiếp, Quản trị, Marketing
('NN23001', 'DLT011', 2, '23-24', 8.5), ('NN23001', 'KTT008', 2, '23-24', 8.0), ('NN23001', 'DLT010', 2, '23-24', 7.5),
('NN23002', 'DLT011', 2, '23-24', 9.0), ('NN23002', 'KTT008', 2, '23-24', 9.0), ('NN23002', 'DLT010', 2, '23-24', 8.5),
('NN23003', 'DLT011', 2, '23-24', 6.5), ('NN23003', 'KTT008', 2, '23-24', 6.0), ('NN23003', 'DLT010', 2, '23-24', 7.0),
('NN23004', 'DLT011', 2, '23-24', 7.0), ('NN23004', 'KTT008', 2, '23-24', 7.5), ('NN23004', 'DLT010', 2, '23-24', 8.0),
-- DL: Quản trị, Pháp luật, Tiếng Anh CN
('DL23001', 'KTT008', 2, '23-24', 8.0), ('DL23001', 'LTT007', 2, '23-24', 8.5), ('DL23001', 'NNT006', 2, '23-24', 7.5),
('DL23002', 'KTT008', 2, '23-24', 7.5), ('DL23002', 'LTT007', 2, '23-24', 7.0), ('DL23002', 'NNT006', 2, '23-24', 8.0),
('DL23003', 'KTT008', 2, '23-24', 6.0), ('DL23003', 'LTT007', 2, '23-24', 6.5), ('DL23003', 'NNT006', 2, '23-24', 7.0),
('DL23004', 'KTT008', 2, '23-24', 9.0), ('DL23004', 'LTT007', 2, '23-24', 9.5), ('DL23004', 'NNT006', 2, '23-24', 8.5),
-- Luật: Quản trị, Giao tiếp, Marketing
('LU23001', 'KTT008', 2, '23-24', 8.5), ('LU23001', 'DLT011', 2, '23-24', 8.0), ('LU23001', 'DLT010', 2, '23-24', 8.0),
('LU23002', 'KTT008', 2, '23-24', 7.0), ('LU23002', 'DLT011', 2, '23-24', 7.5), ('LU23002', 'DLT010', 2, '23-24', 7.0),
-- Y Dược: Giao tiếp, Pháp luật, Quản trị
('YD23001', 'DLT011', 2, '23-24', 8.0), ('YD23001', 'LTT007', 2, '23-24', 8.5), ('YD23001', 'KTT008', 2, '23-24', 8.0),
('YD23002', 'DLT011', 2, '23-24', 7.5), ('YD23002', 'LTT007', 2, '23-24', 7.0), ('YD23002', 'KTT008', 2, '23-24', 7.5),
('YD23003', 'DLT011', 2, '23-24', 6.0), ('YD23003', 'LTT007', 2, '23-24', 6.5), ('YD23003', 'KTT008', 2, '23-24', 7.0),
('YD23004', 'DLT011', 2, '23-24', 8.5), ('YD23004', 'LTT007', 2, '23-24', 8.0), ('YD23004', 'KTT008', 2, '23-24', 8.5),
-- XD: Pháp luật, Giao tiếp, Quản trị
('XD23001', 'LTT007', 2, '23-24', 8.0), ('XD23001', 'DLT011', 2, '23-24', 7.5), ('XD23001', 'KTT008', 2, '23-24', 8.0),
('XD23002', 'LTT007', 2, '23-24', 8.5), ('XD23002', 'DLT011', 2, '23-24', 8.0), ('XD23002', 'KTT008', 2, '23-24', 8.5),
('XD23003', 'LTT007', 2, '23-24', 7.0), ('XD23003', 'DLT011', 2, '23-24', 6.5), ('XD23003', 'KTT008', 2, '23-24', 7.0);
GO

-- 8. INSERT DỮ LIỆU BAN ĐẦU CHO BẢNG TRANG THAI HOC
INSERT INTO TrangThaiHoc (MaSV, Nam, TrangThai)
SELECT 
    SV.MaSV, 
    (26 - L.KhoaSo), 
    CASE 
        WHEN (26 - L.KhoaSo) > 4 THEN N'Đã tốt nghiệp' 
        ELSE N'Đang học' 
    END
FROM SinhVien SV
JOIN Lop L ON SV.MaLop = L.MaLop;
GO