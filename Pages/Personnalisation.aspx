<%@ Page Title="Acceuil" Language="C#" MasterPageFile="../PageMaster/MasterPage.master" AutoEventWireup="true" CodeFile="~/Pages/Personnalisation.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content2" ContentPlaceHolderID="head" Runat="Server">
    <!-- Pour ajouter des imports dans le head -->
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="contentBody" Runat="Server">
    <!-- Contenu de la page -->
    
<div class="row">
    <div class="col-md-6 col-md-offset-3">
        <h2>Personnalisation</h2>
        <br />
        <hr />
            <div class="row">
                
                <div id="divPersoUtilisateur" class="col-sm-12 form-group">
                    <h2>Personnalisation Utilisateur</h2>
                    <label for="tbNewPassword">  
                        Password
                    </label>
                    <input id="tbNewPassword" placeholder="Nouveau mot de passe" type="text" class="glyphicon glyphicon-lock form-control"/>
                </div>
                <hr />
                <div id="divPersoAdmin" class="col-sm-12 form-group">
                    <h2>Personnalisation Administrateur</h2>
                    <label for="comments">
                        Password
                    </label>
                    <input id="tbNewPasswordAdmin" placeholder="Nouveau mot de passe" type="text" class="glyphicon glyphicon-lock form-control"/>
                    <label for="comments">
                        Nom
                    </label>
                    <input id="tbNewNom" placeholder="Nouveau nom d'utilisateur" type="text" class="glyphicon glyphicon-user form-control"/>
                    <label for="comments">
                        Courriel
                    </label>
                    <input id="tbNewCourriel" placeholder="Nouveau courriel d'utilisateur" type="text" class="glyphicon glyphicon-envelope form-control"/>
                </div>
                <div id="divGestionPref" class="col-sm-12 form-group">
                    <h2>Gestion préférences utilisateurs</h2>
                    <label for="comments">
                        Recevoir un courriel lors de l'ajout d'un ou plusieurs DVDs
                    </label>
                    <input id="cbCourrielLorsRetrait" type="checkbox" class="checkbox checkbox-inline" checked="checked"/>
                    <label for="comments">
                        Recevoir un courriel lors du retrait d'un DVD
                    </label>
                    <input id="cbCourrielLorsAjout" type="checkbox" class="checkbox checkbox-inline" checked="checked"/>
                    <label for="comments">
                        Recevoir un courriel lors de l'appropriation d'un DVD
                    </label>
                    <input id="cbCourrielLorsAppropriation" type="checkbox" class="checkbox checkbox-inline" checked="checked"/>
                    <br />
                    <hr />
                    <label for="comments">
                        Nombre de DVDs par page (10 à 100)
                    </label>
                    <input id="nbDVDPage" type="number" min="10" value="10" max="100" step="5"/>
                </div>
            </div>
            <div class="row">
                <div class="col-sm-12 form-group">
                    <button type="submit" class="btn btn-lg btn-primary btn-block" >Modifier la personnalisation →</button>
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

