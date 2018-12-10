<%@ Control Language="C#" AutoEventWireup="true" CodeFile="AjouterUtilisateur.ascx.cs" Inherits="controles_utilisateur_AjouterUtilisateur" %>

<script runat="server">

   protected void Page_Load(object sender, EventArgs e)
   {
      if (!IsPostBack)
      {
         //Remplir les types d'abonnement
         chargerTypeAbonnement();
      }
   }

   protected void chargerTypeAbonnement()
   {
      SQL.Connection();
      List<EntiteTypeAbonnement> entiteTypeAbonnements = new List<EntiteTypeAbonnement>();
      entiteTypeAbonnements = SQL.FindAllTypeAbonnements();
      foreach(EntiteTypeAbonnement typeUtil in entiteTypeAbonnements)
      {
         ddlListeAbonnement.Items.Add(new ListItem(typeUtil.Description,typeUtil.TypeUtilisateur.ToString()));
      }
   }
   /// <summary>
   /// Fonction qui permet d'ajouter l'utilisateur
   /// </summary>
   protected void Ajouter(object sender, EventArgs e)
   {
      //if(courrielVide.)
   }
   /// <summary>
   /// Fonction qui retourne au menu de la gestion des utilisateurs/superutilisateurs
   /// </summary>
   protected void Retour(object sender, EventArgs e)
   {

   }


</script>

<div class="row">
    <div class="col-sm-6">
        <!-- Nom d'utilisateur -->
        <asp:Label runat="server">Nom d'utilisateur :</asp:Label>
        <asp:TextBox ID="tbNomUtilisateur" runat="server"
           MaxLength="25" CssClass="form-control"
            placeholder="Nom d'utilisateur"/>
        <asp:RequiredFieldValidator runat="server" 
             id="usernameVide"  
             Style="color:red" 
             controltovalidate="tbNomUtilisateur"
             errormessage="Entrez un nom d'utilisateur!"/>
         <br />
       <!-- Courriel -->
        <asp:Label runat="server">Courriel :</asp:Label>
        <asp:TextBox ID="tbCourriel" runat="server"
           MaxLength="25" CssClass="form-control"
            placeholder="Nom d'utilisateur"/>
        <asp:RequiredFieldValidator runat="server" 
             id="courrielVide"  
             Style="color:red" 
             controltovalidate="tbNomUtilisateur"
             errormessage="Entrez un courriel!" />
         <br />

        <!-- Mot de passe -->
        <asp:Label runat="server">Mot de Passe :</asp:Label>
        <asp:TextBox ID="tbMotDePasse" runat="server"
           MaxLength="25" CssClass="form-control"
            placeholder="Mot de Passe" />
        <asp:RequiredFieldValidator runat="server" 
             id="passwordAbsent"  
             Style="color:red" 
             controltovalidate="tbNomUtilisateur"
             errormessage="Entrez un Mot de Passe!" />
         <br />

        <!-- Type d'utilisateurs -->
        <asp:Label runat="server">Type d'abonnement :</asp:Label>
        <asp:DropDownList ID="ddlListeAbonnement" runat="server"
           MaxLength="25" CssClass="form-control"
            placeholder="Mot de Passe" />
         <br />
       </div>
   </div>
   <div class="row">
       <div class="col-sm-6">
           <asp:Button ID="btnAjouterUtilisateur" runat="server" class="btn btn-lg btn-primary btn-block" Text="Ajouter" OnClick="Ajouter"/>
          <asp:Button ID="btnRetourGestionUtilisateur" runat="server" class="btn btn-lg btn-danger btn-block" Text="Annuler" OnClick="Retour" CausesValidation="false"/>
       </div>
       <div class="col-sm-6">
           
       </div>
   </div>