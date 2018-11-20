<%@ Control Language="C#" ClassName="Personne_Ddl_Ajout" %>

<script runat="server">
    public DropDownList ControleDDL { get { return ddlPersonne; } }
    private void ChangerVisibilie(object sender, EventArgs e)
    {
        ddlPersonne.Visible = ddlPersonne.Visible ? false : true;
        tbPersonne.Visible = tbPersonne.Visible ? false : true;
    }
</script>
<div class="input-group">
  <span class="input-group-addon" id="basic-addon1">   
      <asp:LinkButton runat="server" ID="btnPersonne" OnClick="ChangerVisibilie" CausesValidation="false">
         <i class="glyphicon glyphicon-option-vertical"></i>
      </asp:LinkButton>
     
  </span>
   <asp:DropDownList ID="ddlPersonne" runat="server" CssClass="form-control"></asp:DropDownList>
   <asp:TextBox ID="tbPersonne" runat="server" Visible="false" CssClass="form-control" placeholder="Entrez un nouveau nom"></asp:TextBox>
</div> 


