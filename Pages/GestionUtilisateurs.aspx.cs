using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;

public partial class Pages_GestionUtilisateurs : System.Web.UI.Page
{
   protected void Page_Load(object sender, EventArgs e)
   {
      if (!IsPostBack)
      {
         
      }
      //Remplir la table d'utilisateurs
      
      Table table = fluxDeDonne();
      phDynamique.Controls.Add(table);
   }

   public void pageAjouterUtil(object sender, EventArgs e)
   {
      String url = "~/Pages/AjouterUtilisateur.aspx";
      Response.Redirect(url, true);
   }

   public Table remplirTable(DataTable dt)
   {
      EntiteUtilisateur utilCourant = SQL.FindUtilisateurByName(HttpContext.Current.User.Identity.Name);
        //Remplir les données
        TableRow tr;
        TableCell td;
      for (int i = 0; i < dt.Rows.Count; i++)
      {
         tr = new TableRow();
            tr.TableSection = TableRowSection.TableBody;
            table.Controls.Add(tr);
         for (int j = 0; j < dt.Columns.Count; j++)
         {
            td = new TableCell();
            td.Text = dt.Rows[i][j].ToString();
            tr.Controls.Add(td);
         }
         
         //Ajouter le bouton modifier et supprimer
         Button btnModfier = new Button();
         btnModfier.ID = "modifier_" + dt.Rows[i][0].ToString();
         btnModfier.Text = "Modifier";
         btnModfier.CssClass = "btn btn-primary";

         if (dt.Rows[i][0].ToString() == utilCourant.NoUtilisateur.ToString())
         {
            btnModfier.Enabled = false;
         }
         btnModfier.Click += new EventHandler(modifieronClick);
         //btnModfier.Click = modifieronClick;
         td = new TableCell();
         td.Controls.Add(btnModfier);
         tr.Controls.Add(td);
         //Button btnSupprimer = new Button();
         td = new TableCell();

         Button btnSupprimer = librairie.btnDYN_JS(td, "supprimer_" + dt.Rows[i][0].ToString(), "Supprimer", new EventHandler(supprimerOnClick));
         btnSupprimer.CssClass = "btn btn-danger";
         if (dt.Rows[i][0].ToString() == utilCourant.NoUtilisateur.ToString())
         {
            btnSupprimer.Enabled = false;
         }
         //btnSupprimer.OnClientClick = "if (!confirm('Désirez-vous vraiment vous supprimer l'utilisateur ?\n\nOK=OUI\n\nAnnuler=NON')) return false;";
         //btnSupprimer.Click += new EventHandler(supprimerOnClick);
         //td.Controls.Add(btnSupprimer);
         tr.Controls.Add(td);
      }
      return table;
   }
   public Table fluxDeDonne()
   {
      string NomDataTable = "Utilisateurs";
      SqlConnection conn = SQL.Connection2();
      DataTable dtTable;
      SqlDataAdapter cmdTable = new SqlDataAdapter("select NoUtilisateur, NomUtilisateur, Courriel, MotPasse, TypesUtilisateur.Description from Utilisateurs inner join TypesUtilisateur on Utilisateurs.TypeUtilisateur = TypesUtilisateur.TypeUtilisateur", conn);
      DataSet dsTable = new DataSet();

      cmdTable.Fill(dsTable, NomDataTable);
      dtTable = dsTable.Tables[NomDataTable];
      conn.Close();

      return remplirTable(dtTable);
   }

   public void modifieronClick(Object sender, EventArgs e)
   {
      Button btn = (Button)sender;
      String url = "~/Pages/ModifierUtilisateur.aspx?Utilisateur=" + btn.ID.Replace("modifier_", "");
      Response.Redirect(url, true);
   }

   public void supprimerOnClick(Object sender, EventArgs e)
   {
      Button btn = (Button)sender;
      string id = btn.ID.Replace("supprimer_", "");

      if (SQL.verifierOKSuppression(int.Parse(id)))
      {
         SQL.supprimerUtilisateur(int.Parse(id));
         String url = "~/Pages/GestionUtilisateurs.aspx";
         Response.Redirect(url, true);
      }
      else
      {
         succes.Visible = false;
         error.Visible = true;
         lblError.Text = "Impossible de supprimer l'utilisateur, car il est associé à des DVD.";
      }

   }

   protected void fermerSucces(object sender, EventArgs e)
   {
      succes.Visible = false;
   }
   protected void fermerError(object sender, EventArgs e)
   {
      error.Visible = false;
   }

}