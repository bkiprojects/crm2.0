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
            CrmEntities v_model = new CrmEntities();
            List<User> v_us = new List<User>();
            v_us = v_model.User.ToList<User>();
            List<decimal> iduser = new List<decimal>(); List<string> names = new List<string>();
            for (int i = 0; i < v_us.Count; i++)
            {
                iduser.Add(v_us[i].Id); names.Add(v_us[i].UserName);
            }

            List<TuDien> v_priority = new List<TuDien>();
            decimal loaitd = v_model.LoaiTuDien.Where(x => x.MaLoaiTuDien == "PriorityType").First().Id;
            v_priority = v_model.TuDien.Where(x => x.IdLoaiTuDien == loaitd).ToList<TuDien>();
            List<decimal> idpriority = new List<decimal>(); List<string> namepriority = new List<string>();
            for (int i = 0; i < v_priority.Count; i++)
            {
                idpriority.Add(v_priority[i].Id); namepriority.Add(v_priority[i].TenTuDien);
            }

            List<TuDien> v_status = new List<TuDien>();
            decimal loaitd1 = v_model.LoaiTuDien.Where(x => x.MaLoaiTuDien == "StatusType").First().Id;
            v_status = v_model.TuDien.Where(x => x.IdLoaiTuDien == loaitd1).ToList<TuDien>();
            List<decimal> idstatus = new List<decimal>(); List<string> namestatus = new List<string>();
            for (int i = 0; i < v_status.Count; i++)
            {
                idstatus.Add(v_status[i].Id); namestatus.Add(v_status[i].TenTuDien);
            }

            List<Contact> v_ct = new List<Contact>();
            v_ct = v_model.Contact.ToList<Contact>();
            List<decimal> idcontact = new List<decimal>(); List<string> namecontact = new List<string>();
            for (int i = 0; i < v_ct.Count; i++)
            {
                idcontact.Add(v_ct[i].Id); namecontact.Add(v_ct[i].Ho + " " + v_ct[i].Ten);
            }
            if (IdTask != null && !IdTask.Equals(""))
            {
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
                        do_quan_trong = v_task.IdPriority,
                        noi_dung = v_task.LamGi,
                        khach_hang = v_task.IdContact,
                        trang_thai = v_task.IdStatus,
                        nhan_vien = v_task.IdUser,
                        ten_cv = v_task.TenCongViec,
                        iduser = iduser,
                        names = names,
                        idpriority,
                        namepriority,
                        idstatus,
                        namestatus,
                        idcontact,
                        namecontact
                    }, JsonRequestBehavior.AllowGet);
                
            }
            else return Json(new
            {
                iduser = iduser,
                names = names,
                idpriority,
                namepriority,
                idstatus,
                namestatus,
                idcontact,
                namecontact
            }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Update(Nullable<decimal> id, Nullable<System.DateTime> taiNgay, Nullable<System.DateTime> duKienHoanThanh, string tenCongViec, string noiDung, Nullable<decimal> lamViecVoi, Nullable<decimal> nhanVienThucHien, Nullable<decimal> trangThaiHienTai, Nullable<decimal> doQuanTrong, Nullable<decimal> idAccount, Nullable<decimal> idLoaiAction)
        {
            if (id != null)
            {
                CrmEntities v_model = new CrmEntities();
                int affected = v_model.pr_Task_Update(id, taiNgay, nhanVienThucHien, noiDung, idAccount, lamViecVoi, doQuanTrong, trangThaiHienTai, idLoaiAction, duKienHoanThanh);
                return Json(affected, JsonRequestBehavior.AllowGet);
            }
            else return Insert(taiNgay, nhanVienThucHien, noiDung, idAccount, lamViecVoi, doQuanTrong, trangThaiHienTai, idLoaiAction, duKienHoanThanh);
        }
        public JsonResult Insert(Nullable<System.DateTime> taiNgay, Nullable<decimal> nhanVienThucHien, string noiDung, Nullable<decimal> idAccount, Nullable<decimal> lamViecVoi, Nullable<decimal> doQuanTrong, Nullable<decimal> trangThaiHienTai, Nullable<decimal> idLoaiAction, Nullable<System.DateTime> duKienHoanThanh)
        {
            var id = new System.Data.Entity.Core.Objects.ObjectParameter("Id", typeof(decimal));
            CrmEntities v_model = new CrmEntities();
            v_model.pr_Task_Insert(taiNgay, nhanVienThucHien, noiDung, idAccount, lamViecVoi, doQuanTrong, trangThaiHienTai, idLoaiAction, duKienHoanThanh, id);
            return Json(true, JsonRequestBehavior.AllowGet);
        }

    }
}
