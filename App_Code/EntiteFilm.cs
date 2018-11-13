using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Description résumée de EntiteFilm
/// </summary>
public class EntiteFilm
{
   public int id { get; set; }
   public string nom { get; set; }
   public string vignette { get; set; }
   public string personne { get; set; }

   public EntiteFilm(int id, string nom, string vignette, string personne)
   {
      this.id = id;
      this.nom = nom;
      this.vignette = vignette;
      this.personne = personne;
   }
}