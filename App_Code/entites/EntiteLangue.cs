using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Description résumée de EntiteLangue
/// </summary>
public class EntiteLangue
{
    public int NoLangue { get; set; }
    public string Langue { get; set; }

    public EntiteLangue(int noLangue, string langue)
    {
        NoLangue = noLangue;
        Langue = langue;
    }
}