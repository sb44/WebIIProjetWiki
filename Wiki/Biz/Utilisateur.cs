using Wiki.Models.Views;
using Wiki.Resources.Models;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Cryptography;
using System.Text;


namespace Wiki.Models.Biz
{
    public class Utilisateur
    {
        public static string[] Langues = { "fr-CA", "en-CA" };

        public int Id { get; set; }

        public string Prenom { get; set; }

        public string NomFamille { get; set; }

        public string Courriel { get; set; }

        public string MDP { get; set; }

        public string Langue { get; set; }

  
    }

}