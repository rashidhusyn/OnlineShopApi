using MediatR;
using OnlineShopApi.Models;

namespace OnlineShopApi.Resources.Queries
{
    public class AddOrUpdateOrderCommand : IRequest<string>
    {
        public string UserId { get; set; }
        public List<OrderItem> Items { get; set; }
        public OrderItem NewProduct { get; set; }
    }
}
