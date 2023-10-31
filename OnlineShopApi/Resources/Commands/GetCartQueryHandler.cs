using MediatR;
using MongoDB.Driver;
using OnlineShopApi.Models;
using OnlineShopApi.Resources.Queries;

namespace OnlineShopApi.Resources.Commands
{
    public class GetCartQueryHandler : IRequestHandler<GetCartQuery, Cart>
    {
        private readonly IMongoCollection<Cart> _cartCollection;

        public GetCartQueryHandler(IMongoDatabase database)
        {
            _cartCollection = database.GetCollection<Cart>("Carts");
        }

        public async Task<Cart> Handle(GetCartQuery request, CancellationToken cancellationToken)
        {
            var userId = request.UserId;
            var cart = await _cartCollection.Find(c => c.UserId == userId).FirstOrDefaultAsync(cancellationToken);
            return cart;
        }

    }
}
