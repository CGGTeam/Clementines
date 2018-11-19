using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Drawing;
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

    public void FilterList(Label lblMessage)
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
        AfficherLesFilms(lblMessage);

    }
    public void UpdateFiltre(object sender, EventArgs e)
    {
        Controls.Remove(phDynamique);
        bool blnfiltreTitre = cbTitre.Checked;
        bool blnfiltrePersonne = cbPersonne.Checked;

        string strOrderby = ddlOrdeyBy.SelectedValue;
        string strFiltre = tbRecherche.Text;

        Response.Redirect("~/Pages/Accueil.aspx?Page=1&Filtre=" + strFiltre+ "&Personne="+ blnfiltrePersonne + "&Titre=" + blnfiltreTitre+ "&Orderby="+ strOrderby, false);
    }

    private void Page_Load(object sender, EventArgs e)
    {
        // Établir connection BD
        Label lblMessage = librairie.lblDYN(phDynamique, "message_vignettes", "", "message_vignettes");
        SqlConnection dbConn = new SqlConnection();
        try
        {
            etablitConnexion(ref dbConn, "strConnexionDreamTeam");
            populerListeFilms(dbConn);
        }
        catch (Exception Ex)
        {
            lblMessage.Text = "Oops... Un problème s'est glissé lors du téléchargement des films! (*′☉.̫☉)";
        }

        lstFilmsAfficher = lstFilms.ToList();

        InitialiserOrderBy();
        InitialiserSearch();
        InitialiserNoPage();

        FilterList(lblMessage);
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
    private void AfficherLesFilms(Label lblMessage)
    {
        System.Diagnostics.Debug.Write("afficher");
        Controls.Remove(phDynamique);
        
        if (lstFilmsAfficher.Count() <= 0)
        {
            Label lblTitre = librairie.lblDYN(lblMessage, "lblvide", "Il n'y a aucun film ! (◕‿◕✿)");
        }
        else
        {
            int indexVignette = 0;
            int numRow = 1;

            Panel rowPagerUp = librairie.divDYN(phDynamique, "row_up" + numRow, "row");
            Panel row = row = librairie.divDYN(phDynamique, "row_" + numRow, "row");

            AfficherPager(rowPagerUp);
            for (int i = ((noPage - 1) * nbElementsParPage); i < (noPage * nbElementsParPage) && i < lstFilmsAfficher.Count; i++)
            {
                if (indexVignette % 4 == 0)
                {
                    numRow++;
                    row = row = librairie.divDYN(phDynamique, "row_" + numRow, "row");
                }

                AfficherFilm(i, row);

                indexVignette++;
            }
            row = row = librairie.divDYN(phDynamique, "row_" + numRow, "row");
            AfficherPager(row);
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
    private void AfficherFilm(int i, Control row)
    {
        Panel col = librairie.divDYN(row, "col_" + lstFilmsAfficher[i].NoFilm, "col-sm-3");
        Panel panel = librairie.divDYN(col, "panel_" + lstFilmsAfficher[i].NoFilm, "panel panel-default");

        Panel panelHeader = librairie.divDYN(panel, "panel-heading" + lstFilmsAfficher[i].NoFilm, "panel-heading");
        Label lblTitre = librairie.lblDYN(panelHeader, "titre-film" + lstFilmsAfficher[i].NoFilm, lstFilmsAfficher[i].TitreFrancais, "titre-film");

        Panel panelBody = librairie.divDYN(panel, "panel-body_" + lstFilmsAfficher[i].NoFilm, "panel-body vignette");
        Panel panelCache = librairie.divDYN(panelBody, "panel-cache_" + lstFilmsAfficher[i].NoFilm, "boutons-caches");

        if (HttpContext.Current.User.Identity.Name == lstFilmsAfficher[i].NomUtilisateur) afficherOptionPropreFilm(i, panelCache);
        else afficherOptionsAutreFilm(i, panelCache);

        System.Web.UI.WebControls.Image img = librairie.imgDYN(panelBody, "img_" + lstFilmsAfficher[i].NoFilm, lstFilmsAfficher[i].ImagePochette, "image-vignette");

        Panel panelFooter = librairie.divDYN(panel, "panel-footer_" + lstFilmsAfficher[i].NoFilm, "panel-footer");
        Label lblProprietaire = librairie.lblDYN(panelFooter, "titre-proprietaire_" + lstFilmsAfficher[i].NoFilm, "Propriétaire : "+ lstFilmsAfficher[i].NomUtilisateur, "titre-film");
    }
    private void afficherOptionPropreFilm(int i, Control panelCache)
    {
        Table table = librairie.tableDYN(panelCache, "table_" + lstFilmsAfficher[i].NoFilm, "tableau-boutons");

        TableRow tr1 = librairie.trDYN(table);
        TableCell td1 = librairie.tdDYN(tr1, "td_affichage_detaillee_" + lstFilmsAfficher[i].NoFilm, "");
        Button btn1 = librairie.btnDYN(td1, "affichage_detaillee_" + lstFilmsAfficher[i].NoFilm, "btn btn-default boutons-options-film", "Affichage détaillée");
        btn1.Click += new EventHandler(AfficherDetails);

        TableRow tr2 = librairie.trDYN(table);
        TableCell td2 = librairie.tdDYN(tr2, "td_modifier_" + lstFilmsAfficher[i].NoFilm, "");
        Button btn2 = librairie.btnDYN(td2, "modifier_" + lstFilmsAfficher[i].NoFilm, "btn btn-default boutons-options-film", "Modifier");
        btn2.Click += new EventHandler(modifieronClick);

        TableRow tr3 = librairie.trDYN(table);
        TableCell td3 = librairie.tdDYN(tr3, "td_supprimer_" + lstFilmsAfficher[i].NoFilm, "");
        Button btn3 = librairie.btnDYN(td3, "supprimer_" + lstFilmsAfficher[i].NoFilm, "btn btn-default boutons-options-film", "Supprimer");
    }
    private void afficherOptionsAutreFilm(int i, Panel panelCache)
    {
        panelCache.BackColor = Color.Orange;
        Table table = librairie.tableDYN(panelCache, "table_" + lstFilmsAfficher[i].NoFilm, "tableau-boutons");

        TableRow tr1 = librairie.trDYN(table);
        TableCell td1 = librairie.tdDYN(tr1, "td_affichage_detaillee_" + lstFilmsAfficher[i].NoFilm, "");
        Button btn1 = librairie.btnDYN(td1, "affichage_detaillee_" + lstFilmsAfficher[i].NoFilm, "btn btn-default boutons-options-film", "Affichage détaillée");
        btn1.Click += new EventHandler(AfficherDetails);

        TableRow tr2 = librairie.trDYN(table);
        TableCell td2 = librairie.tdDYN(tr2, "td_approprier_" + lstFilmsAfficher[i].NoFilm, "");
        Button btn2 = librairie.btnDYN(td2, "approprier_" + lstFilmsAfficher[i].NoFilm, "btn btn-default boutons-options-film", "S'approprier");
        btn1.Click += new EventHandler(ApproprierDVD);

        TableRow tr3 = librairie.trDYN(table);
        TableCell td3 = librairie.tdDYN(tr3, "td_message_" + lstFilms[i].NoFilm, "");
        Button btn3 = librairie.btnDYN(td3, "message_" + lstFilms[i].NoFilm, "btn btn-default boutons-options-film", "Envoyer un message");
        btn1.Click += new EventHandler(EnvoyerUnCourriel);
    }
    public void modifieronClick(Object sender, EventArgs e)
    {
        Button btn = (Button)sender;
        System.Diagnostics.Debug.WriteLine("Modifier: " + btn.ID);
        String url = "~/Pages/ModifierFilm.aspx";
        Response.Redirect(url, true);
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