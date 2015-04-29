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
    public class ContractController : Controller
    {
        //
        // GET: /Contract/

        public ActionResult Index()
        {
            CrmEntities v_model = new CrmEntities();
            List<Contract> v_hd = new List<Contract>();
            v_hd = v_model.Contract.Where(x=>x.Id>0).ToList<Contract>();
            ViewBag.v_hd = v_hd;
            return PartialView();
        }

        public ActionResult Delete(decimal id_kh)
        {
            //decimal id = Convert.ToDecimal(id_kh);
            CrmEntities v_model = new CrmEntities();
            int affected = v_model.pr_Contact_Delete(id_kh);
            v_model.SaveChanges();
            return Json(affected, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult Select(string IdContract)
        {
            if (IdContract != null && !IdContract.Equals(""))
            {
                CrmEntities v_model = new CrmEntities();
                decimal v_id = Convert.ToDecimal(IdContract);
                var v_contract = v_model.Contract.Where(x => x.Id == v_id).First();
                //string v_ngay_bat_dau = "", v_ngay_ket_thuc = "";
                //if (v_contract != null)
                //{
                //    if (v_contract.NgayBatDau != null)
                //    {
                //        v_ngay_bat_dau = ((DateTime)v_contract.NgayBatDau).ToString("yyyy-MM-dd");
                //    }
                //    if (v_contract.NgayKetThuc != null)
                //    {
                //        v_ngay_ket_thuc = ((DateTime)v_contract.NgayKetThuc).ToString("yyyy-MM-dd");
                //    }
                //}
                return Json(new { 
                    ngay_bat_dau= v_contract.NgayBatDau,
                    ngay_ket_thuc=v_contract.NgayKetThuc,
                    so_hop_dong= v_contract.SoHopDong,
                    noi_dung= v_contract.NoiDung  
                }, JsonRequestBehavior.AllowGet);
            }
            else return Json(true, JsonRequestBehavior.AllowGet);
        }
        //public ActionResult Delete(decimal id_kh)
        //{
        //    CrmEntities v_model = new CrmEntities();
        //    int affected = v_model.pr_Contact_Delete(id_kh);
        //    return Json(affected, JsonRequestBehavior.AllowGet);
        //}
    }
}
