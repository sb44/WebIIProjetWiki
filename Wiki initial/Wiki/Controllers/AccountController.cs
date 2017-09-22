///////////////////////////////// Par Haiqiang XU /////////////////////////////////////////////
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Wiki.Models.Biz;
using Wiki.Models.Biz.DTO;
using Wiki.Models.Biz.Interfaces;

namespace Wiki.Controllers {
    public class AccountController : HomeController
    {
        private static readonly IUtilisateurRepository utilisateurRepository = new Models.DAL.Utilisateurs();
        private readonly IUtilisateurRepository _utilisateurRepository; 
        private static UtilisateurManager utilisateurManager;

        Dictionary<string, string> dic = new Dictionary<string, string>{
                {"fr-CA", "Français" },
                {"en-CA", "English" },
                {"es-ES", "espanol" }
            };

        public AccountController() {
            if (utilisateurManager == null) {
                _utilisateurRepository = utilisateurRepository;
                utilisateurManager = new UtilisateurManager(_utilisateurRepository);
            }
        }

        // GET: /Account/Connexion
        [AllowAnonymous]
        public ActionResult Connexion(string returnUrl) {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        //
        // POST: /Account/Connexion
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Connexion(ConnexionViewModel model, string ReturnUrl = "") {
            ViewBag.error = "";
            ViewBag.ReturnUrl = ReturnUrl;
            if (!utilisateurManager.Authentifier(model.Courriel, model.MDP)) {
                if (model.MDP != null) {
                    ViewBag.error = "Courriel ou mot de passe invalide!";
                }
                return View(model);
            } else {
                FormsAuthentication.SetAuthCookie(model.Courriel, false);
                // Users.currentUser = Users.GetUserByEmail(email);

                //ajout sasha
                var uDto = utilisateurManager.FindUtilisateurByCourriel(model.Courriel);
                this.Session["CurrentCulture"] = (uDto.Langue.ToString().ToLower().IndexOf("fr") != -1) ? 0 :
                                                 (uDto.Langue.ToString().ToLower().IndexOf("en") != -1) ? 1 :
                                                 (uDto.Langue.ToString().ToLower().IndexOf("es") != -1) ? 2 : 99; // 99 => aucune langue 

                if (ReturnUrl == "") {
                    return RedirectToAction("Index", "Home");
                } else {
                    return Redirect(ReturnUrl);
                }
            }
        }

        //
        // GET: /Account/Register
        public ActionResult Inscription() {     
            var model = new InscriptionViewModel(); 
            model.SelectionLangue = new SelectList(dic, "Key", "Value");   
            return View(model);
        }

        //
        // POST: /Account/Register
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Inscription(InscriptionViewModel model) {
            if (ModelState.IsValid) {
                utilisateurManager.AddUtilisateur(model.Courriel, model.MDP, model.Prenom, model.NomFamille, model.Langue);

                //ajout sasha 
                this.Session["CurrentCulture"] = (model.Langue.ToString().ToLower().IndexOf("fr") != -1) ? 0 :
                                                 (model.Langue.ToString().ToLower().IndexOf("en") != -1) ? 1 :
                                                 (model.Langue.ToString().ToLower().IndexOf("es") != -1) ? 2 : 99; // 99 => aucune langue 

                return RedirectToAction("Index", "Home");
            }
            // Si nous sommes arrivés là, un échec s’est produit. Réafficher le formulaire
            model.SelectionLangue = new SelectList(dic, "Key", "Value");
            return View(model);
        }

        public ActionResult ModifierProfil(string ReturnUrl) {

            ViewBag.ReturnUrl = ReturnUrl;
            ChangerProfilViewModel model = new ChangerProfilViewModel();
            UtilisateurDTO u = new UtilisateurDTO(); 
            u = utilisateurManager.FindUtilisateurByCourriel(System.Web.HttpContext.Current.User.Identity.Name);
            model.Langue = u.Langue;
            model.NomFamille = u.NomFamille;
            model.Prenom = u.Prenom;
            model.Id = u.Id;
            model.SelectionLangue = new SelectList(dic, "Key", "Value");
            return View(model);
        }

        [HttpPost]
        public ActionResult ModifierProfil(ChangerProfilViewModel model, string ReturnUrl) {
            ViewBag.ReturnUrl = ReturnUrl;
            utilisateurManager.UpdateUtilisateur(model.Prenom, model.NomFamille, model.Id, model.Langue);

            //ajout sasha 
            this.Session["CurrentCulture"] = (model.Langue.ToString().ToLower().IndexOf("fr") != -1) ? 0 :
                                             (model.Langue.ToString().ToLower().IndexOf("en") != -1) ? 1 :
                                             (model.Langue.ToString().ToLower().IndexOf("es") != -1) ? 2 : 99; // 99 => aucune langue 

            return Redirect(ReturnUrl);
        }

        public ActionResult ModifierMDP(int Id, string ReturnUrl) {
            ViewBag.ReturnUrl = ReturnUrl;
            ChangerMotDePasseViewModel model = new ChangerMotDePasseViewModel();
            model.Id = Id;
            return View(model);
        }

        [HttpPost]
        public ActionResult ModifierMDP(ChangerMotDePasseViewModel model, string ReturnUrl) {
            ViewBag.ReturnUrl = ReturnUrl;
            utilisateurManager.UpdateMotDePasse(model.Id, model.MDP);
            return Redirect(ReturnUrl);
        }

        // POST: /Account/LogOff
 

        public ActionResult Deconnexion() {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }
    }
}