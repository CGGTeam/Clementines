<%@ Control Language="C#" AutoEventWireup="true" CodeFile="AjouterUtilisateur.ascx.cs" Inherits="controles_utilisateur_AjouterUtilisateur" %>

<script runat="server">

   protected void Page_Load(object sender, EventArgs e)
   {
      if (!IsPostBack)
      {
         //Remplir les types d'abonnement
      }
      chargerTypeAbonnement();
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
   protected void validationFormatCourriel(object source, ServerValidateEventArgs args)
   {
      if (tbCourriel.Text != "")
      {
         string strPattern = @"\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*";

         Regex regex = new Regex(strPattern);
         Match match = regex.Match(tbCourriel.Text);
         if (match.Success)
         {
            args.IsValid = true;
         }
         else
         {
            args.IsValid = false;
         }
      }
   }

   /// <summary>
   /// Fonction qui permet d'ajouter l'utilisateur
   /// </summary>
   protected void Ajouter(object sender, EventArgs e)
   {
      bool valide = true;
      string messageErreur = "";

      //Nom d'utilisateur
      if (!formatNomUtil.IsValid)
      {
         valide = false;
         messageErreur += "Le format du nom d'utilisateur est invalide.";
      }

      //courriel invalide
      if (!formatCourriel.IsValid)
      {
         valide = false;
         messageErreur += "Le format de courriel n'est pas valide. ";
      }
      //Mot de passe invalide
      if (!formatMotDePasse.IsValid)
      {
         valide = false;
         messageErreur += "Le format du mot de passe est invalide (#####). ";
         tbMotDePasse.Focus();
      }

      if (valide)
      {
         //Aller
         /*if (SQL.ajouterUtilisateur(tbNomUtilisateur.Text, tbCourriel.Text, int.Parse(tbMotDePasse.Text), typeAbonnement[0]))
         {
            error.Visible = false;
            succes.Visible = true;
            lblSucces.Text = "Ajout fait avec succès";
         }
         else
         {
            succes.Visible = false;
            error.Visible = true;
            lblError.Text = "Erreur lors de l'ajout dans la base de donnée";
         }*/
         if (SQL.checkIfNomUtilisateurExiste(tbNomUtilisateur.Text))
         {
            succes.Visible = false;
            error.Visible = true;
            lblError.Text = "Ce nom d'utilisateur est déjà occupé!";
         }
         else
         {
            SQL.ajouterUtilisateur(tbNomUtilisateur.Text, tbCourriel.Text, int.Parse(tbMotDePasse.Text), ddlListeAbonnement.SelectedValue[0]);
            //retourner à la page précédente.

         }
         
      }
      else
      {
         succes.Visible = false;
         error.Visible = true;
         lblError.Text = messageErreur;
      }
   }
   /// <summary>
   /// Fonction qui retourne au menu de la gestion des utilisateurs/superutilisateurs
   /// </summary>
   protected void Retour(object sender, EventArgs e)
   {

   }

   protected void fermerSucces(object sender, EventArgs e)
   {
      succes.Visible = false;
   }
   protected void fermerError(object sender, EventArgs e)
   {
      error.Visible = false;
   }


</script>
<div runat="server" Visible="false" id="succes" class="alert alert-success" role="alert">
   <asp:Label runat="server" ID="lblSucces"></asp:Label>
   <asp:LinkButton runat="server" class="btn-link pull-right" OnClick="fermerSucces">
         <span class="glyphicon glyphicon-remove pull-right"></span>
   </asp:LinkButton>
</div>

<div runat="server" Visible="false" id="error" class="alert alert-danger" role="alert">
   <asp:Label runat="server" ID="lblError"></asp:Label>
   <asp:LinkButton runat="server" type="button" class="btn-link pull-right" OnClick="fermerError">
         <span class="glyphicon glyphicon-remove"></span>
   </asp:LinkButton>
</div>
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
         <asp:RegularExpressionValidator runat="server" id="formatNomUtil"
          controltovalidate="tbNomUtilisateur" validationexpression="^[a-z]{3,10}$"
          EnableClientScript="false" Display="None" />
         <br />
       <!-- Courriel -->
        <asp:Label runat="server">Courriel :</asp:Label>
        <asp:TextBox ID="tbCourriel" runat="server"
           MaxLength="25" CssClass="form-control"
           placeholder="Courriel" type="email" pattern="[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,4}$"/>
        <asp:RequiredFieldValidator runat="server" 
             id="courrielVide"  
             Style="color:red" 
             controltovalidate="tbCourriel"
             errormessage="Entrez un courriel!" />
       <asp:CustomValidator id="formatCourriel" runat="server" 
        ControlToValidate = "tbCourriel"
        OnServerValidate="validationFormatCourriel" EnableClientScript="true" Display="None">
      </asp:CustomValidator>
         <br />

        <!-- Mot de passe -->
        <asp:Label runat="server">Mot de Passe :</asp:Label>
        <asp:TextBox ID="tbMotDePasse" runat="server" CssClass="form-control"
            placeholder="Mot de Passe Format(#####)" type="number" maxlength="5" format="NNNNN"/>
        <asp:RequiredFieldValidator runat="server" 
             id="passwordVide"  
             Style="color:red" 
             controltovalidate="tbMotDePasse"
             errormessage="Entrez un Mot de Passe!" />
       <asp:RegularExpressionValidator runat="server" id="formatMotDePasse"
          controltovalidate="tbMotDePasse" validationexpression="^[0-9]{5}$"
          EnableClientScript="false" Display="None" />
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