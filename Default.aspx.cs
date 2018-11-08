using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _Default :  System.Web.UI.Page
{
    List<String> films = new List<String>();

    protected void Page_Load(object sender, EventArgs e)
    {
        films.ForEach(film => {
                afficherFilm(film);
            });
    }

    protected void afficherFilm(String film)
    {

    }




}