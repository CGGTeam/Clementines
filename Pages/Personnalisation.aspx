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
                    <asp:TextBox runat="server" ID="tbNewPassword" 
                        CssClass="form-control" placeholder="Nouveau mot de passe" />
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

                    <br />

                    <asp:CheckBox runat="server" class="form-check-input" id="cbCourrielRetrait" Checked="true"/>
                    <label class="form-check-label" for="cbPersonne"> Recevoir un courriel lors de l'ajout d'un DVD</label>
                    
                    <br />

                    <asp:CheckBox runat="server" class="form-check-input" id="cbCourrielAjout" Checked="true"/>
                    <label class="form-check-label" for="cbPersonne"> Recevoir un courriel lors du retrait d'un DVD</label>             
                    
                    <br />
                    
                    <asp:CheckBox runat="server" class="form-check-input" id="cbRecevoirCourrielAppropiation" Checked="true" OnClick="return filtrer();" />
                    <label class="form-check-label" for="cbPersonne">Recevoir un courriel lors de l'appropriation d'un DVD</label>
                    
                    <hr />
                </div>
                <br />
                <div class="row">
                    <div class="col-sm-8">
                        <label for="comments">
                            Nombre de DVDs par page :
                        </label>
                    </div>
                    <div class="col-sm-4">
                        <asp:TextBox runat="server" ID="nbDVDPage" 
                            CssClass="form-control" placeholder="Nouveau mot de passe" 
                            type="number" min="10" value="10" max="100" step="5"/>
                    </div>
                </div>
            </div>
        <br />
         <hr />
            <div class="row">
                <div class="col-sm-12 form-group">
                    <button type="submit" class="btn btn-lg btn-primary btn-block" >Modifier la personnalisation →</button>
                </div>
            </div>
        <br />
    </div>
</div>
</asp:Content>

