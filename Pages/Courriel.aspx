<%@ Page Title="Acceuil" Language="C#" MasterPageFile="../PageMaster/MasterPage.master" AutoEventWireup="true" CodeFile="Courriel.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content2" ContentPlaceHolderID="head" Runat="Server">
    <!-- Pour ajouter des imports dans le head -->
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="contentBody" Runat="Server">
    <!-- Contenu de la page -->
    
<div class="row">
    <div class="col-md-6 col-md-offset-3">
        <h2>Envoyer un message</h2>
        <asp:LinkButton runat="server" class="btn btn-danger" Text="Retour" onclick="Retour">
            <span class="glyphicon glyphicon-chevron-left"></span>Retour
        </asp:LinkButton>
        <hr />
        <br />
        <p>
            Veuillez écrire votre message:
        </p>
        <hr />

        <div runat="server" Visible="false" id="error_message" class="alert alert-danger" role="alert">
            <asp:Label runat="server" ID="lblError">Une erreure est survenue, le message et le destinataire ne peuvent pas être vide.</asp:Label>
            <asp:LinkButton runat="server" type="button" class="btn-link pull-right"  OnClick="fermerError">
                <span class="glyphicon glyphicon-remove"></span>
            </asp:LinkButton>
        </div>
        <div runat="server" Visible="false" id="success_message" class="alert alert-success" role="alert">
            <asp:Label runat="server" ID="lblSucces">Message envoyer sans erreure!</asp:Label>
            <asp:LinkButton runat="server" class="btn-link pull-right" OnClick="fermerSucces">
                <span class="glyphicon glyphicon-remove pull-right"></span>
            </asp:LinkButton>
        </div>
            <div class="row">
               <div class="col-sm-12 form-group">
                    <label for="comments">
                        Destinaire :
                    </label>
                   <asp:TextBox runat="server" CssClass="form-control" ID="destinaire" placeholder="Votre distinaire" />
                    
                   <br />
                    <label for="comments">
                        Message :
                    </label>
                    <asp:TextBox CssClass="form-control" ID="tbMessage" MaxLength="60000" Rows="7" TextMode="MultiLine" placeholder="Votre message" style="max-width:100%; max-height:600px;" runat="server"/>
                    <asp:CustomValidator id="messagePresent"
                       OnServerValidate="envoyerMessage"
                       ControlToValidate="tbMessage"
                       ValidateEmptyText="true"
                       runat="server"/>

                </div>
               </div>
                        <div class="row">
                <div class="col-sm-12 form-group">
                    <asp:Button CssClass="btn btn-lg btn-primary btn-block" Text="Envoyer →" OnClick="envoyerMessage" runat="server"/>
                </div>
            </div>
    </div>
</div>
</asp:Content>