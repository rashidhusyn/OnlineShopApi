using MediatR;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using OnlineShopApi.Models;
using OnlineShopApi.Resources.Queries;
using StoreAPI.Models;

namespace OnlineShopApi.Resources.Commands
{
    public class UpdateUserQueryHandler : IRequestHandler<UpdateUserQuery, bool>
    {
        private readonly IMongoCollection<User> _userCollection;

        public UpdateUserQueryHandler(IMongoDatabase database)
        {
            _userCollection = database.GetCollection<User>("Users");
        }

        public async Task<bool> Handle(UpdateUserQuery request, CancellationToken cancellationToken)
        {
            var userId = request.UserId;
            var updatedUser = request.UpdatedUser;

            var result = await _userCollection.ReplaceOneAsync(
                 c => c.Id.ToString() == userId,
                 updatedUser,
                 new ReplaceOptions(),
                 cancellationToken
             );

            return result.IsAcknowledged && result.ModifiedCount > 0;
        }

    }
}
