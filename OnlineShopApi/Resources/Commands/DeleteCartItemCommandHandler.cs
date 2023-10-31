using MediatR;
using MongoDB.Driver;
using OnlineShopApi.Models;
using OnlineShopApi.Resources.Queries;

namespace OnlineShopApi.Resources.Commands
{
    public class DeleteCartItemCommandHandler : IRequestHandler<DeleteCartCommand, bool>
    {
        private readonly IMongoCollection<Cart> _cartCollection;

        public DeleteCartItemCommandHandler(IMongoDatabase database)
        {
            _cartCollection = database.GetCollection<Cart>("Carts");
        }

        public async Task<bool> Handle(DeleteCartCommand request, CancellationToken cancellationToken)
        {
            var userId = request.UserId;
            var productId = request.ProductId;

            var filter = Builders<Cart>.Filter.Eq(c => c.UserId, userId);

            var cart = await _cartCollection.Find(filter).FirstOrDefaultAsync(cancellationToken);

            if (cart != null)
            {
                var itemToRemove = cart.Items.FirstOrDefault(i => i.ProductId == productId);

                if (itemToRemove != null)
                {
                    cart.Items.Remove(itemToRemove);

                    var updateResult = await _cartCollection.ReplaceOneAsync(c => c.Id == cart.Id, cart, cancellationToken: cancellationToken);

                    return updateResult.IsAcknowledged && updateResult.ModifiedCount > 0;
                }
            }

            return false;
        }

    }
}
