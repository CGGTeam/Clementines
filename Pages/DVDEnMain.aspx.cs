using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_DVDEnMain : System.Web.UI.Page
{
   private int nbVignettesParPage = 10; // {valeur déterminé dans les préférences de l'utilisateur}

   protected void Page_Load(object sender, EventArgs e)
   {
      Dictionary<int, string> lstDVD = new Dictionary<int, string>();
      
      if (!Page.IsPostBack)
      {

         lstDVD.Add(1, "DVD 1");
         lstDVD.Add(2, "DVD 2");
         lstDVD.Add(3, "DVD 3");
         lstDVD.Add(4, "DVD 4");
         lstDVD.Add(5, "DVD 5");
         lstDVD.Add(6, "DVD 6");
         lstDVD.Add(7, "DVD 7");
         lstDVD.Add(8, "DVD 8");
         lstDVD.Add(9, "DVD 9");
         lstDVD.Add(10, "DVD 10");

         afficherPageVignettes(lstDVD);
      }
   }

   public void afficherPageVignettes(Dictionary<int, string> lstDVD)
   {
      Panel row = row = lib.divDYN(phVignettes, "row", "row");

      foreach (var vignette in lstDVD)
      {
<<<<<<< HEAD
         Panel col = lib.divDYN(row, "col_" + vignette.Key, "col-sm-3");
         Panel panel = lib.divDYN(col, "panel_" + vignette.Key, "panel panel-default");
         Panel panelBody = lib.divDYN(panel, "panel-body_" + vignette.Key, "panel-body vignette");
         Panel panelCache = lib.divDYN(panelBody, "panel-cache_" + vignette.Key, "boutons-caches");
         Table table = lib.tableDYN(panelCache, "table_" + vignette.Key, "tableau-boutons");

         TableRow tr1 = lib.trDYN(table);
         TableCell td1 = lib.tdDYN(tr1, "td_" + vignette.Key, "");
         Button btn1 = lib.btnDYN(td1, "affichage_detaillee_" + vignette.Key, "btn btn-default boutons-options-film", "Affichage détaillée");

         TableRow tr2 = lib.trDYN(table);
         TableCell td2 = lib.tdDYN(tr2, "td_" + vignette.Key, "");
         Button btn2 = lib.btnDYN(td2, "modifier_" + vignette.Key, "btn btn-default boutons-options-film", "Modifier");
=======
         if (numVignette % 3 == 0)
         {
            row = librairie.divDYN(phVignettes, "row_" + numRow, "row");
            numRow++;
         }
         Panel col = librairie.divDYN(row, "col_" + vignette.Key, "col-sm-4");
         Panel panel = librairie.divDYN(col, "panel_" + vignette.Key, "panel panel-default");
         Panel panelBody = librairie.divDYN(panel, "panel-body_" + vignette.Key, "panel-body");
         Panel panelFooter = librairie.divDYN(panel, "panel-footer_" + vignette.Key, "panel-footer");
>>>>>>> 321646cf799b81e6ccdca58e9e5f310d11e7b5e6

         TableRow tr3 = lib.trDYN(table);
         TableCell td3 = lib.tdDYN(tr3, "td_" + vignette.Key, "");
         Button btn3 = lib.btnDYN(td3, "supprimer_" + vignette.Key, "btn btn-default boutons-options-film", "Supprimer");

         Image img = lib.imgDYN(panelBody, "img_" + vignette.Key, "../Static/images/logo.png", "");

         Panel panelFooter = lib.divDYN(panel, "panel-footer_" + vignette.Key, "panel-footer");
      }


   }
}