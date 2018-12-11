<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ModifierUtilisateur.ascx.cs" Inherits="controles_utilisateur_ModifierUtilisateur" %>

<script runat="server">
   int id = 0;
   EntiteUtilisateur valeurDepart;
   protected void Page_Load(object sender, EventArgs e)
   {
      //int id = 0;
      if (!IsPostBack)
      {
         if(Request["Utilisateur"] != null)
         {
            id = int.Parse(Page.Request["Utilisateur"].ToString());
            valeurDepart = SQL.FindUtilisateurById(id);
         }
         chargerTypeAbonnement();
         EntiteUtilisateur utilisateurAModifier = SQL.FindUtilisateurById(id);
         titreModif.InnerText = "Modification de l'utilisateur: " + utilisateurAModifier.NomUtilisateur.ToString();

         //Charger les anciennes valeurs de l'utilisateur
         tbNomUtilisateur.Text = utilisateurAModifier.NomUtilisateur.Trim();
         tbCourriel.Text = utilisateurAModifier.Courriel.Trim();
         tbMotDePasse.Text = utilisateurAModifier.MotPasse.ToString().Trim();
         ddlListeAbonnement.Items.FindByValue(utilisateurAModifier.TypeUtilisateur.ToString()).Selected = true;
      }

       if(Request["Utilisateur"] != null)
       {
         id = int.Parse(Page.Request["Utilisateur"].ToString());
         valeurDepart = SQL.FindUtilisateurById(id);
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
   protected void Modifier(object sender, EventArgs e)
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
         if ((SQL.checkIfNomUtilisateurExiste(tbNomUtilisateur.Text)) && (tbNomUtilisateur.Text.Trim() != valeurDepart.NomUtilisateur.Trim()))
         {
            succes.Visible = false;
            error.Visible = true;
            lblError.Text = "Ce nom d'utilisateur est déjà occupé!";
         }
         else if ((SQL.checkIfCourrielUtilisateurExiste(tbCourriel.Text)) && (tbCourriel.Text.Trim() != valeurDepart.Courriel.Trim()))
         {
            succes.Visible = false;
            error.Visible = true;
            lblError.Text = "Ce courriel d'utilisateur est déjà occupé";
         }
         else
         {
            SQL.modifierUtilisateur(id, tbNomUtilisateur.Text.Trim(), tbCourriel.Text.Trim(), int.Parse(tbMotDePasse.Text.ToString()), ddlListeAbonnement.SelectedValue.ToString()[0]);
            String url = "~/Pages/GestionUtilisateurs.aspx";
            Response.Redirect(url, true);
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
      String url = "~/Pages/GestionUtilisateurs.aspx";
      Response.Redirect(url, true);
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
<div class="row">
    <div class="col-md-6 col-md-offset-3">
                <h1 id="titreModif" runat="server"></h1>
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
        <hr />
        <!-- Nom d'utilisateur -->
        <asp:Label runat="server" style="text-align:center;">Nom d'utilisateur :</asp:Label>
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
       <div class="col-sm-6" style="float: none; margin: 0 auto;">
          <asp:Button ID="btnAjouterUtilisateur" runat="server" class="btn btn-lg btn-primary btn-block" Text="Modifier" OnClick="Modifier"/>
          <asp:Button ID="btnRetourGestionUtilisateur" runat="server" class="btn btn-lg btn-danger btn-block" Text="Annuler" OnClick="Retour" CausesValidation="false"/>
       </div>
       <div class="col-sm-6">
       
       </div>
   </div>

