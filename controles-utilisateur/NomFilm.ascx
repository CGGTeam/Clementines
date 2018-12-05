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
                    <asp:TextBox ID="film1" runat="server"
                       MaxLength="25"
                        placeholder="Doit être présent"/>
                    <asp:TextBox ID="film2" runat="server"
                        MaxLength="25" style="display:none;"
                        placeholder="Doit être présent"/>
                    <asp:TextBox ID="film3" runat="server"
                        MaxLength="25" style="display:none;"
                        placeholder="Doit être présent"/>
                    <asp:TextBox ID="film4" runat="server"
                        MaxLength="25" style="display:none;"
                        placeholder="Doit être présent"/>
                    <asp:TextBox ID="film5" runat="server"
                        MaxLength="25" style="display:none;"
                        placeholder="Doit être présent"/>
                    <asp:TextBox ID="film6" runat="server"
                        MaxLength="25" style="display:none;"
                        placeholder="Doit être présent"/>
                    <asp:TextBox ID="film7" runat="server"
                        MaxLength="25" style="display:none;"
                        placeholder="Doit être présent"/>
                    <asp:TextBox ID="film8" runat="server"
                        MaxLength="25" style="display:none;"
                        placeholder="Doit être présent"/>
                    <asp:TextBox ID="film9" runat="server"
                        MaxLength="25" style="display:none;"
                        placeholder="Doit être présent"/>
                    <asp:TextBox ID="film10" runat="server"
                        MaxLength="25" style="display:none;"
                        placeholder="Doit être présent"/>

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