using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Wiki.Models.Biz; //aj sb
using Wiki.Models.Biz.Interfaces;
using Wiki.CultureHelp;
using Wiki.Models.ViewModels;
using System.Threading;
using AutoMapper;
using Wiki.Models.Biz.DTO;

namespace Wiki.Controllers {
    public class HomeController : BaseController {
        public ActionResult ChangeCurrentCulture(int id)
        {
            //  
            // Changer la culture courantpour cet utilisateur
            //  
            CultureHelper.CurrentCulture = id;
            // devrait verifier l'exitence 
            if (Response.Cookies["lang"] == null)
            {
                Response.Cookies.Add(new HttpCookie("lang", id.ToString()) { HttpOnly = true });
            }
            else
            {
                Response.Cookies["lang"].Value = id.ToString();
            }
            //  
            // Mettre current culture dans la session HTTP.   
            //  
            Session["CurrentCulture"] = id;
            //  
            // retourner à la page précedente
            //  
            return Redirect(Request.UrlReferrer.ToString());
        }

        /// ////////////////////////////////////////////////////////////
        /// ////////////////////////////////////////////////////////////
        /// ////////////////////////////////////////////////////////////
        
        protected override bool DisableAsyncSupport
        {
            get { return true; }
        }

        // https://www.devtrends.co.uk/blog/integrating-the-unity.mvc3-1.1-nuget-package-from-scratch https://www.devtrends.co.uk/blog/how-not-to-do-dependency-injection-the-static-or-singleton-container https://stackoverflow.com/questions/194999/are-static-class-instances-unique-to-a-request-or-a-server-in-asp-net
        // Utilisation de Unity.MVC5 (ajouter avec NuGet)
        // L'usage de ce constructeur pour HomeController permet d'injecter l'instance et éviter cette ligne de code qui exige référence et crée une dépendance à la couche de persistence :   private readonly IArticleRepository articleRepository = new Models.DAL.Articles();
        // 1. Dans Application_Start() => UnityConfig.RegisterComponents();
        // 2. Dans class UnityConfig, ajout de l'instance => container.RegisterType<Models.Biz.Interfaces.IArticleRepository, Models.DAL.Articles>(); 
        private readonly IArticleRepository _articleRepository; //readonly assure que seul le ctor peut l'assigner
        private ArticleManager _articleManager;

        public HomeController(IArticleRepository articleRepository) {
            _articleRepository = articleRepository;
            _articleManager = new ArticleManager(_articleRepository);
        }

        // GET: Home
        [Route("~/")]                       // cet atttribut défini la page d'accueil de l'application
        [Route("~/home")]
        [Route("home/index/{titre?}")]      // titre est un parametètre optionnel
        public ActionResult Index(string titre, string operation) {

            if (!String.IsNullOrEmpty(operation) && String.IsNullOrEmpty(titre)) {
                ModelState.AddModelError("Titre", Ressource.RessourceView.ERR_titre_vide);
                return View();
            }

            // AFFICHER L'ARTICLE SI SAISIE OU SÉLECTIONNÉ
            if (!String.IsNullOrEmpty(titre)) {

                Article article = _articleManager.lstArticles.FirstOrDefault(p => p.Titre.Equals(titre));
                ArticleViewModel model = Mapper.Map<Article, ArticleViewModel>(article); // conversion d'une entité Article en ArticleViewModel

                if (model != null)
                    return View(model);
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
            IList<Article> lArt = _articleManager.lstArticles;
            IList<ArticleViewModel> model = Mapper.Map<IList<Article>, IList<ArticleViewModel>>(lArt);
            return PartialView(model);
        }

        [HttpGet]
        [Route("home/ajouter/{titre}")]
        public ActionResult ajouter(string titre) //ajouter article dans un blog
        {
            if (!User.Identity.IsAuthenticated)               /////////////////////// Ajout par Haiqiang XU 
            {                  
                return RedirectToAction("Connexion", "Account");   
            }

            ViewBag.TitreParDefault = titre; // titre par défaut dans le cas d'une Saisie d'un article inexistant au clavier:
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        [Route("home/ajouter/{titre}")]
        public ActionResult ajouter(ArticleViewModel a, string operation) {
            switch (operation) {
                case "Ajouter":
                    // Valider que le titre de l'article n'existe pas déjà...
                    if (_articleManager.lstArticles.FirstOrDefault(p => p.Titre.Equals(a.Titre)) != null)
                        ModelState.AddModelError("Titre", Ressource.RessourceView.ERR_HC_Home_titre);

                    if (ModelState.IsValid) {
                        ArticleDTO aDto = Mapper.Map<ArticleViewModel, ArticleDTO>(a); //conversion d'une entité ArticleViewModel en ArticleDTO
                        _articleManager.Add(aDto);
                        return RedirectToAction("Index", "Home", new { titre = a.Titre }); // pour displayer la nouvelle article créé..
                    } else {
                        return View(a);
                    }
                    break;
                case "Html":
                    ViewBag.ApercuContenu = true;
                    break;
            }
            return View(a);
        }

        [HttpGet]
        [Route("home/modifier/{titre}")]
        public ActionResult modifier(string titre) {
            // Va faire afficher la page d'erreur par défault Error.cshtml si le titre de l'article n'existe pas..

            if (!User.Identity.IsAuthenticated)               /////////////////////// Ajout par Haiqiang XU 
            {
                return RedirectToAction("Connexion", "Account");
            }

            Article article = _articleManager.lstArticles.FirstOrDefault(p => p.Titre.Equals(titre));
            ArticleViewModel model = Mapper.Map<Article, ArticleViewModel>(article); // conversion d'une entité Article en ArticleViewModel
            return View(model);
        }

        [HttpPost]
        [Route("home/modifier/{titre}")]
        [ValidateInput(false)]
        public ActionResult modifier(ArticleViewModel a, string operation) {
            switch (operation) {
                case "Enregistrer":
                    if (ModelState.IsValid) {
                        ArticleDTO aDto = Mapper.Map<ArticleViewModel, ArticleDTO>(a);
                        _articleManager.Update(aDto);
                        return RedirectToAction("Index", "Home");
                    } else
                        return View(a);
                    break;
                case "Html":
                    ViewBag.ApercuContenu = true;
                    break;
            }

            return View(a);
        }

        [Route("home/supprimer/{titre}")]
        public ActionResult supprimerArticle(string titre) {
            // Va faire afficher la page d'erreur par défault Error.cshtml si le titre de l'article n'existe pas..

            if (!User.Identity.IsAuthenticated)               /////////////////////// Ajout par Haiqiang XU 
            {
                return RedirectToAction("Connexion", "Account");
            }

            Article article = _articleManager.lstArticles.FirstOrDefault(p => p.Titre.Equals(titre));
            ArticleViewModel model = Mapper.Map<Article, ArticleViewModel>(article); // conversion d'une entité Article en ArticleViewModel
            return View(model);

        }

        [HttpPost]
        [Route("home/supprimer/{titre}")]
        public ActionResult supprimerArticleConfirmation(string titre) {
            _articleManager.Delete(titre);
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