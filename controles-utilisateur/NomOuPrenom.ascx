<%@ Control Language="C#" %>
<script runat="server">
    public String Text                       { set { tb.Text = value; }
        get { return tb.Text; } }
    public TextBox Controle                  { get { return tb; } }
    public RequiredFieldValidator Present    { get { return ValidatorPresent; } }
    public RegularExpressionValidator Format { get { return ValidatorFormat; } }
    public string CssClass { set { tb.CssClass = value; } }

    private void effacer(Object sender, EventArgs e)
    {
        tb.Text = " ";
    }
    private void restaurer(Object sender, EventArgs e)
    {
        tb.Text = hfAncien.Value;
    }
    public void setValue(String str)
    {
        tb.Text = str;
        hfAncien.Value = str;
        updateBtnRestaure();
    }
    private void updateBtnRestaure()
    {
        restaure.Visible = true;
    }
</script>

<style type="text/css">
   .sNomOuPrenom { width:250px; color:Blue; text-align:left; }
</style>

<asp:ImageButton ID="restaure" runat="server" ImageUrl="../images/restaure.png" width="20" Height="20" onclick="restaurer" Visible=false AutoPostBack=false/>

<asp:HiddenField ID="hfAncien" runat="server" value="" />

<asp:TextBox ID="tb" runat="server"
   MaxLength="25"
   CssClass="sNomOuPrenom"
    placeholder="Doit être présent"/>

<asp:ImageButton ID="aide" runat="server" ImageUrl="../images/aide.png" width="20" Height="20" OnClientClick="afficheAide('Format à respecter : \n\n 1 à 25 caractères. \n\n UNE ZONE S\'AFFICHANT EN GRISÉ INDIQUE \n UNE ERREUR DE SAISIE.'); return false;" AutoPostBack=false/>

<asp:ImageButton ID="efface" runat="server" ImageUrl="../images/efface.png" width="20" Height="20" onclick="effacer" AutoPostBack=false/>

<asp:RequiredFieldValidator ID="ValidatorPresent" runat="server"
   ControlToValidate="tb"
   EnableClientScript="false"
   Display="None" />

<asp:RegularExpressionValidator ID="ValidatorFormat" runat="server"
   ControlToValidate="tb"
   ValidationExpression="^[a-zA-ZÀ-ÖØ-öø-ÿ]*([,'\- ]{1}[a-zA-ZÀ-ÖØ-öø-ÿ]+)*[a-zA-ZÀ-ÖØ-öø-ÿ]*$"
   EnableClientScript="false"
   Display="None" />