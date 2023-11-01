using MediatR;
using MongoDB.Driver;
using OnlineShopApi.Models;
using OnlineShopApi.Resources.Queries;

namespace OnlineShopApi.Resources.Commands
{
    public class CheckOrderStatusQueryHandler : IRequestHandler<CheckOrderStatusQuery, OrderStatus>
    {
        private readonly IMongoCollection<Order> _orderCollection;

        public CheckOrderStatusQueryHandler(IMongoDatabase database)
        {
            _orderCollection = database.GetCollection<Order>("Orders");
        }

        public async Task<OrderStatus> Handle(CheckOrderStatusQuery request, CancellationToken cancellationToken)
        {
            var orderId = request.OrderId;
            var filter = Builders<Order>.Filter.Eq(o => o.Id, orderId);
            var order = await _orderCollection.Find(filter).FirstOrDefaultAsync(cancellationToken);
            return order?.Status ?? OrderStatus.Unknown;
        }
    }
}
