﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_DVDEnMain : System.Web.UI.Page
{
   List<EntiteExemplaire> lstExemplaires = new List<EntiteExemplaire>();
   private int nbVignettesParPage = 10; // {valeur déterminé dans les préférences de l'utilisateur}
   private int noUtilisateurCourrant = 3; // {valeur déterminé lors de la connexion}
   private int pageCourante;

    private void Refresh(object sender, EventArgs e)
    {
        phVignettes.Controls.Clear();
        phChangerPageHaut.Controls.Clear();
        phChangerPage.Controls.Clear();
        Page_Load(sender, e);
    }
    protected void Page_Load(object sender, EventArgs e)
   {
        //subscribe au event d'ajout film
        Button btnEnregistrer = (Button)filmAbrege.FindControl("btnEnregistrer");
        btnEnregistrer.Click += new EventHandler(Refresh);

        string utilisateur = HttpContext.Current.User.Identity.Name;
        noUtilisateurCourrant = SQL.FindNoUtilisateurByName(utilisateur);

        // initialiser les préférences de nb films par page
        EntitePreference pref = SQL.GetPreferenceByNoUtilisateur(noUtilisateurCourrant);
        nbVignettesParPage = pref.NbFilmParPage;

        // initialiser label pour message erreur et autres
        Label lblMessage = librairie.lblDYN(phVignettes, "message_vignettes", "", "message_vignettes");
      
        populerListeFilms();

        // Vérifier la page courante
        initialiserNoPage();
        afficherPageVignettes(lblMessage);
   }

   public void afficherPageVignettes(Label lblMessage)
   {
        if (lstExemplaires.Count == 0)
        {
            lblMessage.Text = "Vous avez ajouté aucun film ! (◕‿◕✿)";
        }
        else
        {
            LoadFilms();
        }
   }
    public void LoadFilms()
    {

        int indexVignette = 0;
        int numRow = 1;
        Panel row = row = librairie.divDYN(phVignettes, "row_" + numRow, "row");

        for (int i = ((pageCourante - 1) * nbVignettesParPage); i < (pageCourante * nbVignettesParPage) && i < lstExemplaires.Count; i++)
        {
            if (indexVignette % 4 == 0)
            {
                numRow++;
                row = row = librairie.divDYN(phVignettes, "row_" + numRow, "row");
            }
            Panel col = librairie.divDYN(row, "col_" + lstExemplaires[i].film.NoFilm, "col-sm-3");
            Panel panel = librairie.divDYN(col, "panel_" + lstExemplaires[i].film.NoFilm, "panel panel-default");
            Panel panelBody = librairie.divDYN(panel, "panel-body_" + lstExemplaires[i].film.NoFilm, "panel-body vignette");
            Panel panelCache = librairie.divDYN(panelBody, "panel-cache_" + lstExemplaires[i].film.NoFilm, "boutons-caches");
            Table table = librairie.tableDYN(panelCache, "table_" + lstExemplaires[i].film.NoFilm, "tableau-boutons");

            TableRow tr1 = librairie.trDYN(table);
            TableCell td1 = librairie.tdDYN(tr1, "td_affichage_detaillee_" + lstExemplaires[i].film.NoFilm, "");
            Button btn1 = librairie.btnDYN(td1, "affichage_detaillee_" + lstExemplaires[i].film.NoFilm, "btn btn-default boutons-options-film", "Affichage détaillée");
            btn1.Click += new EventHandler(affichageDetailleonClick);

            TableRow tr2 = librairie.trDYN(table);
            TableCell td2 = librairie.tdDYN(tr2, "td_modifier_" + lstExemplaires[i].film.NoFilm, "");
            Button btn2 = librairie.btnDYN(td2, "modifier_" + lstExemplaires[i].film.NoFilm, "btn btn-default boutons-options-film", "Modifier");
            btn2.Click += new EventHandler(modifieronClick);

            TableRow tr3 = librairie.trDYN(table);
            TableCell td3 = librairie.tdDYN(tr3, "td_supprimer_" + lstExemplaires[i].film.NoFilm, "");
            Button btn3 = librairie.btnDYN(td3, "supprimer_" + lstExemplaires[i].film.NoFilm, "btn btn-default boutons-options-film", "Supprimer");
            btn3.Click += new EventHandler(supprimerOnClick);

            Image img = librairie.imgDYN(panelBody, "img_" + lstExemplaires[i].film.NoFilm, lstExemplaires[i].film.ImagePochette, "image-vignette");

            Panel panelFooter = librairie.divDYN(panel, "panel-footer_" + lstExemplaires[i].film.NoFilm, "panel-footer");
            Label lblTitre = librairie.lblDYN(panelFooter, "titre-film_" + lstExemplaires[i].film.NoFilm, lstExemplaires[i].film.TitreFrancais, "titre-film");

            indexVignette++;
        }

        Panel row2 = librairie.divDYN(phChangerPage, "rowChangerPage", "row");

        afficherPager(phChangerPage);
        afficherPager(phChangerPageHaut);
    }

   private void initialiserNoPage()
   {
      int n;
      if (Request.QueryString["Page"] == null || !int.TryParse(Request.QueryString["Page"], out n))
      {
         this.pageCourante = 1;
      }
      else
      {
         this.pageCourante = n;
      }
   }

   private void afficherPager(Control control)
   {
      LiteralControl pager = new LiteralControl();
      decimal nbPages = Math.Ceiling((decimal)lstExemplaires.Count / (decimal)nbVignettesParPage);
        if (nbPages > 1)
        {
            int previous = pageCourante - 1;
            string strClass = previous <= 0 ? "page-item disabled" : "page-item";
            previous = previous <= 0 ? pageCourante : previous;

            string strDebut = "<nav class='text-center'>" +
                                    "<ul class='pagination justify-content-center'>" +
                                        "<li class='" + strClass + "'><a class='page-link' href='?Page=" + previous + "'> <span class='glyphicon glyphicon-chevron-left'></a></li>";

            pager.Text += strDebut;
            for (int i = 1; i <= nbPages; i++)
            {
                strClass = pageCourante == i ? "page-item active" : "page-item";
                string strMillieu = "<li class='" + strClass + "'><a class='page - link' href='?Page=" + i + "'>" + i + "</a></li>";
                pager.Text += strMillieu;
            }

            int next = pageCourante + 1;
            strClass = next >= nbPages + 1 ? "page-item disabled" : "page-item";
            next = next >= nbPages + 1 ? pageCourante : next;

            string strFin = "<li class='" + strClass + "'><a class='page-link' href='?Page=" + next + "'><span class='glyphicon glyphicon-chevron-right'></a></li>" +
                                    "</ul>" +
                                "</nav>";
            pager.Text += strFin;

            control.Controls.Add(pager);
        }
   }

   public void affichageDetailleonClick(Object sender, EventArgs e)
   {
      Button btn = (Button)sender;
      String url = "~/Pages/AffichageDetaille.aspx?Film=" + btn.ID.Replace("affichage_detaillee_", "");
      Response.Redirect(url, true);
   }

   public void modifieronClick(Object sender, EventArgs e)
   {
      Button btn = (Button)sender;
      String url = "~/Pages/ModifierFilm.aspx?Film=" + btn.ID.Replace("modifier_", "");
      Response.Redirect(url, true);
   }

    public void supprimerOnClick(Object sender, EventArgs e)
    {
        Button btn = (Button)sender;
        String url = "~/Pages/SupprimerDVD.aspx?Film=" + btn.ID.Replace("supprimer_", "");
        Response.Redirect(url, true);
    }


    public void populerListeFilms()
   {
      
      lstExemplaires = SQL.FindAllUserExemplairesEmpruntes(noUtilisateurCourrant);
   }



}