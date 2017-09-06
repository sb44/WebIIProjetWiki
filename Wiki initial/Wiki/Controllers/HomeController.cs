using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Wiki.Models.Biz; //aj sb
using Wiki.Models.DAL;

namespace Wiki.Controllers {
    public class HomeController : Controller {
        static Articles repo = new Articles();
        static IList<Article> lstArticles = repo.GetArticles();

        // GET: Home
        //[Route("home/index/{titre}")]
        public ActionResult Index(string titre) { // http://localhost:63581/?titre=mon%20article%201

            // AFFICHER L'ARTICLE SI SAISIE OU SÉLECTIONNÉ
            if (!String.IsNullOrEmpty(titre)) {
                Article a = lstArticles.FirstOrDefault(p => p.Titre.Equals(titre)); // Article a = repo.Find(titre);
                if (a != null)
                    return View(a);
                else { // Saisie d'un article inexistant au clavier:
                    ViewBag.TitreSaisieInexistant = titre;
                    return View();
                }

            } // FIN 

            //// INVITATION À CRÉER L'ARTICLE S'IL Y A LIEU (home/index/<article inexistant>)
            //string url = HttpContext.Request.Url.ToString();
            //if (url.Substring(url.Length - 11).ToLower().Equals("home/index/")) {  
            //} // FIN

            return View();
        }

        [ChildActionOnly]
        public ActionResult PartialTableDesMatieres() {
            return PartialView(lstArticles);
        }

        [HttpGet]
        public ActionResult ajouter(string titre) //ajouter article dans un blog
        {
            //User.Identity.Name est courriel
            //string fullName = Utilisateur.getFullNameByUserName(User.Identity.Name);
            //ViewBag.Prenom = fullName.Split(' ')[0];
            //ViewBag.Nom = fullName.Split(' ')[1];

            ViewBag.TitreParDefault = titre; // titre par défaut dans le cas d'une Saisie d'un article inexistant au clavier:
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult ajouter(Article a, string operation) {
            switch (operation) {
                case "Enregistrer":
                    if (ModelState.IsValid) {
                        if (repo.Add(a) != 0)
                            lstArticles = repo.GetArticles();
                        return RedirectToAction("Index", "Home", new { titre = a.Titre }); // pour displayer la nouvelle article créé..
                    } else {
                        //string fullName = Utilisateur.getFullNameByUserName(User.Identity.Name);
                        //ViewBag.Prenom = fullName.Split(' ')[0];
                        //ViewBag.Nom = fullName.Split(' ')[1];

                        return View(a);
                    }
                    break;
                case "Apercu Html":
                    ViewBag.ApercuContenu = true;
                    break;
            }
            return View(a);
        }

        [HttpGet]
        public ActionResult modifier(string titre) {
            return View(lstArticles.FirstOrDefault(p => p.Titre.Equals(titre)));
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult modifier(Article a, string operation) {
            switch (operation) {
                case "Enregistrer":
                    if (ModelState.IsValid) {
                        if (repo.Update(a) != 0)
                            lstArticles = repo.GetArticles();
                        return RedirectToAction("Index", "Home");
                    } else
                        return View(a);
                    break;
                case "Apercu Html":
                    ViewBag.ApercuContenu = true;
                    break;
            }

            return View(a);
        }

        public ActionResult supprimerArticle(string titre) {
            return View(lstArticles.FirstOrDefault(p => p.Titre.Equals(titre)));
        }

        [HttpPost, ActionName("supprimerArticle")]
        public ActionResult supprimerArticleConfirmation(string titre) {
            //Article.supprimer(Id);
            //Utilisateur u = Utilisateur.getUtilisateurParName(User.Identity.Name);
            if (repo.Delete(titre) != 0)
                lstArticles.Remove(lstArticles.FirstOrDefault(p => p.Titre.Equals(titre)));

            return RedirectToAction("Index", "Home");
        }


        //Articles repo = new Articles();
        //public ActionResult Index() {

        //    return View();
        //    // pour tester les méthodes du DAL:
        //    // return Redirect(Url.Action("Index", "DAL", null)); // https://stackoverflow.com/questions/15825499/passing-an-object-in-redirecttoaction

        //}

    }
}