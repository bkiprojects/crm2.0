﻿using System;
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
            v_hd = v_model.Contract.Where(x => x.Id > 0).ToList<Contract>();
            ViewBag.v_hd = v_hd;
            return PartialView();
        }

        public ActionResult Delete(decimal id_kh)
        {
            CrmEntities v_model = new CrmEntities();
            int affected = v_model.pr_Contract_Delete(id_kh);
            return Json(affected, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult Select(string IdContract)
        {
            CrmEntities v_model = new CrmEntities();
            List<Account> v_ac = new List<Account>();
            v_ac = v_model.Account.ToList<Account>();
            List<decimal> ids = new List<decimal>(); List<string> names = new List<string>();
            for (int i = 0; i < v_ac.Count; i++)
            {
                ids.Add(v_ac[i].Id); names.Add(v_ac[i].AccountName);
            }
            if (IdContract != null && !IdContract.Equals(""))
            {
                decimal v_id = Convert.ToDecimal(IdContract);
                var v_contract = v_model.Contract.Where(x => x.Id == v_id).First();
                string v_ngay_bat_dau = "", v_ngay_ket_thuc = "";
                if (v_contract != null)
                {
                    if (v_contract.NgayBatDau != null)
                    {
                        v_ngay_bat_dau = ((DateTime)v_contract.NgayBatDau).ToString("yyyy-MM-dd");
                    }
                    if (v_contract.NgayKetThuc != null)
                    {
                        v_ngay_ket_thuc = ((DateTime)v_contract.NgayKetThuc).ToString("yyyy-MM-dd");
                    }
                }
                return Json(new
                {
                    ngay_bat_dau = v_ngay_bat_dau,
                    ngay_ket_thuc = v_ngay_ket_thuc,
                    so_hop_dong = v_contract.SoHopDong,
                    noi_dung = v_contract.NoiDung,
                    account = v_contract.IdAccount,
                    ids = ids,
                    names = names
                }, JsonRequestBehavior.AllowGet);
            }
            else return Json(new { ids = ids, names = names }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult Update(Nullable<decimal> id, Nullable<System.DateTime> ngayBatDau, Nullable<System.DateTime> ngayKetThuc, string soHopDong, string noiDung, Nullable<decimal> idAccount, Nullable<decimal> idLoaiContract, Nullable<decimal> idUser)
        {
            if (id != null)
            {
                CrmEntities v_model = new CrmEntities();
                int affected = v_model.pr_Contract_Update(id, ngayBatDau, ngayKetThuc, soHopDong, noiDung, idAccount, idLoaiContract, idUser);
                return Json(affected, JsonRequestBehavior.AllowGet);
            }
            else return Insert(ngayBatDau, ngayKetThuc, soHopDong, noiDung, idAccount, idLoaiContract, idUser);
        }
        public JsonResult Insert(Nullable<System.DateTime> ngayBatDau, Nullable<System.DateTime> ngayKetThuc, string soHopDong, string noiDung, Nullable<decimal> idAccount, Nullable<decimal> idLoaiContract, Nullable<decimal> idUser)
        {
            var id = new System.Data.Entity.Core.Objects.ObjectParameter("Id", typeof(decimal));
            CrmEntities v_model = new CrmEntities();
            v_model.pr_Contract_Insert(ngayBatDau, ngayKetThuc, soHopDong, noiDung, idAccount, idLoaiContract, idUser, id);
            return Json(true, JsonRequestBehavior.AllowGet);
        }
    }
}
