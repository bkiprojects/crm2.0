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
    public class AccountsController : Controller
    {
        //
        // GET: /Accounts/

        public ActionResult Index()
        {
            CrmEntities v_model = new CrmEntities();
            List<Account> v_ac = new List<Account>();
            List<TuDien> v_td = new List<TuDien>();
            List<LoaiTuDien> v_ltd = new List<LoaiTuDien>();
            decimal id = 0;
            v_ac = v_model.Account.ToList<Account>();
            v_ltd = v_model.LoaiTuDien.Where(x=>x.TenLoaiTuDien=="CompanyType").ToList<LoaiTuDien>();
            if(v_ltd.Count>0)id=v_ltd.First().Id;
            v_td = v_model.TuDien.Where(x => x.IdLoaiTuDien == id).ToList<TuDien>();
            ViewBag.v_ac = v_ac; ViewBag.v_td = v_td;
            return PartialView();
        }

    }
}
