using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _Default : System.Web.UI.Page
{
    private string strDestinataire;
    protected void Page_Load(object sender, EventArgs e)
    {
        InitialiserDestinaire();
    }
    private void InitialiserDestinaire()
    {
        if (!IsPostBack) { 
            if (Request.QueryString["Destinataire"] == null)
            {
                destinaire.Text = "";
            }
            else
            {
                strDestinataire = Request.QueryString["Destinataire"];
                destinaire.Text = strDestinataire;
                destinaire.Enabled = false;
            }
        }
    }
    protected void fermerSucces(object sender, EventArgs e)
    {
        success_message.Visible = false;
    }
    protected void fermerError(object sender, EventArgs e)
    {
        error_message.Visible = false;
    }
    public void envoyerMessage(object sender, EventArgs e)
   {
      if ((string.IsNullOrEmpty(tbMessage.Text)) || (string.IsNullOrEmpty(destinaire.Text)))
      {
         success_message.Visible = false;
         error_message.Visible = true;
      }
      else
      {
         success_message.Visible = true;
         error_message.Visible = false;
      }
   }
}