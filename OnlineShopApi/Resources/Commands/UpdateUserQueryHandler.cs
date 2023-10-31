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

            var existingUser = await _userCollection
               .Find(p => p.Id == userId)
               .FirstOrDefaultAsync(cancellationToken);
            if (existingUser == null)
            {
                return false;
            }
            if (updatedUser.Name != null)
            {
                existingUser.Name = updatedUser.Name;
            }
            if (updatedUser.UserName != null)
            {
                existingUser.UserName = updatedUser.UserName;
            }
            if (updatedUser.Email != null)
            {
                existingUser.Email = updatedUser.Email;
            }
            if (updatedUser.Password != null)
            {
                existingUser.Password = updatedUser.Password;
            }

            var updateResult = await _userCollection.ReplaceOneAsync(
                p => p.Id == userId,
                existingUser,
                cancellationToken: cancellationToken
            );

            return updateResult.IsAcknowledged && updateResult.ModifiedCount > 0;

        }

    }
}
