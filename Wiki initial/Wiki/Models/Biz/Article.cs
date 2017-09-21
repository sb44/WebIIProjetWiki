//// modifié par Arash   pour internasionalisation
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Wiki.Ressource;


namespace Wiki.Models.Biz {
    public class Article {

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