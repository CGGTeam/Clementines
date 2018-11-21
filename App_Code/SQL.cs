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
    /// <summary>
    /// Permet de récupérer la liste de film
    /// </summary>
    /// <returns>List<EntiteFilm></returns>
    public static List<EntiteFilm> FindAllFilm()
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
            int id = (int)drDDL[0];
            EntiteFilm film = new EntiteFilm(id,
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
                (drDDL[16].ToString() == "") ? "" : (string)drDDL[16]);
            lstFilms.Add(film);
        }
        drDDL.Close();
        return lstFilms;
    }
    /// <summary>
    /// Creation d'une 2e connexion pour éviter l'erreur "An unhandled exception of type 'System.InvalidOperationException' occurred in System.Data.dll"
    /// </summary>
    /// <returns>la connection</returns>
    static private SqlConnection Connection2()
    {
        SqlConnection dbConn2 = new SqlConnection();
        dbConn2.ConnectionString = ConfigurationManager.AppSettings["strConnexionDreamTeam"];
        dbConn2.Open();

        return dbConn2;
    }
    static private List<EntiteActeur> getLstActeur(int id)
    {
        SqlConnection dbConn2 = Connection2();

        List<EntiteActeur> lstActeurs = new List<EntiteActeur>();
        String strRequete = "select Acteurs.NoActeur , Acteurs.Nom , Acteurs.Sexe from FilmsActeurs " +
            "LEFT JOIN Acteurs ON FilmsActeurs.NoActeur = Acteurs.NoActeur  " +
            "where NoFilm = @id";

        SqlParameter param = new SqlParameter("@id", id);

        SqlCommand cmdDDL2 = new SqlCommand(strRequete, dbConn2);
        cmdDDL2.Parameters.Add(param);
        SqlDataReader drDDL2 = cmdDDL2.ExecuteReader();
        while (drDDL2.Read())
        {
            lstActeurs.Add(new EntiteActeur((int)drDDL2[0], (string)drDDL2[1], (string)drDDL2[2]));
        }

        dbConn2.Close();
        return lstActeurs;
    }
    static private List<EntiteLangue> getLstLangue(int id)
    {
        SqlConnection dbConn2 = Connection2();

        List<EntiteLangue> lstLangues = new List<EntiteLangue>();
        String strRequete = "select Langues.NoLangue , Langues.Langue from FilmsLangues " +
            "LEFT JOIN Langues ON FilmsLangues.NoLangue = Langues.NoLangue  " +
            "where NoFilm = @id";

        SqlParameter param = new SqlParameter("@id", id);

        SqlCommand cmdDDL2 = new SqlCommand(strRequete, dbConn2);
        cmdDDL2.Parameters.Add(param);
        SqlDataReader drDDL2 = cmdDDL2.ExecuteReader();
        while (drDDL2.Read())
        {
            lstLangues.Add(new EntiteLangue((int)drDDL2[0], (string)drDDL2[1]));
        }

        dbConn2.Close();
        return lstLangues;
    }
    static private List<EntiteSousTitres> getLstSousTitres(int id)
    {
        SqlConnection dbConn2 = Connection2();

        List<EntiteSousTitres> lstSousTitres = new List<EntiteSousTitres>();
        String strRequete = "select SousTitres.NoSousTitre , SousTitres.LangueSousTitre from FilmsSousTitres " +
            "LEFT JOIN SousTitres ON SousTitres.NoSousTitre = FilmsSousTitres.NoSousTitre  " +
            "where NoFilm = @id";

        SqlParameter param = new SqlParameter("@id", id);

        SqlCommand cmdDDL2 = new SqlCommand(strRequete, dbConn2);
        cmdDDL2.Parameters.Add(param);
        SqlDataReader drDDL2 = cmdDDL2.ExecuteReader();
        while (drDDL2.Read())
        {
            lstSousTitres.Add(new EntiteSousTitres((int)drDDL2[0], (string)drDDL2[1]));
        }

        dbConn2.Close();
        return lstSousTitres;
    }
    static private List<EntiteSupplements> getLstSupplements(int id)
    {
        SqlConnection dbConn2 = Connection2();

        List<EntiteSupplements> lstSupplements = new List<EntiteSupplements>();
        String strRequete = "select Supplements.NoSupplement , Supplements.Description from FilmsSupplements " +
            "LEFT JOIN Supplements ON Supplements.NoSupplement = FilmsSupplements.NoSupplement  " +
            "where NoFilm = @id";

        SqlParameter param = new SqlParameter("@id", id);

        SqlCommand cmdDDL2 = new SqlCommand(strRequete, dbConn2);
        cmdDDL2.Parameters.Add(param);
        SqlDataReader drDDL2 = cmdDDL2.ExecuteReader();
        while (drDDL2.Read())
        {
            lstSupplements.Add(new EntiteSupplements((int)drDDL2[0], (string)drDDL2[1]));
        }

        dbConn2.Close();
        return lstSupplements;
    }
   public static List<EntiteFilm> FindAllUserFilm(int id)
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
         "LEFT JOIN Producteurs ON Films.NoProducteur = Producteurs.NoProducteur " +
         "WHERE Films.NoUtilisateurMAJ = @id;";
      SqlParameter paramUsername = new SqlParameter("@id", id);

      SqlCommand cmdDDL = new SqlCommand(strReq, dbConn);
      cmdDDL.Parameters.Add(paramUsername);

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

   public static List<EntiteUtilisateur> FindAllUtilisateur()
   {
      List<EntiteUtilisateur> lstUtilisateur = new List<EntiteUtilisateur>();
      String strRequete = "select * from Utilisateurs;";
      SqlCommand cmdDDL = new SqlCommand(strRequete, dbConn);
      SqlDataReader drDDL = cmdDDL.ExecuteReader();
      while (drDDL.Read())
      {
         lstUtilisateur.Add(new EntiteUtilisateur((int)drDDL[0], (string)drDDL[1], (string)drDDL[2], (int)drDDL[3], Convert.ToChar((string)drDDL[4])));
      }

      drDDL.Close();

      return lstUtilisateur;
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

    /// <summary>
    /// Cette fonction retourne une liste de suppléments
    /// </summary>
    /// <returns> lstSupplements</returns>

    public static List<EntiteSupplements> FindAllSupplement()
    {
        List<EntiteSupplements> lstSupplements = new List<EntiteSupplements>();
        String strRequete = "select * from Supplements";
        SqlCommand cmdDDL = new SqlCommand(strRequete, dbConn);
        SqlDataReader drDDL = cmdDDL.ExecuteReader();
        while (drDDL.Read())
        {
            lstSupplements.Add(new EntiteSupplements((int)drDDL[0], (string)drDDL[1]));
        }
        drDDL.Close();
        return lstSupplements;
    }

    /// Permet de récuper le dvd avec l'id donné
    /// <param name="id"></param>
    /// EntiteUtilisateur
    public static EntiteUtilisateur FindUtilisateurById(int id)
    {
        EntiteUtilisateur utilisateur = null;
        String strRequete = "select * from Utilisateurs where NoUtilisateur = @id";
        SqlParameter paramUsername = new SqlParameter("@id", id);

        SqlCommand cmdDDL = new SqlCommand(strRequete, dbConn);
        cmdDDL.Parameters.Add(paramUsername);

        SqlDataReader drDDL = cmdDDL.ExecuteReader();
        while (drDDL.Read())
        {
            utilisateur = new EntiteUtilisateur((int)drDDL[0], (string)drDDL[1], (string)drDDL[2], (int)drDDL[3], Convert.ToChar((string)drDDL[4]));
        }

        drDDL.Close();
        return utilisateur;
    }
    /// Permet de récuper le dvd avec l'id donné
    /// <param name="id"></param>
    /// EntiteFilm
    public static EntiteFilm FindFilmById(int id)
    {
        EntiteFilm film = null;
        String strRequete = "SELECT Films.NoFilm, Films.AnneeSortie, Categories.[Description], Formats.[Description], Films.DateMAJ, Utilisateurs.NomUtilisateur, " +
           "Films.[Resume], Films.DureeMinutes, Films.FilmOriginal, Films.ImagePochette, Films.NbDisques, Films.TitreFrancais, Films.TitreOriginal, " +
           "Films.VersionEtendue, Realisateurs.Nom, Producteurs.Nom, Films.XTra " +
           "FROM Films " +
           "LEFT JOIN Categories ON Films.Categorie = Categories.NoCategorie " +
           "LEFT JOIN Formats ON Films.Format = Formats.NoFormat " +
           "LEFT JOIN Utilisateurs ON Films.NoUtilisateurMAJ = Utilisateurs.NoUtilisateur " +
           "LEFT JOIN Realisateurs ON Films.NoRealisateur = Realisateurs.NoRealisateur " +
           "LEFT JOIN Producteurs ON Films.NoProducteur = Producteurs.NoProducteur where Films.NoFilm = @id;";
        SqlParameter paramUsername = new SqlParameter("@id", id);

        SqlCommand cmdDDL = new SqlCommand(strRequete, dbConn);
        cmdDDL.Parameters.Add(paramUsername);

        SqlDataReader drDDL = cmdDDL.ExecuteReader();
        while (drDDL.Read())
        {
            int idFilm = (int)drDDL[1];
            film = new EntiteFilm(id,
                (drDDL[1].ToString() == "") ? -1 : idFilm,
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
                (drDDL[16].ToString() == "") ? "" : (string)drDDL[16])
            {
                lstActeurs = getLstActeur(idFilm),
                lstLangues = getLstLangue(idFilm),
                lstSousTitres = getLstSousTitres(idFilm),
                lstSupplements = getLstSupplements(idFilm)
            };
        }

        drDDL.Close();
        return film;
    }
}