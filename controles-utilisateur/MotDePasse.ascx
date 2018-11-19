<%@ Control Language="C#" %>
<script runat="server">
    public String Text                       { set { tb.Text = value; }
        get { return tb.Text; } }
    public TextBox Controle                  { get { return tb; } }
    public RequiredFieldValidator Present    { get { return ValidatorPresent; } }
    public RegularExpressionValidator Format { get { return ValidatorFormat; } }
    public string CssClass { set { tb.CssClass = value; } }
    public string placeholder { set { tb.Attributes.Add("placeholder", value); } }

</script>

<asp:TextBox ID="tb" runat="server"
   MaxLength="25"
    placeholder="Doit être présent"
     TextMode="Password"
    />


<asp:RequiredFieldValidator ID="ValidatorPresent" runat="server"
   ControlToValidate="tb"
   EnableClientScript="false"
   Display="None" />

<asp:RegularExpressionValidator ID="ValidatorFormat" runat="server"
   ControlToValidate="tb"
   ValidationExpression="^\d+$"
   EnableClientScript="false"
   Display="None" />