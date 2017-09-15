using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Wiki.Models.Biz.DTO {
    public class UtilisateurDTO {

        public int Id { get; set; }
        public string Courriel { get; set; }
        public string MDP { get; set; }
        public string NomFamille { get; set; }
        public string Prenom { get; set; }
        public string Langue { get; set; }

    }
}