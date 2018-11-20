using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Description résumée de EntiteFormat
/// </summary>
public class EntiteFormat
{
    public int NoFormat { get; set; }
    public string Description { get; set; }
    
    //Constructeur
    public EntiteFormat(int noFormat, string description)
    {
        NoFormat = noFormat;
        Description = description;
    }
}