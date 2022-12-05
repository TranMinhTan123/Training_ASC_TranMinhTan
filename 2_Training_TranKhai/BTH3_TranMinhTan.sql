
CREATE DATABASE ThucTap;
USE ThucTap;

CREATE TABLE TBLKhoa
(Makhoa char(10)primary key,
 Tenkhoa char(30),
 Dienthoai char(10));

CREATE TABLE TBLGiangVien(
Magv int primary key,
Hotengv char(30),
Luong decimal(5,2),
Makhoa char(10) references TBLKhoa);

CREATE TABLE TBLSinhVien(
Masv int primary key,
Hotensv char(40),
Makhoa char(10)foreign key references TBLKhoa,
Namsinh int,
Quequan char(30));

CREATE TABLE TBLDeTai(
Madt char(10)primary key,
Tendt char(30),
Kinhphi int,
Noithuctap char(30));

CREATE TABLE TBLHuongDan(
Masv int primary key,
Madt char(10)foreign key references TBLDeTai,
Magv int foreign key references TBLGiangVien,
KetQua decimal(5,2));

INSERT INTO TBLKhoa VALUES
('Geo','Dia ly va QLTN',3855413),
('Math','Toan',3855411),
('Bio','Cong nghe Sinh hoc',3855412);

INSERT INTO TBLGiangVien VALUES
(11,'Thanh Binh',700,'Geo'),    
(12,'Thu Huong',500,'Math'),
(13,'Chu Vinh',650,'Geo'),
(14,'Le Thi Ly',500,'Bio'),
(15,'Tran Son',900,'Math');

INSERT INTO TBLSinhVien VALUES
(1,'Le Van Son','Bio',1990,'Nghe An'),
(2,'Nguyen Thi Mai','Geo',1990,'Thanh Hoa'),
(3,'Bui Xuan Duc','Math',1992,'Ha Noi'),
(4,'Nguyen Van Tung','Bio',null,'Ha Tinh'),
(5,'Le Khanh Linh','Bio',1989,'Ha Nam'),
(6,'Tran Khac Trong','Geo',1991,'Thanh Hoa'),
(7,'Le Thi Van','Math',null,'null'),
(8,'Hoang Van Duc','Bio',1992,'Nghe An');

INSERT INTO TBLDeTai VALUES
('Dt01','GIS',100,'Nghe An'),
('Dt02','ARC GIS',500,'Nam Dinh'),
('Dt03','Spatial DB',100, 'Ha Tinh'),
('Dt04','MAP',300,'Quang Binh' );
INSERT INTO TBLHuongDan VALUES
(1,'Dt01',13,8),
(2,'Dt03',14,0),
(3,'Dt03',12,10),
(5,'Dt04',14,7),
(6,'Dt01',13,Null),
(7,'Dt04',11,10),
(8,'Dt03',15,6);
 
 --I.
--1.	Đưa ra thông tin gồm mã số, họ tên và tên khoa của tất cả các giảng viên

	SELECT MAGV,Hotengv,TENKHOA 
	FROM TBLGiangVien GV LEFT JOIN TBLKhoa K ON GV.Makhoa =K.Makhoa

--2.	Đưa ra thông tin gồm mã số, họ tên và tên khoa của các giảng viên của khoa ‘DIA LY va QLTN’
	
	SELECT MAGV,Hotengv,TENKHOA 
	FROM TBLGiangVien GV LEFT JOIN TBLKhoa K ON GV.Makhoa =K.Makhoa
	WHERE  Tenkhoa='Dia ly va QLTN'

--3.	Cho biết số sinh viên của khoa ‘CONG NGHE SINH HOC’

	SELECT COUNT(MASV) AS N'SỐ SINH VIÊN KHOA CONG NGHE SINH HOC'
	FROM TBLSinhVien SV LEFT JOIN TBLKhoa K ON SV.Makhoa =K.Makhoa
	WHERE  Tenkhoa='CONG NGHE SINH HOC'

--4.	Đưa ra danh sách gồm mã số, họ tên và tuổi của các sinh viên khoa ‘TOAN’

	SELECT MASV, Hotensv,(YEAR(GETDATE()) - Namsinh) AS N'TUỔI'
	FROM TBLSinhVien SV LEFT JOIN TBLKhoa K ON SV.Makhoa =K.Makhoa
	WHERE  Tenkhoa='TOAN'

--5.	Cho biết số giảng viên của khoa ‘CONG NGHE SINH HOC’

	SELECT COUNT(MAGV) AS N'SỐ GIẢNG VIÊN KHOA CONG NGHE SINH HOC'
	FROM TBLGiangVien GV LEFT JOIN TBLKhoa K ON GV.Makhoa =K.Makhoa
	WHERE  Tenkhoa='CONG NGHE SINH HOC'

--6.	Cho biết thông tin về sinh viên không tham gia thực tập

	SELECT *
	FROM TBLSinhVien SV
	WHERE NOT EXISTS(SELECT MASV FROM TBLHuongDan HD WHERE HD.Masv=SV.Masv)

--7.	Đưa ra mã khoa, tên khoa và số giảng viên của mỗi khoa

	SELECT K.Makhoa,Tenkhoa,TENKHOA,COUNT(MAGV) AS N'SỐ GIẢNG VIÊN'
	FROM TBLGiangVien GV LEFT JOIN TBLKhoa K ON GV.Makhoa =K.Makhoa
	GROUP BY K.Makhoa,Tenkhoa

--8.	Cho biết số điện thoại của khoa mà sinh viên có tên ‘Le van son’ đang theo học

	SELECT Dienthoai
	FROM TBLSinhVien SV LEFT JOIN TBLKhoa K ON SV.Makhoa =K.Makhoa
	WHERE  Hotensv='Le van son'

--II
--1.	Cho biết mã số và tên của các đề tài do giảng viên ‘Tran son’ hướng dẫn

	SELECT DT.Madt, Tendt
	FROM TBLDeTai DT, TBLGiangVien GV, TBLHuongDan HD
	WHERE DT.Madt=HD.Madt AND GV.Magv=HD.Magv AND Hotengv='Tran son'

--2.	Cho biết tên đề tài không có sinh viên nào thực tập

	SELECT *
	FROM TBLDeTai DT
	WHERE NOT EXISTS(SELECT MADT FROM TBLHuongDan HD WHERE DT.MADT = HD.MADT)

--3.	Cho biết mã số, họ tên, tên khoa của các giảng viên hướng dẫn từ 3 sinh viên trở lên.

	SELECT GV.MAGV, Hotengv,Tenkhoa,COUNT(HD.MASV)
	FROM TBLGiangVien GV, TBLKhoa K, TBLHuongDan HD
	WHERE GV.Makhoa=K.Makhoa AND GV.Magv=HD.Magv
	GROUP BY GV.MAGV,Hotengv,Tenkhoa
	HAVING  COUNT(HD.MASV) >=3

--4.	Cho biết mã số, tên đề tài của đề tài có kinh phí cao nhất

	SELECT  MADT,TENDT, Kinhphi
	FROM TBLDeTai DT
	WHERE Kinhphi=(SELECT MAX(Kinhphi) FROM TBLDeTai)

--5.	Cho biết mã số và tên các đề tài có nhiều hơn 2 sinh viên tham gia thực tập

	SELECT DT.MADT, Tendt
	FROM TBLDeTai DT,TBLHuongDan HD
	WHERE HD.Madt = DT.Madt
	GROUP BY DT.MADT,Tendt
	HAVING  COUNT(HD.MaSV) >2

--6.	Đưa ra mã số, họ tên và điểm của các sinh viên khoa ‘DIALY và QLTN’

	SELECT SV.Masv,Hotensv,KetQua 
	FROM TBLSinhVien SV, TBLHuongDan HD, TBLKhoa K, TBLGiangVien GV
	WHERE SV.Makhoa=K.Makhoa  AND HD.Magv=GV.Magv AND SV.Masv=HD.Masv AND Tenkhoa='Dia ly va QLTN'

--7.	Đưa ra tên khoa, số lượng sinh viên của mỗi khoa

	SELECT Tenkhoa, COUNT(MASV) AS 'SỐ SINH VIÊN'
	FROM TBLSinhVien SV LEFT JOIN TBLKhoa K ON SV.Makhoa =K.Makhoa
	GROUP BY Tenkhoa

--8.	Cho biết thông tin về các sinh viên thực tập tại quê nhà

	SELECT SV.*
	FROM TBLSinhVien SV, TBLDeTai DT, TBLHuongDan HD
	WHERE SV.Masv=HD.Masv AND HD.Madt= DT.Madt AND Quequan=Noithuctap

--9.	Hãy cho biết thông tin về những sinh viên chưa có điểm thực tập

	SELECT *
	FROM TBLSinhVien SV
	WHERE NOT EXISTS(SELECT MADT FROM TBLHuongDan HD WHERE SV.Masv = HD.Masv)

--10.	 Đưa ra danh sách gồm mã số, họ tên các sinh viên có điểm thực tập bằng 0

	SELECT SV.MASV, Hotensv
	FROM TBLSinhVien SV, TBLHuongDan HD
	WHERE SV.Masv=HD.Masv AND KetQua=0