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
    static int i = 0;



    protected void btnEnregistrer_Click(object sender, EventArgs e)
    {
        string utilisateur = HttpContext.Current.User.Identity.Name;
        List<string> lstNomFilm = new List<string>();

        for(int i = 1;i <= 10; i++)
        {
            string nomTextBox = "film" + i;
            TextBox tbFilm = this.FindControl(nomTextBox) as TextBox;

            if (tbFilm != null && !string.IsNullOrEmpty(tbFilm.Text))
            {
                lstNomFilm.Add(tbFilm.Text);
            }
        }

        SQL.Connection();
        SQL.AddMovieShort(lstNomFilm, utilisateur);
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
    if (i <= 9)
        $("#contentBody_tbNomFilm_film" + i++).show();
    else if (i === 10) {
        $("#contentBody_tbNomFilm_film" + i++).show();

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
                  <div id="hasError1" class="form-group has-feedback">
                    <asp:TextBox ID="film1" runat="server" 
                       MaxLength="25" oninput="valider();"
                        placeholder="Doit être présent"/><span id="error1" class="error" style="display:none;">Doit être présent</span>
                  </div>
                  <div id="hasError2" class="form-group has-feedback">
                    <asp:TextBox ID="film2" runat="server"
                        MaxLength="25" style="display:none;" oninput="valider();"
                        placeholder="Doit être présent"/><span id="error2" class="error" style="display:none;">Doit être présent</span>
                    </div>
                  <div id="hasError3" class="form-group has-feedback">
                      <asp:TextBox ID="film3" runat="server"
                        MaxLength="25" style="display:none;" oninput="valider();"
                        placeholder="Doit être présent"/><span id="error3" class="error" style="display:none;">Doit être présent</span>
                    </div>
                  <div id="hasError4" class="form-group has-feedback">
                      <asp:TextBox ID="film4" runat="server"
                        MaxLength="25" style="display:none;" oninput="valider();"
                        placeholder="Doit être présent"/><span id="error4" class="error" style="display:none;">Doit être présent</span>
                    </div>
                  <div id="hasError5" class="form-group has-feedback">
                      <asp:TextBox ID="film5" runat="server"
                        MaxLength="25" style="display:none;" oninput="valider();"
                        placeholder="Doit être présent"/><span id="error5" class="error" style="display:none;">Doit être présent</span>
                    </div>
                  <div id="hasError6" class="form-group has-feedback">
                      <asp:TextBox ID="film6" runat="server"
                        MaxLength="25" style="display:none;" oninput="valider();"
                        placeholder="Doit être présent"/><span id="error6" class="error" style="display:none;">Doit être présent</span>
                    </div>
                  <div id="hasError7" class="form-group has-feedback">
                      <asp:TextBox ID="film7" runat="server"
                        MaxLength="25" style="display:none;" oninput="valider();"
                        placeholder="Doit être présent"/><span id="error7" class="error" style="display:none;">Doit être présent</span>
                    </div>
                  <div id="hasError8" class="form-group has-feedback">
                      <asp:TextBox ID="film8" runat="server"
                        MaxLength="25" style="display:none;" oninput="valider();"
                        placeholder="Doit être présent"/><span id="error8" class="error" style="display:none;">Doit être présent</span>
                    </div>
                  <div id="hasError9" class="form-group has-feedback">
                      <asp:TextBox ID="film9" runat="server"
                        MaxLength="25" style="display:none;" oninput="valider();"
                        placeholder="Doit être présent"/><span id="error9" class="error" style="display:none;">Doit être présent</span>
                    </div>
                  <div id="hasError10" class="form-group has-feedback">
                      <asp:TextBox ID="film10" runat="server"
                        MaxLength="25" style="display:none;" oninput="valider();"
                        placeholder="Doit être présent"/><span id="error10" class="error" style="display:none;">Doit être présent</span>
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