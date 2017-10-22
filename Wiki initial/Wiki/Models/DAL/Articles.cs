using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using Wiki.Models.Biz.DTO;
using Wiki.Models.Biz.Interfaces;
using Wiki.Models.Biz;
using System.Linq;
using AutoMapper;

namespace Wiki.Models.DAL {
    public class Articles : IArticleRepository {

        //private WikiContext _context;

        //public Articles(WikiContext context) {
        //    this._context = context;
        //}


        // Auteurs: Sasha Bouchard 
        public int Add(ArticleDTO aDto) {
            using (var db = new WikiContext()) {
                int nbRecords = 0; //SqlConnection connexion = null;
                try {

                    Article a = new Article {
                        Titre = aDto.Titre,
                        Contenu = aDto.Contenu,
                        IdContributeur = aDto.IdContributeur,
                        DateModification = DateTime.Now,
                        Revision = 1
                    }; //DateModification et Revision à définir  puisqu'on n'utilise pas la procédure stocké ici
                    db.Articles.Add(a);
                    db.SaveChanges();
                    nbRecords = 1;

                } catch (Exception e) {
                    string Msg = e.Message;
                } finally {
                    //connexion.Close();
                }

                return nbRecords;
            }
        }

        // Auteurs: Sasha Bouchard verifié par Arash Amiri
        public int Update(ArticleDTO aDto) {
            using (var db = new WikiContext()) {
                int nbRecords = 0; //SqlConnection connexion = null;
                try {

                    Article a = new Article {
                        Titre = aDto.Titre,
                        Contenu = aDto.Contenu,
                        IdContributeur = aDto.IdContributeur,
                        DateModification = DateTime.Now,
                        Revision = aDto.Revision + 1
                    };

                    Article aExistant = db.Articles.Find(a.Titre);
                    if (aExistant != null) {
                        db.Entry(aExistant).CurrentValues.SetValues(a);
                        db.Entry(aExistant).State = System.Data.Entity.EntityState.Modified;
                    } else {
                        // on le rajoute puisque probablement supprimé par autre utilisateur presque simultané
                        db.Articles.Add(a);
                    }
                    nbRecords = db.SaveChanges(); // https://msdn.microsoft.com/en-us/library/system.data.entity.dbcontext.savechanges(v=vs.113).aspx

                } catch (Exception e) {
                    string Msg = e.Message;
                } finally {
                    //connexion.Close();
                }

                return nbRecords;
            }
        }


        // Auteurs: Sasha Bouchard verifié par Arash Amiri
        public int Delete(string titre) {
            using (var db = new WikiContext()) {
                int nbRecords = 0; //SqlConnection connexion = null;
                try {

                    Article aExistant = db.Articles.Find(titre);
                    if (aExistant != null) {
                        db.Articles.Remove(aExistant);
                        nbRecords = db.SaveChanges();
                    } else {
                        //déjà supprimé...
                    }

                } catch (Exception e) {
                    string Msg = e.Message;
                } finally {
                    // connexion.Close();
                }
                return nbRecords;
            }

        }

        // Auteurs: Sasha Bouchard verifié par Arash Amiri
        public ArticleDTO Find(string titre) {
            using (var db = new WikiContext()) {
                ArticleDTO monArticle = null; //SqlConnection connexion = null;
                try {

                    Article aExistant = db.Articles.Find(titre);
                    if (aExistant != null) {
                        monArticle = Mapper.Map<Article, ArticleDTO>(aExistant);
                    }

                } catch (Exception e) {
                    string Msg = e.Message;
                } finally {
                    //connexion.Close();
                }

                return monArticle; // retourne null par l'assignation en cas d'erreur
            }
        }


        // Auteurs: Sasha Bouchard
        public IList<ArticleDTO> GetArticles() {
            using (var db = new WikiContext()) {
                IList<ArticleDTO> lstArtDto = new List<ArticleDTO>(); //SqlConnection connexion = null;
                try {

                    var arts = db.Articles.ToList();
                    lstArtDto = Mapper.Map<IList<Article>, IList<ArticleDTO>>(arts);

                } catch (Exception e) {
                    string Msg = e.Message;
                } finally {
                    //connexion.Close();
                }

                return lstArtDto; // retourne une liste vide en cas d'erreur (ou si elle est vide)
            }
        }

        // Auteurs: Sasha Bouchard
        public IList<string> GetTitres() {
            using (var db = new WikiContext()) {
                IList<string> lstTitres = new List<string>(); //SqlConnection connexion = null;

                try {

                    var arts = db.Articles.ToList();

                    foreach (var a in arts)
                        lstTitres.Add(a.Titre);

                } catch (Exception e) {
                    string Msg = e.Message;
                } finally {
                    //connexion.Close();
                }

                return lstTitres; // retourne une liste vide en cas d'erreur (ou si elle est vide)
            }
        }

        private string ConnectionString {
            get { return ConfigurationManager.ConnectionStrings["Wiki"].ConnectionString; }
        }

    }
}




// code avant EF:

//using System;
//using System.Configuration;
//using System.Data;
//using System.Data.SqlClient;
//using System.Collections.Generic;
//using Wiki.Models.Biz.DTO;
//using Wiki.Models.Biz.Interfaces;

//namespace Wiki.Models.DAL {
//    public class Articles : IArticleRepository {

//        // Auteurs: Sasha Bouchard 
//        public int Add(ArticleDTO a) {
//            int nbRecords = 0; SqlConnection connexion = null;
//            try {
//                using (connexion = new SqlConnection(ConnectionString)) {
//                    using (var sqlCmd = new SqlCommand("AddArticle", connexion)) { // Stored procedures
//                        sqlCmd.CommandType = CommandType.StoredProcedure;
//                        sqlCmd.Parameters.Add("@Titre", SqlDbType.NVarChar, 100).Value = a.Titre;
//                        sqlCmd.Parameters.Add("@Contenu", SqlDbType.NVarChar, -1).Value = a.Contenu; //nvarcharmax
//                        sqlCmd.Parameters.Add("@IdContributeur", SqlDbType.Int).Value = a.IdContributeur;

//                        connexion.Open();
//                        nbRecords = sqlCmd.ExecuteNonQuery(); // avec "SET NOCOUNT OFF;" ds la procédure stocké
//                    }
//                }
//            } catch (Exception e) {
//                string Msg = e.Message;
//            } finally {
//                connexion.Close();
//            }

//            return nbRecords;
//        }

//        // Auteurs: Sasha Bouchard verifié par Arash Amiri
//        public int Update(ArticleDTO a) {
//            int nbRecords = 0; SqlConnection connexion = null;
//            try {
//                using (connexion = new SqlConnection(ConnectionString)) {                    
//                    using (var sqlCmd = new SqlCommand("UpdateArticle", connexion)) { // Stored procedures
//                        sqlCmd.CommandType = CommandType.StoredProcedure;
//                        sqlCmd.Parameters.Add("@Titre", SqlDbType.NVarChar, 100).Value = a.Titre;
//                        sqlCmd.Parameters.Add("@Contenu", SqlDbType.NVarChar, -1).Value = a.Contenu; //nvarcharmax
//                        sqlCmd.Parameters.Add("@IdContributeur", SqlDbType.Int).Value = a.IdContributeur;

//                        connexion.Open();
//                        nbRecords = sqlCmd.ExecuteNonQuery(); // avec "SET NOCOUNT OFF;" ds la procédure stocké
//                    }
//                }
//            } catch (Exception e) {
//                string Msg = e.Message;
//            } finally {
//                connexion.Close();
//            }

//            return nbRecords;
//        }


//        // Auteurs: Sasha Bouchard verifié par Arash Amiri
//        public int Delete(string titre) {

//            int nbRecords = 0; SqlConnection connexion = null;
//            try {
//                using (connexion = new SqlConnection(ConnectionString)) {
//                    using (var sqlCmd = new SqlCommand("DeleteArticle", connexion)) { // Stored procedures
//                        sqlCmd.CommandType = CommandType.StoredProcedure;
//                        sqlCmd.Parameters.Add("@Titre", SqlDbType.NVarChar, 100).Value = titre;

//                        connexion.Open();
//                        nbRecords = sqlCmd.ExecuteNonQuery(); // avec "SET NOCOUNT OFF;" ds la procédure stocké
//                    }
//                }
//            } catch (Exception e) {
//                string Msg = e.Message;
//            } finally {
//                connexion.Close();
//            }

//            return nbRecords;
//        }

//        // Auteurs: Sasha Bouchard verifié par Arash Amiri
//        public ArticleDTO Find(string titre) {

//            ArticleDTO monArticle = null; SqlConnection connexion = null;
//            try {
//                using (connexion = new SqlConnection(ConnectionString)) {
//                    using (var sqlCmd = new SqlCommand("FindArticle", connexion)) { // Stored procedures
//                        sqlCmd.CommandType = CommandType.StoredProcedure;
//                        sqlCmd.Parameters.Add("@Titre", SqlDbType.NVarChar, 100).Value = titre;

//                        connexion.Open();
//                        SqlDataReader dr = sqlCmd.ExecuteReader();
//                        dr.Read();

//                        monArticle = new ArticleDTO {
//                                Titre = (string)dr["Titre"],
//                                Contenu = (string)dr["Contenu"],
//                                Revision = (int)dr["Revision"],
//                                IdContributeur = (int)dr["IdContributeur"],
//                                DateModification = (DateTime)dr["DateModification"],
//                            };


//                        dr.Close();
//                    }
//                }
//            } catch (Exception e) {
//                    string Msg = e.Message;
//            } finally {
//                    connexion.Close();
//            }

//            return monArticle; // retourne null par l'assignation en cas d'erreur
//        }


//        // Auteurs: Sasha Bouchard
//        public IList<ArticleDTO> GetArticles() {
//            List<ArticleDTO> lstArticles = new List<ArticleDTO>(); SqlConnection connexion = null;
//            try {
//                using (connexion = new SqlConnection(ConnectionString)) {
//                    using (var sqlCmd = new SqlCommand("GetArticles", connexion)) { // Stored procedures
//                        sqlCmd.CommandType = CommandType.StoredProcedure;

//                        connexion.Open();
//                        SqlDataReader dr = sqlCmd.ExecuteReader();

//                        while (dr.Read()) {
//                            lstArticles.Add(new ArticleDTO {
//                                Titre = (string)dr["Titre"],
//                                Contenu = (string)dr["Contenu"],
//                                Revision = (int)dr["Revision"],
//                                IdContributeur = (int)dr["IdContributeur"],
//                                DateModification = (DateTime)dr["DateModification"]
//                            });
//                        }
//                        dr.Close();
//                    }
//                }
//            } catch (Exception e) {
//                string Msg = e.Message;
//            } finally {
//                connexion.Close();
//            }

//            return lstArticles; // retourne une liste vide en cas d'erreur (ou si elle est vide)
//        }

//        // Auteurs: Sasha Bouchard
//        public IList<string> GetTitres() {
//            IList<string> lstTitres = new List<string>(); SqlConnection connexion = null;
//            try {
//                using (connexion = new SqlConnection(ConnectionString)) {
//                    using (var sqlCmd = new SqlCommand("GetTitresArticles", connexion)) { // Stored procedures
//                        sqlCmd.CommandType = CommandType.StoredProcedure;

//                        connexion.Open();
//                        SqlDataReader dr = sqlCmd.ExecuteReader();
//                        while (dr.Read()) {
//                            lstTitres.Add((string)dr["Titre"]);
//                        }
//                        dr.Close();
//                    }
//                }
//            } catch (Exception e) {
//                string Msg = e.Message;
//            } finally {
//                connexion.Close();
//            }

//            return lstTitres; // retourne une liste vide en cas d'erreur (ou si elle est vide)
//        }

//        private string ConnectionString {
//            get { return ConfigurationManager.ConnectionStrings["Wiki"].ConnectionString; }
//        }

//    }
//}