using System;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Web;
using System.Collections.Generic;
using Wiki.Models.DAL;
using Wiki.Models.Biz;

namespace Wiki.Models.DAL {
    public class Articles {

        // Auteurs: Sasha Bouchard 
        public int Add(Article a) {
            int nbRecords = 0; SqlConnection connexion = null;
            try {
                using (connexion = new SqlConnection(ConnectionString)) {
                    using (var sqlCmd = new SqlCommand("AddArticle", connexion)) { // Stored procedures
                        sqlCmd.CommandType = CommandType.StoredProcedure;
                        sqlCmd.Parameters.Add("@Titre", SqlDbType.NVarChar, 100).Value = a.Titre;
                        sqlCmd.Parameters.Add("@Contenu", SqlDbType.NVarChar, -1).Value = a.Contenu; //nvarcharmax
                        sqlCmd.Parameters.Add("@IdContributeur", SqlDbType.Int).Value = a.IdContributeur;

                        connexion.Open();
                        nbRecords = sqlCmd.ExecuteNonQuery(); // avec "SET NOCOUNT OFF;" ds la procédure stocké
                    }
                }
            } catch (Exception e) {
                string Msg = e.Message;
            } finally {
                connexion.Close();
            }

            return nbRecords;
        }

        // Auteurs: Sasha Bouchard verifié par Arash Amiri
        public int Update(Article a) {
            int nbRecords = 0; SqlConnection connexion = null;
            try {
                using (connexion = new SqlConnection(ConnectionString)) {                    
                    using (var sqlCmd = new SqlCommand("UpdateArticle", connexion)) { // Stored procedures
                        sqlCmd.CommandType = CommandType.StoredProcedure;
                        sqlCmd.Parameters.Add("@Titre", SqlDbType.NVarChar, 100).Value = a.Titre;
                        sqlCmd.Parameters.Add("@Contenu", SqlDbType.NVarChar, -1).Value = a.Contenu; //nvarcharmax
                        sqlCmd.Parameters.Add("@IdContributeur", SqlDbType.Int).Value = a.IdContributeur;

                        connexion.Open();
                        nbRecords = sqlCmd.ExecuteNonQuery(); // avec "SET NOCOUNT OFF;" ds la procédure stocké
                    }
                }
            } catch (Exception e) {
                string Msg = e.Message;
            } finally {
                connexion.Close();
            }

            return nbRecords;
        }


        // Auteurs: Sasha Bouchard verifié par Arash Amiri
        public int Delete(string titre) {

            int nbRecords = 0; SqlConnection connexion = null;
            try {
                using (connexion = new SqlConnection(ConnectionString)) {
                    using (var sqlCmd = new SqlCommand("DeleteArticle", connexion)) { // Stored procedures
                        sqlCmd.CommandType = CommandType.StoredProcedure;
                        sqlCmd.Parameters.Add("@Titre", SqlDbType.NVarChar, 100).Value = titre;

                        connexion.Open();
                        nbRecords = sqlCmd.ExecuteNonQuery(); // avec "SET NOCOUNT OFF;" ds la procédure stocké
                    }
                }
            } catch (Exception e) {
                string Msg = e.Message;
            } finally {
                connexion.Close();
            }

            return nbRecords;
        }

        // Auteurs: Sasha Bouchard verifié par Arash Amiri
        public Article Find(string titre) {

            Article monArticle = null; SqlConnection connexion = null;
            try {
                using (connexion = new SqlConnection(ConnectionString)) {
                    using (var sqlCmd = new SqlCommand("FindArticle", connexion)) { // Stored procedures
                        sqlCmd.CommandType = CommandType.StoredProcedure;
                        sqlCmd.Parameters.Add("@Titre", SqlDbType.NVarChar, 100).Value = titre;

                        connexion.Open();
                        SqlDataReader dr = sqlCmd.ExecuteReader();
                        dr.Read();
                        
                        monArticle = new Article {
                                Titre = (string)dr["Titre"],
                                Contenu = (string)dr["Contenu"],
                                Revision = (int)dr["Revision"],
                                IdContributeur = (int)dr["IdContributeur"],
                                DateModification = (DateTime)dr["DateModification"],
                            };
                        

                        dr.Close();
                    }
                }
            } catch (Exception e) {
                    string Msg = e.Message;
            } finally {
                    connexion.Close();
            }
            
            return monArticle; // retourne null par l'assignation en cas d'erreur
        }


        // Auteurs: Sasha Bouchard
        public IList<Article> GetArticles() {
            List<Article> lstArticles = new List<Article>(); SqlConnection connexion = null;
            try {
                using (connexion = new SqlConnection(ConnectionString)) {
                    using (var sqlCmd = new SqlCommand("GetArticles", connexion)) { // Stored procedures
                        sqlCmd.CommandType = CommandType.StoredProcedure;

                        connexion.Open();
                        SqlDataReader dr = sqlCmd.ExecuteReader();

                        while (dr.Read()) {
                            lstArticles.Add(new Article {
                                Titre = (string)dr["Titre"],
                                Contenu = (string)dr["Contenu"],
                                Revision = (int)dr["Revision"],
                                IdContributeur = (int)dr["IdContributeur"],
                                DateModification = (DateTime)dr["DateModification"]
                            });
                        }
                        dr.Close();
                    }
                }
            } catch (Exception e) {
                string Msg = e.Message;
            } finally {
                connexion.Close();
            }

            return lstArticles; // retourne une liste vide en cas d'erreur (ou si elle est vide)
        }

        // Auteurs: Sasha Bouchard
        public IList<string> GetTitres() {
            IList<string> lstTitres = new List<string>(); SqlConnection connexion = null;
            try {
                using (connexion = new SqlConnection(ConnectionString)) {
                    using (var sqlCmd = new SqlCommand("GetTitresArticles", connexion)) { // Stored procedures
                        sqlCmd.CommandType = CommandType.StoredProcedure;

                        connexion.Open();
                        SqlDataReader dr = sqlCmd.ExecuteReader();
                        while (dr.Read()) {
                            lstTitres.Add((string)dr["Titre"]);
                        }
                        dr.Close();
                    }
                }
            } catch (Exception e) {
                string Msg = e.Message;
            } finally {
                connexion.Close();
            }

            return lstTitres; // retourne une liste vide en cas d'erreur (ou si elle est vide)
        }

        private string ConnectionString {
            get { return ConfigurationManager.ConnectionStrings["Wiki"].ConnectionString; }
        }

    }
}