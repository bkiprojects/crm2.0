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
            List<V_CONTRACT> v_hd = new List<V_CONTRACT>();
            List<Account> v_ac = new List<Account>();
           
            v_hd = v_model.V_CONTRACT.Where(x => x.Id > 0 && x.IsDeleted==false).ToList<V_CONTRACT>();
            v_ac = v_model.Account.ToList<Account>();
         
            ViewBag.v_hd = v_hd;
            ViewBag.v_ac = v_ac;
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
            decimal? v_contact_chinh = null ;
            CrmEntities v_model = new CrmEntities();
            List<Account> v_ac = new List<Account>();
            List<Contact> v_contact = new List<Contact>();
            List<ContractContactRole> v_ct = new List<ContractContactRole>();
            v_ac = v_model.Account.ToList<Account>();
            List<decimal> ids = new List<decimal>(); List<string> names = new List<string>();
            for (int i = 0; i < v_ac.Count; i++)
            {
                ids.Add(v_ac[i].Id); names.Add(v_ac[i].AccountName);
            }

            v_contact = v_model.Contact.ToList<Contact>();
            v_ct = v_model.ContractContactRole.Where(x => x.Id > 0).ToList<ContractContactRole>();
            List<decimal?> idct = new List<decimal?>(); List<string> namect = new List<string>();
            for (int i = 0; i < v_contact.Count; i++)
            {
                decimal? index = v_contact[i].Id;
                idct.Add(index);

                namect.Add(v_contact[i].Ho + " " + v_contact[i].Ten);
            }
            List<decimal?> idct_chon = new List<decimal?>();
            for (int i = 0; i < v_ct.Count; i++)
            {
                decimal? index = v_ct[i].IdContact;
                idct_chon.Add(index);
                if ((bool)v_ct[i].IsPrimary)
                {
                    v_contact_chinh = index;
                }
            }

           

            if (IdContract != null && !IdContract.Equals(""))
            {
                decimal v_id = Convert.ToDecimal(IdContract);
                var v_contract = v_model.V_CONTRACT.FirstOrDefault(x => x.Id == v_id);
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
                    ho= v_contract.Ho,
                    ten= v_contract.Ten,
                    ids = ids,
                    names = names,
                    idct= idct,
                    namect= namect,
                    idct_chon= idct_chon,
                    v_contact_chinh= v_contact_chinh

                }, JsonRequestBehavior.AllowGet);
            }
            else return Json(new { ids = ids, names = names, idct = idct, namect = namect, idct_chon = idct_chon, v_contact_chinh = v_contact_chinh }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult Update(Nullable<decimal> id, Nullable<System.DateTime> ngayBatDau, Nullable<System.DateTime> ngayKetThuc, string soHopDong, string noiDung, Nullable<decimal> idAccount, Nullable<decimal> idLoaiContract, Nullable<decimal> idUser, string idContact, decimal? idContactChinh)
        {
            decimal[] v_id = new decimal[0];
            if (idContact != null)
            {
                string[] IdContact = idContact.Split(',');
                 v_id = new decimal[IdContact.Length];
                for (int i = 0; i < v_id.Length; i++)
                {
                    v_id[i] = Convert.ToDecimal(IdContact[i]);
                }
            }
           
              
            
            if (id != null)
            {
                CrmEntities v_model = new CrmEntities();
                for (int i = 0; i < v_id.Length; i++)
                {
                    v_model.pr_Contract_Update(id, ngayBatDau, ngayKetThuc, soHopDong, noiDung, idAccount, idLoaiContract, idUser, v_id[i], v_id[i] == idContactChinh);
                }
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            else return Insert(ngayBatDau, ngayKetThuc, soHopDong, noiDung, idAccount, idLoaiContract, idUser, v_id, idContactChinh);
        }
        public JsonResult Insert(Nullable<System.DateTime> ngayBatDau, Nullable<System.DateTime> ngayKetThuc, string soHopDong, string noiDung, Nullable<decimal> idAccount, Nullable<decimal> idLoaiContract, Nullable<decimal> idUser, decimal[] idContact, decimal? idContactChinh)
        {
            var id = new System.Data.Entity.Core.Objects.ObjectParameter("Id", typeof(decimal));
            var id1 = new System.Data.Entity.Core.Objects.ObjectParameter("Id", typeof(decimal));
            CrmEntities v_model = new CrmEntities();
            decimal v_index= v_model.pr_Contract_Insert(ngayBatDau, ngayKetThuc, soHopDong, noiDung, idAccount, idLoaiContract, idUser, id);

            for (int i = 0; i < idContact.Length; i++)
            {
                v_model.pr_ContractContactRole_Insert(idContact[i],v_index,false,(idContact[i]==idContactChinh),id1);
            }
            
            return Json(true, JsonRequestBehavior.AllowGet);
        }
    }
}
