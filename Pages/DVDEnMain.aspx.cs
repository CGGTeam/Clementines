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
      int numVignette = 0;
      int numRow = 1;
      Panel row = null;

      foreach (var vignette in lstDVD)
      {
         if (numVignette % 3 == 0)
         {
            row = librairie.divDYN(phVignettes, "row_" + numRow, "row");
            numRow++;
         }
         Panel col = librairie.divDYN(row, "col_" + vignette.Key, "col-sm-4");
         Panel panel = librairie.divDYN(col, "panel_" + vignette.Key, "panel panel-default");
         Panel panelBody = librairie.divDYN(panel, "panel-body_" + vignette.Key, "panel-body");
         Panel panelFooter = librairie.divDYN(panel, "panel-footer_" + vignette.Key, "panel-footer");

         numVignette++;
      }


   }
}