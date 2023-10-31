using MediatR;
using OnlineShopApi.Models;

namespace OnlineShopApi.Resources.Queries
{
    public class GetAllCartsQuery : IRequest<IEnumerable<Cart>>
    {
    }
}
