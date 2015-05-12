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
    public class UserController : Controller
    {
        //
        // GET: /User/

        public ActionResult Index()
        {
            CrmEntities v_model = new CrmEntities();
            List<List<User>> v_user = new List<List<User>>();
            List<UserGroup> state = v_model.UserGroups.OrderBy(x => x.Order).ToList<UserGroup>();
            for (int i = 0; i < state.Count; i++)
            {
                decimal temp = state[i].Id;
                v_user.Add(v_model.User.Where(x => x.IdUserGroup == temp).ToList<User>());
            }
            List<User> v_us = new List<User>();
            v_us = v_model.User.ToList<User>();
            List<decimal> idus = new List<decimal>(); 
            for (int i = 0; i < v_us.Count; i++)
            {
                idus.Add(v_us[i].Id); 
            }
            ViewBag.v_user = v_user;
            ViewBag.idus = idus;
            ViewBag.state = state;
            //v_account = v_model.Account.ToList<Account>();
            //ViewBag.v_account = v_account;
            return PartialView();
        }

        [HttpGet]
        public JsonResult Select(string IdUser)
        {
            CrmEntities v_model = new CrmEntities();
              List<User> v_us = new List<User>();
                v_us = v_model.User.ToList<User>();
                List<decimal> idus = new List<decimal>(); List<string> nameus = new List<string>();
                for (int i = 0; i < v_us.Count; i++)
                {
                    idus.Add(v_us[i].Id); nameus.Add(v_us[i].HoNhanVien + " " + v_us[i].TenNhanVien);
                }
            if (!String.IsNullOrEmpty(IdUser))
            {
               
                decimal? v_id = Convert.ToDecimal(IdUser);
                var v_user = v_model.User.FirstOrDefault(x => x.Id == v_id);
                var v_id_nhan_vien_cap_tren = v_user.IdParentUser;
                var v_nhan_vien_cap_tren = v_model.User.FirstOrDefault(x => x.Id == v_id_nhan_vien_cap_tren);
                string nhanviencaptren = "";
                if (v_nhan_vien_cap_tren != null)
                {
                    nhanviencaptren = v_nhan_vien_cap_tren.HoNhanVien + " " + v_nhan_vien_cap_tren.TenNhanVien;
                }
              
               
                //List<UserContactRole> v_us_ct = new List<UserContactRole>();
                //v_us_ct = v_model.UserContactRole.ToList<UserContactRole>();
                //List<decimal?> idus_ct = new List<decimal?>();
                //List<string> nameus_ct = new List<string>();
                //for (int i = 0; i < v_us_ct.Count; i++)
                //{
                //    idus_ct.Add(v_us_ct[i].IdContact);
                //    var v_ct = v_model.Contact.Where(x=>x.Id==v_us_ct[i].IdContact).FirstOrDefault();
                //    nameus_ct.Add(v_ct.Ho+" "+ v_ct.Ten);
                //}
             
               
                return Json(new
                {
                    hodem = v_user.HoNhanVien,
                    ten = v_user.TenNhanVien,
                    image = v_user.Image,
                   username= v_user.UserName,
                   password= v_user.Password,
                   nhanviencaptren= v_user.IdParentUser,
                    sdt01 = v_user.Sdt01,
                    sdt02 = v_user.Sdt02,
                    email = v_user.Email,
                    idus= idus,
                    nameus=nameus
                    //idus_ct= idus_ct,
                    //nameus_ct= nameus_ct
                   
                   
                }, JsonRequestBehavior.AllowGet);
            }
            else return Json(new { idus=idus, nameus= nameus}, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult SelectListContact(string IdUser)
        {
            if (!String.IsNullOrEmpty(IdUser))
            {
                CrmEntities v_model = new CrmEntities();
                decimal? v_id = Convert.ToDecimal(IdUser);
                List<UserContactRole> v_us_ct = new List<UserContactRole>();
                v_us_ct = v_model.UserContactRole.Where(x=>x.IdUser==v_id).ToList<UserContactRole>();
                List<decimal?> idus_ct = new List<decimal?>();
                List<string> nameus_ct = new List<string>();
                if (v_us_ct.Count > 0)
                {
                   
                    for (int i = 0; i < v_us_ct.Count; i++)
                    {
                         var index=v_us_ct[i].IdContact;
                        idus_ct.Add(index);

                        var v_ct = v_model.Contact.Where(x => x.Id == index).First();
                        nameus_ct.Add(v_ct.Ho + " " + v_ct.Ten);
                    }
                }

                return Json(new
                {
                    idus_ct= idus_ct,
                    nameus_ct= nameus_ct
                }, JsonRequestBehavior.AllowGet);
            }
            else return Json(true, JsonRequestBehavior.AllowGet);
        }


        public string GetFileRequest()
        {
            HttpPostedFile pic = null; decimal IdUser = -1;
            if (System.Web.HttpContext.Current.Request.Files.AllKeys.Any())
            {
                pic = System.Web.HttpContext.Current.Request.Files["HelpSectionImages"];
            }
            return UploadImg(pic, IdUser);
        }
        public string UploadImg(HttpPostedFile file, decimal IdUser)
        {
            string[] name = System.IO.Path.GetFileName(file.FileName).Split('.');
            string pic = IdUser + "." + name[name.Length - 1];
            string path = System.IO.Path.Combine(Server.MapPath("~/Images/profile"), pic);
            file.SaveAs(path);
            return "../Images/profile/" + pic + "%" + path;
        }

    }
}
