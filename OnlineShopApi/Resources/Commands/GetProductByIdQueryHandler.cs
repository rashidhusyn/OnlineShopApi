using MediatR;
using MongoDB.Driver;
using OnlineShopApi.Resources.Queries;
using StoreAPI.Models;

namespace OnlineShopApi.Resources.Commands
{
    public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, Product>
    {
        private readonly IMongoCollection<Product> _productCollection;

        public GetProductByIdQueryHandler(IMongoDatabase database)
        {
            _productCollection = database.GetCollection<Product>("Products");
        }

        public async Task<Product> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            return await _productCollection.Find(p => p.Id == request.ProductId).FirstOrDefaultAsync(cancellationToken);
        }

    }
}
