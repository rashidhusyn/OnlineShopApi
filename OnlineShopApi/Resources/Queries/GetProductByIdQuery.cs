using MediatR;
using StoreAPI.Models;

namespace OnlineShopApi.Resources.Queries
{
    public class GetProductByIdQuery:IRequest<Product>
    {
        public string ProductId { get; set; }
    }
}
