using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BTH5_EFW;
namespace BTH5_EntityFrameWork_TranMinhTan
{
    class Program
    {
        static void Main(string[] args)
        {
            Class1 c = new Class1();
            //1
            Console.WriteLine("\n---------------Câu 1----------------");
            Console.WriteLine("\nDanh sach Ma, Ho Ten Sinh Vien: ");
            c.DSSinhVien();
            //2
            Console.WriteLine("\n---------------Câu 2----------------");
            Console.WriteLine("\nSo luong sinh vien thuc tap: "+c.DemSoLuongSVThucTap());

            //3
            Console.WriteLine("\n---------------Câu 3----------------");
            Console.WriteLine("\nDanh sach ho ten sinh vien:");
            c.DSHoTenSinhVien();

            //4
            Console.WriteLine("\n-------------Câu 4,5---------------");
            Console.WriteLine("\nDanh sach Sinh vien ban dau:");
            c.ktDanhSachSV();
            Console.WriteLine("\n4. Danh sach Sinh vien sau khi them:");
            //c.ThemSinhVien();
            c.ktDanhSachSV();

            //5
            Console.WriteLine("\n5. Danh sach sinh vien sau khi cap nhat:");
            c.CapNhatSinhVien();
            c.ktDanhSachSV();

            //6
            Console.WriteLine("\n---------------Câu 6----------------");
            Console.WriteLine("\nDanh sach de tai ban dau:");
            c.ktDanhSachDeTai();
            Console.WriteLine("\nDanh sach de tai sau khi xoa:");
            c.XoaDeTai();
            c.ktDanhSachDeTai();

            //7
            Console.WriteLine("\n---------------Câu 7----------------");
            c.DemSoLuongSVMoiDeTai();
            Console.ReadLine();
        }
    }
}
