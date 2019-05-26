using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.Core.Models
{
    public class ProductCatagory
    {
        public string  Id { get; set; }
        public string Catagory { get; set; }

        public ProductCatagory()
        {
            this.Id = Guid.NewGuid().ToString();
        }


    }
}
