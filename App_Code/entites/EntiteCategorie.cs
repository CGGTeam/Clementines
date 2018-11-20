using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Description résumée de EntiteCategorie
/// </summary>
public class EntiteCategorie
{
    public int NoCategorie { get; set;}
    public string Description { get; set; }
    public EntiteCategorie(int noCategorie, string description)
    {
        NoCategorie = noCategorie;
        Description = description;
    }
}