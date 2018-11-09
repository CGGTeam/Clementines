using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

/// <summary>
/// Summary description for librairie
/// </summary>
public class librairie : System.Web.UI.Page
{
    public Panel divDYN(Control Conteneur, String strID)
    {
        Panel panel = new Panel();
        panel.ID = strID;
        Conteneur.Controls.Add(panel);
        return panel;
    }
    public Panel divDYN(Control Conteneur, String strID, String strStyle)
    {
        Panel panel = divDYN(Conteneur, strID);
        panel.CssClass = strStyle;
        return panel;
    }
    public ImageButton btnImgDYN(Control Conteneur, String strID, String strImg, String strClass)
    {
        ImageButton img = new ImageButton
        {
            ID = strID,
            CssClass = strClass,
            ImageUrl = strImg,
        };
        Conteneur.Controls.Add(img);
        return img;
    }
    public Image imgDYN(Control Conteneur, String strID, String strImg, String strClass)
    {
        Image img = new Image
        {
            ID = strID,
            CssClass = strClass,
            ImageUrl = strImg,
        };
        Conteneur.Controls.Add(img);
        return img;
    }
    public Label lblDYN(Control Conteneur, String strID, String strValeur)
    {
        Label lbl = new Label()
        {
            ID = strID,
            Text = strValeur
        };
        Conteneur.Controls.Add(lbl);
        return lbl;
    }
    public void brDYN(Control Conteneur)
    {
        Literal br = new Literal();
        br.Text = "<br />";
        Conteneur.Controls.Add(br);
    }
    public void brDYN(Control Conteneur, Int16 intNb)
    {

        Literal br = new Literal();
        br.Text = "";
        for (Int16 i = 1; i <= intNb; i++)
        {
            br.Text += "<br />";
        }
        Conteneur.Controls.Add(br);
    }
    public Table tableDYN(Control Conteneur,String strID,String strStyle)
    {
        Table t = new Table()
        {
            ID = strID,
            CssClass = strStyle
        };
        Conteneur.Controls.Add(t);
        return t;
    }
    public TableCell tdDYN(TableRow Conteneur,String strID,String strStyle)
    {
        TableCell t = new TableCell()
        {
            ID = strID,
            CssClass = strStyle
        };
        Conteneur.Controls.Add(t);
        return t;
    }
    public TableRow trDYN(Table Conteneur)
    {
        TableRow t = new TableRow();
        Conteneur.Controls.Add(t);
        return t;
    }


}