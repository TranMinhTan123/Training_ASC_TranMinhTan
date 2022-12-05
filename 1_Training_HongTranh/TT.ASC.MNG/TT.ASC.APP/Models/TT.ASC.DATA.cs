using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace TT.ASC.APP.Models
{
    public class TT
    {
        //Buổi 1
        #region 1.	Viết hàm để định dạng ngày tháng năm, giờ phút giây
        public string DinhDangDateTime(DateTime date)
        {
            string kq = "";
            kq = string.Format("{0:dd/MM/yyyy HH:mm:ss}", date);
            return kq;
        }
        #endregion 
        #region 2.	Viết hàm để Đọc dãy số bất kỳ
        public string docSo(int chuoi)
        {
            string kq="";
            switch (chuoi)
            {
                case 1:
                    return kq = "Một";
                    break;
                case 2:
                    return kq = "Hai";
                    break;
                case 3:
                    return kq = "Ba";
                    break;
                case 4:
                    return kq = "Bốn";
                    break;
                case 5:
                    return kq = "Năm";
                    break;
                case 6:
                    return kq = "Sáu";
                    break;
                case 7:
                    return kq = "Bảy";
                    break;
                case 8:
                    return kq = "Tám";
                    break;
                case 9:
                    return kq = "Chín";
                    break;
                case 0:
                    return kq = "Không";
                    break;
            } return kq;
        }
        public string DocChuoi(string chuoi)
        {
            string kq = "";
            if(chuoi!="")
            {
                for(int i=0;i<chuoi.Trim().Length;i++)
                {
                    kq += docSo(int.Parse(chuoi.Substring(i, 1)))+" "; 
                }
            } return kq;
        }
        #endregion
        #region 3.	Viết hàm đọc số tiền bất kỳ
        static string[] mNumText = "không;một;hai;ba;bốn;năm;sáu;bảy;tám;chín".Split(';');

        private static string DocHangChuc(double so, bool daydu)
        {
            string chuoi = "";
            //Hàm để lấy số hàng chục ví dụ 21/10 = 2
            Int64 chuc = Convert.ToInt64(Math.Floor((double)(so / 10)));
            //Lấy số hàng đơn vị bằng phép chia 21 % 10 = 1
            Int64 donvi = (Int64)so % 10;
            //Nếu số hàng chục tồn tại tức >=20
            if (chuc > 1)
            {
                chuoi = " " + mNumText[chuc] + " mươi";
                if (donvi == 1)
                {
                    chuoi += " mốt";
                }
            }
            else if (chuc == 1)
            {//Số hàng chục từ 10-19
                chuoi = " mười";
                if (donvi == 1)
                {
                    chuoi += " một";
                }
            }
            else if (daydu && donvi > 0)
            {//Nếu hàng đơn vị khác 0 và có các số hàng trăm ví dụ 101 => thì biến daydu = true => và sẽ đọc một trăm lẻ một
                chuoi = " lẻ";
            }
            if (donvi == 5 && chuc >= 1)
            {//Nếu đơn vị là số 5 và có hàng chục thì chuỗi sẽ là " lăm" chứ không phải là " năm"
                chuoi += " lăm";
            }
            else if (donvi > 1 || (donvi == 1 && chuc == 0))
            {
                chuoi += " " + mNumText[donvi];
            }
            return chuoi;
        }
        private static string DocHangTram(double so, bool daydu)
        {
            string chuoi = "";
            //Lấy số hàng trăm ví du 434 / 100 = 4 (hàm Floor sẽ làm tròn số nguyên bé nhất)
            Int64 tram = Convert.ToInt64(Math.Floor((double)so / 100));
            //Lấy phần còn lại của hàng trăm 434 % 100 = 34 (dư 34)
            so = so % 100;
            if (daydu || tram > 0)
            {
                chuoi = " " + mNumText[tram] + " trăm";
                chuoi += DocHangChuc(so, true);
            }
            else
            {
                chuoi = DocHangChuc(so, false);
            }
            return chuoi;
        }
        private static string DocHangTrieu(double so, bool daydu)
        {
            string chuoi = "";
            //Lấy số hàng triệu
            Int64 trieu = Convert.ToInt64(Math.Floor((double)so / 1000000));
            //Lấy phần dư sau số hàng triệu ví dụ 2,123,000 => so = 123,000
            so = so % 1000000;
            if (trieu > 0)
            {
                chuoi = DocHangTram(trieu, daydu) + " triệu";
                daydu = true;
            }
            //Lấy số hàng nghìn
            Int64 nghin = Convert.ToInt64(Math.Floor((double)so / 1000));
            //Lấy phần dư sau số hàng nghin 
            so = so % 1000;
            if (nghin > 0)
            {
                chuoi += DocHangTram(nghin, daydu) + " nghìn";
                daydu = true;
            }
            if (so > 0)
            {
                chuoi += DocHangTram(so, daydu);
            }
            return chuoi;
        }
        public string ChuyenSoSangChuoi(double so)
        {
            if (so == 0)
                return mNumText[0];
            string chuoi = "", hauto = "";
            Int64 ty;
            do
            {
                //Lấy số hàng tỷ
                ty = Convert.ToInt64(Math.Floor((double)so / 1000000000));
                //Lấy phần dư sau số hàng tỷ
                so = so % 1000000000;
                if (ty > 0)
                {
                    chuoi = DocHangTrieu(so, true) + hauto + chuoi;
                }
                else
                {
                    chuoi = DocHangTrieu(so, false) + hauto + chuoi;
                }
                hauto = " tỷ";
            } while (ty > 0);
            return chuoi + " đồng";
        }
        #endregion
        #region 4.	Viết hàm random chuỗi ký tự và số có chiều dài bất kỳ ( quy định được chiều dài chuỗi, số lượng ký tự số trong chuỗi)
        public string GetRandomString(int lenOfTheNewStr)
        {
            string output = string.Empty;
            while (true)
            {
                output = output + Path.GetRandomFileName().Replace(".", string.Empty);
                if (output.Length > lenOfTheNewStr)
                {
                    output = output.Substring(0, lenOfTheNewStr);
                    break;
                }
            }
            return output;
        }
        #endregion
        #region 5.	Viết hàm truyền vào Tháng và Năm , trả về ngày đầu tiên của tháng
        public  DateTime xuatNgayDauTien(int Year,int _Month)
        {
            var firstDayOfMonth = new DateTime(Year, _Month, 1);
            return firstDayOfMonth;
        }
        #endregion
        #region 6.	Viết hàm truyền vào Tháng và Năm , trả về ngày cuối tháng
        public DateTime xuatNgayCuoiThang(int Year,int _Month)
        {
            var firstDayOfMonth = new DateTime(Year, _Month, 1);
            var lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);
            return lastDayOfMonth;
        }
        #endregion
        #region 7.	Viết hàm truyền vào ngày bất kỳ, trả về ngày đầu tuần ( thứ 2 của tuần đó)
        public DateTime xuatNgayDauTuan(int nam, int thang, int ngay)
        {
            DateTime r = new DateTime();
            var startDate = new DateTime(nam, thang, ngay);
            if (startDate.DayOfWeek != DayOfWeek.Monday)
                r = startDate.Subtract(new TimeSpan((int)startDate.DayOfWeek - 1, 0, 0, 0));
            return r;
        }
        #endregion
        #region 8.	Viết hàm truyền vào ngày bất kỳ, trả về ngày cuối tuần ( chủ nhật của tuần đó)
        public DateTime xuatNgayCuoiTuan(int nam, int thang, int ngay)
        {
            DateTime r = new DateTime();
            var startDate = new DateTime(nam, thang, ngay);
            if (startDate.DayOfWeek != DayOfWeek.Sunday)
                r = startDate.Subtract(new TimeSpan((int)startDate.DayOfWeek - 7, 0, 0, 0));
            return r;
        }
        #endregion
        #region 9.	Viết hàm đếm số khoản trắng trong 1 chuỗi dữ liệu truyền vào.
        public int DemKhoangTrang(string str)
        {
            int bien_dem = 0;
            string str1;
            for (int i = 0; i < str.Length; i++)
            {
                str1 = str.Substring(i, 1);
                if (str1 == " ")
                    bien_dem++;
            }
            return bien_dem;
        }
        #endregion
        #region 10.	 Viết hàm kiểm tra Tính đúng đắn của 1 địa chỉ email truyền vào.
        public string kiemTraEmail(string email)
        {
            string kq = "";
            Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            Match match = regex.Match(email);
            if (match.Success)
                kq = "Email chính xác";
            else
                kq = "Email không chính xác";
            return kq;
        }
        #endregion
        #region 11.	Viết hàm cắt chuỗi họ tên ( tách ra họ và tên đệm khi một chuổi họ tên được truyền vào)
        public string CatChuoiHoTen(string hoVaTen)
        {
            string HoDem = "";
            HoDem = hoVaTen.Substring(0, hoVaTen.LastIndexOf(" "));
            string Ten = "";
            Ten = hoVaTen.Substring(hoVaTen.LastIndexOf(" "), hoVaTen.Length - hoVaTen.LastIndexOf(" "));
            string kq = "Họ đệm là : " + HoDem + " Tên là: " + Ten;
            return kq;
        }
        #endregion
        #region 12. Tìm hiểu các dạng làm tròn và viết hàm làm tròn các số thập phân truyền vào theo các dạng: Floor, Truncate, Round, Ceiling
        #endregion
        #region 13. Viết hàm xử lý viết hoa đầu mỗi ký tự đâu tiên theo tên được truyền vào.
        public string vietHoaKyTuDauTien(string kyTu)
        {
            char[] charArray = kyTu.ToLower().ToCharArray();
            bool foundSpace = true;
            for (int i = 0; i < charArray.Length; i++)
            {
                if (Char.IsLetter(charArray[i]))
                {
                    if (foundSpace)
                    {
                        charArray[i] = Char.ToUpper(charArray[i]);
                        foundSpace = false;
                    }
                }
                else
                {
                    foundSpace = true;
                }
            }
            kyTu = new string(charArray);
            return kyTu;
        }
        #endregion
        //Buổi 2
        #region 1.	Viết hàm kiểm tra chuỗi ký tự, kết quả trả về xác định được trong chuỗi có bao nhiêu ký tự được tìm và ở vị trí bao nhiêu.
        public string ViTriChuoi(string pChuoiKT, char pKyTu)
        {
            string ViTri="";
            int dem = 0;
            string kq = "";
            for (int i = 0; i < pChuoiKT.Length; i++)
            {
                char kyTu = Convert.ToChar(pChuoiKT.Substring(i,1));
                if (kyTu == pKyTu)
                {
                    dem++;
                    ViTri = ViTri +i +" ";
                    kq = "Số lượng: " + dem + " - Vị trí: " + ViTri;
                }
            }
            return kq;
        }
        #endregion
        #region 2.	Viết hàm random họ tên theo danh sách 3 mảng các từ có sẵn (Họ, Đệm, Tên)
        public string RandomHoTen()
        {
            string kq = "";
            var r = new Random();
            var ho = new List<string> {"Nguyễn", "Trần", "Lê", "Hồ" };
            var dem = new List<string> { "Văn", "Thanh", "Hoàng", "Thị", "Kim", "Ngọc" };
            var ten = new List<string> { "Nhân", "Hậu", "Nghĩa", "Lễ", "Trí", "Tín", "Trang", "Thúy", "Phương", "Hiếu" };
            kq= kq + ho[r.Next(0, ho.Count)] + " " + dem[r.Next(0, ho.Count)]
                + " " + ten[r.Next(0, ho.Count)];
            return kq;
        }
        #endregion
        #region 3
        public List<CoSo> ListCoSo(int pSoLuong)
        {
            List<CoSo> listCoSo = new List<CoSo>();
            Random r = new Random();
            for (int i = 0; i < pSoLuong; i++)
            {
                CoSo coSo = new CoSo();
                coSo.ID = i;
                coSo.Ma = "CS" + r.Next(100, 999);
                coSo.Ten = "Cơ sở " + r.Next(0, 999);
                listCoSo.Add(coSo);
            }
            return listCoSo;
        }
        public List<LopHoc> ListLopHoc(int pSoLuong)
        {
            List<LopHoc> listLopHoc = new List<LopHoc>();
            Random r = new Random();
            for (int i = 0; i < pSoLuong; i++)
            {
                LopHoc lop = new LopHoc();
                lop.ID = i;
                lop.MaLop = "LH" + r.Next(100, 999);
                lop.TenLop = "10DHTH " + r.Next(0, 999);
                lop.SoLuong = 20;
                lop.NamMoLop = 2020;
                lop.CoSoDaoTao= r.Next(0, 999);
                lop.HienThi = true;

                listLopHoc.Add(lop);
            }
            return listLopHoc;
        }
        public List<SinhVien> ListSinhVien()
        {
            List<SinhVien> listSinhVien = new List<SinhVien>();
            Random r = new Random();
            for (int i = 0; i < 20; i++)
            {
                SinhVien sv = new SinhVien();
                sv.ID = i;
                //sv.ID = r.Next(100, 999);
                sv.MaSV = "SV" + r.Next(100, 999);
                sv.HoDem = "";
                sv.Ten = "";
                sv.GioiTinh = 2020;
                sv.LopHoc = r.Next(0, 999);

                listSinhVien.Add(sv);
            }
            return listSinhVien;
        }
        #endregion

    }
}