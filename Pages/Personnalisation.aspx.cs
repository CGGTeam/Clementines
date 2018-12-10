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
        if (!IsPostBack)
        {
            string utilisateur = HttpContext.Current.User.Identity.Name;
            int noUtilisateur = SQL.FindNoUtilisateurByName(utilisateur);
            EntitePreference mesPreferences = SQL.GetPreferenceByNoUtilisateur(noUtilisateur);

            LoadChamps(mesPreferences);
        }
    }
    private void LoadChamps(EntitePreference mesPreferences)
    {
        couleurFond.Text = mesPreferences.CouleurFond;
        couleurTexte.Text = mesPreferences.CouleurTexte;

        cbCourrielRetrait.Checked = mesPreferences.CourrielSiSuppression;
        cbCourrielAjout.Checked = mesPreferences.CourrielSiAjout;
        cbCourrielAppropiation.Checked = mesPreferences.CourrielSiAppropriation;

        nbDVDPage.Text = mesPreferences.NbFilmParPage.ToString();
    }

    protected void modifier_Click(object sender, EventArgs e)
    {
        string utilisateur = HttpContext.Current.User.Identity.Name;
        int noUtilisateur = SQL.FindNoUtilisateurByName(utilisateur);

        int nbParPages = 10;
        int.TryParse(nbDVDPage.Text, out nbParPages);
        EntitePreference entitePreference = new EntitePreference()
        {
            CouleurFond = couleurFond.Text,
            CouleurTexte = couleurTexte.Text,
            CourrielSiSuppression = cbCourrielRetrait.Checked,
            CourrielSiAjout = cbCourrielAjout.Checked,
            CourrielSiAppropriation = cbCourrielAppropiation.Checked,

            NbFilmParPage = nbParPages 
        };
      
        if (SQL.UpdatePreference(entitePreference, noUtilisateur))
        {
            succes.Visible = true;
            lblSucces.Text = "Les modifications ont été apporté";
        }
        else
        {
            error.Visible = true;
            lblError.Text = "Une erreure est survenue";
        }

    }
    protected void fermerSucces(object sender, EventArgs e)
    {
        succes.Visible = false;
    }
    protected void fermerError(object sender, EventArgs e)
    {
        error.Visible = false;
    }
}