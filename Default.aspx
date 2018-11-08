<%@ Page Title="Acceuil" Language="C#" MasterPageFile="PageMaster/MasterPage.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>
<%@ Register tagprefix="pers" TagName="film" Src="controles-utilisateur/NomFilm.ascx" %>

<asp:Content ID="Content2" ContentPlaceHolderID="head" Runat="Server">
    <!-- Pour ajouter des imports dans le head -->
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css">
  <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.3.1/jquery.min.js"></script>
  <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js"></script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="contentBody" Runat="Server">
    <!-- Contenu de la page -->
    <h1>CLÉMENTINE JULIEN</h1>
    <hr />
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
              <div class="panel-body">
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

    <hr />

    <asp:PlaceHolder id="phDynamique" runat="server" />
</asp:Content>

