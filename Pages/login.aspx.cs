using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _Default :  System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void tentativeLogin(object sender, EventArgs e)
    {
        String strNom = tbIdentifiant.Text;
        String strPassword = tbPassword.Text;

        /*
         * Faire validation
         */

        /*
         * Connecter à la BD
         */
         if(strNom == "user" && strPassword == "Pass1")
            Response.Redirect("~/Default.aspx");
    }
}