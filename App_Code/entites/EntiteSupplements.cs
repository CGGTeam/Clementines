using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Description résumée de EntiteSupplements
/// </summary>
public class EntiteSupplements
{
    public int NoSupplement { get; set; }
    public string Description { get; set; }
    public EntiteSupplements(int noSupplement, string description)
    {
        NoSupplement = noSupplement;
        Description = description;
    }
}