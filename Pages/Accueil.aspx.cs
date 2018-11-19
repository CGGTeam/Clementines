using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _Default : System.Web.UI.Page
{

    private int nbElementsParPage = 10;
    private int noPage = 1;

    private string filtre;
    private bool filtreTitre = true;
    private bool filtrePersonne = false;
    private TypeOrderBy orderBy;

    private List<EntiteFilm> lstFilms = new List<EntiteFilm>();
    private List<EntiteFilm> lstFilmsAfficher = new List<EntiteFilm>();

    private Panel col1;
    private Panel col2;

    private Panel row2;

    public void FilterList()
    {
        lstFilmsAfficher.Clear();
        if (filtreTitre && filtrePersonne)
        {
            lstFilmsAfficher = lstFilms.Where(film => film.TitreFrancais.ToLower().Contains(filtre.ToLower()) || film.NomUtilisateur.ToLower().Contains(filtre.ToLower())).ToList();
        }
        else if (filtreTitre)
        {
            lstFilmsAfficher = lstFilms.Where(film => film.TitreFrancais.ToLower().Contains(filtre.ToLower())).ToList();
        }
        else if (filtrePersonne)
        {
            lstFilmsAfficher = lstFilms.Where(film => film.NomUtilisateur.ToLower().Contains(filtre.ToLower())).ToList();
        }
        else
        {
            lstFilmsAfficher = new List<EntiteFilm>();
        }
        OrderBy();
        AfficherLesFilms(col1, col2, row2);

    }
    public void UpdateFiltre(object sender, EventArgs e)
    {
        
        bool blnfiltreTitre = cbTitre.Checked;
        bool blnfiltrePersonne = cbPersonne.Checked;

        string strOrderby = ddlOrdeyBy.SelectedValue;
        string strFiltre = tbRecherche.Text;

        Response.Redirect("~/Pages/Accueil.aspx?Page=1&Filtre=" + strFiltre+ "&Personne="+ blnfiltrePersonne + "&Titre=" + blnfiltreTitre+ "&Orderby="+ strOrderby, false);
    }

    private void Page_Load(object sender, EventArgs e)
    {
        // Établir connection BD
        SqlConnection dbConn = new SqlConnection();
        try
        {
            etablitConnexion(ref dbConn, "strConnexionDreamTeam");
            populerListeFilms(dbConn);
        }
        catch (Exception Ex)
        {
            // peut-être rajouter un message d'erreur...
            System.Diagnostics.Debug.Write("Erreur");
        }

        lstFilmsAfficher = lstFilms.ToList();

        InitialiserOrderBy();
        InitialiserSearch();
        InitialiserNoPage();

        Panel row = librairie.divDYN(phDynamique, "row", "row");
        col1 = librairie.divDYN(row, "col1", "col-sm-6");
        col2 = librairie.divDYN(row, "col2", "col-sm-6");

        row2 = librairie.divDYN(phDynamique, "row2", "row");
        FilterList();
    }
    private void Page_LoadComplete(object sender, EventArgs e)
    {
        tbRecherche.Text = filtre;
        InitialiserCheckBoxState();
        InitialiserDDLState();
    }
    private void InitialiserDDLState()
    {
        ddlOrdeyBy.SelectedValue = orderBy.ToString();
    }
    private void AfficherLesFilms(Control col1, Control col2, Control row2)
    {
        col1.Controls.Clear();
        col2.Controls.Clear();
        row2.Controls.Clear();

        if (lstFilmsAfficher.Count() <= 0)
        {
            Label lblTitre = librairie.lblDYN(row2, "lblvide", "Il n'y a aucun film");
        }
        else
        {
            for (int i = (noPage * nbElementsParPage) - (nbElementsParPage - 1); i <= nbElementsParPage * noPage && i <= lstFilmsAfficher.Count(); i++)
            {
                if (i % 2 != 0)
                    AfficherFilm(lstFilmsAfficher[i - 1], col1);
                else
                    AfficherFilm(lstFilmsAfficher[i - 1], col2);
            }
            AfficherPager(row2);
        }
    }
    private void OrderBy()
    {
        switch (orderBy)
        {
            case TypeOrderBy.TitrePersonne:
                lstFilmsAfficher.OrderBy(film => film.TitreFrancais).ThenBy(s => s.NomUtilisateur);
                break;
            case TypeOrderBy.Personne:
                lstFilmsAfficher.Sort((x, y) => x.NomUtilisateur.CompareTo(y.NomUtilisateur));
                break;
            case TypeOrderBy.Titre:
                lstFilmsAfficher.Sort((x, y) => x.TitreFrancais.CompareTo(y.TitreFrancais));
                break;
            default:
                lstFilmsAfficher.OrderBy(film => film.TitreFrancais).ThenBy(s => s.NomUtilisateur);
                break;
        }
    }
    private void InitialiserNoPage()
    {
        int n;
        if (Request.QueryString["Page"] == null || !int.TryParse(Request.QueryString["Page"], out n))
        {
            noPage = 1;
        }
        else
        {
            noPage = n;
        }
    }
    private void InitialiserOrderBy()
    {
        if (Request.QueryString["Orderby"] == null)
        {
            orderBy = TypeOrderBy.TitrePersonne;
        }
        else
        {
            if (Request.QueryString["Orderby"] == "TitrePersonne")
                orderBy = TypeOrderBy.TitrePersonne;
            else if(Request.QueryString["Orderby"] == "Titre")
                orderBy = TypeOrderBy.Titre;
            else if(Request.QueryString["Orderby"] == "Personne")
                orderBy = TypeOrderBy.Personne;
            else
                orderBy = TypeOrderBy.TitrePersonne;
        }
    }
    private void InitialiserSearch()
    {
        if (Request.QueryString["Filtre"] == null)
        {
            filtre = "";
        }
        else
        {
            filtre = Request.QueryString["Filtre"];
        }
        //personne
        if (Request.QueryString["Personne"] == null)
        {
            filtrePersonne = true;
        }
        else
        {
            if (Request.QueryString["Personne"].ToLower() == "True".ToLower())
                filtrePersonne = true;
            else if (Request.QueryString["Personne"].ToLower() == "False".ToLower())
                filtrePersonne = false;
            else
                filtrePersonne = true;
        }
        //titre
        if (Request.QueryString["Titre"] == null)
        {
            filtreTitre = true;
        }
        else
        {
            if (Request.QueryString["Titre"].ToLower() == "True".ToLower())
                filtreTitre = true;
            else if (Request.QueryString["Titre"].ToLower() == "False".ToLower())
                filtreTitre = false;
            else
                filtreTitre = true;
        }

    }
    private void InitialiserCheckBoxState()
    {
        cbTitre.Checked = filtreTitre;
        cbPersonne.Checked = filtrePersonne;
    }
    private void AfficherFilm(EntiteFilm film, Control container)
    {
        Panel div = librairie.divDYN(container, "section"+film.NoFilm, "panel panel-warning");
        Panel header = librairie.divDYN(div, "header" + film.NoFilm, "panel-heading");
        Panel content = librairie.divDYN(div, "content" + film.NoFilm, "panel-body vignette");


        Panel panelCache = librairie.divDYN(content, "panel-cache_" + film.NoFilm, "boutons-caches row ");

        Button btn1 = librairie.btnDYN(panelCache, "courriel_" + film.NoFilm, "btn btn-sm btn-default boutons-options-film col-xs-6 pull-right", "Envoyer un courriel à " + film.NomUtilisateur);
        btn1.Click += new EventHandler(EnvoyerUnCourriel);
        Button btn2 = librairie.btnDYN(panelCache, "affichage_detaillee_" + film.NoFilm, "btn btn-sm btn-default boutons-options-film col-xs-6 pull-right", "Affichage détaillée");
        btn2.Click += new EventHandler(AfficherDetails);
        Button btn3 = librairie.btnDYN(panelCache, "approprier" + film.NoFilm, "btn btn-sm btn-default boutons-options-film col-xs-6 pull-right", "S'approprier le film");
        btn3.Click += new EventHandler(ApproprierDVD);

        Image img = librairie.imgDYN(content, "img" + film.NoFilm, film.ImagePochette, ".img-rounded col-sm-2");
        Panel divProprietaire = librairie.divDYN(content, film.NoFilm + "Personne", "pull-right");
        librairie.brDYN(divProprietaire);
        Label lblPersonne = librairie.lblDYN(divProprietaire, "lblPersonne" + film.NoFilm, film.NomUtilisateur);
        Label lblTitre = librairie.lblDYN(header, "lbl" + film.NoFilm, film.TitreFrancais);
    }
    public void AfficherDetails(object sender, EventArgs e)
    {
        String url = "~/Pages/AffichageDetaille.aspx";
        Response.Redirect(url, true);
    }
    public void ApproprierDVD(object sender, EventArgs e)
    {
        // TODO : S'approprier un DVD
    }
    private void EnvoyerUnCourriel(object sender, EventArgs e)
    {
        String url = "~/Pages/Courriel.aspx";
        Response.Redirect(url, true);
    }

    private void AfficherPager(Control control)
    {
        LiteralControl pager = new LiteralControl();
        decimal nbPages = Math.Ceiling((decimal)lstFilmsAfficher.Count / (decimal)nbElementsParPage);

        string strFiltreComplet = "&Filtre=" + filtre+"&Personne=" + filtrePersonne + "&Titre=" + filtreTitre + "Orderby=" + orderBy.ToString();

        int previous = noPage - 1;
        string strClass = previous <= 0 ? "page-item disabled" : "page-item";
        previous = previous <= 0 ? noPage : previous;

        string strDebut = "<nav aria - label = 'Page navigation example' >" +
                                "<ul class='pagination justify-content-center'>" +
                                    "<li class='" + strClass + "'><a class='page-link' href='?Page=" + previous + strFiltreComplet + "'> Previous</a></li>";

        pager.Text += strDebut;
        for (int i = 1; i <= nbPages; i++)
        {
            strClass = noPage == i ? "page-item active" : "page-item";
            string strMillieu = "<li class='" + strClass + "'><a class='page - link' href='?Page=" + i + strFiltreComplet + "'>" + i + "</a></li>";
            pager.Text += strMillieu;
        }

        int next = noPage + 1;
        strClass = next >= nbPages + 1 ? "page-item disabled" : "page-item";
        next = next >= nbPages + 1 ? noPage : next;

        string strFin = "<li class='" + strClass + "'><a class='page-link' href='?Page=" + next + strFiltreComplet + "'>Next</a></li>" +
                                "</ul>" +
                            "</nav>";
        pager.Text += strFin;

        control.Controls.Add(pager);
    }
    public void populerListeFilms(SqlConnection dbConn)
    {
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
    }
    protected void etablitConnexion(ref SqlConnection dbConn, String strChaineConnexion)
    {
        dbConn.ConnectionString = ConfigurationManager.AppSettings[strChaineConnexion];
        dbConn.Open();
    }
}