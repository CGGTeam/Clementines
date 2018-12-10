using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;

public partial class PageMaster_MasterPage : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string utilisateur = HttpContext.Current.User.Identity.Name;
        int noUtilisateurCourrant = SQL.FindNoUtilisateurByName(utilisateur);
        EntitePreference mesPreferences = SQL.GetPreferenceByNoUtilisateur(noUtilisateurCourrant);

        body.Attributes.Add("style", "background-color:"+ mesPreferences.CouleurFond+ "; color:" + mesPreferences.CouleurTexte);
    }

    protected void PageLogin(Object sender, EventArgs e)
    {
        FormsAuthentication.SignOut();
        FormsAuthentication.RedirectToLoginPage();
    }
    protected void Search(Object sender, EventArgs e)
    {
        string str = tbNavSearch.Text;
        Response.Redirect("~/Pages/Accueil.aspx?Page=1&Filtre=" + str, false);
    }
}
