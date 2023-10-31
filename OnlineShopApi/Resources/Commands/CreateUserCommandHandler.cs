using MediatR;
using MongoDB.Driver;
using OnlineShopApi.Models;
using OnlineShopApi.Resources.Queries;
using StoreAPI.Models;

namespace OnlineShopApi.Resources.Commands
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserQuery, string>
    {
        private readonly IMongoCollection<User> _userCollection;

        public CreateUserCommandHandler(IMongoDatabase database)
        {
            _userCollection = database.GetCollection<User>("Users");
        }

        public async Task<string> Handle(CreateUserQuery request, CancellationToken cancellationToken)
        {
            var user = new User
            {
                Name = request.Name,
                UserName = request.UserName,
                Email = request.Email,
                Password = request.Password,
            };

            await _userCollection.InsertOneAsync(user);

            return user.Id.ToString(); ;
        }

    }
}
