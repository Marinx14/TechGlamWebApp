using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApp.enumeration
{


    /// <summary>
    /// Class containing various enums used in the web application.
    /// </summary>
    ///
    public class WebAppEnum
    {
        /// <summary>
        /// Enum representing product categories.
        /// </summary>
        public enum Category
        {
            Rings, // Represents rings
            Bracelets, // Represents bracelets
            Watches // Represents watches
        }

        /// <summary>
        /// Enum representing sizes for rings and bracelets.
        /// </summary>
        public enum Size
        {
            S, // Small size
            M, // Medium size
            L, // Large size
            XL // Extra-large size
        }
    }
}
