using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class PageMaster_MasterPage : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        tbNavSearch.Attributes.Add("onkeydown", "return (event.keyCode!=13);");
    }

    protected void PageLogin(Object sender, EventArgs e)
    {
        Response.Redirect("~/Pages/login.aspx");
    }
    protected void Search(Object sender, EventArgs e)
    {
        string str = tbNavSearch.Text;
        Response.Redirect("~/Default.aspx?Page=1&Filtre=" + str, false);
    }
}
