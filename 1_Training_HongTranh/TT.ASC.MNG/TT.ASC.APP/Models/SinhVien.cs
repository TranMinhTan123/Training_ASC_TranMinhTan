using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TT.ASC.APP.Models
{
    public class SinhVien
    {
        public int ID { get; set; }
        public string MaSV { get; set; }
        public string HoDem { get; set; }
        public string Ten { get; set; }
        public int GioiTinh { get; set; }
        public int LopHoc { get; set; }
        public SinhVien(int pID, string pMa,string pHoDem, string pTen,int pGT,int pLop)
        {
            ID = pID;
            MaSV = pMa;
            HoDem = pHoDem;
            Ten = pTen;
            GioiTinh = pGT;
            LopHoc = pLop;
        }
        public SinhVien()
        {

        }
    }

}