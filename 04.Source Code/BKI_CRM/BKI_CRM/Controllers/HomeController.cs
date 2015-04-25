using System;
using System.Collections.Generic;
using System.Data.Objects;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using BKI_CRM.Models;
using BKI_CRM.Model;
using Framework.Extensions;
using System.IO;
using OpenXMLExcel.SLExcelUtility;
using BKI_CRM.Controllers;
using System.Net.Mail;
using System.Text;

namespace BKI_CRM.Controllers
{
    public class HomeController : Controller
    {
        PhanQuyenHeThong m_p;
        public ActionResult Index()
        {
            BKI_CRMEntities v_model = new BKI_CRMEntities();
            string v_id_user = Session["IdUser"].ToString();
            List<V_GD_KHACH_HANG_CHUYEN_TRANG_THAI> v_lst_khach_hang = v_model.V_GD_KHACH_HANG_CHUYEN_TRANG_THAI.Where(x => x.ID_NGUOI_SU_DUNG == new Guid(v_id_user)).ToList();
            List<KhachHangModel> v_lst_khach_hang_model = new List<KhachHangModel>();
            foreach (var item in v_lst_khach_hang)
            {
                KhachHangModel v_khach_hang_model = item.CopyAs<KhachHangModel>();
                v_khach_hang_model.LstChuyenTrangThai = v_model.V_GD_CHUYEN_TRANG_THAI.Where(x => x.ID_TRANG_THAI_BAN_DAU == item.ID_TRANG_THAI).ToList();
                v_khach_hang_model.ID_KHACH_HANG = v_model.DM_KHACH_HANG.Where(x => x.ID == v_khach_hang_model.ID_KHACH_HANG).First().ID;
                v_khach_hang_model.ID_CONG_TY = (v_model.DM_KHACH_HANG.Where(x => x.ID == v_khach_hang_model.ID_KHACH_HANG).First().ID_CONG_TY);
                v_khach_hang_model.TEN_KHACH_HANG = v_model.DM_KHACH_HANG.Where(x => x.ID == v_khach_hang_model.ID_KHACH_HANG).First().TEN_KHACH_HANG;
                v_lst_khach_hang_model.Add(v_khach_hang_model);
            }
            v_lst_khach_hang_model = v_lst_khach_hang_model.OrderByDescending(x => x.MA_TRANG_THAI).ToList();
            ViewBag.khach_hang = v_lst_khach_hang_model;
            LayDanhSach_TrangThai_Quyen();
            return View();
        }
        public ActionResult Index2()
        {
            BKI_CRMEntities v_model = new BKI_CRMEntities();
            string v_id_user = Session["IdUser"].ToString();
            List<V_GD_KHACH_HANG_CHUYEN_TRANG_THAI> v_lst_khach_hang = v_model.V_GD_KHACH_HANG_CHUYEN_TRANG_THAI.Where(x => x.ID_NGUOI_SU_DUNG == new Guid(v_id_user)).ToList();
            List<KhachHangModel> v_lst_khach_hang_model = new List<KhachHangModel>();
            foreach (var item in v_lst_khach_hang)
            {
                KhachHangModel v_khach_hang_model = item.CopyAs<KhachHangModel>();
                v_khach_hang_model.LstChuyenTrangThai = v_model.V_GD_CHUYEN_TRANG_THAI.Where(x => x.ID_TRANG_THAI_BAN_DAU == item.ID_TRANG_THAI).ToList();
                v_khach_hang_model.ID_KHACH_HANG = v_model.DM_KHACH_HANG.Where(x => x.ID == v_khach_hang_model.ID_KHACH_HANG).First().ID;
                v_khach_hang_model.ID_CONG_TY = v_model.DM_KHACH_HANG.Where(x => x.ID == v_khach_hang_model.ID_KHACH_HANG).First().ID_CONG_TY;
                v_khach_hang_model.TEN_KHACH_HANG = v_model.DM_KHACH_HANG.Where(x => x.ID == v_khach_hang_model.ID_KHACH_HANG).First().TEN_KHACH_HANG;
                v_lst_khach_hang_model.Add(v_khach_hang_model);
            }
            v_lst_khach_hang_model = v_lst_khach_hang_model.OrderByDescending(x => x.MA_TRANG_THAI).ToList();
            ViewBag.khach_hang = v_lst_khach_hang_model;
            LayDanhSach_TrangThai_Quyen();
            return PartialView();
        }
        public ActionResult ThemKhachHang()
        {
            ViewBag.LstKhachHang = layDanhSachKhachHang();
            layDanhSachNguoiQuanLy();
            layDanhMucLoaiKhachHang();
            layDanhMucSanPham();
            layDanhMucTrangThai();
            return PartialView();
        }

        private List<DM_KHACH_HANG> layDanhSachKhachHang()
        {
            BKI_CRMEntities v_model = new BKI_CRMEntities();
            List<DM_KHACH_HANG> v_lst_dm_khach_hang = new List<DM_KHACH_HANG>();
            v_lst_dm_khach_hang = v_model.DM_KHACH_HANG.Where(x => x.ID_CONG_TY == new Guid("c2a1b448-d15b-4877-a373-2f7ee30c665e")).OrderBy(x => x.ID_LOAI_KHACH_HANG).ToList();
            return v_lst_dm_khach_hang;
        }

        public ActionResult CustommerInsert()
        {
            layDanhMucLoaiKhachHang();
            return PartialView();
        }

        private void layDanhMucTrangThai()
        {
            BKI_CRMEntities v_model = new BKI_CRMEntities();
            List<DM_TRANG_THAI> v_lst_dm_trang_thai = new List<DM_TRANG_THAI>();
            v_lst_dm_trang_thai = v_model.DM_TRANG_THAI.Where(x => x.ID_CONG_TY == new Guid("c2a1b448-d15b-4877-a373-2f7ee30c665e")).OrderBy(x => x.MA_TRANG_THAI).ToList();
            ViewBag.LstTrangThai = v_lst_dm_trang_thai;
        }

        private void layDanhMucSanPham()
        {
            BKI_CRMEntities v_model = new BKI_CRMEntities();
            List<DM_SAN_PHAM> v_lst_dm_san_pham = new List<DM_SAN_PHAM>();
            v_lst_dm_san_pham = v_model.DM_SAN_PHAM.Where(x => x.DM_NHOM_SAN_PHAM.ID_CONG_TY == new Guid("c2a1b448-d15b-4877-a373-2f7ee30c665e")).ToList();
            ViewBag.LstSanPham = v_lst_dm_san_pham;
        }

        private void layDanhMucLoaiKhachHang()
        {
            BKI_CRMEntities v_model = new BKI_CRMEntities();
            List<CM_DM_TU_DIEN> v_lst_loai_khach_hang = new List<CM_DM_TU_DIEN>();
            v_lst_loai_khach_hang = v_model.CM_DM_TU_DIEN.Where(x => x.ID_LOAI_TU_DIEN == new Guid("99902207-1101-4362-97f8-131d74ca1124")).ToList();
            ViewBag.LstLoaiKhachHang = v_lst_loai_khach_hang;
        }

        private void layDanhSachNguoiQuanLy()
        {
            BKI_CRMEntities v_model = new BKI_CRMEntities();
            List<V_DM_NHAN_VIEN_CONG_TY> v_lst_nv = new List<V_DM_NHAN_VIEN_CONG_TY>();
            v_lst_nv = v_model.V_DM_NHAN_VIEN_CONG_TY.Where(x => x.ID == new Guid("c2a1b448-d15b-4877-a373-2f7ee30c665e")).ToList();
            ViewBag.LstNhanVien = v_lst_nv;
        }

        private void LayDanhSach_TrangThai_Quyen()
        {
            BKI_CRMEntities v_model = new BKI_CRMEntities();

            Guid v_guid_cty = new Guid("c2a1b448-d15b-4877-a373-2f7ee30c665e");//dùng tạm id công ty bkindex
            List<DM_TRANG_THAI> v_lst_trang_thai = v_model.DM_TRANG_THAI.Where(x => x.ID_CONG_TY == v_guid_cty).OrderBy(x => x.MA_TRANG_THAI).ToList();
            ViewBag.LstTrangThai = v_lst_trang_thai;

            Guid v_guid_quyen_ql = new Guid("34db7994-c12f-4429-8976-0b051686290a");//dùng id của quyền trong cm_dm_loai_td
            List<CM_DM_TU_DIEN> v_lst_quyen_quan_ly_kh = v_model.CM_DM_TU_DIEN.Where(x => x.ID_LOAI_TU_DIEN == v_guid_quyen_ql).ToList();
            ViewBag.LstQuyen = v_lst_quyen_quan_ly_kh;
        }

        [HttpPost]
        public ActionResult LayThongTinKhachHang(string ip_str_id_khach_hang)
        {
            try
            {
                BKI_CRMEntities v_model = new BKI_CRMEntities();
                DM_KHACH_HANG v_dm_kh = new DM_KHACH_HANG();
                ThongTinKhachHang v_dm_khach_hang = new ThongTinKhachHang();
                v_dm_kh = v_model.DM_KHACH_HANG.Where(x => x.ID == new Guid(ip_str_id_khach_hang)).First();
                v_dm_khach_hang = v_dm_kh.CopyAs<ThongTinKhachHang>();
                return Json(v_dm_khach_hang, JsonRequestBehavior.AllowGet);
            }
            catch (System.Exception v_e)
            {
                v_e.Log();
                return Json(false);
            }

        }

        [HttpPost]
        public ActionResult LayYeuCauTrangThai(string ip_str_id_trang_thai, string ip_str_id_kh_chuyen_trang_thai)
        {
            try
            {
                BKI_CRMEntities v_model = new BKI_CRMEntities();
                List<V_DM_YEU_CAU_TRANG_THAI> v_lst_dm_tt = new List<V_DM_YEU_CAU_TRANG_THAI>();
                v_lst_dm_tt = v_model.V_DM_YEU_CAU_TRANG_THAI.Where(x => x.ID_TRANG_THAI == new Guid(ip_str_id_trang_thai)).OrderBy(x => x.THU_TU).ToList();
                List<DanhSachTrangThaiModel> v_lst_dm_tt_model = new List<DanhSachTrangThaiModel>();
                for (int i = 0; i < v_lst_dm_tt.Count; i++)
                {
                    DanhSachTrangThaiModel v_model_dstt = new DanhSachTrangThaiModel();
                    v_model_dstt = v_lst_dm_tt[i].CopyAs<DanhSachTrangThaiModel>();
                    string v_str_da_duyet = v_model.GD_CHUYEN_TRANG_THAI.Where(x => x.ID == new Guid(ip_str_id_kh_chuyen_trang_thai)).First().KIEM_TRA_YEU_CAU_CHUYEN_TRANG_THAI;
                    if (v_str_da_duyet.Trim().Substring(i, 1) == "1")
                    {
                        v_model_dstt.DA_DUYET = true;
                    }
                    else
                    {
                        v_model_dstt.DA_DUYET = false;
                    }
                    v_lst_dm_tt_model.Add(v_model_dstt);
                }
                TrangThaiHienTaiModel v_model_trang_thai_hien_tai = new TrangThaiHienTaiModel();
                v_model_trang_thai_hien_tai.LST_YEU_CAU = v_lst_dm_tt_model;
                var v_gd_chuyen_tt = v_model.GD_CHUYEN_TRANG_THAI.Where(x => x.ID == new Guid(ip_str_id_kh_chuyen_trang_thai)).First();
                v_model_trang_thai_hien_tai.GHI_CHU = v_gd_chuyen_tt.GHI_CHU;
                v_model_trang_thai_hien_tai.SO_TIEN_DA_NHAN = v_gd_chuyen_tt.SO_TIEN_DA_NHAN;
                return Json(v_model_trang_thai_hien_tai, JsonRequestBehavior.AllowGet);
            }
            catch (System.Exception v_e)
            {
                v_e.Log();
                return Json(false);
            }
        }

        [HttpPost]
        public ActionResult LayLichSuKhachHang(string ip_str_id_khach_hang)
        {
            try
            {
                BKI_CRMEntities v_model = new BKI_CRMEntities();
                List<V_GD_LICH_SU_KHACH_HANG> v_lst_gd_ls = new List<V_GD_LICH_SU_KHACH_HANG>();
                v_lst_gd_ls = v_model.V_GD_LICH_SU_KHACH_HANG.Where(x => x.ID_KHACH_HANG == new Guid(ip_str_id_khach_hang)).ToList();
                return Json(v_lst_gd_ls, JsonRequestBehavior.AllowGet);
            }
            catch (System.Exception v_e)
            {
                v_e.Log();
                return Json(false);
            }

        }
        [HttpPost]
        public ActionResult LayQuanLyKhachHang(string ip_str_id_khach_hang)
        {
            try
            {
                BKI_CRMEntities v_model = new BKI_CRMEntities();
                List<V_GD_NGUOI_QUAN_LY_KHACH_HANG> v_lst_gd_ql_kh = new List<V_GD_NGUOI_QUAN_LY_KHACH_HANG>();
                v_lst_gd_ql_kh = v_model.V_GD_NGUOI_QUAN_LY_KHACH_HANG.Where(x => x.ID_KHACH_HANG_SU_DUNG_SAN_PHAM == new Guid(ip_str_id_khach_hang)).ToList();
                return Json(v_lst_gd_ql_kh, JsonRequestBehavior.AllowGet);
            }
            catch (System.Exception v_e)
            {
                v_e.Log();
                return Json(false);
            }
        }

        [HttpPost]
        public ActionResult LayDanhSachQuanLyKhachHang(string ip_str_id_cong_ty)
        {
            try
            {
                BKI_CRMEntities v_model = new BKI_CRMEntities();
                List<V_DM_NHAN_VIEN_CONG_TY> v_lst_nv = new List<V_DM_NHAN_VIEN_CONG_TY>();
                v_lst_nv = v_model.V_DM_NHAN_VIEN_CONG_TY.Where(x => x.ID == new Guid(ip_str_id_cong_ty)).ToList();
                return Json(v_lst_nv, JsonRequestBehavior.AllowGet);
            }
            catch (System.Exception v_e)
            {
                v_e.Log();
                return Json(false);
            }
        }

        [HttpPost]
        public ActionResult CapNhatTrangThai(string ip_id_chuyen_trang_thai_hien_tai, string ip_str_id_khach_hang, string ip_str_id_trang_thai, string ip_str_ip_nguoi_chuyen_tt, string ip_dc_so_tien, string ip_str_ghi_chu, string ip_str_kiem_tra_yeu_cau)
        {
            try
            {

                if (ip_str_kiem_tra_yeu_cau.Trim().Contains("0"))
                {
                    BKI_CRMEntities v_model = new BKI_CRMEntities();
                    var v_model_gd_chuyen_tt = v_model.GD_CHUYEN_TRANG_THAI.Where(x => x.ID == new Guid(ip_id_chuyen_trang_thai_hien_tai)).First();
                    if (ip_str_kiem_tra_yeu_cau.Trim() == v_model_gd_chuyen_tt.KIEM_TRA_YEU_CAU_CHUYEN_TRANG_THAI.Trim())
                    {
                        return Json(true, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        v_model_gd_chuyen_tt.KIEM_TRA_YEU_CAU_CHUYEN_TRANG_THAI = ip_str_kiem_tra_yeu_cau;
                        v_model_gd_chuyen_tt.SO_TIEN_DA_NHAN = decimal.Parse(ip_dc_so_tien);
                        v_model.SaveChanges();
                        return Json(true, JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    BKI_CRMEntities v_model = new BKI_CRMEntities();
                    var v_model_gd_chuyen_tt = v_model.GD_CHUYEN_TRANG_THAI.Where(x => x.ID == new Guid(ip_id_chuyen_trang_thai_hien_tai)).First();
                    v_model_gd_chuyen_tt.TRANG_THAI_HIEN_TAI_YN = false;
                    v_model_gd_chuyen_tt.KIEM_TRA_YEU_CAU_CHUYEN_TRANG_THAI = ip_str_kiem_tra_yeu_cau.Trim();
                    v_model.SaveChanges();
                    GD_CHUYEN_TRANG_THAI v_gd_chuyen_tt = new GD_CHUYEN_TRANG_THAI();
                    //--------------------------------------------------------------------
                    v_gd_chuyen_tt.ID_KHACH_HANG = v_model_gd_chuyen_tt.ID_KHACH_HANG;
                    v_gd_chuyen_tt.NGAY_CHUYEN_TRANG_THAI = DateTime.Now;
                    v_gd_chuyen_tt.DANG_CHAM_SOC_YN = true;
                    v_gd_chuyen_tt.TRANG_THAI_HIEN_TAI_YN = true;
                    //--------------------------------------------------------------------
                    v_gd_chuyen_tt.ID = Guid.NewGuid();
                    v_gd_chuyen_tt.TRANG_THAI_HIEN_TAI_YN = true;
                    v_gd_chuyen_tt.SO_TIEN_DA_NHAN = decimal.Parse(ip_dc_so_tien);
                    v_gd_chuyen_tt.ID_TRANG_THAI = new Guid(ip_str_id_trang_thai);
                    v_gd_chuyen_tt.ID_NGUOI_CHUYEN_TRANG_THAI = new Guid(ip_str_ip_nguoi_chuyen_tt);
                    v_gd_chuyen_tt.KIEM_TRA_YEU_CAU_CHUYEN_TRANG_THAI = "000";
                    v_model.GD_CHUYEN_TRANG_THAI.Add(v_gd_chuyen_tt);
                    v_model.SaveChanges();
                }
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            catch (System.Exception v_e)
            {
                v_e.Log();
                return Json(false);
            }
        }

        [HttpPost]
        public ActionResult XoaQuanLyKhachHang(string ip_str_id_nguoi_quan_ly, string ip_str_id_khach_hang_sd_sp)
        {
            try
            {
                BKI_CRMEntities v_model = new BKI_CRMEntities();
                var v_gd_nguoi_quan_ly = v_model.GD_NGUOI_QUAN_LY_KHACH_HANG.Where(x => x.ID_KHACH_HANG == new Guid(ip_str_id_khach_hang_sd_sp) && x.ID_NGUOI_SU_DUNG == new Guid(ip_str_id_nguoi_quan_ly)).First();
                v_model.GD_NGUOI_QUAN_LY_KHACH_HANG.Remove(v_gd_nguoi_quan_ly);
                v_model.SaveChanges();
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            catch (System.Exception v_e)
            {
                v_e.Log();
                return Json(false);
            }
        }

        [HttpPost]
        public ActionResult ThemQuanLyKhachHang(string ip_str_id_nguoi_quan_ly, string ip_str_id_quyen, string ip_str_id_khach_hang_sd_sp)
        {
            try
            {
                BKI_CRMEntities v_model = new BKI_CRMEntities();
                GD_NGUOI_QUAN_LY_KHACH_HANG v_gd_nguoi_quan_ly_kh = new GD_NGUOI_QUAN_LY_KHACH_HANG();
                v_gd_nguoi_quan_ly_kh.ID = Guid.NewGuid();
                v_gd_nguoi_quan_ly_kh.ID_NGUOI_SU_DUNG = new Guid(ip_str_id_nguoi_quan_ly);
                v_gd_nguoi_quan_ly_kh.ID_KHACH_HANG = new Guid(ip_str_id_khach_hang_sd_sp);
                v_gd_nguoi_quan_ly_kh.ID_QUYEN_QUAN_LY = new Guid(ip_str_id_quyen);
                v_gd_nguoi_quan_ly_kh.ACTIVE_YN = true;
                v_gd_nguoi_quan_ly_kh.NGAY_CAP_QUYEN = DateTime.Now;
                v_model.GD_NGUOI_QUAN_LY_KHACH_HANG.Add(v_gd_nguoi_quan_ly_kh);
                v_model.SaveChanges();
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            catch (System.Exception v_e)
            {
                v_e.Log();
                return Json(false);
            }
        }
        public void ThemNguoiQuanLyKhachHang(string ip_id_nguoi_quan_ly, Guid ip_id_khach_hang_sd_sp)
        {
            try
            {
                BKI_CRMEntities v_model = new BKI_CRMEntities();
                GD_NGUOI_QUAN_LY_KHACH_HANG v_gd_nguoi_quan_ly = new GD_NGUOI_QUAN_LY_KHACH_HANG();

                v_gd_nguoi_quan_ly.ID = Guid.NewGuid();
                v_gd_nguoi_quan_ly.ID_NGUOI_SU_DUNG = new Guid(ip_id_nguoi_quan_ly);
                v_gd_nguoi_quan_ly.ID_KHACH_HANG = ip_id_khach_hang_sd_sp;
                v_gd_nguoi_quan_ly.ACTIVE_YN = true;
                v_gd_nguoi_quan_ly.NGAY_CAP_QUYEN = DateTime.Now.Date;
                v_gd_nguoi_quan_ly.ID_QUYEN_QUAN_LY = new Guid("b2891955-3080-4b5f-a761-ce9ceb551637");

                v_model.GD_NGUOI_QUAN_LY_KHACH_HANG.Add(v_gd_nguoi_quan_ly);
                v_model.SaveChanges();
            }
            catch (System.Exception v_e)
            {
                v_e.Log();
            }
        }

        public void capNhatTrangThaiKhachHang(string ip_id_trang_thai, Guid ip_id_khach_hang_sd_sp, string ip_str_so_tien, string ip_str_ghi_chu, string ip_str_dang_cham_soc)
        {
            try
            {
                BKI_CRMEntities v_model = new BKI_CRMEntities();
                GD_CHUYEN_TRANG_THAI v_gd_chuyen_trang_thai = new GD_CHUYEN_TRANG_THAI();

                v_gd_chuyen_trang_thai.ID = Guid.NewGuid();
                v_gd_chuyen_trang_thai.TRANG_THAI_HIEN_TAI_YN = true;
                v_gd_chuyen_trang_thai.ID_TRANG_THAI = new Guid(ip_id_trang_thai);
                v_gd_chuyen_trang_thai.ID_KHACH_HANG = ip_id_khach_hang_sd_sp;
                v_gd_chuyen_trang_thai.ID_NGUOI_CHUYEN_TRANG_THAI = new Guid("31a0d5da-6d48-482b-ac68-1273ba8c5bf6");
                v_gd_chuyen_trang_thai.SO_TIEN_DA_NHAN = decimal.Parse(ip_str_so_tien);
                v_gd_chuyen_trang_thai.GHI_CHU = ip_str_ghi_chu;
                v_gd_chuyen_trang_thai.NGAY_CHUYEN_TRANG_THAI = DateTime.Now.Date;
                v_gd_chuyen_trang_thai.DANG_CHAM_SOC_YN = bool.Parse(ip_str_dang_cham_soc);
                v_gd_chuyen_trang_thai.KIEM_TRA_YEU_CAU_CHUYEN_TRANG_THAI = "000000";
                v_model.GD_CHUYEN_TRANG_THAI.Add(v_gd_chuyen_trang_thai);
                v_model.SaveChanges();
            }
            catch (System.Exception v_e)
            {
                v_e.Log();
            }
        }

        public ActionResult DinhNghiaTrangThai() {
            ViewBag.LstTrangThai = LayDanhSachTrangThai();
            return PartialView();
        }

        private List<TrangThaiModel> LayDanhSachTrangThai()
        {
            BKI_CRMEntities v_model = new BKI_CRMEntities();
            var lst_trang_thai = v_model.DM_TRANG_THAI.ToList();
            List<TrangThaiModel> v_lst_trang_thai = new List<TrangThaiModel>();
            foreach (var item in lst_trang_thai)
            {
                TrangThaiModel v_tt = new TrangThaiModel();
                v_tt = item.CopyAs<TrangThaiModel>();
                v_tt.LST_TRANG_THAI_TIEP_THEO = new List<TrangThaiModel>();
                List<DM_CHUYEN_TRANG_THAI> v_lst_chuyen_trang_thai = v_model.DM_CHUYEN_TRANG_THAI.Where(x => x.ID_TRANG_THAI_BAN_DAU == v_tt.ID).ToList();
                foreach (var item2 in v_lst_chuyen_trang_thai)
                {
                    var v_tt_model = new TrangThaiModel();
                    v_tt_model.MA_TRANG_THAI = item2.DM_TRANG_THAI1.MA_TRANG_THAI;
                    v_tt_model.ID = item2.DM_TRANG_THAI1.ID;
                    v_tt_model.LOAI_TRANG_THAI_YN = item2.DM_TRANG_THAI1.LOAI_TRANG_THAI_YN;
                    v_tt_model.MO_TA = item2.DM_TRANG_THAI1.MO_TA;
                    v_tt.LST_TRANG_THAI_TIEP_THEO.Add(v_tt_model);
                }
                v_tt.LST_TRANG_THAI_TIEP_THEO = v_tt.LST_TRANG_THAI_TIEP_THEO.OrderByDescending(x => x.LOAI_TRANG_THAI_YN).ToList();
                v_lst_trang_thai.Add(v_tt);
            }
            v_lst_trang_thai = v_lst_trang_thai.OrderBy(x => x.MA_TRANG_THAI).ToList();
            return v_lst_trang_thai;
        }

        public ActionResult bao_cao_01() {
            
            return View();
        }


        public ActionResult quanLyNguoiSuDung()
        {
            return PartialView();   
        }

        public ActionResult taiKhoanCaNhan()
        {
            return PartialView();
        }

        public ActionResult thongTinLienHe()
        {
            layDanhMucLoaiKhachHang();
            return PartialView();
        }

        public ActionResult DMKhachHang() 
        {
            BKI_CRMEntities v_model = new BKI_CRMEntities();
            List<DM_KHACH_HANG> v_dm_kh = new List<DM_KHACH_HANG>();
            List<V_TU_DIEN> v_tu_dien = new List<V_TU_DIEN>();
            v_dm_kh = v_model.DM_KHACH_HANG.ToList<DM_KHACH_HANG>();
            v_tu_dien = v_model.V_TU_DIEN.Where(x => x.TEN_LOAI_TU_DIEN == "Loại khách hàng").ToList<V_TU_DIEN>();
            ViewBag.v_dm_kh = v_dm_kh;
            ViewBag.v_tu_dien = v_tu_dien;
            return PartialView();
        }
        [HttpGet]
        public ActionResult LayThongTinLienHe(string id_kh)
        {
            BKI_CRMEntities db = new BKI_CRMEntities();
            var id = Guid.Parse(id_kh);
            var thong_tin = db.DM_KHACH_HANG.Where(x => x.ID == id).ToList<DM_KHACH_HANG>();
            string bday = "";
            if(thong_tin.Count>0)bday=thong_tin[0].NGAY_SINH.ToString();
            if(bday.Length>6)
                bday = bday.Substring(6, 4) + "-" + bday.Substring(0, 2) + "-" + bday.Substring(3, 2);
            return Json(new {dat=thong_tin,bday=bday},JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult XoaThongTinLienHe(string id_kh)
        {
            BKI_CRMEntities db = new BKI_CRMEntities();
            var id = Guid.Parse(id_kh);
            db.Database.ExecuteSqlCommand("delete from GD_THONG_TIN_HOP_DONG where ID_KHACH_HANG='" + id + "'");
            db.Database.ExecuteSqlCommand("delete from GD_NGUOI_QUAN_LY_KHACH_HANG where ID_KHACH_HANG='" + id + "'");
            db.Database.ExecuteSqlCommand("delete from GD_CHUYEN_TRANG_THAI where ID_KHACH_HANG='" + id + "'");
            db.Database.ExecuteSqlCommand("delete from GD_ACTION where ID_KHACH_HANG='" + id + "'");
            db.Database.ExecuteSqlCommand("delete from DM_KHACH_HANG where ID='" + id + "'");
            return Json(true, JsonRequestBehavior.AllowGet);
        }
        public ActionResult importExcel()
        {

            if (Session["username"] == null)
            {
                return RedirectToAction("Login", "Admin");
            }
            m_p = new PhanQuyenHeThong();
            string v_actionName = this.ControllerContext.RouteData.Values["action"].ToString();
            string v_controllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
            var v_c = m_p.checkQuyenTruyCap(
                Session["IdUserGroup"].ToString()
                , v_controllerName
                , v_actionName);
            if (v_c == false)
            {
                return RedirectToAction("Login", "Admin");
            }
            return PartialView();
        }
        public ActionResult UploadFile(HttpPostedFileBase excelFile)
        {
            if (Session["username"] == null)
            {
                return RedirectToAction("Login", "Admin");
            }

            m_p = new PhanQuyenHeThong();
            string v_actionName = this.ControllerContext.RouteData.Values["action"].ToString();
            string v_controllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
            var v_c = m_p.checkQuyenTruyCap(
                Session["IdUserGroup"].ToString()
                , v_controllerName
                , v_actionName);
            if (v_c == false)
            {
                return RedirectToAction("Login", "Admin");
            }
            var data = (new SLExcelReader()).ReadExcel(excelFile);
            Session["SessionExcelData"] = data;
            return RedirectToAction("ExcelToDB");
        }

        //private List<NhanVienModel> Lay_danh_sach_nhan_vien(string ip_dc_id_user_cap_tren)
        //{
        //    List<NhanVienModel> op_lst_nv = new List<NhanVienModel>();
        //    BKI_CRMEntities v_model = new BKI_CRMEntities();
        //    var v_lst_ht_user = v_model.HT_USER.Where(x => x.ID_USER_CAP_TREN == ip_dc_id_nhan_vien || ip_dc_id_nhan_vien == -1).ToList();
        //    foreach (var item in v_lst_ht_user)
        //    {
        //        NhanVienModel v_nv = new NhanVienModel();
        //        v_nv = item.CopyAs<NhanVienModel>();
        //        op_lst_nv.Add(v_nv);
        //    }
        //    return op_lst_nv;
        //}
        [HttpPost]
        public ActionResult ExcelToTable()
        {
            var data = Session["SessionExcelData"];
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        private List<ColumnsModel> Lay_danh_sach_cot_table_khach_hang()
        {
            List<ColumnsModel> op_lst_col_kh = new List<ColumnsModel>();
            BKI_CRMEntities v_model = new BKI_CRMEntities();
            var v_guid = new Guid("d63820f1-b56a-4553-a488-d3b79b27f4f1");
            var v_lst_col_kh = v_model.CM_DM_TU_DIEN.Where(x => x.ID_LOAI_TU_DIEN == v_guid).ToList();
            foreach (var item in v_lst_col_kh)
            {
                ColumnsModel v_col_kh = new ColumnsModel();
                v_col_kh = item.CopyAs<ColumnsModel>();
                op_lst_col_kh.Add(v_col_kh);
            }
            return op_lst_col_kh;
        }

        public ActionResult ExcelToDB()
        {
            if (Session["username"] == null)
            {
                return RedirectToAction("Login", "Admin");
            }

            m_p = new PhanQuyenHeThong();
            string v_actionName = this.ControllerContext.RouteData.Values["action"].ToString();
            string v_controllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
            var v_c = m_p.checkQuyenTruyCap(
                Session["IdUserGroup"].ToString()
                , v_controllerName
                , v_actionName);
            if (v_c == false)
            {
                return RedirectToAction("Login", "Admin");
            }
            ViewBag.LstNhanVien = ((SLExcelData)Session["SessionExcelData"]).DataRows;
            ViewBag.LstColumn = Lay_danh_sach_cot_table_khach_hang();
            return PartialView();
        }
        public ActionResult execQueryThemKhachHang(string ip_str_query_insert)
        {
            BKI_CRMEntities v_model = new BKI_CRMEntities();
            try
            {
                v_model.Database.ExecuteSqlCommand(ip_str_query_insert);
                return RedirectToAction("importExcel", "Home");
            }
            catch (Exception)
            {
                return new HttpStatusCodeResult(500, "SQL execution failed");
            }
        }
    }
}
