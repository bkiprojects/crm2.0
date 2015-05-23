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
using System.Text.RegularExpressions;

namespace BKI_CRM2.Controllers
{
    public class SendMailerController : Controller
    {
    [HttpGet]
        public ActionResult Index()
        {
            return PartialView();
        }
        [HttpPost]
        public ActionResult Index(string values)
        {
            if (values != "")
            {
                CrmEntities v_model = new CrmEntities();
                 List<Contact> v_ct = new List<Contact>();
                v_ct = v_model.Contact.ToList<Contact>();
                 var chuoi_email= "";
                decimal[] v_id = new decimal[0];
                string[] IdContact = values.Split(',');
                v_id = new decimal[IdContact.Length];
                for (int i = 0; i < v_id.Length; i++)
                {
                    v_id[i] = Convert.ToDecimal(IdContact[i]);
                   int  index= (int)v_id[i];
                   var v_email = v_ct.FirstOrDefault(x => x.Id == index).Email;
                   if (!String.IsNullOrEmpty(v_email))
                    {
                        chuoi_email+=v_ct[i].Email+",";
                    }
                }
                BKI_CRM2.Models.MailModel citiesViewModel = new BKI_CRM2.Models.MailModel()
                {
                    To = chuoi_email,
                };
                return PartialView(citiesViewModel);
            }
            else
            {
                 return PartialView();
            }    
        }//[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Index(BKI_CRM2.Models.MailModel objModelMail/*, HttpPostedFileBase fileUploader*/)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        HttpPostedFile fileUploader = (HttpPostedFile)Session["EmailAttachment"];
        //        string from = "buihongnhungxinh@gmail.com"; //example:- sourabh9303@gmail.com
              
        //        using (MailMessage mail = new MailMessage(from, objModelMail.txt_To))
        //        {
        //            mail.Subject = objModelMail.Subject;
        //            mail.Body = objModelMail.Body;
        //            if (fileUploader != null)
        //            {
        //                string fileName = Path.GetFileName(fileUploader.FileName);
        //                mail.Attachments.Add(new Attachment(fileUploader.InputStream, fileName));
        //            }
        //            mail.IsBodyHtml = false;
        //            SmtpClient smtp = new SmtpClient();
        //            smtp.Host = "smtp.gmail.com";
        //            smtp.EnableSsl = true;
        //            NetworkCredential networkCredential = new NetworkCredential(from, "buihongnhung");
        //            smtp.UseDefaultCredentials = true;
        //            smtp.Credentials = networkCredential;
        //            smtp.Port = 587;
        //            smtp.Send(mail);
        //            ViewBag.Message = "Sent";
        //            return PartialView("Index", objModelMail);
        //        }
        //    }
        //    else
        //    {
        //        ViewBag.Message = "Not Sent";
        //        return PartialView();
        //    }
        //}

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Sending(BKI_CRM2.Models.MailModel objModelMail/*, HttpPostedFileBase fileUploader*/)
        {
            if (ModelState.IsValid)
            {
                Regex emailcheck = new Regex(".+\\@.+\\..+");
                CrmEntities v_model = new CrmEntities();
                HttpPostedFile fileUploader = (HttpPostedFile)Session["EmailAttachment"];
                string from = "buihongnhungxinh@gmail.com"; //example:- sourabh9303@gmail.com
                //List<string> list = objModelMail.SelectedContact.ToList();
                string list_email = objModelMail.To;

                var danh_sach_email = list_email.Split(new char[] { ',',';' }, StringSplitOptions.RemoveEmptyEntries);

                using (MailMessage mail = new MailMessage())
                {
                    MailAddress from1 = new MailAddress("buihongnhungxinh@gmail.com");
                    mail.From = from1;
                    mail.Subject = objModelMail.Subject;
                    mail.Body = objModelMail.Body;
                    if (fileUploader != null)
                    {
                        string fileName = Path.GetFileName(fileUploader.FileName);
                        mail.Attachments.Add(new Attachment(fileUploader.InputStream, fileName));
                    }
                    for (int i = 0; i < danh_sach_email.Length; i++)
                    {
                        if (!emailcheck.IsMatch(danh_sach_email[i])) {
                            ViewBag.Message = "Wrong address";
                            Session["EmailAttachment"] = null;
                            return PartialView("Index"); 
                        }
                        mail.To.Add(danh_sach_email[i]);
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
                ViewBag.Message = "No address";
                Session["EmailAttachment"] = null;
                return PartialView("Index");
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