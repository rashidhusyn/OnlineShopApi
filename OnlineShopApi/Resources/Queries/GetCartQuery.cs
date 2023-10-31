using MediatR;
using OnlineShopApi.Models;

namespace OnlineShopApi.Resources.Queries
{
    public class GetCartQuery : IRequest<Cart>
    {
        public string UserId { get; set; }

    }
}
