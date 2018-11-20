﻿using System;
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

    
}