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
        if (Request.QueryString["Destinataire"] == null)
        {
            strDestinataire = "";
        }
        else
        {
            destinaire.Text = Request.QueryString["Destinataire"];
        }
    }
}