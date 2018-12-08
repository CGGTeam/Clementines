<%@ Control Language="C#" %>
<script runat="server">
    public String Text                       { set { film1.Text = value; }
        get { return film1.Text; } }
    public TextBox Controle                  { get { return film1; } }
    public RequiredFieldValidator Present    { get { return ValidatorPresent; } }
    public RegularExpressionValidator Format { get { return ValidatorFormat; } }
    public string CssClass {
        set {
            for (int i = 1; i <= 10; i++)
            {
                string nomTextBox = "film" + i;
                TextBox tbFilm = this.FindControl(nomTextBox) as TextBox;
                tbFilm.CssClass = value;
            }
        } }
    public string placeholder {
        set {
            for (int i = 1; i <= 10; i++)
            {
                string nomTextBox = "film" + i;
                TextBox tbFilm = this.FindControl(nomTextBox) as TextBox;
                tbFilm.Attributes.Add("placeholder", value);
            }
        } }

    protected void btnEnregistrer_Click(object sender, EventArgs e)
    {
        string utilisateur = HttpContext.Current.User.Identity.Name;
        List<string> lstNomFilm = new List<string>();
        List<string> lstMauvais = new List<string>();

        for (int i = 1; i <= 10; i++)
        {
            string nomTextBox = "film" + i;
            TextBox tbFilm = this.FindControl(nomTextBox) as TextBox;

            if (tbFilm != null && !string.IsNullOrEmpty(tbFilm.Text))
            {
                string nom = tbFilm.Text;
                SQL.Connection();
                if (!SQL.checkIfNomFilmExiste(nom))
                    lstNomFilm.Add(nom);
                else
                    lstMauvais.Add(nom);
            }
        }

        SQL.Connection();
        SQL.AddMovieShort(lstNomFilm, utilisateur);

        string sep = ", ";

        if (lstNomFilm.Any())
        {
            succes.Visible = true;
            lblSucces.Text = "Les DVDs suivant ont été enregistré : " + String.Join(sep, lstNomFilm);
        }
        if (lstMauvais.Any())
        {
            error.Visible = true;
            lblError.Text = "Les DVDs suivant n'ont pu être enregistré car ils existent déjà : " + String.Join(sep, lstMauvais);
        }
    }
    protected void Page_Load(object sender, EventArgs e)
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
<script type="text/javascript">
    $( document ).ready(function() {
        $("#btnMoreTb").attr("disabled", true);
        $("#contentBody_tbNomFilm_btnEnregistrer").attr("disabled", true);
    });

    var i = 2;
    function CreateTxt() {
        $("#contentBody_tbNomFilm_btnEnregistrer").attr('value', 'Enregistrer les films');
        $("#div" + i++).show();
        if (i === 10) {
            $("#btnMoreTb").hide();
        }
        valider()
        return false;
    }
    function valider() {
        var estValide = true;
        for (var i = 1; i <= 10; i++) {
        
            if ($("#contentBody_tbNomFilm_film" + i).val() == "" && $("#contentBody_tbNomFilm_film" + i).is(":visible")) {
                estValide = false;
                $("hasError" + i).toggleClass("has-error");
            } else {
                $("hasError"+i).toggleClass("has-error");
            }
        }
        $("#btnMoreTb").attr("disabled", !estValide);
        $("#contentBody_tbNomFilm_btnEnregistrer").attr("disabled", !estValide);
        return false;
    }
</script>
<style>
.error{
	color: red;
	margin-left: 10px;
}
</style>
    <div class="row">
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

        <div class="col-sm-4">
          <div class="panel panel-default">
            <div class="panel-heading">
              <h4 class="panel-title">
                <a data-toggle="collapse" data-parent="#accordion" href="#collapse1">
                Enregistrer un nouveau film</a>
              </h4>
            </div>
            <div id="collapse1" class="panel-collapse collapse out">
              <div class="panel-body">
                  <label for="tbNomFilm" class="sr-only">Nom du film</label>
                  <!-- 1 -->
                  <div id="div1" class="input-group">
                      <span class="input-group-addon">   
                          1
                      </span>
                    <asp:TextBox ID="film1" runat="server" 
                       MaxLength="25" oninput="valider();"
                        placeholder="Doit être présent"/>
                  </div>
                  <!-- 2 -->
                  <div id="div2" class="input-group" style="display:none;">
                      <span class="input-group-addon">   
                          2
                      </span>
                    <asp:TextBox ID="film2" runat="server"
                        MaxLength="25" oninput="valider();"
                        placeholder="Doit être présent"/>
                    </div>
                  <!-- 3 -->
                  <div id="div3" class="input-group" style="display:none;">
                      <span class="input-group-addon">   
                          3
                      </span>
                      <asp:TextBox ID="film3" runat="server"
                        MaxLength="25" oninput="valider();"
                        placeholder="Doit être présent" />
                    </div>
                  <!-- 4 -->
                  <div id="div4" class="input-group" style="display:none;">
                      <span class="input-group-addon">   
                          4
                      </span>
                      <asp:TextBox ID="film4" runat="server"
                        MaxLength="25" oninput="valider();"
                        placeholder="Doit être présent"/>
                    </div>
                  <!-- 5 -->
                  <div id="div5" class="input-group" style="display:none;">
                      <span class="input-group-addon">   
                          5
                      </span>
                      <asp:TextBox ID="film5" runat="server"
                        MaxLength="25" oninput="valider();"
                        placeholder="Doit être présent"/>
                    </div>
                  <!-- 6 -->
                  <div id="div6" class="input-group" style="display:none;">
                      <span class="input-group-addon">   
                          6
                      </span>
                      <asp:TextBox ID="film6" runat="server"
                        MaxLength="25" oninput="valider();"
                        placeholder="Doit être présent"/>
                    </div>
                  <!-- 7 -->
                  <div id="div7" class="input-group" style="display:none;">
                      <span class="input-group-addon">   
                          7
                      </span>
                      <asp:TextBox ID="film7" runat="server"
                        MaxLength="25" oninput="valider();"
                        placeholder="Doit être présent" />
                    </div>
                  <!-- 8 -->
                  <div id="div8" class="input-group" style="display:none;">
                      <span class="input-group-addon">   
                          8
                      </span>
                      <asp:TextBox ID="film8" runat="server"
                        MaxLength="25" oninput="valider();"
                        placeholder="Doit être présent"/>
                    </div>
                  <!-- 9 -->
                  <div id="div9" class="input-group" style="display:none;">
                      <span class="input-group-addon">   
                          9
                      </span>
                      <asp:TextBox ID="film9" runat="server"
                        MaxLength="25" oninput="valider();"
                        placeholder="Doit être présent"/>
                    </div>
                  <!-- 10 -->
                  <div id="div10" class="input-group" style="display:none;">
                      <span class="input-group-addon">   
                          10
                      </span>
                      <asp:TextBox ID="film10" runat="server"
                        MaxLength="25" oninput="valider();"
                        placeholder="Doit être présent"/>
                  </div>
                      <br />
                        <button id="btnMoreTb"  onclick = "CreateTxt();return false;" class="btn btn-light">
                            <span class="glyphicon glyphicon-plus"></span>
                        </button>
                      <hr />
                    </div>
                    <asp:Button id="btnEnregistrer" runat="server" class="btn btn-lg btn-primary btn-block" 
                        Text="Enregistrer le film" OnClicK="btnEnregistrer_Click"/>
            </div>
          </div>
        </div>
    </div>

    
<asp:RequiredFieldValidator ID="ValidatorPresent" runat="server"
   ControlToValidate="film1"
   EnableClientScript="false"
   Display="None" />

<asp:RegularExpressionValidator ID="ValidatorFormat" runat="server"
   ControlToValidate="film1"
   ValidationExpression="^[a-zA-ZÀ-ÖØ-öø-ÿ]*([,'\- ]{1}[a-zA-ZÀ-ÖØ-öø-ÿ]+)*[a-zA-ZÀ-ÖØ-öø-ÿ]*$"
   EnableClientScript="false"                                                  
   Display="None" />