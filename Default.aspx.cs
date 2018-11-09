using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _Default : System.Web.UI.Page
{
    protected int nbElementsParPage = 12;
    protected int noPage = 1;

    private librairie lib = new librairie();
    private List<Film> films = new List<Film>();

    protected class Film {
        public string nom  {get; private set; }
        public string vignette { get; private set; }
        public string personne { get; private set; }

    public Film(string nom, string vignette, string personne)
        {
            this.nom = nom;
            this.vignette = vignette;
            this.personne = personne;
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        string img = "Static/images/flavicon.png";
        initialiserNoPage();

        for (int i = 1; i<= 32; i++)
        {
            films.Add(new Film("Film" + i, img, "Personne"+i));
        }

        Panel row = lib.divDYN(phDynamique, "row", "row");
        Panel col1 = lib.divDYN(row, "col1", "col-sm-6");
        Panel col2 = lib.divDYN(row, "col2", "col-sm-6");
        for (int i = (noPage * nbElementsParPage) - (nbElementsParPage-1); i <= nbElementsParPage* noPage && i <= films.Count(); i++)
        {
            if(i % 2 != 0)
                afficherFilm(films[i-1], col1);
            else
                afficherFilm(films[i-1], col2);
        }

        Panel row2 = lib.divDYN(phDynamique, "row2", "row");
        afficherPager(row2);
    }
    protected void initialiserNoPage()
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

    protected void afficherFilm(Film film, Control container)
    {
        Panel div = lib.divDYN(container, film.nom, "panel panel-warning");
        Panel header = lib.divDYN(div, "header" + film.nom, "panel-heading");
        Panel content = lib.divDYN(div, "content" + film.nom, "panel-body");
        Label lblHeader = lib.lblDYN(header, "lbl" + film.nom, film.nom);
        Image img = lib.imgDYN(content, "img" + film.nom, film.vignette, ".img-rounded col-sm-2");
        Label lblContent = lib.lblDYN(content, "lblPersonne" + film.nom, film.personne);
    }

    protected void afficherPager(Control control)
    {
        LiteralControl pager = new LiteralControl();
        decimal nbPages = Math.Ceiling((decimal)films.Count / (decimal)nbElementsParPage);

        int previous = noPage - 1;
        string strClass = previous <= 0 ? "page-item disabled" : "page-item";
        previous = previous <= 0 ? noPage : previous;

        string strDebut =  "<nav aria - label = 'Page navigation example' >" +
                                "<ul class='pagination justify-content-center'>" +
                                    "<li class='" + strClass + "'><a class='page-link' href='?Page=" + previous+"'> Previous</a></li>";

        pager.Text += strDebut;
        for(int i = 1; i <= nbPages; i++)
        {
            strClass = noPage == i ? "page-item active" : "page-item";
            string strMillieu = "<li class='"+ strClass + "'><a class='page - link' href='?Page="+ i+"'>"+i+"</a></li>";
            pager.Text += strMillieu;
        }

        int next = noPage + 1;
        strClass = next >= nbPages + 1 ? "page-item disabled" : "page-item";
        next = next >= nbPages + 1 ? noPage : next;

        string strFin = "<li class='" + strClass + "'><a class='page-link' href='?Page=" + next + "'>Next</a></li>" +
                                "</ul>" +
                            "</nav>";
        pager.Text += strFin;

        control.Controls.Add(pager);
    }
}