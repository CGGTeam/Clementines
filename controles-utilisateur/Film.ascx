<%@ Control Language="C#" %>
<%@ Register tagprefix="pers" TagName="Personne" Src="Personne_Ddl_Ajout.ascx" %>
<%@ Import Namespace="System.IO" %>
<script runat="server">


    static string prevPage = String.Empty;
    EntiteUtilisateur utilCourant;

    protected void Page_Load(object sender, EventArgs e)
    {
        //garder le fileInput apres un postback
        if (Session["FileUpload"] == null && btnUploadImagePochette.HasFile)
        {
            Session["FileUpload"] = btnUploadImagePochette;
        }
        else if (Session["FileUpload"] != null && (! btnUploadImagePochette.HasFile))
        {
            btnUploadImagePochette = (FileUpload) Session["FileUpload"];
        }
        else if (btnUploadImagePochette.HasFile)
        {
            Session["FileUpload"] = btnUploadImagePochette;
        }

           
        utilCourant = SQL.FindUtilisateurByName(HttpContext.Current.User.Identity.Name);
        if (!IsPostBack)
        {  
            if (utilCourant.TypeUtilisateur == 'S')
            {
                div_identite.Visible = true;
                populerDDLIdentite();
            }


            if (Request.UrlReferrer != null)
            {
                prevPage = Request.UrlReferrer.ToString();
            }
            foreach (Control c in this.Controls)
            {
                if (c is Personne_Ddl_Ajout)
                    chargeListeRequete((Personne_Ddl_Ajout) c);
            }
            chargeListeSupplements();
            chargeListeSousTitres();
            chargeListeLangues();
            chargeListeNbCD();
            chargeListeAnneeSortie();
            chargeListeFormats();
            chargeListeCategories();
        }
    }


    protected void chargeListeRequete(Personne_Ddl_Ajout control)
    {
        //SQL.Connection();
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

    private void populerDDLIdentite()
    {
        ddlIdentite.Items.Clear();
        List<EntiteUtilisateur> lstUtilisateurs = SQL.FindAllUtilisateurSaufCourantEtEmprunteur(utilCourant.NoUtilisateur, utilCourant.NoUtilisateur);
        ddlIdentite.Items.Add(new ListItem("Moi", utilCourant.NoUtilisateur.ToString()));
        foreach (EntiteUtilisateur utilisateur in lstUtilisateurs)
        {
            ddlIdentite.Items.Add(new ListItem(utilisateur.NomUtilisateur, utilisateur.NoUtilisateur.ToString()));
        }
    }

    protected void chargeListeSupplements()
    {
        //SQL.Connection();
        List<EntiteSupplements> lstSupplements = SQL.FindAllSupplement();
        lbSupplements.Items.Add(new ListItem("-- Aucun --", "0"));
        foreach (EntiteSupplements supplement in lstSupplements)
        {
            lbSupplements.Items.Add(new ListItem(supplement.Description, supplement.NoSupplement.ToString()));
        }
        lbSupplements.Rows = lbSupplements.Items.Count;
        lbSupplements.Items.FindByValue("0").Selected = true;
    }

    protected void chargeListeSousTitres()
    {
        // SQL.Connection();
        List<EntiteSousTitres> lstSousTitres = SQL.FindAllSousTitre();
        lbSousTitre.Items.Add(new ListItem("-- Aucun --", "0"));
        foreach (EntiteSousTitres sousTitres in lstSousTitres)
        {
            lbSousTitre.Items.Add(new ListItem(sousTitres.LangueSousTitre, sousTitres.NoSousTitre.ToString()));
        }
        lbSousTitre.Rows = lbSousTitre.Items.Count;
        lbSousTitre.Items.FindByValue("0").Selected = true;
    }

    protected void chargeListeLangues()
    {
        //SQL.Connection();
        List<EntiteLangue> lstLangues = SQL.FindAllLangue();
        lbLangue.Items.Add(new ListItem("-- Aucune --", "0"));
        foreach (EntiteLangue langue in lstLangues)
        {
            lbLangue.Items.Add(new ListItem(langue.Langue, langue.NoLangue.ToString()));
        }
        lbLangue.Rows = lbLangue.Items.Count;
        lbLangue.Items.FindByValue("0").Selected = true;
    }

    protected void chargeListeFormats()
    {
        //SQL.Connection();
        List<EntiteFormat> lstFormat = SQL.FindAllFormat();
        ddlFormat.Items.Add(new ListItem("-- Aucun --", "0"));
        foreach (EntiteFormat format in lstFormat)
        {
            ddlFormat.Items.Add(new ListItem(format.Description, format.NoFormat.ToString()));
        }
    }

    protected void chargeListeCategories()
    {
        //SQL.Connection();
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
    protected void Ajouter(object sender, EventArgs e)
    {
        /*=============================================================================================================
         ==============================================================================================================
         ==============================================================================================================
         ==============================================================================================================
         ==============================================================================================================
         ==============================================================================================================*/
        if(btnUploadImagePochette.PostedFile.ContentLength >= 1048576)
        {
            error.Visible = true;
            lblError.Text = "L'image doit être plus petite que 1mb";
            return;
        }


        if (rerFieldValidatorTitreOriginal.IsValid && rangeValDuree.IsValid &&
        choixProducteur.ControleCustomValidator.IsValid && choixRealisateur.ControleCustomValidator.IsValid &&
        choixActeur1.ControleCustomValidator.IsValid && choixActeur2.ControleCustomValidator.IsValid &&
        choixActeur3.ControleCustomValidator.IsValid && cv1.IsValid && CV2.IsValid && validationActeur())
        {
            string realisateur = "";
            string producteur = "";
            int noFilm = SQL.FindNextNoFilm();

            //faire l'ajout de nouveaux producteur et réalisateur s'il en est le cas.
            if (choixRealisateur.ControleTextBox.Visible)
            {
                string nomRealisateurNOUVEAU = choixRealisateur.ControleTextBox.Text;
                int ID = SQL.trouverDernierIDRealisateur();
                ID++;
                SQL.ajouteRealisateur(ID, nomRealisateurNOUVEAU);
                realisateur = ID.ToString();
            }

            if (choixProducteur.ControleTextBox.Visible)
            {
                string nomProducteurNOUVEAU = choixProducteur.ControleTextBox.Text;
                int ID = SQL.trouverDernierIDProducteur();
                ID++;
                SQL.ajouteProducteur(ID, nomProducteurNOUVEAU);
                producteur = ID.ToString();
            }

            /*int noUtilisateurCourrant;
            string utilisateur = HttpContext.Current.User.Identity.Name;
            noUtilisateurCourrant = SQL.FindNoUtilisateurByName(utilisateur);*/

            //Récupérer toutes mes informations dans mes variables. (pour le table Film)
            int anneSortie = corrigerLesDDl(ddlAnnee.SelectedItem.ToString());
            string categorie = ddlCategorie.SelectedValue.ToString();
            string format = ddlFormat.SelectedValue.ToString();
            DateTime date = DateTime.Now;
            string noUtilisateur;
            if (utilCourant.TypeUtilisateur == 'S')
            {
                noUtilisateur = ddlIdentite.SelectedValue.ToString();
            }
            else
            {
                noUtilisateur= utilCourant.NoUtilisateur.ToString();
            }
            string resume = tbResume.Text.Trim();
            int duree = corrigerLesDDl(tbDuree.Text.ToString().Trim());
            bool filmOriginal = cbOriginal.Checked;
            string imagePochette = "";

            //récupération et téléchargement de l'image dans nos ressources
            if(btnUploadImagePochette.HasFile)
            {

                try
                {

                    string filename = Path.GetFileName(btnUploadImagePochette.FileName);
                    btnUploadImagePochette.SaveAs(Server.MapPath("~/static/images/") + filename);
                    imagePochette = filename;
                }
                catch(Exception ex)
                {

                    //mettre une erreur?
                }
            }

            int nbDisques = int.Parse(ddlNbDisques.SelectedValue);
            string titreFrancais = tbTitreFrancais.Text;
            string titreOriginal = tbTitreOriginal.Text.Trim();
            bool versionEtendue = cbEtendue.Checked;

            //gestions des réalisateurs et producteurs dans le cas du ddl visible
            if (!choixRealisateur.ControleTextBox.Visible)
            {
                realisateur = choixRealisateur.ControleDDL.SelectedValue.ToString();
            }


            if (!choixProducteur.ControleTextBox.Visible)
            {
                producteur = choixProducteur.ControleDDL.SelectedValue.ToString();
            }

            string extras = tbExtras.Text.Trim();
            EntiteFilm entite = new EntiteFilm(noFilm, anneSortie, categorie, format, date, noUtilisateur, resume, duree, filmOriginal, imagePochette, nbDisques, titreFrancais, titreOriginal, versionEtendue, realisateur, producteur, extras);
            SQL.ajoutFilmComplet(entite);

            //ajout dans Films Acteur (si le cas)


            //ajout dans films supplements 
            foreach (ListItem item in lbSupplements.Items)
            {
                if (item.Selected)
                {
                    if (item.Value != "0")
                    {
                        SQL.ajouterFilmSupplement(noFilm, int.Parse(item.Value.ToString()));
                    }
                }
            }
            //Ajout des langues
            foreach (ListItem item in lbLangue.Items)
            {
                if (item.Selected)
                {
                    if (item.Value != "0")
                    {
                        SQL.ajouterFilmLangue(noFilm, int.Parse(item.Value.ToString()));
                    }
                }
            }

            //Ajout des sous titre
            foreach (ListItem item in lbSousTitre.Items)
            {
                if (item.Selected)
                {
                    if (item.Value != "0")
                    {
                        SQL.ajouterFilmSousTitre(noFilm, int.Parse(item.Value.ToString()));
                    }
                }
            }


            //ajout d'un nouvel acteur 1 
            if (choixActeur1.ControleTextBox.Visible)
            {
                string nomActeur = choixActeur1.ControleTextBox.Text;
                int ID = SQL.trouverDernierIDActeur();
                ID++;
                SQL.ajouteActeur(ID, nomActeur);

                //Ajouter dans la table filmacteur
                SQL.ajouterFilmActeur(noFilm, ID);

            }
            else
            {
                //si n'est pas "aucun" qui est sélectionner on fait le nouveau lien filmActeur
                if (choixActeur1.ControleDDL.SelectedValue != "0")
                {
                    SQL.ajouterFilmActeur(noFilm, int.Parse(choixActeur1.ControleDDL.SelectedValue));
                }
            }
            //ajout d'un nouvel acteur 2 
            if (choixActeur2.ControleTextBox.Visible)
            {
                string nomActeur = choixActeur2.ControleTextBox.Text;
                int ID = SQL.trouverDernierIDActeur();
                ID++;
                SQL.ajouteActeur(ID, nomActeur);
                SQL.ajouterFilmActeur(noFilm, ID);
            }
            else
            {
                //si n'est pas "aucun" qui est sélectionner on fait le nouveau lien filmActeur
                if (choixActeur2.ControleDDL.SelectedValue != "0")
                {
                    SQL.ajouterFilmActeur(noFilm, int.Parse(choixActeur2.ControleDDL.SelectedValue));
                }
            }
            //ajout d'un nouvel acteur 3 
            if (choixActeur3.ControleTextBox.Visible)
            {
                string nomActeur = choixActeur3.ControleTextBox.Text;
                int ID = SQL.trouverDernierIDActeur();
                ID++;
                SQL.ajouteActeur(ID, nomActeur);
                SQL.ajouterFilmActeur(noFilm, ID);
            }
            else
            {
                //si n'est pas "aucun" qui est sélectionner on fait le nouveau lien filmActeur
                if (choixActeur3.ControleDDL.SelectedValue != "0")
                {
                    SQL.ajouterFilmActeur(noFilm, int.Parse(choixActeur3.ControleDDL.SelectedValue));
                }
            }


            //vider les tb
            foreach (Control controle in this.Controls)
            {
                if (controle is TextBox)
                {
                    TextBox tb = (TextBox)controle;
                    tb.Text = "";
                }

                if (controle is DropDownList)
                {
                    DropDownList ddl = (DropDownList)controle;
                    ddl.Items.Clear();
                }

                if (controle is CheckBox)
                {
                    CheckBox cb = (CheckBox)controle;
                    cb.Checked = false;
                }

                if (controle is ListBox)
                {
                    ListBox lb = (ListBox)controle;
                    lb.Items.Clear();
                }

                if (controle is Personne_Ddl_Ajout)
                {
                    Personne_Ddl_Ajout personne = (Personne_Ddl_Ajout) controle;
                    personne.ControleDDL.Items.Clear();
                    personne.ControleTextBox.Text = "";
                    personne.ControleDDL.Visible = true;
                    personne.ControleTextBox.Visible = false;
                    personne.CssClass = "glyphicon glyphicon-option-vertical";
                    chargeListeRequete(personne);
                }
            }
            succes.Visible = true;
            lblSucces.Text = "Le films a été ajouté avec succès!";

            //charger les listes


            chargeListeSupplements();
            chargeListeSousTitres();
            chargeListeLangues();
            chargeListeNbCD();
            chargeListeAnneeSortie();
            chargeListeFormats();
            chargeListeCategories();

        }
    }

    public int corrigerLesDDl(string aValider)
    {
        int retour = -1;

        if (!int.TryParse(aValider, out retour))
        {
            retour = -1;
        }

        return retour;
    }

    public int? ToNullableInt(string s)
    {
        int i;
        if (int.TryParse(s, out i)) return i;
        return null;
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
        for (int i = 1; i <= 99; i++)
        {
            ddlNbDisques.Items.Add(new ListItem(i.ToString(), i.ToString()));
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

    protected bool validationActeur()
    {
        bool retour = true;
        if (choixActeur1.ControleDDL.SelectedValue == choixActeur2.ControleDDL.SelectedValue && choixActeur1.ControleDDL.SelectedValue != "0" && choixActeur2.ControleDDL.SelectedValue != "0" && choixActeur1.ControleDDL.Visible && choixActeur2.ControleDDL.Visible ||
            choixActeur1.ControleDDL.SelectedValue == choixActeur3.ControleDDL.SelectedValue && choixActeur1.ControleDDL.SelectedValue != "0" && choixActeur3.ControleDDL.SelectedValue != "0" && choixActeur1.ControleDDL.Visible && choixActeur3.ControleDDL.Visible||
            choixActeur2.ControleDDL.SelectedValue == choixActeur3.ControleDDL.SelectedValue && choixActeur2.ControleDDL.SelectedValue != "0" && choixActeur3.ControleDDL.SelectedValue != "0" && choixActeur2.ControleDDL.Visible && choixActeur3.ControleDDL.Visible)
        {
            error.Visible = true;
            succes.Visible = false;
            lblError.Text = "Vous ne pouvez pas avoir plusieurs fois le même acteur";
            retour = false;
        }
        else
        {
            error.Visible = false;
        }
        return retour;
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
<!-- changer d'identité-->
<div class="row" runat="server" id="div_identite" Visible="false">
        <div class="col-sm-4">
            <div class="input-group">
              <span class="input-group-addon">Changer d'identité </span>
              <span class="input-group-addon"><i class="glyphicon glyphicon-user"></i></span>
              <asp:DropDownList ID="ddlIdentite" runat="server"
                  CssClass="form-control"/>
        </div>
        </div>
    </div>
<!-- les deux trucs de validations -->
        <div runat="server" Visible="false" id="succes" class="alert alert-success" role="alert">
            <asp:Label runat="server" ID="lblSucces"></asp:Label>
            <asp:LinkButton runat="server" class="btn-link pull-right" OnClick="fermerSucces" CausesValidation="false">
                <span class="glyphicon glyphicon-remove pull-right"></span>
            </asp:LinkButton>
        </div>

        <div runat="server" Visible="false" id="error" class="alert alert-danger" role="alert">
            <asp:Label runat="server" ID="lblError"></asp:Label>
            <asp:LinkButton runat="server" type="button" class="btn-link pull-right"  OnClick="fermerError" CausesValidation="false">
                <span class="glyphicon glyphicon-remove"></span>
            </asp:LinkButton>
        </div>

<div class="row">
    <div class="col-sm-6">
        <!-- Titre français -->
        <asp:Label runat="server">Titre francais :</asp:Label>
        <asp:TextBox ID="tbTitreFrancais" runat="server"
           MaxLength="250" CssClass="form-control"
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
           MaxLength="250" CssClass="form-control"
           placeholder="Année de sortie"/>
        <br />
        <!-- Categorie Requete-->
        <asp:Label runat="server">Catégorie :</asp:Label>
        <asp:DropDownList ID="ddlCategorie" runat="server"
           MaxLength="250" CssClass="form-control"
            placeholder="Nom du producteur"/>
        <br />
        <!-- Durée Fait-->
        <asp:Label runat="server">Durée :</asp:Label>
        <asp:TextBox ID="tbDuree" runat="server"
           MaxLength="250" CssClass="form-control"
            placeholder="Durée (en minutes)"/>
        <asp:RangeValidator runat="server"
            ControlToValidate="tbDuree"
            Type="Integer"
            MinimumValue="0"
            MaximumValue="9999"
            ID="rangeValDuree"
            Style="color:red"
            ErrorMessage="nombre entre 0 et 9999" />
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
        
        <!-- Ajouter des extras -->
          <asp:Label runat="server">Liens des extras :</asp:Label>
        <asp:TextBox ID="tbExtras" runat="server"
           MaxLength="250" CssClass="form-control"
            placeholder="URL..."/>
        <br />
    </div>
    <!-- ------------------------------------------------------------------------------------------------------------------------------------- -->
    <div class="col-sm-6">
        <!-- Titre original -->
        <asp:Label runat="server">Titre original :</asp:Label>
        <asp:TextBox ID="tbTitreOriginal" runat="server"
           MaxLength="250" CssClass="form-control"
            placeholder="Titre originale"/>
                <asp:CustomValidator ID="CV2" runat="server"
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
            <div class="col-sm-6">
                <!-- Version originale? -->
                <asp:Label runat="server">Version originale :</asp:Label>
                <asp:CheckBox ID="cbOriginal" runat="server"/>
            </div>
            <div class="col-sm-6">
                <!-- Version étendue?-->
                <asp:Label runat="server">Version étendue :</asp:Label>
                <asp:CheckBox ID="cbEtendue" runat="server"/>
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
<hr />
    <div class="row" style="align-content:center;">
        <div class="col-sm-6" style="float: none; margin: 0 auto;">
                    <!-- Image -->
            <asp:Label runat="server">Image de la pochette :</asp:Label>
             <div class="input-group">
              <span class="input-group-addon">   
                  <i class="glyphicon glyphicon glyphicon-file"></i>
              </span>
                 <asp:FileUpload  id="btnUploadImagePochette" runat="server" CssClass="form-control"/>
            </div>
            <br />
        </div>
    </div>
<!-- TODO : ajouter d'autres champs, modifier textbox pour des dropdown list  -->
<div class="row">
    <div class="col-sm-6">
        <asp:Button runat="server" class="btn btn-lg btn-primary btn-block" Text="Ajouter" OnClick="Ajouter"/>
    </div>
    <div class="col-sm-6">
        <asp:Button runat="server" class="btn btn-lg btn-danger btn-block" Text="Annuler" OnClick="Retour" CausesValidation="false"/>
    </div>
</div>
