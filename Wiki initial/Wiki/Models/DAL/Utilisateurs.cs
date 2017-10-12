////////////////////////////////// Par Haiqiang XU  /////////////////////////////////////////////////////////
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Wiki.Models.Biz;
using Wiki.Models.Biz.DTO;
using Wiki.Models.Biz.Interfaces;

namespace Wiki.Models.DAL {
    public class Utilisateurs : IUtilisateurRepository {

        private string ConnectionString = ConfigurationManager.ConnectionStrings["Wiki"].ConnectionString;


        // ajout sasha
        public IList<UtilisateurDTO> GetUtilisateurs() {
            throw new NotImplementedException();
        }


        public int AddUtilisateur(string Courriel, string MDP, string Prenom, string NomDeFamille, string Langue) {
            int OK = 0;
            using (var db = new WikiContext()) {
                try {
                /* connexion.Open();
                sqlCmd.ExecuteNonQuery(); */
                Utilisateur u = new Utilisateur { Courriel = Courriel,
                                                MDP = PasswordHash.CreateHash(MDP),
                                                Prenom = Prenom,
                                                NomFamille = NomDeFamille,
                                                Langue = Langue};

                    db.Utilisateurs.Add(u);
                    db.SaveChanges();

                    OK = 1;

            } catch (Exception e) {
                string Msg = e.Message;
                OK = -1;
            } finally {
                //connexion.Close();
            }
            return OK;
            }
        }


        public UtilisateurDTO FindUtilisateurByCourriel(string Courriel) {
            UtilisateurDTO u = new UtilisateurDTO();
            SqlConnection connexion = new SqlConnection(ConnectionString);
            SqlCommand sqlCmd = new SqlCommand("FindUtilisateurByCourriel", connexion);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.Add("@Courriel", SqlDbType.NVarChar).Value = Courriel;

            try {

                connexion.Open();
                SqlDataReader dataReader = sqlCmd.ExecuteReader();

                while (dataReader.Read()) {
                    u.Id = (int)dataReader["Id"];
                    u.Courriel = (string)dataReader["Courriel"]; // mod sb
                    u.NomFamille = (string)dataReader["NomFamille"];
                    u.Prenom = (string)dataReader["Prenom"];
                    u.Langue = (string)dataReader["Langue"];
                    u.MDP = (string)dataReader["MDP"];
                }

            } catch (Exception e) {
                string Msg = e.Message;
            } finally {
               connexion.Close();
            }

            if (u.Courriel != null)
                return u;
            else
                return null;
        }

        public UtilisateurDTO FindUtilisateurById(int Id) {
            UtilisateurDTO u = new UtilisateurDTO();
            SqlConnection connexion = new SqlConnection(ConnectionString);
            SqlCommand sqlCmd = new SqlCommand("FindUtilisateurById", connexion);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.Add("@Id", SqlDbType.Int).Value = Id;

            try {
                connexion.Open();
                SqlDataReader dataReader = sqlCmd.ExecuteReader();

                while (dataReader.Read()) {
                    u.Id = Id;
                    u.Courriel = (string)dataReader["Courriel"];
                    u.NomFamille = (string)dataReader["NomFamille"];
                    u.Prenom = (string)dataReader["Prenom"];
                    u.Langue = (string)dataReader["Langue"];
                    u.MDP = (string)dataReader["MDP"];

                    return u;
                }
            } catch (Exception e) {
                string Msg = e.Message;
            } finally {
                connexion.Close();
            }
            return null;
        }

        public int UpdateMotDePasse(int Id, string NouveauMDP) {
            int OK = 0;
            SqlConnection connexion = new SqlConnection(ConnectionString);
            SqlCommand sqlCmd = new SqlCommand("UpdateMotDePasse", connexion);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.Add("@Id", SqlDbType.Int).Value = Id;
            sqlCmd.Parameters.Add("@nouveauMDP", SqlDbType.NVarChar).Value = PasswordHash.CreateHash(NouveauMDP);
            try {
                connexion.Open();
                sqlCmd.ExecuteNonQuery();
                OK = 1;
            } catch (Exception e) {
                string Msg = e.Message;
                OK = -1;
            } finally {
                connexion.Close();
            }
            return OK;
        }

        public int UpdateUtilisateur(int Id, string NomDeFamille, string Prenom, string Langue) {
            int OK = 0;
            SqlConnection connexion = new SqlConnection(ConnectionString);
            SqlCommand sqlCmd = new SqlCommand("UpdateUtilisateur", connexion);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.Add("@Id", SqlDbType.Int).Value = Id;
            sqlCmd.Parameters.Add("@Prenom", SqlDbType.NVarChar).Value = Prenom;
            sqlCmd.Parameters.Add("@NomFamille", SqlDbType.NVarChar).Value = NomDeFamille;
            sqlCmd.Parameters.Add("@Langue", SqlDbType.NVarChar).Value = Langue;
            try {
                connexion.Open();
                sqlCmd.ExecuteNonQuery();
                OK = 1;
            } catch (Exception e) {
                string Msg = e.Message;
                OK = -1;
            } finally {
                connexion.Close();
            }
            return OK;
        }
    }
}


// Code avant EF:

//////////////////////////////////// Par Haiqiang XU  /////////////////////////////////////////////////////////
//using System;
//using System.Collections.Generic;
//using System.Configuration;
//using System.Data;
//using System.Data.SqlClient;
//using System.Linq;
//using System.Web;
//using Wiki.Models.Biz;
//using Wiki.Models.Biz.DTO;
//using Wiki.Models.Biz.Interfaces;

//namespace Wiki.Models.DAL {
//    public class Utilisateurs : IUtilisateurRepository {

//        private string ConnectionString = ConfigurationManager.ConnectionStrings["Wiki"].ConnectionString;

//        // ajout sasha
//        public IList<UtilisateurDTO> GetUtilisateurs() {
//            throw new NotImplementedException();
//        }


//        public int AddUtilisateur(string Courriel, string MDP, string Prenom, string NomDeFamille, string Langue) {
//            int OK = 0;

//            string a = PasswordHash.CreateHash(MDP);
//            SqlConnection connexion = new SqlConnection(ConnectionString);
//            SqlCommand sqlCmd = new SqlCommand("AddUtilisateur", connexion);
//            sqlCmd.CommandType = CommandType.StoredProcedure;
//            sqlCmd.Parameters.Add("@Courriel", SqlDbType.NVarChar).Value = Courriel;
//            sqlCmd.Parameters.Add("@MDP", SqlDbType.NVarChar).Value = PasswordHash.CreateHash(MDP);
//            sqlCmd.Parameters.Add("@Prenom", SqlDbType.NVarChar).Value = Prenom;
//            sqlCmd.Parameters.Add("@NomFamille", SqlDbType.NVarChar).Value = NomDeFamille;
//            sqlCmd.Parameters.Add("@Langue", SqlDbType.NVarChar).Value = Langue;
//            try {
//                connexion.Open();
//                sqlCmd.ExecuteNonQuery();
//                OK = 1;
//            } catch (Exception e) {
//                string Msg = e.Message;
//                OK = -1;
//            } finally {
//                connexion.Close();
//            }
//            return OK;
//        }


//        public UtilisateurDTO FindUtilisateurByCourriel(string Courriel) {
//            UtilisateurDTO  u = new UtilisateurDTO();
//            SqlConnection connexion = new SqlConnection(ConnectionString);
//            SqlCommand sqlCmd = new SqlCommand("FindUtilisateurByCourriel", connexion);
//            sqlCmd.CommandType = CommandType.StoredProcedure;
//            sqlCmd.Parameters.Add("@Courriel", SqlDbType.NVarChar).Value = Courriel;

//            try {
//                connexion.Open();
//                SqlDataReader dataReader = sqlCmd.ExecuteReader();

//                while (dataReader.Read()) {
//                    u.Id = (int)dataReader["Id"];
//                    u.Courriel = Courriel;
//                    u.NomFamille = (string)dataReader["NomFamille"];
//                    u.Prenom = (string)dataReader["Prenom"];
//                    u.Langue = (string)dataReader["Langue"];
//                    u.MDP = (string)dataReader["MDP"];

//                    return u;
//                }
//            } catch (Exception e) {
//                string Msg = e.Message;
//            } finally {
//                connexion.Close();
//            }
//            return null;
//        }

//        public UtilisateurDTO FindUtilisateurById(int Id) {
//            UtilisateurDTO u = new UtilisateurDTO();
//            SqlConnection connexion = new SqlConnection(ConnectionString);
//            SqlCommand sqlCmd = new SqlCommand("FindUtilisateurById", connexion);
//            sqlCmd.CommandType = CommandType.StoredProcedure;
//            sqlCmd.Parameters.Add("@Id", SqlDbType.Int).Value = Id;

//            try {
//                connexion.Open();
//                SqlDataReader dataReader = sqlCmd.ExecuteReader();

//                while (dataReader.Read()) {
//                    u.Id = Id;
//                    u.Courriel = (string)dataReader["Courriel"];
//                    u.NomFamille = (string)dataReader["NomFamille"];
//                    u.Prenom = (string)dataReader["Prenom"];
//                    u.Langue = (string)dataReader["Langue"];
//                    u.MDP = (string)dataReader["MDP"];

//                    return u;
//                }
//            } catch (Exception e) {
//                string Msg = e.Message;
//            } finally {
//                connexion.Close();
//            }
//            return null;
//        }

//        public int UpdateMotDePasse(int Id, string NouveauMDP) {
//            int OK = 0;
//            SqlConnection connexion = new SqlConnection(ConnectionString);
//            SqlCommand sqlCmd = new SqlCommand("UpdateMotDePasse", connexion);
//            sqlCmd.CommandType = CommandType.StoredProcedure;
//            sqlCmd.Parameters.Add("@Id", SqlDbType.Int).Value = Id;
//            sqlCmd.Parameters.Add("@nouveauMDP", SqlDbType.NVarChar).Value = PasswordHash.CreateHash(NouveauMDP);
//            try {
//                connexion.Open();
//                sqlCmd.ExecuteNonQuery();
//                OK = 1;
//            } catch (Exception e) {
//                string Msg = e.Message;
//                OK = -1;
//            } finally {
//                connexion.Close();
//            }
//            return OK;
//        }

//        public int UpdateUtilisateur(int Id, string NomDeFamille, string Prenom, string Langue) {
//            int OK = 0;
//            SqlConnection connexion = new SqlConnection(ConnectionString);
//            SqlCommand sqlCmd = new SqlCommand("UpdateUtilisateur", connexion);
//            sqlCmd.CommandType = CommandType.StoredProcedure;
//            sqlCmd.Parameters.Add("@Id", SqlDbType.Int).Value = Id;
//            sqlCmd.Parameters.Add("@Prenom", SqlDbType.NVarChar).Value = Prenom;
//            sqlCmd.Parameters.Add("@NomFamille", SqlDbType.NVarChar).Value = NomDeFamille;
//            sqlCmd.Parameters.Add("@Langue", SqlDbType.NVarChar).Value = Langue;
//            try {
//                connexion.Open();
//                sqlCmd.ExecuteNonQuery();
//                OK = 1;
//            } catch (Exception e) {
//                string Msg = e.Message;
//                OK = -1;
//            } finally {
//                connexion.Close();
//            }
//            return OK;
//        }
//    }
//}