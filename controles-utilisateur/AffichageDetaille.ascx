<%@ Control Language="C#" %>
<script runat="server">

</script>

 <div class="panel panel-default">
        <div class="panel-body">
            <div class="row">
        <div class="col-sm-3">
            <img src="../Static/images/logo.png" class="vignette"/>
        </div>

        <div class="col-sm-4">
                <div>
                    <asp:Label runat="server" CssClass="label-gras">Titre originale</asp:Label>
                   
                </div>
                <div>
                    <asp:Label runat="server" CssClass="label-gras">Producteur</asp:Label>
                    
                </div>
                <div>
                    <asp:Label runat="server" CssClass="label-gras">Année de sortie</asp:Label>
                   
                </div>
                <div>
                    <asp:Label runat="server" CssClass="label-gras">Durée</asp:Label>
                   
                </div>
                <div>
                    <asp:Label runat="server" CssClass="label-gras">Nombres de disques</asp:Label>
                   
                </div>
                <div>
                    <asp:Label runat="server" CssClass="label-gras">Format</asp:Label>
                   
                </div>
                <div>
                    <asp:Label runat="server" CssClass="label-gras">Titre francais</asp:Label>
                    
                </div>
                <div>
                    <asp:Label runat="server" CssClass="label-gras">Réalisateur</asp:Label>
                   
                </div>
                <div>
                    <asp:Label runat="server" CssClass="label-gras">Acteur 1</asp:Label>
                    
                </div>
                <div>
                    <asp:Label runat="server" CssClass="label-gras">Acteur 2</asp:Label>
                   
                </div>
                <div>
                    <asp:Label runat="server" CssClass="label-gras">Acteur 3</asp:Label>
                    
                </div>
                <div>
                    <asp:Label runat="server" CssClass="label-gras">Langue</asp:Label>
                   
                </div> 
                <div>
                    <asp:Label runat="server" CssClass="label-gras">Sous-titres</asp:Label>
                   
                </div>
            </div>
        <!--</div> -->
            <div class="col-sm-5">
                <div>
                     <asp:Label ID="lblTitreOriginal" runat="server"
                      Text="Titre original" CssClass="label-non-gras"/>
                </div>
                <div>
                    <asp:Label ID="lblProducteur" runat="server"
                      Text="Producteur" CssClass="label-non-gras"/>
                </div>
                <div>
                     <asp:Label ID="lblAnneeSortie" runat="server"
                      Text="Année de sortie" CssClass="label-non-gras"/>
                </div>
                <div>
                     <asp:Label ID="lblDuree" runat="server"
                      Text="Durée" CssClass="label-non-gras"/>
                </div>
                <div>
                     <asp:Label ID="lblNbDisques" runat="server"
                      Text="Nombres de disques" CssClass="label-non-gras"/>
                </div>
                <div>
                     <asp:Label ID="lblFormat" runat="server"
                      Text="Format" CssClass="label-non-gras"/>
                </div>
                <div>
                    <asp:Label ID="lblTitreFrancais" runat="server"
                      Text="Titre francais" CssClass="label-non-gras"/>
                </div>
                <div>
                     <asp:Label ID="lblRealisateur" runat="server"
                      Text="Réalisateur" CssClass="label-non-gras"/>
                </div>
                <div>
                    <asp:Label ID="lblActeur1" runat="server"
                      Text="Acteur 1" CssClass="label-non-gras"/>
                </div>
                <div>
                     <asp:Label ID="lblActeur2" runat="server"
                      Text="Acteur 2" CssClass="label-non-gras"/>
                </div>
                <div>
                    <asp:Label ID="lblActeur3" runat="server"
                      Text="Acteur 3" CssClass="label-non-gras"/>
                </div>
                <div>
                     <asp:Label ID="lblLangue" runat="server"
                        Text="Langue" CssClass="label-non-gras"/>
                </div>
                <div>
                     <asp:Label ID="lblSousTitres" runat="server"
                        Text="Sous-titres" CssClass="label-non-gras"/>
                </div>
            </div>
         </div>
    </div>
</div>
<!-- TODO : ajouter d'autres champs, modifier textbox pour des dropdown list  -->
<div class="row">
    <div class="col-sm-6">
        <asp:Button runat="server" class="btn btn-lg btn-primary btn-block" Text="Ajouter"/>
    </div>
    <div class="col-sm-6">
        <asp:Button runat="server" class="btn btn-lg btn-danger btn-block" Text="Annuler"/>
    </div>
</div>
