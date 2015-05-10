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
            //decimal loaitd = -1; var temp2 = v_model.LoaiTuDien.FirstOrDefault(x => x.MaLoaiTuDien == "ContactType");
            //if (temp2 != null) loaitd = temp2.Id;
            //v_tu_dien = v_model.TuDien.Where(x => x.IdLoaiTuDien == loaitd).ToList<TuDien>();
            ViewBag.v_user = v_user;
            //ViewBag.v_tu_dien = v_tu_dien;
            ViewBag.state = state;
            //v_account = v_model.Account.ToList<Account>();
            //ViewBag.v_account = v_account;
            return PartialView();
        }

    }
}
