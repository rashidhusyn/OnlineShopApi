using MediatR;
using MongoDB.Driver;
using OnlineShopApi.Models;
using OnlineShopApi.Resources.Queries;

namespace OnlineShopApi.Resources.Commands
{
    public class UpdateCartItemCommandHandler : IRequestHandler<UpdateCartItemCommand, bool>
    {
        private readonly IMongoCollection<Cart> _cartCollection;

        public UpdateCartItemCommandHandler(IMongoDatabase database)
        {
            _cartCollection = database.GetCollection<Cart>("Carts");
        }

        public async Task<bool> Handle(UpdateCartItemCommand request, CancellationToken cancellationToken)
        {
            var userId = request.UserId;
            var productId = request.ProductId;
            var newQuantity = request.Quantity;

            var filter = Builders<Cart>.Filter.Eq(c => c.UserId, userId);
            var cart = await _cartCollection.Find(filter).FirstOrDefaultAsync(cancellationToken);

            if (cart != null)
            {
                var itemToUpdate = cart.Items.FirstOrDefault(item => item.ProductId == productId);
                if (itemToUpdate != null)
                {
                    // Update the item's quantity.
                    itemToUpdate.Quantity = newQuantity;

                    // Save the updated cart in the database.
                    var updateResult = await _cartCollection.ReplaceOneAsync(filter, cart, cancellationToken: cancellationToken);

                    return updateResult.IsAcknowledged && updateResult.ModifiedCount > 0;
                }
            }

            return false; // Item or cart not found or not updated.
        }
    }

}
