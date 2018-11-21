﻿<%@ Page Language="C#" MasterPageFile="~/PageMaster/MasterPage.master" CodeFile="DVDsAutreUtilisateur.aspx.cs" AutoEventWireup="true" Inherits="Pages_DVDsAutreUtilisateur" %>

<asp:Content ID="Content2" ContentPlaceHolderID="head" Runat="Server">
    <!-- Pour ajouter des imports dans le head -->
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css">
    <link rel="stylesheet" href="../Static/styles/index.css">
  <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.3.1/jquery.min.js"></script>
  <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js"></script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="contentBody" Runat="Server">
    <%@ Register tagprefix="pers" TagName="film" Src="../controles-utilisateur/NomFilm.ascx" %>

    <!-- Contenu de la page -->
        
        <asp:PlaceHolder id="phVignettes" runat="server" />
        <asp:PlaceHolder id="phChangerPage" runat="server" />
    

</asp:Content>