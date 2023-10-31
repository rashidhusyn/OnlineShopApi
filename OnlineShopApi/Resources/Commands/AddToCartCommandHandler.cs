using MediatR;
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
            }

            // Check if the product already exists in the cart.
            var existingItem = cart.Items.FirstOrDefault(item => item.ProductId == productId);
            if (existingItem != null)
            {
                
                existingItem.Quantity += quantity;
            }
            else
            {
               
                var newItem = new CartItem
                {
                    ProductId = productId,
                    Quantity = quantity
                };
                cart.Items.Add(newItem);
            }

            // Save or update the cart in the database.
            var options = new UpdateOptions { IsUpsert = true };
            var updateResult = await _cartCollection.ReplaceOneAsync(filter, cart, options, cancellationToken);

            return updateResult.IsAcknowledged && updateResult.ModifiedCount > 0;
        }

    }
}
