///////////////////////// Par Haiqiang XU  ///////////////////////////////////////////////////////
using System;
using System.Collections.Generic;
using Wiki.Models.Biz.DTO;
using Wiki.Models.Biz.Interfaces;

namespace Wiki.Models.Biz {
    public class UtilisateurManager {
        private readonly IUtilisateurRepository _utilisateurRepository; // readonly: seulement le ctor peut modifier sa valeur
        
        //ajout sasha
        public IList<Utilisateur> lstUtilisateur { get; set; }

        public UtilisateurManager(IUtilisateurRepository utilisateurRepository) {
            this._utilisateurRepository = utilisateurRepository; 
            //ajout sasha
            this.GetUtilisateurs();
        }

        //ajout sasha
        public IList<UtilisateurDTO> GetUtilisateurs() {
            try {
                return _utilisateurRepository.GetUtilisateurs();

            } catch (System.Exception e) {
                string Msg = e.Message;
                return null;
            }
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
                return PasswordHash.ValidatePassword(MDP, u.MDP);
            }  catch (System.Exception e) {
                string Msg = e.Message;
                return false;
            }
} 
    }
}