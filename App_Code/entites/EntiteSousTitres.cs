using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Description résumée de EntiteSousTitres
/// </summary>
public class EntiteSousTitres
{
    public int NoSousTitre { get; set; }
    public string LangueSousTitre { get; set; }
    public EntiteSousTitres(int noSousTitre, string langueSousTitre)
    {
        NoSousTitre = noSousTitre;
        LangueSousTitre = langueSousTitre;
    }

    public override string ToString()
    {
        return LangueSousTitre;
    }
}