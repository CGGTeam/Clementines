<%@ Control Language="C#" %>
<script runat="server">
        public String Text                       { set { tb.Text = value; }
            get { return tb.Text; } }
        public TextBox Controle                  { get { return tb; } }
        public RequiredFieldValidator Present    { get { return ValidatorPresent; } }
        public RegularExpressionValidator Format { get { return ValidatorFormat; } }
        public string CssClass { set { tb.CssClass = value; } }
        public string placeholder { set { tb.Attributes.Add("placeholder", value); } }

        protected void ajouterChamp(Object sender, EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("in");

            TextBox tb = new TextBox
            {
                MaxLength = 25,
                ID = "2",
                CssClass = "form-control"
            };
            moreTb.Controls.Add(tb);

            
        }

</script>

<asp:TextBox ID="tb" runat="server"
   MaxLength="25"
    placeholder="Doit être présent"/>

<asp:Panel runat="server" ID="moreTb">

</asp:Panel>

<asp:LinkButton  ID="lus" runat="server"
    onclick="ajouterChamp" AutoPostBack=false >
    <span class="glyphicon glyphicon-plus"></span>
</asp:LinkButton>


<asp:RequiredFieldValidator ID="ValidatorPresent" runat="server"
   ControlToValidate="tb"
   EnableClientScript="false"
   Display="None" />

<asp:RegularExpressionValidator ID="ValidatorFormat" runat="server"
   ControlToValidate="tb"
   ValidationExpression="^[a-zA-ZÀ-ÖØ-öø-ÿ]*([,'\- ]{1}[a-zA-ZÀ-ÖØ-öø-ÿ]+)*[a-zA-ZÀ-ÖØ-öø-ÿ]*$"
   EnableClientScript="false"
   Display="None" />