<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="../PageMaster/MasterPage.master" CodeFile="GestionUtilisateurs.aspx.cs" Inherits="Pages_GestionUtilisateurs" %>
<asp:Content ID="Content2" ContentPlaceHolderID="head" Runat="Server">
    <!-- Pour ajouter des imports dans le head -->

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="contentBody" Runat="Server">
    <!-- Contenu de la page -->

<h1>Gestion des utilisateurs</h1>
<div runat="server" id="divBody" class="row">
   <!-- Liste des utilisateurs dans un tableau dynamique-->
   <asp:PlaceHolder ID="phDynamique" runat="server"/>

</div>
</asp:Content>
