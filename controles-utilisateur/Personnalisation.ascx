<%@ Control Language="C#" %>
<script runat="server">
    public String Text                       { set { tb.Text = value; }
        get { return tb.Text; } }
    public TextBox Controle { get { return tb; } }
    public string CssClass { set { tb.CssClass = value; } }

    private void effacer(Object sender, EventArgs e)
    {
        tb.Text = " ";
    }
</script>

<style type="text/css">
   .sNomOuPrenom { width:250px; color:Blue; text-align:left; }
</style>

<asp:TextBox ID="tb" runat="server"
   MaxLength="25"
   CssClass="sNomOuPrenom"
    placeholder="Doit être présent"/>

