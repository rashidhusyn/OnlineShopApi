using MediatR;
using OnlineShopApi.Models;
using StoreAPI.Models;

namespace OnlineShopApi.Resources.Queries
{
    public class GetUsersQuery:IRequest<IEnumerable<User>>
    {

    }
}
