using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_DVDsAutreUtilisateur : System.Web.UI.Page
{
   List<EntiteExemplaire> lstExemplaires = new List<EntiteExemplaire>();
   private int nbVignettesParPage = 10; // {valeur déterminé dans les préférences de l'utilisateur}
   private int noUtilisateurCourrant = 1; // {valeur déterminé lors de la connexion}
   private int noUtilisateurVisionne; // {valeur déterminé lors de la connexion}
   private int pageCourante;

    EntiteUtilisateur utilCourant;

   protected void Page_Load(object sender, EventArgs e)
   {
      // initialiser label pour message erreur et autres
      Label lblMessage = librairie.lblDYN(phVignettes, "message_vignettes", "", "message_vignettes");

        utilCourant = SQL.FindUtilisateurByName(HttpContext.Current.User.Identity.Name);
        noUtilisateurCourrant = utilCourant.NoUtilisateur;

        // initialiser les préférences de nb films par page
        EntitePreference pref = SQL.GetPreferenceByNoUtilisateur(noUtilisateurCourrant);
        nbVignettesParPage = pref.NbFilmParPage;

        if (!Page.IsPostBack)
      {
            populerDDLUtilisateurs();
            
      }

      initialiserNoUtilisateur();
      populerListeFilms();

      // Vérifier la page courante
      initialiserNoPage();

      afficherPageVignettes(lblMessage);
   }

   protected void Page_LoadComplete(object sender, EventArgs e)
   {
      ddlUtilisateur.SelectedValue = noUtilisateurVisionne.ToString();
   }

   public void afficherPageVignettes(Label lblMessage)
   {
      if (noUtilisateurVisionne != 0)
      {
         EntiteUtilisateur util = SQL.FindUtilisateurById(noUtilisateurVisionne);

         lblNomUtilisateur.Text = "Vous visualisez les DVDs de " + util.NomUtilisateur;
         if (lstExemplaires.Count == 0)
         {
            lblMessage.Text = "L'utilisateur sélectionné n'a aucun film ! (◕‿◕✿)";
         }
         else
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
               TableCell td2 = librairie.tdDYN(tr2, "td_courriel_" + lstExemplaires[i].film.NoFilm, "");
               Button btn2 = librairie.btnDYN(td2, "courriel_" + lstExemplaires[i].film.NoFilm, "btn btn-default boutons-options-film", "Envoyer courriel");
                    btn2.Click += new EventHandler(EnvoyerUnCourriel);
                
                    if (utilCourant.TypeUtilisateur != 'A')
                    {
                        TableRow tr3 = librairie.trDYN(table);
                        TableCell td3 = librairie.tdDYN(tr3, "td_appropriation_" + lstExemplaires[i].film.NoFilm, "");
                        Button btn3 = librairie.btnDYN(td3, "appropriation_" + lstExemplaires[i].film.NoFilm, "btn btn-default boutons-options-film", "Appropriation du DVD");
                        btn3.Click += new EventHandler(appropriationOnClick);
                    }
               
               Image img = librairie.imgDYN(panelBody, "img_" + lstExemplaires[i].film.NoFilm, lstExemplaires[i].film.ImagePochette, "image-vignette");

               Panel panelFooter = librairie.divDYN(panel, "panel-footer_" + lstExemplaires[i].film.NoFilm, "panel-footer");
               Label lblTitre = librairie.lblDYN(panelFooter, "titre-film_" + lstExemplaires[i].film.NoFilm, lstExemplaires[i].film.TitreFrancais, "titre-film");

               indexVignette++;
            }

            Panel row2 = librairie.divDYN(phChangerPage, "rowChangerPage", "row");

            afficherPager(phChangerPage);
         }
      }
      else
      {
         lblNomUtilisateur.Text = "Vous devez d'abord sélectionner un utilisateur ! (◕‿◕✿)";
      }
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

      int previous = pageCourante - 1;
      string strClass = previous <= 0 ? "page-item disabled" : "page-item";
      previous = previous <= 0 ? pageCourante : previous;

      string strDebut = "<nav aria - label = 'Page navigation example' >" +
                              "<ul class='pagination justify-content-center'>" +
                                  "<li class='" + strClass + "'><a class='page-link' href='?Page=" + previous + "&Utilisateur=" + noUtilisateurVisionne + "'> Previous</a></li>";

      pager.Text += strDebut;
      for (int i = 1; i <= nbPages; i++)
      {
         strClass = pageCourante == i ? "page-item active" : "page-item";
         string strMillieu = "<li class='" + strClass + "'><a class='page - link' href='?Page=" + i + "&Utilisateur=" + noUtilisateurVisionne + "'>" + i + "</a></li>";
         pager.Text += strMillieu;
      }

      int next = pageCourante + 1;
      strClass = next >= nbPages + 1 ? "page-item disabled" : "page-item";
      next = next >= nbPages + 1 ? pageCourante : next;

      string strFin = "<li class='" + strClass + "'><a class='page-link' href='?Page=" + next + "&Utilisateur=" + noUtilisateurVisionne + "'>Next</a></li>" +
                              "</ul>" +
                          "</nav>";
      pager.Text += strFin;

      control.Controls.Add(pager);
   }

   public void affichageDetailleonClick(Object sender, EventArgs e)
   {
      Button btn = (Button)sender;
      String url = "~/Pages/AffichageDetaille.aspx?Film=" + btn.ID.Replace("affichage_detaillee_", "");
      Response.Redirect(url, true);
   }

    public void appropriationOnClick(Object sender, EventArgs e)
    {
        Button btn = (Button)sender;
        String url = "~/Pages/AppropriationDVD.aspx?Film=" + btn.ID.Replace("appropriation_", "");
        Response.Redirect(url, true);
    }

    private void EnvoyerUnCourriel(object sender, EventArgs e)
    {
        Button btn = (Button)sender;
        EntiteUtilisateur utilCourriel = SQL.FindUtilisateurById(noUtilisateurVisionne);
        String url = "~/Pages/Courriel.aspx?Destinataire=" + utilCourriel.NomUtilisateur;
        Response.Redirect(url, true);
    }


    public void populerListeFilms()
   {
      SQL.Connection();
      lstExemplaires = SQL.FindAllUserExemplairesEmpruntes(noUtilisateurVisionne);
   }

   public void onDdlUtilisateurChanged(Object sender, EventArgs e)
   {
      string strNoSelected = ddlUtilisateur.SelectedValue;
      this.noUtilisateurVisionne = int.Parse(strNoSelected.Trim());
      String url = "../Pages/DVDsAutreUtilisateur.aspx?Utilisateur=" + strNoSelected.Trim();
      System.Diagnostics.Debug.WriteLine(url);
      Response.Redirect(url);
   }

   private void initialiserNoUtilisateur()
   {
      int n;
      if (Request.QueryString["Utilisateur"] == null || !int.TryParse(Request.QueryString["Utilisateur"], out n))
      {
         this.noUtilisateurVisionne = 0;
      }
      else
      {
         this.noUtilisateurVisionne = n;
      }

   }

   private void populerDDLUtilisateurs()
   {
      ddlUtilisateur.Items.Clear();
      List<EntiteUtilisateur> lstUtilisateurs = SQL.FindAllAutresUtilisateur(noUtilisateurCourrant);
      ddlUtilisateur.Items.Add(new ListItem("-- Aucun --", "0"));
      foreach (EntiteUtilisateur utilisateur in lstUtilisateurs)
      {
         ddlUtilisateur.Items.Add(new ListItem(utilisateur.NomUtilisateur, utilisateur.NoUtilisateur.ToString()));
      }
   }
}