using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Wiki.Models.Biz; //aj sb

namespace Wiki.Controllers
{
    public class HomeController : Controller
    {

        // GET: Home
        public ActionResult Index()
        {
            var a = new Article {
                Titre = "Article 1",
                Contenu = "",
                Revision = 1,
                IdContributeur = 1,
                DateModification = DateTime.Now
            };

            return Redirect(Url.Action("Index", "DAL", a)); // https://stackoverflow.com/questions/15825499/passing-an-object-in-redirecttoaction

            //return View();

        }
    }
}