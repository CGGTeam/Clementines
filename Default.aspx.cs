using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _Default :  System.Web.UI.Page
{
    private librairie lib = new librairie();
    private List<String> films = new List<String>();

    protected void Page_Load(object sender, EventArgs e)
    {
        films.Add("Film1");
        films.Add("Film2");
        films.Add("Film3");
        films.Add("Film4");
        films.Add("Film5");
        films.Add("Film6");
        films.Add("Film7");
        films.Add("Film8");
        films.Add("Film9");
        films.Add("Film10");

        Panel row = lib.divDYN(phDynamique, "row", "row");
        Panel col1 = lib.divDYN(row, "col1", "col-sm-6");
        for (int i = 0; i < films.Count/2; i++)
        {
            afficherFilm(films[i], col1);
        }
        Panel col2 = lib.divDYN(row, "col2", "col-sm-6");
        for (int i = films.Count / 2; i < films.Count; i++)
        {
            afficherFilm(films[i], col2);
        }
        afficherPager(phDynamique);
    }

    protected void afficherFilm(String film, Control container)
    {
        Panel div = lib.divDYN(container, film, "panel panel-warning");
        Panel header = lib.divDYN(div, "header" + film, "panel-heading");
        Panel content = lib.divDYN(div, "content" + film, "panel-body");
        Label lblHeader = lib.lblDYN(header, "txtHeader" + film, film);
        Label lblContent = lib.lblDYN(content, "txtContent" + film, film);
    }

    protected void afficherPager(Control container)
    {
        /*
         *Next and Previous Control 
         */

    }




}