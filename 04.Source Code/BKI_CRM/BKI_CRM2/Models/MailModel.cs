using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BKI_CRM2.Models
{
    public class MailModel
    {
        [Required(ErrorMessage = "Nhập email để gửi đến")]
        [DataType(DataType.EmailAddress)]
        [RegularExpression(".+\\@.+\\..+", ErrorMessage = "Email không hợp lệ!")]
        public string To { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
    }
}