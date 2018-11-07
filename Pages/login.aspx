<%@ Page Title="Login" Language="C#" MasterPageFile="../PageMaster/MasterPage.master" AutoEventWireup="true" CodeFile="login.aspx.cs" Inherits="_Default" %>
<%@ Register tagprefix="pers" TagName="Identifiant" Src="/controles-utilisateur/Identifiant.ascx" %>
<%@ Register tagprefix="pers" TagName="Password" Src="/controles-utilisateur/Identifiant.ascx" %>

<asp:Content ID="Content2" ContentPlaceHolderID="head" Runat="Server">
    <!-- Pour ajouter des imports dans le head -->
    <link rel="stylesheet" href="../Static/styles/login.css">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="contentBody" Runat="Server">
    <!-- Contenu de la page -->
    <div class="form-signin">

        <img src="../Static/images/logo.png">
        <br /><br /><br />
        <h2 class="form-signin-heading">Identifiez-vous!</h2>

        <label for="tbIdentifiant" class="sr-only">Identifiant</label>
        <pers:Identifiant runat="server" id="tbIdentifiant" placeholder="Votre identifiant" CssClass="form-control"></pers:Identifiant>

        <br />

        <label for="tbPassword" class="sr-only">Password</label>
        <pers:Identifiant runat="server" id="tbPassword" placeholder="Votre mot de passe" CssClass="form-control"></pers:Identifiant>

        <div class="checkbox">
          <hr />
        </div>
        <button class="btn btn-lg btn-primary btn-block" type="submit">Se Connecter</button>
    </div>
</asp:Content>

