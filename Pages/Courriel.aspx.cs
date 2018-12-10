using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _Default : System.Web.UI.Page
{
    private string strDestinataire;
    static string prevPage = String.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.UrlReferrer != null)
            {
                prevPage = Request.UrlReferrer.ToString();
            }

        }
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
    protected void Retour(object sender, EventArgs e)
    {
        Response.Redirect(prevPage);
    }
    protected void fermerSucces(object sender, EventArgs e)
    {
        success_message.Visible = false;
        error_message.Visible = false;
    }
    protected void fermerError(object sender, EventArgs e)
    {
        error_message.Visible = false;
        success_message.Visible = false;
    }
    public void envoyerMessage(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(tbMessage.Text))
        {
            error_message.Visible = true;
            lblError.Text = "Le contenu du message ne peut être vide";
            return;
        }
        if (string.IsNullOrEmpty(destinaire.Text))
        {
            error_message.Visible = true;
            lblError.Text = "Le destinataire ne peut être vide";
            return;
        }

        string[] lstDestinataires = destinaire.Text.Split(',');
        //List<string> destinataireValide = lstDestinataires.Where(d=> SQL.FindUtilisateurByName(d.Trim())!=null).ToList();
        List<string> destinataireErronne = lstDestinataires.Where(d=> SQL.FindUtilisateurByName(d.Trim())==null).ToList();
        List<string> destinataireValide = lstDestinataires.Except(destinataireErronne).ToList();

        string sep = ", ";
        if (IsPostBack)
        {
            if (destinataireValide.Any())
            {
                success_message.Visible = true;
                lblSucces.Text = "Le message a été envoyé à " + String.Join(sep, destinataireValide).Trim();
            }
            if (destinataireErronne.Any())
            {
                error_message.Visible = true;
                lblError.Text = destinataireErronne.Count > 1 ?
                    "Le message n'a pu être envoyé à " + String.Join(sep, destinataireErronne).Trim() + " car ils n'esxistent pas"
                    : "Le message n'a pu être envoyé à " + String.Join(sep, destinataireErronne).Trim() + " car il n'esxiste pas";
            }
        }
       
    }
    private void ClearTextBox()
    {
        tbMessage.Text = "";
        destinaire.Text = "";
    }
}