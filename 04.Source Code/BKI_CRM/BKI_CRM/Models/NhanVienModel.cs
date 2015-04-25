using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BKI_CRM.Models
{
    public class NhanVienModel
    {
        public int ID { get; set; }
        public string TEN_NHAN_VIEN { get; set; }
        public string TEN_DANG_NHAP { get; set; }
        public string MAT_KHAU { get; set; }
        public int ID_USER_GROUP { get; set; }
        public int ID_USER_CAP_TREN { get; set; }
        public string ANH_NHAN_VIEN { get; set; }
        public string MA_NHAN_VIEN { get; set; }
        public string SDT01 { get; set; }
        public string SDT02 { get; set; }
        public string DIA_CHI { get; set; }
    }
}