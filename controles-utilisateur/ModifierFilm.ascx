﻿<%@ Control Language="C#" %>
<%@ Register tagprefix="pers" TagName="Personne" Src="Personne_Ddl_Ajout.ascx" %>
<script runat="server">
   static string prevPage = String.Empty;

   protected void Page_Load(object sender, EventArgs e)
   {

      if (!IsPostBack)
      {
         chargeListeSupplements();
         chargeListeSousTitres();
         chargeListeLangues();
         chargeListeNbCD();
         chargeListeAnneeSortie();
         chargeListeFormats();
         chargeListeCategories();
         if (Request.UrlReferrer != null)
         {
            prevPage = Request.UrlReferrer.ToString();
         }
         foreach (Control c in this.Controls)
         {
            if (c is Personne_Ddl_Ajout)
               chargeListeRequete((Personne_Ddl_Ajout) c);
         }
         if (Request.QueryString["Film"] != null)
         {
            string btnModifier = Request.QueryString["Film"];
            int idFilm = int.Parse(btnModifier);
            SQL.Connection();
            EntiteFilm filmAModifier = SQL.FindFilmById(idFilm);
            //Maintenant remplir les informations à partir de ce ID.
            Titre.Text = "Modification du film: " + filmAModifier.TitreOriginal.ToString();
            tbTitreFrancais.Text = filmAModifier.TitreFrancais.ToString();
            tbTitreOriginal.Text = filmAModifier.TitreOriginal.ToString();
            //ddlAnnee.ClearSelection(); //making sure the previous selection has been cleared
            ddlAnnee.Items.FindByValue(filmAModifier.AnneeSortie.ToString().Trim()).Selected = true;
            ddlCategorie.Items.FindByText(filmAModifier.Categorie.ToString().Trim()).Selected = true;

            //Suppléments
            foreach (EntiteSupplements supplement in filmAModifier.lstSupplements)
            {
               lbSupplements.Items[supplement.NoSupplement].Selected = true;
            }
            //Langues
            foreach (EntiteLangue langue in filmAModifier.lstLangues)
            {
               lbLangue.Items[langue.NoLangue].Selected = true;
            }
            //Sous-titres
            foreach (EntiteSousTitres sousTitres in filmAModifier.lstSousTitres)
            {
               lbSousTitre.Items[sousTitres.NoSousTitre].Selected = true;
            }

            //Remplir les producteurs et réalisateurs.
            choixProducteur.ControleDDL.Items.FindByText(filmAModifier.NomProducteur.ToString()).Selected = true;
            choixRealisateur.ControleDDL.Items.FindByText(filmAModifier.NomRealisateur.ToString()).Selected = true;

            // Remplir les acteurs
            for(int i = 0; i < 3; i++)
            {
               if((i < filmAModifier.lstActeurs.Count()) && (filmAModifier.lstActeurs.Count != 0))
               {
                  if(i == 0)
                  {
                     choixActeur1.ControleDDL.Items.FindByValue(filmAModifier.lstActeurs.ElementAt(i).NoActeur.ToString()).Selected = true;
                  }
                  else if (i == 1)
                  {
                     choixActeur2.ControleDDL.Items.FindByValue(filmAModifier.lstActeurs.ElementAt(i).NoActeur.ToString()).Selected = true;
                  }
                  else if (i == 2)
                  {
                     choixActeur3.ControleDDL.Items.FindByValue(filmAModifier.lstActeurs.ElementAt(i).NoActeur.ToString()).Selected = true;
                  }
               }
            }
            //Durée
            tbDuree.Text = filmAModifier.Duree.ToString();

            //Format
            ddlFormat.Items.FindByText(filmAModifier.Format.ToString()).Selected = true;
            //NbDisques
            ddlNbDisques.Items.FindByText(filmAModifier.NbDisques.ToString()).Selected = true;

            //Les options
            cbEtendue.Checked = filmAModifier.VersionEtendue;
            cbOriginal.Checked = filmAModifier.FilmOriginal;
            //Le résumé
            tbResume.Text = filmAModifier.Resume.ToString();

         }
      }
   }


   protected void chargeListeRequete(Personne_Ddl_Ajout control)
   {
      SQL.Connection();
      if(control == choixProducteur)
      {
         List<EntiteProducteur> lstProducteurs  = SQL.FindAllProducteur();
         control.ControleDDL.Items.Add(new ListItem("-- Aucun --", "0"));
         foreach (EntiteProducteur producteur in lstProducteurs)
         {
            control.ControleDDL.Items.Add(new ListItem(producteur.Nom, producteur.NoProducteur.ToString()));
         }
      }else if (control == choixRealisateur)
      {
         List<EntiteRealisateur> lstRealisateurs  = SQL.FindAllRealisateur();
         control.ControleDDL.Items.Add(new ListItem("-- Aucun --", "0"));
         foreach (EntiteRealisateur realisa in lstRealisateurs)
         {
            control.ControleDDL.Items.Add(new ListItem(realisa.Nom, realisa.NoRealisateur.ToString()));
         }
      }else if (control == choixActeur1)
      {
         List<EntiteActeur> lstActeurs  = SQL.FindAllActeurs();
         control.ControleDDL.Items.Add(new ListItem("-- Aucun --", "0"));
         foreach (EntiteActeur acteur in lstActeurs)
         {
            control.ControleDDL.Items.Add(new ListItem(acteur.Nom, acteur.NoActeur.ToString()));
         }
      }else if (control == choixActeur2)
      {
         List<EntiteActeur> lstActeurs  = SQL.FindAllActeurs();
         control.ControleDDL.Items.Add(new ListItem("-- Aucun --", "0"));
         foreach (EntiteActeur acteur in lstActeurs)
         {
            control.ControleDDL.Items.Add(new ListItem(acteur.Nom, acteur.NoActeur.ToString()));
         }
      }else if (control == choixActeur3)
      {
         List<EntiteActeur> lstActeurs  = SQL.FindAllActeurs();
         control.ControleDDL.Items.Add(new ListItem("-- Aucun --", "0"));
         foreach (EntiteActeur acteur in lstActeurs)
         {
            control.ControleDDL.Items.Add(new ListItem(acteur.Nom, acteur.NoActeur.ToString()));
         }
      }


   }

   protected void chargeListeSupplements()
   {
      SQL.Connection();
      List<EntiteSupplements> lstSupplements = SQL.FindAllSupplement();
      lbSupplements.Items.Add(new ListItem("-- Aucun --", "0"));
      foreach (EntiteSupplements supplement in lstSupplements)
      {
         lbSupplements.Items.Add(new ListItem(supplement.Description, supplement.NoSupplement.ToString()));
      }
      lbSupplements.Rows = lbSupplements.Items.Count;
      //lbSupplements.Items.FindByValue("0").Selected = true;
   }

   protected void chargeListeSousTitres()
   {
      SQL.Connection();
      List<EntiteSousTitres> lstSousTitres = SQL.FindAllSousTitre();
      lbSousTitre.Items.Add(new ListItem("-- Aucun --", "0"));
      foreach (EntiteSousTitres sousTitres in lstSousTitres)
      {
         lbSousTitre.Items.Add(new ListItem(sousTitres.LangueSousTitre, sousTitres.NoSousTitre.ToString()));
      }
      lbSousTitre.Rows = lbSousTitre.Items.Count;
      //lbSousTitre.Items.FindByValue("0").Selected = true;
   }

   protected void chargeListeLangues()
   {
      SQL.Connection();
      List<EntiteLangue> lstLangues = SQL.FindAllLangue();
      lbLangue.Items.Add(new ListItem("-- Aucune --", "0"));
      foreach (EntiteLangue langue in lstLangues)
      {
         lbLangue.Items.Add(new ListItem(langue.Langue, langue.NoLangue.ToString()));
      }
      lbLangue.Rows = lbLangue.Items.Count;
      //lbLangue.Items.FindByValue("0").Selected = true;
   }

   protected void chargeListeFormats()
   {
      SQL.Connection();
      List<EntiteFormat> lstFormat = SQL.FindAllFormat();
      ddlFormat.Items.Add(new ListItem("-- Aucun --", "0"));
      foreach (EntiteFormat format in lstFormat)
      {
         ddlFormat.Items.Add(new ListItem(format.Description, format.NoFormat.ToString()));
      }
   }

   protected void chargeListeCategories()
   {
      SQL.Connection();
      List<EntiteCategorie> lstCategorie = SQL.FindAllCategorie();
      ddlCategorie.Items.Add(new ListItem("-- Aucune --", "0"));
      foreach (EntiteCategorie categorie in lstCategorie)
      {
         ddlCategorie.Items.Add(new ListItem(categorie.Description, categorie.NoCategorie.ToString()));
      }
   }

   protected void Retour(object sender, EventArgs e)
   {
      Response.Redirect(prevPage);
   }
   protected void Modifier(object sender, EventArgs e)
   {
      /*=============================================================================================================
       ==============================================================================================================
       ==============================================================================================================
       ==============================================================================================================
       ==============================================================================================================
       ==============================================================================================================*/

      if (rerFieldValidatorTitreOriginal.IsValid && RegularExpressionDuree.IsValid &&
      choixProducteur.ControleCustomValidator.IsValid && choixRealisateur.ControleCustomValidator.IsValid &&
      choixActeur1.ControleCustomValidator.IsValid && choixActeur2.ControleCustomValidator.IsValid &&
      choixActeur3.ControleCustomValidator.IsValid)
      {
         //on ajoute
         //tbTitreFrancais.Text = "Aucun validator actif";
      }
      else
      {
         //on fait what ever qu'on veut, cas de validation active.
      }
   }

   protected void chargeListeAnneeSortie()
   {
      int annee = DateTime.Now.Year;

      ddlAnnee.Items.Add(new ListItem("-- Aucune --", "0"));
      for (int i = 1900; i <= annee; i++)
      {
         ddlAnnee.Items.Add(i.ToString());
      }
   }
   protected void chargeListeNbCD()
   {
      ddlNbDisques.Items.Add(new ListItem("-- Aucun --", "0"));
      for (int i = 1; i <= 10; i++)
      {
         ddlNbDisques.Items.Add(i.ToString());
      }

   }

   protected void maValidation(Object sender, ServerValidateEventArgs Arguments)
   {
      SQL.Connection();
      if (SQL.checkIfNomFilmExiste(tbTitreFrancais.Text))
      {
         Arguments.IsValid = false;
      }
      else
      {
         Arguments.IsValid = true;
      }
   }

   protected void maValidationTitreOriginal(Object sender, ServerValidateEventArgs Arguments)
   {
      SQL.Connection();
      if (SQL.checkIfNomOriginalFilmExiste(tbTitreOriginal.Text))
      {
         Arguments.IsValid = false;
      }
      else
      {
         Arguments.IsValid = true;
      }
   }
</script>
   <asp:Label ID="Titre" runat="server" style="color: #7c795d; font-family: 'Source Sans Pro', sans-serif; font-size: 28px; font-weight: 400; line-height: 32px; margin: 0 0 24px;"/>
<hr />

<div class="row">
    <div class="col-sm-6">
        <!-- Titre français -->
        <asp:Label runat="server">Titre francais :</asp:Label>
        <asp:TextBox ID="tbTitreFrancais" runat="server"
           MaxLength="25" CssClass="form-control"
            placeholder="Titre francais"/>
        <asp:RequiredFieldValidator runat="server" 
             id="rerFieldValidatorTitreOriginal"  
             Style="color:red" 
             controltovalidate="tbTitreFrancais" 
             errormessage="Entrez un tite!" />
        <asp:CustomValidator ID="cv1" runat="server"
        ControlToValidate="tbTitreFrancais"
        OnServerValidate="maValidation"
        EnableClientScript="false"
        ValidateEmptyText="true"
        Style="color:red" 
        Display="dynamic"
        ErrorMessage="Ce titre est déjà inscrit" />
        <br />
         <!-- Année de sortie Fait -->
        <asp:Label runat="server">Année de sortie :</asp:Label>
        <asp:DropDownList ID="ddlAnnee" runat="server"
           MaxLength="25" CssClass="form-control"
           placeholder="Année de sortie"/>
        <br />
        <!-- Categorie Requete-->
        <asp:Label runat="server">Catégorie :</asp:Label>
        <asp:DropDownList ID="ddlCategorie" runat="server"
           MaxLength="25" CssClass="form-control"
            placeholder="Nom du producteur"/>
        <br />
        <!-- Durée Fait-->
        <asp:Label runat="server">Durée :</asp:Label>
        <asp:TextBox ID="tbDuree" runat="server"
           MaxLength="25" CssClass="form-control"
            placeholder="Durée (en minutes)"/>
        <asp:RegularExpressionValidator ID="RegularExpressionDuree"
            ControlToValidate="tbDuree" runat="server"
            ErrorMessage="Nombres entiers seulement!"
            Style="color:red"
            ValidationExpression="\d+">
        </asp:RegularExpressionValidator>
        <br />
        <!-- Format Requete-->
        <asp:Label runat="server">Format :</asp:Label>
        <asp:DropDownList ID="ddlFormat" runat="server"
             CssClass="form-control"/>
        <br />
        <!-- Nombre de disques Fait-->
        <asp:Label runat="server">Nombres de disques :</asp:Label>
        <asp:DropDownList ID="ddlNbDisques" runat="server"
             CssClass="form-control"/>
        <br />

        <!-- Langue Requete -->
        <asp:Label runat="server">Langues :</asp:Label>
          <div class="panel panel-default">
             <button class="form-control" type="button" 
                 data-toggle="collapse" data-target="#collapseLangue" 
                 aria-expanded="false" aria-controls="collapseLangue" 
                 style="text-align:left" onclick="changeIcon('langues')">
                 <span id="langues" class="glyphicon glyphicon-chevron-down"></span>
                Afficher les langues disponnibles
              </button>
            <div id="collapseLangue" class="panel-collapse collapse out">
             
               <div>
                <asp:ListBox ID="lbLangue" runat="server" SelectionMode="Multiple"
                     CssClass="form-control" Width="100%" Height="100%" style="overflow:hidden; background-color:rgb(239, 239, 239)"/>
               </div>
            </div>
          </div>

        <!-- Sous-titre Requete-->
         <asp:Label runat="server">Sous-Titres :</asp:Label>
          <div class="panel panel-default">
             <button class="form-control" type="button" 
                 data-toggle="collapse" data-target="#collapseST" 
                 aria-expanded="false" aria-controls="collapseST" 
                 style="text-align:left" onclick="changeIcon('soustitre')">
                 <span id="soustitre" class="glyphicon glyphicon-chevron-down"></span>
                Afficher les sous-titres disponnibles
              </button>
            <div id="collapseST" class="panel-collapse collapse out">
             
               <div>
                <asp:ListBox ID="lbSousTitre" runat="server" SelectionMode="Multiple"
                     CssClass="form-control" Width="100%" Height="100%" style="overflow:hidden; background-color:rgb(239, 239, 239)"/>
               </div>
            </div>
          </div>
        
        <!-- Image incertain-->
        <asp:Label runat="server">Image de la pochette :</asp:Label>
         <div class="input-group">
          <span class="input-group-addon">   
              <i class="glyphicon glyphicon glyphicon-file"></i>
          </span>
           <input type="file" class="form-control">
        </div> 
    </div>
    <!-- ------------------------------------------------------------------------------------------------------------------------------------- -->
    <div class="col-sm-6">
        <!-- Titre original -->
        <asp:Label runat="server">Titre original :</asp:Label>
        <asp:TextBox ID="tbTitreOriginal" runat="server"
           MaxLength="25" CssClass="form-control"
            placeholder="Titre originale"/>
                <asp:CustomValidator ID="CustomValidator1" runat="server"
        ControlToValidate="tbTitreFrancais"
        OnServerValidate="maValidationTitreOriginal"
        EnableClientScript="false"
        ValidateEmptyText="true"
        Style="color:red" 
        ErrorMessage="Ce titre est déjà inscrit" />
        <br />

        <!-- Suppléments Requete -->
        <asp:Label runat="server">Suppléments :</asp:Label>
          <div class="panel panel-default">
             <button class="form-control" type="button" 
                 data-toggle="collapse" data-target="#collapseExample" 
                 aria-expanded="false" aria-controls="collapseExample" 
                 style="text-align:left" onclick="changeIcon('supplement');">
                 <span id="supplement" class="glyphicon glyphicon-chevron-down"></span>
                Afficher les suppléments disponnibles 
              </button>
            <div id="collapseExample" class="panel-collapse collapse out">
             
               <div>
                <asp:ListBox ID="lbSupplements" runat="server" SelectionMode="Multiple"
                     CssClass="form-control" Width="100%" Height="100%" style="overflow:hidden; background-color:rgb(239, 239, 239)"/>
               </div>
            </div>
          </div>

        <!-- Producteur Requete-->
        <asp:Label runat="server">Producteur :</asp:Label>
        <pers:Personne ID="choixProducteur" runat="server"/>
        <br />
        <!-- Réalisateur Requete-->
        <asp:Label runat="server">Réalisateur :</asp:Label>
        <pers:Personne ID="choixRealisateur" runat="server"/>
        <br />
        <!-- Les 3 check box -->
        <asp:Label runat="server">Options :</asp:Label>
        <div class="form-control">
            <div class="col-sm-4">
                <!-- Version originale? -->
                <asp:Label runat="server">Version originale :</asp:Label>
                <asp:CheckBox ID="cbOriginal" runat="server"/>
            </div>
            <div class="col-sm-4">
                <!-- Version étendue?-->
                <asp:Label runat="server">Version étendue :</asp:Label>
                <asp:CheckBox ID="cbEtendue" runat="server"/>
            </div>
            <div class="col-sm-4">
                <!-- Visible à tous? -->
                <asp:Label runat="server">Visible à tous :</asp:Label>
                <asp:CheckBox ID="cbVisible" runat="server"/>
            </div>
        </div>
        <br />

        <!-- Acteur 1 Requete (control donnant choix)-->
        <asp:Label runat="server">Acteur 1 :</asp:Label>
        <pers:Personne ID="choixActeur1" runat="server"/>
        <br />
        <!-- Acteur 2  Requete (control donnant choix)-->
        <asp:Label runat="server">Acteur 2 :</asp:Label>
        <pers:Personne ID="choixActeur2" runat="server"/>
        <br />
        <!-- Acteur 3 Requete (control donnant choix) -->
        <asp:Label runat="server">Acteur 3 :</asp:Label>
        <pers:Personne ID="choixActeur3" runat="server"/>
        <br />

        <!-- Resume film -->
        <asp:Label runat="server">Résumé :</asp:Label>
          <div class="panel panel-default">
             <button class="form-control" type="button" 
                 data-toggle="collapse" data-target="#collapseResume" 
                 aria-expanded="false" aria-controls="collapseResume" 
                 style="text-align:left" onclick="changeIcon('resume')">
                 <span id="resume" class="glyphicon glyphicon-chevron-down"></span>
                Afficher la boîte de saisie du résumé
              </button>
            <div id="collapseResume" class="panel-collapse collapse out">             
               <div>
                 <asp:TextBox ID="tbResume" runat="server"
                    TextMode="MultiLine"
                     style="max-width:100%; min-width:100%; min-height:180px;max-height:180px;"
                     CssClass="form-control"/>
               </div>
            </div>
          </div>
    </div>
</div>
<!-- TODO : ajouter d'autres champs, modifier textbox pour des dropdown list  -->
<div class="row">
    <div class="col-sm-6">
        <asp:Button runat="server" class="btn btn-lg btn-primary btn-block" Text="Modifier" OnClick="Modifier"/>
    </div>
    <div class="col-sm-6">
        <asp:Button runat="server" class="btn btn-lg btn-danger btn-block" Text="Annuler" OnClick="Retour"/>
    </div>
</div>
