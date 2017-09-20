///////////////////////// Par Haiqiang XU  ///////////////////////////////////////////////////////
using Wiki.Models.Biz.DTO;
using Wiki.Models.Biz.Interfaces;

namespace Wiki.Models.Biz {
    public class UtilisateurManager {
        private readonly IUtilisateurRepository _utilisateurRepository; // readonly: seulement le ctor peut modifier sa valeur

        public UtilisateurManager(IUtilisateurRepository utilisateurRepository) {
            this._utilisateurRepository = utilisateurRepository;

        }

        public int AddUtilisateur(string Courriel, string MDP, string Prenom, string NomDeFamille, string Langue) {
            try {
                return _utilisateurRepository.AddUtilisateur(Courriel, MDP, Prenom, NomDeFamille, Langue);
            } catch (System.Exception e) {
                string Msg = e.Message;
                return -1;
            }
        }

        public int UpdateUtilisateur(string Prenom, string NomDeFamille, int Id, string Langue) {
            try {
                return _utilisateurRepository.UpdateUtilisateur(Id, NomDeFamille, Prenom, Langue);
            } catch (System.Exception e) {
                string Msg = e.Message;
                return -1;
            }
        }

        public int UpdateMotDePasse(int Id, string NouveauMDP) {
            try {
                return _utilisateurRepository.UpdateMotDePasse(Id, NouveauMDP);
            } catch (System.Exception e) {
                string Msg = e.Message;
                return -1;
            }
        }

        public UtilisateurDTO FindUtilisateurByCourriel(string Courriel) {
            try {
                return _utilisateurRepository.FindUtilisateurByCourriel(Courriel);
            } catch (System.Exception e) {
                string Msg = e.Message;
                return null;
            }
        }

        public UtilisateurDTO FindUtilisateurById(int Id) {
            try {
                return _utilisateurRepository.FindUtilisateurById(Id);
            } catch (System.Exception e) {
                string Msg = e.Message;
                return null;
            }
        }

        public bool Authentifier(string Courriel, string MDP) {
            UtilisateurDTO u = new UtilisateurDTO();
            try {
                u = _utilisateurRepository.FindUtilisateurByCourriel(Courriel);
 //               string hash = PasswordHash.CreateHash(MDP);
                bool a =  MDP == u.MDP ? true : false;
                return a;
            }  catch (System.Exception e) {
                string Msg = e.Message;
                return false;
            }
} 
    }
}