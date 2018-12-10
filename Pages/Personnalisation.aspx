<%@ Page Title="Acceuil" Language="C#" MasterPageFile="../PageMaster/MasterPage.master" AutoEventWireup="true" CodeFile="~/Pages/Personnalisation.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content2" ContentPlaceHolderID="head" Runat="Server">
    <!-- Pour ajouter des imports dans le head -->
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="contentBody" Runat="Server">
    <!-- Contenu de la page -->
    
<div runat="server" id="divBody" class="row">
    <div class="col-md-8 col-md-offset-2">
        <h1>Personnalisation</h1>

        <div runat="server" Visible="false" id="succes" class="alert alert-success" role="alert">
            <asp:Label runat="server" ID="lblSucces"></asp:Label>
            <asp:LinkButton runat="server" class="btn-link pull-right" OnClick="fermerSucces">
                <span class="glyphicon glyphicon-remove pull-right"></span>
            </asp:LinkButton>
        </div>

        <div runat="server" Visible="false" id="error" class="alert alert-danger" role="alert">
            <asp:Label runat="server" ID="lblError"></asp:Label>
            <asp:LinkButton runat="server" type="button" class="btn-link pull-right"  OnClick="fermerError">
                <span class="glyphicon glyphicon-remove"></span>
            </asp:LinkButton>
        </div>

        <hr />
            <div class="row">
                
                <div id="divPersoUtilisateur" class="col-sm-12 form-group">
                    <label for="tbNewPassword">  
                        Password
                    </label>
                    <asp:TextBox runat="server" ID="tbNewPassword" 
                        CssClass="form-control" placeholder="Nouveau mot de passe" 
                         MaxLength="5" pattern="[0-9]{5}" title="5 chiffres"/>

                    <asp:RequiredFieldValidator ID="PasswordPresent" runat="server"
                       ControlToValidate="tbNewPassword"
                       EnableClientScript="false"
                       Display="None" />

                    <asp:RangeValidator runat="server" Type="Integer" ID="passValide"
                        MinimumValue="11111" MaximumValue="99999" ControlToValidate="tbNewPassword" 
                        EnableClientScript="false"
                       Display="None" />
                </div>
                <hr />
                <!--
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
                -->
                <div id="divGestionPref" class="col-sm-12 form-group">
                    <h2>Gestion préférences utilisateurs</h2>

                    <br />

                    <br />

                    <asp:CheckBox runat="server" class="form-check-input" id="cbCourrielAjout" Checked="true"/>
                    <label class="form-check-label" for="cbPersonne"> Recevoir un courriel lors du retrait d'un DVD</label>             
                    
                    <br />

                    <asp:CheckBox runat="server" class="form-check-input" id="cbCourrielRetrait" Checked="true"/>
                    <label class="form-check-label" for="cbPersonne"> Recevoir un courriel lors de l'ajout d'un DVD</label>
                    
                    <br />
                    
                    <asp:CheckBox runat="server" class="form-check-input" id="cbCourrielAppropiation" Checked="true" OnClick="return filtrer();" />
                    <label class="form-check-label" for="cbPersonne">Recevoir un courriel lors de l'appropriation d'un DVD</label>
                    
                    <hr />
                </div>
                <br />
                <div class="row">
                    <div class="col-sm-4">
                        <label for="comments">
                            Nombre de DVDs par page :
                        </label>
                    </div>
                    <div class="col-sm-8 pull-left">
                        <asp:TextBox runat="server" ID="nbDVDPage" 
                            CssClass="form-control" placeholder="Nouveau mot de passe" 
                            type="number" min="10" value="10" max="100" step="5"/>
                    </div>
                </div>
                <div class="row">
                    <div class="col-sm-4">
                        <label for="comments">
                            Couleur de fond :
                        </label>
                    </div>
                    <div class="col-sm-8 pull-left">
                        <asp:TextBox runat="server" ID="couleurFond" CssClass="form-control" 
                            type="color" value="#ffffff" Text="#ffffff"/>
                    </div>
                </div>
                <div class="row">
                    <div class="col-sm-4">
                        <label for="comments">
                            Couleur du texte :
                        </label>
                    </div>
                    <div class="col-sm-8 pull-left">
                        <asp:TextBox runat="server" ID="couleurTexte" CssClass="form-control"
                            type="color"/>
                    </div>
                </div>
            </div>
        <br />
         <hr />
            <div class="row">
                <div class="col-sm-12 form-group">
                    <asp:Button runat="server" ID="modifier" OnClick="modifier_Click" 
                        Text=" Modifier la personnalisation →" CssClass="btn btn-lg btn-primary btn-block" />
                </div>
            </div>
        <br />
    </div>
</div>
</asp:Content>

