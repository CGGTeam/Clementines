using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Description résumée de EntiteFilm
/// </summary>
public class EntiteProducteur
{
   public int NoProducteur { get; set; }
   public String Nom { get; set; }
  

   public EntiteProducteur (int noProducteur, String nom)
    {
        NoProducteur = noProducteur;
        Nom = nom;
    }

}