using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_DVDEnMain : System.Web.UI.Page
{
   List<EntiteFilm> lstFilms = new List<EntiteFilm>();
   private int nbVignettesParPage = 10; // {valeur déterminé dans les préférences de l'utilisateur}
   private int pageCourante;

   protected void Page_Load(object sender, EventArgs e)
   {

      // Vérifier la page courante
      initialiserNoPage();

      lstFilms.Add(new EntiteFilm(1, "Le retour de JFL", "../Static/images/logo.png", "JFL"));
         lstFilms.Add(new EntiteFilm(2, "Le retour de JFL", "../Static/images/logo.png", "JFL"));
         lstFilms.Add(new EntiteFilm(3, "Le retour de JFL", "../Static/images/logo.png", "JFL"));
         lstFilms.Add(new EntiteFilm(4, "Ley retour de JFL", "../Static/images/logo.png", "JFL"));
         lstFilms.Add(new EntiteFilm(5, "Le retour de JFL", "../Static/images/logo.png", "JFL"));
         lstFilms.Add(new EntiteFilm(6, "Le retour de JFL", "../Static/images/logo.png", "JFL"));
         lstFilms.Add(new EntiteFilm(7, "Le retour de JFL", "../Static/images/logo.png", "JFL"));
         lstFilms.Add(new EntiteFilm(8, "Le retour de JFL", "../Static/images/logo.png", "JFL"));
         lstFilms.Add(new EntiteFilm(9, "Le retour de JFL", "../Static/images/logo.png", "JFL"));
         lstFilms.Add(new EntiteFilm(10, "Le retour de JFL", "../Static/images/logo.png", "JFL"));
         lstFilms.Add(new EntiteFilm(11, "Le retour de JFL", "../Static/images/logo.png", "JFL"));
         lstFilms.Add(new EntiteFilm(12, "Le retour de JFL", "../Static/images/logo.png", "JFL"));
         lstFilms.Add(new EntiteFilm(13, "Le retour de JFL", "../Static/images/logo.png", "JFL"));
         lstFilms.Add(new EntiteFilm(14, "Le retour de JFL", "../Static/images/logo.png", "JFL"));
         lstFilms.Add(new EntiteFilm(15, "Le retour de JFL", "../Static/images/logo.png", "JFL"));
         lstFilms.Add(new EntiteFilm(16, "Le retour de JFL", "../Static/images/logo.png", "JFL"));
         lstFilms.Add(new EntiteFilm(17, "Le retour de JFL", "../Static/images/logo.png", "JFL"));
         lstFilms.Add(new EntiteFilm(18, "Le retour de JFL", "../Static/images/logo.png", "JFL"));
         lstFilms.Add(new EntiteFilm(19, "Le retour de JFL", "../Static/images/logo.png", "JFL"));
         //lstFilms.Add(new EntiteFilm(20, "Le retour de JFL", "../Static/images/logo.png", "JFL"));

         afficherPageVignettes(lstFilms);
         afficherPager(phChangerPage);
   }

   public void afficherPageVignettes(List<EntiteFilm> lstFilms)
   {
      Panel row = row = librairie.divDYN(phVignettes, "row", "row");

      for (int i = ((pageCourante-1) * nbVignettesParPage); i < (pageCourante * nbVignettesParPage) && i < lstFilms.Count; i++)
      {
         Panel col = librairie.divDYN(row, "col_" + lstFilms[i].id, "col-sm-3");
         Panel panel = librairie.divDYN(col, "panel_" + lstFilms[i].id, "panel panel-default");
         Panel panelBody = librairie.divDYN(panel, "panel-body_" + lstFilms[i].id, "panel-body vignette");
         Panel panelCache = librairie.divDYN(panelBody, "panel-cache_" + lstFilms[i].id, "boutons-caches");
         Table table = librairie.tableDYN(panelCache, "table_" + lstFilms[i].id, "tableau-boutons");

         TableRow tr1 = librairie.trDYN(table);
         TableCell td1 = librairie.tdDYN(tr1, "td_affichage_detaillee_" + lstFilms[i].id, "");
         Button btn1 = librairie.btnDYN(td1, "affichage_detaillee_" + lstFilms[i].id, "btn btn-default boutons-options-film", "Affichage détaillée");
         btn1.Click += new EventHandler(affichageDetailleonClick);

         TableRow tr2 = librairie.trDYN(table);
         TableCell td2 = librairie.tdDYN(tr2, "td_modifier_" + lstFilms[i].id, "");
         Button btn2 = librairie.btnDYN(td2, "modifier_" + lstFilms[i].id, "btn btn-default boutons-options-film", "Modifier");
         btn2.Click += new EventHandler(modifieronClick);

         TableRow tr3 = librairie.trDYN(table);
         TableCell td3 = librairie.tdDYN(tr3, "td_supprimer_" + lstFilms[i].id, "");
         Button btn3 = librairie.btnDYN(td3, "supprimer_" + lstFilms[i].id, "btn btn-default boutons-options-film", "Supprimer");

         Image img = librairie.imgDYN(panelBody, "img_" + lstFilms[i].id, lstFilms[i].vignette, "");

         Panel panelFooter = librairie.divDYN(panel, "panel-footer_" + lstFilms[i].id, "panel-footer");
         Label lblTitre = librairie.lblDYN(panelFooter, "titre-film_" + lstFilms[i].id, lstFilms[i].nom, "titre-film");
      }

      Panel row2 = librairie.divDYN(phChangerPage, "rowChangerPage", "row");

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
      decimal nbPages = Math.Ceiling((decimal)lstFilms.Count / (decimal)nbVignettesParPage);

      int previous = pageCourante - 1;
      string strClass = previous <= 0 ? "page-item disabled" : "page-item";
      previous = previous <= 0 ? pageCourante : previous;

      string strDebut = "<nav aria - label = 'Page navigation example' >" +
                              "<ul class='pagination justify-content-center'>" +
                                  "<li class='" + strClass + "'><a class='page-link' href='?Page=" + previous + "'> Previous</a></li>";

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

      string strFin = "<li class='" + strClass + "'><a class='page-link' href='?Page=" + next + "'>Next</a></li>" +
                              "</ul>" +
                          "</nav>";
      pager.Text += strFin;

      control.Controls.Add(pager);
   }

   public void affichageDetailleonClick(Object sender, EventArgs e)
   {
      Button btn = (Button)sender;
      System.Diagnostics.Debug.WriteLine("Affichage Detaille: " + btn.ID);
      String url = "~/Pages/AffichageDetaille.aspx";
      Response.Redirect(url, true);
   }

   public void modifieronClick(Object sender, EventArgs e)
   {
      Button btn = (Button)sender;
      System.Diagnostics.Debug.WriteLine("Modifier: " + btn.ID);
      String url = "~/Pages/ModifierFilm.aspx";
      Response.Redirect(url, true);
   }

  

}