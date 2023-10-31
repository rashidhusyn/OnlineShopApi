using MediatR;
using MongoDB.Driver;
using OnlineShopApi.Resources.Queries;
using StoreAPI.Models;

namespace OnlineShopApi.Resources.Commands
{
    public class SortProductsQueryHandler : IRequestHandler<SortProductsQuery, IEnumerable<Product>>
    {
        private readonly IMongoCollection<Product> _productCollection;

        public SortProductsQueryHandler(IMongoDatabase database)
        {
            _productCollection = database.GetCollection<Product>("Products");
        }

        public async Task<IEnumerable<Product>> Handle(SortProductsQuery request, CancellationToken cancellationToken)
        {
            var order = request.SortOrder;
            var products = await _productCollection
                .Find(_ => true)
                .Sort(order == "asc" ? Builders<Product>.Sort.Ascending("Title") : Builders<Product>.Sort.Descending("Title"))
                .ToListAsync(cancellationToken);

            return products;
        }

    }
}
