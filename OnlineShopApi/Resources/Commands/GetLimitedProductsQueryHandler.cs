using MediatR;
using MongoDB.Driver;
using OnlineShopApi.Resources.Queries;
using StoreAPI.Models;

namespace OnlineShopApi.Resources.Commands
{
    public class GetLimitedProductsQueryHandler : IRequestHandler<GetLimitedProductsQuery, IEnumerable<Product>>
    {
        private readonly IMongoCollection<Product> _productCollection;

        public GetLimitedProductsQueryHandler(IMongoDatabase database)
        {
            _productCollection = database.GetCollection<Product>("Products");
        }

        public async Task<IEnumerable<Product>> Handle(GetLimitedProductsQuery request, CancellationToken cancellationToken)
        {
            var limit = request.Limit;
            var products = await _productCollection
                .Find(_ => true)
                .Limit(limit)
                .ToListAsync(cancellationToken);

            return products;
        }
    }
}
