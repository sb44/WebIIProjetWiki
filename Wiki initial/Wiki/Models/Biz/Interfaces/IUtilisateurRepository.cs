using System.Collections.Generic;
using Wiki.Models.Biz.DTO;

namespace Wiki.Models.Biz.Interfaces {
    public interface IUtilisateurRepository {
        /////////////////////////// Ajout par Haiqiang Xu
        int AddUtilisateur(string Courriel, string MDP, string Prenom, string NomdeFamille, string Langue);
        int UpdateUtilisateur(int Id, string NomDeFamille, string Prenom, string Langue);
        int UpdateMotDePasse(int Id, string NouveauMDP);
        UtilisateurDTO FindUtilisateurById(int Id);
        UtilisateurDTO FindUtilisateurByCourriel(string Courriel);
        IList<UtilisateurDTO> GetUtilisateurs();
    }
}