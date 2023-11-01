using MediatR;
using MongoDB.Bson;
using MongoDB.Driver;
using OnlineShopApi.Models;
using OnlineShopApi.Resources.Queries;

namespace OnlineShopApi.Resources.Commands
{
    public class AddToCartCommandHandler : IRequestHandler<AddToCartCommand, bool>
    {
        private readonly IMongoCollection<Cart> _cartCollection;

        public AddToCartCommandHandler(IMongoDatabase database)
        {
            _cartCollection = database.GetCollection<Cart>("Carts");
        }

        public async Task<bool> Handle(AddToCartCommand request, CancellationToken cancellationToken)
        {
            var userId = request.UserId;
            var productId = request.ProductId;
            var quantity = request.Quantity;

            var filter = Builders<Cart>.Filter.Eq(c => c.UserId, userId);
            var cart = await _cartCollection.Find(filter).FirstOrDefaultAsync(cancellationToken);

            if (cart == null)
            {
                cart = new Cart
                {
                    UserId = userId,
                    Items = new List<CartItem>()
                };
                await _cartCollection.InsertOneAsync(cart, cancellationToken);
            }

            // Check if the product already exists in the cart.
            var existingItem = cart.Items.FirstOrDefault(item => item.ProductId == productId);

            if (existingItem != null)
            {
                var updateFilter = Builders<Cart>.Filter.And(
                    Builders<Cart>.Filter.Eq(c => c.UserId, userId),
                    Builders<Cart>.Filter.Eq("Items.ProductId", productId)
                );

                var update = Builders<Cart>.Update.Inc("Items.$.Quantity", quantity);
                var updateResult = await _cartCollection.UpdateOneAsync(updateFilter, update, null, cancellationToken);

                return updateResult.IsAcknowledged && updateResult.ModifiedCount > 0;
            }
            else
            {
                
                var newItem = new CartItem
                {
                    ProductId = productId,
                    Quantity = quantity
                };
                cart.Items.Add(newItem);

                var replaceResult = await _cartCollection.ReplaceOneAsync(filter, cart, new UpdateOptions { IsUpsert = true }, cancellationToken);

                return replaceResult.IsAcknowledged && replaceResult.ModifiedCount > 0;

                
            }

      
        }

    }
}
