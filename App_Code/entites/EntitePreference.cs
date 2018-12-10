using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for EntitePreference
/// </summary>
public class EntitePreference
{
    
    public string CouleurFond { get; set; }  //1
    public string CouleurTexte { get; set; } //2
    public bool CourrielSiAjout { get; set; }//3
    public bool CourrielSiAppropriation { get; set; }//4
    public bool CourrielSiSuppression { get; set; }//5
    public string ImageFond { get; set; }//6
    public int NbFilmParPage { get; set; }//7
    public EntitePreference()
    {
        CouleurFond = "#ffffff";
        CouleurTexte = "#00000";
        CourrielSiAjout = true;
        CourrielSiAppropriation = true;
        CourrielSiSuppression = true;
        ImageFond = "";
        NbFilmParPage = 10;
    }
}