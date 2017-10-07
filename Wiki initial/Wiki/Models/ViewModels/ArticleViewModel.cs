using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Wiki.Ressource;

namespace Wiki.Models.ViewModels {
    public class ArticleViewModel {

        [Required]
        [Display(Name = "Titre", ResourceType = typeof(RessourceView))]
        public string Titre { get; set; }

        [Required]
        [Display(Name = "Contenu", ResourceType = typeof(RessourceView))]
        public string Contenu { get; set; }

        [Display(Name = "DateModification", ResourceType = typeof(RessourceView))]
        public DateTime DateModification { get; set; }

        [Display(Name = "Revision", ResourceType = typeof(RessourceView))]
        public int Revision { get; set; }

        [Display(Name = "IdContributeur", ResourceType = typeof(RessourceView))]
        public int IdContributeur { get; set; }


    }
}