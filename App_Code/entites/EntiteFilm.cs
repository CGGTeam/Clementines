using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Description résumée de EntiteFilm
/// </summary>
public class EntiteFilm
{
   public int NoFilm { get; set; }
   public int AnneeSortie { get; set; }
   public string Categorie { get; set; }
   public string Format { get; set; }
   public DateTime DateMAJ { get; set; }
   public string NomUtilisateur { get; set; }
   public string Resume { get; set; }
   public int Duree { get; set; }
   public bool FilmOriginal { get; set; }
   public string ImagePochette { get; set; }
   public int NbDisques { get; set; }
   public string TitreFrancais { get; set; }
   public string TitreOriginal { get; set; }
   public bool VersionEtendue { get; set; }
   public string NomRealisateur { get; set; }
   public string NomProducteur { get; set; }
   public string LienInternet { get; set; }

   public List<EntiteActeur> lstActeurs { get; set; }
   public List<EntiteLangue> lstLangues { get; set; }
   public List<EntiteSousTitres> lstSousTitres { get; set; }
   public List<EntiteSupplements> lstSupplements { get; set; }



   public EntiteFilm(int noFilm, int anneeSortie, string categorie, string format, DateTime dateMAJ, string nomUtilisateur, string resume, int duree, bool filmOriginal, string imagePochette, int nbDisques, string titreFrancais, string titreOriginal, bool versionEtendue, string nomRealisateur, string nomProducteur, string lienInternet)
   {
      NoFilm = noFilm;
      AnneeSortie = anneeSortie;
      Categorie = categorie;
      Format = format;
      DateMAJ = dateMAJ;
      NomUtilisateur = nomUtilisateur;
      Resume = resume;
      Duree = duree;
      FilmOriginal = filmOriginal;
      ImagePochette = imagePochette;
      NbDisques = nbDisques;
      TitreFrancais = titreFrancais;
      TitreOriginal = titreOriginal;
      VersionEtendue = versionEtendue;
      NomRealisateur = nomRealisateur;
      NomProducteur = nomProducteur;
      LienInternet = lienInternet;
   }
}