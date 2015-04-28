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
    public class ContactController : Controller
    {
        //
        // GET: /Contact/

        public ActionResult Index()
        {
            CrmEntities v_model = new CrmEntities();
            List<List<Contact>> v_dm_kh = new List<List<Contact>>();
            List<TuDien> TuDien = new List<TuDien>();
            List<ContactState> state = v_model.ContactState.ToList<ContactState>();
            for (int i = 0; i < state.Count; i++)
            {
                decimal temp = state[i].Id;
                v_dm_kh.Add(v_model.Contact.Where(x => x.IdTrangThaiHienTai == temp).ToList<Contact>());
            }
            TuDien = v_model.TuDien.Where(x => x.LoaiTuDien.TenLoaiTuDien == "Loại khách hàng").ToList<TuDien>();
            ViewBag.v_dm_kh = v_dm_kh;
            ViewBag.TuDien = TuDien;
            ViewBag.state = state;
            return PartialView();
        }
        public ActionResult Select(string IdContact)
        {
            if (IdContact != null)
            {
                CrmEntities v_model = new CrmEntities();
                decimal v_id = Convert.ToDecimal(IdContact);
                var v_contact = v_model.Contact.Where(x => x.Id == v_id).First();
                string v_bday = "", v_expire = "";
                if (v_contact != null)
                {
                    if (v_contact.NgaySinh != null)
                    {
                        v_bday = v_contact.NgaySinh.ToString();
                        v_bday = v_bday.Substring(6, 4) + "-" + v_bday.Substring(0, 2) + "-" + v_bday.Substring(3, 2);
                    }
                    if (v_contact.HanKhachHang != null)
                    {
                        v_expire = v_contact.HanKhachHang.ToString();
                        v_expire = v_expire.Substring(6, 4) + "-" + v_expire.Substring(0, 2) + "-" + v_expire.Substring(3, 2);
                    }
                }
                return Json(new { data = v_contact, bday = v_bday, hankh = v_expire }, JsonRequestBehavior.AllowGet);
            }
            else return Json(new { }, JsonRequestBehavior.AllowGet);
        }
    }
}
