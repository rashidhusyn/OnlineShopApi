using MediatR;
using MongoDB.Driver;
using OnlineShopApi.Models;
using OnlineShopApi.Resources.Queries;
using StoreAPI.Models;

namespace OnlineShopApi.Resources.Commands
{
    public class DeleteUserQueryHandler : IRequestHandler<DeleteUserQuery, bool>
    {
        private readonly IMongoCollection<User> _userCollection;

        public DeleteUserQueryHandler(IMongoDatabase database)
        {
            _userCollection = database.GetCollection<User>("Users");
        }

        public async Task<bool> Handle(DeleteUserQuery request, CancellationToken cancellationToken)
        {
            if (int.TryParse(request.UserId, out int userId))
            {
                var result = await _userCollection.DeleteOneAsync(c => c.Id == userId, cancellationToken);
                return result.IsAcknowledged && result.DeletedCount > 0;
            }

            return false;
        }

    }
}
