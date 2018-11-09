<%@ Control Language="C#" %>
<script runat="server">
    public String Text                       { set { film1.Text = value; }
        get { return film1.Text; } }
    public TextBox Controle                  { get { return film1; } }
    public RequiredFieldValidator Present    { get { return ValidatorPresent; } }
    public RegularExpressionValidator Format { get { return ValidatorFormat; } }
    public string CssClass { set { film1.CssClass = value; } }
    public string placeholder { set { film1.Attributes.Add("placeholder", value); } }
    static int i = 0;

   

</script>

<asp:TextBox ID="film1" runat="server"
   MaxLength="25"
    placeholder="Doit être présent"/>

<div ID="moreTextboxs"></div>
    
<button  onclick = "CreateTxt();return false;" class="btn btn-light">
    <span class="glyphicon glyphicon-plus"></span>
</button>
<asp:RequiredFieldValidator ID="ValidatorPresent" runat="server"
   ControlToValidate="film1"
   EnableClientScript="false"
   Display="None" />

<asp:RegularExpressionValidator ID="ValidatorFormat" runat="server"
   ControlToValidate="film1"
   ValidationExpression="^[a-zA-ZÀ-ÖØ-öø-ÿ]*([,'\- ]{1}[a-zA-ZÀ-ÖØ-öø-ÿ]+)*[a-zA-ZÀ-ÖØ-öø-ÿ]*$"
   EnableClientScript="false"                                                  
   Display="None" />