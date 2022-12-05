using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TT.ASC.APP.Models
{
    public class LopHoc
    {
        public int ID { get; set; }
        public string MaLop { get; set; }
        public string TenLop { get; set; }
        public int SoLuong { get; set; }
        public int NamMoLop { get; set; }
        public int CoSoDaoTao { get; set; }
        public bool HienThi { get; set; }
        public LopHoc(int pID, string pMa, string pTenLop,int pSL,int pNamML,int pCoSoDT, bool pHienThi)
        {
            ID= pID;
            MaLop = pMa;
            TenLop = pTenLop;
            SoLuong = pSL;
            NamMoLop = pNamML;
            CoSoDaoTao = pCoSoDT;
            HienThi = pHienThi;
        }
        public LopHoc()
        {

        }
    }
}