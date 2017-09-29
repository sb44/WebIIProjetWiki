/////////////////////////////////// Par Haiqiang Xu   ////////////////////////////////////////////////////  
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Wiki.Ressource;
using System.Resources;

using Wiki.Models.DAL;

namespace Wiki.Models.ViewModels {
    public class ConnexionViewModel {
        [Required(ErrorMessageResourceName = "ERR_courriel", ErrorMessageResourceType = typeof(RessourceView)), StringLength(50,
                ErrorMessageResourceName = "ERR_courriel50", ErrorMessageResourceType = typeof(RessourceView))]
        //[Required(AllowEmptyStrings = false,ErrorMessageResourceName = "", ErrorMessageResourceType = typeof(RessourceView))]
        [RegularExpression("^[a-z0-9_\\+-]+(\\.[a-z0-9_\\+-]+)*@[a-z0-9-]+(\\.[a-z0-9]+)*\\.([a-z]{2,4})$",
                ErrorMessageResourceName = "ERR_courriel_invalide", ErrorMessageResourceType = typeof(RessourceView))]
        [Display(Name = "courriel", ResourceType = typeof(RessourceView))]
        public string Courriel { set; get; }

        [DataType(DataType.Password)]
        [Required(
            ErrorMessageResourceName = "ERR_MDP_entree", ErrorMessageResourceType = typeof(RessourceView))
            , StringLength(70, MinimumLength = 6,
          ErrorMessageResourceName = "ERR_MDP", ErrorMessageResourceType = typeof(RessourceView))]
        [Display(Name = "motDePass", ResourceType = typeof(RessourceView))]
        public string MDP { get; set; }

    }

    public class InscriptionViewModel {
        [Uniqueness("Courriel")]
        [Required(
            ErrorMessageResourceName = "ERR_courriel", ErrorMessageResourceType = typeof(RessourceView)), StringLength(50,
            ErrorMessageResourceName = "ERR_courriel50", ErrorMessageResourceType = typeof(RessourceView))]
        [RegularExpression("^[a-z0-9_\\+-]+(\\.[a-z0-9_\\+-]+)*@[a-z0-9-]+(\\.[a-z0-9]+)*\\.([a-z]{2,4})$", 
            ErrorMessage = "Courriel invalide!")]
        [Display(Name = "courriel", ResourceType = typeof(RessourceView))]
        public string Courriel { set; get; }

        [DataType(DataType.Password)]
        [Required, StringLength(70, MinimumLength = 6,
            ErrorMessageResourceName = "ERR_MDP", ErrorMessageResourceType = typeof(RessourceView))]
        [Display(Name = "motDePass", ResourceType = typeof(RessourceView))]
        public string MDP { get; set; }

        [DataType(DataType.Password)]
        [Required, StringLength(70, MinimumLength = 6,
            ErrorMessageResourceName = "ERR_MDP", ErrorMessageResourceType = typeof(RessourceView) )]
        [Compare("MDP",
            ErrorMessageResourceName = "ERR_MDPCompare", ErrorMessageResourceType = typeof(RessourceView))]
        [Display(Name = "MDPConfirmer", ResourceType = typeof(RessourceView))]
        public string MDPConfirmer { get; set; }

        [Required(
            ErrorMessageResourceName = "ERR_nom", ErrorMessageResourceType = typeof(RessourceView)), StringLength(50,
            ErrorMessageResourceName = "ERR_nom50", ErrorMessageResourceType = typeof(RessourceView))]
        [Display(Name = "nomFamille", ResourceType = typeof(RessourceView))]
        public string NomFamille { get; set; }

        [Required(
            ErrorMessageResourceName = "ERR_prenom", ErrorMessageResourceType = typeof(RessourceView)), StringLength(50,
            ErrorMessageResourceName = "ERR_prenom50", ErrorMessageResourceType = typeof(RessourceView))]
        [Display(Name = "prenom", ResourceType = typeof(RessourceView))]
        public string Prenom { get; set; }

        [Required]
        [Display(Name = "langue", ResourceType = typeof(RessourceView))]
        public string Langue { get; set; }

        [Display(ResourceType =typeof(RessourceView))]
        public System.Web.Mvc.SelectList SelectionLangue { get; set; }
    }

    public class ChangerMotDePasseViewModel {

        public int Id { get; set; }

        [DataType(DataType.Password)]
        [Required, StringLength(70, MinimumLength = 6,
            ErrorMessageResourceName = "ERR_MDP", ErrorMessageResourceType = typeof(RessourceView))]
        [Display(Name = "nouvMotDePass", ResourceType = typeof(RessourceView))]
        public string MDP { get; set; }

        [DataType(DataType.Password)]
        [Required, StringLength(70, MinimumLength = 6,
            ErrorMessageResourceName = "ERR_MDP", ErrorMessageResourceType = typeof(RessourceView))]
        [Compare("MDP",
            ErrorMessageResourceName = "ERR_MDPCompare", ErrorMessageResourceType = typeof(RessourceView))]
        [Display(Name = "MDPConfirmer", ResourceType = typeof(RessourceView))]
        public string MDPConfirmer { get; set; }
    }

    public class ChangerProfilViewModel {

        public int Id { get; set; }

        [Required(
            ErrorMessageResourceName = "ERR_nom", ErrorMessageResourceType = typeof(RessourceView)), StringLength(50,
            ErrorMessageResourceName = "ERR_nom50", ErrorMessageResourceType = typeof(RessourceView))]
        [Display(Name = "nomFamille", ResourceType = typeof(RessourceView))]
        public string NomFamille { get; set; }
       
        [Required(
            ErrorMessageResourceName = "ERR_prenom", ErrorMessageResourceType = typeof(RessourceView)), StringLength(50,
            ErrorMessageResourceName = "ERR_prenom50", ErrorMessageResourceType = typeof(RessourceView) )]
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

 
            if (value != null && repo.FindUtilisateurByCourriel(value.ToString()) != null) {
                return new ValidationResult(validationContext.DisplayName + Wiki.Ressource.RessourceView.ERR_dejaExist);
            }
            return null;
        }
    }
}