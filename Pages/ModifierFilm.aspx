<%@ Page Title="Acceuil" Language="C#" MasterPageFile="../PageMaster/MasterPage.master" AutoEventWireup="true" CodeFile="ModifierFilm.aspx.cs" Inherits="_Default" %>
<%@ Register tagprefix="Personne" TagName="Film" Src="../controles-utilisateur/ModifierFilm.ascx" %>
<asp:Content ID="Content2" ContentPlaceHolderID="head" Runat="Server">
    <!-- Pour ajouter des imports dans le head -->
   <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css">
   <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.3.1/jquery.min.js"></script>
    <script src="../Static/scripts/Scriptdefault.js"></script>
    <script src="../Static/scripts/librairies.js"></script>
    <link rel="stylesheet" href="../Static/styles/index.css">
  <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js"></script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="contentBody" Runat="Server">
    <!-- Contenu de la page -->
        <Personne:film runat="server"></Personne:film>

    <hr />
</asp:Content>