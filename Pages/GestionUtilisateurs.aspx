<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="../PageMaster/MasterPage.master" CodeFile="GestionUtilisateurs.aspx.cs" Inherits="Pages_GestionUtilisateurs" %>
<asp:Content ID="Content2" ContentPlaceHolderID="head" Runat="Server">
    <!-- Pour ajouter des imports dans le head -->
    <script src="../Static/scripts/UtilisateurFilter.js"></script>
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css">
  <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.3.1/jquery.min.js"></script>
    <script src="../Static/scripts/Scriptdefault.js"></script>
    <script src="../Static/scripts/librairies.js"></script>
    <link rel="stylesheet" href="../Static/styles/index.css">
  <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js"></script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="contentBody" Runat="Server">
    <!-- Contenu de la page -->
<div runat="server" Visible="false" id="succes" class="alert alert-success" role="alert">
   <asp:Label runat="server" ID="lblSucces"></asp:Label>
   <asp:LinkButton runat="server" class="btn-link pull-right" OnClick="fermerSucces">
         <span class="glyphicon glyphicon-remove pull-right"></span>
   </asp:LinkButton>
</div>

<div runat="server" Visible="false" id="error" class="alert alert-danger" role="alert">
   <asp:Label runat="server" ID="lblError"></asp:Label>
   <asp:LinkButton runat="server" type="button" class="btn-link pull-right" OnClick="fermerError">
         <span class="glyphicon glyphicon-remove"></span>
   </asp:LinkButton>
</div>
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
    <div class="row" style="align-content:center;">
        <div class="col-sm-6" style="float: none; margin: 0 auto;">
           <asp:Button runat="server" class="btn btn-lg btn-primary btn-block" Text="Ajouter un utilisateur" OnClick="pageAjouterUtil"/>
    
        </div>
    </div>
</div>
    <br />
</asp:Content>
