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
    public class ContractController : Controller
    {
        //
        // GET: /Contract/

        public ActionResult Index()
        {
            CrmEntities v_model = new CrmEntities();
            List<Contract> v_hd = new List<Contract>();
            v_hd = v_model.Contract.Where(x=>x.Id>0).ToList<Contract>();
            ViewBag.v_hd = v_hd;
            return PartialView();
        }

    }
}
