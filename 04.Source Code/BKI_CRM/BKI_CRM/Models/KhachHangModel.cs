using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BKI_CRM.Model;

namespace BKI_CRM.Models
{
    public class KhachHangModel:V_GD_KHACH_HANG_CHUYEN_TRANG_THAI
    {
        public List<V_GD_CHUYEN_TRANG_THAI> LstChuyenTrangThai;
        public Guid? ID_CONG_TY { get; set; }
        public Guid ID_KHACH_HANG { get; set; }
        public string TEN_KHACH_HANG { get; set; }
    }
}