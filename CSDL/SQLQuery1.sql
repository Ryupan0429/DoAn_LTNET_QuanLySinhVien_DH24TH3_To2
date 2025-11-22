IF NOT EXISTS (SELECT name FROM master.dbo.sysdatabases WHERE name = N'QuanLySinhVien')
BEGIN
    CREATE DATABASE QuanLySinhVien;
END
GO

USE QuanLySinhVien
GO

-- Xóa các bảng cũ nếu tồn tại để tránh lỗi khi chạy lại
IF OBJECT_ID('dbo.TongKetNam', 'U') IS NOT NULL DROP TABLE TongKetNam;
IF OBJECT_ID('dbo.XepLoaiHocKy', 'U') IS NOT NULL DROP TABLE XepLoaiHocKy;
IF OBJECT_ID('dbo.Diem', 'U') IS NOT NULL DROP TABLE Diem;
IF OBJECT_ID('dbo.SinhVien', 'U') IS NOT NULL DROP TABLE SinhVien;
IF OBJECT_ID('dbo.ChuongTrinhDaoTao', 'U') IS NOT NULL DROP TABLE ChuongTrinhDaoTao;
IF OBJECT_ID('dbo.HocPhan', 'U') IS NOT NULL DROP TABLE HocPhan;
IF OBJECT_ID('dbo.Lop', 'U') IS NOT NULL DROP TABLE Lop;
IF OBJECT_ID('dbo.Khoa', 'U') IS NOT NULL DROP TABLE Khoa;
GO

CREATE TABLE Khoa (
    MaKhoa VARCHAR(10) NOT NULL PRIMARY KEY, 
    TenKhoa NVARCHAR(100) NOT NULL
);

CREATE TABLE Lop (
    MaLop VARCHAR(10) NOT NULL PRIMARY KEY, 
    SiSo INT NOT NULL DEFAULT 50, 
    MaKhoa VARCHAR(10) NOT NULL,
    KhoaSo INT NOT NULL, 
    FOREIGN KEY (MaKhoa) REFERENCES Khoa(MaKhoa)
        ON DELETE CASCADE ON UPDATE CASCADE
);

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

CREATE TABLE ChuongTrinhDaoTao (
    KhoaSo INT NOT NULL, 
    MaHP CHAR(6) NOT NULL,
    HocKy INT NOT NULL, 
    PRIMARY KEY (KhoaSo, MaHP), 
    FOREIGN KEY (MaHP) REFERENCES HocPhan(MaHP)
);

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

-- CHÈN DỮ LIỆU MẪU
INSERT INTO Khoa (MaKhoa, TenKhoa) VALUES 
('CNTT', N'Công nghệ Thông tin'),
('KT', N'Kinh tế - Quản trị'),
('NN', N'Ngoại ngữ'),
('DL', N'Du lịch & Khách sạn'),
('LUAT', N'Luật'),
('YD', N'Y Dược'),
('XD', N'Xây dựng');

INSERT INTO Lop (MaLop, SiSo, MaKhoa, KhoaSo) VALUES
('DH23IT01', 60, 'CNTT', 23),
('DH23PM', 55, 'CNTT', 23),
('DH24MMT', 50, 'CNTT', 24),
('DH23QT', 45, 'KT', 23),
('DH23KT', 52, 'KT', 23),
('DH24NH', 48, 'KT', 24),
('DH23AV', 55, 'NN', 23),
('DH23TQ', 40, 'NN', 23),
('DH23KS', 50, 'DL', 23),
('DH23LH', 45, 'DL', 23),
('DH23LKT', 55, 'LUAT', 23),
('DH23LDS', 50, 'LUAT', 23),
('DH23YK', 40, 'YD', 23),
('DH23DU', 45, 'YD', 23),
('DH23XD', 60, 'XD', 23);

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

INSERT INTO SinhVien (MaSV, HoLot, TenSV, NgaySinh, Phai, SDT, QueQuan, DiaChi, MaLop) VALUES
('CN23001', N'Nguyễn Văn', N'An', '2000-01-15', N'Nam', '0901111111', N'Hà Nội', N'18 Ung Văn Khiêm, P. Đông Xuyên, TP. Long Xuyên', 'DH23IT01'),
('CN23002', N'Trần Thị', N'Bình', '2001-05-20', N'Nữ', '0902222222', N'Nghệ An', N'KTX ĐH An Giang, TP. Long Xuyên', 'DH23IT01'),
('CN23003', N'Lê Văn', N'Chính', '2002-11-30', N'Nam', '0903333333', N'TP. Hồ Chí Minh', N'Huyện Chợ Mới, An Giang', 'DH23PM'),
('KT23004', N'Phạm Thu', N'Dung', '2000-08-10', N'Nữ', '0904444444', N'Lâm Đồng', N'12 Trần Hưng Đạo, TP. Long Xuyên', 'DH23QT'),
('KT23005', N'Hoàng Minh', N'Hải', '2001-03-05', N'Nam', '0905555555', N'Cà Mau', N'P. Hưng Lợi, Q. Ninh Kiều, TP. Cần Thơ', 'DH23QT');
GO