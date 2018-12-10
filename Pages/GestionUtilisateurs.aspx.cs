using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_GestionUtilisateurs : System.Web.UI.Page
{
   protected void Page_Load(object sender, EventArgs e)
   {
      if (!IsPostBack)
      {
         //Remplir la table d'utilisateurs
         Table table = SQL.fluxDeDonne();
         phDynamique.Controls.Add(table);
      }
   }

   public static void remplirListeUtilisateurs()
   {
      
   }


}