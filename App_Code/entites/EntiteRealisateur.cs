using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Description résumée de EntiteFilm
/// </summary>
public class EntiteRealisateur
{
   public int NoRealisateur { get; set; }
   public String Nom { get; set; }
  

   public EntiteRealisateur (int noRealisateur, String nom)
    {
        NoRealisateur = noRealisateur;
        Nom = nom;
    }

}