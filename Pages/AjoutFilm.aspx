<%@ Page Title="Acceuil" Language="C#" MasterPageFile="../PageMaster/MasterPage.master" AutoEventWireup="true" CodeFile="AjoutFilm.aspx.cs" Inherits="_Default" %>
<%@ Register tagprefix="pers" TagName="Film" Src="../controles-utilisateur/Film.ascx" %>


<asp:Content ID="Content2" ContentPlaceHolderID="head" Runat="Server">
    <!-- Pour ajouter des imports dans le head -->
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="contentBody" Runat="Server">
    <!-- Contenu de la page -->
    <h1>Ajout de film</h1>
    <hr />

    <pers:film runat="server"></pers:film>

    <hr />
</asp:Content>

