using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class controles_utilisateur_NomFilm : System.Web.UI.UserControl
{
    public String Text
    {
        set { film1.Text = value; }
        get { return film1.Text; }
    }
    public TextBox Controle { get { return film1; } }
    public string proprietaire { get; set; }
    public string titre { set { Titre.Text = value; } }

    


    public string CssClass
    {
        set
        {
            for (int i = 1; i <= 10; i++)
            {
                string nomTextBox = "film" + i;
                TextBox tbFilm = this.FindControl(nomTextBox) as TextBox;
                tbFilm.CssClass = value;
            }
        }
    }
    public string placeholder
    {
        set
        {
            for (int i = 1; i <= 10; i++)
            {
                string nomTextBox = "film" + i;
                TextBox tbFilm = this.FindControl(nomTextBox) as TextBox;
                tbFilm.Attributes.Add("placeholder", value);
            }
        }
    }
    public void SetProprietaire(string propri)
    {
        this.proprietaire = propri.Trim();
    }
    protected void btnEnregistrer_Click(object sender, EventArgs e)
    {
        string utilisateur = string.IsNullOrEmpty(this.proprietaire) ?
            HttpContext.Current.User.Identity.Name : proprietaire;
        List<string> lstNomFilm = new List<string>();
        List<string> lstMauvais = new List<string>();

        for (int i = 1; i <= 10; i++)
        {
            string nomTextBox = "film" + i;
            TextBox tbFilm = this.FindControl(nomTextBox) as TextBox;

            if (tbFilm != null && !string.IsNullOrEmpty(tbFilm.Text))
            {
                string nom = tbFilm.Text;
                SQL.Connection();
                if (!SQL.checkIfNomFilmExiste(nom))
                    lstNomFilm.Add(nom);
                else
                    lstMauvais.Add(nom);
            }
        }


        SQL.AddMovieShort(lstNomFilm, utilisateur);

        string sep = ", ";

        if (lstNomFilm.Any())
        {
            succes.Visible = true;
            lblSucces.Text = "Les DVDs suivant ont été enregistré : " + String.Join(sep, lstNomFilm);
        }
        if (lstMauvais.Any())
        {
            error.Visible = true;
            lblError.Text = "Les DVDs suivant n'ont pu être enregistré car ils existent déjà : " + String.Join(sep, lstMauvais);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
            proprietaire = null;
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