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
            SQL.Connection();
            film = SQL.FindFilmById(no);

            title.InnerText += film.TitreFrancais;
            PHVignette.ImageUrl = film.ImagePochette!=String.Empty ? film.ImagePochette : "../Static/images/pas-de-vignette.png";

            PHTitreOriginal.Text = film.TitreOriginal!=string.Empty ? film.TitreOriginal : na;
            PHTitreFrancais.Text = film.TitreFrancais!=string.Empty ? film.TitreFrancais : na;
            PHNomRealisateur.Text = film.NomRealisateur!=string.Empty ? film.NomRealisateur : na;
            PHNomProducteur.Text =film.NomProducteur!=string.Empty ? film.NomProducteur : na;
            PHAnneeSortie.Text =film.AnneeSortie!=-1 ? film.AnneeSortie.ToString() : na;
            PHNbDisques.Text =film.NbDisques!=-1 ? film.NbDisques.ToString() : na;
            PHCategorie.Text =film.Categorie!=string.Empty ? film.Categorie : na;
            PHDurée.Text = film.Duree!=-1 ? film.Duree + " min" : na;
            PHProprietaire.Text = film.NomUtilisateur!=string.Empty ? film.NomUtilisateur : na;
            PHFormat.Text = film.Format!=string.Empty ? film.Format : na;

            PHMiseAJourDate.Text = film.DateMAJ.ToString("yyyy-MM-dd") !=string.Empty ? film.DateMAJ.ToString("yyyy-MM-dd") : na;
            PHMiseAJourDatePar.Text = film.NomUtilisateur!=string.Empty ? film.NomUtilisateur : na;

            
        }
        catch(Exception e)
        {
            System.Diagnostics.Debug.WriteLine(e);
        }
    }
</script>
    <h1 runat="server" ID="title">Affichage détaillé du film </h1>
    <hr />

 <div class="panel panel-default">
        <div class="panel-body">
        <div class="col-sm-3">
            <asp:Image ID="PHVignette" runat="server" class="vignette"/>
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
                        <asp:Label ID="Label7" runat="server" CssClass="label-gras">Langue</asp:Label>       
                    </div>
                    <div class="col-sm-7">
                        <asp:Label ID="PHLangue" runat="server" CssClass="label-non-gras"></asp:Label>       
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
                        <asp:Label ID="Label9" runat="server" CssClass="label-gras">Propriéaire</asp:Label>       
                    </div>
                    <div class="col-sm-7">
                        <asp:Label ID="PHProprietaire" runat="server" CssClass="label-non-gras"></asp:Label>       
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
             </div>
        </div>
</div>
    <div class="row">
    <asp:Button runat="server" class="btn btn-lg btn-danger btn-block" Text="Retour" onclick="Retour"/>
</div>