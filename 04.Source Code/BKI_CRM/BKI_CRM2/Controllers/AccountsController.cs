using System;
using System.Collections.Generic;
using System.Data.Objects;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using BKI_CRM2.Models;
using System.IO;
using OpenXMLExcel.SLExcelUtility;
using BKI_CRM2.Controllers;
using System.Net.Mail;
using System.Text;

namespace BKI_CRM2.Controllers
{
    public class AccountsController : Controller
    {
        //
        // GET: /Accounts/

        public ActionResult Index()
        {
            CrmEntities v_model = new CrmEntities();
            List<Account> v_ac = new List<Account>();
            List<TuDien> v_td = new List<TuDien>();
            List<LoaiTuDien> v_ltd = new List<LoaiTuDien>();
            List<V_ACCOUNT> v_tc = new List<V_ACCOUNT>();
            decimal id = 0;
            v_ac = v_model.Account.ToList<Account>();
            v_tc = v_model.V_ACCOUNT.ToList<V_ACCOUNT>();
            v_ltd = v_model.LoaiTuDien.Where(x => x.TenLoaiTuDien == "CompanyType").ToList<LoaiTuDien>();
            if (v_ltd.Count > 0) id = v_ltd.First().Id;
            v_td = v_model.TuDien.Where(x => x.IdLoaiTuDien == id).ToList<TuDien>();
            ViewBag.v_ac = v_ac; ViewBag.v_td = v_td;
            ViewBag.v_tc = v_tc;
            return PartialView();
        }

        public ActionResult Delete(decimal id_ac)
        {
            CrmEntities v_model = new CrmEntities();
            int affected = v_model.pr_Account_Delete(id_ac);
            return Json(affected, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult Select(string IdAccount)
        {
            CrmEntities v_model = new CrmEntities();

            List<TuDien> v_companytype = new List<TuDien>();
            var temp1 = v_model.LoaiTuDien.FirstOrDefault(x => x.MaLoaiTuDien == "CompanyType");
            decimal loaitd = -1; if (temp1 != null) loaitd = temp1.Id;
            v_companytype = v_model.TuDien.Where(x => x.IdLoaiTuDien == loaitd).ToList<TuDien>();
            List<decimal> ids = new List<decimal>(); List<string> names = new List<string>();
            for (int i = 0; i < v_companytype.Count; i++)
            {
                ids.Add(v_companytype[i].Id); names.Add(v_companytype[i].TenTuDien);
            }

            if (IdAccount != null && !IdAccount.Equals(""))
            {
                decimal v_id = Convert.ToDecimal(IdAccount);
                var v_ac = v_model.V_ACCOUNT.Where(x => x.Id == v_id).First();
              

                return Json(new
                {
                   ten_cong_ty= v_ac.AccountName,
                   dia_chi= v_ac.DiaChi,
                   phone_1= v_ac.Sdt01,
                   phone_2= v_ac.Sdt02,
                   phan_loai_cong_ty= v_ac.IdAccountType,
                   ids,
                   names
                }, JsonRequestBehavior.AllowGet);
            }
              
            else return Json(new
            {
                ids,
                names
            }, JsonRequestBehavior.AllowGet);

        }

        public JsonResult Update(Nullable<decimal> id,string dia_chi,string ten_cong_ty, string phone_1, string phone_2, Nullable<decimal> phan_loai_cong_ty)
        {
            if (id != null)
            {
                CrmEntities v_model = new CrmEntities();
                int affected = v_model.pr_Account_Update(id, ten_cong_ty,dia_chi,phone_1,phone_2,phan_loai_cong_ty);
                return Json(affected, JsonRequestBehavior.AllowGet);
            }
            else return Insert(ten_cong_ty, dia_chi, phone_1, phone_2, phan_loai_cong_ty);
        }
        public JsonResult Insert(string dia_chi, string ten_cong_ty, string phone_1, string phone_2, Nullable<decimal> phan_loai_cong_ty)
        {
            var id = new System.Data.Entity.Core.Objects.ObjectParameter("Id", typeof(decimal));
            CrmEntities v_model = new CrmEntities();
            v_model.pr_Account_Insert(ten_cong_ty, dia_chi, phone_1, phone_2, phan_loai_cong_ty, id);
            return Json(true, JsonRequestBehavior.AllowGet);
        }
    }
}
