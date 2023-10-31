using MediatR;
using MongoDB.Driver;
using OnlineShopApi.Models;
using OnlineShopApi.Resources.Queries;

namespace OnlineShopApi.Resources.Commands
{
    public class GetAllCartsQueryHandler : IRequestHandler<GetAllCartsQuery, IEnumerable<Cart>>
    {
        private readonly IMongoCollection<Cart> _cartCollection;

        public GetAllCartsQueryHandler(IMongoDatabase database)
        {
            _cartCollection = database.GetCollection<Cart>("Carts");
        }

        public async Task<IEnumerable<Cart>> Handle(GetAllCartsQuery request, CancellationToken cancellationToken)
        {
            var carts = await _cartCollection.Find(_ => true).ToListAsync(cancellationToken);
            return carts;
        }

    }
}
