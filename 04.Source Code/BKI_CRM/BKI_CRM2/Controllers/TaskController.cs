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
    public class TaskController : Controller
    {
        //
        // GET: /Task/

        public ActionResult Index()
        {
            CrmEntities v_model = new CrmEntities();
            List<V_TASK> v_cv = new List<V_TASK>();
            List<TuDien> v_tu_dien = new List<TuDien>();
            List<TuDien> v_tu_dien_status = new List<TuDien>();
            decimal loaitd = v_model.LoaiTuDien.Where(x => x.MaLoaiTuDien == "PriorityType").First().Id;
            decimal loaitdStatus = v_model.LoaiTuDien.Where(x => x.MaLoaiTuDien == "StatusType").First().Id;
            v_tu_dien = v_model.TuDien.Where(x => x.IdLoaiTuDien == loaitd).ToList<TuDien>();
            v_tu_dien_status = v_model.TuDien.Where(x => x.IdLoaiTuDien == loaitdStatus).ToList<TuDien>();
            v_cv = v_model.V_TASK.Where(x => x.Id > 0).ToList<V_TASK>();
            ViewBag.v_cv = v_cv;
            ViewBag.v_tu_dien = v_tu_dien;
            ViewBag.v_tu_dien_status = v_tu_dien_status;
            return PartialView();
        }

    }
}
