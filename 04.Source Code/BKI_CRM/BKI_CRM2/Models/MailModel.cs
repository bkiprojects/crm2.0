using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BKI_CRM2.Models
{
    public class MailModel
    {
        public IEnumerable<string> SelectedCities { get; set; }
        [Required(ErrorMessage = "Nhập email để gửi đến")]
        [DataType(DataType.EmailAddress)]
        [RegularExpression(".+\\@.+\\..+", ErrorMessage = "Email không hợp lệ!")]
        public IEnumerable<SelectListItem> To { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
    }
}