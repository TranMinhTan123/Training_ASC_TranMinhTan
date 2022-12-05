using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTH5_EFW
{
    public class Class1
    {
        ThucTapEntities _entities = new ThucTapEntities();
        #region 1.	Đưa ra danh sách gồm mã số, họ tên các sinh viên có điểm thực tập bằng 0 
        public void DSSinhVien()
        {
            foreach (var query in 
               from sv in _entities.TBLSinhViens
               join hd in _entities.TBLHuongDans on sv.Masv equals hd.Masv
               where hd.KetQua == 0
               select new { MaSV = sv.Masv, Meta = sv.Hotensv })
            {
                Console.WriteLine("\tMa SV: {0} \t Ho ten SV: {1}", query.MaSV, query.Meta);
            }
        }
        #endregion
        #region 2.	Đếm số lượng sinh viên thực thập
        public int DemSoLuongSVThucTap()
        {
            List<TBLHuongDan> ds = _entities.TBLHuongDans.ToList();
            int dem = ds.Count(t=>t.Masv!=0);
            return dem;
        }
        #endregion
        # region 3.	In ra màn hình danh sách HoTen sinh viên
        public void DSHoTenSinhVien()
        {     
            List<TBLSinhVien> ds = _entities.TBLSinhViens.ToList();
            foreach (var item in ds)
            {
                var kq = item.Hotensv;
                Console.WriteLine("\t"+kq);
            }
                
        }
        #endregion

        # region 4.	Thêm một sinh viên tên: Ngô Nhật Long/Bio/1993/Vung Tau
        public void ThemSinhVien()
        {
            TBLSinhVien sv = new TBLSinhVien();
            sv.Masv = 10;
            sv.Hotensv = "Ngô Nhật Long";
            sv.Makhoa = "Bio";
            sv.Namsinh = 1993;
            sv.Quequan = "Vung Tau";

            _entities.TBLSinhViens.Add(sv);
            _entities.SaveChanges();
        }
        // Kiem tra danh sách sinh viên khi thêm
        public void ktDanhSachSV()
        {
            List<TBLSinhVien> ds = _entities.TBLSinhViens.ToList();
            foreach (var item in ds)
            {
                var kq = item.Masv +"\t"+ item.Hotensv + item.Makhoa + item.Namsinh +"\t"+ item.Quequan;
                Console.WriteLine("\t" + kq);
            }

        }
        #endregion
        # region 5.	Cập nhật sinh viên 'Tran Khac Trong' năm sinh 2018, Quê quán Ha nam
        public void CapNhatSinhVien()
        {
            TBLSinhVien sv = _entities.TBLSinhViens.SingleOrDefault(t => t.Hotensv == "Tran Khac Trong");
            sv.Namsinh = 2018;
            sv.Quequan = "Ha nam";
            _entities.SaveChanges();
        }
        #endregion
        # region 6.	Xóa đề tài 'Dt03'
        public void XoaDeTai()
        {
            List<TBLHuongDan> del1 = _entities.TBLHuongDans.Where(t => t.Madt == "Dt03").ToList();
            //TBLHuongDan del1 = _entities.TBLHuongDans.(t => t.Madt == "Dt03");
            TBLDeTai del2 = _entities.TBLDeTais.SingleOrDefault(t => t.Madt == "Dt03");
            try
            {
                if (del2 != null)
                {
                    foreach (var item in del1)
                    {
                        _entities.TBLHuongDans.Remove(item);
                    }
                    _entities.TBLDeTais.Remove(del2);
                    _entities.SaveChanges();
                }
            }
            catch { }
        }
        // Kiem tra danh sách đề tài sau khi xóa
        public void ktDanhSachDeTai()
        {
            List<TBLDeTai> ds = _entities.TBLDeTais.ToList();
            foreach (var item in ds)
            {
                var kq = item.Madt + item.Tendt + item.Kinhphi +"\t"+ item.Noithuctap ;
                Console.WriteLine("\t" + kq);
            }

        }
        #endregion
        #region 7.	Đếm số lượng sinh viên của mỗi đề tài
        public void DemSoLuongSVMoiDeTai()
        {
            foreach (var line in _entities.TBLHuongDans.GroupBy(info => info.Madt)
                                    .Select(group => new {
                                        Madt = group.Key,
                                        SoSV = group.Count()
                                    })
                                    .OrderBy(x => x.Madt))
            {
                Console.WriteLine("Ma de tai: {0} - So Sinh Vien: {1}", line.Madt, line.SoSV);
            }
        }
        #endregion
    }
}
