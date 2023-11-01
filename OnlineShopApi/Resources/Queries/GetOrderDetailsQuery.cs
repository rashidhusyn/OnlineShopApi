using MediatR;
using OnlineShopApi.Models;

namespace OnlineShopApi.Resources.Queries
{
    public class GetOrderDetailsQuery : IRequest<Order>
    {
        public string OrderId { get; set; }
    }
}
