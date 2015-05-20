using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

namespace BKI_CRM2.Controllers
{
    public class SendMailerController : Controller
    {
        public ActionResult Index()
        {
            return PartialView();
        }

        [HttpPost]
        public ActionResult Index(BKI_CRM2.Models.MailModel objModelMail, HttpPostedFileBase fileUploader)
        {
            if (ModelState.IsValid)
            {
                string from = "buihongnhungxinh@gmail.com"; //example:- sourabh9303@gmail.com
                using (MailMessage mail = new MailMessage(from, objModelMail.To))
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
                return PartialView();
            }
        }
    }
}