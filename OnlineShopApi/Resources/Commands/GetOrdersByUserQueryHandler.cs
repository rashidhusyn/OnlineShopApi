using MediatR;
using MongoDB.Driver;
using OnlineShopApi.Models;
using OnlineShopApi.Resources.Queries;

namespace OnlineShopApi.Resources.Commands
{
    public class GetOrdersByUserQueryHandler : IRequestHandler<GetOrdersByUserQuery, List<Order>>
    {
        private readonly IMongoCollection<Order> _orderCollection;

        public GetOrdersByUserQueryHandler(IMongoDatabase database)
        {
            _orderCollection = database.GetCollection<Order>("Orders");
        }

        public async Task<List<Order>> Handle(GetOrdersByUserQuery request, CancellationToken cancellationToken)
        {
            var userId = request.UserId;
            var filter = Builders<Order>.Filter.Eq(o => o.UserId, userId);
            var orders = await _orderCollection.Find(filter).ToListAsync(cancellationToken);
            return orders;
        }

    }
}
