using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _Default : System.Web.UI.Page
{
    private int nbElementsParPage = 10;
    private int noPage = 1;
    private string filtre;

    private List<Film> filmsOG = new List<Film>();
    private List<Film> films = new List<Film>();

    private Panel col1;
    private Panel col2;

    private Panel row2;

    private class Film
    {
        public string nom { get; set; }
        public string vignette { get; set; }
        public string personne { get; set; }

        public Film(string nom, string vignette, string personne)
        {
            this.nom = nom;
            this.vignette = vignette;
            this.personne = personne;
        }
    }
    public void FilterList()
    {
        InitialiserSearch();

        bool rechercheParTitre = cbTitre.Checked;
        bool rechercheParPersonne = cbPersonne.Checked;

        films.Clear();
        if (rechercheParTitre && rechercheParPersonne)
            films = filmsOG.Where(film => film.nom.ToLower().Contains(filtre.ToLower()) || film.personne.ToLower().Contains(filtre.ToLower())).ToList();
        else if (rechercheParPersonne)
            films = filmsOG.Where(film => film.personne.ToLower().Contains(filtre.ToLower())).ToList();
        else if (rechercheParTitre)
            films = filmsOG.Where(film => film.nom.ToLower().Contains(filtre.ToLower())).ToList();
        else
            films = filmsOG.ToList();

        AfficherLesFilms(col1, col2, row2);

    }
    public void UpdateFiltre(object sender, EventArgs e)
    {
        string strFiltre = tbRecherche.Text;
        System.Diagnostics.Debug.WriteLine("in");
        Response.Redirect("~/Pages/Accueil.aspx?Page=1&Filtre=" + strFiltre, false);
    }

    private void Page_Load(object sender, EventArgs e)
    {
        //if (!Page.IsPostBack) { 


            string img = "../Static/images/flavicon.png";
            for (int i = 1; i <= 32; i++)
            {
                filmsOG.Add(new Film("Film" + i, img, "Personne" + i));
            }
            films = filmsOG.ToList();
        //}
        InitialiserCheckBoxState();
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
    }
    private void AfficherLesFilms(Control col1, Control col2, Control row2)
    {
        col1.Controls.Clear();
        col2.Controls.Clear();
        row2.Controls.Clear();

        for (int i = (noPage * nbElementsParPage) - (nbElementsParPage - 1); i <= nbElementsParPage * noPage && i <= films.Count(); i++)
        {
            if (i % 2 != 0)
                AfficherFilm(films[i - 1], col1);
            else
                AfficherFilm(films[i - 1], col2);
        }
        AfficherPager(row2);
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
    }
    public void Check(object sender, EventArgs e)
    {
        CheckBox cb = (CheckBox)sender;
        System.Diagnostics.Debug.WriteLine(cb.ID);
        if (cb == cbPersonne)
        {
            hfPersonne.Value = cbPersonne.Checked ? "true" : "false";
        }
        else if (cb == cbTitre)
        {
            hfTitre.Value = cbTitre.Checked ? "true" : "false";
        }
        System.Diagnostics.Debug.WriteLine(hfPersonne.Value);
    }
    private void InitialiserCheckBoxState()
    {
        cbPersonne.Checked = Convert.ToBoolean(hfPersonne.Value);
        cbTitre.Checked = Convert.ToBoolean(hfTitre.Value);
    }
    private void AfficherFilm(Film film, Control container)
    {
        Panel div = librairie.divDYN(container, film.nom, "panel panel-warning");
        Panel header = librairie.divDYN(div, "header" + film.nom, "panel-heading");
        Panel content = librairie.divDYN(div, "content" + film.nom, "panel-body vignette");


        Panel panelCache = librairie.divDYN(content, "panel-cache_" + film.nom, "boutons-caches row ");

        Button btn1 = librairie.btnDYN(panelCache, "courriel_" + film.nom, "btn btn-sm btn-default boutons-options-film col-xs-6 pull-right", "Envoyer un courriel à " + film.personne);
        btn1.Click += new EventHandler(EnvoyerUnCourriel);
        Button btn2 = librairie.btnDYN(panelCache, "affichage_detaillee_" + film.nom, "btn btn-sm btn-default boutons-options-film col-xs-6 pull-right", "Affichage détaillée de " + film.nom);
        btn2.Click += new EventHandler(AfficherDetails);
        Button btn3 = librairie.btnDYN(panelCache, "approprier" + film.nom, "btn btn-sm btn-default boutons-options-film col-xs-6 pull-right", "S'approprier le " + film.nom);
        btn3.Click += new EventHandler(ApproprierDVD);

        Image img = librairie.imgDYN(content, "img" + film.nom, film.vignette, ".img-rounded col-sm-2");
        Panel divProprietaire = librairie.divDYN(content, film.nom + "Personne", "pull-right");
        librairie.brDYN(divProprietaire);
        Label lblPersonne = librairie.lblDYN(divProprietaire, "lblPersonne" + film.nom, film.personne);
        Label lblTitre = librairie.lblDYN(header, "lbl" + film.nom, film.nom);
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
        decimal nbPages = Math.Ceiling((decimal)films.Count / (decimal)nbElementsParPage);

        string strFiltre = filtre == "" ? "" : "&Filtre=" + filtre;

        int previous = noPage - 1;
        string strClass = previous <= 0 ? "page-item disabled" : "page-item";
        previous = previous <= 0 ? noPage : previous;

        string strDebut = "<nav aria - label = 'Page navigation example' >" +
                                "<ul class='pagination justify-content-center'>" +
                                    "<li class='" + strClass + "'><a class='page-link' href='?Page=" + previous + strFiltre + "'> Previous</a></li>";

        pager.Text += strDebut;
        for (int i = 1; i <= nbPages; i++)
        {
            strClass = noPage == i ? "page-item active" : "page-item";
            string strMillieu = "<li class='" + strClass + "'><a class='page - link' href='?Page=" + i + strFiltre + "'>" + i + "</a></li>";
            pager.Text += strMillieu;
        }

        int next = noPage + 1;
        strClass = next >= nbPages + 1 ? "page-item disabled" : "page-item";
        next = next >= nbPages + 1 ? noPage : next;

        string strFin = "<li class='" + strClass + "'><a class='page-link' href='?Page=" + next + strFiltre + "'>Next</a></li>" +
                                "</ul>" +
                            "</nav>";
        pager.Text += strFin;

        control.Controls.Add(pager);
    }

    protected void trierTitre(object sender, EventArgs e)
    {
        films.OrderBy(film => film.nom).ToList();
    }
    protected void trierPersonne(object sender, EventArgs e)
    {

    }
    protected void TrierLesDeux(object sender, EventArgs e)
    {

    }
}