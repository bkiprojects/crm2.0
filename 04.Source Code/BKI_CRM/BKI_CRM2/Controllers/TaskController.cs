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
            List<Contact> v_contact = new List<Contact>();
            List<User> v_user = new List<User>();
            decimal loaitd = v_model.LoaiTuDien.Where(x => x.MaLoaiTuDien == "PriorityType").First().Id;
            decimal loaitdStatus = v_model.LoaiTuDien.Where(x => x.MaLoaiTuDien == "StatusType").First().Id;
            v_tu_dien = v_model.TuDien.Where(x => x.IdLoaiTuDien == loaitd).ToList<TuDien>();
            v_tu_dien_status = v_model.TuDien.Where(x => x.IdLoaiTuDien == loaitdStatus).ToList<TuDien>();
            v_cv = v_model.V_TASK.Where(x => x.Id > 0).ToList<V_TASK>();
            v_contact = v_model.Contact.Where(x => x.Id > 0).ToList<Contact>();
            v_user = v_model.User.Where(x => x.Id > 0).ToList<User>();
            ViewBag.v_cv = v_cv;
            ViewBag.v_tu_dien = v_tu_dien;
            ViewBag.v_tu_dien_status = v_tu_dien_status;
            ViewBag.v_contact = v_contact;
            ViewBag.v_user = v_user;
            return PartialView();
        }

        public ActionResult Delete(decimal id_cv)
        {
            CrmEntities v_model = new CrmEntities();
            int affected = v_model.pr_Task_Delete(id_cv);
            return Json(affected, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult Select(string IdTask)
        {
            if (IdTask != null && !IdTask.Equals(""))
            {
                CrmEntities v_model = new CrmEntities();
                decimal v_id = Convert.ToDecimal(IdTask);
                var v_task = v_model.V_TASK.Where(x => x.Id == v_id).First();
                string v_ngay_bat_dau = "", v_ngay_ket_thuc = "";
                if (v_task != null)
                {
                    if (v_task.TaiNgay != null)
                    {
                        v_ngay_bat_dau = ((DateTime)v_task.TaiNgay).ToString("yyyy-MM-dd");
                    }
                    if (v_task.DuKienHoanThanh != null)
                    {
                        v_ngay_ket_thuc = ((DateTime)v_task.DuKienHoanThanh).ToString("yyyy-MM-dd");
                    }
                }
               
                return Json(new
                {
                    tai_ngay = v_ngay_bat_dau,
                    du_kien_hoan_thanh = v_ngay_ket_thuc,
                   // do_quan_trong= v_task.IdPriority,
                    noi_dung = v_task.LamGi,
                    khach_hang= v_task.IdContact,
                    trang_thai= v_task.IdStatus,
                    nhan_vien= v_task.IdUser,
                    ten_cv= v_task.TenCongViec,
                }, JsonRequestBehavior.AllowGet);
            }
            else return Json(true, JsonRequestBehavior.AllowGet);
        }

    }
}
