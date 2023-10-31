using MediatR;
using StoreAPI.Models;

namespace OnlineShopApi.Resources.Queries
{
    public class GetLimitedProductsQuery : IRequest<IEnumerable<Product>>
    {
        public int Limit { get; set; }

    }
}
