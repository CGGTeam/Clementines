<%@ Page Language="C#" MasterPageFile="~/PageMaster/MasterPage.master" CodeFile="DVDEnMain.aspx.cs" AutoEventWireup="true" Inherits="Pages_DVDEnMain" %>

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
        <asp:PlaceHolder id="phVignettes" runat="server" />
        <asp:PlaceHolder id="phChangerPage" runat="server" />
    

</asp:Content>

