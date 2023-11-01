using MediatR;
using OnlineShopApi.Models;

namespace OnlineShopApi.Resources.Queries
{
    public class GetOrdersByUserQuery : IRequest<List<Order>>
    {
        public string UserId { get; set; }
    }
}
