using MediatR;
using StoreAPI.Models;

namespace OnlineShopApi.Resources.Queries
{
    public class GetProductsQuery:IRequest<IEnumerable<Product>>
    {

    }
}
