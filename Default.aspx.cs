using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void tentativeLogin(object sender, EventArgs e)
    {
      
        if (!tbPassword.Format.IsValid)
        {
            lblError.Text = "Connexion échouée";

            return;
        }
        String strNom = tbIdentifiant.Text;
        String strPassword = tbPassword.Text;

        if (AuthenticateUser(strNom, strPassword))
        {
            FormsAuthentication.RedirectFromLoginPage(strNom, false);
            Response.Redirect("~/Pages/Accueil.aspx", false);
        }
        //connexion échoué
        else
        {
            lblError.Text = "Connexion échouée";
        }

            
    }
    private bool AuthenticateUser(string username, string password)
    {
        SqlConnection con = new SqlConnection();
        con.ConnectionString = ConfigurationManager.AppSettings["strConnexionDreamTeam"];
        using (con)
        {
            SqlCommand cmd = new SqlCommand("spAuthenticateUser", con);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter paramUsername = new SqlParameter("@UserName", username);
            SqlParameter paramPassword = new SqlParameter("@Password", password);

            cmd.Parameters.Add(paramUsername);
            cmd.Parameters.Add(paramPassword);

            con.Open();
            var ReturnCode = cmd.ExecuteScalar();
            return (int)ReturnCode == 1;
        }
    }
    protected void etablitConnexion(ref SqlConnection dbConn, String strChaineConnexion)
    {
        dbConn.ConnectionString = ConfigurationManager.AppSettings[strChaineConnexion];
        dbConn.Open();
    }
}