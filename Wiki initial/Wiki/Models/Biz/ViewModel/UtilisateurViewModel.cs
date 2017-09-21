/////////////////////////////////// Par Haiqiang Xu   ////////////////////////////////////////////////////  
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Wiki.Ressource;

using Wiki.Models.DAL;

namespace Wiki.Models.Biz {
    public class ConnexionViewModel {
        [Required(ErrorMessage = "Veuillez entrer votre courriel!"), StringLength(50, ErrorMessage = "Courriel ne peut pas depasser 50 lettres!")]
        [RegularExpression("^[a-z0-9_\\+-]+(\\.[a-z0-9_\\+-]+)*@[a-z0-9-]+(\\.[a-z0-9]+)*\\.([a-z]{2,4})$", ErrorMessage = "Courriel invalide!")]
        [Display(Name = "courriel", ResourceType = typeof(RessourceView))]
        public string Courriel { set; get; }

        [DataType(DataType.Password)]
        [Required, StringLength(70, MinimumLength = 6, ErrorMessage = "Mot de passe doit etre moins que 70 letters et plus que 6 lesttres!")]
        [Display(Name = "motDePass", ResourceType = typeof(RessourceView))]
        public string MDP { get; set; }
    }

    public class InscriptionViewModel {
        [Uniqueness("Courriel")]
        [Required(ErrorMessage = "Veuillez entrer votre courriel!"), StringLength(50,ErrorMessage = "Courriel ne peut pas depasser 50 lettres!")]
        [RegularExpression("^[a-z0-9_\\+-]+(\\.[a-z0-9_\\+-]+)*@[a-z0-9-]+(\\.[a-z0-9]+)*\\.([a-z]{2,4})$", ErrorMessage = "Courriel invalide!")]
        [Display(Name = "courriel", ResourceType = typeof(RessourceView))]
        public string Courriel { set; get; }

        [DataType(DataType.Password)]
        [Required, StringLength(70, MinimumLength = 6, ErrorMessage = "Mot de passe doit etre moins que 70 letters et plus que 6 lesttres!")]
        [Display(Name = "motDePass", ResourceType = typeof(RessourceView))]
        public string MDP { get; set; }

        [DataType(DataType.Password)]
        [Required, StringLength(70, MinimumLength = 6, ErrorMessage = "Mot de passe doit etre moins que 70 letters et plus que 6 lesttres!")]
        [Compare("MDP", ErrorMessage = "les mots de passe ne sont pas memes!")]
        [Display(Name = "MDPConfirmer", ResourceType = typeof(RessourceView))]
        public string MDPConfirmer { get; set; }

        [Required(ErrorMessage = "Veuillez entrer votre nom de famille!"), StringLength(50, ErrorMessage = "Nom de famille ne peut pas depasser 50 lettres!")]
        [Display(Name = "nomFamille", ResourceType = typeof(RessourceView))]
        public string NomFamille { get; set; }

        [Required(ErrorMessage = "Veuillez entrer votre prenom!"), StringLength(50, ErrorMessage = "Prenom ne peut pas depasser 50 lettres!")]
        [Display(Name = "prenom", ResourceType = typeof(RessourceView))]
        public string Prenom { get; set; }

        [Required]
        [Display(Name = "langue", ResourceType = typeof(RessourceView))]
        public string Langue { get; set; }

        [Required]
        public System.Web.Mvc.SelectList SelectionLangue { get; set; }
    }

    public class ChangerMotDePasseViewModel {

        public int Id { get; set; }

        [DataType(DataType.Password)]
        [Required, StringLength(70, MinimumLength = 6, ErrorMessage = "Mot de passe doit etre moins que 70 lettres et plus que 6 lesttres!")]
        [Display(Name = "nouvMotDePass", ResourceType = typeof(RessourceView))]
        public string MDP { get; set; }

        [DataType(DataType.Password)]
        [Required, StringLength(70, MinimumLength = 6, ErrorMessage = "Mot de passe doit etre moins que 70 lettres et plus que 6 lesttres!")]
        [Compare("MDP", ErrorMessage = "les mots de passe ne sont pas memes!")]
        [Display(Name = "MDPConfirmer", ResourceType = typeof(RessourceView))]
        public string MDPConfirmer { get; set; }
    }

    public class ChangerProfilViewModel {

        public int Id { get; set; }

        [Required(ErrorMessage = "Veuillez entrer votre nom de famille!"), StringLength(50, ErrorMessage = "Nom de famille ne peut pas depasser 50 lettres!")]
        [Display(Name = "nomFamille", ResourceType = typeof(RessourceView))]
        public string NomFamille { get; set; }
       
        [Required(ErrorMessage = "Veuillez entrer votre prenom!"), StringLength(50, ErrorMessage = "Prenom ne peut pas depasser 50 lettres!")]
        [Display(Name = "prenom", ResourceType = typeof(RessourceView))]
        public string Prenom { get; set; }

        [Required]
        [Display(Name = "langue", ResourceType = typeof(RessourceView))]
        public string Langue { get; set; }

        public System.Web.Mvc.SelectList SelectionLangue { get; set; }
    }

    public class UniquenessAttribute : ValidationAttribute {

        string Courriel { get; set; }
        static Utilisateurs repo = new Utilisateurs();

        public UniquenessAttribute(string Courriel) {
            this.Courriel = Courriel;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext) {
            var property = validationContext.ObjectType.GetProperty(Courriel);
            if (property == null) {
                return new ValidationResult(
                    string.Format("Unknown property: {0}", Courriel)
                );
            }

 
            if (repo.FindUtilisateurByCourriel(value.ToString()) != null) {
                return new ValidationResult(validationContext.DisplayName + Wiki.Ressource.RessourceView.ERR_dejaExist);
            }
            return null;
        }
    }
}