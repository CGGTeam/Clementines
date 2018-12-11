using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for EntiteUtilisateur
/// </summary>
public class EntiteUtilisateur
{
    public int NoUtilisateur { get; set; }
    public string NomUtilisateur { get; set; }
    public string Courriel { get; set; }
    public int MotPasse { get; set; }
    public char TypeUtilisateur { get; set; }

    public EntiteUtilisateur(int no, string nom, string courriel, int mdp, char type)
    {
        NoUtilisateur = no;
        NomUtilisateur = nom;
        Courriel = courriel;
        MotPasse = mdp;
        TypeUtilisateur = type;
    }
}