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

    private List<EntiteExemplaire> lstFilms = new List<EntiteExemplaire>();
    private List<EntiteExemplaire> lstFilmsAfficher = new List<EntiteExemplaire>();

    public void FilterList(Label lblMessage)
    {
        lstFilmsAfficher.Clear();
        if (filtreTitre && filtrePersonne)
        {
            lstFilmsAfficher = lstFilms.Where(film => film.film.TitreFrancais.ToLower().Contains(filtre.ToLower()) || film.film.NomUtilisateur.ToLower().Contains(filtre.ToLower())).ToList();
        }
        else if (filtreTitre)
        {
            lstFilmsAfficher = lstFilms.Where(film => film.film.TitreFrancais.ToLower().Contains(filtre.ToLower())).ToList();
        }
        else if (filtrePersonne)
        {
            lstFilmsAfficher = lstFilms.Where(film => film.film.NomUtilisateur.ToLower().Contains(filtre.ToLower())).ToList();
        }
        else
        {
            lstFilmsAfficher = new List<EntiteExemplaire>();
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
        string utilisateur = HttpContext.Current.User.Identity.Name;
        int noUtilisateurCourrant = SQL.FindNoUtilisateurByName(utilisateur);
        EntitePreference mesPreferences = SQL.GetPreferenceByNoUtilisateur(noUtilisateurCourrant);
        nbElementsParPage = mesPreferences.NbFilmParPage;

        Label lblMessage = librairie.lblDYN(phDynamique, "message_vignettes", "", "message_vignettes");
        //if (!Page.IsPostBack)
        //{
            // Établir connection BD
            
            try
            {
                SQL.Connection();
                lstFilms = SQL.FindAllExemplairesEmpruntes();
            }
            catch (Exception Ex)
            {
                lblMessage.Text = "Oops... Un problème s'est glissé lors du téléchargement des films! (*′☉.̫☉)";
            }

            lstFilmsAfficher = lstFilms.ToList();
        //}
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
            row = row = librairie.divDYN(phDynamique, "row_" + ++numRow, "row");
            AfficherPager(row);
        }
    }
    private void OrderBy()
    {
        switch (orderBy)
        {
            case TypeOrderBy.TitrePersonne:
                lstFilmsAfficher.OrderBy(film => film.film.TitreFrancais).ThenBy(s => s.proprietaire.NomUtilisateur);
                break;
            case TypeOrderBy.Personne:
                lstFilmsAfficher.Sort((x, y) => x.proprietaire.NomUtilisateur.CompareTo(y.proprietaire.NomUtilisateur));
                break;
            case TypeOrderBy.Titre:
                lstFilmsAfficher.Sort((x, y) => x.film.TitreFrancais.CompareTo(y.film.TitreFrancais));
                break;
            default:
                lstFilmsAfficher.OrderBy(film => film.film.TitreFrancais).ThenBy(s => s.proprietaire.NomUtilisateur);
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
        Panel col = librairie.divDYN(row, "col_" + lstFilmsAfficher[i].film.NoFilm, "col-sm-3");
        Panel panel = librairie.divDYN(col, "panel_" + lstFilmsAfficher[i].film.NoFilm, "panel panel-default");

        Panel panelHeader = librairie.divDYN(panel, "panel-heading" + lstFilmsAfficher[i].film.NoFilm, "panel-heading");
        Label lblTitre = librairie.lblDYN(panelHeader, "titre-film" + lstFilmsAfficher[i].film.NoFilm, lstFilmsAfficher[i].film.TitreFrancais, "titre-film");

        Panel panelBody = librairie.divDYN(panel, "panel-body_" + lstFilmsAfficher[i].film.NoFilm, "panel-body vignette");
        Panel panelCache = librairie.divDYN(panelBody, "panel-cache_" + lstFilmsAfficher[i].film.NoFilm, "boutons-caches");

        string utilisateur = HttpContext.Current.User.Identity.Name;
        EntiteUtilisateur user = SQL.FindUtilisateurByName(utilisateur);
        string proprietaire = lstFilmsAfficher[i].proprietaire.NomUtilisateur;

        if(user.TypeUtilisateur == 'A') afficherOptionsAdmin(i, panelCache);
        else if(user.TypeUtilisateur == 'S') afficherOptionSuperUtilisateur(i, panelCache, utilisateur, proprietaire);
        else if (utilisateur.Trim().Equals(proprietaire.Trim())) afficherOptionPropreFilm(i, panelCache);
        else afficherOptionsAutreFilm(i, panelCache);

        System.Web.UI.WebControls.Image img = librairie.imgDYN(panelBody, "img_" + lstFilmsAfficher[i].film.NoFilm, lstFilmsAfficher[i].film.ImagePochette, "image-vignette");

        Panel panelFooter = librairie.divDYN(panel, "panel-footer_" + lstFilmsAfficher[i].film.NoFilm, "panel-footer");
        Label lblProprietaire = librairie.lblDYN(panelFooter, "titre-proprietaire_" + lstFilmsAfficher[i].film.NoFilm, "Propriétaire : "+ lstFilmsAfficher[i].film.NomUtilisateur, "titre-film");
    }
    private void afficherOptionSuperUtilisateur(int i, Panel panelCache, string user, string proprietaire)
    {
        Table table = librairie.tableDYN(panelCache, "table_" + lstFilmsAfficher[i].film.NoFilm, "tableau-boutons");

        if (!user.Trim().Equals(proprietaire.Trim())){
            panelCache.Attributes.Add("style", "background:rgba(240, 168, 60, 0.83);");

            TableRow tr4 = librairie.trDYN(table);
            TableCell td4 = librairie.tdDYN(tr4, "td_approprier_" + lstFilmsAfficher[i].film.NoFilm, "");
            Button btn4 = librairie.btnDYN(td4, "approprier_" + lstFilmsAfficher[i].film.NoFilm, "btn btn-default boutons-options-film", "S'approprier");
            btn4.Click += new EventHandler(ApproprierDVD);

            TableRow tr5 = librairie.trDYN(table);
            TableCell td5 = librairie.tdDYN(tr5, "td_message_" + lstFilms[i].film.NoFilm, "");
            Button btn5 = librairie.btnDYN(td5, "message" + lstFilmsAfficher[i].film.NoFilm + "_" + lstFilmsAfficher[i].proprietaire.NomUtilisateur, "btn btn-default boutons-options-film", "Envoyer un message");
            btn5.Click += new EventHandler(EnvoyerUnCourriel);
        }
        
        TableRow tr1 = librairie.trDYN(table);
        TableCell td1 = librairie.tdDYN(tr1, "td_affichage_detaillee_" + lstFilmsAfficher[i].film.NoFilm, "");
        Button btn1 = librairie.btnDYN(td1, "affichage_detaillee_" + lstFilmsAfficher[i].film.NoFilm, "btn btn-default boutons-options-film", "Affichage détaillée");
        btn1.Click += new EventHandler(AfficherDetails);

        TableRow tr2 = librairie.trDYN(table);
        TableCell td2 = librairie.tdDYN(tr2, "td_modifier_" + lstFilmsAfficher[i].film.NoFilm, "");
        Button btn2 = librairie.btnDYN(td2, "modifier_" + lstFilmsAfficher[i].film.NoFilm, "btn btn-default boutons-options-film", "Modifier");
        btn2.Click += new EventHandler(modifieronClick);

        TableRow tr3 = librairie.trDYN(table);
        TableCell td3 = librairie.tdDYN(tr3, "td_supprimer_" + lstFilmsAfficher[i].film.NoFilm, "");
        Button btn3 = librairie.btnDYN(td3, "supprimer_" + lstFilmsAfficher[i].film.NoFilm, "btn btn-default boutons-options-film", "Supprimer");
        btn3.Click += new EventHandler(supprimerOnClick);
    }
    private void afficherOptionPropreFilm(int i, Panel panelCache)
    {
        Table table = librairie.tableDYN(panelCache, "table_" + lstFilmsAfficher[i].film.NoFilm, "tableau-boutons");

        TableRow tr1 = librairie.trDYN(table);
        TableCell td1 = librairie.tdDYN(tr1, "td_affichage_detaillee_" + lstFilmsAfficher[i].film.NoFilm, "");
        Button btn1 = librairie.btnDYN(td1, "affichage_detaillee_" + lstFilmsAfficher[i].film.NoFilm, "btn btn-default boutons-options-film", "Affichage détaillée");
        btn1.Click += new EventHandler(AfficherDetails);

        TableRow tr2 = librairie.trDYN(table);
        TableCell td2 = librairie.tdDYN(tr2, "td_modifier_" + lstFilmsAfficher[i].film.NoFilm, "");
        Button btn2 = librairie.btnDYN(td2, "modifier_" + lstFilmsAfficher[i].film.NoFilm, "btn btn-default boutons-options-film", "Modifier");
        btn2.Click += new EventHandler(modifieronClick);

        TableRow tr3 = librairie.trDYN(table);
        TableCell td3 = librairie.tdDYN(tr3, "td_supprimer_" + lstFilmsAfficher[i].film.NoFilm, "");
        Button btn3 = librairie.btnDYN(td3, "supprimer_" + lstFilmsAfficher[i].film.NoFilm, "btn btn-default boutons-options-film", "Supprimer");
        btn3.Click += new EventHandler(supprimerOnClick);
    }
    private void afficherOptionsAutreFilm(int i, Panel panelCache)
    {
        panelCache.Attributes.Add("style", "background:rgba(240, 168, 60, 0.83);");
        Table table = librairie.tableDYN(panelCache, "table_" + lstFilmsAfficher[i].film.NoFilm, "tableau-boutons");

        TableRow tr1 = librairie.trDYN(table);
        TableCell td1 = librairie.tdDYN(tr1, "td_affichage_detaillee_" + lstFilmsAfficher[i].film.NoFilm, "");
        Button btn1 = librairie.btnDYN(td1, "affichage_" + lstFilmsAfficher[i].film.NoFilm, "btn btn-default boutons-options-film", "Affichage détaillée");
        btn1.Click += new EventHandler(AfficherDetails);

        TableRow tr2 = librairie.trDYN(table);
        TableCell td2 = librairie.tdDYN(tr2, "td_approprier_" + lstFilmsAfficher[i].film.NoFilm, "");
        Button btn2 = librairie.btnDYN(td2, "approprier_" + lstFilmsAfficher[i].film.NoFilm, "btn btn-default boutons-options-film", "S'approprier");
        btn2.Click += new EventHandler(ApproprierDVD);

        TableRow tr3 = librairie.trDYN(table);
        TableCell td3 = librairie.tdDYN(tr3, "td_message_" + lstFilms[i].film.NoFilm, "");
        Button btn3 = librairie.btnDYN(td3, "message" + lstFilmsAfficher[i].film.NoFilm +"_"+lstFilmsAfficher[i].proprietaire.NomUtilisateur, "btn btn-default boutons-options-film", "Envoyer un message");
        btn3.Click += new EventHandler(EnvoyerUnCourriel);
         
        
    }
    private void afficherOptionsAdmin(int i, Panel panelCache)
    {
        panelCache.Attributes.Add("style", "background:rgba(240, 121, 232, 0.83);");
        Table table = librairie.tableDYN(panelCache, "table_" + lstFilmsAfficher[i].film.NoFilm, "tableau-boutons");

        TableRow tr1 = librairie.trDYN(table);
        TableCell td1 = librairie.tdDYN(tr1, "td_affichage_detaillee_" + lstFilmsAfficher[i].film.NoFilm, "");
        Button btn1 = librairie.btnDYN(td1, "affichage_" + lstFilmsAfficher[i].film.NoFilm, "btn btn-default boutons-options-film", "Affichage détaillée");
        btn1.Click += new EventHandler(AfficherDetails);

        TableRow tr3 = librairie.trDYN(table);
        TableCell td3 = librairie.tdDYN(tr3, "td_message_" + lstFilms[i].film.NoFilm, "");
        Button btn3 = librairie.btnDYN(td3, "message" + lstFilmsAfficher[i].film.NoFilm + "_" + lstFilmsAfficher[i].proprietaire.NomUtilisateur, "btn btn-default boutons-options-film", "Envoyer un message");
        btn3.Click += new EventHandler(EnvoyerUnCourriel);

        TableRow tr2 = librairie.trDYN(table);
        TableCell td2 = librairie.tdDYN(tr2, "td_modifier_" + lstFilmsAfficher[i].film.NoFilm, "");
        Button btn2 = librairie.btnDYN(td2, "modifier_" + lstFilmsAfficher[i].film.NoFilm, "btn btn-default boutons-options-film", "Modifier");
        btn2.Click += new EventHandler(modifieronClick);

        TableRow tr4 = librairie.trDYN(table);
        TableCell td4 = librairie.tdDYN(tr4, "td_supprimer_" + lstFilmsAfficher[i].film.NoFilm, "");
        Button btn4 = librairie.btnDYN(td4, "supprimer_" + lstFilmsAfficher[i].film.NoFilm, "btn btn-default boutons-options-film", "Supprimer");
        btn4.Click += new EventHandler(supprimerOnClick);

    }
    public void supprimerOnClick(Object sender, EventArgs e)
    {
        Button btn = (Button)sender;
        String url = "~/Pages/SupprimerDVD.aspx?Film=" + btn.ID.Replace("supprimer_", "");
        Response.Redirect(url, true);
    }
    public void modifieronClick(Object sender, EventArgs e)
    {
        Button btn = (Button)sender;
        String url = "~/Pages/ModifierFilm.aspx?Film=" + btn.ID.Replace("modifier_", "");
        Response.Redirect(url, true);
    }
    public void AfficherDetails(object sender, EventArgs e)
    {
        Button btn = (Button)sender;
        String id = btn.ID.Substring(btn.ID.LastIndexOf('_') + 1);

        String url = "~/Pages/AffichageDetaille.aspx?Film="+ id;
        Response.Redirect(url, true);
    }
    public void ApproprierDVD(object sender, EventArgs e)
    {
        Button btn = (Button)sender;
        String url = "~/Pages/AppropriationDVD.aspx?Film=" + btn.ID.Replace("approprier_", "");
        Response.Redirect(url, true);
    }
    private void EnvoyerUnCourriel(object sender, EventArgs e)
    {
        Button btn = (Button)sender;
        String destinataire = btn.ID.Substring(btn.ID.LastIndexOf('_') + 1);

        String url = "~/Pages/Courriel.aspx?Destinataire="+ destinataire;
        Response.Redirect(url, true);
    }

    private void AfficherPager(Control control)
    {
        LiteralControl pager = new LiteralControl();
        decimal nbPages = Math.Ceiling((decimal)lstFilmsAfficher.Count / (decimal)nbElementsParPage);
        if (nbPages > 1)
        {
            string strFiltreComplet = "&Filtre=" + filtre + "&Personne=" + filtrePersonne + "&Titre=" + filtreTitre + "Orderby=" + orderBy.ToString();

            int previous = noPage - 1;
            string strClass = previous <= 0 ? "page-item disabled" : "page-item";
            previous = previous <= 0 ? noPage : previous;

            string strDebut = "<nav class='text-center'>" +
                                    "<ul class='pagination justify-content-center'>" +
                                        "<li class='" + strClass + "'><a class='page-link' href='?Page=" + previous + strFiltreComplet + "'> <span class='glyphicon glyphicon-chevron-left'></a></li>";

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

            string strFin = "<li class='" + strClass + "'><a class='page-link' href='?Page=" + next + strFiltreComplet + "'><span class='glyphicon glyphicon-chevron-right'></a></li>" +
                                    "</ul>" +
                                "</nav>";
            pager.Text += strFin;

            control.Controls.Add(pager);
        }
    }
}