using MediatR;
using StoreAPI.Models;

namespace OnlineShopApi.Resources.Queries
{
    public class GetProductsInCategoryQuery : IRequest<IEnumerable<Product>>
    {
        public string Category { get; set; }

    }
}
