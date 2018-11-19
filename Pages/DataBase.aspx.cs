using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        SqlConnection dbConn = new SqlConnection();
        Boolean binErreur = false;
        try
        {
            etablitConnexion(ref dbConn, "strConnexionDreamTeam");
        }
        catch (Exception Ex)
        {
            lblTest.Text = "DREAMTEAM as encountered aids, abort mission @ the connection";
            binErreur = true;
        }
        if (!binErreur)
        {
            String path = Server.MapPath("/CSV/");

            if (Directory.Exists(path))
            {
                //Array de toutes nos files. Ex : Acteurs.csv
                string[] fileArray = Directory.GetFiles(path)
                                                .Select(Path.GetFileName)
                                                .ToArray();

                SqlCommand cmdConn;
                cmdConn = new SqlCommand("DELETE FROM FilmsActeurs", dbConn);
                cmdConn.ExecuteNonQuery();
                cmdConn = new SqlCommand("DELETE FROM FilmsLangues", dbConn);
                cmdConn.ExecuteNonQuery();
                cmdConn = new SqlCommand("DELETE FROM FilmsSousTitres", dbConn);
                cmdConn.ExecuteNonQuery();
                cmdConn = new SqlCommand("DELETE FROM FilmsSupplements", dbConn);
                cmdConn.ExecuteNonQuery();
                cmdConn = new SqlCommand("DELETE FROM Films", dbConn);
                cmdConn.ExecuteNonQuery();
                cmdConn = new SqlCommand("DELETE FROM ValeursPreferences", dbConn);
                cmdConn.ExecuteNonQuery();
                cmdConn = new SqlCommand("DELETE FROM UtilisateursPreferences", dbConn);
                cmdConn.ExecuteNonQuery();
                cmdConn = new SqlCommand("DELETE FROM Formats", dbConn);
                cmdConn.ExecuteNonQuery();
                cmdConn = new SqlCommand("DELETE FROM Langues", dbConn);
                cmdConn.ExecuteNonQuery();
                cmdConn = new SqlCommand("DELETE FROM Producteurs", dbConn);
                cmdConn.ExecuteNonQuery();
                cmdConn = new SqlCommand("DELETE FROM Realisateurs", dbConn);
                cmdConn.ExecuteNonQuery();
                cmdConn = new SqlCommand("DELETE FROM EmpruntsFilms", dbConn);
                cmdConn.ExecuteNonQuery();
                cmdConn = new SqlCommand("DELETE FROM Exemplaires", dbConn);
                cmdConn.ExecuteNonQuery();
                cmdConn = new SqlCommand("DELETE FROM Utilisateurs", dbConn);
                cmdConn.ExecuteNonQuery();
                cmdConn = new SqlCommand("DELETE FROM TypesUtilisateur", dbConn);
                cmdConn.ExecuteNonQuery();
                cmdConn = new SqlCommand("DELETE FROM Acteurs", dbConn);
                cmdConn.ExecuteNonQuery();
                cmdConn = new SqlCommand("DELETE FROM Categories", dbConn);
                cmdConn.ExecuteNonQuery();
                cmdConn = new SqlCommand("DELETE FROM Preferences", dbConn);
                cmdConn.ExecuteNonQuery();
                cmdConn = new SqlCommand("DELETE FROM SousTitres", dbConn);
                cmdConn.ExecuteNonQuery();
                cmdConn = new SqlCommand("DELETE FROM Supplements", dbConn);
                cmdConn.ExecuteNonQuery();


                //Passer au travers de chaque fichier CSV 
                for (int i = 0; i < fileArray.Length; i++)
                {
                    //pour avoir accès au nom de la table.
                    string[] nomTable = fileArray[i].Split('.');

                    String strCheminFichier;
                    strCheminFichier = Server.MapPath("/CSV/" + fileArray[i]);

                    StreamReader reader = new StreamReader(strCheminFichier);
                    int compteur = 0;
                    
                    //vider la table en question

                    string pourDeletTable = nomTable[0];
                    pourDeletTable = pourDeletTable.Replace("1", "");
                    pourDeletTable = pourDeletTable.Replace("2", "");

                    //compter le nombre de ; dans le CSV en question.

                    int compteurLigne = 0;

                    while (!reader.EndOfStream)
                    {
                        string line = reader.ReadLine();
                        
                        if (!String.IsNullOrWhiteSpace(line))
                        {
                            string[] values = line.Split(';');
                            compteurLigne = values.Length;
                            if (compteur != 0)
                            {
                                    string strReq = genereRequete(nomTable[0], compteurLigne, values);
                                    //lblTest.Text += strReq;
                                    cmdConn = new SqlCommand(strReq, dbConn);
                                    cmdConn.ExecuteNonQuery();                             
                                
                            }
                                
                        }
                        compteur++;
                    }

                    
                }

            fermeConnexion(dbConn);
        }

        }
    }

    protected string genereRequete(string nomTablereq, int nbRep, string[] array)
    {
        //string strReq = "INSERT INTO " + nomTable[0] + " VALUES('" + values[0] + "','" + values[1] + "','" + values[2] + "')";
        nomTablereq = nomTablereq.Replace("1", "");
        nomTablereq = nomTablereq.Replace("2", "");
        string strReq = "INSERT INTO " + nomTablereq + " VALUES(";

        for (int i = 0; i < nbRep; i++)
        {
            if (i != nbRep-1)
            {
                if (array[i] == "")
                {
                    strReq += "NULL,";
                }
                else
                {
                    strReq += "'" + array[i].Replace("'", "''") + "',";
                }
                
            }
            else
            {
                if (array[i] == "")
                {
                    strReq += "NULL);";
                }
                else
                {
                    strReq += "'" + array[i].Replace("'", "''") + "');";
                }
            }
           
        }

        return strReq;
    }
    /*
    |--------------------------------------------------------------------------------------------------------------------------------------|
    | void etablitConnexion(ref SqlConnection dbConn, String strChaineConnexion)
    |--------------------------------------------------------------------------------------------------------------------------------------|
    */
    protected void etablitConnexion(ref SqlConnection dbConn, String strChaineConnexion)
    {
        dbConn.ConnectionString = ConfigurationManager.AppSettings[strChaineConnexion];
        dbConn.Open();
    }

    /*
    |--------------------------------------------------------------------------------------------------------------------------------------|
    | void fermeConnexion(SqlConnection dbConn)
    |--------------------------------------------------------------------------------------------------------------------------------------|
    */
    protected void fermeConnexion(SqlConnection dbConn)
    {
        dbConn.Close();
    }
}