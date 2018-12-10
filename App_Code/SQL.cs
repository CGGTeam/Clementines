using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
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
    public static List<EntiteExemplaire> FindAllUserExemplairesEmpruntes(int id)
    {
        List<EntiteExemplaire> lstExemplaires = new List<EntiteExemplaire>();
        String strReq = "SELECT Films.NoFilm, Films.AnneeSortie, Categories.[Description], Formats.[Description], Films.DateMAJ, Utilisateurs.NomUtilisateur, " +
                          "Films.[Resume], Films.DureeMinutes, Films.FilmOriginal, Films.ImagePochette, Films.NbDisques, Films.TitreFrancais, Films.TitreOriginal, " +
                          "Films.VersionEtendue, Realisateurs.Nom, Producteurs.Nom, Films.XTra, " +
                            "Utilisateurs.NoUtilisateur, Utilisateurs.NomUtilisateur, Utilisateurs.Courriel, Utilisateurs.MotPasse, Utilisateurs.TypeUtilisateur " +
                          "FROM Films " +
                          "LEFT JOIN Categories ON Films.Categorie = Categories.NoCategorie " +
                          "LEFT JOIN Formats ON Films.Format = Formats.NoFormat " +
                          "LEFT JOIN Realisateurs ON Films.NoRealisateur = Realisateurs.NoRealisateur " +
                          "LEFT JOIN Producteurs ON Films.NoProducteur = Producteurs.NoProducteur " +
                          "inner join EmpruntsFilms on Films.NoFilm = LEFT(CONVERT(NVARCHAR, EmpruntsFilms.NoExemplaire), 6) " +
                          "inner join Utilisateurs on EmpruntsFilms.NoUtilisateur = Utilisateurs.NoUtilisateur " +
                          "where EmpruntsFilms.NoUtilisateur = @id " +
                          "order by Films.TitreFrancais";

        System.Diagnostics.Debug.WriteLine(strReq);


        SqlParameter paramUsername = new SqlParameter("@id", id);

        SqlCommand cmdDDL = new SqlCommand(strReq, dbConn);
        cmdDDL.Parameters.Add(paramUsername);

        SqlDataReader drDDL = cmdDDL.ExecuteReader();
        while (drDDL.Read())
        {
            EntiteExemplaire exemplaire = new EntiteExemplaire
            {
                film = new EntiteFilm((int)drDDL[0],
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
               (drDDL[16].ToString() == "") ? "" : (string)drDDL[16]),

                proprietaire = new EntiteUtilisateur((int)drDDL[17],
               (string)drDDL[18],
               (string)drDDL[19],
               (int)drDDL[20],
               Convert.ToChar((string)drDDL[21]))
            };
            lstExemplaires.Add(exemplaire);
        }
        drDDL.Close();

        return lstExemplaires;
    }

    public static List<EntiteExemplaire> FindAllUserExemplaires(int id)
   {
      List<EntiteExemplaire> lstExemplaires = new List<EntiteExemplaire>();
      String strReq = "SELECT Films.NoFilm, Films.AnneeSortie, Categories.[Description], Formats.[Description], Films.DateMAJ, Utilisateurs.NomUtilisateur, " +
                        "Films.[Resume], Films.DureeMinutes, Films.FilmOriginal, Films.ImagePochette, Films.NbDisques, Films.TitreFrancais, Films.TitreOriginal, " +
                        "Films.VersionEtendue, Realisateurs.Nom, Producteurs.Nom, Films.XTra, " +
		                  "Utilisateurs.NoUtilisateur, Utilisateurs.NomUtilisateur, Utilisateurs.Courriel, Utilisateurs.MotPasse, Utilisateurs.TypeUtilisateur " +
                        "FROM Films " +
                        "LEFT JOIN Categories ON Films.Categorie = Categories.NoCategorie " +
                        "LEFT JOIN Formats ON Films.Format = Formats.NoFormat " +
                        "LEFT JOIN Realisateurs ON Films.NoRealisateur = Realisateurs.NoRealisateur " +
                        "LEFT JOIN Producteurs ON Films.NoProducteur = Producteurs.NoProducteur " +
                        "inner join Exemplaires on Films.NoFilm = LEFT(CONVERT(NVARCHAR, Exemplaires.NoExemplaire), 6) " +
                        "inner join Utilisateurs on Exemplaires.NoUtilisateurProprietaire = Utilisateurs.NoUtilisateur " +
                        "where Exemplaires.NoUtilisateurProprietaire = @id " +
                        "order by Films.TitreFrancais";


      SqlParameter paramUsername = new SqlParameter("@id", id);

      SqlCommand cmdDDL = new SqlCommand(strReq, dbConn);
      cmdDDL.Parameters.Add(paramUsername);

      SqlDataReader drDDL = cmdDDL.ExecuteReader();
      while (drDDL.Read())
      {
         EntiteExemplaire exemplaire = new EntiteExemplaire
         {
            film = new EntiteFilm((int)drDDL[0],
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
            (drDDL[16].ToString() == "") ? "" : (string)drDDL[16]),

            proprietaire = new EntiteUtilisateur((int)drDDL[17], 
            (string)drDDL[18], 
            (string)drDDL[19], 
            (int)drDDL[20], 
            Convert.ToChar((string)drDDL[21]))
         };
         lstExemplaires.Add(exemplaire);
      }
      drDDL.Close();

      return lstExemplaires;
   }
    public static List<EntiteExemplaire> FindAllExemplaires()
    {
        List<EntiteExemplaire> lstExemplaires = new List<EntiteExemplaire>();
        String strReq = "SELECT Films.NoFilm, Films.AnneeSortie, Categories.[Description], Formats.[Description], Films.DateMAJ, Utilisateurs.NomUtilisateur, " +
                          "Films.[Resume], Films.DureeMinutes, Films.FilmOriginal, Films.ImagePochette, Films.NbDisques, Films.TitreFrancais, Films.TitreOriginal, " +
                          "Films.VersionEtendue, Realisateurs.Nom, Producteurs.Nom, Films.XTra, " +
                            "Utilisateurs.NoUtilisateur, Utilisateurs.NomUtilisateur, Utilisateurs.Courriel, Utilisateurs.MotPasse, Utilisateurs.TypeUtilisateur " +
                          "FROM Films " +
                          "LEFT JOIN Categories ON Films.Categorie = Categories.NoCategorie " +
                          "LEFT JOIN Formats ON Films.Format = Formats.NoFormat " +
                          "LEFT JOIN Realisateurs ON Films.NoRealisateur = Realisateurs.NoRealisateur " +
                          "LEFT JOIN Producteurs ON Films.NoProducteur = Producteurs.NoProducteur " +
                          "inner join Exemplaires on Films.NoFilm = LEFT(CONVERT(NVARCHAR, Exemplaires.NoExemplaire), 6) " +
                          "inner join Utilisateurs on Exemplaires.NoUtilisateurProprietaire = Utilisateurs.NoUtilisateur";

        SqlCommand cmdDDL = new SqlCommand(strReq, dbConn);

        SqlDataReader drDDL = cmdDDL.ExecuteReader();
        while (drDDL.Read())
        {
            EntiteExemplaire exemplaire = new EntiteExemplaire
            {
                film = new EntiteFilm((int)drDDL[0],
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
               (drDDL[16].ToString() == "") ? "" : (string)drDDL[16]),

                proprietaire = new EntiteUtilisateur((int)drDDL[17],
               (string)drDDL[18],
               (string)drDDL[19],
               (int)drDDL[20],
               Convert.ToChar((string)drDDL[21]))
            };
            lstExemplaires.Add(exemplaire);
        }
        drDDL.Close();

        return lstExemplaires;
    }
    public static EntiteExemplaire FindExemplaireById(int id)
    {
        EntiteExemplaire exemplaire = null;
        String strReq = "SELECT Films.NoFilm, Films.AnneeSortie, Categories.[Description], Formats.[Description], Films.DateMAJ, Utilisateurs.NomUtilisateur, " +
                              "Films.[Resume], Films.DureeMinutes, Films.FilmOriginal, Films.ImagePochette, Films.NbDisques, Films.TitreFrancais, Films.TitreOriginal, " +
                              "Films.VersionEtendue, Realisateurs.Nom, Producteurs.Nom, Films.XTra, " +
                                "Utilisateurs.NoUtilisateur, Utilisateurs.NomUtilisateur, Utilisateurs.Courriel, Utilisateurs.MotPasse, Utilisateurs.TypeUtilisateur " +
                              "FROM Films " +
                              "LEFT JOIN Categories ON Films.Categorie = Categories.NoCategorie " +
                              "LEFT JOIN Formats ON Films.Format = Formats.NoFormat " +
                              "LEFT JOIN Realisateurs ON Films.NoRealisateur = Realisateurs.NoRealisateur " +
                              "LEFT JOIN Producteurs ON Films.NoProducteur = Producteurs.NoProducteur " +
                              "inner join Exemplaires on Films.NoFilm = LEFT(CONVERT(NVARCHAR, Exemplaires.NoExemplaire), 6) " +
                              "inner join Utilisateurs on Exemplaires.NoUtilisateurProprietaire = Utilisateurs.NoUtilisateur " +
                              "where FILMS.NoFilm = @id";


        SqlParameter paramId = new SqlParameter("@id", id);

        SqlCommand cmdDDL = new SqlCommand(strReq, dbConn);
        cmdDDL.Parameters.Add(paramId);

        SqlDataReader drDDL = cmdDDL.ExecuteReader();
        while (drDDL.Read())
        {
            exemplaire = new EntiteExemplaire
            {
                film = new EntiteFilm((int)drDDL[0],
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
               (drDDL[16].ToString() == "") ? "" : (string)drDDL[16])
                {
                    lstActeurs = getLstActeur((int)drDDL[0]),
                    lstLangues = getLstLangue((int)drDDL[0]),
                    lstSousTitres = getLstSousTitres((int)drDDL[0]),
                    lstSupplements = getLstSupplements((int)drDDL[0])
                },

                proprietaire = new EntiteUtilisateur((int)drDDL[17],
               (string)drDDL[18],
               (string)drDDL[19],
               (int)drDDL[20],
               Convert.ToChar((string)drDDL[21]))
            };
        }
        drDDL.Close();

        return exemplaire;
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

    public static List<EntiteUtilisateur> FindAllAutresUtilisateur(int noUtilCourant)
    {
        List<EntiteUtilisateur> lstUtilisateur = new List<EntiteUtilisateur>();
        String strRequete = "select * from Utilisateurs where NoUtilisateur != " + noUtilCourant + " and TypeUtilisateur != 'A';";
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
        SQL.Connection2();
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
        SQL.Connection2();
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

    public static EntiteUtilisateur FindUtilisateurByName(string name)
    {
        SqlConnection dbConn2 = Connection2();

        EntiteUtilisateur utilisateur = null;
        String strRequete = "select * from Utilisateurs where NomUtilisateur = @name";
        SqlParameter paramUsername = new SqlParameter("@name", name);

        SqlCommand cmdDDL = new SqlCommand(strRequete, dbConn2);
        cmdDDL.Parameters.Add(paramUsername);

        SqlDataReader drDDL = cmdDDL.ExecuteReader();
        while (drDDL.Read())
        {
            utilisateur = new EntiteUtilisateur((int)drDDL[0], (string)drDDL[1], (string)drDDL[2], (int)drDDL[3], Convert.ToChar((string)drDDL[4]));
        }

        drDDL.Close();
        dbConn2.Close();
        return utilisateur;
    }

    /// Permet de récuper le dvd avec l'id donné
    /// <param name="id"></param>
    /// EntiteUtilisateur
    public static int FindNoUtilisateurByName(string name)
    {
        SqlConnection dbConn2 = Connection2();
        int noUtilisateur = 0;
        String strRequete = "select NoUtilisateur from Utilisateurs where NomUtilisateur = @name";
        SqlParameter paramUsername = new SqlParameter("@name", name);

        SqlCommand cmdDDL = new SqlCommand(strRequete, dbConn2);
        cmdDDL.Parameters.Add(paramUsername);

        SqlDataReader drDDL = cmdDDL.ExecuteReader();
        while (drDDL.Read())
        {
            noUtilisateur = (int)drDDL[0];
        }
        dbConn2.Close();
        return noUtilisateur;
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
            int idFilm = (int)drDDL[0];
            film = new EntiteFilm(idFilm,               
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
    private static int FindNextNoFilm()
    {
        int noFilm = 0;
        String strRequete = "select max(NoFilm) from Films";

        SqlCommand cmdDDL = new SqlCommand(strRequete, dbConn);

        SqlDataReader drDDL = cmdDDL.ExecuteReader();
        while (drDDL.Read())
        {
            noFilm = (int)drDDL[0];
        }
        drDDL.Close();
        return noFilm +1;
    }

    /// Permet d'ajouter un film abrégé dans la BD
    /// <param name="film"></param>
    /// <param name="nomDeUtilisateur"></param>
    /// <returns>Si la requete est réeussi</returns>
    public static int AddMovieShort(List<string> lstNomFilm, string nomDeUtilisateur)
    {
        int intNbAjout = 0;    
        int noUtilisateurMAJ = FindNoUtilisateurByName(nomDeUtilisateur);
        DateTime DateMAJ = DateTime.Now;

        foreach (string nomFilm in lstNomFilm)
        {
            int noFilm = FindNextNoFilm();
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Connection = dbConn;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "INSERT INTO Films(NoFilm, TitreFrancais, NoUtilisateurMAJ, DateMAJ) Values (@no, @titre, @noMAJ, @date)";
                cmd.Parameters.AddWithValue("@no", noFilm);
                cmd.Parameters.AddWithValue("@titre", nomFilm);
                cmd.Parameters.AddWithValue("@noMAJ", noUtilisateurMAJ);
                cmd.Parameters.AddWithValue("@date", DateMAJ);

                intNbAjout += cmd.ExecuteNonQuery();
            }
            CreerExemplaire(noFilm, noUtilisateurMAJ);     
        }
        return intNbAjout;
    }

    public static int ApproprierDVD(int noNewOwnerNo, int noFilm)
    {
        string strFilm = noFilm.ToString();
        SqlConnection dbConn2 = Connection2();
        String strRequete = "UPDATE Films SET DateMAJ = '" + DateTime.Now.ToShortDateString() + "', NoUtilisateurMAJ = " + noNewOwnerNo + " WHERE NoFilm = " + strFilm + "; ";
        SqlCommand cmdDDL = new SqlCommand(strRequete, dbConn2);
        int nbLignes = cmdDDL.ExecuteNonQuery();
        //System.Diagnostics.Debug.WriteLine(strRequete);
        
        String strRequete2 = "UPDATE EmpruntsFilms SET DateEmprunt = " + "'" + DateTime.Now.ToString() + "'" + ", NoUtilisateur = " + noNewOwnerNo + " WHERE NoExemplaire = " + strFilm + "01" + "; ";
        SqlCommand cmdDDL2 = new SqlCommand(strRequete2, dbConn2);
        int nbLignes2 = cmdDDL2.ExecuteNonQuery();

        dbConn2.Close();
        return nbLignes + nbLignes2;
    }

    public static int SupprimerDVD(int noFilm)
    {
        string strFilm = noFilm.ToString();
        SqlConnection dbConn2 = Connection2();
        String strRequete = "DELETE FROM FilmsActeurs WHERE NoFilm = " + noFilm + "; ";
        strRequete += "DELETE FROM FilmsLangues WHERE NoFilm = " + noFilm + "; ";
        strRequete += "DELETE FROM FilmsSousTitres WHERE NoFilm = " + noFilm + "; ";
        strRequete += "DELETE FROM FilmsSupplements WHERE NoFilm = " + noFilm + "; ";
        strRequete += "DELETE FROM Films WHERE NoFilm = " + noFilm + "; ";
        strRequete += "DELETE FROM EmpruntsFilms WHERE NoExemplaire = " + noFilm + "01" + "; ";
        strRequete += "DELETE FROM Exemplaires WHERE NoExemplaire = " + noFilm + "01" + "; ";
        
        SqlCommand cmdDDL = new SqlCommand(strRequete, dbConn2);
        int nbLignes = cmdDDL.ExecuteNonQuery();

        dbConn2.Close();
        return nbLignes;
    }

    public static bool checkIfNomFilmExiste(string titre)
    {
        bool estPresent = false;
        string strRequete = "SELECT COUNT(*) FROM Films" +
            " WHERE TitreFrancais = @titre";
        SqlParameter paramTitre = new SqlParameter("@titre", titre);
        SqlCommand cmdDDL = new SqlCommand(strRequete, dbConn);
        cmdDDL.Parameters.Add(paramTitre);
        SqlDataReader drDDL = cmdDDL.ExecuteReader();

        while (drDDL.Read())
        {
            estPresent = (int)drDDL[0]>=1;
        }
        dbConn.Close();
        return estPresent;
    }

    public static bool checkIfNomOriginalFilmExiste(string titre)
    {
        bool estPresent = false;
        string strRequete = "SELECT COUNT(*) FROM Films" +
            " WHERE TitreOriginal = @titre";
        SqlParameter paramTitre = new SqlParameter("@titre", titre);
        SqlCommand cmdDDL = new SqlCommand(strRequete, dbConn);
        cmdDDL.Parameters.Add(paramTitre);
        SqlDataReader drDDL = cmdDDL.ExecuteReader();

        while (drDDL.Read())
        {
            estPresent = (int)drDDL[0] >= 1;
        }
        dbConn.Close();
        return estPresent;
    }

    public static bool CreerExemplaire(int noFilm, int noUtilisateur)
    {
        int intNbAjout =0;
        DateTime now = DateTime.Now;
        int noExemplaire = (int.Parse(noFilm + "01"));
        using (SqlCommand cmd = new SqlCommand())
        {
            cmd.Connection = dbConn;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "INSERT INTO Exemplaires(NoExemplaire, NoUtilisateurProprietaire) Values (@no, @proprietaire)";
            cmd.Parameters.AddWithValue("@no", noExemplaire);
            cmd.Parameters.AddWithValue("@proprietaire", noUtilisateur);

            intNbAjout += cmd.ExecuteNonQuery();
        }
        if (intNbAjout == 0) return false;
        using (SqlCommand cmd = new SqlCommand())
        {
            cmd.Connection = dbConn;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "INSERT INTO EmpruntsFilms(NoExemplaire, NoUtilisateur, DateEmprunt) Values (@no, @proprietaire, @date)";
            cmd.Parameters.AddWithValue("@no", noExemplaire);
            cmd.Parameters.AddWithValue("@proprietaire", noUtilisateur);
            cmd.Parameters.AddWithValue("@date", now);

            intNbAjout += cmd.ExecuteNonQuery();
        }
        return intNbAjout >= 1;
    }

    public static EntitePreference GetPreferenceByNoUtilisateur(int noUtilisateur)
    {
        SqlConnection dbConn2 = Connection2();
        EntitePreference preference = new EntitePreference();
        using (SqlCommand cmd = new SqlCommand())
        {
            cmd.Connection = dbConn2;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select NoPreference, Valeur from ValeursPreferences " +
                    "where NoUtilisateur = @No";
            cmd.Parameters.AddWithValue("@no", noUtilisateur);

            SqlDataReader drDDL = cmd.ExecuteReader();

            while (drDDL.Read())
            {
                switch ((int)drDDL[0])
                {
                    case 1:
                        preference.CouleurFond = (string)drDDL[1];
                        break;
                    case 2:
                        preference.CouleurTexte = (string)drDDL[1];
                        break;
                    case 3:
                        preference.CourrielSiAjout = (string)drDDL[1]== "1";
                        break;
                    case 4:
                        preference.CourrielSiAppropriation = (string)drDDL[1] == "1";
                        break;
                    case 5:
                        preference.CourrielSiSuppression = (string)drDDL[1] == "1";
                        break;
                    case 6:
                        preference.ImageFond = (string)drDDL[1];
                        break;
                    case 7:
                        preference.NbFilmParPage = int.Parse((string)drDDL[1]);
                        break;
                }
            }
        }
        dbConn2.Close();

        return preference;
    }
    public static bool UpdatePreference(EntitePreference preferences, int noUtilisateur)
    {
        bool e = noUtilisateur == 1;
        int intNbAjout = 0;
        SqlConnection dbConn2 = Connection2();
        using (SqlCommand cmd = new SqlCommand())
        {
            cmd.Connection = dbConn2;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "DELETE FROM ValeursPreferences WHERE NoUtilisateur = @no;";
            cmd.Parameters.AddWithValue("@no", noUtilisateur);

            intNbAjout += cmd.ExecuteNonQuery();
        }
        using (SqlCommand cmd = new SqlCommand())
        {
            cmd.Connection = dbConn2;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "DELETE FROM UtilisateursPreferences WHERE NoUtilisateur = @no;";
            cmd.Parameters.AddWithValue("@no", noUtilisateur);

            intNbAjout += cmd.ExecuteNonQuery();
        }
        for(int i = 1; i <= 7; i ++)
        {
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Connection = dbConn2;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "INSERT INTO UtilisateursPreferences(NoUtilisateur, NoPreference) Values (@no, @noPreference)";
                cmd.Parameters.AddWithValue("@no", noUtilisateur);
                cmd.Parameters.AddWithValue("@noPreference", i);

                intNbAjout += cmd.ExecuteNonQuery();
            }
        }
        for(int i = 1; i <= 7; i++)
        {
            using (SqlCommand cmd = new SqlCommand())
            {
                string valeur = "";
                switch (i)
                {
                    case 1: valeur = preferences.CouleurFond; break;
                    case 2: valeur = preferences.CouleurTexte; break;
                    case 3: valeur = preferences.CourrielSiAjout ? "1" : "0"; break;
                    case 4: valeur = preferences.CourrielSiAppropriation ? "1" : "0"; break;
                    case 5: valeur = preferences.CourrielSiSuppression ? "1" : "0"; break;
                    case 6: valeur = preferences.ImageFond; break;
                    case 7: valeur = preferences.NbFilmParPage.ToString(); break;
                }

                cmd.Connection = dbConn2;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "INSERT INTO ValeursPreferences(NoUtilisateur, NoPreference, Valeur) Values (@no, @noPreference, @valeur)";
                cmd.Parameters.AddWithValue("@no", noUtilisateur);
                cmd.Parameters.AddWithValue("@noPreference", i);
                cmd.Parameters.AddWithValue("@valeur", valeur);

                intNbAjout += cmd.ExecuteNonQuery();
            }
        }

        return intNbAjout>=1;
    }
}