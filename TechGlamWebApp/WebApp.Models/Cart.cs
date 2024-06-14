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
    }


}