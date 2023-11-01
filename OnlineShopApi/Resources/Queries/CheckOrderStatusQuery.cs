using MediatR;
using OnlineShopApi.Models;

namespace OnlineShopApi.Resources.Queries
{
    public class CheckOrderStatusQuery : IRequest<OrderStatus>
    {
        public string OrderId { get; set; }
    }
}
