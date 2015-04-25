using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BKI_CRM.Models
{
    public class TrangThaiModel
    {
        public Guid ID { get; set; }
        public string MA_TRANG_THAI { get; set; }
        public string MO_TA { get; set; }
        public bool LOAI_TRANG_THAI_YN { get; set; }
        public List<TrangThaiModel> LST_TRANG_THAI_TIEP_THEO { get; set; }
    }
}