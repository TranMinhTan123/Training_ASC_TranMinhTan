CREATE DATABASE ThucTap_DoAn
GO
USE ThucTap_DoAn
GO

--USE master
--DROP DATABASE ThucTap_DoAn

CREATE TABLE TBL_SinhVien
(
	Mssv VARCHAR(10) NOT NULL,
	Hoten NVARCHAR(30),
	Gioitinh NVARCHAR(10),
	Ngaysinh DATE,
	Lophoc VARCHAR(10),
	Mail VARCHAR(50),
	MatKhau NVARCHAR(100),
	PRIMARY KEY (Mssv)
)

CREATE TABLE TBL_KhoanThuMonHoc
(
	Id int IDENTITY(1,1) PRIMARY KEY NOT NULL, 
	MaMH VARCHAR(100) ,
	TenMH NVARCHAR(200),
	SoTC INT,
	SoTiet INT,
	DonGia DECIMAL
)

CREATE TABLE TBL_DangKy
(
	MaDangKy VARCHAR(30) NOT NULL,
	Mssv VARCHAR(10),
	MaHocKi VARCHAR(30),
	NgayDK DATE,
	MaMH VARCHAR(100)
)

CREATE TABLE TBL_PhieuThu
(
	SoPhieu VARCHAR(100) PRIMARY KEY NOT NULL,
	Mssv VARCHAR(10),
	NoiDung NVARCHAR(100),
	NgayThanhToan DATE,
	SoTienThu DECIMAL,
	DonViThu NVARCHAR(100),
	TrangThai NVARCHAR(20)
)

CREATE TABLE TBL_ChiTietPhieuThu
(
	SoPhieu VARCHAR(100) NOT NULL,
	MaMH VARCHAR(100) NOT NULL,
	PRIMARY KEY(SoPhieu,MaMH)
)

CREATE TABLE TBL_ThanhToan
(
	Idtt VARCHAR(100) PRIMARY KEY NOT NULL,
	Mssv VARCHAR(10),
	SoPhieu VARCHAR(100),
	SoThe INT,
	TenThe VARCHAR(100),
	So INT,
	NgayHetHan DATE
)

INSERT INTO TBL_KhoanThuMonHoc VALUES
('MH1',N'Nhập môn lập trình',3,45,1695000),
('MH2',N'Xác suất thống kê',3,45,1695000),
('MH3',N'Chủ nghĩa xã hội khoa học',2,30,1130000),
('MH4',N'Cấu trúc dữ liệu và giải thuật',2,30,1170000),
('MH5',N'Hệ điều hành',3,45,1755000),
('MH6',N'Lập trình Web',3,45,2095000),
('MH7',N'Công nghệ .NET',3,45,1990250),
('MH8',N'Kho dữ liệu và OLAP',2,30,1170000),
('MH9',N'Khoá luận tốt nghiệp',8,240,4680000),
('MH10',N'Thực tập nghề nghiệp',4,0,2340000)
INSERT INTO TBL_SinhVien VALUES
('SV001', N'Huỳnh Kiến Phúc', N'Nam', '05/19/2001', '10DHTH2', 'kphuc2015@gmail.com', '123'),
('SV002', N'Nguyễn Thị Thu Hà', N'Nữ', '11/09/1998', '10DHTH1', 'thuha1998@gmail.com', '123'),
('SV003', N'Phạm Thu Anh', N'Nữ', '10/19/1998', '10DHTH2', 'thuanh1998@gmail.com', '123'),
('SV004', N'Nguyễn Gia Huy', N'Nam', '09/01/2002', '10DHTH3', 'giahuy2002@gmail.com', '123'),
('SV005', N'Tạ Quang Anh', N'Nam', '05/19/1999', '10DHTH4', 'quanganh1999@gmail.com', '123'),
('SV006', N'Huỳnh Văn Quyết', N'Nam', '08/20/2000', '10DHTH5', 'vanquyet2000@gmail.com', '123');


--SELECT 
	CREATE PROC sp_KhoanThu
	AS
	SELECT * FROM TBL_KhoanThuMonHoc 
	
--THỰC THI
	--EXEC sp_KhoanThu


-- INSERT
	CREATE PROC sp_Insert_KhoanThu
	   @Mamh VARCHAR(100) , 
       @Tenmh NVARCHAR(200) ,
	   @Sotc INT, 
       @Sotiet INT, 
       @Dongia DECIMAL  
	AS
	BEGIN
		IF EXISTS(SELECT *FROM TBL_KhoanThuMonHoc WHERE MaMH=@Mamh)
			BEGIN
				RETURN -1 
			END
		INSERT INTO TBL_KhoanThuMonHoc(MaMH,TenMH,SoTC,SoTiet,DonGia) VALUES
		(@Mamh,@Tenmh,@Sotc,@Sotiet,@Dongia)
	END
	--THỰC THI
	--EXEC sp_Insert_KhoanThu 'MH15','CN.Net',3,45,1700000
	
--Update
	CREATE PROC sp_Update_KhoanThu
	   @id int,
	   @Mamh VARCHAR(100) , 
       @Tenmh NVARCHAR(200) ,
	   @Sotc INT, 
       @Sotiet INT, 
       @Dongia DECIMAL 
	AS
	BEGIN
		UPDATE TBL_KhoanThuMonHoc
		SET MaMH= @Mamh , TenMH = @TenMH, SoTC=@Sotc,SoTiet=@Sotiet,DonGia=@Dongia
		WHERE Id=@id
	END
	--EXEC sp_Update_KhoanThu 'MH16','CN.Net',3,45,1700000

--Delete
	CREATE PROC sp_Delete_KhoanThu
	   @id int
	AS
	BEGIN
		DELETE TBL_KhoanThuMonHoc
		WHERE Id=@id
	END
--EXEC sp_Delete_KhoanThu 1


--login
CREATE PROC sp_GetSinhVien @mssv VARCHAR(20), @matkhau NVARCHAR(100)
AS
BEGIN
	SELECT sv.Mssv, sv.Hoten, sv.Gioitinh, sv.Ngaysinh, sv.Lophoc, sv.Mail, sv.MatKhau
	FROM TBL_SinhVien sv
	WHERE sv.Mssv = @mssv AND sv.MatKhau = @matkhau
END;
GO