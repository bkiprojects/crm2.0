using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BKI_CRM.Models
{
    public class TrangThaiHienTaiModel
    {
        public Nullable<decimal> SO_TIEN_DA_NHAN { get; set; }
        public string GHI_CHU { get; set; }
        public List<DanhSachTrangThaiModel> LST_YEU_CAU;
    }
}