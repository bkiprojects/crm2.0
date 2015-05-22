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
using System.Net;

namespace BKI_CRM2.Controllers
{
    public class SendMailerController : Controller
    {
        public ActionResult Index()
        {
            return PartialView();
        }
        public ActionResult Index1(string values)
        {
            if (values != "")
            {
                CrmEntities v_model = new CrmEntities();
                decimal[] v_id = new decimal[0];
                string[] IdContact = values.Split(',');
                v_id = new decimal[IdContact.Length];
                for (int i = 0; i < v_id.Length; i++)
                {
                    v_id[i] = Convert.ToDecimal(IdContact[i]);
                }

                List<Contact> v_ct = new List<Contact>();
                v_ct = v_model.Contact.ToList<Contact>();
                //  List<decimal> idct = new List<decimal>(); List<string> namect = new List<string>();
                List<SelectListItem> listSelectListItems = new List<SelectListItem>();
                for (int i = 0; i < v_ct.Count; i++)
                {
                    Boolean bl=false;
                    for (int j = 0; j < v_id.Length; j++)
                    {
                        if (v_ct[i].Id == v_id[j])
                        {
                            bl = true;
                        }
                    }
                    SelectListItem selectList = new SelectListItem()
                    {
                       // Text = v_ct[i].Ho + " " + v_ct[i].Ten,
                       Text= v_ct[i].Email,
                        Value = v_ct[i].Id.ToString(),
                       Selected = bl,
                    };
                    //nếu có email thì add vào list, không thì ... chưa xử lí
                    if( selectList.Text!="")
                    listSelectListItems.Add(selectList);
                    //  idct.Add(v_ct[i].Id); namect.Add(v_ct[i].Ho + " " + v_ct[i].Ten);
                }
                ViewBag.listSelectListItems = listSelectListItems;

                BKI_CRM2.Models.MailModel citiesViewModel = new BKI_CRM2.Models.MailModel()
                {
                    To = listSelectListItems,
                };
                //return PartialView(citiesViewModel);
                // return Json(true, JsonRequestBehavior.AllowGet);
                return PartialView(citiesViewModel);
            }
            else return Json(true, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(BKI_CRM2.Models.MailModel objModelMail/*, HttpPostedFileBase fileUploader*/)
        {
            if (ModelState.IsValid)
            {
                HttpPostedFile fileUploader = (HttpPostedFile)Session["EmailAttachment"];
                string from = "buihongnhungxinh@gmail.com"; //example:- sourabh9303@gmail.com
                List<string> list= objModelMail.SelectedContact.ToList();
                using (MailMessage mail = new MailMessage(from, list[0]))
                {
                    mail.Subject = objModelMail.Subject;
                    mail.Body = objModelMail.Body;
                    if (fileUploader != null)
                    {
                        string fileName = Path.GetFileName(fileUploader.FileName);
                        mail.Attachments.Add(new Attachment(fileUploader.InputStream, fileName));
                    }
                    mail.IsBodyHtml = false;
                    SmtpClient smtp = new SmtpClient();
                    smtp.Host = "smtp.gmail.com";
                    smtp.EnableSsl = true;
                    NetworkCredential networkCredential = new NetworkCredential(from, "buihongnhung");
                    smtp.UseDefaultCredentials = true;
                    smtp.Credentials = networkCredential;
                    smtp.Port = 587;
                    smtp.Send(mail);
                    ViewBag.Message = "Sent";
                    return PartialView("Index", objModelMail);
                }
            }
            else
            {
                ViewBag.Message = "Not Sent";
                return PartialView();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index2(BKI_CRM2.Models.MailModel objModelMail/*, HttpPostedFileBase fileUploader*/)
        {

            if (objModelMail.SelectedContact != null)
            {
            CrmEntities v_model = new CrmEntities();
                HttpPostedFile fileUploader = (HttpPostedFile)Session["EmailAttachment"];
                string from = "buihongnhungxinh@gmail.com"; //example:- sourabh9303@gmail.com
                List<string> list = objModelMail.SelectedContact.ToList();
                decimal[] v_id= new decimal[list.Count];
                for (int i = 0; i < v_id.Length; i++)
                {
                    v_id[i] = Convert.ToDecimal(list[i]);
                    var index= v_id[i];
                    var email = v_model.Contact.Where(x => x.Id == index).First().Email;
                    using (MailMessage mail = new MailMessage(from, email))
                    {
                        mail.Subject = objModelMail.Subject;
                        mail.Body = objModelMail.Body;
                        if (fileUploader != null)
                        {
                            string fileName = Path.GetFileName(fileUploader.FileName);
                            mail.Attachments.Add(new Attachment(fileUploader.InputStream, fileName));
                        }
                        mail.IsBodyHtml = false;
                        SmtpClient smtp = new SmtpClient();
                        smtp.Host = "smtp.gmail.com";
                        smtp.EnableSsl = true;
                        NetworkCredential networkCredential = new NetworkCredential(from, "buihongnhung");
                        smtp.UseDefaultCredentials = true;
                        smtp.Credentials = networkCredential;
                        smtp.Port = 587;
                        smtp.Send(mail);
                       
                    }
                }
                ViewBag.Message = "Sent";
                return PartialView("Index", objModelMail);
               
            }
            else
            {
                ViewBag.Message = "No address";
                return PartialView("Index1");
            }
        }
        public void GetFileRequest()
        {
            HttpPostedFile pic = null; decimal IdContact = -1;
            if (System.Web.HttpContext.Current.Request.Files.AllKeys.Any())
            {
                pic = System.Web.HttpContext.Current.Request.Files["Attachments"];
            }
            Session["EmailAttachment"] = pic;
        }
    }
}