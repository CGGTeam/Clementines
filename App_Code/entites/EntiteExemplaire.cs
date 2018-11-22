using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Description résumée de EntiteExemplaire
/// </summary>
public class EntiteExemplaire
{
   public EntiteFilm film { get; set; }
   public EntiteUtilisateur proprietaire { get; set; }


   public EntiteExemplaire()
   {
      
   }
}
