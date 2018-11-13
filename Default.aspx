<%@ Page Title="Login" Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>
<%@ Register tagprefix="pers" TagName="Identifiant" Src="controles-utilisateur/Identifiant.ascx" %>
<%@ Register tagprefix="pers" TagName="Password" Src="controles-utilisateur/Identifiant.ascx" %>

<!DOCTYPE html>

<html>

<head runat="server">
    <title>
        <%: Page.Title %> - Clémentine
    </title>

      <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.3.1/jquery.min.js"></script>
    <link rel="stylesheet" href="~/Static/styles/StyleSheet.css" runat="server" media="screen">
    <script src="Static/scripts/JavaScript.js" ></script>
    <script src="../Static/scripts/JavaScript.js" ></script>
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css">

    <link rel="shortcut icon" type="image/x-icon" href="~/Static/images/flavicon.png" />
    <link rel="stylesheet" href="../Static/styles/login.css">

</head>
<body>
    <form runat="server">
     <div id="main-content" class="container">
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
        <asp:Button runat="server" CssClass="btn btn-lg btn-primary btn-block" OnClick="tentativeLogin" Text="Se connecter"/>
    </div>
         
 </div>
        </form>
</body>
</html>

