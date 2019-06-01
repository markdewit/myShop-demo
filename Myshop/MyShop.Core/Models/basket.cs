using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.Core.Models
{
    public class basket :BaseEntity
    {
        public virtual ICollection<BasketItem> BasketItems { get; set; }

        public basket()
        {
            this.BasketItems = new List<BasketItem>();

        }
    }
}
