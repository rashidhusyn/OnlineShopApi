using MediatR;
using MongoDB.Driver;
using OnlineShopApi.Models;

namespace OnlineShopApi.Resources.Queries
{
    public class GetOrderDetailsQueryHandler : IRequestHandler<GetOrderDetailsQuery, Order>
    {
        private readonly IMongoCollection<Order> _orderCollection;

        public GetOrderDetailsQueryHandler(IMongoDatabase database)
        {
            _orderCollection = database.GetCollection<Order>("Orders");
        }

        public async Task<Order> Handle(GetOrderDetailsQuery request, CancellationToken cancellationToken)
        {
            var orderId = request.OrderId;
            var filter = Builders<Order>.Filter.Eq(o => o.Id, orderId);
            var order = await _orderCollection.Find(filter).FirstOrDefaultAsync(cancellationToken);
            return order;
        }
    }
}
