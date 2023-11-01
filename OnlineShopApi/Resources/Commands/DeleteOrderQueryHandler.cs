using MediatR;
using MongoDB.Driver;
using OnlineShopApi.Models;
using OnlineShopApi.Resources.Queries;

namespace OnlineShopApi.Resources.Commands
{
    public class DeleteOrderQueryHandler : IRequestHandler<DeleteOrderQuery, bool>
    {
        private readonly IMongoCollection<Order> _orderCollection;

        public DeleteOrderQueryHandler(IMongoDatabase database)
        {
            _orderCollection = database.GetCollection<Order>("Orders");
        }

        public async Task<bool> Handle(DeleteOrderQuery request, CancellationToken cancellationToken)
        {
            var filter = Builders<Order>.Filter.Eq(o => o.Id, request.OrderId);
            var update = Builders<Order>.Update.PullFilter(o => o.Items, i => i.ProductId == request.ProductId);

            var updateResult = await _orderCollection.UpdateOneAsync(filter, update, null, cancellationToken);

            return updateResult.IsAcknowledged && updateResult.ModifiedCount > 0;
        }   
    }
}
