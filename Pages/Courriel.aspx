<%@ Page Title="Acceuil" Language="C#" MasterPageFile="../PageMaster/MasterPage.master" AutoEventWireup="true" CodeFile="Courriel.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content2" ContentPlaceHolderID="head" Runat="Server">
    <!-- Pour ajouter des imports dans le head -->
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="contentBody" Runat="Server">
    <!-- Contenu de la page -->
    
<div class="row">
    <div class="col-md-6 col-md-offset-3">
        <h2>Envoyer un message</h2>
        <br />
        <p>
            Veuillez écrire votre message:
        </p>
        <hr />
            <div class="row">
                <div class="col-sm-12 form-group">
                    <label for="comments">
                        Destinaire :</label>
                    <asp:TextBox runat="server" CssClass="form-control" ID="destinaire" placeholder="Votre distinaire" />

                    <br />
                    <label for="comments">
                        Message :</label>
                    <textarea class="form-control" name="comments" 
                        id="comments" placeholder="Votre message" maxlength="6000" rows="7" style="max-width:100%; max-height:600px;"></textarea>
                </div>
            </div>

                        <div class="row">
                <div class="col-sm-12 form-group">
                    <button type="submit" class="btn btn-lg btn-primary btn-block" >Envoyer →</button>
                </div>
            </div>

        <div id="success_message" style="width:100%; height:100%; display:none; ">
            <h3>Message envoyer sans erreure!</h3>
        </div>
        <div id="error_message"
                style="width:100%; height:100%; display:none; ">
                    <h3>Error</h3>
                    Une erreure est survenue

        </div>
    </div>
</div>
</asp:Content>

