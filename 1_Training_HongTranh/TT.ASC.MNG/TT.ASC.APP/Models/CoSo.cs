using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TT.ASC.APP.Models
{
    public class CoSo
    {
        public int ID { get; set; }
        public string Ma { get; set; }
        public string Ten { get; set; }

        public CoSo(int pID,string pMa, string pTen)
        {
            ID=pID;
            Ma=pMa;
            Ten=pTen;
        }
        public CoSo()
        {

        }
    }
}