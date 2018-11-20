<%@ Control Language="C#" %>
<%@ Register tagprefix="pers" TagName="Personne" Src="Personne_Ddl_Ajout.ascx" %>
<script runat="server">
    static string prevPage = String.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        if( !IsPostBack )
        {
            if (Request.UrlReferrer != null)
            {
                prevPage = Request.UrlReferrer.ToString();
            }
            chargeListeAnneeSortie();
            chargeListeNbCD();

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

    protected void chargeListeAnneeSortie()
    {
        int annee = DateTime.Now.Year;

        ddlAnnee.Items.Add("...");
        for (int i = 1900; i <= annee; i++)
        {
            ddlAnnee.Items.Add(i.ToString());
        }
        
    }
    protected void chargeListeNbCD()
    {
        ddlNbDisques.Items.Add("...");
        for (int i = 1; i <= 10; i++)
        {
            ddlNbDisques.Items.Add(i.ToString());
        }
        
    }
</script>

<div class="row">
    <div class="col-sm-6">
        <!-- Titre français -->
        <asp:Label runat="server">Titre francais :</asp:Label>
        <asp:TextBox ID="tbTitreFrancais" runat="server"
           MaxLength="25" CssClass="form-control"
            placeholder="Titre francais"/>
        <asp:RequiredFieldValidator runat="server" 
             id="rerFieldValidatorTitreOriginal"  
             Style="color:red" 
             controltovalidate="tbTitreFrancais" 
             errormessage="Entrez un tite!" />
        <br />
         <!-- Année de sortie Fait -->
        <asp:Label runat="server">Année de sortie :</asp:Label>
        <asp:DropDownList ID="ddlAnnee" runat="server"
           MaxLength="25" CssClass="form-control"
           placeholder="Année de sortie"/>
        <br />
        <!-- Categorie Requete-->
        <asp:Label runat="server">Catégorie :</asp:Label>
        <asp:DropDownList ID="ddlCategorie" runat="server"
           MaxLength="25" CssClass="form-control"
            placeholder="Nom du producteur"/>
        <br />
        <!-- Durée Fait-->
        <asp:Label runat="server">Durée :</asp:Label>
        <asp:TextBox ID="tbDuree" runat="server"
           MaxLength="25" CssClass="form-control"
            placeholder="Durée (en minutes)"/>
        <asp:RegularExpressionValidator ID="RegularExpressionDuree"
            ControlToValidate="tbDuree" runat="server"
            ErrorMessage="Nombres entiers seulement!"
            Style="color:red"
            ValidationExpression="\d+">
        </asp:RegularExpressionValidator>
        <br />
        <!-- Format Requete-->
        <asp:Label runat="server">Format :</asp:Label>
        <asp:DropDownList ID="ddlFormat" runat="server"
             CssClass="form-control"/>
        <br />
        <!-- Nombre de disques Fait-->
        <asp:Label runat="server">Nombres de disques :</asp:Label>
        <asp:DropDownList ID="ddlNbDisques" runat="server"
             CssClass="form-control"/>
        <br />

        <!-- Langue Requete -->
        <asp:Label runat="server">Langue :</asp:Label>
        <asp:DropDownList ID="ddlLangue" runat="server"
            CssClass="form-control"/>
        <br />
        <!-- Sous-titre Requete-->
        <asp:Label runat="server">Sous-titres :</asp:Label>
        <asp:DropDownList ID="ddlSousTitre" runat="server"
           CssClass="form-control"/>
        <br />

        
        <!-- Image incertain-->
        <asp:Label runat="server">Image de la pochette :</asp:Label>
         <div class="input-group">
          <span class="input-group-addon">   
              <i class="glyphicon glyphicon glyphicon-file"></i>
          </span>
           <input type="file" class="form-control">
        </div> 
    </div>
    <!-- ------------------------------------------------------------------------------------------------------------------------------------- -->
    <div class="col-sm-6">
        <!-- Titre original -->
        <asp:Label runat="server">Titre original :</asp:Label>
        <asp:TextBox ID="tbTitreOriginal" runat="server"
           MaxLength="25" CssClass="form-control"
            placeholder="Titre originale"/>
        <br />
        <!-- Suppléments Requete -->
        <asp:Label runat="server">Suppléments :</asp:Label>
        <asp:DropDownList ID="ddlSupplements" runat="server"
           MaxLength="25" CssClass="form-control"
           placeholder="Année de sortie"/>
        <br />
        <!-- Producteur Requete-->
        <asp:Label runat="server">Producteur :</asp:Label>
        <pers:Personne ID="choixProducteur" runat="server"/>
        <br />
        <!-- Réalisateur Requete-->
        <asp:Label runat="server">Réalisateur :</asp:Label>
        <pers:Personne ID="choixRealisateur" runat="server"/>
        <br />
        <!-- Les 3 check box -->
        <asp:Label runat="server">Options :</asp:Label>
        <div class="form-control">
            <div class="col-sm-4">
                <!-- Version originale? -->
                <asp:Label runat="server">Version originale :</asp:Label>
                <asp:CheckBox ID="cbOriginal" runat="server"/>
            </div>
            <div class="col-sm-4">
                <!-- Version étendue?-->
                <asp:Label runat="server">Version étendue :</asp:Label>
                <asp:CheckBox ID="cbEtendue" runat="server"/>
            </div>
            <div class="col-sm-4">
                <!-- Visible à tous? -->
                <asp:Label runat="server">Visible à tous :</asp:Label>
                <asp:CheckBox ID="cbVisible" runat="server"/>
            </div>
        </div>
        <br />

        <!-- Acteur 1 Requete (control donnant choix)-->
        <asp:Label runat="server">Acteur 1 :</asp:Label>
        <pers:Personne ID="choixActeur1" runat="server"/>
        <br />
        <!-- Acteur 2  Requete (control donnant choix)-->
        <asp:Label runat="server">Acteur 2 :</asp:Label>
        <pers:Personne ID="choixActeur2" runat="server"/>
        <br />
        <!-- Acteur 3 Requete (control donnant choix) -->
        <asp:Label runat="server">Acteur 3 :</asp:Label>
        <pers:Personne ID="choixActeur3" runat="server"/>
        <br />

        <!-- Resume film -->
        <asp:Label runat="server">Résumé du film :</asp:Label>
        <asp:TextBox ID="tbResume" runat="server"
            TextMode="MultiLine"
             style="max-width:100%; min-width:100%; min-height:180px;max-height:180px;"
           CssClass="form-control"/>
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
