using MediatR;
using MongoDB.Driver;
using OnlineShopApi.Resources.Queries;
using StoreAPI.Models;

namespace OnlineShopApi.Resources.Commands
{
    public class GetProductsInCategoryQueryHandler : IRequestHandler<GetProductsInCategoryQuery, IEnumerable<Product>>
    {
        private readonly IMongoCollection<Product> _productCollection;

        public GetProductsInCategoryQueryHandler(IMongoDatabase database)
        {
            _productCollection = database.GetCollection<Product>("Products");
        }

        public async Task<IEnumerable<Product>> Handle(GetProductsInCategoryQuery request, CancellationToken cancellationToken)
        {
            var category = request.Category;
            var products = await _productCollection
                .Find(product => product.CategoryId == category)
                .ToListAsync(cancellationToken);

            return products;
        }

    }
}
