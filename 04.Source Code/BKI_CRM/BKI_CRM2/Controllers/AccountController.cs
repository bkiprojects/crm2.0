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
using BKI_CRM2.Filters;
using BKI_CRM2.Models;

namespace BKI_CRM2.Controllers
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
            if (kiemTraTaiKhoan(model))
            {
                return RedirectToAction("Index", "Home");
            }
            return View(model);
        }

        private bool kiemTraTaiKhoan(LoginModel model)
        {
            CrmEntities v_model = new CrmEntities();
            var v_ht_user = v_model.User.Where(x => x.UserName.Trim() == model.UserName.Trim()).First();
            if (v_ht_user == null)
            {
                return false;
            }
            else
            {
                if (v_ht_user.Password.Trim() == model.Password.Trim() && v_ht_user.IsActive == true)
                {
                    Session["IdUser"] = v_ht_user.Id.ToString();
                    Session["UserName"] = v_ht_user.UserName;
                    Session["DisplayName"] = v_ht_user.HoNhanVien + " " + v_ht_user.TenNhanVien;
                    Session["IdCompany"] = v_ht_user.IdCompany.ToString();
                    Session["IdUserGroup"] = v_ht_user.IdUserGroup.ToString();
                    Session["IdParentUser"] = v_ht_user.IdParentUser.ToString();
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
