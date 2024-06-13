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
        public virtual ICollection<ClonedProduct> clonedProduct { get; set; } = null;
    }

    public class ClonedProduct
    {
    }
}