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
        EntiteUtilisateur utilCourant = SQL.FindUtilisateurByName(HttpContext.Current.User.Identity.Name);
        lbl_user_connected.Text = utilCourant.NomUtilisateur;
        if (utilCourant.TypeUtilisateur == 'A')
        {
            nav_dvdenmain.Visible = false;
            nav_ajoutfilm.Visible = false;
            nav_gestionUtilisateur.Visible = true;
        }

        EntitePreference mesPreferences = SQL.GetPreferenceByNoUtilisateur(utilCourant.NoUtilisateur);

        UpdateColor();
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
    public void UpdateColor()
    {
        EntiteUtilisateur utilCourant = SQL.FindUtilisateurByName(HttpContext.Current.User.Identity.Name);
        EntitePreference mesPreferences = SQL.GetPreferenceByNoUtilisateur(utilCourant.NoUtilisateur);
        body.Attributes.Add("style", "background-color:" + mesPreferences.CouleurFond + "; color:" + mesPreferences.CouleurTexte);
    }
}
