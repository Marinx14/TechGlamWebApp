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
        public string IdUser { get; set; }
        public List<ClonedProduct> ClonedProducts { get; set; }

        public Cart(Guid idCart, string idUser, List<ClonedProduct> clonedProducts)
        {
            IdCart = idCart;
            IdUser = idUser ?? throw new ArgumentNullException(nameof(idUser));
            ClonedProducts = clonedProducts ?? throw new ArgumentNullException(nameof(clonedProducts));
        }
    }
}