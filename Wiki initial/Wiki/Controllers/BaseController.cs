using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Wiki.CultureHelp;
using System.Threading;

namespace Wiki.Controllers
{
    public class BaseController : Controller
    {
        protected override void ExecuteCore()
        {
            HttpCookie languageCookie = System.Web.HttpContext.Current.Request.Cookies["lang"];
            if (languageCookie != null)
            {
                CultureHelper.CurrentCulture = int.Parse(languageCookie.Value);
            }
            else
            {
                int culture = 0;
                if (this.Session == null || this.Session["CurrentCulture"] == null)
                {

                    int.TryParse(System.Configuration.ConfigurationManager.AppSettings["Culture"], out culture);
                    this.Session["CurrentCulture"] = culture;
                }
                else
                {
                    culture = (int)this.Session["CurrentCulture"];
                }
                //  CultureHelper classe méthode 
                CultureHelper.CurrentCulture = culture;
            }




            base.ExecuteCore();
        }

        protected override bool DisableAsyncSupport
        {
            get { return true; }
        }

    }
}