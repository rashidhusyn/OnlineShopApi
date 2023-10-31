using MediatR;
using StoreAPI.Models;

namespace OnlineShopApi.Resources.Queries
{
    public class UpdateProductQuery : IRequest<bool>
    {
        public string ProductId { get; set; }
        public Product UpdatedProduct { get; set; }
    }

}

