﻿<%@ Page Title="Acceuil" Language="C#" MasterPageFile="../PageMaster/MasterPage.master" AutoEventWireup="true" CodeFile="AffichageDetaille.aspx.cs" Inherits="_Default" %>
<%@ Register tagprefix="AffichageDetaille" TagName="Film" Src="../controles-utilisateur/AffichageDetaille.ascx" %>
<asp:Content ID="Content2" ContentPlaceHolderID="head" Runat="Server">
    <!-- Pour ajouter des imports dans le head -->
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css">
    <link rel="stylesheet" href="../Static/styles/AffichageDetaille.css">
  <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.3.1/jquery.min.js"></script>
  <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js"></script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="contentBody" Runat="Server">
    <!-- Contenu de la page -->
    <h1>Affichage détaillé du film [Mettre titre du film]</h1>
    <hr />

   <AffichageDetaille:Film runat="server"></AffichageDetaille:Film>

    <hr />
</asp:Content>
