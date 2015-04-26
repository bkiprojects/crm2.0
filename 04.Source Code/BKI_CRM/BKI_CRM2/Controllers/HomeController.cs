using System;
using System.Collections.Generic;
using System.Data.Objects;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Objects;
using System.Web.Script.Serialization;
using BKI_CRM2.Models;
using System.IO;
using OpenXMLExcel.SLExcelUtility;
using BKI_CRM2.Controllers;
using System.Net.Mail;
using System.Text;


namespace BKI_CRM2.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            CrmEntities v_model = new CrmEntities();
            List<Contact> v_dm_kh = new List<Contact>();
            List<TuDien> TuDien = new List<TuDien>();
            v_dm_kh = v_model.Contact.ToList<Contact>();
            TuDien = v_model.TuDien.Where(x => x.LoaiTuDien.TenLoaiTuDien == "Loại khách hàng").ToList<TuDien>();
            ViewBag.v_dm_kh = v_dm_kh;
            ViewBag.TuDien = TuDien;
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
