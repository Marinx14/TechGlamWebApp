using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApp.Models
{
    public class Cart
    {
        public Guid IdCart { get; set; }
        public string IdUserAssociated { get; set; }
        public virtual ICollection<ClonedProduct> ClonedProducts { get; set; } = null;

        /*public Cart(Guid idCart, string IdUserAssociated, List<ClonedProduct> clonedProducts)
        {
            IdCart = idCart;
            IdUserAssociated = IdUserAssociated ?? throw new ArgumentNullException(nameof(IdUserAssociated));
            ClonedProducts = clonedProducts ?? throw new ArgumentNullException(nameof(clonedProducts));
        }*/

        public Cart()
        {
        }
    }

}