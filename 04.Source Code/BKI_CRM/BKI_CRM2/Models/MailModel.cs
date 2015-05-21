using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BKI_CRM2.Models
{
    public class MailModel
    {
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string To { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
    }
}