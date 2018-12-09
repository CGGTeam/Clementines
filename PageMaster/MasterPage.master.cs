﻿using System;
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

        if (utilCourant.TypeUtilisateur == 'A')
        {
            nav_dvdenmain.Visible = false;
            nav_ajoutfilm.Visible = false;
        }
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
