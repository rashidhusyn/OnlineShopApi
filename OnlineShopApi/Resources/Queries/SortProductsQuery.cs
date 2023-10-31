using MediatR;
using StoreAPI.Models;

namespace OnlineShopApi.Resources.Queries
{
    public class SortProductsQuery : IRequest<IEnumerable<Product>>
    {
        public string SortOrder { get; set; }

    }
}
