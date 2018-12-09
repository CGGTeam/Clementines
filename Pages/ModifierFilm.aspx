<%@ Page Title="Acceuil" Language="C#" MasterPageFile="../PageMaster/MasterPage.master" AutoEventWireup="true" CodeFile="ModifierFilm.aspx.cs" Inherits="_Default" %>
<%@ Register tagprefix="Personne" TagName="Film" Src="../controles-utilisateur/ModifierFilm.ascx" %>
<asp:Content ID="Content2" ContentPlaceHolderID="head" Runat="Server">
    <!-- Pour ajouter des imports dans le head -->
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="contentBody" Runat="Server">
    <!-- Contenu de la page -->
        <Personne:film runat="server"></Personne:film>

    <hr />
</asp:Content>