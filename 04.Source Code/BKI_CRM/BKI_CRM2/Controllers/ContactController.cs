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
        [HttpGet]
        public JsonResult Select(string IdContact)
        {
            if (IdContact != null && !IdContact.Equals(""))
            {
                CrmEntities v_model = new CrmEntities();
                decimal v_id = Convert.ToDecimal(IdContact);
                var v_contact = v_model.Contact.Where(x => x.Id == v_id).First();
                string v_bday = "", v_expire = "";
                if (v_contact != null)
                {
                    if (v_contact.NgaySinh != null)
                    {
                        v_bday = ((DateTime)v_contact.NgaySinh).ToString("yyyy-MM-dd");
                    }
                    if (v_contact.HanKhachHang != null)
                    {
                        v_expire = ((DateTime)v_contact.HanKhachHang).ToString("yyyy-MM-dd");
                    }
                }
                return Json(new {
                    ho = v_contact.Ho,
                    ten = v_contact.Ten,
                    diachi = v_contact.DiaChi,
                    gioitinh = v_contact.GioiTinh,
                    image = v_contact.Image,
                    facebook = v_contact.Facebook,
                    skype = v_contact.Skype,
                    ngaysinh = v_bday,
                    sdt01 = v_contact.Sdt01,
                    sdt02 = v_contact.Sdt02,
                    masothue = v_contact.MaSoThue,
                    sotaikhoan = v_contact.SoTaiKhoan,
                    website = v_contact.Website,
                    email = v_contact.Email,
                    hankhachhang = v_expire,
                    idloaikhachhang = v_contact.IdLoaiKhachHang,
                    idtrangthaihientai = v_contact.IdTrangThaiHienTai
                }, JsonRequestBehavior.AllowGet);
            }
            else return Json(true,JsonRequestBehavior.AllowGet);
        }
    }
}
