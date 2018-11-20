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
            SQL.Connection();
            film = SQL.FindFilmById(no);
            PHTitreOriginal.Text = film.TitreOriginal!=string.Empty ? film.TitreOriginal : "N/A";
            PHTitreFrancais.Text = film.TitreFrancais!=string.Empty ? film.TitreFrancais : "N/A";
            PHNomRealisateur.Text = film.NomRealisateur!=string.Empty ? film.NomRealisateur : "N/A";
            PHNomProducteur.Text =film.NomProducteur!=string.Empty ? film.NomProducteur : "N/A";
            PHAnneeSortie.Text =film.AnneeSortie.ToString()!=string.Empty ? film.AnneeSortie.ToString() : "N/A";
            PHNbDisques.Text =film.NbDisques.ToString()!=string.Empty ? film.NbDisques.ToString() : "N/A";
            PHCategorie.Text =film.Categorie!=string.Empty ? film.Categorie : "N/A";
            PHDurée.Text = film.Duree.ToString()!=string.Empty ? film.Duree + " min" : "N/A";
            PHProprietaire.Text = film.NomUtilisateur!=string.Empty ? film.NomUtilisateur : "N/A";
            
        }
        catch(Exception e)
        {
            System.Diagnostics.Debug.WriteLine(e);
        }
    }
</script>

 <div class="panel panel-default">
        <div class="panel-body">
            <div class="row">
        <div class="col-sm-3">
            <img src="../Static/images/logo.png" class="vignette"/>
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
         </div>
    </div>
</div>
<!-- TODO : ajouter d'autres champs, modifier textbox pour des dropdown list  -->
<div class="row">
        <asp:Button runat="server" class="btn btn-lg btn-danger btn-block" Text="Retour" onclick="Retour"/>
</div>
