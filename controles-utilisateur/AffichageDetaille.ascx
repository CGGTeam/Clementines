<%@ Control Language="C#" %>
<script runat="server">
    static string prevPage = String.Empty;
    private EntiteFilm film;

    protected void Page_Load(object sender, EventArgs e)
    {
        if( !IsPostBack )
        {
            prevPage = Request.UrlReferrer.ToString();
        }
        InitialiserDestinaire();
    }

    protected void Retour(object sender, EventArgs e)
    {
        Response.Redirect(prevPage);
    }

    private void InitialiserDestinaire()
    {
        if (Request.QueryString["Film"] == null)
        {

        }
        else
        {
            string noFilm = Request.QueryString["Film"];
            LoadFilm(noFilm);
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
            film = SQL.FindFilmById(no);

            PHVignette.ImageUrl = film.ImagePochette!=String.Empty ? film.ImagePochette : "../Static/images/pas-de-vignette.png";

            PHTitreOriginal.Text = film.TitreOriginal!=string.Empty ? film.TitreOriginal : na;
            PHTitreFrancais.Text = film.TitreFrancais!=string.Empty ? film.TitreFrancais : na;
            PHNomRealisateur.Text = film.NomRealisateur!=string.Empty ? film.NomRealisateur : na;
            PHNomProducteur.Text =film.NomProducteur!=string.Empty ? film.NomProducteur : na;
            PHActeurs.Text = film.lstActeurs.Count() != 0? String.Join(sep, film.lstActeurs) : na;

            PHAnneeSortie.Text =film.AnneeSortie!=-1 ? film.AnneeSortie.ToString() : na;
            PHNbDisques.Text =film.NbDisques!=-1 ? film.NbDisques.ToString() : na;
            PHCategorie.Text =film.Categorie!=string.Empty ? film.Categorie : na;
            PHDurée.Text = film.Duree!=-1 ? film.Duree + " min" : na;
            PHFormat.Text = film.Format!=string.Empty ? film.Format : na;

            PHLangue.Text = film.lstLangues.Count() != 0? String.Join(sep, film.lstLangues) : na;
            PHSousTitre.Text = film.lstSousTitres.Count() != 0? String.Join(sep, film.lstSousTitres) : na;

            PHMiseAJourDate.Text = film.DateMAJ.ToString("yyyy-MM-dd") !=string.Empty ? film.DateMAJ.ToString("yyyy-MM-dd") : na;
            PHMiseAJourDatePar.Text = film.NomUtilisateur!=string.Empty ? film.NomUtilisateur : na;
            PHProprietaire.Text = film.NomUtilisateur!=string.Empty ? film.NomUtilisateur : na;

            PHSupplement.Text = film.lstSupplements.Count() != 0? String.Join(sep, film.lstSupplements) : na;
            PHOriginale.Text = film.FilmOriginal ? "Oui" : "Non";
            PHEtendue.Text = film.VersionEtendue ? "Oui" : "Non";

            PHResume.Text = film.Resume!=string.Empty ? film.Resume : na;


        }
        catch(Exception e)
        {
            System.Diagnostics.Debug.WriteLine(e);
        }
    }
</script>
    <h1>Affichage détaillé du film <span style="color:darkred;"><%= film.TitreFrancais %></span></h1> 
        <asp:LinkButton runat="server" class="btn btn-danger" Text="Retour" onclick="Retour">
            <span class="glyphicon glyphicon-chevron-left"></span>Retour
        </asp:LinkButton>
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
                        <asp:Label ID="Label2" runat="server" CssClass="label-gras">Nom du producteur</asp:Label>       
                    </div>
                    <div class="col-sm-7">
                        <asp:Label ID="PHNomProducteur" runat="server" CssClass="label-non-gras"></asp:Label>       
                    </div>
                </div>
                <div class="row">
                    <div class="col-sm-5">
                        <asp:Label ID="Label3" runat="server" CssClass="label-gras">Nom du réalisateur</asp:Label>       
                    </div>
                    <div class="col-sm-7">
                        <asp:Label ID="PHNomRealisateur" runat="server" CssClass="label-non-gras"></asp:Label>       
                    </div>
                </div> 
                <div class="row">
                    <div class="col-sm-5">
                        <asp:Label ID="Label13" runat="server" CssClass="label-gras">Nom des principaux acteurs</asp:Label>       
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