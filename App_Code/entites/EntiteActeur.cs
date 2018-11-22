using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Description résumée de EntiteActeur
/// </summary>
public class EntiteActeur
{
    public int NoActeur {get; set;}
    public string Nom { get; set;}
    public string Sexe { get; set;}
    public EntiteActeur(int noActeur, string nom, string sexe)
    {
        NoActeur = noActeur;
        Nom = nom;
        Sexe = sexe;
    }

    public override string ToString()
    {
        return Nom;
    }
}