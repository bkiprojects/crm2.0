using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BKI_CRM.Models
{
    public class ThongTinKhachHang
    {
        public Guid ID { get; set; }
        public string TEN_KHACH_HANG { get; set; }
        public string SDT { get; set; }
        public string EMAIL { get; set; }
        public string DIA_CHI { get; set; }
        public string THONG_TIN_BO_SUNG_01 { get; set; }
        public string THONG_TIN_BO_SUNG_02 { get; set; }
    }
}