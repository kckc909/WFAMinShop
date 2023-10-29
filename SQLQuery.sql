use TestCase
Drop database MinShopWFA
create database MinShopWFA
use MinShopWFA
go

create table LoaiHang(
	LH_Id nvarchar(15) primary key,
	LH_Name nvarchar(15)
)
create table MatHang(
	MH_Id nvarchar(15) primary key,
	MH_Name nvarchar(50),
	MH_Description nvarchar(50),
	MH_Quantity int,
	MH_Unit nvarchar(15),
	LH_Id nvarchar(15) references LoaiHang(LH_Id) on update cascade on delete cascade
)
create table KhachHang(
	KH_Id nvarchar(15) primary key,
	KH_Name nvarchar(50),
	KH_Sex nvarchar(5),
	KH_PhoneNumbers nvarchar(11),
	KH_Address nvarchar(50)
)
create table NhanVien(
	NV_Id nvarchar(15) primary key ,
	NV_Name nvarchar(50),
	NV_Sex nvarchar(5),
	NV_Birthday date,
	NV_PhoneNumbers nvarchar(11),
	NV_Address nvarchar(50)
)
create table NhaCungCap(
	NCC_Id nvarchar(15) primary key,
	NCC_Name nvarchar(50),
	NCC_PhoneNumbers nvarchar(11),
	NCC_Address nvarchar(50)
)
create table HoaDonBan(
	HDB_Id nvarchar(15) primary key,
	NV_Id nvarchar(15) references NhanVien(NV_Id) on update cascade on delete cascade,
	KH_Id nvarchar(15) references KhachHang(KH_Id) on update cascade on delete cascade,
	NgayBan date,
)
create table ChiTietHDB(
	HDB_Id nvarchar(15) references HoaDonBan(HDB_Id) on update cascade on delete cascade,
	MH_Id nvarchar(15) references MatHang(MH_Id) on update cascade on delete cascade,
	Quantity int,
	Unit nvarchar(15),
	constraint PR_ChiTietHDB Primary key (HDB_Id, MH_Id)
)
create table HoaDonNhap(
	HDN_Id nvarchar(15) primary key,
	NV_Id nvarchar(15) references NhanVien(NV_Id) on update cascade on delete cascade,
	NCC_Id nvarchar(15) references NhaCungCap(NCC_Id)on update cascade on delete cascade,
	NgayNhap date
)
Create table ChiTietHDN(
	HDN_Id nvarchar(15) references HoaDonNhap(HDN_Id) on update cascade on delete cascade,
	MH_Id nvarchar(15) references MatHang(MH_Id) on update cascade on delete cascade,
	Quantity int,
	Unit nvarchar(15),
	constraint PR_ChiTietHDN primary key (HDN_Id, MH_Id)
)
go 
-- Nhập liệu

INSERT INTO LoaiHang(LH_Id, LH_Name)
VALUES
('LH001', 'Thực phẩm'),
('LH002', 'Điện tử'),
('LH003', 'Thời trang'),
('LH004', 'Đồ gia dụng'),
('LH005', 'Mỹ phẩm');
go
INSERT INTO MatHang(MH_Id, MH_Name, MH_Description, MH_Quantity, MH_Unit, LH_Id) 
VALUES 
('MH001', N'Gạo', N'Gạo Bắp thơm', 100, 'Kg', 'LH001'),
('MH002', N'Điện thoại', N'Điện thoại di động', 50, N'Chiếc', 'LH002'),
('MH003', N'Áo sơ mi', N'Áo sơ mi nam', 20, N'Cái' , 'LH003'),
('MH004', N'Bếp gas', N'Bếp gas 3 bếp', 10, N'Cái', 'LH004'),
('MH005', N'Son môi', N'Son môi màu hồng', 30, N'Cây', 'LH005'),
('MH006', N'Bánh mì', N'Bánh mì trắng', 200, N'Cái', 'LH001'),
('MH007', N'Máy tính xách tay', N'Máy tính xách tay', 15, N'Chiếc', 'LH002'),
('MH008', N'Quần jeans', N'Quần jeans nam', 25, N'Cái', 'LH003'),
('MH009', N'Lò vi sóng', N'Lò vi sóng 20l', 5, N'Cái', 'LH004'),
('MH010', N'Mascara', N'Mascara dày mi', 35, N'Tuýt', 'LH005'),
('MH011', N'Cá hồi', N'Cá hồi tươi sống', 50, N'Kg', 'LH001'),
('MH012', N'Ổ cứng di động', N'Ổ cứng di động 1TB', 30, N'Chiếc', 'LH002'),
('MH013', N'Áo khoác dạ', N'Áo khoác dạ nữ', 15, N'Cái', 'LH003'),
('MH014', N'Tủ lạnh', N'Tủ lạnh 2 cánh', 10, N'Cái', 'LH004'),
('MH015', N'Mascara', N'Mascara đen dày mi', 40, N'Tuýt', 'LH005');
go
INSERT INTO KhachHang(KH_Id, KH_Name, KH_Sex, KH_PhoneNumbers, KH_Address) 
VALUES 
('KH001', 'Nguyễn Văn Anh', 'Nam', '0987654321', '12 Nguyễn Du, Hà Nội'),
('KH002', 'Trần Thị Bình', 'Nữ', '0901234567', '34 Lê Lợi, Hưng Yên'),
('KH003', 'Phạm Thanh Hà', 'Nữ', '0912345678', '56 Trần Quang Khải, Đà Nẵng'),
('KH004', 'Lê Minh Đức', 'Nam', '0976543210', '78 Hai Bà Trưng, Hà Nội'),
('KH005', 'Nguyễn Mai Phương', 'Nữ', '0965432109', '90 Ngô Quyền, Hải Phòng'),
('KH006', 'Trần Văn Nam', 'Nam', '0911111111', '8 Ba Đình, Hưng Yên'),
('KH007', 'Nguyễn Thị Thu', 'Nữ', '0900000000', '10 Đường số 3, Hà Nội'),
('KH008', 'Hoàng Quốc Tuấn', 'Nam', '0988888888', '20 Nguyễn Cao, Hưng Yên'),
('KH009', 'Trần Thị Lệ', 'Nữ', '0977777777', '30 Lý Thường Kiệt, Đà Nẵng'),
('KH010', 'Nguyễn Văn Tùng', 'Nam', '0999999999', '40 Trường Chinh, Hà Nội');
go
INSERT INTO NhanVien(NV_Id, NV_Name, NV_Sex, NV_Birthday, NV_PhoneNumbers, NV_Address) 
VALUES 
('NV001', 'Nguyễn Thị Lan', 'Nữ', '1991-03-21', '0987654321', '789 Trần Phú, Hà Nội'),
('NV002', 'Trần Văn Thắng', 'Nam', '1988-07-10', '0901234567', '567 Quang Trung, Hưng Yên'),
('NV003', 'Phạm Thị Hương', 'Nữ', '1994-11-15', '0912345678', '456 Lê Lai, Đà Nẵng'),
('NV004', 'Lê Hoàng Nam', 'Nam', '1990-06-05', '0976543210', '890 Nguyễn Gia Thiều, Hà Nội'),
('NV005', 'Nguyễn Thị Thu Trang', 'Nữ', '1993-08-25', '0965432109', '123 Lý Thường Kiệt, Hưng Yên'),
('NV006', 'Trần Văn Quốc', 'Nam', '1989-12-31', '09119621111', '234 Nguyễn Chí Thanh, Hà Nội'),
('NV007', 'Nguyễn Thị Loan', 'Nữ', '1995-05-30', '0900234000', '345 Đường số 4, Hưng Yên'),
('NV008', 'Hoàng Đình Tú', 'Nam', '1992-04-17', '0988767888', '678 Trần Tự Trân, Đà Nẵng'),
('NV009', 'Trần Thị Minh', 'Nữ', '1996-01-12', '0977123777', '890 Lê Lợi, Hà Nội'),
('NV010', 'Nguyễn Văn Nam', 'Nam', '1997-09-28', '0923789999', '567 Trường Thành, Hưng Yên');
go
INSERT INTO NhaCungCap(NCC_Id, NCC_Name, NCC_PhoneNumbers, NCC_Address) 
VALUES 
('NCC001', 'Công ty Thực phẩm', '0987000001', '123 Đường 1, Hà Nội'),
('NCC002', 'Công ty Điện tử', '0912000003', '789 Đường 3, Đà Nẵng'),
('NCC003', 'Công ty Thời trang', '0965000005', '890 Đường 5, Hải Phòng'),
('NCC004', 'Công ty Đồ gia dụng', '0977000009', '123 Đường 9, Đà Nẵng'),
('NCC005', 'Công ty Mỹ phẩm', '0999000010', '345 Đường 10, Hà Nội');
go
INSERT INTO HoaDonBan (HDB_Id, NV_Id, KH_Id, NgayBan)
VALUES
('HDB001', 'NV010', 'KH001', '2023-09-01'),
('HDB002', 'NV002', 'KH002', '2023-09-02'),
('HDB003', 'NV005', 'KH003', '2023-09-03'),
('HDB004', 'NV004', 'KH004', '2023-09-04'),
('HDB005', 'NV001', 'KH005', '2023-09-05'),
('HDB006', 'NV006', 'KH006', '2023-09-06'),
('HDB007', 'NV003', 'KH007', '2023-09-07'),
('HDB008', 'NV007', 'KH008', '2023-09-08'),
('HDB009', 'NV001', 'KH009', '2023-09-09'),
('HDB010', 'NV008', 'KH010', '2023-09-10');
go
INSERT INTO ChiTietHDB (HDB_Id, MH_Id, Quantity, Unit)
VALUES
('HDB001', 'MH001', 3, 'Kg'),
('HDB002', 'MH002', 2, N'Chiếc'),
('HDB003', 'MH003', 1, N'Cái'),
('HDB004', 'MH004', 2, N'Cái'),
('HDB005', 'MH005', 3, N'Cây'),
('HDB006', 'MH006', 4, N'Cái'),
('HDB007', 'MH007', 1, N'Chiếc'),
('HDB008', 'MH008', 2, N'Cái'),
('HDB009', 'MH009', 1, N'Cái'),
('HDB010', 'MH010', 3, N'Tuýt');
go
INSERT INTO HoaDonNhap (HDN_Id, NV_Id, NCC_Id, NgayNhap)
VALUES
('HDN001', 'NV009', 'NCC001', '2023-09-01'),
('HDN002', 'NV002', 'NCC002', '2023-09-02'),
('HDN003', 'NV003', 'NCC003', '2023-09-03'),
('HDN004', 'NV004', 'NCC004', '2023-09-04'),
('HDN005', 'NV001', 'NCC005', '2023-09-05');
go
INSERT INTO ChiTietHDN(HDN_Id, MH_Id, Quantity, Unit)
VALUES 
    ('HDN001', 'MH001', 100, 'Kg'),
    ('HDN002', 'MH002', 50, N'Chiếc'),
    ('HDN003', 'MH003', 20, N'Cái'),
    ('HDN004', 'MH004', 10, N'Cái'),
    ('HDN005', 'MH005', 30, N'Cây'),
    ('HDN001', 'MH006', 200, N'Cái'),
    ('HDN002', 'MH007', 15, N'Chiếc'),
    ('HDN003', 'MH008', 25, N'Cái'),
    ('HDN004', 'MH009', 5, N'Cái'),
    ('HDN005', 'MH010', 35, N'Tuýt'),
    ('HDN001', 'MH011', 50, N'Kg'),
    ('HDN002', 'MH012', 30, N'Chiếc'),
    ('HDN003', 'MH013', 15, N'Cái'),
    ('HDN004', 'MH014', 10, N'Cái'),
    ('HDN005', 'MH015', 40, N'Tuýt');
go



