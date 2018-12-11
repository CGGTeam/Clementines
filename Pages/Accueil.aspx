<%@ Page Title="Acceuil" Language="C#" MasterPageFile="../PageMaster/MasterPage.master" AutoEventWireup="true" CodeFile="Accueil.aspx.cs" Inherits="_Default" %>
<%@ Register tagprefix="pers" TagName="filmAbrege" Src="../controles-utilisateur/NomFilm.ascx" %>

<asp:Content ID="Content2" ContentPlaceHolderID="head" Runat="Server">
    <!-- Pour ajouter des imports dans le head -->
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css">
  <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.3.1/jquery.min.js"></script>
    <script src="../Static/scripts/Scriptdefault.js"></script>
    <link rel="stylesheet" href="../Static/styles/index.css">
  <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js"></script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="contentBody" Runat="Server">
    <!-- Contenu de la page -->
  

    <div class="row">
        <div class="col-sm-6">
            <div class="input-group">
                <asp:TextBox runat="server" ID="tbRecherche" CssClass="tbRecherhce"></asp:TextBox>
                <asp:LinkButton runat="server" id="btnRecherche" OnClick="UpdateFiltre">
                            <div class="glyphicon glyphicon-search"></div>
                </asp:LinkButton> 
            </div>
            <div class="input-group">
                <asp:CheckBox runat="server" class="form-check-input" id="cbTitre" Checked="true" OnClick="return filtrer();" />
                <label class="form-check-label" for="cbTitre">Titre</label>

              &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;

                <asp:CheckBox runat="server" class="form-check-input" id="cbPersonne" Checked="true" OnClick="return filtrer();" />
                <label class="form-check-label" for="cbPersonne">Propriétaire</label>
            </div>
        </div>
        <div class="col-sm-2 pull-right ">
            <label class="form-check-label">Trier par :</label>
            <asp:DropDownList ID="ddlOrdeyBy" runat="server" CssClass="form-control" onchange="filtrer()">
                <asp:ListItem Enabled="true" Text="Titre et personne" Value="TitrePersonne"></asp:ListItem>
                <asp:ListItem Text="Titre" Value="Titre"></asp:ListItem>
                <asp:ListItem Text="Personne" Value="Personne"></asp:ListItem>
            </asp:DropDownList>
        </div>
    </div>
    
    <hr />
    <asp:FileUpload id="btnUploadImagePochette" runat="server" CssClass="form-control"/> <!--d-->
    <asp:PlaceHolder id="phDynamique" runat="server" />
</asp:Content>

