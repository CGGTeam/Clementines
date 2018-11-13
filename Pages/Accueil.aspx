<%@ Page Title="Acceuil" Language="C#" MasterPageFile="../PageMaster/MasterPage.master" AutoEventWireup="true" CodeFile="Accueil.aspx.cs" Inherits="_Default" %>
<%@ Register tagprefix="pers" TagName="film" Src="../controles-utilisateur/NomFilm.ascx" %>
<%@ Register tagprefix="pers" TagName="identifiant" Src="../controles-utilisateur/NomFilm.ascx" %>

<asp:Content ID="Content2" ContentPlaceHolderID="head" Runat="Server">
    <!-- Pour ajouter des imports dans le head -->
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css">
  <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.3.1/jquery.min.js"></script>
    <script src="../Static/scripts/Scriptdefault.js"></script>
  <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js"></script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="contentBody" Runat="Server">
    <!-- Contenu de la page -->
    <h1>CLÉMENTINE</h1>
    <hr />
    <!--   P'tit truc cute pour ajouter des document (juste titre)
    <div class="row">
        <div class="col-sm-4">
          <div class="panel panel-default">
            <div class="panel-heading">
              <h4 class="panel-title">
                <a data-toggle="collapse" data-parent="#accordion" href="#collapse1">
                Enregistrer un nouveau film</a>
              </h4>
            </div>
            <div id="collapse1" class="panel-collapse collapse out">
              <div class="panel-body">http://localhost:59445/~Static/scripts/Scriptdefault.js
                  <label for="tbNomFilm" class="sr-only">Nom du film</label>
                    <pers:film runat="server" id="tbNomFilm" placeholder="Nom du film" CssClass="form-control"></pers:film>

                    <div class="checkbox">
                      <hr />
                    </div>
                    <asp:Button runat="server" class="btn btn-lg btn-primary btn-block" Text="Enregistrer le film"/>
   
              </div>
            </div>
          </div>
        </div>
    </div>
    -->
    <div class="input-group">
        <asp:TextBox runat="server" ID="tbRecherche" CssClass="tbRecherhce"></asp:TextBox>
        <asp:LinkButton runat="server" id="btnRecherche" OnClick="UpdateFiltre">
                    <div class="glyphicon glyphicon-search"></div>
        </asp:LinkButton> 
    </div>
    <div class="input-group">
        <asp:HiddenField runat="server" ID="hfTitre" Value="true"/>
        <asp:CheckBox runat="server" class="form-check-input" id="cbTitre" Checked="true" OnCheckedChanged="Check" AutoPostBack="True"/>
        <label class="form-check-label" for="cbTitre">Titre</label>

      &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;

        <asp:HiddenField runat="server" ID="hfPersonne" Value="false"/>
        <asp:CheckBox runat="server" class="form-check-input" id="cbPersonne" OnCheckedChanged="Check" AutoPostBack="True"/>
        <label class="form-check-label" for="cbPersonne">Propriétaire</label>
    </div>
    <hr />

    <asp:PlaceHolder id="phDynamique" runat="server" />
</asp:Content>

