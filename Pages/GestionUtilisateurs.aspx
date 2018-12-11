<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="../PageMaster/MasterPage.master" CodeFile="GestionUtilisateurs.aspx.cs" Inherits="Pages_GestionUtilisateurs" %>
<asp:Content ID="Content2" ContentPlaceHolderID="head" Runat="Server">
    <!-- Pour ajouter des imports dans le head -->
    <script src="../Static/scripts/UtilisateurFilter.js"></script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="contentBody" Runat="Server">
    <!-- Contenu de la page -->

<h1>Gestion des utilisateurs</h1>
<div runat="server" id="divBody" class="row">
   <!-- Liste des utilisateurs dans un tableau dynamique-->

    <!--tb pour la recherche -->
    <div class="row">
      <div class="col-sm-4">
        <input id="filter" type="text" placeholder="Recherche.." class="form-control">
    </div>
    </div>

    <hr />

   <asp:PlaceHolder ID="phDynamique" runat="server"/>
   <div class="col-sm-6">
        <asp:Button runat="server" class="btn btn-lg btn-primary btn-block" Text="Ajouter un utilisateur" OnClick="pageAjouterUtil"/>
    </div>
</div>
</asp:Content>
