using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TT.ASC.APP.Models;

namespace TT.ASC.APP.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/
        /// <summary>
        /// Trần Minh Tân
        /// </summary>
        TT.ASC.APP.Models.TT t = new TT.ASC.APP.Models.TT();
        public ActionResult Index()
        {
            return View();
        }
        //1. Viết hàm để định dạng ngày tháng năm, giờ phút giây
        [HttpGet]
        public ActionResult dinhDangThoiGian()
        {
            return View();
        }
        [HttpPost]
        public ActionResult XuLyDinhDangThoiGian(FormCollection col)
        {
            DateTime date = DateTime.Parse(col["txtDate"]);
            ViewBag.kq = "Kết quả " + t.DinhDangDateTime(date);
            return View("dinhDangThoiGian");
        }
        //2. Viết hàm để Đọc dãy số bất kỳ
        [HttpGet]
        public ActionResult docDaySo()
        {
            return View();
        }
        [HttpPost]
        public ActionResult XuLydocDaySo(FormCollection col)
        {
            string daySo = col["txtDaySo"];
            ViewBag.kq = "Kết quả: " + t.DocChuoi(daySo);
            return View("docDaySo");
        }
        //3. Viết hàm đọc số tiền bất kỳ
        [HttpGet]
        public ActionResult docSoTien()
        {
            return View();
        }
        [HttpPost]
        public ActionResult XuLyDocSoTien(FormCollection col)
        {
            string SoTien = col["txtSoTien"];
            ViewBag.kq = "Số tiền là: " + t.ChuyenSoSangChuoi(double.Parse(SoTien));
            return View("docSoTien");
        }

        //4.	Viết hàm random chuỗi ký tự và số có chiều dài bất kỳ ( quy định được chiều dài chuỗi, số lượng ký tự số trong chuỗi)
        [HttpGet]
        public ActionResult randomKyTu()
        {
            return View();
        }
        [HttpPost]
        public ActionResult XuLyRandomKyTu(FormCollection col)
        {
            int soKyTu = int.Parse(col["txtSoKyTu"]);
            string output = t.GetRandomString(soKyTu);
            ViewBag.kq = output.ToUpper();
            return View("randomKyTu");
        }

        //5. Viết hàm truyền vào Tháng và Năm, trả về ngày đầu tiên của tháng
        [HttpGet]
        public ActionResult xuatNgayDauTien()
        {
            return View();
        }
        [HttpPost]
        public ActionResult XuLyXuatNgayDauTien(FormCollection col)
        {
            string thang = col["txtThang"];
            string nam = col["txtNam"];
            ViewBag.kq = "Kết quả: " + t.xuatNgayDauTien(int.Parse(nam), int.Parse(thang));
            return View("xuatNgayDauTien");
        }
        //6.	Viết hàm truyền vào Tháng và Năm, trả về ngày cuối tháng
        [HttpGet]
        public ActionResult xuatNgayCuoiThang()
        {
            return View();
        }
        [HttpPost]
        public ActionResult XuLyXuatNgayCuoiThang(FormCollection col)
        {
            string thang = col["txtThang"];
            string nam = col["txtNam"];
            ViewBag.kq = "Kết quả: " + t.xuatNgayCuoiThang(int.Parse(nam), int.Parse(thang));
            return View("xuatNgayCuoiThang");
        }
        //7.Viết hàm truyền vào ngày bất kỳ, trả về ngày đầu tuần (thứ 2 của tuần đó)
        [HttpGet]
        public ActionResult xuatNgayDauTuan()
        {
            return View();
        }
        [HttpPost]
        public ActionResult XuLyXuatNgayDauTuan(FormCollection col)
        {
            string ngay = col["txtNgay"];
            string thang = col["txtThang"];
            string nam = col["txtNam"];
            ViewBag.kq = "Kết quả: " + t.xuatNgayDauTuan(int.Parse(nam), int.Parse(thang), int.Parse(ngay));
            return View("xuatNgayDauTuan");
        }
        //8. Viết hàm truyền vào ngày bất kỳ, trả về ngày cuối tuần ( chủ nhật của tuần đó)
        [HttpGet]
        public ActionResult xuatNgayCuoiTuan()
        {
            return View();
        }
        [HttpPost]
        public ActionResult XuLyXuatNgayCuoiTuan(FormCollection col)
        {
            string ngay = col["txtNgay"];
            string thang = col["txtThang"];
            string nam = col["txtNam"];
            ViewBag.kq = "Kết quả: " + t.xuatNgayCuoiTuan(int.Parse(nam), int.Parse(thang), int.Parse(ngay));
            return View("xuatNgayCuoiTuan");
        }
        //9. Viết hàm đếm số khoản trắng trong 1 chuỗi dữ liệu truyền vào.
        [HttpGet]
        public ActionResult demKhoangTrang()
        {
            return View();
        }
        [HttpPost]
        public ActionResult XuLyDemKhoangTrang(FormCollection col)
        {
            string Chuoi = col["txtChuoi"];
            ViewBag.kq = "Chuỗi trên chứa " + t.DemKhoangTrang(Chuoi) + " Khoảng trắng";
            return View("demKhoangTrang");
        }
        //10. Viết hàm kiểm tra Tính đúng đắn của 1 địa chỉ email truyền vào.
        [HttpGet]
        public ActionResult kiemTraEmail()
        {
            return View();
        }
        [HttpPost]
        public ActionResult XuLyKiemTraEmail(FormCollection col)
        {
            string email = col["txtEmail"];
            ViewBag.kq = "Kết quả: " + t.kiemTraEmail(email);
            return View("kiemTraEmail");
        }
        //11.	Viết hàm cắt chuỗi họ tên ( tách ra họ và tên đệm khi một chuổi họ tên được truyền vào)
        [HttpGet]
        public ActionResult catChuoiHoTen()
        {
            return View();
        }
        [HttpPost]
        public ActionResult XuLyCatChuoiHoTen(FormCollection col)
        {
            string hoTen = col["txtHoTen"];
            ViewBag.kq = "Kết quả: " + t.CatChuoiHoTen(hoTen);
            return View("catChuoiHoTen");
        }
        //12. Tìm hiểu các dạng làm tròn và viết hàm làm tròn các số thập phân truyền vào theo các dạng: Floor, Truncate, Round, Ceiling
        [HttpGet]
        public ActionResult lamTronSoThapPhan()
        {
            return View();
        }
        [HttpPost]
        public ActionResult XuLyLamTronSoThapPhan(FormCollection col)
        {
            double soThapPhan = double.Parse(col["txtSoThapPhan"]);
            ViewBag.kq = "Số " + soThapPhan + " sau khi lam tron hai chu so thap  phan la: " + Math.Round(soThapPhan, 2);
            return View("catChuoiHoTen");
        }
        //13. Viết hàm xử lý viết hoa đầu mỗi ký tự đâu tiên theo tên được truyền vào.
        [HttpGet]
        public ActionResult vietHoaKyTuDauTien()
        {
            return View();
        }
        [HttpPost]
        public ActionResult XuLyVietHoaKyTuDauTien(FormCollection col)
        {
            string kyTu = col["txtKyTu"];
            ViewBag.kq = "Ký tự sau khi chuyển đổi: " + t.vietHoaKyTuDauTien(kyTu);
            return View("vietHoaKyTuDauTien");
        }

        //Buổi 2
        //1.	Viết hàm kiểm tra chuỗi ký tự, kết quả trả về xác định được trong chuỗi có bao nhiêu ký tự được tìm và ở vị trí bao nhiêu.
        [HttpGet]
        public ActionResult TimViTriKT()
        {
            return View();
        }
        [HttpPost]
        public ActionResult XuLyTimViTriKT(FormCollection col)
        {
            string ChuoiKyTu = col["txtChuoiKyTu"];
            char KyTu = char.Parse(col["txtKyTu"]);
            ViewBag.kq = t.ViTriChuoi(ChuoiKyTu, KyTu);
            return View("TimViTriKT");
        }
        //2.	Viết hàm random họ tên theo danh sách 3 mảng các từ có sẵn (Họ, Đệm, Tên)
        [HttpGet]
        public ActionResult RandomHoTen()
        {
            return View();
        }
        [HttpPost]
        public ActionResult XuLyRandomHoTen(FormCollection col)
        {
            ViewBag.kq = t.RandomHoTen();
            return View("RandomHoTen");
        }
        //3
        [HttpGet]
        public ActionResult TreeView()
        {
            return View();
        }
        [HttpPost]
        public ActionResult TreeViewCoSo(FormCollection col)
        {
            int SoCS = int.Parse(col["txtCoSo"]);
            List<TT.ASC.APP.Models.CoSo> listCoSo = t.ListCoSo(SoCS);
            return View("TreeViewLopHoc", listCoSo);
        }
        [HttpPost]
        public ActionResult TreeViewLopHoc(FormCollection col)
        {
            int SoLop = int.Parse(col["txtLop"]);
            List<TT.ASC.APP.Models.LopHoc> ListLopHoc = t.ListLopHoc(SoLop);
            return View("TreeView", ListLopHoc);
        }
        [HttpPost]
        public ActionResult TreeViewSinhVien(FormCollection col)
        {
            //int SoLop = int.Parse(col["txtLop"]);
            List<TT.ASC.APP.Models.SinhVien> listSinhVien = t.ListSinhVien();
            return View("TreeView", listSinhVien);
        }
    }
}
