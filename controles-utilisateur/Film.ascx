<%@ Control Language="C#" %>
<script runat="server">
    static string prevPage = String.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        if( !IsPostBack )
        {
            prevPage = Request.UrlReferrer.ToString();
        }

    }
    protected void Retour(object sender, EventArgs e)
    {
        Response.Redirect(prevPage);
    }
    protected void Ajouter(object sender, EventArgs e)
    {
        Retour(sender, e);
    }
</script>

<div class="row">
    <div class="col-sm-6">

        <asp:Label runat="server">Titre originale :</asp:Label>
        <asp:TextBox ID="tbTitreOriginal" runat="server"
           MaxLength="25" CssClass="form-control"
            placeholder="Titre originale"/>
        <br />

        <asp:Label runat="server">Producteur :</asp:Label>
        <asp:TextBox ID="tbNomProducteur" runat="server"
           MaxLength="25" CssClass="form-control"
            placeholder="Nom du producteur"/>
        <br />
        
        <asp:Label runat="server">Année de sortie :</asp:Label>
        <asp:TextBox ID="tbAnnee" runat="server"
           MaxLength="25" CssClass="form-control"
            placeholder="Année de sortie"/>
        <br />

        <asp:Label runat="server">Durée :</asp:Label>
        <asp:TextBox ID="tbDurée" runat="server"
           MaxLength="25" CssClass="form-control"
            placeholder="Durée (en minutes)"/>
        <br />

        <asp:Label runat="server">Nombres de disques :</asp:Label>
        <asp:TextBox ID="tbNbDisques" runat="server"
           MaxLength="25" CssClass="form-control"
            placeholder="Nombre de diques"/>
        <br />

        <asp:Label runat="server">Format :</asp:Label>
        <asp:TextBox ID="tbFormat" runat="server"
           MaxLength="25" CssClass="form-control"
            placeholder="Format"/>
        <br />
    </div>
    <div class="col-sm-6">

        <asp:Label runat="server">Titre francais :</asp:Label>
        <asp:TextBox ID="tbTitreFrancais" runat="server"
           MaxLength="25" CssClass="form-control"
            placeholder="Titre francais"/>
        <br />

        <asp:Label runat="server">Réalisateur :</asp:Label>
        <asp:TextBox ID="tbNomRealisateur" runat="server"
           MaxLength="25" CssClass="form-control"
            placeholder="Nom du réalisateur"/>
        <br />

        <asp:Label runat="server">Acteur 1 :</asp:Label>
        <asp:TextBox ID="tbActeur1" runat="server"
           MaxLength="25" CssClass="form-control"
            placeholder="Nom de l'acteur"/>
        <br />

        <asp:Label runat="server">Acteur 2 :</asp:Label>
        <asp:TextBox ID="tbActeur2" runat="server"
           MaxLength="25" CssClass="form-control"
            placeholder="Nom de l'acteur"/>
        <br />

        <asp:Label runat="server">Acteur 3 :</asp:Label>
        <asp:TextBox ID="tbActeur3" runat="server"
           MaxLength="25" CssClass="form-control"
            placeholder="Nom de l'acteur"/>
        <br />

        <asp:Label runat="server">Langue :</asp:Label>
        <asp:TextBox ID="tbLangue" runat="server"
           MaxLength="25" CssClass="form-control"
            placeholder="Langue du film"/>
        <br />

        <asp:Label runat="server">Sous-titres :</asp:Label>
        <asp:TextBox ID="tbSousTitres" runat="server"
           MaxLength="25" CssClass="form-control"
            placeholder="Sous-titres du film"/>
        <br />
    </div>
</div>
<!-- TODO : ajouter d'autres champs, modifier textbox pour des dropdown list  -->
<div class="row">
    <div class="col-sm-6">
        <asp:Button runat="server" class="btn btn-lg btn-primary btn-block" Text="Ajouter" OnClick="Ajouter"/>
    </div>
    <div class="col-sm-6">
        <asp:Button runat="server" class="btn btn-lg btn-danger btn-block" Text="Annuler" OnClick="Retour"/>
    </div>
</div>
