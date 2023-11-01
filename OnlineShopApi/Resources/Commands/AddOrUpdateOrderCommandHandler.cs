using MediatR;
using MongoDB.Driver;
using OnlineShopApi.Models;
using OnlineShopApi.Resources.Queries;

namespace OnlineShopApi.Resources.Commands
{
    public class AddOrUpdateOrderCommandHandler : IRequestHandler<AddOrUpdateOrderCommand, string>
    {
        private readonly IMongoCollection<Order> _orderCollection;

        public AddOrUpdateOrderCommandHandler(IMongoDatabase database)
        {
            _orderCollection = database.GetCollection<Order>("Orders");
        }

        public async Task<string> Handle(AddOrUpdateOrderCommand request, CancellationToken cancellationToken)
        {
            var existingOrder = await _orderCollection.Find(o => o.UserId == request.UserId).FirstOrDefaultAsync(cancellationToken);

            if (existingOrder != null)
            {

                if (request.NewProduct != null)
                {
                    existingOrder.Items.Add(request.NewProduct);
                    existingOrder.OrderDate = DateTime.UtcNow;

                    var filter = Builders<Order>.Filter.Eq(o => o.Id, existingOrder.Id);
                    var updateResult = await _orderCollection.ReplaceOneAsync(filter, existingOrder, new ReplaceOptions { IsUpsert = true }, cancellationToken);

                    if (updateResult.IsAcknowledged && updateResult.ModifiedCount > 0)
                    {
                        return existingOrder.Id;
                    }
                }
            }
            else
            {
                var newOrder = new Order
                {
                    UserId = request.UserId,
                    Items = request.Items,
                    Status = OrderStatus.Pending,
                    OrderDate = DateTime.UtcNow
                };

                await _orderCollection.InsertOneAsync(newOrder, null, cancellationToken);

                return newOrder.Id;
            }

            return null;
        }
   
    
    }
}
