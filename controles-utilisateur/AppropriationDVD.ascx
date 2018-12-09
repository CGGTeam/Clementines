<%@ Control Language="C#" %>
<script runat="server">

    static string prevPage = String.Empty;
    private EntiteExemplaire film;
    private EntiteUtilisateur utilCourant;

    protected void Page_Load(object sender, EventArgs e)
    {
        utilCourant = SQL.FindUtilisateurByName(HttpContext.Current.User.Identity.Name);
        if( !IsPostBack )
        {
            if (utilCourant.TypeUtilisateur == 'S')
            {
                div_identite.Visible = true;
                populerDDLIdentite();
            }
            //System.Diagnostics.Debug.WriteLine("Num: " + utilCourant.NoUtilisateur);
            if(Request.UrlReferrer != null)
            {
                prevPage = Request.UrlReferrer.ToString();
            }

        }
        InitialiserDestinaire();
        InitialiserVieuxRetour();
    }


    protected void Retour(object sender, EventArgs e)
    {
        Response.Redirect(prevPage);
    }

    private void InitialiserDestinaire()
    {
        if (Request.QueryString["Film"] == null)
        {
            btnApproprier.Visible = false;
            div_identite.Visible = false;
        }
        else
        {
            string noFilm = Request.QueryString["Film"];
            LoadFilm(noFilm);
        }
    }
    private void InitialiserVieuxRetour()
    {
        if (Request.QueryString["Retour"] == null)
        {

        }
        else
        {
            div_identite.Visible = false;
            btnApproprier.Visible = false;
            prevPage = Request.QueryString["Retour"];
        }
    }
    private void LoadFilm(string id)
    {
        int no;
        int.TryParse(id, out no);
        try
        {
            string na = "N/A";
            string sep = ", ";

            SQL.Connection();
            film = SQL.FindExemplaireById(no);

            PHVignette.ImageUrl = film.film.ImagePochette!=String.Empty ? film.film.ImagePochette : "../Static/images/pas-de-vignette.png";

            PHTitreOriginal.Text = film.film.TitreOriginal!=string.Empty ? film.film.TitreOriginal : na;
            PHTitreFrancais.Text = film.film.TitreFrancais!=string.Empty ? film.film.TitreFrancais : na;
            PHNomRealisateur.Text = film.film.NomRealisateur!=string.Empty ? film.film.NomRealisateur : na;
            PHNomProducteur.Text =film.film.NomProducteur!=string.Empty ? film.film.NomProducteur : na;
            PHActeurs.Text = film.film.lstActeurs.Count() != 0? String.Join(sep, film.film.lstActeurs) : na;

            PHAnneeSortie.Text =film.film.AnneeSortie!=-1 ? film.film.AnneeSortie.ToString() : na;
            PHNbDisques.Text =film.film.NbDisques!=-1 ? film.film.NbDisques.ToString() : na;
            PHCategorie.Text =film.film.Categorie!=string.Empty ? film.film.Categorie : na;
            PHDurée.Text = film.film.Duree!=-1 ? film.film.Duree + " min" : na;
            PHFormat.Text = film.film.Format!=string.Empty ? film.film.Format : na;

            PHLangue.Text = film.film.lstLangues.Count() != 0? String.Join(sep, film.film.lstLangues) : na;
            PHSousTitre.Text = film.film.lstSousTitres.Count() != 0? String.Join(sep, film.film.lstSousTitres) : na;

            PHMiseAJourDate.Text = film.film.DateMAJ.ToString("yyyy-MM-dd") !=string.Empty ? film.film.DateMAJ.ToString("yyyy-MM-dd") : na;
            PHMiseAJourDatePar.Text = film.film.NomUtilisateur!=string.Empty ? film.film.NomUtilisateur : na;
            PHProprietaire.Text = film.proprietaire.NomUtilisateur!=string.Empty ? film.proprietaire.NomUtilisateur : na;

            PHSupplement.Text = film.film.lstSupplements.Count() != 0? String.Join(sep, film.film.lstSupplements) : na;
            PHOriginale.Text = film.film.FilmOriginal ? "Oui" : "Non";
            PHEtendue.Text = film.film.VersionEtendue ? "Oui" : "Non";

            PHResume.Text = film.film.Resume!=string.Empty ? film.film.Resume : na;


        }
        catch(Exception e)
        {
            System.Diagnostics.Debug.WriteLine(e);
        }
    }

    private void approprier_onClick(Object sender, EventArgs e)
    {
        //System.Diagnostics.Debug.WriteLine(utilCourant.NoUtilisateur);
        // requête approprier DVD
        int nbRetour = 0;
        if (utilCourant.TypeUtilisateur != 'S')
        {
            nbRetour = SQL.ApproprierDVD(utilCourant.NoUtilisateur, film.film.NoFilm);
        }
        else
        {
            nbRetour = SQL.ApproprierDVD(int.Parse(ddlIdentite.SelectedValue), film.film.NoFilm);
        }

        if (nbRetour > 0)
        {
            String url = "~/Pages/AppropriationDVD.aspx?Film=" + film.film.NoFilm + "&Retour=" + prevPage;
            Response.Redirect(url);
        }
    }

    private void populerDDLIdentite()
    {


        ddlIdentite.Items.Clear();
        List<EntiteUtilisateur> lstUtilisateurs = SQL.FindAllAutresUtilisateur(utilCourant.NoUtilisateur);
        ddlIdentite.Items.Add(new ListItem("Moi", utilCourant.NoUtilisateur.ToString()));
        foreach (EntiteUtilisateur utilisateur in lstUtilisateurs)
        {
            ddlIdentite.Items.Add(new ListItem(utilisateur.NomUtilisateur, utilisateur.NoUtilisateur.ToString()));
        }
    }
</script>

    <h1>Affichage détaillé du film <span style="color:darkred;"></span></h1>

<div class="row" runat="server" id="div_identite" Visible="false">
        <div class="col-sm-4">
            <div class="input-group">
              <span class="input-group-addon">Changer d'identité </span>
              <span class="input-group-addon"><i class="glyphicon glyphicon-user"></i></span>
              <asp:DropDownList ID="ddlIdentite" runat="server"
                  CssClass="form-control"/>
        </div>
        </div>
    </div>
<br />
        <asp:LinkButton runat="server" class="btn btn-danger" Text="Retour" onclick="Retour" >
            <span class="glyphicon glyphicon-chevron-left"></span>Retour
        </asp:LinkButton>
        
        <asp:Button id="btnApproprier" Text="S'approprier le DVD" runat="server" OnClick="approprier_onClick" CssClass="btn btn-primary"
            OnClientClick="if ( !confirm('Désirez-vous vraiment vous approprier le DVD courant ?\n\nOK=OUI\n\nAnnuler=NON')) return false;"/> 
    <hr />

 <div class="panel panel-default">
        <div class="panel-body">
        <div class="col-sm-3" align="center">
            <asp:Image ID="PHVignette" runat="server" class="vignette"/>

            <br />
            <button type="button" class="btn btn-primary" data-toggle="modal" data-target="#exampleModal">
                          Afficher le résumé
            </button>   
            
        </div>

        <div class="col-sm-8">
                <div class="row">
                    <div class="col-sm-5">
                        <asp:Label ID="TitreOriginal" runat="server" CssClass="label-gras">Titre originale</asp:Label>       
                    </div>
                    <div class="col-sm-7">
                        <asp:Label ID="PHTitreOriginal" runat="server" CssClass="label-non-gras"></asp:Label>       
                    </div>
                </div> 
                <div class="row">
                    <div class="col-sm-5">
                        <asp:Label ID="Label1" runat="server" CssClass="label-gras">Titre français</asp:Label>       
                    </div>
                    <div class="col-sm-7">
                        <asp:Label ID="PHTitreFrancais" runat="server" CssClass="label-non-gras"></asp:Label>       
                    </div>
                </div> 
                <div class="row">
                    <div class="col-sm-5">
                        <asp:Label ID="Label2" runat="server" CssClass="label-gras">Producteur</asp:Label>       
                    </div>
                    <div class="col-sm-7">
                        <asp:Label ID="PHNomProducteur" runat="server" CssClass="label-non-gras"></asp:Label>       
                    </div>
                </div>
                <div class="row">
                    <div class="col-sm-5">
                        <asp:Label ID="Label3" runat="server" CssClass="label-gras">Réalisateur</asp:Label>       
                    </div>
                    <div class="col-sm-7">
                        <asp:Label ID="PHNomRealisateur" runat="server" CssClass="label-non-gras"></asp:Label>       
                    </div>
                </div> 
                <div class="row">
                    <div class="col-sm-5">
                        <asp:Label ID="Label13" runat="server" CssClass="label-gras">Acteurs</asp:Label>       
                    </div>
                    <div class="col-sm-7">
                        <asp:Label ID="PHActeurs" runat="server" CssClass="label-non-gras"></asp:Label>       
                    </div>
                </div> 
                <div class="row">
                    <div class="col-sm-5">
                        <asp:Label ID="Label4" runat="server" CssClass="label-gras">Année de sortie</asp:Label>       
                    </div>
                    <div class="col-sm-7">
                        <asp:Label ID="PHAnneeSortie" runat="server" CssClass="label-non-gras"></asp:Label>       
                    </div>
                </div>
                <div class="row">
                    <div class="col-sm-5">
                        <asp:Label ID="Label5" runat="server" CssClass="label-gras">Catégorie</asp:Label>       
                    </div>
                    <div class="col-sm-7">
                        <asp:Label ID="PHCategorie" runat="server" CssClass="label-non-gras"></asp:Label>       
                    </div>
                </div> 
                <div class="row">
                    <div class="col-sm-5">
                        <asp:Label ID="Label6" runat="server" CssClass="label-gras">Durée</asp:Label>       
                    </div>
                    <div class="col-sm-7">
                        <asp:Label ID="PHDurée" runat="server" CssClass="label-non-gras"></asp:Label>       
                    </div>
                </div>
                <div class="row">
                    <div class="col-sm-5">
                        <asp:Label ID="Label8" runat="server" CssClass="label-gras">Nombre de disques</asp:Label>       
                    </div>
                    <div class="col-sm-7">
                        <asp:Label ID="PHNbDisques" runat="server" CssClass="label-non-gras"></asp:Label>       
                    </div>
                </div>               
                <div class="row">
                    <div class="col-sm-5">
                        <asp:Label ID="Label10" runat="server" CssClass="label-gras">Format</asp:Label>       
                    </div>
                    <div class="col-sm-7">
                        <asp:Label ID="PHFormat" runat="server" CssClass="label-non-gras"></asp:Label>       
                    </div>
                </div>

                <hr />

                <div class="row">
                    <div class="col-sm-5">
                        <asp:Label ID="Label7" runat="server" CssClass="label-gras">Langue</asp:Label>       
                    </div>
                    <div class="col-sm-7">
                        <asp:Label ID="PHLangue" runat="server" CssClass="label-non-gras"></asp:Label>       
                    </div>
                </div>
                <div class="row">
                    <div class="col-sm-5">
                        <asp:Label ID="Label12" runat="server" CssClass="label-gras">Sous-titres</asp:Label>       
                    </div>
                    <div class="col-sm-7">
                        <asp:Label ID="PHSousTitre" runat="server" CssClass="label-non-gras"></asp:Label>       
                    </div>
                </div>

                <hr />

                <div class="row">
                        <div class="col-sm-5">
                            <asp:Label ID="Label11" runat="server" CssClass="label-gras">Dernière mise à jour</asp:Label>       
                        </div>
                        <div class="col-sm-7">
                            <asp:Label ID="PHMiseAJourDate" runat="server" CssClass="label-non-gras"></asp:Label>       
                        </div>
                    </div>
                 <div class="row">
                    <div class="col-sm-5">
                        <asp:Label ID="Label14" runat="server" CssClass="label-gras">Par</asp:Label>       
                    </div>
                    <div class="col-sm-7">
                        <asp:Label ID="PHMiseAJourDatePar" runat="server" CssClass="label-non-gras"></asp:Label>       
                    </div>
                </div>
                <div class="row">
                    <div class="col-sm-5">
                        <asp:Label ID="Label9" runat="server" CssClass="label-gras">Propriéaire</asp:Label>       
                    </div>
                    <div class="col-sm-7">
                        <asp:Label ID="PHProprietaire" runat="server" CssClass="label-non-gras"></asp:Label>       
                    </div>
                </div>
                <div class="row">
                    <div class="col-sm-5">
                        <asp:Label ID="Label16" runat="server" CssClass="label-gras">Emprunteur</asp:Label>       
                    </div>
                    <div class="col-sm-7">
                        <asp:Label ID="PHEmprunter" runat="server" CssClass="label-non-gras"></asp:Label>       
                    </div>
                </div>

                <hr />

                <div class="row">
                    <div class="col-sm-5">
                        <asp:Label ID="Label15" runat="server" CssClass="label-gras">Supplément</asp:Label>       
                    </div>
                    <div class="col-sm-7">
                        <asp:Label ID="PHSupplement" runat="server" CssClass="label-non-gras"></asp:Label>       
                    </div>
                </div>
                <div class="row">
                    <div class="col-sm-5">
                        <asp:Label ID="Label17" runat="server" CssClass="label-gras">DVD originale</asp:Label>       
                    </div>
                    <div class="col-sm-7">
                        <asp:Label ID="PHOriginale" runat="server" CssClass="label-non-gras"></asp:Label>       
                    </div>
                </div>
                <div class="row">
                    <div class="col-sm-5">
                        <asp:Label ID="Label19" runat="server" CssClass="label-gras">Version étendue</asp:Label>       
                    </div>
                    <div class="col-sm-7">
                        <asp:Label ID="PHEtendue" runat="server" CssClass="label-non-gras"></asp:Label>       
                    </div>
                </div>
             </div>
        </div>
</div>

<!-- Modal pour le résumé-->
<div class="modal fade" id="exampleModal" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
  <div class="modal-dialog" role="document">
    <div class="modal-content">
      <div class="modal-header">
        <h5 class="modal-title" id="exampleModalLabel">Résumé</h5>
        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
          <span aria-hidden="true">&times;</span>
        </button>
      </div>
      <div class="modal-body">
        <asp:Label ID="PHResume" runat="server" CssClass="label-non-gras"></asp:Label>  
      </div>
      <div class="modal-footer">
        <button type="button" class="btn btn-secondary" data-dismiss="modal">Fermer</button>
      </div>
    </div>
  </div>
</div>
