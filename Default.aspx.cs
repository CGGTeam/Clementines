using System;
using System.Collections.Generic;
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
        String strNom = tbIdentifiant.Text;
        String strPassword = tbPassword.Text;

        if (AuthenticateUser(strNom, strPassword))
        {
            FormsAuthentication.RedirectFromLoginPage(strNom, true);
        }
        //connexion échoué
        else
        {
            Response.Redirect("~/Pages/Accueil.aspx");        
        }

            
    }
    private bool AuthenticateUser(string username, string password)
    {
        /*
        // ConfigurationManager class is in System.Configuration namespace
        string CS = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
        // SqlConnection is in System.Data.SqlClient namespace
        using (SqlConnection con = new SqlConnection(CS))
        {
            SqlCommand cmd = new SqlCommand("spAuthenticateUser", con);
            cmd.CommandType = CommandType.StoredProcedure;

            // FormsAuthentication is in System.Web.Security
            string EncryptedPassword = FormsAuthentication.HashPasswordForStoringInConfigFile(password, "SHA1");
            // SqlParameter is in System.Data namespace
            SqlParameter paramUsername = new SqlParameter("@UserName", username);
            SqlParameter paramPassword = new SqlParameter("@Password", EncryptedPassword);

            cmd.Parameters.Add(paramUsername);
            cmd.Parameters.Add(paramPassword);

            con.Open();
            int ReturnCode = (int)cmd.ExecuteScalar();
            return ReturnCode == 1;
        }
        */
        return true;
    }
}