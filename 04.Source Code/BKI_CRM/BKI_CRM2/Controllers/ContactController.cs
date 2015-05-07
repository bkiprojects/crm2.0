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
    public class ContactController : Controller
    {
        //
        // GET: /Contact/

        public ActionResult Index()
        {
            CrmEntities v_model = new CrmEntities();
            List<List<Contact>> v_dm_kh = new List<List<Contact>>();
            List<TuDien> v_tu_dien = new List<TuDien>();
            List<Account> v_account = new List<Account>();
            List<ContactState> state = v_model.ContactState.OrderBy(x => x.Order).ToList<ContactState>();
            for (int i = 0; i < state.Count; i++)
            {
                decimal temp = state[i].Id;
                v_dm_kh.Add(v_model.Contact.Where(x => x.IdTrangThaiHienTai == temp).ToList<Contact>());
            }
            decimal loaitd = -1; var temp2 = v_model.LoaiTuDien.FirstOrDefault(x => x.MaLoaiTuDien == "ContactType");
            if (temp2 != null) loaitd = temp2.Id;
            v_tu_dien = v_model.TuDien.Where(x => x.IdLoaiTuDien == loaitd).ToList<TuDien>();
            ViewBag.v_dm_kh = v_dm_kh;
            ViewBag.v_tu_dien = v_tu_dien;
            ViewBag.state = state;
            v_account = v_model.Account.ToList<Account>();
            ViewBag.v_account = v_account;
            return PartialView();
        }
        [HttpGet]
        public JsonResult Select(string IdContact)
        {
            if (IdContact != null && !IdContact.Equals(""))
            {
                CrmEntities v_model = new CrmEntities();
                decimal v_id = Convert.ToDecimal(IdContact), v_ac = -1;
                var v_contact = v_model.Contact.FirstOrDefault(x => x.Id == v_id);
                var v_acrole = v_model.AccountContactRole.FirstOrDefault(x => x.IdContact == v_id);
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
                if (v_acrole != null)
                {
                    v_ac = (decimal)v_acrole.IdAccount;
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
                    idtrangthaihientai = v_contact.IdTrangThaiHienTai,
                    idaccount = v_ac
                }, JsonRequestBehavior.AllowGet);
            }
            else return Json(true,JsonRequestBehavior.AllowGet);
        }
        public ActionResult Delete(decimal id_kh) {
            CrmEntities v_model = new CrmEntities();
            int affected = v_model.pr_Contact_Delete(id_kh);
            return Json(affected, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Update(Nullable<decimal> id, string hoten, string diaChi, Nullable<bool> gioiTinh, string image, string facebook, string skype, Nullable<System.DateTime> ngaySinh, string sdt01, string sdt02, string maSoThue, string soTaiKhoan, string website, string email, Nullable<System.DateTime> hanKhachHang, Nullable<decimal> idLoaiKhachHang, Nullable<decimal> idTrangThaiHienTai) {
            string ho = "", ten = "";
            if (!string.IsNullOrEmpty(hoten))
            {
                string[] temp = hoten.Split(new char[]{' '},System.StringSplitOptions.RemoveEmptyEntries);
                ten = temp[temp.Length - 1];
                for (int i = 0; i < temp.Length - 1; i++) ho += temp[i] + " ";
            }
            if (id != null)
            {
                CrmEntities v_model = new CrmEntities();
                idTrangThaiHienTai = v_model.Contact.FirstOrDefault(x => x.Id == id).IdTrangThaiHienTai;
                int affected = v_model.pr_Contact_Update(id, ho, ten, diaChi, gioiTinh, image, facebook, skype, ngaySinh, sdt01, sdt02, maSoThue, soTaiKhoan, website, email, hanKhachHang, idLoaiKhachHang, idTrangThaiHienTai);
                return Json(affected, JsonRequestBehavior.AllowGet);
            }
            else return Insert(ho, ten, diaChi, gioiTinh, image, facebook, skype, ngaySinh, sdt01, sdt02, maSoThue, soTaiKhoan, website, email, hanKhachHang, idLoaiKhachHang, idTrangThaiHienTai);
        }

        public ActionResult Insert(string ho, string ten, string diaChi, Nullable<bool> gioiTinh, string image, string facebook, string skype, Nullable<System.DateTime> ngaySinh, string sdt01, string sdt02, string maSoThue, string soTaiKhoan, string website, string email, Nullable<System.DateTime> hanKhachHang, Nullable<decimal> idLoaiKhachHang, Nullable<decimal> idTrangThaiHienTai){
            var id = new System.Data.Entity.Core.Objects.ObjectParameter("Id", typeof(decimal));
            CrmEntities v_model = new CrmEntities();
            idTrangThaiHienTai = v_model.ContactState.OrderBy(x => x.Order).First().Id;
            v_model.pr_Contact_Insert(ho, ten, diaChi, gioiTinh, image, facebook, skype, ngaySinh, sdt01, sdt02, maSoThue, soTaiKhoan, website, email, hanKhachHang, idLoaiKhachHang, idTrangThaiHienTai, id);
            return Json(true, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetState(decimal IdContact)
        {
            CrmEntities v_model = new CrmEntities();
            decimal cstate = (decimal)v_model.Contact.FirstOrDefault(x => x.Id == IdContact).IdTrangThaiHienTai;
            var astate = v_model.ContactStateProcess.Where(x => x.IdTrangThaiTruoc == cstate && x.IdTrangThaiSau != null).ToList<ContactStateProcess>();
            string state = v_model.ContactState.FirstOrDefault(x => x.Id == cstate).TenTrangThai;
            List<decimal> states = new List<decimal>();
            List<string> states_name = new List<string>();
            for (int i = 0; i < astate.Count; i++)
            {
                decimal temp = (decimal)astate[i].IdTrangThaiSau;
                states.Add(temp);
                states_name.Add(v_model.ContactState.FirstOrDefault(x => x.Id == temp).TenTrangThai);
            }
            return Json(new { state = state, states = states, states_name = states_name }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult SwitchState(decimal IdContact, decimal IdTrangThai)
        {
            try
            {
                CrmEntities v_model = new CrmEntities();
                v_model.Database.ExecuteSqlCommand("update [Contact] set [IdTrangThaiHienTai]="+IdTrangThai+" where [Id]="+IdContact);
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
        }
    }
}
