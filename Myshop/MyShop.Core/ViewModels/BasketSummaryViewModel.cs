using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.Core.ViewModels
{
    public class BasketSummaryViewModel
    {
        public int  Basketcount { get; set; }
        public decimal BasketTotalValue { get; set; }

        public BasketSummaryViewModel()
        {

        }
        public BasketSummaryViewModel(int BaketCount, decimal basketTotal)
        {
            this.Basketcount = BaketCount;
            this.BasketTotalValue = basketTotal;

        }
    }
}
