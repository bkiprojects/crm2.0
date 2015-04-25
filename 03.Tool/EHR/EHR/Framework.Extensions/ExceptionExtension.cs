using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net;

namespace Framework.Extensions
{
    public static class ExceptionExtension
    {
        private static ILog logger = LogManager.GetLogger((System.Type)typeof(ExceptionExtension));

        public static void Log(this System.Exception ex, bool sendmail = true)
        {
            if (ex != null)
            {
                logger.Error(ex);
                if (sendmail)
                {
                    // TODO: Implement send email
                //    string str = ConfigurationManager.AppSettings["ExceptionNotificationEmail");
                //    if (str.IsNotNullOrEmpty())
                //    {
                //        string subject = "Exception from:" + ((HttpContext.Current == null) ? System.AppDomain.CurrentDomain.get_FriendlyName() : HttpContext.Current.Request.Url.ToString());
                //        SmtpMail.SendNetworkEmailFromConfig(str, subject, ex.ToString(), true, true, false);
                //    }
                }
            }
        }
    }
}
