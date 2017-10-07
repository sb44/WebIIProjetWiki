//// modifié par Arash   pour internasionalisation
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Wiki.Models.Biz {
    public class Article {

        [Key]
        [StringLength(100)]
        public string Titre { get; set; }

        [Required]
        public string Contenu { get; set; }

        public int Revision { get; set; }

        public int IdContributeur { get; set; }

        public DateTime DateModification { get; set; }

        public virtual Utilisateur utilisateur { get; set; }



        //public string Titre { get; set; }
        //public string Contenu { get; set; }
        //public DateTime DateModification { get; set; }
        //public int Revision { get; set; }
        //public int IdContributeur { get; set; }
        //public Utilisateur utilisateur { get; set; } //ajout sb

        //[Required]
        //[Display(Name = "Titre", ResourceType = typeof(RessourceView))]
        //public string Titre { get; set; }

        //[Required]
        //[Display(Name = "Contenu", ResourceType = typeof(RessourceView))]
        //public string Contenu { get; set; }

        //[Display(Name = "DateModification", ResourceType = typeof(RessourceView))]
        //public DateTime DateModification { get; set; }

        //[Display(Name = "Revision", ResourceType = typeof(RessourceView))]
        //public int Revision { get; set; }

        //[Display(Name = "IdContributeur", ResourceType = typeof(RessourceView))]
        //public int IdContributeur { get; set; }

    }


}