using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using DotNetOpenAuth.AspNet;
using Microsoft.Web.WebPages.OAuth;
using WebMatrix.WebData;
using BKI_CRM.Filters;
using BKI_CRM.Models;
using BKI_CRM.Model;

namespace BKI_CRM.Controllers
{
    [Authorize]
    [InitializeSimpleMembership]
    public class AccountController : Controller
    {
        //
        // GET: /Account/Login

        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }

        //
        // POST: /Account/Login

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Login(LoginModel model)
        {
            if (kiemTraTaiKhoan(model)) {
                return RedirectToAction("Index", "Home");
            }
            return View(model);
        }

        private bool kiemTraTaiKhoan(LoginModel model)
        {
            BKI_CRMEntities v_model = new BKI_CRMEntities();
            var v_ht_user = v_model.HT_USER.Where(x => x.USERNAME.Trim() == model.UserName.Trim()).First();
            if (v_ht_user == null)
            {
                return false;
            }
            else
            {
                if (v_ht_user.PASSWORD.Trim() == model.Password.Trim() && v_ht_user.ACTIVE_YN == true)
                {
                    Session["IdUser"] = v_ht_user.ID.ToString();
                    Session["UserName"] = v_ht_user.USERNAME;
                    Session["DisplayName"] = v_ht_user.HO_TEN_NHAN_VIEN;
                    Session["IdPhongBan"] = v_ht_user.ID_PHONG_BAN.ToString();
                    Session["IdUserGroup"] = v_ht_user.ID_USER_GROUP.ToString();
                    Session["IdUserParent"] = v_ht_user.ID_USER_CAP_TREN.ToString();
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
    }
}
