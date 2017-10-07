using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Wiki.Models.Biz {
    public class Utilisateur {

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Utilisateur() {
            lstArticle = new HashSet<Article>();
        }

        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Courriel { get; set; }

        [Required]
        [StringLength(70)]
        public string MDP { get; set; }

        [Required]
        [StringLength(50)]
        public string NomFamille { get; set; }

        [Required]
        [StringLength(50)]
        public string Prenom { get; set; }

        [Required]
        [StringLength(5)]
        public string Langue { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Article> lstArticle { get; set; }

        //public int Id { get; set; }
        //public string Courriel { get; set; }
        //public string MDP { get; set; }
        //public string NomFamille { get; set; }
        //public string Prenom { get; set; }
        //public string Langue { get; set; }

        //protected IList<Article> lstArticle { get; set; }

        //public Utilisateur() {

        //}
    }
}