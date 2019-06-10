using Myshop.Core.contracts;
using MyShop.Core.Models;
using MyShop.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.Services
{
    public class OrderService : Core.Contracts.IOrderService
    {
        IRepository<Order> orderContext;
        public OrderService(IRepository<Order> OrderContext) {
            this.orderContext = OrderContext;

        }

        public void CreateOder(Order baseOrder, List<BasketItemViewModel> basketItems)
        {
            foreach (var item in basketItems) {
                baseOrder.OrderItems.Add(new OrderItem() {
                    ProductId = item.Id,
                    Image = item.Image,
                    Price = item.Price,
                    ProductName = item.Name,
                    Quanity =item.Quanity


                });

            }
            orderContext.Insert(baseOrder);
            orderContext.Commit();
        }
    }
}
