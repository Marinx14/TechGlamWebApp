using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// Class containing various enums used in the web application.
/// </summary>
public class WebAppEnum
{
    /// <summary>
    /// Enum representing product categories.
    /// </summary>
    public enum Category
    {
        Rings,       // Represents rings
        Bracelets,   // Represents bracelets
        Watches      // Represents watches
    }

    /// <summary>
    /// Enum representing sizes for rings and bracelets.
    /// </summary>
    public enum SizeRingsBracelets
    {
        S,   // Small size
        M,   // Medium size
        L,   // Large size
        XL   // Extra-large size
    }

    /*/// <summary>
    /// Enum representing sizes for watches.
    /// </summary>
    /// da validare se aggiungere o meno gli orologi
    public enum SizeWatches
    {
        mm40 = 40,   // 40 mm size
        mm44 = 44,   // 44 mm size
        mm45 = 45    // 45 mm size
    }*/
}
