<%@ Control Language="C#" ClassName="Personne_Ddl_Ajout" %>

<script runat="server">
    public TextBox ControleTextBox { get { return tbPersonne; } }
    public DropDownList ControleDDL { get { return ddlPersonne; } }
    public CustomValidator ControleCustomValidator {get { return cv1; }  }
    public string CssClass { set { icon.CssClass = value; } }
    private void ChangerVisibilie(object sender, EventArgs e)
    {
        ddlPersonne.Visible = ddlPersonne.Visible ? false : true;
        tbPersonne.Visible = tbPersonne.Visible ? false : true;

        icon.CssClass = icon.CssClass == "glyphicon glyphicon-option-horizontal" ?"glyphicon glyphicon-option-vertical" : "glyphicon glyphicon-option-horizontal";
    }

    void maValidation(Object sender, ServerValidateEventArgs Arguments)
    {
        if (string.IsNullOrEmpty(Arguments.Value) && tbPersonne.Visible == true)
        {
            Arguments.IsValid = false;
        }
        else
        {
            Arguments.IsValid = true;
        }
    }
</script>



<div class="input-group">
  <span class="input-group-addon" id="basic-addon1">   
      <asp:LinkButton runat="server" ID="btnPersonne" OnClick="ChangerVisibilie" CausesValidation="false">
         <asp:Label runat="server" ID="icon" class="glyphicon glyphicon-option-vertical"></asp:Label>
      </asp:LinkButton>
     
  </span>
   <asp:DropDownList ID="ddlPersonne" runat="server" CssClass="form-control"></asp:DropDownList>
   <asp:TextBox ID="tbPersonne" runat="server" Visible="false" CssClass="form-control" placeholder="Entrez un nouveau nom"></asp:TextBox> 
</div> 
<asp:CustomValidator ID="cv1" runat="server"
        ControlToValidate="tbPersonne"
        OnServerValidate="maValidation"
        EnableClientScript="false"
        ValidateEmptyText="true"
        Style="color:red" 
        Display="dynamic"
        ErrorMessage="Vous devez entrer un nom, sinon choisir l'un de ceux offert" />


