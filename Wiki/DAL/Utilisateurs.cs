using System;
using System.Configuration;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Wiki.Models.DAL;
using Wiki.Models.Biz;

namespace Wiki.Models.DAL
{
    public class Utilisateurs 
    {

         public void Add(Utilisateur u)
        {
            using (SqlConnection connexion = new SqlConnection(ConnectionString))
            {
                connexion.Open();

                //Création d'une commande
                SqlCommand commande = new SqlCommand("AddUtilisateur", connexion);
                commande.CommandType = CommandType.StoredProcedure;
                commande.Parameters.Add("mdp", SqlDbType.NVarChar).SqlValue = u.MDP;
                commande.Parameters.Add("prenom", SqlDbType.NVarChar).SqlValue = u.Prenom;
                commande.Parameters.Add("nomFamille", SqlDbType.NVarChar).SqlValue = u.NomFamille;
                commande.Parameters.Add("courriel", SqlDbType.NVarChar).SqlValue = u.Courriel;
                commande.Parameters.Add("langue", SqlDbType.NVarChar).SqlValue = u.Langue;

                try
                {
                    u.Id = Convert.ToInt32(commande.ExecuteScalar());
                }
                catch (Exception e)
                {
                    throw new Exception("Erreur d'ajout", e);
                }
            }
        }

        public void Update(Utilisateur u)
        {
            using (SqlConnection connexion = new SqlConnection(ConnectionString))
            {
                connexion.Open();

                //Création d'une commande
                SqlCommand commande = new SqlCommand("UpdateUtilisateur", connexion);
                commande.CommandType = CommandType.StoredProcedure;
                commande.Parameters.Add("prenom", SqlDbType.NVarChar).SqlValue = u.Prenom;
                commande.Parameters.Add("nomFamille", SqlDbType.NVarChar).SqlValue = u.NomFamille;
                commande.Parameters.Add("courriel", SqlDbType.NVarChar).SqlValue = u.Courriel;
                commande.Parameters.Add("langue", SqlDbType.NVarChar).SqlValue = u.Langue;

                try
                {
                    commande.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    throw new Exception("Erreur de modification", e);
                }
            }

        }

        public void Update(string courriel, string ancienMdP, string nouveauMdP)
        {
             using (SqlConnection connexion = new SqlConnection(ConnectionString))
            {
                connexion.Open();

                //Création d'une commande
                SqlCommand commande = new SqlCommand("UpdateMotDePasse", connexion);
                commande.CommandType = CommandType.StoredProcedure;
                commande.Parameters.Add("courriel", SqlDbType.NVarChar).SqlValue = courriel;
                commande.Parameters.Add("ancienMdp", SqlDbType.NVarChar).SqlValue = ancienMdP;
                commande.Parameters.Add("nouveauMdp", SqlDbType.NVarChar).SqlValue = nouveauMdP;

                try
                {
                    if (commande.ExecuteNonQuery() != 1)
                        throw new Exception("Erreur de modification");
                }
                catch (Exception e)
                {
                    throw new Exception("Erreur de modification", e);
                }
            }

        }

        public void Remove(int id)
        {
            using (SqlConnection connexion = new SqlConnection(ConnectionString))
            {
                connexion.Open();

                //Création d'une commande
                SqlCommand commande = new SqlCommand("DeleteUtilisateur", connexion);
                commande.CommandType = CommandType.StoredProcedure;
                commande.Parameters.Add("Id", SqlDbType.Int).SqlValue = id;

                try
                {
                    commande.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    throw new Exception("Erreur de suppression", e);
                }
            }

        }


        public Utilisateur FindByCourriel(string courriel)
        {
            Utilisateur u = null;
            using (SqlConnection connexion = new SqlConnection(ConnectionString))
            {
                connexion.Open();

                //Création d'une commande
                SqlCommand commande = new SqlCommand("FindUtilisateurByCourriel", connexion);
                commande.CommandType = CommandType.StoredProcedure;
                commande.Parameters.Add("Courriel", SqlDbType.NVarChar).SqlValue = courriel;

                //Traitement du DataReader
                SqlDataReader dr = commande.ExecuteReader();
                dr.Read();
                u = ExtraireUtilisateur(dr);
                dr.Close();
            }

            return u;
        }


        public Utilisateur FindById(int id)
        {
            Utilisateur u = null;
            using (SqlConnection connexion = new SqlConnection(ConnectionString))
            {
                connexion.Open();

                //Création d'une commande
                SqlCommand commande = new SqlCommand("FindUtilisateurById", connexion);
                commande.CommandType = CommandType.StoredProcedure;
                commande.Parameters.Add("Id", SqlDbType.Int).SqlValue = id;

                //Traitement du DataReader
                SqlDataReader dr = commande.ExecuteReader();
                dr.Read();
                u = ExtraireUtilisateur(dr);
                dr.Close();
            }

            return u;
        }

        private Utilisateur ExtraireUtilisateur(IDataReader dr)
        {
            Utilisateur u = new Utilisateur();
            u.Id = (int)dr["Id"];
            u.Courriel = (string)dr["Courriel"];
            u.MDP = (string)dr["MDP"];
            u.NomFamille = (string)dr["nomFamille"];
            u.Prenom = (string)dr["prenom"];
            u.Courriel = (string)dr["courriel"];
            u.Langue = (string)dr["langue"];
            return u;
        }

        private string ConnectionString
        {
            get { return ConfigurationManager.ConnectionStrings["Wiki"].ConnectionString; }
        }

    }
}