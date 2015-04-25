using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BKI_CRM.Models;
using BKI_CRM.Model;

namespace BKI_CRM.Models
{
    public class PhanQuyenHeThong
    {
        public bool checkQuyenTruyCap(string ip_id_user_group, string ip_controller_name, string ip_action_name) {
            BKI_CRMEntities v_model = new BKI_CRMEntities();
            Guid v_guid_id_user_group = new Guid(ip_id_user_group);
            var v_q = v_model.V_HT_PHAN_QUYEN_CHI_TIET.Where(x => x.ACTION_NAME.ToUpper().Trim() == ip_action_name.ToUpper().Trim() && x.CONTROLLER_NAME.ToUpper().Trim() == ip_controller_name.ToUpper().Trim() && x.ID_USER_GROUP == v_guid_id_user_group).ToList();
            //if (v_q.Count == 0)
            //{
            //    return false;
            //}
            return true;
        }
    }
}