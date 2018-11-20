using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for SQL
/// </summary>
static public class SQL
{
    private static SqlConnection dbConn;
    public static void Connection()
    {
        dbConn = new SqlConnection();
        dbConn.ConnectionString = ConfigurationManager.AppSettings["strConnexionDreamTeam"];
        dbConn.Open();
    }
    public static List<EntiteFilm> FindAllDVD()
    {
        List<EntiteFilm> lstFilms = new List<EntiteFilm>();
        String strReq = "SELECT Films.NoFilm, Films.AnneeSortie, Categories.[Description], Formats.[Description], Films.DateMAJ, Utilisateurs.NomUtilisateur, " +
           "Films.[Resume], Films.DureeMinutes, Films.FilmOriginal, Films.ImagePochette, Films.NbDisques, Films.TitreFrancais, Films.TitreOriginal, " +
           "Films.VersionEtendue, Realisateurs.Nom, Producteurs.Nom, Films.XTra " +
           "FROM Films " +
           "LEFT JOIN Categories ON Films.Categorie = Categories.NoCategorie " +
           "LEFT JOIN Formats ON Films.Format = Formats.NoFormat " +
           "LEFT JOIN Utilisateurs ON Films.NoUtilisateurMAJ = Utilisateurs.NoUtilisateur " +
           "LEFT JOIN Realisateurs ON Films.NoRealisateur = Realisateurs.NoRealisateur " +
           "LEFT JOIN Producteurs ON Films.NoProducteur = Producteurs.NoProducteur;";
        SqlCommand cmdDDL = new SqlCommand(strReq, dbConn);
        SqlDataReader drDDL = cmdDDL.ExecuteReader();
        while (drDDL.Read())
        {
            lstFilms.Add(new EntiteFilm((int)drDDL[0],
               (drDDL[1].ToString() == "") ? -1 : (int)drDDL[1],
               (drDDL[2].ToString() == "") ? "" : (string)drDDL[2],
               (drDDL[3].ToString() == "") ? "" : (string)drDDL[3],
               (DateTime)drDDL[4],
               (string)drDDL[5],
               (drDDL[6].ToString() == "") ? "" : (string)drDDL[6],
               (drDDL[7].ToString() == "") ? -1 : (int)drDDL[7],
               (drDDL[8].ToString() == "") ? false : (bool)drDDL[8],
               (drDDL[9].ToString() == "") ? "../Static/images/pas-de-vignette.jpeg" : "../Static/images/" + (string)drDDL[9],
               (drDDL[10].ToString() == "") ? -1 : (int)drDDL[10],
               (string)drDDL[11],
               (drDDL[12].ToString() == "") ? "" : (string)drDDL[12],
               (drDDL[13].ToString() == "") ? false : (bool)drDDL[13],
               (drDDL[14].ToString() == "") ? "" : (string)drDDL[14],
               (drDDL[15].ToString() == "") ? "" : (string)drDDL[15],
               (drDDL[16].ToString() == "") ? "" : (string)drDDL[16]));
        }
        drDDL.Close();
        return lstFilms;
    }

    //Cette fonction permet de retourner une liste de producteur 
    public static List<EntiteProducteur> FindAllProducteur()
    {
        List<EntiteProducteur> lstProducteurs = new List<EntiteProducteur>();
        String strRequete = "select * from Producteurs";
        SqlCommand cmdDDL = new SqlCommand(strRequete, dbConn);
        SqlDataReader drDDL = cmdDDL.ExecuteReader();
        while (drDDL.Read())
        {
            lstProducteurs.Add(new EntiteProducteur((int)drDDL[0], (string)drDDL[1]));
        }

        drDDL.Close();
        return lstProducteurs;
    }

    //Cette fonction permet de retourner une liste de Réalisateur 
    public static List<EntiteRealisateur> FindAllRealisateur()
    {
        List<EntiteRealisateur> lstRealisateurs = new List<EntiteRealisateur>();
        String strRequete = "select * from Realisateurs";
        SqlCommand cmdDDL = new SqlCommand(strRequete, dbConn);
        SqlDataReader drDDL = cmdDDL.ExecuteReader();
        while (drDDL.Read())
        {
            lstRealisateurs.Add(new EntiteRealisateur((int)drDDL[0], (string)drDDL[1]));
        }

        drDDL.Close();
        return lstRealisateurs;
    }

    /// <summary>
    /// Cette fonction retourne une lise de format
    /// </summary>
    /// <returns> lstFormats</returns>
    
    public static List<EntiteFormat> FindAllFormat()
    {
        List<EntiteFormat> lstFormats = new List<EntiteFormat>();
        String strRequete = "select * from Formats";
        SqlCommand cmdDDL = new SqlCommand(strRequete, dbConn);
        SqlDataReader drDDL = cmdDDL.ExecuteReader();
        while (drDDL.Read())
        {
            lstFormats.Add(new EntiteFormat((int)drDDL[0], (string)drDDL[1]));
        }

        drDDL.Close();
        return lstFormats;
    }

    /// <summary>
    /// Cette fonction retourne une liste de catégories
    /// </summary>
    /// <returns> lstCategories</returns>

    public static List<EntiteCategorie> FindAllCategorie()
    {
        List<EntiteCategorie> lstCategories = new List<EntiteCategorie>();
        String strRequete = "select * from Categories";
        SqlCommand cmdDDL = new SqlCommand(strRequete, dbConn);
        SqlDataReader drDDL = cmdDDL.ExecuteReader();
        while (drDDL.Read())
        {
            lstCategories.Add(new EntiteCategorie((int)drDDL[0], (string)drDDL[1]));
        }
        drDDL.Close();
        return lstCategories;
    }

    /// <summary>
    /// Cette fonction retourne une liste d'acteurs
    /// </summary>
    /// <returns> lstActeurs</returns>

    public static List<EntiteActeur> FindAllActeurs()
    {
        List<EntiteActeur> lstActeurs = new List<EntiteActeur>();
        String strRequete = "select * from Acteurs";
        SqlCommand cmdDDL = new SqlCommand(strRequete, dbConn);
        SqlDataReader drDDL = cmdDDL.ExecuteReader();
        while (drDDL.Read())
        {
            lstActeurs.Add(new EntiteActeur((int)drDDL[0], (string)drDDL[1], (string)drDDL[2]));
        }
        drDDL.Close();
        return lstActeurs;
    }

    /// <summary>
    /// Cette fonction retourne une liste de langues
    /// </summary>
    /// <returns> lstLangues</returns>

    public static List<EntiteLangue> FindAllLangue()
    {
        List<EntiteLangue> lstLangues = new List<EntiteLangue>();
        String strRequete = "select * from Langues";
        SqlCommand cmdDDL = new SqlCommand(strRequete, dbConn);
        SqlDataReader drDDL = cmdDDL.ExecuteReader();
        while (drDDL.Read())
        {
            lstLangues.Add(new EntiteLangue((int)drDDL[0], (string)drDDL[1]));
        }
        drDDL.Close();
        return lstLangues;
    }

    /// <summary>
    /// Cette fonction retourne une liste de sous-titres
    /// </summary>
    /// <returns> lstSousTitres</returns>

    public static List<EntiteSousTitres> FindAllSousTitre()
    {
        List<EntiteSousTitres> lstSousTitres = new List<EntiteSousTitres>();
        String strRequete = "select * from SousTitres";
        SqlCommand cmdDDL = new SqlCommand(strRequete, dbConn);
        SqlDataReader drDDL = cmdDDL.ExecuteReader();
        while (drDDL.Read())
        {
            lstSousTitres.Add(new EntiteSousTitres((int)drDDL[0], (string)drDDL[1]));
        }
        drDDL.Close();
        return lstSousTitres;
    }
}