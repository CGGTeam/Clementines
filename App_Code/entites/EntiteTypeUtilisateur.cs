using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for EntiteTypeAbonnement
/// </summary>
public class EntiteTypeAbonnement
{
   public char TypeUtilisateur;
   public string Description;
   public EntiteTypeAbonnement(char typeUtilisateur, string description)
   {
      TypeUtilisateur = typeUtilisateur;
      Description = description;
   }
   public override string ToString()
   {
      return Description;
   }
}