using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Wiki.Models.Biz; //aj sb
using Wiki.Models.Biz.Interfaces;

namespace Wiki.Controllers {
    public class HomeController : Controller {

        //static Articles repo = new Articles();
        //static IList<Article> lstArticles = repo.GetArticles(); 

        private static readonly IArticleRepository articleRepository = new Models.DAL.Articles();
        private readonly IArticleRepository _articleRepository; //readonly assure que seul le ctor peut l'assigner
        private static ArticleManager articleManager;

        public HomeController() {
            if (articleManager == null) {
                _articleRepository = articleRepository;
                articleManager = new ArticleManager(_articleRepository);
            }
        }

        // GET: Home
        [Route("~/")]                       // cet atttribut défini la page d'accueil de l'application
        [Route("~/home")]
        [Route("home/index/{titre?}")]      // titre est un parametètre optionnel
        public ActionResult Index(string titre) {

            // AFFICHER L'ARTICLE SI SAISIE OU SÉLECTIONNÉ
            if (!String.IsNullOrEmpty(titre)) {
                Article a = articleManager.lstArticles.FirstOrDefault(p => p.Titre.Equals(titre)); // Article a = repo.Find(titre);
                if (a != null)
                    return View(a);
                else { /* Saisie d'un article inexistant au clavier, donc 
                       INVITATION À CRÉER L'ARTICLE S'IL Y A LIEU (home/index/<article inexistant>) */
                    ViewBag.TitreSaisieInexistant = titre;
                    return View();
                }

            } // FIN 

            return View();
        }

        [ChildActionOnly]
        public ActionResult PartialTableDesMatieres() {
            return PartialView(articleManager.lstArticles);
        }

        [HttpGet]
        [Route("home/ajouter/{titre}")]
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
        [Route("home/ajouter/{titre}")]
        public ActionResult ajouter(Article a, string operation) {
            switch (operation) {
                case "Ajouter":

                    // Valider que le titre de l'article n'existe pas déjà...
                    if (articleManager.lstArticles.FirstOrDefault(p => p.Titre.Equals(a.Titre)) != null)
                        ModelState.AddModelError("Titre", "Ce titre est déjà existant, veuillez réessayer avec un autre titre.");

                    if (ModelState.IsValid) {

                        //if (repo.Add(a) != 0)
                        //    lstArticles = repo.GetArticles();
                        articleManager.Add(new Models.Biz.DTO.ArticleDTO { Titre = a.Titre, Contenu = a.Contenu, DateModification = a.DateModification, Revision = a.Revision, IdContributeur = a.IdContributeur });

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
        [Route("home/modifier/{titre}")]
        public ActionResult modifier(string titre) {
            // Va faire afficher la page d'erreur par défault Error.cshtml si le titre de l'article n'existe pas..
            return View(articleManager.lstArticles.FirstOrDefault(p => p.Titre.Equals(titre)));
        }

        [HttpPost]
        [Route("home/modifier/{titre}")]
        [ValidateInput(false)]
        public ActionResult modifier(Article a, string operation) {
            switch (operation) {
                case "Enregistrer":
                    if (ModelState.IsValid) {
                        articleManager.Update(new Models.Biz.DTO.ArticleDTO { Titre = a.Titre, Contenu = a.Contenu, DateModification = a.DateModification, Revision = a.Revision, IdContributeur = a.IdContributeur });
                        //if (repo.Update(a) != 0)
                        //    lstArticles = repo.GetArticles();
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

        [Route("home/supprimer/{titre}")]
        public ActionResult supprimerArticle(string titre) {
            // Va faire afficher la page d'erreur par défault Error.cshtml si le titre de l'article n'existe pas..
            return View(articleManager.lstArticles.FirstOrDefault(p => p.Titre.Equals(titre)));
        }

        [HttpPost]
        [Route("home/supprimer/{titre}")]
        public ActionResult supprimerArticleConfirmation(string titre) {
            //Article.supprimer(Id);
            //Utilisateur u = Utilisateur.getUtilisateurParName(User.Identity.Name);
            //if (repo.Delete(titre) != 0)
            //    articleManager.lstArticles.Remove(articleManager.lstArticles.FirstOrDefault(p => p.Titre.Equals(titre)));
            articleManager.Delete(titre);
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