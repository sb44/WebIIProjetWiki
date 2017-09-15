using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Wiki.Models.Biz.DTO {
    public class ArticleDTO {

        public string Titre { get; set; }
        public string Contenu { get; set; }
        public DateTime DateModification { get; set; }
        public int Revision { get; set; }
        public int IdContributeur { get; set; }

    }
}